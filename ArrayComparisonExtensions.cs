using System.Collections;

namespace ExtensionMethods
{
    /// <summary>
    /// Provides extension methods to perform item by item array comparisons.
    /// </summary>
    public static class ArrayComparisonExtensions
    {
        /// <summary>
        /// Compares this array with another int array.
        /// </summary>
        /// <param name="val1">This array.</param>
        /// <param name="val2">int array to compare to.</param>
        /// <returns>True if they are equal, false otherwise.</returns>
        public static bool IsEqualTo(this int[] val1, int[] val2)
        {
            if (val1.Length != val2.Length)
            {
                return false;
            }
            
            for (int i = 0; i < val1.Length; ++i)
            {
                if (val1[i] != val2[i])
                {
                    return false;
                }
            }

            // if we got here, then they are equal.
            return true;
        }

        public static bool IsEqualTo(this byte[] val1, byte[] val2)
        {
            if (val1.Length != val2.Length)
            {
                return false;
            }

            for (int i = 0; i < val1.Length; ++i)
            {
                if (val1[i] != val2[i])
                {
                    return false;
                }
            }

            // if we got here, then they are equal.
            return true;
        }

        public static bool IsEqualTo(this long[] val1, long[] val2)
        {
            if (val1.Length != val2.Length)
            {
                return false;
            }

            for (int i = 0; i < val1.Length; ++i)
            {
                if (val1[i] != val2[i])
                {
                    return false;
                }
            }

            // if we got here, then they are equal.
            return true;
        }

        public static bool IsEqualTo(this short[] val1, short[] val2)
        {
            if (val1.Length != val2.Length)
            {
                return false;
            }

            for (int i = 0; i < val1.Length; ++i)
            {
                if (val1[i] != val2[i])
                {
                    return false;
                }
            }

            // if we got here, then they are equal.
            return true;
        }

        public static bool IsEqualTo(this char[] val1, char[] val2)
        {
            if (val1.Length != val2.Length)
            {
                return false;
            }

            for (int i = 0; i < val1.Length; ++i)
            {
                if (val1[i] != val2[i])
                {
                    return false;
                }
            }

            // if we got here, then they are equal.
            return true;
        }

        public static bool IsEqualTo(this float[] val1, float[] val2)
        {
            if (val1.Length != val2.Length)
            {
                return false;
            }

            for (int i = 0; i < val1.Length; ++i)
            {
                if (val1[i] != val2[i])
                {
                    return false;
                }
            }

            // if we got here, then they are equal.
            return true;
        }

        public static bool IsEqualTo(this double[] val1, double[] val2)
        {
            if (val1.Length != val2.Length)
            {
                return false;
            }

            for (int i = 0; i < val1.Length; ++i)
            {
                if (val1[i] != val2[i])
                {
                    return false;
                }
            }

            // if we got here, then they are equal.
            return true;
        }

        public static bool IsEqualTo(this decimal[] val1, decimal[] val2)
        {
            if (val1.Length != val2.Length)
            {
                return false;
            }

            for (int i = 0; i < val1.Length; ++i)
            {
                if (val1[i] != val2[i])
                {
                    return false;
                }
            }

            // if we got here, then they are equal.
            return true;
        }

        public static bool IsEqualTo(this IList col1, IList col2)
        {
            if (col1.Count != col2.Count)
            {
                return false;
            }

            for (int i = 0; i < col1.Count; ++i)
            {
                if (col1[i] != col2[i])
                {
                    return false;
                }
            }

            // if we got here, then they are equal.
            return true;
        }
    }
}