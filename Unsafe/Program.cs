
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Unsafe
{
    public class Program
    {
        static void Main(string[] args)
        {
            //提前预知List的大小,有助于提高性能,
            //因为List的底层是数组,toarray的时候性能开销比未知的小
            //而且一整块内存,可以避免频繁的内存分配和释放
            var PointCount=12;
            List<int> list = new List<int>(12) { 1, 2, 3, 4, 5 ,99,33,9494,23,4,8,9};
            unsafe
            {
               
                fixed (int*p = list.ToArray())
                {
                    int* ptr = p + (list.Count - 1);
                    Console.WriteLine("The last element of the list is: " + *ptr);
                    QuickSort(p,ptr);
                    print(p, list.Count);
                    Console.WriteLine("现在使用C++进行逆序反转" );
                    Reverse (p, list.Count);
                    print(p, list.Count);
                }
       
            }
            Console.WriteLine($"总计元素数量:{list.Count}");
            Console.ReadKey();
        }
        public static unsafe void QuickSort(int *start, int *end)
        {
            if (start<end)
            {
                int *pivot = Partition(start,end);
                QuickSort(start, pivot);
                QuickSort( pivot + 1, end);
            }
        }

        private static unsafe int* Partition( int *start, int *end)
        {
            int pivot = *(start);
            int *i = start;
            int *j = end;
            while (true)
             {
                while (*i <= pivot && i < j)
                {
                     i= i+1;
                }

                while (*j >= pivot&& j >= i)
                {
                    j = j-1;
                 }


                if (i >= j)
                {
                    Swap(start, j);
                    return j;
                }

                Swap(i, j);
            }
        }

        private static unsafe void Swap(int* a, int* b)
        {
            int temp = *a;
            *a = *b;
            *b = temp;
        }

        private static unsafe void print(int* arr, int size)
        {
            for (int i = 0; i < size; i++)
            {
                Console.WriteLine($"第{i+1}位是" + *(arr + i));
            }
         
        }
        //charset仅在Unicaode可进行C++调用(具体看C++内部设置)
        [DllImport(@"..\..\..\..\MauiPanel(WinodwsOnly)\x64\Debug\CplusplusDLL.dll", EntryPoint = "reverse", CallingConvention = CallingConvention.Cdecl,CharSet =CharSet.Unicode)]
        private static extern unsafe void Reverse(int* arr, int size);

    }
}
