# Assignment 6: Game Matchmaking System - Implementation Notes

**Name:** Vitoria Resende

## Multi-Queue Pattern Understanding

**How the multi-queue pattern works for game matchmaking:**
Casual: Simple FIFO (first in, first out). First two players get matched.

Ranked: Only matches players if their skill difference is ±2 or less.

QuickPlay: First tries to match by skill like Ranked, but if more than 4 players are waiting, it matches any two to make the game start faster.

## Challenges and Solutions

**Biggest challenge faced:**
Making Ranked and QuickPlay match players correctly while still keeping queue order and not losing any players.

**How you solved it:**
I temporarily converted the queue to a list, found valid skill pairs, and then rebuilt the queue without those players. Testing with different skills helped confirm it worked.

**Most confusing concept:**
Making sure players aren’t in more than one queue at the same time and correctly removing them using RemoveFromAllQueues().

## Code Quality

**What you're most proud of in your implementation:**
The TryCreateMatch() method logic, especially in Ranked mode. It’s clean, works correctly, and keeps the queues organized.

**What you would improve if you had more time:**
Add a better console UI and more realistic estimated wait times instead of simple “short” or “long”.

## Testing Approach

**How you tested your implementation:**
I created players with different skills, added them to each queue, and tested if they matched the correct way depending on game mode.

**Test scenarios you used:**
2 players in Casual → instant match

Ranked players with skills 5 and 9 → no match

QuickPlay with 5+ players → still matches even with different skills

Empty or 1-player queues → returns null safely

## Game Mode Understanding

**Casual Mode matching strategy:**
Casual: FIFO, no skill checks.

Ranked: Skill difference must be ≤ 2.

QuickPlay: Try skill matching first, but if more than 4 players are waiting, match the first two quickly..

## Real-World Applications

This system feels similar to games like Overwatch or Fortnite:

Casual = fun, fast games.

Ranked = fair matches, tighter skill levels.

QuickPlay = “just start the game already” mode.

I realized real matchmaking is not random, it's a mix of fairness and speed.

## Stretch Features
I implemented the Advanced Analytics stretch goal:

Tracks peak queue sizes

Tracks average waiting time (seconds)
These show how efficient the matchmaking system works.

## Time Spent

**Total time:** [5:30 hours]

**Breakdown:**

- Understanding the assignment and queue concepts: [30 min]
- Implementing the 6 core methods: [2 hours]
- Testing different game modes and scenarios: [1 hours]
- Debugging and fixing issues: [1 hours]
- Writing these notes: [1 hours]

**Most time-consuming part:** Getting Ranked and QuickPlay matching to work correctly without breaking queues.

## Key Learning Outcomes

**Queue concepts learned:**
How to manage multiple queues in one system.

How to design matching algorithms using skill ratings.

How to update player stats after matches.

How wait times and fair matches improve player experience.