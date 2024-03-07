using Godot;
using System;
using MonoCustomResourceRegistry;
using Subsets.Dda;
using Godot.Collections;
using Array = Godot.Collections.Array;

namespace Subsets.Dda
{
    [RegisteredType(nameof(Vector2Variable), "", nameof(Resource))]
    [Tool]
    public class Vector2Variable : Variable<Vector2>
    {
        public Vector2Variable()
        {
            //GD.Print("Vector2Variable::Vector2Variable()");
        }
     
        protected override Vector2 DuplicateValue(Vector2 value)
        {
            return value;
        }

        protected override Vector2 InitialValue
        {
            get
            {
                return initialValue;
            }
            set
            {
                initialValue = value;
            }
        }


        [Export] public Vector2 initialValue;
    }   
}