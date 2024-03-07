using System.Collections.Generic;
using Godot;
using MonoCustomResourceRegistry;

namespace Subsets.Dda
{
    [RegisteredType(nameof(BoolEventListener), "", nameof(Node))]
    [Tool]
    public class BoolEventListener : BaseEventListener<BoolEventProperties>
    {
        protected override void DynamicInvoke(GodotEventResponse response, BoolEventProperties dynamicValue)
        {
            List<DynamicInvokeValue> invokeValue = new List<DynamicInvokeValue>();
            invokeValue.Add(new DynamicInvokeValue()
            {
                Type = Variant.Type.Bool,
                Value = dynamicValue.Value
            });
            response.Invoke(invokeValue);
        }
        
    }
}