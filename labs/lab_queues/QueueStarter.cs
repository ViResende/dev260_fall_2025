using System;
using System.Collections.Generic;

/*
=== QUICK REFERENCE GUIDE ===

Queue<T> Essential Operations:
- new Queue<SupportTicket>()        // Create empty queue
- queue.Enqueue(item)               // Add item to back (FIFO)
- queue.Dequeue()                   // Remove and return front item
- queue.Peek()                      // Look at front item (don't remove)
- queue.Clear()                     // Remove all items
- queue.Count                       // Get number of items

Safety Rules:
- ALWAYS check queue.Count > 0 before Dequeue() or Peek()
- Empty queue Dequeue() throws InvalidOperationException
- Empty queue Peek() throws InvalidOperationException

Common Patterns:
- Guard clause: if (queue.Count > 0) { ... }
- FIFO order: First item enqueued is first item dequeued
- Enumeration: foreach gives front-to-back order

Helpful Icons:
- ✅ Success
- ❌ Error
- 👀 Look
- 📋 Display
- ℹ️ Information
- 📊 Stats
- 🎫 Ticket
- 🔄 Process
*/

namespace QueueLab
{
    /// <summary>
    /// Student skeleton version - follow along with instructor to build this out!
    /// Complete the TODO steps to build a complete IT Support Desk Queue system.
    /// </summary>
    class Program
    {
        // TODO Step 1: Set up your data structures and tracking variables
        // Queue to store all support tickets
        private static Queue<SupportTicket> ticketQueue = new Queue<SupportTicket>();

        // Ticket counter 
        private static int ticketCounter = 1;

        // Track session start time to show statistics at the end
        private static DateTime sessionStart = DateTime.Now;

        // Count the total number of operations performed (submit, process, clear, etc.)
        private static int totalOperations = 0;

        // Pre-defined ticket options for easy selection during demos
        private static readonly string[] CommonIssues = {
            "Login issues - cannot access email",
            "Password reset request",
            "Software installation help",
            "Printer not working",
            "Internet connection problems",
            "Computer running slowly",
            "Email not sending/receiving",
            "VPN connection issues",
            "Application crashes on startup",
            "File recovery assistance",
            "Monitor display problems",
            "Keyboard/mouse not responding",
            "Video conference setup help",
            "File sharing permission issues",
            "Security software alert"
        };

        static void Main(string[] args)
        {
            Console.WriteLine("🎫 IT Support Desk Queue Management");
            Console.WriteLine("===================================");
            Console.WriteLine("Building a ticket queue system with FIFO processing\n");

            bool running = true;
            while (running)
            {
                DisplayMenu();
                string choice = Console.ReadLine()?.ToLower() ?? "";

                switch (choice)
                {
                    case "1":
                    case "submit":
                        HandleSubmitTicket();
                        break;
                    case "2":
                    case "process":
                        HandleProcessTicket();
                        break;
                    case "3":
                    case "peek":
                    case "next":
                        HandlePeekNext();
                        break;
                    case "4":
                    case "display":
                    case "queue":
                        HandleDisplayQueue();
                        break;
                    case "5":
                    case "urgent":
                        HandleUrgentTicket();
                        break;
                    case "6":
                    case "search":
                        HandleSearchTicket();
                        break;
                    case "7":
                    case "stats":
                        HandleQueueStatistics();
                        break;
                    case "8":
                    case "clear":
                        HandleClearQueue();
                        break;
                    case "9":
                    case "exit":
                        running = false;
                        ShowSessionSummary();
                        break;
                    default:
                        Console.WriteLine("❌ Invalid choice. Please try again.\n");
                        break;
                }
            }
        }

        static void DisplayMenu()
        {
            Console.WriteLine();

            string nextTicket = ticketQueue.Count > 0 ? ticketQueue.Peek().TicketId : "None";

            Console.WriteLine("┌─ Support Desk Queue Operations ─────────────────┐");
            Console.WriteLine("│ 1. Submit      │ 2. Process    │ 3. Peek/Next  │");
            Console.WriteLine("│ 4. Display     │ 5. Urgent     │ 6. Search      │");
            Console.WriteLine("│ 7. Stats       │ 8. Clear      │ 9. Exit        │");
            Console.WriteLine("└─────────────────────────────────────────────────┘");
            Console.WriteLine($"Queue: {ticketQueue.Count} tickets | Next: {nextTicket} | Total ops: {totalOperations}");
            Console.Write("\nChoose operation (number or name): ");
        }

