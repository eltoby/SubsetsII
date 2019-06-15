using System.Collections.Generic;
using System.Linq;

namespace SubsetsII
{
    public class Solution
    {
        public IList<IList<int>> SubsetsWithDup(int[] nums)
        {
            var dic = new Dictionary<int, int>();

            foreach (var num in nums)
            {
                if (dic.ContainsKey(num))
                    dic[num]++;
                else
                    dic.Add(num, 1);
            }

            var subsets = new List<IList<int>>();
            subsets.Add(new List<int>());
            for (var i = 1; i <= nums.Length; i++)
            {
                var clonedDic = new Dictionary<int, int>(dic);
                var partial = this.GetSubsets(clonedDic, i);
                subsets.AddRange(partial);
            }

            return subsets;
        }

        private IList<IList<int>> GetSubsets(Dictionary<int, int> dic, int subsetLength)
        {
            var result = new List<IList<int>>();

            if (subsetLength == 1)
            {
                foreach (var key in dic.Keys)
                    result.Add(new List<int>() { key });

                return result;
            }

            if (dic.Keys.Count == 1)
            {
                var allSameResult = new List<int>();
                var onlyKeyLeft = dic.Keys.First();
                for (var i = 0; i < subsetLength; i++)
                    allSameResult.Add(onlyKeyLeft);

                result.Add(allSameResult);
                return result;
            }

            foreach (var key in dic.Keys)
            {
                var clonedDic = new Dictionary<int, int>(dic.Where(x => x.Key >= key).ToDictionary(x => x.Key, x => x.Value));

                if (clonedDic.Sum(x => x.Value) < subsetLength)
                    continue;

                if (clonedDic[key] == 1)
                    clonedDic.Remove(key);
                else
                    clonedDic[key]--;

                var partialSubsets = this.GetSubsets(clonedDic, subsetLength - 1);

                foreach (var partial in partialSubsets)
                {
                    partial.Add(key);

                    if (partial.Count == subsetLength)
                        result.Add(partial);
                }
            }

            return result;
        }
    }
}
