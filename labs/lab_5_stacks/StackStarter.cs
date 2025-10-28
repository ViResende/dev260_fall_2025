using System;
using System.Collections.Generic;

/*
=== QUICK REFERENCE GUIDE ===

Stack<T> Essential Operations:
- new Stack<string>()           // Create empty stack
- stack.Push(item)              // Add item to top (LIFO)
- stack.Pop()                   // Remove and return top item
- stack.Peek()                  // Look at top item (don't remove)
- stack.Clear()                 // Remove all items
- stack.Count                   // Get number of items

Safety Rules:
- ALWAYS check stack.Count > 0 before Pop() or Peek()
- Empty stack Pop() throws InvalidOperationException
- Empty stack Peek() throws InvalidOperationException

Common Patterns:
- Guard clause: if (stack.Count > 0) { ... }
- LIFO order: Last item pushed is first item popped
- Enumeration: foreach gives top-to-bottom order

Helpful icons!:
- ✅ Success
- ❌ Error
- 👀 Look
- 📋 Display out
- ℹ️ Information
- 📊 Stats
- 📝 Write
*/

using System;
using System.Collections.Generic;

namespace StackLab
{
    class StudentSkeleton
    {
        // Step 1 - Stacks
        static Stack<string> actionHistory = new Stack<string>();
        static Stack<string> undoHistory = new Stack<string>();
        static Stack<string> redoHistory = new Stack<string>();

        // Step 2 - total operations counter
        static int totalOperations = 0;

        static void Main(string[] args)
        {
            Console.WriteLine("=== Interactive Stack Demo ===");
            Console.WriteLine("Building an action history system with undo/redo\n");

            bool running = true;
            while (running)
            {
                DisplayMenu();
                string choice = Console.ReadLine()?.ToLower() ?? "";

                switch (choice)
                {
                    case "1": HandlePush(); break;
                    case "2": HandlePop(); break;
                    case "3": HandlePeek(); break;
                    case "4": HandleDisplay(); break;
                    case "5": HandleClear(); break;
                    case "6": HandleUndo(); break;
                    case "7": HandleRedo(); break;
                    case "8": ShowStatistics(); break;
                    case "9": running = false; ShowSessionSummary(); break;
                    default: Console.WriteLine("❌ Invalid choice.\n"); break;
                }
            }
        }

        static void DisplayMenu()
        {
            Console.WriteLine("┌─ Stack Operations Menu ─────────────────────────┐");
            Console.WriteLine("│ 1. Push │ 2. Pop │ 3. Peek │");
            Console.WriteLine("│ 4. Display │ 5. Clear │ 6. Undo │");
            Console.WriteLine("│ 7. Redo │ 8. Stats │ 9. Exit │");
            Console.WriteLine("└─────────────────────────────────────────────────┘");

            Console.WriteLine($"\nℹ️ Stack: {actionHistory.Count} | Undo: {undoHistory.Count} | Redo: {redoHistory.Count} | Ops: {totalOperations}");
            Console.Write("\nChoose: ");
        }

        static void HandlePush()
        {
            Console.Write("📝 Enter action: ");
            string input = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("❌ No action entered.");
                return;
            }

            actionHistory.Push(input.Trim());
            undoHistory.Clear();
            redoHistory.Clear();
            totalOperations++;

            Console.WriteLine($"✅ Added: {input}");
        }

        static void HandlePop()
        {
            if (actionHistory.Count == 0)
            {
                Console.WriteLine("❌ Stack empty.");
                return;
            }

            string removed = actionHistory.Pop();
            undoHistory.Push(removed);
            redoHistory.Clear();
            totalOperations++;

            Console.WriteLine($"✅ Removed: {removed}");
        }

        static void HandlePeek()
        {
            if (actionHistory.Count == 0)
            {
                Console.WriteLine("👀 Empty.");
                return;
            }

            Console.WriteLine($"👀 Top: {actionHistory.Peek()}");
        }

        static void HandleDisplay()
        {
            Console.WriteLine("\n📋 Stack (top → bottom):");

            if (actionHistory.Count == 0)
            {
                Console.WriteLine("[empty]");
                return;
            }

            bool first = true;
            foreach (var item in actionHistory)
            {
                Console.WriteLine(first ? $"TOP → {item}" : $"      {item}");
                first = false;
            }
        }

        static void HandleClear()
        {
            if (actionHistory.Count == 0)
            {
                Console.WriteLine("ℹ️ Nothing to clear.");
                return;
            }

            int cleared = actionHistory.Count;
            actionHistory.Clear();
            undoHistory.Clear();
            redoHistory.Clear();
            totalOperations++;

            Console.WriteLine($"✅ Cleared {cleared} items.");
        }

        static void HandleUndo()
        {
            if (undoHistory.Count == 0)
            {
                Console.WriteLine("ℹ️ Nothing to undo.");
                return;
            }

            string restored = undoHistory.Pop();
            actionHistory.Push(restored);
            redoHistory.Push(restored);
            totalOperations++;

            Console.WriteLine($"✅ Undo: {restored}");
        }

        static void HandleRedo()
        {
            if (redoHistory.Count == 0)
            {
                Console.WriteLine("ℹ️ Nothing to redo.");
                return;
            }

            string again = redoHistory.Pop();

            if (actionHistory.Count > 0 && actionHistory.Peek() == again)
            {
                actionHistory.Pop();
                undoHistory.Push(again);
                totalOperations++;
                Console.WriteLine($"✅ Redo: {again}");
            }
            else
            {
                Console.WriteLine("⚠️ Redo mismatch.");
            }
        }

        static void ShowStatistics()
        {
            Console.WriteLine("\n📊 Stats");
            Console.WriteLine($"Stack: {actionHistory.Count}");
            Console.WriteLine($"Undo: {undoHistory.Count}");
            Console.WriteLine($"Redo: {redoHistory.Count}");
            Console.WriteLine($"Ops:  {totalOperations}");
        }

        static void ShowSessionSummary()
        {
            Console.WriteLine("\n===== Summary =====");
            Console.WriteLine($"Operations: {totalOperations}");
            Console.WriteLine($"Final size: {actionHistory.Count}");

            if (actionHistory.Count > 0)
            {
                Console.WriteLine("Remaining:");
                foreach (var item in actionHistory)
                    Console.WriteLine($"- {item}");
            }

            Console.WriteLine("\nPress any key...");
            Console.ReadKey();
        }
    }
}
