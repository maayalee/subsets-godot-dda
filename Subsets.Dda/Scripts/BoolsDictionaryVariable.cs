using Godot;
using System;
using MonoCustomResourceRegistry;
using Subsets.Dda;
using Godot.Collections;
using Array = Godot.Collections.Array;
using Object = Godot.Object;

namespace Subsets.Dda
{
    [RegisteredType(nameof(BoolsDictionaryVariable), "", nameof(Resource))]
    [Tool]
    public class BoolsDictionaryVariable : Variable<Dictionary<object, bool>>
    {
        public BoolsDictionaryVariable()
        {
            GD.Print("BoolsDictionaryVariable::BoolsDictionaryVariable()");
        }

        protected override Dictionary<object, bool> InitialValue
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
        
        protected override Dictionary<object, bool> DuplicateValue(Dictionary<object, bool> value)
        {
            return value;
        }


        [Export] public Dictionary<object, bool> initialValue = new Dictionary<object, bool>();
    }   
}