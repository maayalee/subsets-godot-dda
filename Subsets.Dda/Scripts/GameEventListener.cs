using System.Collections.Generic;
using Godot;
using MonoCustomResourceRegistry;
using Nprosoft.Cards.Client.Models;

namespace Subsets.Dda
{
    [RegisteredType(nameof(GameEventListener), "", nameof(Node))]
    [Tool]
    public class GameEventListener : BaseEventListener<GameEventProperties>
    {
        protected override void DynamicInvoke(GodotEventResponse response, GameEventProperties dynamicValueIsVoid)
        {
            List<DynamicInvokeValue> invokeValue = new List<DynamicInvokeValue>();
            response.Invoke(invokeValue);
        }
        
    }
}