# Assignment 5: Browser Navigation System - Implementation Notes

**Name:** Vitoria Resende  

## Dual-Stack Pattern Understanding

**How the dual-stack pattern works for browser navigation:**  
The dual-stack idea makes it possible to move backward and forward between pages without losing where you’ve been.  
One stack keeps track of the pages I already visited (**back stack**), and the other remembers pages that I can move forward to (**forward stack**).  
Whenever I visit something new, I add the current page to the back stack and clear the forward one.  
This keeps the navigation clean, so it behaves like a real browser where new visits erase any “future” history.  
It’s basically like having two undo/redo buttons working together.

## Challenges and Solutions

**Biggest challenge faced:**  
The most difficult part was not mixing up the direction of the stacks.  
Sometimes I accidentally pushed the wrong page onto the wrong stack, which flipped the navigation order.  

**How you solved it:**  
I added console prints inside each method to show what was being pushed or popped.  
Seeing how the stacks changed after each action helped me fix the logic and confirm which one was being used for back or forward.  

**Most confusing concept:**  
Understanding stack enumeration — when printing history, the top item shows first because of LIFO order.  
At first I tried reversing it manually but realized it already displays in the right sequence automatically.

## Code Quality

**What you're most proud of in your implementation:**  
I’m proud that the program feels interactive and easy to follow when running in the console.  
The messages are clear, and I added simple formatting to make it feel like an actual browser interface.  
I also liked that I could reuse ideas from Lab 5 but still make the code look cleaner and more organized.

**What you would improve if you had more time:**  
I’d probably make it more realistic by showing the full browsing path (like “Back → Forward → Current”).  
Another idea would be saving the session to a file so it could resume later, or showing timestamps for each visited page.

## Testing Approach

**How you tested your implementation:**  
I tested by visiting several websites in order, going back and forward multiple times, and then clearing everything.  
I also tried edge cases like trying to go back when there’s no history or visiting a page twice in a row.  
I made sure every method gave a friendly message instead of crashing when something wasn’t possible.

**Issues you discovered during testing:**  
At one point, the back history wasn’t showing in the right order because I forgot that Stack already lists items from newest to oldest.  
After removing an unnecessary reverse step, the output looked correct.

## Stretch Features
I didn’t include extra credit, but I thought about adding a history size limit or showing the most recent 5 pages only.

## Time Spent

**Total time:** Around 3.5 hours  

**Breakdown:**
- Reviewing the starter code and understanding TODOs: 45 minutes  
- Writing and testing methods: 2 hours  
- Debugging and improving messages: 30 minutes  
- Reflection writing: 15 minutes  

**Most time-consuming part:**  
Getting the display methods to show clean output and make the navigation messages easy to read.
