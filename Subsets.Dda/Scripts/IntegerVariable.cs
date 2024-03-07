using Godot;
using System;
using MonoCustomResourceRegistry;
using Subsets.Dda;
using Godot.Collections;
using Array = Godot.Collections.Array;

namespace Subsets.Dda
{
    [RegisteredType(nameof(IntegerVariable), "", nameof(Resource))]
    [Tool]
    public class IntegerVariable : Variable<int>
    {
        public IntegerVariable()
        {
            //GD.Print("IntegerVariable::IntegerVariable()");
        }
     
        protected override int DuplicateValue(int value)
        {
            return value;
        }

        protected override int InitialValue
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


        [Export] public int initialValue;
    }   
}