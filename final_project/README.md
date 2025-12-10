Study Planner App

A simple console app that helps students organize and track their study tasks by course, due date, and priority.

What I Built (Overview)

Problem this solves:
I built a small Study Planner that helps students keep track of their tasks for each class. It solves the problem of forgetting deadlines, losing notes, or not knowing which task is more urgent. The app keeps everything in one place and lets the user add tasks, list all tasks, search by course, and mark tasks as completed. It is simple, fast, and easy to use.

Core features:

Add a study task

List all tasks

Search tasks by course

Mark tasks as completed

Prevent duplicate completed entries

Input validation for numbers and dates

How to Run

Requirements:

.NET 8.0 SDK

Windows, macOS, or Linux

No external dependencies

git clone <your-repo-url>
cd final_project
dotnet build


Run:

dotnet run


Sample data:
No external sample data is required. Tasks are entered manually through the console.

Using the App (Quick Start)

Typical workflow:

Start the app and choose an option from the menu.

Add a task with title, course, due date, and priority.

List or search tasks to see what you need to do.

Mark tasks as completed once you finish them.

Input tips:

Course names are case-insensitive (“math”, “Math”, “MATH” all match).

Dates must be valid (app keeps asking until correct).

Priority must be between 1 and 3.

Wrong input will not crash the program — the app safely asks again.

Data Structures (Brief Summary)

Dictionary<string, StudyTask> → Stores tasks by unique ID for fast lookup and updates.

List<StudyTask> → Keeps an ordered list of tasks for displaying and searching by course.

HashSet<string> → Tracks completed task IDs and prevents duplicates.

Manual Testing Summary
Scenario 1: Add a new task

Steps: Choose option 1, enter title, course, due date, priority.

Expected result: Task appears in list.

Actual result: Works correctly.

Scenario 2: Search by course

Steps: Add a task for “Math”, then choose option 3 and type “math”.

Expected result: Only Math tasks appear.

Actual result: Works correctly.

Scenario 3: Mark task as completed

Steps: Add a task → Select complete option → Enter ID.

Expected result: Task is stored as completed and cannot be completed twice.

Actual result: Works correctly.

Scenario 4: Invalid date input

Steps: Enter letters or invalid date.

Expected: App re-asks until a valid date is entered.

Actual: Works correctly.

Scenario 5: Priority outside 1–3

Steps: Enter priority 10.

Expected result: App forces retry until valid priority given.

Actual: Works correctly.

Known Limitations

Data is not saved between sessions (no file persistence).

No sorting by due date (could be added in future).

No editing of existing tasks.

Comparers & String Handling

Keys comparer:
I used StringComparer.OrdinalIgnoreCase so course names and IDs are compared without case sensitivity. This helps avoid duplicates like “math” vs “Math”.

Normalization:

Trim spaces

Convert to consistent casing when needed

Validate inputs before saving

Credits & AI Disclosure

Resources used:

Microsoft .NET Documentation

Class lecture examples

AI usage:
I used AI assistance to help structure my README, DESIGN.md, and understand how to create the .csproj file manually. I verified all code logic and tested the app myself.

Challenges and Solutions

Biggest challenge faced:
Creating the project structure correctly and making Visual Studio run the program without extra folders.

How I solved it:
I learned how to create a .csproj manually and link it with the teacher-provided files. I also tested the program step-by-step to make sure everything ran properly.

Most confusing concept:
Choosing the correct data structures and understanding when to use a Dictionary vs. List vs. HashSet.

Code Quality

What I'm most proud of:
My input validation, clean menu structure, and the combination of three data structures working together smoothly.

What I would improve:
Adding sorting, editing tasks, saving data to a JSON file, and improving the user interface.

Real-World Applications

This project relates to real task-management systems like Trello, Todoist, and school study apps. It uses the same ideas: unique IDs, fast search, filtering, and marking tasks as completed.

What I learned:
I learned how important data structure choices are. Dictionaries make lookup fast, Lists are useful for displaying ordered results, and HashSet is perfect for avoiding duplicates.
