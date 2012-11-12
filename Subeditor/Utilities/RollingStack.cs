using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Subeditor.Utilities
{
    public class RollingStack<T> : IEnumerable<T>, ICollection
    {
        private readonly T[] data;
        private readonly int size;
        private int currentIndex;
        
        public RollingStack(int size)
        {
            this.data = new T[size];
            this.size = size;
            this.currentIndex = -1;
        }

        public int Count 
        {
            get 
            { 
                return currentIndex + 1; 
            }
        }

        public void Push(T element)
        {
            if (currentIndex + 1 < size)
            {
                currentIndex++;
            }
            else
            {
                RollBackByOne();
            }

            data[currentIndex] = element;

        }

        public T Peek()
        {
            if (currentIndex >= 0)
            {
                return data[currentIndex];
            }
            else
            {
                return default(T);
            }
        }

        public T Pop()
        {
            if (currentIndex >= 0)
            {
                T result = data[currentIndex];

                currentIndex--;

                return result;
            }
            else
            {
                return default(T);
            }
        }

        public void Clear()
        {
            currentIndex = -1;
        }

        public T[] ToArray()
        {
            T[] result = new T[currentIndex + 1];
            for (int i = currentIndex, j = 0; i >= 0; --i, ++j)
            {
                result[j] = data[i];
            }

            return result;
        }

        private void RollBackByOne()
        {
            if (size <= 1)
            {
                return;
            }

            for (int i = 1; i < size; ++i)
            {
                data[i - 1] = data[i];
            }
        }

        #region IEnumerable<T>

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = currentIndex; i >= 0; --i)
            {
                yield return data[i];
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion //IEnumerable<T>

        #region ICollection

        public void CopyTo(Array array, int index)
        {
            data.CopyTo(array, index);
        }

        public bool IsSynchronized
        {
            get { return false; }
        }

        public object SyncRoot
        {
            get { return data.SyncRoot; }
        }

        #endregion //ICollection
    }
}
