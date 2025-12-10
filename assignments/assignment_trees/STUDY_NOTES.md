Assignment 9: BST File System Navigator - Implementation Notes

Name: Vitoria Caroline

Binary Search Tree Pattern Understanding

How BST operations work for file system navigation:
In this assignment I really learned how a BST keeps everything organized for me without needing extra sorting. Because every insert goes left or right based on the name, the tree basically sorts files and directories automatically. That makes searching way faster, since instead of checking everything one by one, we just follow the left/right path. Using in-order traversal also makes it super easy to list things in alphabetical order. It honestly feels like how real file systems keep folders organized in a structured way instead of a messy list.

Challenges and Solutions

Biggest challenge faced:
The hardest part for me was the delete operation. It has three cases and I kept mixing up the “two children” logic. Understanding how to replace the node with the inorder successor took me a bit of time.

How I solved it:
I looked up examples online and reviewed the Week 9 slides again. Then I drew the tree by hand and practiced deleting nodes from a small BST. After that, the logic finally clicked and I was able to rewrite the delete method correctly.

Most confusing concept:
Recursive thinking still confuses me sometimes. It’s weird seeing a function call itself over and over, especially with multiple branches. But after testing it, stepping through with breakpoints, and printing out values, I started seeing how the tree “walks” itself automatically.

Code Quality

What I'm most proud of:
I’m proud of how clean and readable I made my recursive methods. I kept them simple and clear, and I didn’t overcomplicate anything. I also liked my pattern-matching extra feature, it was small but fun to implement.

What I would improve if I had more time:
I would improve error handling so the system shows better messages when the user types something wrong. I would also try to add more advanced search features or maybe cache some results for speed.

Real-World Applications

How this relates to actual file systems:
Doing this assignment helped me understand how Windows File Explorer and macOS Finder organize files behind the scenes. Real file systems are way more complex, but the idea of using trees for quick lookups and sorted order is the same. Even databases use tree structures (like B-Trees) for indexing and fast searches.

What I learned about tree algorithms:
I learned that tree algorithms depend heavily on understanding “left vs right” movement and trusting recursion to do the rest. I also learned that hierarchical data makes way more sense to store in a tree than in a list.

Stretch Features

I added a simple file pattern-matching feature using * and ? wildcards. It lets the user search for things like "*.txt" or "file?.cs", which feels close to how real search tools work.

Time Spent

Total time: 4 hours

Breakdown:

Understanding BST concepts and assignment requirements: 1 hour

Implementing the 8 core TODO methods: 1.5 hours

Testing with different file scenarios: 0.5 hour

Debugging recursive algorithms and BST operations: 0.5 hour

Writing these notes: 0.5 hour

Most time-consuming part:
The deletion case with two children, because making sure the tree stayed correct after removing a node required careful thinking.
