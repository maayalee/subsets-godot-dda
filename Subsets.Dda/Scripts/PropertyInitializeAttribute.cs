using System;

namespace Cards2.addons.Subsets.Dda.Scripts
{
    public enum PropertyInitialize
    {
        Dynamic,
        Static
    }
    
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class PropertyInitializeAttribute : Attribute
    {
        private PropertyInitialize initialize;
        public PropertyInitializeAttribute(PropertyInitialize initialize)
        {
            this.initialize = initialize;
        }

        public bool IsStatic()
        {
            return initialize == PropertyInitialize.Static;
        }
    }
}