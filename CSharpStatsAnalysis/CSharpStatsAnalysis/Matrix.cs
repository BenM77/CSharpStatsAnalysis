using System;

public class Matrix
{
    private int matrixType;
    private int dimR, dimC; // track the dimensions of the matrix
    private Object[,] theMatrix;
	public Matrix(Vector v1, Vector v2, bool bindRow)// if bindRow is true, add the vectors as rows instead of as columns
	{
        int v1Length = v1.getLength();
        int v2length = v2.getLength();
        Type v1Type = v1.getType();
        Type v2Type = v2.getType();
        int dimension;

        if (v1Type != v2Type)
            throw new Exception("Two vectors must be of the same type to form a matrix");

        if (v1Length > v2length)//if the vectors are different length, fill the rest of the shorter vector's column with zeroes
            dimension = v1Length;
        else
            dimension = v2length;

        if (bindRow == true)
        {
            theMatrix = new v1Type[dimension, 2];
        }
        else//by column
        {
            theMatrix = new v1Type[2, dimension];
        }
    }
}
