using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MauiPanel.Models;

namespace MauiPanel.ViewModels
{
    public partial class UnsafeAccessViewModel : ObservableObject
    {
        public UnsafeAccessViewModel()
        {
            p = new Person("John");
            UnsafeAccessorSpnedTime = "";
            ReflexSpnedTime = "";
            ref var s = ref SafeAccessField(ref p);
            s = 16;
            [UnsafeAccessor(UnsafeAccessorKind.Field, Name = "Age")]
            static extern ref int SafeAccessField(ref Person person);

        }

        [ObservableProperty] int? age;
        [ObservableProperty] string? name;
        [ObservableProperty] string? unsafeAccessorSpnedTime;
        [ObservableProperty] string? reflexSpnedTime;
        [ObservableProperty] string? unsafeAccessorMethodSpnedTime;
        [ObservableProperty] string? reflexMethodSpnedTime;
        public Person p;
        public void Refresh()
        {
            Name = p.Name;
            Age = SafeAccess(p);
            [UnsafeAccessor(UnsafeAccessorKind.Field, Name = "Age")]
            static extern ref int SafeAccess(Person person);
        }

        public void ChangeAgeByField(string NewAge)
        {
            var Age2 = int.Parse(NewAge);
            var time = DateTime.Now;
            ref var s = ref SafeAccessField(p);
            s = Age2;
            [UnsafeAccessor(UnsafeAccessorKind.Field, Name = "Age")]
            static extern ref int SafeAccessField(Person person);
            UnsafeAccessorSpnedTime = (DateTime.Now - time).TotalMilliseconds.ToString() + "ms";

            //刷新界面
            Refresh();
        }
        public void ChangeAgeByReflex(string NewAge)
        {
            var Age = int.Parse(NewAge);

            var time = DateTime.Now;
            Type type = p.GetType();
            FieldInfo? fieldInfo = type.GetField("Age", BindingFlags.NonPublic | BindingFlags.Instance);
            fieldInfo.SetValue(p, Age);

            ReflexSpnedTime = (DateTime.Now - time).TotalMilliseconds.ToString() + "ms";
            Refresh();

        }
        public void ChangeAgeByMethod(string NewAge)
        {
            var Age = int.Parse(NewAge);

            var time = DateTime.Now;
            SafeAccessMethod(p, Age);
            [UnsafeAccessor(UnsafeAccessorKind.Method, Name = "ChangeAge")]
            static extern void SafeAccessMethod(Person person, int newAge);
            UnsafeAccessorMethodSpnedTime = (DateTime.Now - time).TotalMilliseconds.ToString() + "ms";

            Refresh();
        }
        public void ChangeAgeByReflexMethod(string NewAge)
        {
            var Age = int.Parse(NewAge);

            var time = DateTime.Now;
            Type type = p.GetType();
            MethodInfo? fieldInfo = type.GetMethod("ChangeAge", BindingFlags.NonPublic | BindingFlags.Instance);
            fieldInfo.Invoke(p, [Age]);
            ReflexMethodSpnedTime = (DateTime.Now - time).TotalMilliseconds.ToString() + "ms";

            Refresh();
        }


    }
}
