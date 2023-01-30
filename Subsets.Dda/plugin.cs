#if TOOLS
using Godot;
using System;

[Tool]
public class plugin : EditorPlugin
{
    public override void _EnterTree()
    {
        var texture = GD.Load<Texture>("icon.png");
        AddCustomType("FloatVariable", "Resource", GD.Load<Script>("addons/Subsets.Dda/Scripts/FloatVariable.cs"), texture);
        AddCustomType("TestA", "Resource", GD.Load<Script>("addons/Subsets.Dda/Scripts/TestA.cs"), texture);
    }

    public override void _ExitTree()
    {
        RemoveCustomType("FloatVariable");
        RemoveCustomType("TestA");
    }
}
#endif