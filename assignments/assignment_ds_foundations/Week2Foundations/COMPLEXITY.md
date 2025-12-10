# Week 2 — Complexity Predictions (Average Case)

| Structure        | Operation                    | Big-O (Avg) | Explanation |
|------------------|------------------------------|-------------|-------------|
| Array            | Access by index              | O(1)        | You can jump straight to the element using its index, so time stays constant. |
| Array            | Search (unsorted)            | O(n)        | Each item must be checked one by one until a match is found. |
| List<T>          | Add at end                   | O(1)*       | Usually constant time; resizing happens occasionally but rarely. |
| List<T>          | Insert at index              | O(n)        | Elements after that index must shift over, increasing cost with size. |
| Stack<T>         | Push / Pop / Peek            | O(1)        | Always handles the top element, making operations instant. |
| Queue<T>         | Enqueue / Dequeue / Peek     | O(1)        | Works at the front and back only, so time doesn’t depend on length. |
| Dictionary<K,V>  | Add / Lookup / Remove        | O(1)        | Uses hashing to locate entries directly in constant average time. |
| HashSet<T>       | Add / Contains / Remove      | O(1)        | Hash-based lookup means performance doesn’t change much as it grows. |

*amortized

