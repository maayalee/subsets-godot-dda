using Godot;
using System;

public class TestA : Resource
{
    [Export]
    public int Health { get; set;  }

    public TestA(int health = 0)
    {
        Health = health;
    }

    public TestA()
    {
        
    }
}
