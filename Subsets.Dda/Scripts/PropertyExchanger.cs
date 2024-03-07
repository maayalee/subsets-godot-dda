using System;
using System.Collections.Generic;
using System.Reflection;
using Godot;
using Godot.Collections;
using Nprosoft.Cards.Shared.Core;
using Array = Godot.Collections.Array;

namespace Subsets.Dda
{
    public class PropertyExchangerSettings
    {
        public bool UexExportFilter { get; set; } = true;
        public List<Type> ExportAttributeTypes { get; set; } = new List<Type>();
    }
    
    // @todo 재귀 함수로 하위의 프로퍼티들에 대해서도 import, export 할 수 있도록 변경
    public class PropertyExchanger
    {
        private PropertyExchangerSettings settings;
        
        public PropertyExchanger(PropertyExchangerSettings settings)
        {
            this.settings = settings;
        }
        
        public PropertyExchanger()
        {
            settings = new PropertyExchangerSettings();
            settings.UexExportFilter = true;
            settings.ExportAttributeTypes = new List<Type>()
            {
                typeof(PropertySerializeAttribute)
            };
        }
        
        public System.Collections.Generic.Dictionary<string, object> Export<TProperties>(object target)
        {
            System.Collections.Generic.Dictionary<string, object> result = new System.Collections.Generic.Dictionary<string, object>();
            PropertyInfo[] infos = typeof(TProperties).GetProperties();
            foreach (PropertyInfo info in infos)
            {
                PropertyInfo propertyInfo = typeof(TProperties).GetProperty(info.Name);
                if (IsExportAttribute(propertyInfo))
                {
                    result.Add(info.Name, propertyInfo.GetValue(target));
                }
            }
            return result;
        }


        public object Import<TProperties>(Godot.Collections.Dictionary<string, object> properties, object target,
            bool initializeNotExistProperties = true)
        {
            System.Collections.Generic.Dictionary<string, object> dictionary =
                new System.Collections.Generic.Dictionary<string, object>();
            foreach (KeyValuePair<string, object> pair in properties)
            {
                dictionary.Add(pair.Key, pair.Value);
            }
            return Import<TProperties>(dictionary, target, initializeNotExistProperties);
        }
        
        public object Import<TProperties>(System.Collections.Generic.Dictionary<string, object> properties, object target, bool initializeNotExistProperties = true)
        {
            PropertyInfo[] infos = typeof(TProperties).GetProperties();
            foreach (PropertyInfo info in infos)
            {
                PropertyInfo propertyInfo = typeof(TProperties).GetProperty(info.Name);
                if (IsExportAttribute(propertyInfo))
                {
                    object value = null;
                    if (properties.ContainsKey(info.Name))
                    {
                        value = properties[info.Name];
                    }

                    if (value != null)
                    {
                        if (info.PropertyType.IsEnum)
                        {
                            value = Enum.Parse(info.PropertyType, value.ToString(), true);
                        }
                        if (value.GetType() == typeof(Int64) && propertyInfo.PropertyType == typeof(Int32))
                        {
                            value = Convert.ToInt32(value);
                        }

                        if ((value.GetType() == typeof(Int32) || value.GetType() == typeof(Int64)) && propertyInfo.PropertyType == typeof(Double))
                        {
                            value = Convert.ToDouble(value);
                        }
                        //
                        
                        if (propertyInfo.PropertyType != value.GetType())
                        {
                            GD.PrintErr(String.Format(
                                "PropertyExchanger::Import::property value type is mismatch. properties type: {0}, name: {1}, " +
                                "property_type: {2}, " +
                                "value_type: {3}", 
                                typeof(TProperties).Name, info.Name, propertyInfo.PropertyType, value.GetType()));
                            continue; 
                        }
                    }
                                        
                    propertyInfo.SetValue(target, value);    
                }
            }

            return target;
        }
        
        
        bool IsExportAttribute(PropertyInfo propertyInfo)
        {
            if (!settings.UexExportFilter)
                return true;
                
            foreach (Type exportType in settings.ExportAttributeTypes)
            {
                if (propertyInfo.IsDefined(exportType))
                    return true;
            }
            return false;
        }
    }
}