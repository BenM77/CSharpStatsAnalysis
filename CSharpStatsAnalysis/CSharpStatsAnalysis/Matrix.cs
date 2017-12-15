using System;

namespace CSharpStatsAnalysis
{
    public class Matrix
    {
        private Type matrixType;
        private int dimR, dimC; // track the dimensions of the matrix
        private Object[,] theMatrix;

        public static Matrix operator +(Matrix m1, Matrix m2)
        {
            if (m1.matrixType != typeof(Double))
                throw new Exception("Cannot add matrices of non-numeric types");
            if (m1.dimC != m2.dimC)
                throw new Exception("The matrices to add have different numbers of columns");
            if (m1.dimR != m2.dimR)
                throw new Exception("The matrices to add have different numbers of rows");
            if (m1.matrixType != m2.matrixType)
                throw new Exception("The matrices to add have different types");
            Matrix newMatrix = new Matrix(m1.dimR, m1.dimC, m1.matrixType);
            for(int i = 0; i < m1.dimR; i++)
                for(int j = 0; j < m1.dimC; j++)
                {
                    newMatrix.theMatrix[j, i] = (Double)m1.theMatrix[j, i] + (Double)m2.theMatrix[j, i]; //type casts are redundant but required because the compiler does not know about the first if statement
                }
            return newMatrix;
        }

        public Matrix(int newDimR, int newDimC, Type T)//create a new matrix without initializing any values
        {
            if (newDimR < 1)
                throw new Exception("The number of rows in a new matrix must be at least one");
            if (newDimC < 1)
                throw new Exception("The number of columns in a new matrix must be at least one");
            dimR = newDimR;
            dimC = newDimC;
            matrixType = T;
            theMatrix = new Object[dimR, dimC];
        }

        // a constructor that takes two vectors as rows or columns
        public Matrix(Vector<double> v1, Vector<double> v2, bool bindRow)// if bindRow is true, add the vectors as rows instead of as columns
        {
            int v1Length = v1.getLength();
            int v2length = v2.getLength();
            Type v1Type = v1.getType();
            Type v2Type = v2.getType();
            int dimension;

            if (v1Type != v2Type)
                throw new Exception("Two vectors must be of the same type to form a matrix");
            matrixType = v1Type;
            if (v1Length > v2length)//if the vectors are different length, fill the rest of the shorter vector's column with zeroes
                dimension = v1Length;
            else
                dimension = v2length;

            if (bindRow == true)
            {
                dimR = 2;
                dimC = dimension;
                theMatrix = new Object[dimR, dimC];//the matrix will be made of objects, but when doing matrix operations, the type will checked from the MatrixType variable
                for(int i = 0; i < v1Length; i++)//fill in row 1
                {
                    theMatrix[0, i] = v1.getVal(i);
                }
                for(int i = 0; i < v2length; i++)//fill in row 2
                {
                    theMatrix[1, i] = v2.getVal(i);
                }
            }
            else//by column. the opposite of above
            {
                dimR = dimension;
                dimC = 2;
                theMatrix = new Object[dimR, dimC];
                for(int i = 0; i < v1Length; i++)
                {
                    theMatrix[i, 1] = v1.getVal(i);
                }
                for(int i = 0; i < v2length; i++)
                {
                    theMatrix[i, 2] = v2.getVal(i);
                }
            }
        }

        //create a new matrix by giving all the elements in a vector and the number of rows(length of each column) and vice versa for columns
        public Matrix(Vector<double> v1, int numRows, int numCols)
        {
            if ((numRows * numCols) < v1.getLength())
                throw new Exception("A matrix of those dimensions does not have enough spaces for all the elements in that vector");
            theMatrix = new Object[numRows, numCols];
            int vectorIndex = 0;
            for(int i = 0; i < numRows; i++)
            {
                for(int j = 0; j < numCols; j++)
                {
                    if (vectorIndex <= v1.getLength())
                    {
                        theMatrix[i, j] = v1.getVal(vectorIndex);
                    }
                    else
                        theMatrix[i, j] = 0;//leftover spaces will be filled with zeroes if the matrix is too large
                    vectorIndex++;
                }
            }
        }

