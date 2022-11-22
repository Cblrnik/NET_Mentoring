using System;
using Tasks.DoNotChange;

namespace Tasks
{
    public class HybridFlowProcessor<T> : IHybridFlowProcessor<T>
    {
        private readonly DoublyLinkedList<T> list;
        public HybridFlowProcessor()
        {
            list = new DoublyLinkedList<T>();
        }

        public T Dequeue()
        {
            try
            {
                return list.RemoveAt(0);
            }
            catch (IndexOutOfRangeException e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }

        public void Enqueue(T item)
        {
            list.AddAt(list.Length, item);
        }

        public T Pop()
        {
            try
            {
                return list.RemoveAt(list.Length - 1);
            }
            catch (IndexOutOfRangeException e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }

        public void Push(T item)
        {
            list.Add(item);
        }
    }
}
