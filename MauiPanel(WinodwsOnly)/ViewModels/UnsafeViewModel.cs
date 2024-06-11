using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace MauiPanel.ViewModels
{
    public partial class UnsafeViewModel: ObservableObject
    {


        [ObservableProperty]
        string input;

        [ObservableProperty]
        string dict;


        public ObservableCollection<int> inputed { get; set; } = new ObservableCollection<int>();

        public UnsafeViewModel()
        {
            Input="";
            inputed.Add(222);
            Dict = FileSystem.Current.AppDataDirectory; //获取程序目录
        }

        [RelayCommand]
        public void inputEntery()
        {
           
                inputed.Add(int.Parse(Input));
                Input = "";
        }
        [RelayCommand]
        public  void  UnsafeSort()
        {
            unsafe
            {
                fixed (int* p = inputed.ToArray())
                {
                    QuickSort(p,p+inputed.Count-1);
                    var Count=inputed.Count;
                    inputed.Clear();
                    for (int i = 0; i < Count; i++)
                    {
                        
                        inputed.Add(*(p + i));

                    }
                    
                }
            }
        }
        [RelayCommand]
        public void CplusplusReverse()
        {
            unsafe
            {
                fixed (int* p = inputed.ToArray())
                {
                    Reverse(p, inputed.Count);
                    var Count = inputed.Count;
                    inputed.Clear();
                    for (int i = 0; i < Count; i++)
                    {
                        inputed.Add(*(p + i));
                    }

                }
            }
        }


        [RelayCommand]
        public void clear()
        {
            inputed.Clear();
        }
        #region 快排
        public static unsafe void QuickSort(int* start, int* end)
        {
            if (start < end)
            {
                int* pivot = Partition(start, end);
                QuickSort(start, pivot);
                QuickSort(pivot + 1, end);
            }
        }

        private static unsafe int* Partition(int* start, int* end)
        {
            int pivot = *(start);
            int* i = start;
            int* j = end;
            while (true)
            {
                while (*i <= pivot && i < j)
                {
                    i = i + 1;
                }

                while (*j >= pivot && j >= i)
                {
                    j = j - 1;
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
        #endregion
        //MaybeError
        //charset仅在Unicaode可进行C++调用(具体看C++内部设置)
        [DllImport(@"C:\Users\14798\Desktop\C#\MauiPanel(WinodwsOnly)\x64\Debug\CplusplusDLL.dll", EntryPoint = "reverse", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern unsafe void Reverse(int* arr, int size);


    }
}
