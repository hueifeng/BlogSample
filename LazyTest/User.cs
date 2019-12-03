using System;
using System.Collections.Generic;
using System.Text;

namespace LazyTest
{
    public class User
    {
        public string Name { get; set; }

        public int Age { get; set; }


        public User() {
            this.Name = "Name";
            this.Age = 0;
        }
    }
}
