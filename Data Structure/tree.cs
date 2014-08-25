using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tree
{
    public class Node
    {
        public int data;
        public Node left;
        public Node right;
        public Node parent;
    }

    public class tree
    {
        public Node root;

        // insert node in BST
        public void insert(Node root, int node)
        {
            if (root == null)
            {
                Node newNode = new Node();
                newNode.data = node;
                root = newNode;
            }
            if (node < root.data)
            {
                insert(root.left, node);
            }
            else if (node > root.data)
            {
                insert(root.right, node);
            }
        }

        // Insert node into tree Iterative
        public void insertItr(Node root, int data)
        {
            Node newNode = new Node();
            newNode.data = data;
            if (root == null)
            {
                root.data = data;
            }
            else
            {
                Node current = root;
                while (true)
                {
                    if (data < current.data)
                    {
                        current = current.left;
                        if (current == null)
                        {
                            current.left = newNode;
                            return;
                        }
                    }
                    else
                    {
                        current = current.right;
                        if (current == null)
                        {
                            current.right = newNode;
                            return;
                        }
                    }
                }
            }
        }

        // recursive search in BST
        public Node search(Node root, int data)
        {
            if (root == null)
                return null;
            if (root.data == data)
                return root;
            if (data < root.data)
                search(root.left, data);
            else if (data > root.data)
                search(root.right, data);
            return null;
        }

        // DFS : Implement a depth first search algorithm recursively.
        private bool DFS(Node root, int target)
        {
            if (root == null)
                return false;
            if (root.data == target)
                return true;
            return DFS(root.left, target) || DFS(root.right, target);
        }

        // BFS : Implement a breadth first search algorithm.
        // Level order traversal
        private bool BFS(Node root, int target)
        {
            if (root == null)
                return false;
            Queue<Node> _queue = new Queue<Node>();
            Node tmp = null;
            _queue.Enqueue(root);

            while (_queue.Count() > 0)
            {
                tmp = _queue.Dequeue();
                if (tmp.data == target)
                    return true;
                if (tmp.left != null)
                    _queue.Enqueue(tmp.left);
                if (tmp.right != null)
                    _queue.Enqueue(tmp.right);
            }
            return false;
        }

        // Inorder traversal of BST
        public void inOrder(Node root)
        {
            if (root != null)
            {
                inOrder(root.left);
                Console.Write(root.data);
                inOrder(root.right);
            }
        }

        // Preorder traversal of BST
        public void preOrder(Node root)
        {
            if (root != null)
            {
                Console.Write(root.data);
                preOrder(root.left);
                preOrder(root.right);
            }
        }

        // An iterative process to print preorder traversal of Binary tree
        void iterativePreorder(Node root)
        {
            // Base Case
            if (root == null)
                return;
            Stack<Node> nodeStack = new Stack<Node>();
            nodeStack.Push(root);

            /* Pop all items one by one. Do following for every popped item
               a) print it
               b) push its right child
               c) push its left child
            Note that right child is pushed first so that left is processed first */
            while (nodeStack.Count > 0)
            {
                // Pop the top item from stack and print it
                Node node = nodeStack.Peek();
                Console.Write(node.data);
                nodeStack.Pop();
                if (node.right != null)
                    nodeStack.Push(node.right);
                if (node.left != null)
                    nodeStack.Push(node.left);
            }
        }

        // Postorder traversal of BST
        public void postOrder(Node root)
        {
            if (root != null)
            {
                postOrder(root.left);
                postOrder(root.right);
                Console.Write(root.data);
            }
        }

        // An iterative function to do postorder traversal of a given binary tree
        void postOrderIterative(Node root)
        {
            if (root == null)
                return;

            Stack<Node> _stack = new Stack<Node>();
            _stack.Push(root);
            Node prev, curr;
            prev = null;

            while (_stack != null)
            {
                curr = _stack.Peek();
                if (prev == null || prev.left == curr || prev.right == curr) //top to bottom
                {
                    if (curr.left != null)
                        _stack.Push(curr.left);
                    else if (curr.right != null)
                        _stack.Push(curr.right);
                    else
                    {
                        Console.Write(curr.data);
                        _stack.Pop();
                    }
                }
                else if (curr.left == prev) //bottom up
                {
                    if (curr.right != null)
                    {
                        _stack.Push(curr.right);
                    }
                    else
                    {
                        Console.Write(curr.data);
                        _stack.Pop();
                    }
                }
                else if (curr.right == prev)
                {
                    Console.Write(curr.data);
                    _stack.Pop();
                }
                prev = curr;
            }
        }

        // Delete node from tree
        public void delete(int key)
        {
            Node parent = null;

            Node nodetoDelete = FindParent(key, ref parent);

            if (nodetoDelete.left == null && nodetoDelete.right == null)
            {
                if (parent != null)
                {
                    if (parent.left == nodetoDelete)
                        parent.left = null;
                    else
                        parent.right = null;
                }
                else
                    root = null;
            }
            else if (nodetoDelete.left == null)
            {
                if (parent != null)
                {
                    if (parent.left == nodetoDelete)
                    {
                        parent.left = nodetoDelete.right;
                    }
                    else
                    {
                        parent.right = nodetoDelete.right;
                    }
                }
                else
                    root = nodetoDelete.right;
            }
            else if (nodetoDelete.right == null)
            {
                if (parent != null)
                {
                    if (parent.left == nodetoDelete)
                    {
                        parent.left = nodetoDelete.left;
                    }
                    else
                    {
                        parent.right = nodetoDelete.left;
                    }
                }
                else
                    root = nodetoDelete.left;
            }
            else
            {
                Node successor = FindSuccessor(nodetoDelete, ref parent);
                parent.left = successor.right;
                nodetoDelete = successor;
            }

        }

        private Node FindParent(int key, ref Node Parent)
        {
            Node node = root;
            while (node != null)
            {
                if (node.data == key)
                    return node;
                if (key < node.data)
                {
                    Parent = node;
                    node = node.left;
                }
                else
                {
                    Parent = node;
                    node = node.right;
                }
            }
            return null;
        }

        private Node FindSuccessor(Node startNode, ref Node parent)
        {
            parent = startNode;
            startNode = startNode.right;
            while (startNode.left != null)
            {
                parent = startNode;
                startNode = startNode.right;
            }
            return startNode;
        }

        //In a binary search tree, find the lowest common ancestor.
        private Node FindLCA(Node root, Node one, Node two)
        {
            while (root != null)
            {
                if (root.data < one.data && root.data < two.data)
                    return root.right;
                else if (root.data > one.data && root.data > two.data)
                    return root.left;
                else
                    return root;
            }
            return null;
        }

        // Find LCA of Binary Tree. 
        //The run time complexity is O(h), where h is the tree’s height. The space complexity is also O(h)
        public Node FindLCA_BTree(Node root, Node one, Node two)
        {
            HashSet<Node> hash = new HashSet<Node>();
            while (one != null || two != null)
            {
                if (one != null)
                {
                    if (hash.Contains(one))
                        return one;
                    else
                        hash.Add(one);
                    one = one.parent;
                }
                if (two != null)
                {
                    if (hash.Contains(two))
                        return two;
                    else
                        hash.Add(two);
                    two = two.parent;
                }
            }
            return null;
        }

        // Find LCA best way no Space Required
        public Node FindLCA_Best(Node root, Node one, Node two)
        {
            int h1 = getHeight(one);
            int h2 = getHeight(two);
            //swap elements
            if (h1 > h2)
            {
                //swapping h1 and h2 using XOR gate
                h1 ^= h2;
                h2 ^= h1;
                h1 ^= h2;
                swap(one.data, two.data);
            }
            int dh = h2 - h1;
            for (int i = 0; i < dh; i++)
            {
                two = two.parent;
            }
            while (one != null && two != null)
            {
                if (one.data == two.data)
                    return one;
                one = one.parent;
                two = two.parent;
            }
            return null;
        }

        public int getHeight(Node node)
        {
            int height = 0;
            while (node != null)
            {
                height++;
                node = node.parent;
            }
            return height;
        }

        // Find the Diameter of Binary Tree
        public int diameterOfBinaryTree(Node node)
        {
            if (node == null)
                return 0;

            int leftHeight = heightOfBinaryTree(node.left);
            int rightHeight = heightOfBinaryTree(node.right);

            int leftDiameter = diameterOfBinaryTree(node.left);
            int rightDiameter = diameterOfBinaryTree(node.right);

            return Math.Max(leftHeight + rightHeight + 1, Math.Max(leftDiameter, rightDiameter));
        }
        public int heightOfBinaryTree(Node node)
        {
            if (node == null)
                return 0;
            else
            {
                return 1 + Math.Max(heightOfBinaryTree(node.left), heightOfBinaryTree(node.right));
            }
        }

        // Convert sorted array into balanced tree
        private Node sortedArraytoBST(int[] arr, int start, int end)
        {
            if (end > start)
                return null;
            int mid = (start + end) / 2;
            Node tree = new Node();
            tree.data = arr[mid];
            tree.left = sortedArraytoBST(arr, start, mid - 1);
            tree.right = sortedArraytoBST(arr, mid + 1, end);

            return tree;
        }

        // Find out if given tree is Binary Search Tree or not
        private bool isBST(Node root)
        {
            // do inorder traversal and check it is sorted or not
            Node prev = null;
            if (root != null)
            {
                if (!isBST(root.left))
                    return false;
                if (prev != null && root.data <= prev.data)
                    return false;
                prev = root;

                return isBST(root.right);
            }
            return true;
        }


        // Find the Nth smallest element from the BST

        // Fidn the Nth smallest element from the BST

        Node temp;
        public Node getNthMin(int target)
        {
            if (target == 1)
                return getMin();
            else
            {
                int counter = 1;
                temp = getMin();
                while (true)
                {
                    temp = temp.parent;
                    counter++;
                    if (counter == target)
                        return temp;
                    if (temp.right != null)
                        counter++;
                    if (counter == target)
                        return temp.right;
                }
            }
        }
        public Node getMin()
        {
            temp = root;
            while (true)
            {
                if (temp.left == null)
                    return temp;
                else
                    temp = root.left;
            }
        }

        /* A O(n) iterative program for construction of BST from preorder traversal */
        public Node ConstuctTreeFromPreOrder(int[] preOrder)
        {
            Node root = new Node();
            root.data = preOrder[0];
            Stack<Node> _stack = new Stack<Node>();
            Node temp;
            for (int i = 1; i < preOrder.Length; i++)
            {
                temp = null;
                while (_stack.Count > 0 && preOrder[i] > _stack.Peek().data)
                {
                    temp = _stack.Pop();
                }

                if (temp != null)
                {
                    temp.right.data = preOrder[i];
                    _stack.Push(temp.right);
                }
                else
                {
                    temp.left.data = preOrder[i];
                    _stack.Push(temp.left);
                }
            }
            return root;
        }

        //Two nodes of a BST are swapped, correct the BST
        public void CorrectBST(Node root)
        {
            Node first = null, middle = null, last = null, prev = null;
            CorrectBSTUtil(root, first, middle, last, prev);
            if (first != null && last != null)
                swap(first.data, last.data);
            else if (first != null && middle != null)
                swap(first.data, middle.data);
        }
        public void CorrectBSTUtil(Node root, Node first, Node middle, Node last, Node prev)
        {
            if (root != null)
            {
                CorrectBSTUtil(root.left, first, middle, last, prev);
                if (prev != null && root.data < prev.data)
                {
                    if (first == null)
                    {
                        first = prev;
                        middle = root;
                    }
                    else
                    {
                        last = root;
                    }
                }
                prev = root;
                CorrectBSTUtil(root.right, first, middle, last, prev);
            }
        }
        public void swap(int a, int b)
        {
            int t = a;
            a = b;
            b = t;
        }

        //Binary Tree Maximum Path Sum
        public int maxPathSum(Node root)
        {
            if (root == null)
            {
                return 0;
            }
            int maxLeft = maxPathSum(root.left); //max length in the whole left subtree
            int maxRight = maxPathSum(root.right); //max length in the whole right subtree
            int leftLen = 0; //max length in the left subtree starting with left child
            int rightLen = 0; //max length in the left subtree starting with left child
            if (root.left != null)
            {
                leftLen = Math.Max(root.left.data, 0);
            }
            if (root.right != null)
            {
                rightLen = Math.Max(root.right.data, 0);
            }
            int maxLength = root.data;
            if (leftLen > 0)
            {
                maxLength += leftLen;
            }
            if (rightLen > 0)
            {
                maxLength += rightLen;
            }
            if (root.left != null)
            {
                maxLength = Math.Max(maxLeft, maxLength);
            }
            if (root.right != null)
            {
                maxLength = Math.Max(maxRight, maxLength);
            }
            //root.val is replaced with the maximum length starting from root downwards
            root.data = Math.Max(leftLen, rightLen) + root.data;
            return maxLength;
        }

        //Given a binary tree, every node has a int value, return the root node of subtree with the largest sum up value. 
        private Node maxSumSubtree(Node root)
        {
            if (root == null) return null;
            
            int maxsum = 0;
            Node res = null;
            helper(root, res, maxsum);
            return res;
        }
        int helper(Node p, Node res, int maxsum)
        {
            if (p == null) return 0;
            int lsum = helper(p.left, res, maxsum);
            int rsum = helper(p.right, res, maxsum);
            int total = lsum + rsum + p.data;
            if (total > maxsum)
            {
                maxsum = total;
                res = p;
            }
            return total;
        }

        //Find the maximum sum of the subtree (triangle) from the given tree.
        private int FindMaxSumSubtree(Node root)
        {            
            int max_sum = 0;
            max_sum = MaxSumSubtree(root, max_sum);
            return max_sum;
        }
        private int MaxSumSubtree(Node root, int max_sum)
        {
            int sum = 0;
            int lsum = 0;
            int rsum = 0;
            if (root == null)
                return 0;
            if (root.left != null)
                lsum = MaxSumSubtree(root.left, max_sum);
            if (root.right != null)
                rsum = MaxSumSubtree(root.right, max_sum);
            sum = root.data + lsum + rsum;
            if (max_sum < sum)
                max_sum = sum;
            return max_sum;
        }

        //Print Right View of a Binary Tree
        public void rightView()
        {
            int max_level = 0;
            rightViewUtil(root, 1, max_level);
        }
        public void rightViewUtil(Node root, int level, int max_level)
        {
            if (root == null) return;
            if (max_level < level)
            {
                Console.WriteLine(root.data);
                max_level = level;
            }
            rightViewUtil(root.right, level + 1, max_level);
            rightViewUtil(root.left, level + 1, max_level);
        }
    }
}
