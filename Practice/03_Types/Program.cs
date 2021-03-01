using System;

namespace _03_Types
{
    class Program
    {
        static void Main(string[] args)
        {
            // tuples

            (int, int) t1 = (1, 2);
            t1.Item1 = 10; 
            
            var t21 = (1, 2);
            t21.Item2 = 20;
            var t22 = (1, "abc");
            t22.Item2 = "xyz";

            (int no1, int no2) t3 = (1, 2);
            (int no1, int no2) t4 = (no1: 1, no2: 2);
            t3.no1 = 10;

            t1 = t4;

            // arrays
            // they are objects of types derived from System.Array abstract class
            // size is fixed!

            int[] a1 = new int[3]; // 1 dimensional array declared
            int[] a11 = new int[] { 1, 2, 3 }; // no need for 3 since it's initialized
            int[] a111 = { 1, 2, 3 }; // actually new int[] can also be ommitted 
            
            int[,] a21; // 2 dimensinal array declared
            a21 = new int[1, 2]; // 1 x 2 array is defined but not initialized

            // Array declarations examples without initialization
            int[] tab1; // single-dimensional array
                        // of int type elements
            int[,] tab2; // two-dimensional array
                         // of int type elements
            int[][] tab3; // single-dimensional array, whose
                          // elements are single-dimensional arrays
                          // of int type elements
            int[,][][,,] tab4; // two-dimensional array, whose
                               // elements are single-dimensional arrays,
                               // whose elements are 3-dimensional arrays
                               // of int type elements

            // Array declarations examples
            // with initialization by new operator
            int[] tab11 = new int[5];
            int[,] tab21 = new int[4, 2];
            
            int[][] tab31 = new int[5][];
            // then each element is initialized seperately:
            tab31[1] = new int[5];
            tab31[2] = new int[2];


            int[,][][,,] tab41 = new int[3, 4][][,,];
            // then each element is initialized seperately:
            tab41[2, 1] = new int[5][,,];
            // then each element is again initialized seperately:
            tab41[2, 1][3] = new int[7, 8, 9];

            // simpler to use var
            var tab411 = new int[3, 4][][,,];

            // these give error because they elements cannot be initialized at bulk like this:            
            // int[][] tab31a = new int[5][4]; // error
            // int[,][][,,] tab41a = new int[3, 4][5][6, 7, 8]; // error
            // int[][] tab31b = new int[][4]; // error
            // int[,][][,,] tab41b = new int[3,][][,,]; // error

            // Array declarations examples with array initializator {}
            int[] tab12 = { 1, 2, 3, 4, 5 };
            // creates 5-element array
            
            int[,] tab22 = { { 1, 2 }, { 3, 4 }, { 5, 6 } };
            // create 3-row, 2-column array
            
            int[][] tab32 = { new int[10], new int[20] };
            // create 2-element array of arrays
            // tab32[0] is 10-element array
            // tab32[1] is 20-element array
            
            int[,][][,,] tab42 = { { new int[2][,,], new int[4][,,] } , { new int[3][,,], new int[5][,,] } };
            // create 2-element array of arrays
            

            // int[][] tab52 = { { 1, 2 }, { 3, 4 } };
            // error - nested array must be created
            // using new operator
            // (because tab5 is array of arrays)

            // array of reference type can contain elements of different types (e.g. those derived from the reference type)
            object[] tab = { 1, 2.5, "text", null, 1u, 2L };

            // Array declarations examples with implicit elements types
            // single-dimensional array
            var tab1x = new int[5]; // int[] tab1x = new int[5];
                                   // two-dimensional array
            var tab2x = new int[4, 2]; // int[,] tab2x = new int[4,2];
                                      // arrays of arrays
            var tab3x = new int[5][]; // int[][] tab3x = new int[5][];
                                     // declarations with initialization
                                     // - elements types can be omitted too!
            var tab4x = new[] { 1, 2, 3 }; //int[] tab4x = new int [] { 1, 2, 3 };
                                    // elements must be of the same type

            var tab5x = new[] { new[] { 1, 2 }, new[] { 3, 4 } };
            //int[][] tab5x = new int[2][] { new int[2] { 1, 2 }, new int[2] { 3, 4 } };

            var tab1xx = new[] { 1, 2, 3, 4, 5 };
            var tab2xx = new[] { 1, 2, 3, 4, 5 };
            var t1xx = new int[5];
            var t2xx = new int[5];

            t1xx = tab1xx; // references tab1xx and t1xx refer to
                           // the same array
            t2xx = tab2xx[..]; // references tab2xx and t2xx refer to two
                               // separate arrays with the same elements,
                               // t2xx is a shallow copy of tab2xx



            int[,] a; // it will be one object
            a = new int[5, 5];
            for (int i = 0; i < a.GetLength(0); ++i)  // GetLength() method returns the length of the given dimension
                for (int j = 0; j < a.GetLength(1); ++j)
                    a[i, j] = i + j;
            
            int[][] b; // it will be many objects
            b = new int[5][];
            for (int i = 0; i < b.Length; ++i)   // Length property gives the length of the array
            {
                b[i] = new int[5];
                for (int j = 0; j < b[i].Length; ++j)
                    b[i][j] = i + j;
            }

        }
    }
}
