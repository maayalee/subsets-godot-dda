using System.Collections.Generic;
using Godot;
using MonoCustomResourceRegistry;

namespace Subsets.Dda
{
    public enum StringCompare
    {
        Equal,
        Contains,
        IsNot,
        Updated,
    }
    
    
    [RegisteredType(nameof(StringEventListener), "", nameof(Node))]
    [Tool]
    public class StringEventListener : BaseEventListener<StringEventProperties>
    {
        protected override void DynamicInvoke(GodotEventResponse response, StringEventProperties dynamicValue)
        {
            List<DynamicInvokeValue> invokeValue = new List<DynamicInvokeValue>();
            invokeValue.Add(new DynamicInvokeValue()
            {
                Type = Variant.Type.String,
                Value = dynamicValue.Value
            });
            response.Invoke(invokeValue);
        }
        
    }
}