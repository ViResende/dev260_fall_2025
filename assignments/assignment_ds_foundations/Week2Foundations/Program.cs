using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

class Program
{
    // ========= Part 2: tiny demos =========

    static void RunArrayDemo()
    {
        Console.WriteLine("A) Array demo");
        int[] a = new int[10];
        a[0] = 7; a[2] = 42; a[9] = -5;

        // index access O(1)
        Console.WriteLine($"index 2 -> {a[2]}");

        // linear search O(n)
        int target = 42;
        bool found = false;
        for (int i = 0; i < a.Length; i++)
            if (a[i] == target) { found = true; break; }

        Console.WriteLine($"linear search for {target} -> {found}");
        Console.WriteLine();
    }

    static void RunListDemo()
    {
        Console.WriteLine("B) List<T> demo");
        var list = new List<int> { 1, 2, 3, 4, 5 };
        list.Insert(2, 99);    // [1,2,99,3,4,5]
        list.Remove(99);       // [1,2,3,4,5]
        Console.WriteLine($"final Count -> {list.Count}");
        Console.WriteLine();
    }

    static void RunStackDemo()
    {
        Console.WriteLine("C) Stack<T> demo");
        var history = new Stack<string>();
        history.Push("/home");
        history.Push("/products");
        history.Push("/cart");

        Console.WriteLine($"Peek -> {history.Peek()}"); // current page
        Console.Write("Pop order -> ");
        bool first = true;
        while (history.Count > 0)
        {
            if (!first) Console.Write(" -> ");
            Console.Write(history.Pop());
            first = false;
        }
        Console.WriteLine("\n");
    }

    static void RunQueueDemo()
    {
        Console.WriteLine("D) Queue<T> demo");
        var q = new Queue<string>();
        q.Enqueue("job-1");
        q.Enqueue("job-2");
        q.Enqueue("job-3");

        Console.WriteLine($"Peek -> {q.Peek()}");
        Console.Write("Dequeue order -> ");
        bool first = true;
        while (q.Count > 0)
        {
            if (!first) Console.Write(" -> ");
            Console.Write(q.Dequeue());
            first = false;
        }
        Console.WriteLine("\n");
    }

    static void RunDictionaryDemo()
    {
        Console.WriteLine("E) Dictionary<K,V> demo");
        var inv = new Dictionary<string, int>
        {
            ["SKU123"] = 10,
            ["SKU456"] = 5,
            ["SKU789"] = 0
        };

        inv["SKU456"] = 6; // update
        bool ok = inv.TryGetValue("missing", out int qty);
        Console.WriteLine($"TryGetValue(\"missing\") -> {ok} (qty unused)");
        Console.WriteLine();
    }

    static void RunHashSetDemo()
    {
        Console.WriteLine("F) HashSet<T> demo");
        var hs = new HashSet<int>();
        Console.WriteLine($"Add 3 -> {hs.Add(3)}");
        Console.WriteLine($"Add 3 again -> {hs.Add(3)}  (duplicate returns false)");
        hs.UnionWith(new[] { 3, 4, 5 });
        Console.WriteLine($"final Count -> {hs.Count}");
        Console.WriteLine();
    }

    // ========= Part 3: simple membership benchmark =========
    static double TimeMs(Action action, int runs = 5)
    {
        double best = double.MaxValue;
        // tiny warmup outside measurement
        action();
        for (int i = 0; i < runs; i++)
        {
            var sw = Stopwatch.StartNew();
            action();
            sw.Stop();
            best = Math.Min(best, sw.Elapsed.TotalMilliseconds);
        }
        return best;
    }

    static void RunBenchmarks()
    {
        Console.WriteLine("=== Membership Benchmarks (best of 5; lower is better) ===");
        int[] Ns = new[] { 1_000, 10_000, 100_000 }; // add 250_000 if your machine is fast

        foreach (int N in Ns)
        {
            // data 0..N-1
            var data = Enumerable.Range(0, N).ToArray();

            // build outside timers
            var list = new List<int>(data);
            var set = new HashSet<int>(data);
            var dict = data.ToDictionary(x => x, x => true);

            int present = N - 1;
            int missing = -1;

            double t1a = TimeMs(() => list.Contains(present));
            double t2a = TimeMs(() => set.Contains(present));
            double t3a = TimeMs(() => dict.ContainsKey(present));

            double t1b = TimeMs(() => list.Contains(missing));
            double t2b = TimeMs(() => set.Contains(missing));
            double t3b = TimeMs(() => dict.ContainsKey(missing));

            Console.WriteLine($"N={N}");
            Console.WriteLine($"List.Contains(N-1):   {t1a:F3} ms");
            Console.WriteLine($"HashSet.Contains:     {t2a:F3} ms");
            Console.WriteLine($"Dict.ContainsKey:     {t3a:F3} ms");
            Console.WriteLine($"List.Contains(-1):    {t1b:F3} ms");
            Console.WriteLine($"HashSet.Contains(-1): {t2b:F3} ms");
            Console.WriteLine($"Dict.ContainsKey(-1): {t3b:F3} ms");
            Console.WriteLine();
        }
    }

    // ========= Program entry =========
    static void Main()
    {
        // Part 2
        RunArrayDemo();
        RunListDemo();
        RunStackDemo();
        RunQueueDemo();
        RunDictionaryDemo();
        RunHashSetDemo();

        // Part 3
        RunBenchmarks();

        Console.WriteLine("Done.");
    }
}
