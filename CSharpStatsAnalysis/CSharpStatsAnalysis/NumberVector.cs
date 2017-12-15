using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpStatsAnalysis
{
    class NumberVector : Vector<double>
    {
        // repeat or sequence
        public NumberVector(int num, int num2, bool repeat)
        {
            // repeat
            if (repeat == true)
            {
                if(num2 < 0)
                {
                    throw new Exception("Cannot create vector: length is negative.");
                }
                // num2 is now length
                vecArr = new double[num2];
                for (int i = 0; i < vecArr.Length; i++)
                {
                    vecArr[i] = num;
                }
            }
            // sequence
            else
            {
                if(num > num2)
                {
                    int temp = num;
                    num = num2;
                    num2 = temp;
                }

                vecArr = new double[(num2 - num)+1];
                for(int i = 0; i < vecArr.Length; i++)
                {
                    vecArr[i] = num++;
                }
            }
        }

        // repeat with end parameter
        public NumberVector(int start, int end, int length)
        {
            int counter = start;

            vecArr = new double[length];
            for (int i = 0; i < length; i++)
            {
                vecArr[i] = counter++;
                if (counter == end)
                    counter = start;
            }
        }

        // repeat with array variation
        public NumberVector(int start, int end, int[] c)
        {
            int diff = end - start;
            if(diff != c.Length-1)
            {
                throw new Exception("Cannot create vector: given array is not the correct length to match with elements");
            }
            else
            {
                // checking to see if any integer within the array is less than 0
                testArrayInput(c);

                int numVecElements = 0;
                for(int i = 0; i < c.Length; i++)
                {
                    numVecElements += c[i];
                }
                vecArr = new double[numVecElements];

                int vecCounter;
                int vecIndex = 0;
                int cIndex = 0;
                for(int i = start; i <= end; i++)
                {
                    vecCounter = 0;
                    vecCounter = c[cIndex];

                    int vecInc = vecIndex;
                    while(vecIndex < vecInc + vecCounter)
                    {
                        if(vecIndex > vecArr.Length)
                        {
                            // throw exception?
                            break;
                        }
                        vecArr[vecIndex++] = i;
                    }

                    cIndex++;
                }
            }
        }

        // sequence
        public NumberVector(double start, double end, bool byParameter, double byOrLength)
        {
            if(end < start)
            {
                // swapping start and end
                double temp = start;
                start = end;
                end = temp;
            }

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

        // testing array input for negative numbers
        private void testArrayInput(int[] cArr)
        {
            foreach(int c in cArr)
            {
                if (c < 0)
                    throw new Exception("Cannot create vector: input array contains negative number");
            }
        }

        public double getMean()
        {
            double total = 0;
            foreach(double x in vecArr)
            {
                total += x;
            }
            return total / vecArr.Length;
        }

        public double getMedian()
        {
            double[] vecArrCopy = vecArr;
            Array.Sort(vecArrCopy);
            return vecArrCopy[vecArrCopy.Length / 2];
        }

        public double[] getMode()
        {
            Dictionary<double, int> vecDict = new Dictionary<double, int>();
            foreach(double v in vecArr)
            {
                if (vecDict.Keys.Contains(v))
                    vecDict[v]++;
                else
                    vecDict.Add(v, 0);
            }

            // using array in case of multiple modes
            double[] results = new double[vecArr.Length];
            int modeNum = vecDict.Values.Max();

            int resultsIndex = 0;
            foreach(KeyValuePair<double, int> kvp in vecDict)
            {
                if(kvp.Value == modeNum)
                {
                    results[resultsIndex++] = kvp.Key;
                }
            }
            return results.Take(resultsIndex).ToArray();
        }
    }
}
