using Godot;
using System;
using MonoCustomResourceRegistry;
using Nprosoft.Cards.Client.Models;

namespace Subsets.Dda
{
    public class GameEventProperties : BaseProperties
    {
    }
    
    [RegisteredType(nameof(GameEvent), "", nameof(Resource))]
    public class GameEvent : BaseEvent<GameEventProperties>
    {
        public GameEvent()
        {
            //GD.Print("GameEvent::GameEvent");
        }
        
        public override object[] ToArgs()
        {
            return new object[]
            {
                Newtonsoft.Json.JsonConvert.SerializeObject(new Directory())
            };
        }

        public override void FromArgs(object[] args)
        {
        }
    }
}