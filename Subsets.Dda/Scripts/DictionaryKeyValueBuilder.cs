using System;
using System.Reflection;
using Cards2.addons.Subsets.Dda.Scripts;
using Godot;
using Godot.Collections;
using Subsets.Dda;
using Array = Godot.Collections.Array;

namespace Nprosoft.Cards.Shared.Core
{
    public class DictionaryKeyValueBuilder
    {
        public delegate bool IsInitializeMethod(PropertyInfo field);

        private IsInitializeMethod IsInitialize;
        
        public DictionaryKeyValueBuilder()
        {
            this.IsInitialize = lPropertyInfo => true;
        }

        public DictionaryKeyValueBuilder(IsInitializeMethod method)
        {
            this.IsInitialize = method;
        }
        
        public void Initialize<TProperties>(Dictionary<string, object> target)
        {
            PropertyInfo[] infos = typeof(TProperties).GetProperties();
            foreach (PropertyInfo info in infos)
            {
                PropertyInfo propertyInfo = typeof(TProperties).GetProperty(info.Name);
                if (IsInitialize(propertyInfo))
                {
                    object value = null;
                    if (info.PropertyType == typeof(System.Collections.IList) ||
                        info.PropertyType == typeof(System.Collections.IDictionary) ||
                        info.PropertyType == typeof(Array) ||
                        info.PropertyType == typeof(Dictionary) ||
                        info.PropertyType == typeof(NodePath))
                    {
                        value = Activator.CreateInstance(info.PropertyType);
                    }
                    else if (info.PropertyType.IsSubclassOf(typeof(Resource)))
                    {
                        // 에디터가 아닌 코드에서 생성한 경우 그냥 빈 Resource 형태로만 저장되기
                        // 때문에 동적으로 사용할 수 없다. 그냥 드래그 앤 드랍을 편히 하기 위한 용도로 봐야 한다.
                        value = Activator.CreateInstance(info.PropertyType);
                    }
                    else if (info.PropertyType == typeof(int) || 
                             info.PropertyType == typeof(double))
                        value = 0;
                    else if (info.PropertyType == typeof(string))
                        value = "";
                    else if (info.PropertyType == typeof(bool))
                        value = false;
                    else
                        value = null;
                    target[info.Name] = value;
                }
            }
        }
    }
}