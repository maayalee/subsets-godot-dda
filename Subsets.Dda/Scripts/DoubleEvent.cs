using Godot;
using MonoCustomResourceRegistry;
using Nprosoft.Cards.Client.Models;
using Nprosoft.Cards.Shared.Core;

namespace Subsets.Dda
{
    public class DoubleEventVariable : BaseProperties
    {
        [PropertySerialize]
        public double Value;
    }
    
    [RegisteredType(nameof(DoubleEvent), "", nameof(Resource))]
    public class DoubleEvent : BaseEvent<DoubleEventVariable>
    {
        public void Raise(double value)
        {
            this.Value.Value = value;
            this.Raise();
        }
    }
}