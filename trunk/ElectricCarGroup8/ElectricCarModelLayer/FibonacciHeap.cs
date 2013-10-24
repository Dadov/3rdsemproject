using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarModelLayer
{
    public class FibonacciHeap
    {
        private FibonacciNode minNode;
        public DoubleLinkedList root { get; set; }
        

        public int numberOfNodes { get; set; }
        public List<int> stationIDs { get; set; }

        public FibonacciHeap()
        {
            numberOfNodes = 0;
            root = new DoubleLinkedList();
        }

        
        public FibonacciNode insert(int id)
        {
            FibonacciNode node = new FibonacciNode() { StationID = id };
            if (minNode == null || node.MinPathValue < minNode.MinPathValue)
            {
                minNode = node;
                root.Add(node);
            }
            stationIDs.Add(id);
            numberOfNodes++;
            return node;
        }

        public FibonacciNode extractMinNode()
        {
            FibonacciNode extractNode = minNode;
            if (extractNode != null)
            {
                for (FibonacciNode node = extractNode.Child; node.RightNode != extractNode.Child.RightNode; node = node.RightNode)
                {
                    root.Add(node);
                    node.Parent = null;
                }
                root.Delete(extractNode);
                if (extractNode == extractNode.RightNode)
                {
                    //if extractNode is the only node in the root
                    root = null;
                }
                else
                {
                    minNode = extractNode.RightNode; //not necessary a minimum node
                    consolidateTree(root);
                }
                numberOfNodes--;
            }
            return extractNode;
        }

        public void consolidateTree(DoubleLinkedList root)
        {
            FibonacciNode[] A = new FibonacciNode[calculateArraySize(numberOfNodes)];
            for (int i = 0; i < A.Length; i++)
            {
                A[i] = null;
            }
            for (FibonacciNode w = root.head; w.RightNode != root.head.Child.RightNode; w = w.RightNode)
            {
                FibonacciNode x = w;
                int d = x.Degree;
                while (A[d] != null)
                {
                    FibonacciNode y = A[d];
                    if (x.MinPathValue > y.MinPathValue)
                    {
                        A[d] = x;
                    }
                    Link(root, y, x);
                    A[d] = null;
                    d += 1;
                }
                A[d] = x;
            }
            minNode = null; 
            root = new DoubleLinkedList();
            for (int j = 0; j < A.Length; j++)
            {
                if (A[j] != null)
                {
                    root.Add(A[j]);
                    if (minNode == null || A[j].MinPathValue < minNode.MinPathValue)
                    {
                        minNode = A[j];
                    }
                }
            }
        }

        //remove node y from root list, and link node y to x 
        private void Link(DoubleLinkedList root, FibonacciNode y, FibonacciNode x)
        {
            root.Delete(y);
            //add y as a child of x
            if (x.Child == null)
            {
                y.Parent = x;
            }
            else
            {
                FibonacciNode lastNode = x.Child.LeftNode;
                lastNode.RightNode = y;
                y.RightNode = x.Child;
                y.LeftNode = lastNode;
                x.Child.LeftNode = y;
            }
            x.Degree++;
            y.Mark = false;
        }

        public void DecreasingKey(DoubleLinkedList list, FibonacciNode x, decimal k)
        {
            if (x.MinPathValue < k)
            {
                throw new SystemException("The new node min value is greater than current node value.");
            }
            x.MinPathValue = k;
            FibonacciNode y = x.Parent;
            if (y != null && x.MinPathValue < y.MinPathValue)
	        {
		        Cut(list, x, y);
                CascadingCut(list, y);
	        }
            if (x.MinPathValue < minNode.MinPathValue)
	        {
		        minNode = x;
	        }
        }

        private void Cut(DoubleLinkedList list, FibonacciNode x, FibonacciNode y)
        {
            //remove x from y child least
            FibonacciNode leftNode = x.LeftNode;
            FibonacciNode rightNode = x.RightNode;
            leftNode.RightNode = rightNode;
            rightNode.LeftNode = leftNode;

            //add x to the list
            FibonacciNode lastNode = list.head.LeftNode;
            lastNode.RightNode = x;
            x.LeftNode = lastNode;
            x.RightNode = list.head;
            list.head.LeftNode = x;

            x.Parent = null;
            x.Mark = false;
        }

        private void CascadingCut(DoubleLinkedList list, FibonacciNode y)
        {
            FibonacciNode z = y.Parent;
            if (z != null)
            {
                if (y.Mark == false)
                {
                    y.Mark = true;
                }
                else
                {
                    Cut(list, y, z);
                    CascadingCut(list, z);
                }
            }
        }

        private int calculateArraySize(int n)
        {
            int s;
            long result = 0;
            for (s= 0; s < n; s++)
            {
                result = result + (long)Math.Pow(2, s);
                if (result >= n)
                    break;
            }
            return s;
        }
        
    }
}
