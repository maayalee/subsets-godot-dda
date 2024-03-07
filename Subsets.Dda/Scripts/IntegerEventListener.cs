using System.Collections.Generic;
using Godot;
using MonoCustomResourceRegistry;

namespace Subsets.Dda
{
    public enum IntegerCompare
    {
        Equal,
        Contains,
        IsNot,
        Updated,
    }
    
    [RegisteredType(nameof(IntegerEventListener), "", nameof(Node))]
    [Tool]
    public class IntegerEventListener : BaseEventListener<IntegerEventProperties>
    {
        protected override void DynamicInvoke(GodotEventResponse response, IntegerEventProperties dynamicValue)
        {
            List<DynamicInvokeValue> invokeValue = new List<DynamicInvokeValue>();
            invokeValue.Add(new DynamicInvokeValue()
            {
                Type = Variant.Type.Int,
                Value = dynamicValue.Value
            });
            response.Invoke(invokeValue);
        }
        
    }
}