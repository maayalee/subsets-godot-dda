using Godot;
using Godot.Collections;
using MonoCustomResourceRegistry;
using Nprosoft.Cards.Client.Models;

namespace Subsets.Dda
{
    [RegisteredType(nameof(EventVariable), "", nameof(Node))]
    [Tool]
    public class EventVariable : Node
    {
        [Export]
        public Resource Event;

        [Export] public string Value;
    }

}