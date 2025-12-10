# Study Notes – Assignment 3: Doubly Linked Lists

## Step-by-Step Progress

- **Step 3:** I started with the add methods (`AddFirst`, `AddLast`, `Insert`). I had to test a few times because the order wasn’t showing right at first, but after fixing the links, it worked.  
- **Step 4:** I created the forward and backward display methods. This part helped me see how nodes connect in both directions.  
- **Step 5:** Then I added the search methods (`Contains`, `Find`, `IndexOf`). It was fun testing them with values that didn’t exist in the list to see if they returned false.  
- **Step 6:** I worked on the remove methods. At first, the program crashed when I removed the first or last element, but I added checks for empty lists and it stopped breaking.  
- **Step 7:** I added `Reverse` and `Clear`. Reversing the list was confusing at first, but once I switched the next and previous pointers correctly, it worked fine.  
- **Part B:** I created the `Song` and `MusicPlaylist` classes using the doubly linked list. I tested adding, removing, and skipping songs. The navigation worked after a few adjustments.
- Note: I took schreenshots for each part. 
---

## Problems and Fixes

- I had a **hard time running the project** at the beginning because some methods were not implemented yet, and the program threw exceptions. Once I finished the missing steps, it ran without errors.  
- I also had trouble **displaying the list** because my loop didn’t move to the next node, so it froze. I fixed it by adding `current = current.Next` inside the loop.  
- When I tried to **remove songs**, the current song pointer didn’t change, which caused errors. I fixed it by setting it to the next or previous node after removing one.  
- Sometimes my **count value** didn’t update, so I made sure to increase or decrease it after every add or remove operation.

---

## What I Learned

- A doubly linked list is more flexible than a regular list because it lets you go both forward and backward.  
- It’s better for situations where you need to **insert or delete elements often**, especially in the middle.  
- Arrays are faster for random access, but linked lists are better for dynamic data.  
- I learned how important it is to handle **null pointers** carefully to avoid errors.  
- I also realized how small mistakes in node linking can break the entire structure.

---

## Reflection

This project helped me really understand how data moves through a linked list.  
At first, it was frustrating because the code didn’t run right away, but testing each step separately made it easier to debug.  
It was satisfying to see everything working together, especially when the playlist started showing songs and navigation worked smoothly.

