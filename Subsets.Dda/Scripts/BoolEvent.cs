using Godot;
using MonoCustomResourceRegistry;
using Nprosoft.Cards.Client.Models;
using Nprosoft.Cards.Shared.Core;

namespace Subsets.Dda
{
    public class BoolEventProperties : BaseProperties
    {
        [PropertySerialize]
        public bool Value;
    }
    
    [RegisteredType(nameof(BoolEvent), "", nameof(Resource))]
    [Tool]
    public class BoolEvent : BaseEvent<BoolEventProperties>
    {
    }
}