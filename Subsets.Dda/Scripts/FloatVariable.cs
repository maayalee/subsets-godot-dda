using Godot;
using System;

public class FloatVariable : Resource
{
    [Export]
    public float InitialValue
    {
        get
        {
            return this. initialValue;
        }
        set
        {
            this.InitialValue = value;
        }
    }

    private float initialValue;

    [Export]
    public float Value { get; set; }

    public FloatVariable(float value = 0.0f)
    {
        InitialValue = value;
        GD.Print("FloatVariable::FloatVariable(float value)");
    }

    public FloatVariable()
    {
        InitialValue = 0.0f;
        GD.Print("FloatVariable::FloatVariable()");
    }
}