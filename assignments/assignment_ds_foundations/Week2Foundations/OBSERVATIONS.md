I ran in Release (best of 5). For small N a lot of times rounded to 0,000 ms because the operations were extremely fast, but the growth pattern is clear: List.Contains gets slower as N increases (0,001 → 0,025 ms), while HashSet.Contains and Dict.ContainsKey stayed basically constant (~0,000 ms). This matches the O(n) vs O(1) predictions. For many membership checks on large data, I’d pick HashSet (or Dictionary if I need values).

The List.Contains got slower as the number of items grew, while HashSet and Dictionary stayed almost the same speed.

For smaller sets, even the List felt fast because there isn’t much data to loop through.

The numbers were close for HashSet and Dictionary since both use hashing.

Surprises:
I noticed that sometimes the first test was slower, probably because of warm-up time. After a few runs, everything got more stable.

My choice for big data:
If I need to check membership many times, I’d definitely pick a HashSet (or Dictionary if I also need values). It’s the fastest and scales better than List.