// Week 3 — Arrays & Sorting 
// By: Vitoria Resende
// What this app has:
// - Part A: Tic‑Tac‑Toe using a 2D array (char[3,3]).
//   You can see the board, make moves, it checks win/draw, and you can play again.
// - Part B: Book Catalog.
//   Loads book titles, normalizes text, sorts with my own recursive MergeSort
//   (no Array.Sort or LINQ), builds a 2D index int[26,26] to jump fast to a slice,
//   then does binary search in that small slice. Shows suggestions if no exact match.
//
// How to run:
//   1) dotnet run
//   2) Choose 1 for Tic‑Tac‑Toe or 2 for Book Catalog.
//   3) For Part B, put a books.txt next to Program.cs (one title per line) or use the sample.
//
// Big‑O in short words:
//   - MergeSort: O(n log n) time, O(n) extra space.
//   - Build 2D index: O(n) (one pass over sorted list).
//   - Lookup: O(1) to get slice + O(log k) to binary search inside that slice.
//

using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    // ============= Entry Menu =============
    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Week 3 — Arrays & Sorting");
            Console.WriteLine("1) Part A: Tic‑Tac‑Toe (2D array)");
            Console.WriteLine("2) Part B: Book Catalog (MergeSort + 2D index)");
            Console.WriteLine("0) Exit");
            Console.Write("Choose: ");
            string? choice = Console.ReadLine();

            if (choice == "1")
                TicTacToe.Run();
            else if (choice == "2")
                BookCatalogCLI.Run();
            else if (choice == "0")
                return;
            else
            {
                Console.WriteLine("Invalid choice. Press Enter.");
                Console.ReadLine();
            }
        }
    }
}

// ========================= Part A — Tic‑Tac‑Toe =========================
static class TicTacToe
{
    // Note: Using a 3x3 rectangular array as required (char[,]).
    static char[,] board = new char[3, 3];

    public static void Run()
    {
        while (true)
        {
            ResetBoard();
            char current = 'X';
            int moves = 0;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Tic‑Tac‑Toe — 3x3");
                PrintBoard();
                Console.WriteLine($"Player {current}, enter your move.");

                int row = ReadInt("Row (0-2): ", 0, 2);
                int col = ReadInt("Col (0-2): ", 0, 2);

                if (board[row, col] != ' ')
                {
                    Console.WriteLine("That spot is taken. Press Enter and try again.");
                    Console.ReadLine();
                    continue;
                }

                board[row, col] = current;
                moves++;

                if (IsWin(current))
                {
                    Console.Clear();
                    PrintBoard();
                    Console.WriteLine($"Player {current} wins!\n");
                    break;
                }

                if (moves == 9)
                {
                    Console.Clear();
                    PrintBoard();
                    Console.WriteLine("It's a draw!\n");
                    break;
                }

                // swap player
                current = (current == 'X') ? 'O' : 'X';
            }

            Console.Write("Play again? (y/n): ");
            string? again = Console.ReadLine();
            if (!string.Equals(again, "y", StringComparison.OrdinalIgnoreCase))
                break;
        }
    }

    static void ResetBoard()
    {
        for (int r = 0; r < 3; r++)
            for (int c = 0; c < 3; c++)
                board[r, c] = ' ';
    }

    static void PrintBoard()
    {
        Console.WriteLine("   0   1   2");
        for (int r = 0; r < 3; r++)
        {
            Console.Write($"{r}  ");
            for (int c = 0; c < 3; c++)
            {
                Console.Write(board[r, c]);
                if (c < 2) Console.Write(" | ");
            }
            Console.WriteLine();
            if (r < 2) Console.WriteLine("  ---+---+---");
        }
        Console.WriteLine();
    }

    static int ReadInt(string prompt, int min, int max)
    {
        while (true)
        {
            Console.Write(prompt);
            string? s = Console.ReadLine();
            if (int.TryParse(s, out int value) && value >= min && value <= max)
                return value;
            Console.WriteLine("Invalid number. Try again.");
        }
    }

    static bool IsWin(char p)
    {
        // rows
        for (int r = 0; r < 3; r++)
            if (board[r, 0] == p && board[r, 1] == p && board[r, 2] == p) return true;
        // cols
        for (int c = 0; c < 3; c++)
            if (board[0, c] == p && board[1, c] == p && board[2, c] == p) return true;
        // diagonals
        if (board[0, 0] == p && board[1, 1] == p && board[2, 2] == p) return true;
        if (board[0, 2] == p && board[1, 1] == p && board[2, 0] == p) return true;
        return false;
    }
}

// ====================== Part B — Book Catalog ======================

// This class holds original and normalized title.
class BookTitle
{
    public string Original;
    public string Normalized;

