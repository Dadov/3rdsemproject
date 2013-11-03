using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarModelLayer
{
    public class FibonacciHeap
    {
        public FibonacciNode minNode { get; set; }
        public DoubleLinkedList root { get; set; }


        public int numberOfNodes { get; set; }
        public List<int> stationIDs { get; set; }

        public FibonacciHeap()
        {
            minNode = null;
            numberOfNodes = 0;
            root = new DoubleLinkedList();
            stationIDs = new List<int>();
        }


        public FibonacciNode insert(int id)
        {
            FibonacciNode node = new FibonacciNode() { StationID = id };
            if (minNode == null || node.MinPathValue < minNode.MinPathValue)
            {
                minNode = node;
            }
            root.Add(node);
            stationIDs.Add(id);
            numberOfNodes++;
            return node;
        }

        public FibonacciNode extractMinNode()
        {
            FibonacciNode extractNode = minNode;
            if (extractNode != null)
            {
                if (extractNode.Child != null)
                {
                    //TODO could be a bug when tranversing child list
                    FibonacciNode node = extractNode.Child;
                    for (int i = 0; i < extractNode.Degree; i++)
                    {
                        FibonacciNode nextNode = node.RightNode; //store current node's next node before the next node change to root list node
                        root.Add(node);
                        node.Parent = null;
                        node = nextNode;
                    }


                }
                root.Delete(extractNode);
                numberOfNodes--;
                if (extractNode == extractNode.RightNode)
                {
                    //if extractNode is the only node in the root
                    root = new DoubleLinkedList();
                    minNode = null;
                }
                else
                {
                    minNode = extractNode.RightNode; //not necessary a minimum node
                    consolidateTree(root);
                }

            }
            return extractNode;
        }

        public void consolidateTree(DoubleLinkedList root)
        {
            FibonacciNode[] A = new FibonacciNode[calculateArraySize(numberOfNodes)];
            //FibonacciNode[] A = new FibonacciNode[1000000];
            for (int i = 0; i < A.Length; i++)
            {
                A[i] = null;
            }

            FibonacciNode w = root.head; //pointer for tranverse nodes

            int size = root.Size;
            for (int i = 0; i < size; i++)
            {
                FibonacciNode x = w;

                int d = x.Degree;
                while (A[d] != null)
                {
                    FibonacciNode y = A[d];
                    
                    if (x.MinPathValue > y.MinPathValue)
                    {
                        //exchange x and y
                        FibonacciNode c = x;
                        x = y;
                        y = c;
                        //w = w.LeftNode;//set the pointer back to the node on the left
                    }
                    if ( w==y)
                    {
                        w = w.LeftNode;
                    }
                    Link(root, y, x); //x (w) is to be deleted in root list
                    //w = x; //TODO could be bug 


                    A[d] = null;
                    d += 1;
                }
                A[d] = x;
                w = w.RightNode; //pointer continue move to next node 
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
        public void Link(DoubleLinkedList root, FibonacciNode y, FibonacciNode x)
        {
            root.Delete(y);
            //add y as a child of x
            if (x.Child == null)
            {
                y.Parent = x;
                x.Child = y;
                y.RightNode = y;
                y.LeftNode = y;
            }
            else
            {
                FibonacciNode lastNode = x.Child.LeftNode;
                lastNode.RightNode = y;
                y.RightNode = x.Child;
                y.LeftNode = lastNode;
                x.Child.LeftNode = y;

                y.Parent = x;
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

        public void Cut(DoubleLinkedList list, FibonacciNode x, FibonacciNode y)
        {
            //remove x from y child least
            if (x == x.LeftNode)
            {
                y.Child = null;
            }
            else
            {

                if (x == y.Child)
                {
                    y.Child = x.RightNode;
                }

                FibonacciNode leftNode = x.LeftNode;
                FibonacciNode rightNode = x.RightNode;
                leftNode.RightNode = rightNode;
                rightNode.LeftNode = leftNode;
                
            }
            y.Degree--;

            //add x to the list
            list.Add(x);
            //FibonacciNode lastNode = list.head.LeftNode;
            //lastNode.RightNode = x;
            //x.LeftNode = lastNode;
            //x.RightNode = list.head;
            //list.head.LeftNode = x;
            //list.Size++;

            x.Parent = null;
            x.Mark = false;
        }

        public void CascadingCut(DoubleLinkedList list, FibonacciNode y)
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

        public int calculateArraySize(int n)
        {
            int s;
            //long result = 0;
            //for (s = 0; s < n; s++)
            //{
            //    result = result + (long)Math.Pow(2, s);
            //    if (result >= n)
            //        break;
            //}

            double result = Math.Log(n) / Math.Log((1 + Math.Sqrt(5)) / 2);
            s = (int)Math.Floor(result);
            return s + 1;
        }

    }
}
