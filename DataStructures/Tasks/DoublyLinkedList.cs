using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Tasks.DoNotChange;

namespace Tasks
{
    public class DoublyLinkedList<T> : IDoublyLinkedList<T>
    {
        private Node<T> head;
        private int version = 0;
        private int count = 0;

        private DoublyLinkedListEnumerator<T> enumerator;

        public DoublyLinkedList()
        {
            enumerator = new DoublyLinkedListEnumerator<T>(this);
        }

        public int Length => count;

        public void Add(T data)
        {
            version++;

            var newNode = new Node<T>(data);

            if (head == null)
            {
                head = newNode;
                count++;
                return;
            }

            var last = GetLastNode();
            last.next = newNode;
            newNode.previous = last;
            count++;
        }

        private Node<T> GetLastNode()
        {
            var temp = head;
            while (temp.next != null)
            {
                temp = temp.next;
            }
            return temp;
        }

        public void AddAt(int index, T e)
        {
            version++;

            var node = head;

            if (node == null)
            {
                head = new Node<T>(e);
                count++;
                return;
            }

            for (int i = 0; i < index; i++)
            {
                if (node.next != null)
                {
                    node = node.next;
                }
                
            }
            var newNode = new Node<T>(e);

            if (index == count)
            {
                newNode.next = null;
                newNode.previous = node;

                node.next = newNode;
            }
            else
            {
                newNode.next = node;
                newNode.previous = node.previous;

                if (node != head)
                {
                    node.previous.next = newNode;
                    node.previous = newNode;
                }
                else
                {
                    head.previous = newNode;
                    head = newNode;
                }
            }

            count++;
        }

        public T ElementAt(int index)
        {
            if (index < 0 || index >= count)
            {
                throw new IndexOutOfRangeException(nameof(index));
            }

            var node = head;

            for (int i = 0; i < index; i++)
            {
                node = node.next;
            }

            return node.data;
        }

        public IEnumerator<T> GetEnumerator()
        {
            enumerator.Version = version;
            return enumerator;
        }

        public void Remove(T item)
        {
            version++;
            var temp = head;
            if (temp != null && temp.data.Equals(item))
            {
                head = temp.next;
                head.previous = null;
                count--;
                return;
            }
            while (temp != null && !temp.data.Equals(item))
            {
                temp = temp.next;
            }

            if (temp == null)
            {
                return;
            }

            if (temp.next != null)
            {
                temp.next.previous = temp.previous;
            }

            if (temp.previous != null)
            {
                temp.previous.next = temp.next;
            }

            count--;
        }

        public T RemoveAt(int index)
        {
            if (index < 0 || index >= count)
            {
                throw new IndexOutOfRangeException(nameof(index));
            }

            version++;

            var node = head;
            if (node != null && index == 0)
            {
                head = node.next;
                head.previous = null;
                count--;
                return node.data;
            }
            
            var indexer = 0;
            while (node != null && indexer++ != index)
            {
                node = node.next;
            }

            if (node.next != null)
            {
                node.next.previous = node.previous;
            }

            if (node.previous != null)
            {
                node.previous.next = node.next;
            }

            count--;
            return node.data;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public struct DoublyLinkedListEnumerator<TK> : IEnumerator<TK>
        {
            private readonly DoublyLinkedList<TK> list;
            private TK current;
            private Node<TK> currentNode;

            public DoublyLinkedListEnumerator(DoublyLinkedList<TK> list)
            {
                if (list is null)
                {
                    throw new ArgumentNullException(nameof(list));
                }

                this.list = list;
                Position = 0;
                Version = list.version;
                current = default;
                currentNode = default;
            }

            /// <summary>
            /// Gets current element.
            /// </summary>
            public TK Current
            {
                get => current;
            }

            /// <inheritdoc/>
            object IEnumerator.Current
            {
                get
                {
                    if (Position > list.Length)
                    {
                        throw new InvalidOperationException("Position is more than length");
                    }

                    return Current;
                }
            }

            /// <summary>
            /// Sets version of stack.
            /// </summary>
            internal int Version { private get; set; }

            /// <summary>
            /// Sets count of elements in stack.
            /// </summary>
            internal int Position { private get; set; }

            /// <inheritdoc/>
            public void Dispose()
            {
            }

            /// <inheritdoc/>
            public bool MoveNext()
            {
                if (Version != list.version)
                {
                    throw new InvalidOperationException("Versions are not the same");
                }

                if ((uint)Position++ < list.count)
                {
                    currentNode ??= list.head;
                    current = currentNode.data;
                    currentNode = currentNode.next;
                    
                    return true;
                }

                return MoveNextRare();
            }

            /// <inheritdoc/>
            public void Reset()
            {
                if (Version != list.version)
                {
                    throw new InvalidOperationException("Versions are not the same");
                }

                Position = list.Length - 1;
                current = default;
                currentNode = list.head;
            }

            private bool MoveNextRare()
            {
                Position = list.Length + 1;
                current = default;
                return false;
            }
        }
    }
}
