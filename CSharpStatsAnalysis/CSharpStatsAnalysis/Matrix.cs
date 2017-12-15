using System;

namespace CSharpStatsAnalysis
{
    public class Matrix
    {
        private Type matrixType;
        private int dimR, dimC; // track the dimensions of the matrix
        private Object[,] theMatrix;

        // a constructor that takes two vectors as rows or columns
        public Matrix(Vector<Type> v1, Vector<Type> v2, bool bindRow)// if bindRow is true, add the vectors as rows instead of as columns
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
                theMatrix = new v1Type[dimR, dimC];
                for(int i = 0; i < v1Length; i++)//fill in row 1
                {
                    theMatrix[1, i] = v1.getVal(i);
                }
                for(int i = 0; i < v2length; i++)//fill in row 2
                {
                    theMatrix[2, i] = v2.getVal(i);
                }
            }
            else//by column. the opposite of above
            {
                dimR = dimension;
                dimC = 2;
                theMatrix = new v1Type[dimR, dimC];
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
        public Matrix(Vector<Type> v1, int numRows, int numCols)
        {
            if ((numRows * numCols) < v1.getLength())
                throw new Exception("A matrix of those dimensions does not have enough spaces for all the elements in that vector");
            theMatrix = new Array[numRows, numCols];
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
    }
}