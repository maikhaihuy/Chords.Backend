using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Chords.CoreLib.Utils
{
    public static class ReflectionHelpers
    {
        public static List<PropertyInfo> GetKeyAttributes(Type type)
        {
            return type.GetProperties().Where(property => Attribute.IsDefined(property, typeof(KeyAttribute))).ToList();
        }

        public static object[] GetKeyValues(object entity)
        {
            List<object> keyValues = new List<object>();

            List<PropertyInfo> keyProperties = entity.GetType().GetProperties().Where(property => Attribute.IsDefined(property, typeof(KeyAttribute))).ToList();
            foreach (PropertyInfo propertyInfo in keyProperties)
            {
                object keyValue = propertyInfo.GetValue(entity, null);
                if (keyValue == null) continue;
                keyValues.Add(keyValue);
            }
            
            return keyValues.ToArray();
        }
        
        public static T MergeFieldsChanged<T>(T source, T destination)
        {
            // only update not null
            foreach (var property in typeof(T).GetProperties())
            {
                var newValue = property.GetValue(source, null);
                var oldValue = property.GetValue(destination, null);
                //hard bypass foreign key
                if (newValue == null || newValue == oldValue
                                     || (int.TryParse($"{newValue}", out int valIn) && valIn == 0)
                                     || (newValue is DateTime dtValue && dtValue == default)) continue;
                        
                property.SetValue(destination, newValue, null);
            }

            return destination;
        }
    }

}