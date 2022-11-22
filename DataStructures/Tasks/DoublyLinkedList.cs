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
            this.enumerator = new DoublyLinkedListEnumerator<T>(this);
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

            this.enumerator = new DoublyLinkedListEnumerator<T>(this);
        }

        public int Length => this.indexer;

        public void Add(T e)
        {
            this.version++;
            if (this.arrayData is null)
            {
                T[] newArray = new T[1];
                this.arrayData = newArray;
            }
            else if (this.indexer == this.arrayData.Length)
            {
                T[] newArray = new T[2 * this.arrayData.Length];
                Array.Copy(this.arrayData, 0, newArray, 0, this.indexer);
                this.arrayData = newArray;
            }

            this.arrayData[this.indexer] = e;
            this.indexer++;
        }

        public void AddAt(int index, T e)
        {
            this.version++;
            if (this.arrayData is null)
            {
                T[] newArray = new T[1];
                this.arrayData = newArray;
            }
            else if (index == 0)
            {
                T[] newArray = new T[this.arrayData.Length];
                if (this.indexer == this.arrayData.Length)
                {
                    newArray = new T[2 * this.arrayData.Length];
                }

                newArray[0] = e;
                Array.Copy(this.arrayData, 0, newArray, 1, this.indexer);
                this.arrayData = newArray;
            }
            else if (this.indexer == this.arrayData.Length)
            {
                T[] newArray = new T[2 * this.arrayData.Length];
                var count = this.indexer - index + 1;
                Array.Copy(this.arrayData, 0, newArray, 0, count);
                newArray[index] = e;
                Array.Copy(this.arrayData, this.indexer - index, newArray, 0, this.indexer - count);
                this.arrayData = newArray;
            }

            this.indexer++;
        }

        public T ElementAt(int index)
        {
            if (arrayData is null || index < 0 || index > indexer)
            {
                throw new IndexOutOfRangeException();
            }

            return this.arrayData[index];
        }

        public IEnumerator<T> GetEnumerator()
        {
            this.enumerator.Version = this.version;
            return this.enumerator;

        }

        public void Remove(T item)
        {
            var arrayLength = arrayData.Length;
            var isDeleted = false;
            Func<T, bool> predicate = (T element) =>
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
            };
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
                throw new IndexOutOfRangeException();
            }

            version++;

            var elementToRemove = this.arrayData[index];

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
            return this.GetEnumerator();
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
                this.Position = 0;
                this.Version = list.version;
                this.current = default;
            }

            /// <summary>
            /// Gets current element.
            /// </summary>
            public TK Current
            {
                get => this.current;
            }

            /// <inheritdoc/>
            object IEnumerator.Current
            {
                get
                {
                    if (this.Position > this.list.Length)
                    {
                        throw new InvalidOperationException();
                    }

                    return this.Current;
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
                TK[] localList = this.list.arrayData;

                if (this.Version != this.list.version)
                {
                    throw new InvalidOperationException();
                }

                if ((uint)this.Position < this.list.Length)
                {
                    this.current = localList[this.Position++];
                    return true;
                }

                return this.MoveNextRare();
            }

            /// <inheritdoc/>
            public void Reset()
            {
                if (this.Version != this.list.version)
                {
                    throw new InvalidOperationException();
                }

                this.Position = this.list.Length - 1;
                this.current = default;
            }

            private bool MoveNextRare()
            {
                this.Position = this.list.Length + 1;
                this.current = default;
                return false;
            }
        }

    }
}