        // TODO Step 2: Handle submitting new tickets (Enqueue)
        static void HandleSubmitTicket()
        {
            Console.WriteLine("\n📝 Submit New Support Ticket");
            Console.WriteLine("Choose from common issues or enter custom:");

            // Math.Min() for safe array access - prevents index out of bounds errors
            // Display quick selection options
            for (int i = 0; i < Math.Min(5, CommonIssues.Length); i++)
            {
                Console.WriteLine($"  {i + 1}. {CommonIssues[i]}");
            }
            Console.WriteLine("  6. Enter custom issue");
            Console.WriteLine("  0. Cancel");
            
            Console.Write("\nSelect option (0-6): ");
            string? choice = Console.ReadLine();
            
            if (choice == "0")
            {
                Console.WriteLine("❌ Ticket submission cancelled.\n");
                return;
            }
            
            string description = "";
            // int.TryParse() for safe number conversion - better than catching exceptions
            if (int.TryParse(choice, out int index) && index >= 1 && index <= 5)
            {
                description = CommonIssues[index - 1];
            }
            else if (choice == "6")
            {
                Console.Write("Enter issue description: ");
                description = Console.ReadLine()?.Trim() ?? "";
            }
            
            // Input validation with multiple options - professional apps handle user choice
            if (string.IsNullOrWhiteSpace(description))
            {
                Console.WriteLine("❌ Description cannot be empty. Ticket submission cancelled.\n");
                return;
            }
            // TODO:
            // 1) Make an ID like T001, T002...
            string ticketId = $"T{ticketCounter:D3}";

            // 2) Build the ticket (Normal priority, submitted by User)
            var newTicket = new SupportTicket(ticketId, description, "Normal", "User");

            // 3) Enqueue it
            ticketQueue.Enqueue(newTicket);

            // 4) Update counters
            ticketCounter++;
            totalOperations++;

            // 5) Confirm to the user
            Console.WriteLine($"✅ Ticket {newTicket.TicketId} submitted.");
            Console.WriteLine($"   Description: {newTicket.Description}");
            Console.WriteLine($"   Position in queue: {ticketQueue.Count}\n");
        }

        // TODO Step 3: Handle processing tickets (Dequeue)
        static void HandleProcessTicket()
        {
            Console.WriteLine("\n🔄 Process Next Ticket");

            // 1. Check if any tickets exist
            if (ticketQueue.Count == 0)
            {
                Console.WriteLine("❌ No tickets in queue to process.\n");
                return;
            }

            // 2. Dequeue (remove and get the first ticket)
            SupportTicket currentTicket = ticketQueue.Dequeue();

            // 3. Update counters
            totalOperations++;

            // 4. Show details of the ticket being processed
            Console.WriteLine($"✅ Processing Ticket: {currentTicket.TicketId}");
            Console.WriteLine(currentTicket.ToDetailedString());

            // 5. Show what's next (if available)
            if (ticketQueue.Count > 0)
            {
                var nextTicket = ticketQueue.Peek();
                Console.WriteLine($"\n👀 Next in line: {nextTicket.TicketId} - {nextTicket.Description}");
            }
            else
            {
                Console.WriteLine("\n🎉 All tickets have been processed! Queue is now empty.");
            }

            Console.WriteLine();
        }


        // TODO Step 4: Handle peeking at next ticket
        static void HandlePeekNext()
        {
            Console.WriteLine("\n👀 View Next Ticket");

            // 1. If empty — nothing to show
            if (ticketQueue.Count == 0)
            {
                Console.WriteLine("❌ Queue is empty. No tickets to view.\n");
                return;
            }

            // 2. Get the next ticket WITHOUT removing it (Peek)
            SupportTicket nextTicket = ticketQueue.Peek();

            // 3. Display ticket details
            Console.WriteLine("Next ticket to be processed:");
            Console.WriteLine(nextTicket.ToDetailedString());

            // 4. Show position (always 1 because first in line)
            Console.WriteLine($"Position in queue: 1 of {ticketQueue.Count}\n");
        }


        // TODO Step 5: Handle displaying the full queue
        static void HandleDisplayQueue()
        {
            Console.WriteLine("\n📋 Current Support Queue (FIFO Order):");

            // 1. If queue is empty
            if (ticketQueue.Count == 0)
            {
                Console.WriteLine("Queue is empty - no tickets waiting.\n");
                return;
            }

            // 2. Display total count
            Console.WriteLine($"Total Tickets in Queue: {ticketQueue.Count}");

            int position = 1;

            // 3. Loop through queue
            foreach (var ticket in ticketQueue)
            {
                // Format position as 2 digits (01, 02...)
                string posFormatted = position.ToString("D2");

                // Mark the first item with an arrow to show it’s next
                string marker = position == 1 ? "  ← Next to process" : "";

                // Use the ticket's ToString() method for default display
                Console.WriteLine($"{posFormatted}. {ticket}{marker}");

                position++;
            }

            Console.WriteLine();
        }


        // TODO Step 6: Handle clearing the queue
        static void HandleClearQueue()
{
    Console.WriteLine("\n🧹 Clear All Tickets");

    if (ticketQueue.Count == 0)
    {
        Console.WriteLine("Queue is already empty. Nothing to clear.\n");
        return;
    }

    int toRemove = ticketQueue.Count;
    Console.Write($"This will remove {toRemove} tickets. Are you sure? (y/N): ");
    string resp = (Console.ReadLine() ?? "").Trim().ToLower();

    if (resp == "y" || resp == "yes")
    {
        ticketQueue.Clear();
        totalOperations++;
        Console.WriteLine("✅ Queue cleared.\n");
    }
    else
    {
        Console.WriteLine("❌ Clear operation cancelled.\n");
    }
}


