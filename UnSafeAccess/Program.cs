// See https://aka.ms/new-console-template for more information
//复制这段代码到 https://sharplab.io/查看汇编
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Reflection;
public static class prom
{
    static void Main()
    {
        Console.WriteLine("创建行为人:");
        unsafeAcess ua = new unsafeAcess();
        ua.p.Print();
        Console.WriteLine("打印信息");

        Console.WriteLine("修改年龄:");
        ua.ChangeAgeByField(20);
        Console.WriteLine("再次打印信息");
        ua.p.Print();
        if (ua.GetAge() == 20)
        {
            Console.WriteLine("年龄修改成功");
        }
        while (true) ;

    }
    public class Person
    {
        public Person(string Name)
        {
            this.Name = Name;
        }
        public string Name { get; set; }

        private int Age = 15;
        private void ChangeAge(int Age)
        {
            this.Age = Age;
        }
        public void Print()
        {
            Console.WriteLine(Name + " " + Age.ToString());
        }

    }
    public class unsafeAcess
    {
        public Person p;
        public unsafeAcess()
        {
            p = new Person("John");
        }

        public int GetAge()
        {
            return  SafeAccessField(p);
            [UnsafeAccessor(UnsafeAccessorKind.Field, Name = "Age")]
            static extern ref int SafeAccessField(Person person);
        }

        public void ChangeAgeByField(int NewAge)
        {
            ref var s = ref SafeAccessField(p);
            s = NewAge;
            [UnsafeAccessor(UnsafeAccessorKind.Field, Name = "Age")]
            static extern ref int SafeAccessField(Person person);
        }
        public void ChangeAgeByReflex(int NewAge)
        {

            Type type = p.GetType();
            FieldInfo fieldInfo = type.GetField("Age", BindingFlags.NonPublic | BindingFlags.Instance);
            fieldInfo.SetValue(p, NewAge);
        }
        public void ChangeAgeByMethod(int NewAge)
        {
            SafeAccessMethod(p, NewAge);
            [UnsafeAccessor(UnsafeAccessorKind.Method, Name = "ChangeAge")]
            static extern void SafeAccessMethod( Person person, int newAge);
        }
        public void ChangeAgeByReflexMethod(int NewAge)
        {
            Type type = p.GetType();
            MethodInfo fieldInfo = type.GetMethod("ChangeAge", BindingFlags.NonPublic | BindingFlags.Instance);
            fieldInfo.Invoke(p, [NewAge]);
        }



    }
}




