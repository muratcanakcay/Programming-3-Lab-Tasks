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

            int[] a1 = new int[3]; // 1 dimensional array declared
            int[] a11 = new int[] { 1, 2, 3 }; // no need for 3 since it's initialized
            Console.WriteLine(a1[0]);
            int[,] a21; // 2 dimensinal array declared
            a21 = new int[1, 2]; // 1 x 2 array is defined

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


        }
    }
}
