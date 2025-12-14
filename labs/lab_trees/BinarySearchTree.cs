using System;
using System.Collections.Generic;
using System.Linq;

// ============================================
// 📚 QUICK REFERENCE GUIDE
// ============================================

/*
🌳 BINARY SEARCH TREE CHEAT SHEET:

BST Property:
- Left subtree values < Root value < Right subtree values
- This property enables O(log n) search performance!

Tree Construction:
var bst = new BinarySearchTree();
bst.Insert(50);  // Root node
bst.Insert(30);  // Goes left (30 < 50)
bst.Insert(70);  // Goes right (70 > 50)

Tree Search - O(log n):
bool found = bst.Search(30);  // Navigate tree efficiently

Tree Traversal:
bst.InOrderTraversal();  // Outputs: sorted sequence automatically!
// Left -> Root -> Right = 20, 30, 40, 50, 60, 70, 80

Tree Analysis:
int minimum = bst.FindMinimum();    // Leftmost node
int maximum = bst.FindMaximum();    // Rightmost node
int count = bst.Count();            // Total nodes

🚀 WHY BINARY SEARCH TREES ROCK:
- O(log n) search vs O(n) linear search
- Automatic sorting through in-order traversal
- Perfect for dynamic data with frequent searches
- Natural hierarchical organization
- Foundation for advanced tree structures

🌐 REAL-WORLD USES:
- Database indexing systems
- File system organization
- Expression parsing in compilers
- Game engine spatial partitioning
- Symbol tables in programming languages
- Priority queues and scheduling systems
*/

namespace Lab9_BST
{
    public class EmployeeManagementSystem
    {
        private TreeNode? root;
        private int totalOperations = 0;
        private DateTime systemStartTime = DateTime.Now;

        public EmployeeManagementSystem()
        {
            root = null;
            Console.WriteLine("🏢 Employee Management System Initialized!");
            Console.WriteLine("📊 System ready for BST operations.\n");
        }

        // ============================================
        // 🚀 YOUR MISSION: IMPLEMENT THESE METHODS
        // ============================================

        public void Insert(Employee employee)
        {
            totalOperations++;

            // TODO: Implement this method
            root = InsertRecursive(root, employee);
        }

        public Employee? Search(int employeeId)
        {
            totalOperations++;

            // TODO: Implement this method
            return SearchRecursive(root, employeeId);
        }

        public void InOrderTraversal()
        {
            totalOperations++;
            Console.WriteLine("👥 Employee Directory (sorted by ID):");

            if (root == null)
            {
                Console.WriteLine("   (No employees in system)");
                return;
            }

            // TODO: Implement this method
            InOrderRecursive(root);
        }

        public Employee? FindMinimum()
        {
            totalOperations++;

            // TODO: Implement this method
            if (root == null) return null;
            var current = root;
            while (current.Left != null)
            {
                current = current.Left;
            }
            return current.Employee;
        }

        public Employee? FindMaximum()
        {
            totalOperations++;

            // TODO: Implement this method
            if (root == null) return null;
            var current = root;
            while (current.Right != null)
            {
                current = current.Right;
            }
            return current.Employee;
        }

        public int Count()
        {
            totalOperations++;

            // TODO: Implement this method
            return CountRecursive(root);
        }

        // ============================================
        // 🔧 HELPER METHODS FOR TODO IMPLEMENTATION
        // ============================================

        private TreeNode? InsertRecursive(TreeNode? node, Employee employee)
        {
            // TODO: Implement recursive insertion logic
            if (node == null)
                return new TreeNode(employee);

            if (employee.EmployeeId < node.Employee.EmployeeId)
                node.Left = InsertRecursive(node.Left, employee);
            else if (employee.EmployeeId > node.Employee.EmployeeId)
                node.Right = InsertRecursive(node.Right, employee);

            return node;
        }

