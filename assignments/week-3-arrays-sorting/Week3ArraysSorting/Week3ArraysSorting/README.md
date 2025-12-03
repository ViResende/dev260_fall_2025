Week 3 — Arrays & Sorting

By: Vitoria Resende

 Part A — Tic-Tac-Toe (40 pts)

Board size: 3×3 using char[,] board

How to play:

The program shows the board with row and column numbers (0–2).

Players take turns entering a row and column.

The move is rejected if the spot is taken.

The game checks for 3 in a row (rows, columns, diagonals).

Declares a win or draw.

After finishing, you can start a new game without restarting the app.

How the 2D array is used:
Each cell in the array stores 'X', 'O', or a space ' '.
The program uses nested loops to draw the grid and check for wins.

Part B — Book Catalog (40 pts)

Algorithm used: Recursive MergeSort (no Array.Sort or LINQ).
Index: 2D index int[26,26] for quick lookup by the first two letters.

Normalization rules:

Converts titles to uppercase.

Trims spaces.

Ignores leading articles (“A”, “AN”, “THE”).

Keeps both original and normalized versions.

How the 2D index works:

Each letter pair (A–Z × A–Z) stores the start and end range in the sorted array.

Lookup finds the range instantly (O(1)) and runs a binary search inside (O(log k)).

Big-O summary:

MergeSort: O(n log n) time, O(n) space.

Build index: O(n).

Lookup: O(1) + O(log k).
