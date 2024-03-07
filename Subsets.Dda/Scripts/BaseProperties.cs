using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Godot;
using Subsets.Dda;
using Array = Godot.Collections.Array;
using Object = System.Object;

namespace Subsets.Dda
{
    public class BaseProperties : IDuplicate
    {
        private PropertyExchanger exchanger;
        protected BaseProperties()
        {
            PropertyExchangerSettings settings = new PropertyExchangerSettings();
            settings.UexExportFilter = false;
            // @todo BaseXXX들의 Properties들도 모두 PropertySerzilze 속성만 익스포트해야되지 않나?
            /*
            settings.UexExportFilter = true;
            settings.ExportAttributeTypes.Add(typeof(PropertySerializeAttribute));
            */
            exchanger = new PropertyExchanger(settings);
        }

        public Dictionary<string, object> ToDictionary<TProperties>()
        {
            return exchanger.Export<TProperties>(this);
        }
        
        public void FromDictionary<TProperties>(System.Collections.Generic.Dictionary<string, object> properties)
        {
            exchanger.Import<TProperties>(properties, this);
        }
        
        public void FromDictionary<TProperties>(Godot.Collections.Dictionary<string, object> properties)
        {
            exchanger.Import<TProperties>(properties, this);
        }
        
        public virtual object Duplicate()
        {
            return this;
        }
    }
}