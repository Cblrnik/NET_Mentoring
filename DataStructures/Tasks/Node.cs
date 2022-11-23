using System;
using System.Collections.Generic;
using System.Text;

namespace Tasks
{
    public class Node<TN>
    {
        public TN data;
        public Node<TN> next;
        public Node<TN> previous;
        public Node(TN d)
        {
            data = d;
            next = null;
        }
    }
}