        private Employee? SearchRecursive(TreeNode? node, int employeeId)
        {
            // TODO: Implement recursive search logic
            if (node == null) return null;
            if (employeeId == node.Employee.EmployeeId) return node.Employee;
            if (employeeId < node.Employee.EmployeeId)
                return SearchRecursive(node.Left, employeeId);
            return SearchRecursive(node.Right, employeeId);
        }

        private void InOrderRecursive(TreeNode? node)
        {
            // TODO: Implement recursive in-order traversal
            if (node == null) return;
            InOrderRecursive(node.Left);
            Console.WriteLine($"ID: {node.Employee.EmployeeId}  Name: {node.Employee.Name}  Dept: {node.Employee.Department}");
            InOrderRecursive(node.Right);
        }

        private int CountRecursive(TreeNode? node)
        {
            // TODO: Implement recursive counting
            if (node == null) return 0;
            return 1 + CountRecursive(node.Left) + CountRecursive(node.Right);
        }

        // ============================================
        // 🎯 UTILITY METHODS (PROVIDED)
        // ============================================

        public bool IsEmpty()
        {
            return root == null;
        }

        public void DisplayTree()
        {
            Console.WriteLine("🌳 Tree Structur Visualization:");

            if (root == null)
            {
                Console.WriteLine("   (Empty tree)");
                return;
            }

            Console.WriteLine("\n📊 Enhanced Tree Structure:");
            DisplayTreeEnhanced(root, "", true, true);

            Console.WriteLine("\n🎯 Level-by-Level View:");
            DisplayTreeByLevels();
        }

        private void DisplayTreeEnhanced(TreeNode? node, string prefix, bool isLast, bool isRoot)
        {
            if (node == null) return;

            string connector = isRoot ? "🌟 " : (isLast ? "└── " : "├── ");
            string nodeInfo = $"ID:{node.Employee.EmployeeId} ({node.Employee.Name})";

            Console.WriteLine(prefix + connector + nodeInfo);

            string childPrefix = prefix + (isRoot ? "" : (isLast ? "    " : "│   "));

            bool hasLeft = node.Left != null;
            bool hasRight = node.Right != null;

            if (hasRight)
            {
                Console.WriteLine(childPrefix + "│");
                Console.WriteLine(childPrefix + "├─(R)─┐");
                DisplayTreeEnhanced(node.Right, childPrefix + "│     ", !hasLeft, false);
            }

            if (hasLeft)
            {
                Console.WriteLine(childPrefix + "│");
                Console.WriteLine(childPrefix + "└─(L)─┐");
                DisplayTreeEnhanced(node.Left, childPrefix + "      ", true, false);
            }
        }

        private void DisplayTreeByLevels()
        {
            if (root == null) return;

            var queue = new Queue<(TreeNode?, int)>();
            queue.Enqueue((root, 0));
            int currentLevel = -1;

            while (queue.Count > 0)
            {
                var (node, level) = queue.Dequeue();

                if (level > currentLevel)
                {
                    if (currentLevel >= 0) Console.WriteLine();
                    Console.Write($"Level {level}: ");
                    currentLevel = level;
                }

                if (node != null)
                {
                    Console.Write($"[{node.Employee.EmployeeId}] ");
                    queue.Enqueue((node.Left, level + 1));
                    queue.Enqueue((node.Right, level + 1));
                }
                else
                {
                    Console.Write("[null] ");
                }
            }
            Console.WriteLine();
        }

        public SystemStats GetSystemStats()
        {
            var uptime = DateTime.Now - systemStartTime;

            return new SystemStats
            {
                TotalOperations = totalOperations,
                Uptime = uptime,
                EmployeeCount = IsEmpty() ? 0 : Count(),
                TreeHeight = CalculateHeight(root),
                IsEmpty = IsEmpty()
            };
        }

        private int CalculateHeight(TreeNode? node)
        {
            if (node == null) return 0;
            return 1 + Math.Max(CalculateHeight(node.Left), CalculateHeight(node.Right));
        }

        public void RunInteractiveMenu()
        {
            LabSupport.RunInteractiveMenu(this);
        }
    }
}
