using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phases.Phase1
{
    public class ControlFlowAndLoops
    {

        private static List<WorkTask> GetSampleTasks()
        {
            return new List<WorkTask> 
                {
                new WorkTask { Id = 1, Title = "Code Review", Status = TaskStatus.InProgress, Priority = 2 },
                new WorkTask { Id = 2, Title = "Write Tests", Status = TaskStatus.Pending, Priority = 1 },
                new WorkTask { Id = 3, Title = "Deploy", Status = TaskStatus.Completed, Priority = 3 },
                new WorkTask { Id = 4, Title = "Bug Fix", Status = TaskStatus.InProgress, Priority = 1 },
                new WorkTask { Id = 5, Title = "Documentation", Status = TaskStatus.Pending, Priority = 2 }
                };
      
        
        }

        // for loop using index based iteration

        public static void ForLoopExamples()
        {
            Console.WriteLine("=== FOR LOOP ===\n");
            Console.WriteLine("USE WHEN: You need index access, backwards iteration, or custom steps\n");


            var tasks = GetSampleTasks(); // calls lists with values


            for (int i = 0; i < tasks.Count; i++)
            {
                Console.WriteLine($"Index {i}: {tasks[i].Title}"); // display all the tasks title from the first element 
            }

            Console.WriteLine();
        }
        
        public static void ForEachLoopExamples()
        {
            var tasks = GetSampleTasks(); // let get sample tasks be accessible within the method by asssigning it to variable tasks

            Console.WriteLine(" this is my attempt at for each loops");

            foreach (var task in tasks)
            {
                Console.WriteLine($"{task.Title} - {task.Status}");
            }
            Console.WriteLine();
        }




        





        // Supporting types
        public class WorkTask
        {
            public int Id { get; set; }
            public string Title { get; set; } // no need to make it nullable 
            public TaskStatus Status { get; set; }
            public int Priority { get; set; }  // 1 = highest
        }   
        // unable to code today so Im just gonna make a commit

        public enum TaskStatus
        {
            Pending,
            InProgress,
            Completed
        }
    }
}   


