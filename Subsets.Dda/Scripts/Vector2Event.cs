
using Godot;
using Godot.Collections;
using MonoCustomResourceRegistry;
using Nprosoft.Cards.Client.Models;
using Nprosoft.Cards.Shared.Core;
using Subsets.Dda;

namespace Subsets.Dda
{
    public class Vector2EventProperties : BaseProperties
    {
        [PropertySerialize] public Vector2 Value;
    }
    
    [RegisteredType(nameof(Vector2Event), "", nameof(Resource))]
    [Tool]
    public class Vector2Event : BaseEvent<Vector2EventProperties>
    {
        public void Raise(Vector2 value)
        {
            Value.Value = value;
            Raise();
        }
    }
}