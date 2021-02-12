//using System;
//using System.Collections.Generic;
//using System.Diagnostics;

//namespace DebuggerDisplayAttributes
//{
//    class Program1
//    {
//        static void Main(string[] args)
//        {
//            Student student = new Student()
//            {
//                Name = "Mr.A",
//                Age = 18
//            };

//            //
//            // Student student = new Student();
//            MyClass myClass = new MyClass();
//            Console.WriteLine("Hello World!");
//        }

//        /**
//         *DebuggerTypeProxy用于代理显示某个类，不会显示私有成员，只显示公共成员
//         */

//        [DebuggerTypeProxy(typeof(Student))]
//        class MyClass
//        {
            
//        }

//        //[DebuggerDisplay("Name={Name},Age={Age}")]
//        class Student
//        {
//            public Student()
//            {
//                Students = new List<MyClass>
//                {
//                    new MyClass(),
//                    new MyClass(),
//                    new MyClass()
//                };
//            }

//            /**
//             *DebuggerBrowsableState
//             *Never可隐藏字段属性
//             *Collapsed默认选项，显示成员信息
//             *RootHidden 不显示字段，如果是数组或者集合将以成对的对象形式显示
//             */

//            //[DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
//            public static string y = "Test String";

//            //[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
//            public int Age { get; set; }

//           // [DebuggerBrowsable(DebuggerBrowsableState.Never)]
//            public string Name { get; set; }

//            //[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
//            public List<MyClass> Students { get; set; }
//        }

//    }
//}
