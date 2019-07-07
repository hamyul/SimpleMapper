/*
MIT License

Copyright (c) 2019 Hammond Soares

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

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

            return sourceProperties.Where(a => destinationProperties.Any(b => b.Name == a.Name && IsCompatible(source, destination, b.Name))).ToList();
        }

        protected bool IsCompatible(object source, object destination, string propertyName)
        {
            var sourceType = GetPropertyType(source, propertyName);
            var destinationType = GetPropertyType(destination, propertyName);

            var areSameType = destinationType == sourceType;
            var sourceTypeIsSubclass = sourceType.IsSubclassOf(destinationType);
            var areEquivalent = destinationType.IsEquivalentTo(sourceType);
            var isDestinationAssignable = destinationType.IsAssignableFrom(sourceType);

            return areSameType || sourceTypeIsSubclass || areEquivalent || isDestinationAssignable;
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