    public BookTitle(string original, string normalized)
    {
        Original = original;
        Normalized = normalized;
    }
}

static class BookCatalogCLI
{
    public static void Run()
    {
        Console.Clear();
        Console.WriteLine("Book Catalog — MergeSort + 2D Index\n");
        Console.Write("Enter path to books.txt (or leave blank to use ./books.txt): ");
        string? path = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(path)) path = "books.txt";

        BookCatalog catalog = new BookCatalog();
        try
        {
            catalog.Load(path!);
        }
        catch (Exception)
        {
            Console.WriteLine("Could not load file. Using a small built‑in sample.\n");
            catalog.LoadFromList(new List<string>
            {
                "The Hobbit",
                "A Tale of Two Cities",
                "The Lord of the Rings",
                "Harry Potter and the Sorcerer's Stone",
                "Dune",
                "Pride and Prejudice",
                "Animal Farm",
                "The Great Gatsby",
                "The Catcher in the Rye",
                "The Da Vinci Code"
            });
        }

        catalog.Sort();          
        catalog.BuildIndex2D();  // fill start/end ranges for [A..Z][A..Z]

        Console.WriteLine("Type a title to search (exact). Type 'exit' to leave.\n");
        while (true)
        {
            Console.Write("Enter a book title: ");
            string? q = Console.ReadLine();
            if (q == null) continue;
            if (q.Trim().Equals("exit", StringComparison.OrdinalIgnoreCase)) break;

            var result = catalog.Lookup(q);
            if (result.found)
            {
                Console.WriteLine($"Found: {result.match!.Original}\n");
            }
            else
            {
                Console.WriteLine("No exact match. Suggestions:");
                foreach (var s in result.suggestions)
                    Console.WriteLine("  • " + s.Original);
                Console.WriteLine();
            }
        }

        Console.WriteLine("Done. Press Enter.");
        Console.ReadLine();
    }
}

class BookCatalog
{
    
    private const bool IGNORE_LEADING_ARTICLES = true; // A, AN, THE (extra credit)

    private List<BookTitle> titles = new List<BookTitle>();

    // Sorted arrays after MergeSort 
    private BookTitle[] sorted;           // holds objects in sorted order (by Normalized)

    // 2D index for [firstLetter, secondLetter] ranges
    private int[,] start = new int[26, 26];
    private int[,] end = new int[26, 26];   // exclusive

    public BookCatalog()
    {
        sorted = Array.Empty<BookTitle>();
        // we will fill start with -1 to mark empty later
    }

    // ------------------- Load -------------------
    public void Load(string filePath)
    {
        var lines = File.ReadAllLines(filePath);
        LoadFromList(new List<string>(lines));
    }

    public void LoadFromList(List<string> lines)
    {
        titles.Clear();
        foreach (var line in lines)
        {
            string original = line.TrimEnd('\r', '\n');
            if (string.IsNullOrWhiteSpace(original)) continue;
            string normalized = Normalize(original);
            titles.Add(new BookTitle(original, normalized));
        }
    }

    // ------------------- Normalize -------------------
    private string Normalize(string s)
    {
        // 1) Trim spaces
        s = s.Trim();
        // 2) Uppercase for consistent compare
        s = s.ToUpperInvariant();
        // 3) Optionally remove leading articles
        if (IGNORE_LEADING_ARTICLES)
        {
            if (s.StartsWith("THE ")) s = s.Substring(4);
            else if (s.StartsWith("A ")) s = s.Substring(2);
            else if (s.StartsWith("AN ")) s = s.Substring(3);
        }
        return s;
    }

    // ------------------- Sort (MergeSort) -------------------
    public void Sort()
    {
        // Copy list to array and merge sort by Normalized.
        sorted = titles.ToArray();
        if (sorted.Length <= 1) return;
        var buffer = new BookTitle[sorted.Length];
        MergeSortRecursive(sorted, buffer, 0, sorted.Length - 1);
    }

    // Recursive split/merge. 
    private void MergeSortRecursive(BookTitle[] arr, BookTitle[] buf, int left, int right)
    {
        if (left >= right) return; // base case: 1 element
        int mid = (left + right) / 2;
        MergeSortRecursive(arr, buf, left, mid);
        MergeSortRecursive(arr, buf, mid + 1, right);
        Merge(arr, buf, left, mid, right);
    }

    private void Merge(BookTitle[] arr, BookTitle[] buf, int left, int mid, int right)
    {
        int i = left;
        int j = mid + 1;
        int k = left;

        while (i <= mid && j <= right)
        {
            // Compare by Normalized string
            if (string.Compare(arr[i].Normalized, arr[j].Normalized, StringComparison.Ordinal) <= 0)
            {
                buf[k++] = arr[i++];
            }
            else
            {
                buf[k++] = arr[j++];
            }
        }
        while (i <= mid) buf[k++] = arr[i++];
        while (j <= right) buf[k++] = arr[j++];

        for (int idx = left; idx <= right; idx++)
            arr[idx] = buf[idx];
    }

