using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpStatsAnalysis
{
    class NumberVector : Vector<double>
    {
        // repeat
        NumberVector(int num, int length)
        {
            vecArr = new double[length];
            for(int i = 0; i < vecArr.Length; i++)
            {
                vecArr[i] = num;
            }
        }

        // repeat with end parameter
        NumberVector(int start, int end, int length)
        {
            vecArr = new double[length];
            for(int i = 0; i <= end; i++)
            {
                vecArr[i] = start;
                start++;
            }
        }

        // repeat with array variation
        NumberVector(int start, int end, int[] c)
        {
            int diff = end - start;
            if(diff != c.Length)
            {
                throw new Exception("Cannot create vector: given array is not the correct length to match with elements");
            }
            else
            {
                // initializing array
                int cIndex = 0;
                for(int i = start; i <= end; i++)
                {
                    for(int j = 0; j < c[cIndex]; j++)
                    {
                        // continue----
                    }
                    cIndex++;
                }
            }
        }

        // sequence
        NumberVector(double start, double end, bool byParameter, double byOrLength)
        {
            if (byParameter == true)
            {
                // creating a sequence using by
                int numArrElements = (int)((end - start) / byOrLength);
                vecArr = new double[numArrElements + 1];

                int index = 0;
                for (double i = start; i <= end; i += (int)byOrLength)
                {
                    vecArr[index] = i;
                    index++;
                }
            }
            else
            {
                // creating a sequence using length
                vecArr = new double[(int)byOrLength];

                double diff = end - start;
                byOrLength--;

                double increment = diff / byOrLength;

                int index = 0;
                for (double i = start; i <= end; i += increment)
                {
                    vecArr[index] = i;
                    index++;
                }
            }
        }
    }
}
