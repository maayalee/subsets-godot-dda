using System;
using System.Collections.Generic;

namespace Subsets.Dda
{
    public enum ResponseConditionOperator
    {
        Or,
        And
    }
    public class ConditionCompareResult
    {
        private List<bool> matchResults = new List<bool>();

        public void Add(bool compare)
        {
            matchResults.Add(compare);
        }

        public bool CheckConditionOperator(ResponseConditionOperator conditionOperator)
        {
            if (matchResults.Count == 0)
                return true;
            
            int count = 0;
            foreach (bool matched in matchResults)
            {
                if (matched)
                    count++;
            }
            if (conditionOperator == ResponseConditionOperator.And)
            {
                return count == matchResults.Count || matchResults.Count == 0;
            }
            else if (conditionOperator == ResponseConditionOperator.Or)
            {
                return count > 0 || matchResults.Count == 0;
            }
            return false;
        }
    }
}