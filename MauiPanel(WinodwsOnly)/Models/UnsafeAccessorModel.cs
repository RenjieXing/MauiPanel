using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiPanel.Models
{
    public class Person
    {
        public Person(string Name)
        {
            this.Name = Name;
        }

        public string Name { get; set; }
        //想想,age换成Age2,会不会有问题?
        private int Age = 15;
        private int Age2 { get; set; } = 15;
        private void ChangeAge(int Age)
        {

            this.Age = Age;
        }

        private Person(string Name, int Age)
        {
            this.Name = Name;
            this.Age = Age;
        }
    }
   
}
