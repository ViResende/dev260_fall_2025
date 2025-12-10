using System;
using System.Collections.Generic;

class StudyTask
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Course { get; set; }
    public DateTime DueDate { get; set; }
    public int Priority { get; set; }   // 1 = high, 3 = low
}

class Program
{
    // data structures
    static Dictionary<string, StudyTask> tasksById = new Dictionary<string, StudyTask>();
    static List<StudyTask> taskList = new List<StudyTask>();
    static HashSet<string> completedTaskIds = new HashSet<string>();

    static int nextIdNumber = 1;

    static void Main()
    {
        bool running = true;

        while (running)
        {
            ShowMenu();
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    AddTask();
                    break;
                case "2":
                    ListTasks();
                    break;
                case "3":
                    SearchByCourse();
                    break;
                case "4":
                    UpdateTask();
                    break;
                case "5":
                    DeleteTask();
                    break;
                case "6":
                    ToggleComplete();
                    break;
                case "0":
                    running = false;
                    Console.WriteLine("Goodbye!");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Try again.\n");
                    break;
            }
        }
    }

    static void ShowMenu()
    {
        Console.WriteLine("=== STUDY PLANNER ===");
        Console.WriteLine("1) Add task");
        Console.WriteLine("2) List all tasks");
        Console.WriteLine("3) Search tasks by course");
        Console.WriteLine("4) Update a task");
        Console.WriteLine("5) Delete a task");
        Console.WriteLine("6) Mark task completed / not completed");
        Console.WriteLine("0) Quit");
        Console.WriteLine("=====================\n");
    }

    static void AddTask()
    {
        string title = ReadNonEmpty("Task title: ");
        string course = ReadNonEmpty("Course name: ");

        DateTime dueDate = ReadDate("Due date (YYYY-MM-DD): ");
        int priority = ReadIntInRange("Priority (1 = High, 3 = Low): ", 1, 3);

        string id = "T" + nextIdNumber++;

        StudyTask newTask = new StudyTask
        {
            Id = id,
            Title = title,
            Course = course,
            DueDate = dueDate,
            Priority = priority
        };

        tasksById[id] = newTask;
        taskList.Add(newTask);

        Console.WriteLine($"\nTask {id} added.\n");
    }

    static void ListTasks()
    {
        if (taskList.Count == 0)
        {
            Console.WriteLine("No tasks yet.\n");
            return;
        }

        Console.WriteLine("=== ALL TASKS ===");
        foreach (var task in taskList)
        {
            string status = completedTaskIds.Contains(task.Id) ? "Done" : "Not done";
            Console.WriteLine(FormatTask(task, status));
        }
        Console.WriteLine();
    }

    static void SearchByCourse()
    {
        string course = ReadNonEmpty("Course to search: ");

        bool found = false;
        foreach (var task in taskList)
        {
            if (task.Course.Equals(course, StringComparison.OrdinalIgnoreCase))
            {
                string status = completedTaskIds.Contains(task.Id) ? "Done" : "Not done";
                Console.WriteLine(FormatTask(task, status));
                found = true;
            }
        }

        if (!found)
        {
            Console.WriteLine("No tasks found for that course.");
        }

        Console.WriteLine();
    }

    static void UpdateTask()
    {
        string id = ReadNonEmpty("Enter task ID to update (e.g., T1): ");

        if (!tasksById.TryGetValue(id, out StudyTask task))
        {
            Console.WriteLine("Task not found.\n");
            return;
        }

        Console.WriteLine("Leave blank to keep the current value.");

        Console.Write($"New title (current: {task.Title}): ");
        string newTitle = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newTitle))
        {
            task.Title = newTitle;
        }

        Console.Write($"New course (current: {task.Course}): ");
        string newCourse = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newCourse))
        {
            task.Course = newCourse;
        }

        Console.Write($"New due date (YYYY-MM-DD, current: {task.DueDate:yyyy-MM-dd}): ");
        string dateInput = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(dateInput))
        {
            if (DateTime.TryParse(dateInput, out DateTime newDate))
            {
                task.DueDate = newDate;
            }
            else
            {
                Console.WriteLine("Invalid date, keeping old value.");
            }
        }

        Console.Write($"New priority 1–3 (current: {task.Priority}): ");
        string prioInput = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(prioInput))
        {
            if (int.TryParse(prioInput, out int newPriority) &&
                newPriority >= 1 && newPriority <= 3)
            {
                task.Priority = newPriority;
            }
            else
            {
                Console.WriteLine("Invalid priority, keeping old value.");
            }
        }

        Console.WriteLine("\nTask updated.\n");
    }

    static void DeleteTask()
    {
        string id = ReadNonEmpty("Enter task ID to delete: ");

        if (!tasksById.TryGetValue(id, out StudyTask task))
        {
            Console.WriteLine("Task not found.\n");
            return;
        }

        tasksById.Remove(id);
        taskList.Remove(task);
        completedTaskIds.Remove(id);

        Console.WriteLine("Task deleted.\n");
    }

    static void ToggleComplete()
    {
        string id = ReadNonEmpty("Enter task ID to mark complete / not complete: ");

        if (!tasksById.ContainsKey(id))
        {
            Console.WriteLine("Task not found.\n");
            return;
        }

        if (completedTaskIds.Contains(id))
        {
            completedTaskIds.Remove(id);
            Console.WriteLine("Task marked as not completed.\n");
        }
        else
        {
            completedTaskIds.Add(id);
            Console.WriteLine("Task marked as completed.\n");
        }
    }

    // helpers
    static string ReadNonEmpty(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string value = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(value))
            {
                return value.Trim();
            }
            Console.WriteLine("Please enter something.\n");
        }
    }

    static int ReadIntInRange(string prompt, int min, int max)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            if (int.TryParse(input, out int value) && value >= min && value <= max)
            {
                return value;
            }
            Console.WriteLine($"Enter a number between {min} and {max}.\n");
        }
    }

    static DateTime ReadDate(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            if (DateTime.TryParse(input, out DateTime date))
            {
                return date;
            }
            Console.WriteLine("Invalid date. Use format YYYY-MM-DD.\n");
        }
    }

    static string FormatTask(StudyTask task, string status)
    {
        return $"{task.Id} | {task.Title} | {task.Course} | Due: {task.DueDate:yyyy-MM-dd} | Priority: {task.Priority} | {status}";
    }
}
