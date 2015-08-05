using System;
using System.Linq;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            var n = 8;
            var b = new int[8] { 14, 9, 3, 1, 5, 0, 8, 9 };
            //nổi bọt
            //for (var i = 0; i < n; i++)
            //{
            //    for (var j = n - 1; j > i; j--)
            //    {
            //        if (b[j] > b[j - 1])
            //        {
            //            Swap(ref b[j], ref b[j - 1]);
            //        }
            //    }
            //}
            //C2
            //for (var i = 0; i < n; i++)
            //{
            //    for (var j = 0; j < n-i-1; j++)
            //    {
            //        if (b[j] > b[j + 1])
            //        {
            //            Swap(ref b[j], ref b[j + 1]);
            //        }
            //    }
            //}

            // Chọn
            //int min;
            //for (var i = 0; i < n; i++)
            //{
            //    min = i;
            //    for (var j = i + 1; j < n; j++)
            //    {
            //        if (b[j]>b[min])
            //        {
            //            Swap(ref b[j],ref b[min]);
            //        }
            //    }
            //}

            //chèn 3
            // 1 4 3 2
            //for (var i = 1; i < n; i++)
            //{
            //    var temp = b[i];
            //    int p = i-1;
            //    while (p >= 0 && b[p]>temp)
            //    {
            //        b[p + 1] = b[p];
            //        p--;
            //    }
            //    b[p + 1] = temp;
            //} 

            QuickSort(b, 0, 7);
            Console.ReadLine();
        }

        static void QuickSort(int[] a, int l, int r)
        {
            int v = a[(l + r) / 2]; //chọn phần tử ở giữa đoạn làm chốt
            int i = l;
            int j = r;
            int temp;
            do
            {
                while (a[i] < v) i++; //tìm phần tử phía đầu đoạn mà ≥ v
                while (a[j] > v) j--; //tìm phần tử phía cuối đoạn mà ≤ v
                //lúc này: a[i] ≥ v ≥ a[j]
                if (i <= j)
                {
                    if (i < j)
                    {
                        temp = a[i];
                        a[i] = a[j];
                        a[j] = temp;
                    }
                    //sau khi hoán đổi, ta có: a[i] ≤ v ≤ a[j]
                    i++;
                    j--;
                }
            } while (i <= j);

            //lúc này, a[l]....a[j]..a[i]...a[r], nghĩa là: l ≤ j ≤ i ≤ r
            if (l < j) QuickSort(a, l, j); //nếu a[l]...a[j] là 1 đoạn (nhiều hơn 1 phần tử) thì...
            if (i < r) QuickSort(a, i, r); //nếu a[i]...a[r] là 1 đoạn (nhiều hơn 1 phần tử) thì...
        }

        static void Swap(ref int a, ref int b)
        {
            var temp = a;
            a = b;
            b = temp;
        }








        static void Demo(int n)
        {
            for (var iC = 0; iC < n; iC++)
            {
                for (var iC3 = n - 1; iC3 < iC; iC3--)
                    Console.Write(' ');
                for (var iC2 = 0; iC2 <= iC * 2 + 1; iC2++)
                {
                    Console.Write('*');
                }
                Console.WriteLine();
            }
        }
    }
}
