// File: Program.cs
using System;
using System.Collections.Generic;

namespace LoopsAndConditionalsLab
{
    // I’m keeping everything in one file to make it easy to read and test.
    class Program
    {
        static void Main(string[] args)
        {
            // ===== Task 1: Sum of even numbers (1..100) using different loops =====
            // For loop version
            int sumFor = SumEvenFor(1, 100);
            Console.WriteLine($"Sum of evens (for): {sumFor}");

            // While loop version
            int sumWhile = SumEvenWhile(1, 100);
            Console.WriteLine($"Sum of evens (while): {sumWhile}");

            // Foreach loop version (I build a list 1..100 and iterate it)
            int sumForeach = SumEvenForeach(BuildRange(1, 100));
            Console.WriteLine($"Sum of evens (foreach): {sumForeach}");

            // Mini note to myself: all three results should match

            // ===== Task 2: Grading with conditionals =====
            // I’m testing a few sample scores to verify both implementations return the same letter.
            int[] testScores = { 95, 83, 77, 62, 40, 100, 0 };

            Console.WriteLine("\nGrades using if/else:");
            foreach (int score in testScores)
            {
                Console.WriteLine($"Score {score} -> {GetLetterGradeIfElse(score)}");
            }

            Console.WriteLine("\nGrades using switch:");
            foreach (int score in testScores)
            {
                Console.WriteLine($"Score {score} -> {GetLetterGradeSwitch(score)}");
            }

            // ===== Task 3: Mini Challenge (print message if sum > 2000) =====
            Console.WriteLine("\nMini Challenge checks on sumFor:");

            // Version 1: if/else
            CheckBigNumberIfElse(sumFor);

            // Version 2: ternary operator
            CheckBigNumberTernary(sumFor);

            // I’m pausing so I can see the output when I run locally.
            // (Comment out if not needed.)
            // Console.ReadLine();
        }

        // ========== Helpers for Task 1 ==========

        // For loop: I just iterate from start to end and add only even numbers.
        static int SumEvenFor(int start, int end)
        {
            int total = 0;
            for (int i = start; i <= end; i++)
            {
                if (i % 2 == 0)
                {
                    total += i;
                }
            }
            return total;
        }

        // While loop: same logic but using while for practice.
        static int SumEvenWhile(int start, int end)
        {
            int total = 0;
            int i = start;
            while (i <= end)
            {
                if (i % 2 == 0)
                {
                    total += i;
                }
                i++;
            }
            return total;
        }

        // Foreach loop: I pass a list [start..end] and add even numbers.
        static int SumEvenForeach(List<int> numbers)
        {
            int total = 0;
            foreach (int n in numbers)
            {
                if (n % 2 == 0)
                {
                    total += n;
                }
            }
            return total;
        }

        // I’m building a simple list from start to end so I can use foreach cleanly.
        static List<int> BuildRange(int start, int end)
        {
            List<int> result = new List<int>();
            for (int i = start; i <= end; i++)
            {
                result.Add(i);
            }
            return result;
        }

        // ========== Task 2: Grading ==========

        // If/else version: I’m checking ranges in descending order.
        static string GetLetterGradeIfElse(int score)
        {
            // I’m assuming 0–100 inclusive. No validation here since lab didn’t require it.
            if (score >= 90) return "A";
            else if (score >= 80) return "B";
            else if (score >= 70) return "C";
            else if (score >= 60) return "D";
            else return "F";
        }

        // Switch version: I’m using pattern matching on ranges to keep it readable.
        static string GetLetterGradeSwitch(int score)
        {
            // Keeping it simple, no advanced features.
            switch (score)
            {
                case int s when s >= 90: return "A";
                case int s when s >= 80: return "B";
                case int s when s >= 70: return "C";
                case int s when s >= 60: return "D";
                default: return "F";
            }
        }

        // ========== Task 3: Mini Challenge checks ==========

        // If/else version: print the message if the sum is greater than 2000.
        static void CheckBigNumberIfElse(int sum)
        {
            if (sum > 2000)
            {
                Console.WriteLine("That’s a big number!");
            }
            else
            {
                Console.WriteLine("Sum is not over 2000.");
            }
        }

        // Ternary version: I print one of the two messages in a single line.
        static void CheckBigNumberTernary(int sum)
        {
            string message = (sum > 2000) ? "That’s a big number!" : "Sum is not over 2000.";
            Console.WriteLine(message);
        }
    }
}
