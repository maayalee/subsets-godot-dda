using System.Collections.Generic;
using Godot;
using MonoCustomResourceRegistry;

namespace Subsets.Dda
{
    
    [RegisteredType(nameof(DoubleEventListener), "", nameof(Node))]
    [Tool]
    public class DoubleEventListener : BaseEventListener<DoubleEventVariable>
    {
        protected override void DynamicInvoke(GodotEventResponse response, DoubleEventVariable dynamicValue)
        {
            List<DynamicInvokeValue> invokeValue = new List<DynamicInvokeValue>();
            invokeValue.Add(new DynamicInvokeValue()
            {
                Type = Variant.Type.Real,
                Value = dynamicValue.Value
            });
            response.Invoke(invokeValue);
        }
        
    }
}