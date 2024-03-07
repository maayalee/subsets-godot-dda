using Godot;
using System;
using MonoCustomResourceRegistry;
using Subsets.Dda;
using Godot.Collections;
using Array = Godot.Collections.Array;
using Object = Godot.Object;

namespace Subsets.Dda
{
    [RegisteredType(nameof(StringKeyTexturesVariable), "", nameof(Resource))]
    [Tool]
    public class StringKeyTexturesVariable : Variable<Dictionary<string, Texture>>
    {
        public StringKeyTexturesVariable()
        {
            GD.Print("StringKeyTexturesVariable::StringKeyTexturesVariable()");
        }

        protected override Dictionary<string, Texture> InitialValue
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
        
        protected override Dictionary<string, Texture> DuplicateValue(Dictionary<string, Texture> value)
        {
            return value;
        }


        [Export] public Dictionary<string, Texture> initialValue = new Dictionary<string, Texture>();
    }   
}