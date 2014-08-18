using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tree
{
    public class Queue
    {
        

    }

    //Implement the Queue using two Stack
    public class Queue<E>
    {        
        private Stack<E> Inbox = new Stack<E>();
        private Stack<E> Outbox = new Stack<E>();

        public void EnQueue(E item)
        {
            Inbox.Push(item);
        }
        public E Dequeue()
        {
            if (Outbox.Count == 0)
            {
                while (Inbox.Count != 0)
                {
                    Outbox.Push(Inbox.Pop());
                }
            }
            return Outbox.Pop();
        }
    }
}
