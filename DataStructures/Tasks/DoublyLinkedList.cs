using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using Tasks.DoNotChange;

namespace Tasks
{
    public class DoublyLinkedList<T> : IDoublyLinkedList<T>
    {
        private T[] arrayData;
        private int indexer;
        private int version = 0;
        private DoublyLinkedListEnumerator<T> enumerator;

        public DoublyLinkedList()
        {
            arrayData = null;
            enumerator = new DoublyLinkedListEnumerator<T>(this);
        }

        public DoublyLinkedList(int capacity)
        {
            if (capacity == 0)
            {
                arrayData = null;
            }
            else
            {
                arrayData = new T[capacity];
            }

            enumerator = new DoublyLinkedListEnumerator<T>(this);
        }

        public int Length => indexer;

        public void Add(T e)
        {
            version++;
            if (arrayData is null)
            {
                T[] newArray = new T[1];
                arrayData = newArray;
            }
            else if (indexer == arrayData.Length)
            {
                T[] newArray = new T[2 * arrayData.Length];
                Array.Copy(arrayData, 0, newArray, 0, indexer);
                arrayData = newArray;
            }

            arrayData[indexer] = e;
            indexer++;
        }

        public void AddAt(int index, T e)
        {
            version++;
            if (arrayData is null)
            {
                T[] newArray = new T[1];
                arrayData = newArray;
            }

            if (index == 0)
            {
                T[] newArray = new T[arrayData.Length];
                if (indexer == arrayData.Length)
                {
                    newArray = new T[2 * arrayData.Length];
                }

                newArray[0] = e;
                Array.Copy(arrayData, 0, newArray, 1, indexer);
                arrayData = newArray;
            }
            else if (indexer == arrayData.Length)
            {
                T[] newArray = new T[2 * arrayData.Length];
                var count = indexer - index + 1;
                Array.Copy(arrayData, 0, newArray, 0, count);
                newArray[index] = e;
                Array.Copy(arrayData, indexer - index, newArray, 0, indexer - count);
                arrayData = newArray;
            }

            indexer++;
        }

        public T ElementAt(int index)
        {
            if (arrayData is null || index < 0 || index > indexer)
            {
                throw new IndexOutOfRangeException(nameof(index));
            }

            return arrayData[index];
        }

        public IEnumerator<T> GetEnumerator()
        {
            enumerator.Version = version;
            return enumerator;

        }

        public void Remove(T item)
        {
            var arrayLength = arrayData.Length;
            var isDeleted = false;
            bool predicate(T element)
            {
                if (!element.Equals(item))
                {
                    return true;
                }
                else if (isDeleted)
                {
                    return true;
                }
                else
                {
                    isDeleted = true;
                    return false;
                }
            }
            arrayData = arrayData.Where(e => predicate(e)).ToArray();
            if (arrayData.Length != arrayLength)
            {
                indexer--;
            }
        }

        public T RemoveAt(int index)
        {
            if (arrayData is null || index < 0 || index > indexer)
            {
                throw new IndexOutOfRangeException(nameof(index));
            }

            version++;

            var elementToRemove = arrayData[index];

            T[] dest = new T[arrayData.Length - 1];
            if (index > 0)
            {
                Array.Copy(arrayData, 0, dest, 0, index);
            }

            if (index < arrayData.Length - 1)
            {
                Array.Copy(arrayData, index + 1, dest, index, arrayData.Length - index - 1);
            }

            arrayData = dest;
            indexer--;

            return elementToRemove;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public T[] ToArray()
        {
            return (T[])arrayData.Clone();
        }

        public struct DoublyLinkedListEnumerator<TK> : IEnumerator<TK>
        {
            private readonly DoublyLinkedList<TK> list;
            private TK current;

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
                TK[] localList = list.arrayData;

                if (Version != list.version)
                {
                    throw new InvalidOperationException("Versions are not the same");
                }

                if ((uint)Position < list.Length)
                {
                    current = localList[Position++];
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
