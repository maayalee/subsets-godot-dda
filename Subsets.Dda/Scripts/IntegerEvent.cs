using System;
using Godot;
using MonoCustomResourceRegistry;
using Nprosoft.Cards.Client.Models;
using Nprosoft.Cards.Shared.Core;

namespace Subsets.Dda
{
    public class IntegerEventProperties : BaseProperties
    {
        [PropertySerialize]
        public int Value { get; set; }
    }
    
    [RegisteredType(nameof(IntegerEvent), "", nameof(Resource))]
    [Tool]
    public class IntegerEvent : BaseEvent<IntegerEventProperties>
    {
        public override object[] ToArgs()
        {
            object[] args = new object[]
            {
                Value.Value
            };
            return args;
        }
        
        public override void FromArgs(object[] args)
        {
            Value.Value = Convert.ToInt32(args[0]);
        }

        public void Raise(int value)
        {
            GD.Print("BaseEvent::Raise(Node value)");
        
            this.Value.Value = value;
            this.Raise();
        }

        public void Raise(IntegerVariable value)
        {
            this.Value.Value = value.Value;
            this.Raise();
        }
    }
}