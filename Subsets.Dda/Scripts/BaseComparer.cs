using Godot;

namespace Subsets.Dda
{
    public abstract class BaseComparer<TType> : Node
    {
        [Export] public TType Target;
    }
}