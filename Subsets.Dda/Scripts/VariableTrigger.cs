using System;
using Godot;
using Godot.Collections;
using MonoCustomResourceRegistry;

namespace Subsets.Dda
{

    [RegisteredType(nameof(VariableTrigger), "", nameof(Node))]
    [Tool]
    public partial class VariableTrigger : BaseTrigger
    {
        [Export]
        public ResponseConditionOperator ConditionOperator;
    
        protected override bool IsMatchedConditions()
        {
            ConditionCompareResult result = new ConditionCompareResult();
            foreach (NodePath nodePath in Conditions)
            {
                IPropertyComparer comparer = GetNode<IPropertyComparer>(nodePath);
                result.Add(comparer.IsMatch());
            }
    
            if (result.CheckConditionOperator(ConditionOperator))
            {
                return true;
            }
            return false;
        }
    }
}