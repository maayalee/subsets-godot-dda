using Godot;
using System;
using MonoCustomResourceRegistry;
using Subsets.Dda;
using Godot.Collections;
using Array = Godot.Collections.Array;

namespace Subsets.Dda
{
    [RegisteredType(nameof(StringVariable), "", nameof(Resource))]
    [Tool]
    public class StringVariable : Variable<string>
    {
        public StringVariable()
        {
            //GD.Print("StringVariable::StringVariable()");
        }
     
        protected override string DuplicateValue(string value)
        {
            return value;
        }

        protected override string InitialValue
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


        [Export] public string initialValue;
    }   
}