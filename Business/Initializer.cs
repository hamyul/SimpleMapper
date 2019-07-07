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

            return sourceProperties.Where(a => destinationProperties.Any(b => b.Name == a.Name && b.PropertyType == a.PropertyType)).ToList();
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
    }
}