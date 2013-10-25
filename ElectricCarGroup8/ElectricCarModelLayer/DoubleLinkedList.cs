using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarModelLayer
{
    public class DoubleLinkedList
    {
        public FibonacciNode head { get; set; }
        public int Size { get; set; }
        
        public void Add(FibonacciNode node)
        {
            
            if (head == null)
            {
                head = node;
            }
            else
            {
                FibonacciNode lastNode = head.LeftNode;
                lastNode.RightNode = node;
                node.RightNode = head;
                node.LeftNode = lastNode;
                head.LeftNode = node;
            }
            Size++;
        }

        public void Delete(FibonacciNode node)
        {
            if (node == head)
            {
                head = node.RightNode;
            }
            FibonacciNode leftNode = node.LeftNode;
            FibonacciNode rightNode = node.RightNode;
            leftNode.RightNode = rightNode;
            rightNode.LeftNode = leftNode;

            Size--;
        }

       
    }
}
