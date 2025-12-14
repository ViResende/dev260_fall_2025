Assignment 10: Flight Route Network Navigator – Implementation Notes

Name: Vitoria Resende

Graph Data Structure Understanding
How adjacency list representation works for flight networks

In this project, I learned that a graph with flights works best using an adjacency list.
We store it as:

Dictionary<string, List<Flight>>


This means:

I can find an airport instantly because dictionary lookups are basically O(1).

Each airport only stores the flights it actually has, so it’s good for sparse graphs.

Our CSV only has 16 airports and 52 flights, so storing everything in a big matrix would waste space.

An adjacency list feels more natural because airports → list of outgoing flights is exactly how real airlines work.

So overall, adjacency list made the whole graph lighter, faster, and easier to work with.

Difference between BFS and Dijkstra

I used BFS when I needed the shortest path by number of stops (fewest hops).
I used Dijkstra when I needed the cheapest path by cost.

BFS is simple: use a queue, visit each airport level by level, and it always finds the fastest route in terms of hops.

Dijkstra adds cost math. It uses a priority queue so cheaper paths are expanded first.

Both guarantee the best answer, but the “best” is different depending if we mean cheapest or fewest stops.

Challenges and Solutions
Biggest challenge faced

The hardest part was definitely Dijkstra’s algorithm, especially keeping track of the parent map and understanding when to update the distances.
Also, path reconstruction confused me at first.

How I solved it

I drew a few airports and flights on paper to visualize what was going on.
Then I added breakpoints in Visual Studio and followed the route step by step.
After that, the logic finally made sense and everything clicked.

Most confusing concept

The parent dictionary used to rebuild the final route was the trickiest.
It took me a bit to understand that the algorithm stores where each airport came from, so later I can walk backwards to form the full path.

Algorithm Implementation Details
BFS Implementation

For BFS (FindRoute and FindShortestRoute):

I used a queue to explore airports level-by-level

I tracked visited airports with a HashSet

I used a parent map to rebuild the path

BFS finds the “shortest” route automatically because each layer means one hop

It was nice to see how simple but powerful BFS is.

Dijkstra’s Implementation

For FindCheapestRoute:

I created a PriorityQueue<string, decimal>

I stored the cheapest known cost to reach each airport

If a new path was cheaper, I updated it

I saved the parent so I could rebuild the final path

It felt like a more advanced version of BFS but focused on cost instead of stops.

Path Reconstruction Logic

I started from the destination, walked backwards through the parent map until I reached the origin, and then reversed the list.
At first I kept getting empty paths, but after debugging I realized I wasn’t saving the parent correctly.

Code Quality
What I'm most proud of

I’m proud that my BFS and Dijkstra both work correctly and are clean to read.
I also kept the structure close to what the assignment required.

What I would improve with more time

I would love to:

add better error messages

maybe add a small visualizer

also improve the optional DFS criteria search if I had more hours

But for the assignment, I focused on the core requirements.

Real-World Applications
How this relates to actual routing systems

This is basically how real systems like Google Flights or airline apps work behind the scenes.
They use graphs to figure out the best or cheapest route.
Same idea for Google Maps, Uber routes, or even networking on the internet.

What I learned about graph algorithms

I learned how powerful graphs are.
BFS is great for simple shortest paths, and Dijkstra is perfect when things have weights like cost or time.
I finally understood why adjacency lists matter for efficiency.

Testing and Verification
Test cases I created

I tested:

SEA → JFK

SEA → SFO → LAX

LAS → MIA

Origin = Destination

Isolated airports

Cheapest vs shortest routes to compare the difference

Interesting findings

Sometimes the cheapest route was not the one with the fewest stops.
Also, some airports had way more connections than I expected (real hubs), like DEN and DFW.

Optional Challenge

Not implemented – I focused on core requirements.

Time Spent

Total time: 5 hours

Breakdown:

Understanding graph concepts + reading requirements: 1 hour

Implementing basic operations: 1 hour

BFS shortest route: 45 minutes

Dijkstra cheapest route: 1 hour

Network analysis methods: 30 minutes

Testing with CSV: 30 minutes

Debugging & fixing parent map logic: 15 minutes

Writing these notes: 15 minutes

Most time-consuming part:
Understanding and debugging Dijkstra’s algorithm.

Key Takeaways
Most important lesson learned

Graph problems look scary at first, but once you understand BFS and Dijkstra, you realize that these algorithms show up everywhere in tech.

How this changed my understanding of data structures

Before, I mostly thought about lists, arrays, and trees.
Now I understand graphs are like the “super version” of relationships between things.
They are flexible and match real-life systems much better.