        // TODO Step 7: Handle urgent ticket submission (Priority)
        static void HandleUrgentTicket()
        {
            Console.WriteLine("\n🚨 Submit Urgent Ticket");
            Console.WriteLine("Urgent tickets are marked as high priority!");

            Console.Write("Enter urgent issue description: ");
            string? description = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(description))
            {
                Console.WriteLine("❌ Description cannot be empty. Urgent ticket cancelled.\n");
                return;
            }

            // 1. Create ID like U001, U002...
            string ticketId = $"U{ticketCounter:D3}";

            // 2. Create ticket with "Urgent" priority
            SupportTicket urgentTicket = new SupportTicket(ticketId, description, "Urgent", "User");

            // 3. Add to queue (FIFO - basic implementation)
            ticketQueue.Enqueue(urgentTicket);

            // 4. Update total operations & ticket counter
            totalOperations++;
            ticketCounter++;

            // 5. Confirm
            Console.WriteLine($"✅ Urgent Ticket {ticketId} submitted!");
            Console.WriteLine($"   Description: {description}");
            Console.WriteLine($"   Position in queue: {ticketQueue.Count}");
            Console.WriteLine("\n(Note: In a real system, urgent tickets might jump to the front.)\n");
        }


        // TODO Step 8: Handle searching for tickets
        static void HandleSearchTicket()
        {
            Console.WriteLine("\n🔎 Search Tickets");

            if (ticketQueue.Count == 0)
            {
                Console.WriteLine("Queue is empty. No tickets to search.\n");
                return;
            }

            Console.Write("Enter ticket ID or a keyword: ");
            string term = (Console.ReadLine() ?? "").Trim();

            if (string.IsNullOrWhiteSpace(term))
            {
                Console.WriteLine("❌ Search term cannot be empty.\n");
                return;
            }

            string needle = term.ToLower();
            bool found = false;
            int position = 1;

            Console.WriteLine("\nSearch results:");
            foreach (var t in ticketQueue)
            {
                if (t.TicketId.ToLower().Contains(needle) ||
                    (t.Description ?? "").ToLower().Contains(needle))
                {
                    Console.WriteLine($"{position:D2}. {t}");
                    found = true;
                }
                position++;
            }

            if (!found)
                Console.WriteLine($"No tickets found matching \"{term}\".");

            Console.WriteLine();
            totalOperations++;
        }


        static void HandleQueueStatistics()
        {
            Console.WriteLine("\n📊 Queue Statistics");
            
            TimeSpan sessionDuration = DateTime.Now - sessionStart;
            
            Console.WriteLine($"Current Queue Status:");
            Console.WriteLine($"- Tickets in queue: {ticketQueue.Count}");
            Console.WriteLine($"- Total operations: {totalOperations}");
            Console.WriteLine($"- Session duration: {sessionDuration:hh\\:mm\\:ss}");
            Console.WriteLine($"- Next ticket ID: T{ticketCounter:D3}");
            
            if (ticketQueue.Count > 0)
            {
                var oldestTicket = ticketQueue.Peek();
                Console.WriteLine($"- Longest waiting: {oldestTicket.TicketId} ({oldestTicket.GetFormattedWaitTime()})");
                
                // Count by priority
                int normal = 0, high = 0, urgent = 0;
                foreach (var ticket in ticketQueue)
                {
                    switch (ticket.Priority.ToLower())
                    {
                        case "normal": normal++; break;
                        case "high": high++; break;
                        case "urgent": urgent++; break;
                    }
                }
                Console.WriteLine($"- By priority: 🟢 Normal({normal}) 🟡 High({high}) 🔴 Urgent({urgent})");
            }
            else
            {
                Console.WriteLine("- Queue is empty");
            }
            Console.WriteLine();
        }

        static void ShowSessionSummary()
        {
            Console.WriteLine("\n📋 Final Session Summary");
            Console.WriteLine("========================");
            
            TimeSpan sessionDuration = DateTime.Now - sessionStart;
            
            Console.WriteLine($"Session Statistics:");
            Console.WriteLine($"- Duration: {sessionDuration:hh\\:mm\\:ss}");
            Console.WriteLine($"- Total operations: {totalOperations}");
            Console.WriteLine($"- Tickets remaining: {ticketQueue.Count}");
            
            if (ticketQueue.Count > 0)
            {
                Console.WriteLine($"- Unprocessed tickets:");
                int position = 1;
                foreach (var ticket in ticketQueue)
                {
                    Console.WriteLine($"  {position:D2}. {ticket}");
                    position++;
                }
                Console.WriteLine("\n⚠️ Remember to process remaining tickets!");
            }
            else
            {
                Console.WriteLine("✨ All tickets processed - excellent work!");
            }
            
            Console.WriteLine("\nThank you for using the Support Desk Queue System!");
            Console.WriteLine("You've learned FIFO queue operations and real-world ticket management! 🎫\n");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}