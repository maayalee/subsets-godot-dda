namespace Cards2.addons.Subsets.Dda.Scripts
{
    public interface IEvent
    {
        void RaiseWithProperty(Godot.Collections.Dictionary<string, object> propertyValues);
        void Raise();
        void Raise(object sender);
        object[] ToArgs();
        void FromArgs(object[] args);
    }
}