    // ------------------- 2D Index Build -------------------
    public void BuildIndex2D()
    {
        // fill start with -1 to mark empty buckets
        for (int a = 0; a < 26; a++)
            for (int b = 0; b < 26; b++)
            {
                start[a, b] = -1;
                end[a, b] = -1;
            }

        if (sorted.Length == 0) return;

        // Scan once through sorted to find ranges for first two letters
        int n = sorted.Length;

        int currentA = -1; // 0..25
        int currentB = -1; // 0..25
        int rangeStart = 0;

        for (int i = 0; i < n; i++)
        {
            var (a, b) = FirstTwoLetterIndexes(sorted[i].Normalized);
            if (i == 0)
            {
                currentA = a; currentB = b; rangeStart = 0;
            }
            else
            {
                if (a != currentA || b != currentB)
                {
                    // close old range
                    if (currentA >= 0 && currentB >= 0)
                    {
                        if (start[currentA, currentB] == -1)
                            start[currentA, currentB] = rangeStart;
                        end[currentA, currentB] = i; // exclusive
                    }
                    // start new
                    currentA = a; currentB = b; rangeStart = i;
                }
            }
        }
        // close last range at n
        if (currentA >= 0 && currentB >= 0)
        {
            if (start[currentA, currentB] == -1) start[currentA, currentB] = rangeStart;
            end[currentA, currentB] = n; // exclusive
        }
    }

    // Helper: convert first two letters into [0..25]. Non‑letter => map to 'A' (0).
    private (int, int) FirstTwoLetterIndexes(string s)
    {
        char a = 'A', b = 'A'; // defaults
        if (s.Length >= 1 && s[0] >= 'A' && s[0] <= 'Z') a = s[0];
        if (s.Length >= 2 && s[1] >= 'A' && s[1] <= 'Z') b = s[1];
        int ai = a - 'A';
        int bi = b - 'A';
        if (ai < 0 || ai > 25) ai = 0;
        if (bi < 0 || bi > 25) bi = 0;
        return (ai, bi);
    }

    // ------------------- Lookup -------------------
    public (bool found, BookTitle? match, List<BookTitle> suggestions) Lookup(string queryOriginal)
    {
        string q = Normalize(queryOriginal);
        var (ai, bi) = FirstTwoLetterIndexes(q);
        int s = start[ai, bi];
        int e = end[ai, bi]; // exclusive

        if (s == -1 || e == -1 || s >= e)
        {
            
            int ins = LowerBound(sorted, q, 0, sorted.Length);
            return (false, null, SuggestFromGlobal(ins, 5));
        }

        
        int idx = BinarySearchNormalized(sorted, q, s, e);
        if (idx >= 0)
        {
            return (true, sorted[idx], new List<BookTitle>());
        }
        else
        {
            int ins = LowerBound(sorted, q, s, e);
            return (false, null, SuggestFromSlice(ins, s, e, 5));
        }
    }

    // Standard binary search by Normalized in [s,e). Returns index or -1.
    private int BinarySearchNormalized(BookTitle[] arr, string key, int s, int e)
    {
        int lo = s, hi = e - 1;
        while (lo <= hi)
        {
            int mid = (lo + hi) / 2;
            int cmp = string.Compare(arr[mid].Normalized, key, StringComparison.Ordinal);
            if (cmp == 0) return mid;
            if (cmp < 0) lo = mid + 1; else hi = mid - 1;
        }
        return -1;
    }

    // LowerBound: first index >= key within [s,e)
    private int LowerBound(BookTitle[] arr, string key, int s, int e)
    {
        int lo = s, hi = e;
        while (lo < hi)
        {
            int mid = (lo + hi) / 2;
            if (string.Compare(arr[mid].Normalized, key, StringComparison.Ordinal) < 0)
                lo = mid + 1;
            else
                hi = mid;
        }
        return lo;
    }

    private List<BookTitle> SuggestFromSlice(int ins, int s, int e, int count)
    {
        var list = new List<BookTitle>();
        
        int left = ins - 1;
        int right = ins;
        while (list.Count < count && (left >= s || right < e))
        {
            if (right < e)
            {
                list.Add(sorted[right]);
                right++;
            }
            if (list.Count >= count) break;
            if (left >= s)
            {
                list.Add(sorted[left]);
                left--;
            }
        }
        return list;
    }

    private List<BookTitle> SuggestFromGlobal(int ins, int count)
    {
        
        return SuggestFromSlice(ins, 0, sorted.Length, count);
    }
}
