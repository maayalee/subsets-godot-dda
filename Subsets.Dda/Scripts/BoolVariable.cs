using Godot;
using System;
using MonoCustomResourceRegistry;
using Subsets.Dda;
using Godot.Collections;
using Array = Godot.Collections.Array;

namespace Subsets.Dda
{
    [RegisteredType(nameof(BoolVariable), "", nameof(Resource))]
    [Tool]
    public class BoolVariable : Variable<bool>
    {
        public BoolVariable()
        {
            //GD.Print("BoolVariable::BoolVariable()");
        }
     
        protected override bool DuplicateValue(bool value)
        {
            return value;
        }

        protected override bool InitialValue
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


        [Export] public bool initialValue;
    }   
}