

using System;
using System.Net.Security;


namespace Phase1
{
    public class valuesVsReferences
    {
        // first we implement structs and classes
        // structs are used to hold data that will not change or immutable and used as a reference
        // examples are levels, priority order or coordinates

        public struct taskPriority // each task priority will have its corresponding name and level
        {
            private int Level { get; set; }
            private string Name { get; set; }


            // set up constructor
            public taskPriority(int level, string name)
            {
                Level = level;
                Name = name;
            }


        }

        // creating a class with different attributes
        public class User
        {
            public int Id { get; }
            public string uniqueName { get; set; }

            public string Address { get; set; }

            public taskPriority priority { get; set; }


            public User(int id, string uniquename, string address)
            {
                Id = id;
                uniqueName = uniquename;
                Address = address;
            }
        }

        public static void valueCopyExample()
        {
            // copying by value
            // first you create an instance of the object
            taskPriority taskPriority1 = new taskPriority(1, "high"); // instance of struct
            User user1 = new User(1, "Miles", "Salesi"); // instance of class

            //perform copy by value
            taskPriority taskPriority2 = taskPriority1; // this holds the same memory address now


            User user2 = user1;

            user2.uniqueName = "Hinrich";

            // Call constructor AND use initializer
            var user3 = new User(2, "Muertigue", "Home") // set using constructor as theya 
            {
                priority = new taskPriority(1, "medium")
            };


            // use constructors for fields that you want to be initialized with a value 




            // applies to classes as calsses are reference types and primitivs and structs are value types etc


        }
    }
}