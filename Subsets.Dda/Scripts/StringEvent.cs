using System;
using Godot;
using MonoCustomResourceRegistry;
using Nprosoft.Cards.Client.Models;
using Nprosoft.Cards.Shared.Core;

namespace Subsets.Dda
{
    public class StringEventProperties : BaseProperties
    {
        [PropertySerialize]
        public string Value { get; set; }
    }
    
    [RegisteredType(nameof(StringEvent), "", nameof(Resource))]
    [Tool]
    public class StringEvent : BaseEvent<StringEventProperties>
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
            Value.Value = args[0] as string;
        }
        
        public void Raise(string value)
        {
            GD.Print("BaseEvent::Raise(Node value)");
        
            this.Value.Value = value;
            this.Raise();
        }
    }
}