using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpStatsAnalysis
{
    public class Vector<T>
    {
        protected T[] vecArr;
        public Vector(){
        }

        public Type getType()
        {
            return typeof(T);
        }

        public int getLength()
        {
            return vecArr.Length;
        }

        public T getVal(int index)
        {
            return vecArr[index];
        }
    }
}
