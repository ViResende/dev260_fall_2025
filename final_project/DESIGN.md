Project Design & Rationale

Instructions: This project is a simple Study Planner console application that helps a student keep track of tasks for different courses. The goal is to practice using multiple data structures (Dictionary, List, and HashSet), handling user input safely, and organizing the program in a clean and understandable way.

I chose a very simple design because this is a personal planner with small data, and I wanted to focus on clarity instead of complexity. The app allows the user to add tasks, list tasks, search for tasks by course, and mark tasks as completed. The entire design supports these features in the easiest and most efficient way.

Data Model & Entities

Core entities:
My app only needs one main entity, which represents a study task that a student wants to track.

Your Answer:

Entity A: StudyTask

Name: StudyTask

Key fields: Id, Title, Course, DueDate, Priority

Identifiers: The Id is a string made from an auto-incrementing number (ex: “T1”, “T2”).

Relationships: No relationships to other entities. Each task is independent.

Entity B (if applicable):

N/A — The Study Planner only needs one entity.

Identifiers (keys) and why they're chosen:

Your Answer:
I chose to use a string Id because it is simple and readable (“T1”, “T2”, etc.). It also avoids duplicates.
Using a numeric auto-counter makes sure every task has a unique identifier without the user having to type one.
A string key works well with Dictionary<string, StudyTask>.

Data Structures — Choices & Justification

Below are the 3 structures required for the Bronze level. They also make sense for the features of this Study Planner.

Structure #1

Chosen Data Structure:
Dictionary<string, StudyTask>

Your Answer:
This dictionary stores all tasks using their Id as the key.

Purpose / Role in App:
It is used for fast lookup by Id, which is needed for update, delete, marking complete, and searching a specific task.

Why it fits:

Lookup by key is O(1) average time.

Perfect for “find task by Id”.

Tasks have unique Ids, which matches the Dictionary’s requirement.

Very easy to use in a console app.

Alternatives considered:

List<T>: Too slow for searching by Id because it needs a loop (O(n)).

SortedDictionary: Not needed because I don’t need sorted keys.

Database: Too complex for this assignment.

Structure #2

Chosen Data Structure:
List<StudyTask>

Your Answer:
The list keeps tasks in the order they were added.

Purpose / Role in App:
It is used to display all tasks in the order they were entered and to support search by course name.

Why it fits:

Very simple structure.

Easy to loop through (for listing and searching).

Works well for small/medium amounts of data.

Order matters for the user.

Alternatives considered:

Dictionary only: Not great for listing because order is not guaranteed.

LinkedList: No need for heavy insert/delete operations.

Queue: Wrong behavior — we don’t need FIFO.

Structure #3

Chosen Data Structure:
HashSet<string>

Your Answer:
This HashSet stores the Ids of tasks that are completed.

Purpose / Role in App:
It is used to quickly check if a task is completed.

Why it fits:

Checking membership is O(1) on average.

Ensures no duplicate Ids.

Very lightweight and ideal for true/false membership.

Alternatives considered:

List<string>: Would require O(n) scanning to check if an Id is completed.

Dictionary<string,bool>: More memory and less clean than a HashSet.

Additional Structures (if applicable)

N/A — No additional structures were needed. The app already meets Bronze and Silver requirements.

Comparers & String Handling

Comparer choices:
The app uses StringComparison.OrdinalIgnoreCase when comparing course names so that course search works even if the user types different casing.

Your Answer:

For keys:
Keys in the Dictionary use default comparer because Ids are auto-generated and always the same format (“T1”, “T2”, etc.).

For display sorting (if different):
N/A — Tasks are listed in the order they were added, not sorted.

Normalization rules:

Trim input text to remove extra spaces.

Convert course name comparisons to case-insensitive.

Validate numeric and date inputs inside loops.

Bad key examples avoided:

Using Title as the key (not unique, could repeat).

Using Course as a key (many tasks can belong to the same course).

Using user-typed Ids (risk of typos and duplicates).

Performance Considerations

Expected data scale:
The app is designed for about 20–200 tasks, since it is a personal study planner.
A List and Dictionary are more than enough for this scale.

Your Answer:

Performance bottlenecks identified:

Listing/searching by course uses a List loop (O(n)).
This is acceptable because n is small in this type of app.

No heavy computation or large memory usage.

Your Answer:
No major bottlenecks for the expected size.

Big-O analysis of core operations:

Your Answer:

Add: O(1) for Dictionary insert + O(1) for List append

Search:

By Id: O(1) using Dictionary

By Course: O(n) scanning the List

List: O(n)

Update: O(1) lookup + O(1) assignment

Delete:

Remove from Dictionary: O(1)

Remove from List: O(n) worst case (shifting elements)

Design Tradeoffs & Decisions

Key design decisions:

Keep one simple entity (StudyTask) to match assignment goals.

Use Dictionary for Id lookup and List for display.

Separate “completed tasks” into a HashSet so the app doesn’t need a bool inside the main model (cleaner design).

Use input validation loops to make the app safe for user mistakes.

Your Answer:

Tradeoffs made:

I chose List instead of a sorted structure. This means listing is not sorted, but it keeps the program simple.

I use O(n) search for courses, but it is fine for small data.

Storing completed Ids separately means double-checking both structures, but gives faster lookups.

Your Answer:

What you would do differently with more time:

Add saving and loading tasks to a file (persistence).

Add optional sorting by due date or priority.

Improve the UI with colors or a simple menu framework.

Add reminders or overdue task detection.

Your Answer: