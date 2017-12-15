using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpStatsAnalysis
{
    class Vector<T>
    {
        protected T[] vecArr;
        public Vector(){
        }

        public void display()
        {
            Console.Write("{");
            for(int i = 0; i < vecArr.Length; i++)
            {
                Console.Write(vecArr[i].ToString());
                if (i == vecArr.Length - 1)
                    Console.Write("}\n");
                else
                    Console.Write(", ");
            }
        }

        public Type getType()
        {
            return typeof(T);
        }

        public T[] getMode()
        {
            Dictionary<T, int> vecDict = new Dictionary<T, int>();
            foreach (T v in vecArr)
            {
                if (vecDict.Keys.Contains(v))
                    vecDict[v]++;
                else
                    vecDict.Add(v, 0);
            }

            // using array in case of multiple modes
            T[] results = new T[vecArr.Length];
            int modeNum = vecDict.Values.Max();

            int resultsIndex = 0;
            foreach (KeyValuePair<T, int> kvp in vecDict)
            {
                if (kvp.Value == modeNum)
                {
                    results[resultsIndex++] = kvp.Key;
                }
            }
            return results.Take(resultsIndex).ToArray();
        }
    }
}
