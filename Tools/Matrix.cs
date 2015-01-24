using System;
using System.Numerics;
using System.Text;

namespace Tools
{
    public class DynamicMatrix
    {
        private dynamic matrix;

        public DynamicMatrix(dynamic Array2D)
        {
            matrix = Array2D;
        }

        public static DynamicMatrix operator +(DynamicMatrix a, DynamicMatrix b)
        {
            Type t = GetLargestType(a, b);

            if (a.matrix.GetLength(0) == b.matrix.GetLength(0) &&
                a.matrix.GetLength(1) == b.matrix.GetLength(1))
            {
                dynamic resMatrix = Array.CreateInstance(t, a.matrix.GetLength(0), a.matrix.GetLength(1));

                for (int i = 0; i < a.matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < a.matrix.GetLength(1); j++)
                    {
                        resMatrix[i, j] = a[i, j] + b[i, j];
                    }
                }

                return new DynamicMatrix(resMatrix);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public static DynamicMatrix operator -(DynamicMatrix a, DynamicMatrix b)
        {
            Type t = GetLargestType(a, b);

            if (a.matrix.GetLength(0) == b.matrix.GetLength(0) &&
                a.matrix.GetLength(1) == b.matrix.GetLength(1))
            {
                dynamic resMatrix = Array.CreateInstance(t, a.matrix.GetLength(0), a.matrix.GetLength(1));

                for (int i = 0; i < a.matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < a.matrix.GetLength(1); j++)
                    {
                        resMatrix[i, j] = a[i, j] - b[i, j];
                    }
                }

                return new DynamicMatrix(resMatrix);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public static DynamicMatrix operator *(DynamicMatrix a, int k)
        {
            Type t = a.matrix[0, 0].GetType();
            dynamic resMatrix = Array.CreateInstance(t, a.matrix.GetLength(0), a.matrix.GetLength(1));

            for (int i = 0; i < a.matrix.GetLength(0); i++)
            {
                for (int j = 0; j < a.matrix.GetLength(1); j++)
                {
                    resMatrix[i, j] = a[i, j] * k;
                }
            }

            return new DynamicMatrix(resMatrix);
        }

        public static DynamicMatrix operator *(DynamicMatrix a, DynamicMatrix b)
        {
            Type t = GetLargestType(a, b);

            if (a.matrix.GetLength(1) == b.matrix.GetLength(0))
            {
                dynamic cMatrix = Array.CreateInstance(t, a.matrix.GetLength(0), b.matrix.GetLength(1));

                for (int i = 0; i < cMatrix.GetLength(0); i++)
                {
                    for (int j = 0; j < cMatrix.GetLength(1); j++)
                    {
                        for (int r = 0; r < a.matrix.GetLength(1); r++)
                        {
                            cMatrix[i, j] += a[i, r] * b[r, j];
                        }
                    }
                }

                return new DynamicMatrix(cMatrix);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public static DynamicMatrix IdentityMatrix(int size, Type type)
        {
            dynamic matrix = Array.CreateInstance(type, size, size);
            for (int i = 0; i < size; i++)
            {
                matrix[i, i] = 1;
            }

            return new DynamicMatrix(matrix);
        }

        public int GetLength(int dimension)
        {
            return matrix.GetLength(dimension);
        }

        public Type GetInnerType()
        {
            return matrix[0, 0].GetType();
        }

        public dynamic this[int x, int y]
        {
            get { return this.matrix[x, y]; }
        }

        public override string ToString()
        {
            StringBuilder res = new StringBuilder();

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    res.AppendFormat("{0} ", this[i, j]);
                }
                res.Append('\n');
            }

            return res.ToString().TrimEnd('\n');
        }

        private static readonly Type tInt = typeof(int),
                                    tUInt = typeof(uint),
                                    tLong = typeof(long),
                                    tULong = typeof(ulong),
                                    tFloat = typeof(float),
                                    tDouble = typeof(double),
                                    tDecimal = typeof(decimal),
                                    tBigInteger = typeof(BigInteger);

        private static Type GetLargestType(DynamicMatrix a, DynamicMatrix b)
        {
            Type t1 = a.matrix[0, 0].GetType();
            Type t2 = b.matrix[0, 0].GetType();

            if (t1 == tDecimal || t2 == tDecimal)
            {
                return tDecimal;
            }
            if (t1 == tDouble || t2 == tDouble)
            {
                return tDouble;
            }
            if (t1 == tFloat || t2 == tFloat)
            {
                return tFloat;
            }
            if (t1 == tBigInteger || t2 == tBigInteger)
            {
                return tBigInteger;
            }
            if (t1 == tULong || t2 == tULong)
            {
                return tULong;
            }
            if (t1 == tLong || t2 == tLong)
            {
                return tLong;
            }
            if (t1 == tUInt || t2 == tUInt)
            {
                return tUInt;
            }
            return tInt;
        }
    }
}
