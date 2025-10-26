using System;

namespace Phase1_CoreConcepts
{
    /// <summary>
    /// Demonstrates value vs reference types in enterprise context
    /// Key Learning: Understanding memory behavior prevents bugs and performance issues
    /// </summary>
    public class ValueVsReferenceTypes
    {
        // ============================================
        // VALUE TYPE EXAMPLE: Struct for coordinates
        // ============================================
        // Use structs for small, immutable data that's frequently copied
        // Best for: DTOs, coordinates, mathematical types
        public struct TaskPriority
        {
            public int Level { get; }
            public string Name { get; }

            public TaskPriority(int level, string name)
            {
                Level = level;
                Name = name;
            }

            // Structs should be immutable in enterprise code
            // Why? Prevents confusion when passing by value
        }

        // ============================================
        // REFERENCE TYPE EXAMPLE: Class for entities
        // ============================================
        // Use classes for complex business entities with identity
        // Best for: Domain models, entities, services
        public class Task
        {
            public Guid Id { get; set; }  // Guid: Value type, but unique across systems
            public string? Title { get; set; }
            public TaskPriority Priority { get; set; }  // Embedded value type
            public DateTime CreatedAt { get; set; }  // DateTime is a struct
        }

        public static void DemonstrateValueTypes()
        {
            Console.WriteLine("=== VALUE TYPE BEHAVIOR ===\n");

            // Value types: Copy by value
            int originalPriority = 1;
            int copiedPriority = originalPriority;
            copiedPriority = 5;

            Console.WriteLine($"Original: {originalPriority}");  // 1
            Console.WriteLine($"Copy: {copiedPriority}");        // 5
            Console.WriteLine("Each variable has its own copy on the stack\n");

            // Struct behavior
            var priority1 = new TaskPriority(1, "High");
            var priority2 = priority1;  // Entire struct is copied

            // priority2 = new TaskPriority(2, "Low"); // Would need reassignment
            Console.WriteLine($"Priority1: {priority1.Level}");  // 1
            Console.WriteLine($"Priority2: {priority2.Level}");  // 1
            Console.WriteLine("Structs are copied entirely\n");
        }

        public static void DemonstrateReferenceTypes()
        {
            Console.WriteLine("=== REFERENCE TYPE BEHAVIOR ===\n");

            // Reference types: Copy by reference
            var task1 = new Task
            {
                Id = Guid.NewGuid(),
                Title = "Implement authentication",
                Priority = new TaskPriority(1, "High"),
                CreatedAt = DateTime.UtcNow
            };

            var task2 = task1;  // Both variables point to SAME object on heap
            task2.Title = "Implement authorization";

            Console.WriteLine($"Task1 Title: {task1.Title}");  // "Implement authorization"
            Console.WriteLine($"Task2 Title: {task2.Title}");  // "Implement authorization"
            Console.WriteLine("Both references point to the same heap object\n");

            // ENTERPRISE PITFALL: Unintended shared state
            Console.WriteLine("⚠️  DANGER: This can cause bugs in enterprise systems!");
            Console.WriteLine("If you pass task1 to a method, modifications affect original\n");
        }

        public static void DemonstrateBoxingUnboxing()
        {
            Console.WriteLine("=== BOXING & UNBOXING (Performance Impact) ===\n");

            int valueType = 42;

            // Boxing: Value type → Reference type (allocates heap memory)
            object boxed = valueType;  // Implicit boxing
            Console.WriteLine($"Boxed value: {boxed}");
            Console.WriteLine("Boxing allocates on heap + GC overhead\n");

            // Unboxing: Reference type → Value type (type-safe cast required)
            int unboxed = (int)boxed;
            Console.WriteLine($"Unboxed value: {unboxed}");

            // ENTERPRISE CONCERN: Avoid boxing in hot paths (loops, frequent calls)
            Console.WriteLine("\n⚠️  In enterprise systems:");
            Console.WriteLine("- Boxing/unboxing in loops causes GC pressure");
            Console.WriteLine("- Use generics (List<int>) instead of non-generic (ArrayList)");
            Console.WriteLine("- Generics avoid boxing: List<int> vs ArrayList\n");
        }

        public static void EnterpriseDesignGuidelines()
        {
            Console.WriteLine("=== ENTERPRISE DESIGN GUIDELINES ===\n");

            Console.WriteLine("USE VALUE TYPES (struct) when:");
            Console.WriteLine("✓ Small size (<16 bytes recommended)");
            Console.WriteLine("✓ Immutable data");
            Console.WriteLine("✓ Frequently copied (DTOs, coordinates)");
            Console.WriteLine("✓ No inheritance needed");
            Console.WriteLine("Examples: TaskPriority, Money, Coordinates\n");

            Console.WriteLine("USE REFERENCE TYPES (class) when:");
            Console.WriteLine("✓ Large objects");
            Console.WriteLine("✓ Identity matters (entities with Id)");
            Console.WriteLine("✓ Inheritance/polymorphism needed");
            Console.WriteLine("✓ Mutable state");
            Console.WriteLine("Examples: Task, User, Project, Order\n");

            Console.WriteLine("PERFORMANCE TIP:");
            Console.WriteLine("- Passing large structs by value = expensive copying");
            Console.WriteLine("- Solution: Pass by 'ref' or 'in' keywords");
            Console.WriteLine("- Or use class if frequently passed around\n");
        }

        // ============================================
        // DEMONSTRATION OF PASS BY VALUE vs REFERENCE
        // ============================================
        public static void ModifyValue(int number)
        {
            number = 100;  // Only modifies local copy
        }

        public static void ModifyReference(Task task)
        {
            task.Title = "Modified!";  // Modifies original object
        }

        public static void ModifyValueByRef(ref int number)
        {
            number = 100;  // Modifies original variable
        }

        public static void ReadOnlyValueReference(in TaskPriority priority)
        {
            // 'in' keyword: Pass by reference but readonly (performance + safety)
            // priority.Level = 10;  // Compile error - readonly
            Console.WriteLine($"Priority level: {priority.Level}");
        }

        public static void DemonstratePassingBehavior()
        {
            Console.WriteLine("=== PASSING BEHAVIOR ===\n");

            // Value type passing
            int myNumber = 50;
            ModifyValue(myNumber);
            Console.WriteLine($"After ModifyValue: {myNumber}");  // Still 50

            // Reference type passing
            var myTask = new Task { Title = "Original" };
            ModifyReference(myTask);
            Console.WriteLine($"After ModifyReference: {myTask.Title}");  // "Modified!"

            // Value type by reference
            ModifyValueByRef(ref myNumber);
            Console.WriteLine($"After ModifyValueByRef: {myNumber}");  // 100

            Console.WriteLine();
        }

        public static void RunAllDemonstrations()
        {
            DemonstrateValueTypes();
            DemonstrateReferenceTypes();
            DemonstrateBoxingUnboxing();
            DemonstratePassingBehavior();
            EnterpriseDesignGuidelines();

            Console.WriteLine("\n=== KEY TAKEAWAYS ===");
            Console.WriteLine("1. Value types = copy by value, stack allocated");
            Console.WriteLine("2. Reference types = copy by reference, heap allocated");
            Console.WriteLine("3. Choose based on size, mutability, and identity needs");
            Console.WriteLine("4. Avoid boxing in performance-critical code");
            Console.WriteLine("5. Understand passing behavior to prevent bugs");
        }
    }
}