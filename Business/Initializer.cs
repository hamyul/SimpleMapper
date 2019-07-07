using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Business
{
    public class Initializer
    {
        public Destination Map<Destination>(object source)
            where Destination : new()
        {
            var destination = new Destination();
            var similarProperties = GetSimilarProperties(source, destination);
            similarProperties.ForEach(property => SetValue(source, destination, property.Name));

            return destination;
        }

        protected void SetValue(object source, object destination, string propertyName)
        {
            var destinationProperty = GetProperty(destination, propertyName);
            var sourceProperty = GetProperty(source, propertyName);
            var sourcePropertyValue = sourceProperty.GetValue(source);

            destinationProperty.SetValue(destination, sourcePropertyValue);
        }

        protected List<PropertyInfo> GetSimilarProperties(object source, object destination)
        {
            var sourceProperties = GetProperties(source);
            var destinationProperties = GetProperties(destination);

            var output = new List<PropertyInfo>();
            var validProperties = sourceProperties.Where(a => destinationProperties.Any(b => b.Name == a.Name)).ToList();
            foreach(var property in validProperties)
            {
                var sourceType = GetPropertyType(source, property.Name);
                var destinationType = GetPropertyType(destination, property.Name);

                var areSameType = destinationType == sourceType;
                var sourceTypeIsSubclass = sourceType.IsSubclassOf(destinationType);
                var areEquivalent = destinationType.IsEquivalentTo(sourceType);
                var isDestinationAssignable = destinationType.IsAssignableFrom(sourceType);


                var isValid = areSameType || sourceTypeIsSubclass || areEquivalent || isDestinationAssignable;
                if (!isValid)
                    continue;

                output.Add(property);
            }

            return output;
        }

        protected PropertyInfo[] GetProperties(object obj)
        {
            var sourceType = obj.GetType();
            return sourceType.GetProperties();
        }

        protected PropertyInfo GetProperty(object obj, string propertyName)
        {
            return obj.GetType().GetProperty(propertyName);
        }

        protected Type GetPropertyType(object obj, string propertyName)
        {
            return obj.GetType().GetProperty(propertyName).PropertyType;
        }
    }
}