        public void edit(int row, int column, double value)//change a specific value in the matrix
        {
            if (row < 0)
                throw new Exception("the row number cannot be negative");
            if (row > dimR)
                throw new Exception("the row to access cannot be larger than the number of rows in the matrix");
            if (column < 0)
                throw new Exception("the column number cannot be negative");
            if (column > dimC)
                throw new Exception("the column to access cannot be larger than the number of columns in the matrix");
            theMatrix[row, column] = value;
        }

        public void edit(Vector<Object> v1, int index, bool replaceRow)
        {
            if (v1.getType() != matrixType)
                throw new Exception("the vector must have the same type as the matrix it is being put into");
            if (replaceRow)
            {
                if (v1.getLength() > dimC)
                    throw new Exception("The vector is longer than the length of the row");//if it is shorter just keep the rest of the values the same
                if (index > dimR - 1)
                    throw new Exception("the row to replace is out of bounds of the matrix");
                if (index < 0)
                    throw new Exception("the row number to replace is negative");
                for(int i = 0; i < v1.getLength(); i++)
                {
                    theMatrix[index, i] = v1.getVal(i);
                }
            }
            else
            {
                if (v1.getLength() > dimR)
                    throw new Exception("The vector is longer than the length of the column");//if it is shorter just keep the rest of the values the same
                if (index > dimC - 1)
                    throw new Exception("the column to replace is out of bounds of the matrix");
                if (index < 0)
                    throw new Exception("the column number to replace is negative");
                for (int i = 0; i < v1.getLength(); i++)
                {
                    theMatrix[i, index] = v1.getVal(i);
                }
            }
        }

        public void display()//will work with matrices with less than 1000 rows
        {
            listColumns();
            for(int i = 0; i < dimR; i++)
            {
                if (i < 10)
                    Console.Write("{0}  \t");
                else if (i < 100)
                    Console.Write("{0} \t");
                else
                    Console.Write("{0}\t");
                listRowElements(i);
                Console.WriteLine();
            }
        }

        private void listColumns()//the top row for the display
        {
            Console.Write("   \t");
            for(int i = 0; i < dimC; i++)
            {
                Console.Write("{0}\t", i + 1);
            }
            Console.WriteLine();
        }

        private void listRowElements(int row)
        {
            for(int i = 0; i < dimC; i++)
            {
                Console.Write("{0}\t",theMatrix[row, i]);
            }
        }

        //the following are statistical functions
        public double max()
        {
            if (matrixType != typeof(Double))
                throw new Exception("The max method can only be used on numeric type matrices");
            double theMax = (double)theMatrix[1, 1];//the default starting value. typecast required because the compiler does not know the matrix is of type double
            double temp;
            for(int i = 0; i < dimC; i++)
                for(int j = 0; j < dimR; j++)
                {
                    temp = (double)theMatrix[j, i];
                    if (temp > theMax)
                        theMax = temp;
                }
            return theMax;
        }

        public double min()
        {
            if (matrixType != typeof(Double))
                throw new Exception("The min method can only be used on numeric type matrices");
            double theMin = (double)theMatrix[1, 1];
            double temp;
            for (int i = 0; i < dimC; i++)
                for (int j = 0; j < dimR; j++)
                {
                    temp = (double)theMatrix[j, i];
                    if (temp < theMin)
                        theMin = temp;
                }
            return theMin;
        }

        public double mean()
        {
            if (matrixType != typeof(Double))
                throw new Exception("the mean method can obly be used on numeric type matrices");
            double total = 0;
            for(int i = 0; i < dimC; i++)
                for(int j = 0; j < dimR; j++)
                {
                    total += (double)theMatrix[j, i];
                }
            return total / (dimC * dimR);//total / number of elements
        }

        public NumberVector ConvertToVector()//the opposite of the second constructor-- takes a matrix and returns a vector
        {
            double[] theArray = new double[dimC * dimR];
            int arrayIndex = 0;
            for(int i = 0; i < dimC; i++)
                for(int j = 0; j < dimR; j++)
                {
                    theArray[arrayIndex] = (double)theMatrix[j, i];
                    arrayIndex++;
                }
            return new NumberVector(theArray);       
        }

        public double[] mode()//works with any type of matrix
        {
            NumberVector tempVector = ConvertToVector();
            return tempVector.getMode();
        }
    }
}