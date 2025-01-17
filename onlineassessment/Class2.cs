﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onlineassessment
{
    class MaxHeap<T>
    {
        IComparer<T> _comparer;
        private int Capacity = 10;
        public int size = 0;
        T[] items;
        public MaxHeap(IComparer<T> comparer)
        {
            items = new T[Capacity];
            _comparer = comparer;
        }
        int getleftChildIndex(int parentIndex) { return (parentIndex * 2) + 1; }
        int getrightChildIndex(int parentIndex) { return (parentIndex * 2) + 2; }
        public int getParentindex(int childIndex) { return (childIndex - 1) / 2; }

        bool hasleftChild(int index) { return getleftChildIndex(index) < size; }
        bool hasrightChild(int index) { return getrightChildIndex(index) < size; }
        bool hasParent(int index) { return getParentindex(index) >= 0; }

        T leftChild(int index) { return items[getleftChildIndex(index)]; }
        T rightChild(int index) { return items[getrightChildIndex(index)]; }
        T Parent(int index) { return items[getParentindex(index)]; }

        void swap(int firstIndex, int secondIndex)
        {
            T temp = items[firstIndex];
            items[firstIndex] = items[secondIndex];
            items[secondIndex] = temp;
        }
        void ensureExtraCapacity()
        {
            if (size >= Capacity)
            {
                Array.Resize(ref items, 2 * Capacity);
                Capacity *= 2;
            }
        }
        public T Peek()
        {
            if (size == 0) throw new Exception();
            return items[0];
        }
        public T Poll()
        {
            if (size == 0) throw new Exception();
            T val = items[0];
            items[0] = items[size - 1];
            size--;
            heapifydown();
            return val;
        }
        public void add(T item)
        {
            ensureExtraCapacity();
            items[size] = item;
            size++;
            heapifyup();
        }
        public void heapifydown()
        {
            int index = 0;
            while (hasleftChild(index))
            {
                int ChildIndex = getleftChildIndex(index);
                if (hasrightChild(index) && _comparer.Compare(rightChild(index), leftChild(index)) == 1)
                {
                    ChildIndex = getrightChildIndex(index);
                }
                if (_comparer.Compare(items[index], items[ChildIndex]) == -1)
                {
                    swap(index, ChildIndex);
                    index = ChildIndex;
                }
                else
                    break;
            }
        }
        public void heapifyup()
        {
            int index = size - 1;
            while (hasParent(index) && _comparer.Compare(Parent(index), items[index]) == -1)
            {
                int ParentIndex = getParentindex(index);
                swap(ParentIndex, index);
                index = ParentIndex;
            }
        }
    }
    class CompareTuple : IComparer<Tuple<int, int>>
    {
        public int Compare(Tuple<int, int> x, Tuple<int, int> y)
        {
            return x.Item1.CompareTo(y.Item1);
        }
    }
    class Class2
    {
        public static int selectStock(int saving, List<int> currentValue, List<int> futureValue)
        {
            List<int> profit = new List<int>();
            CompareTuple cmp = new CompareTuple();
            int maxprofit = 0;
            MaxHeap<Tuple<int, int>> mxheap = new MaxHeap<Tuple<int, int>>(cmp);
            for (int i = 0; i < currentValue.Count; i++)
            {
                if (futureValue[i] - currentValue[i] > 0)
                {
                    mxheap.add(new Tuple<int, int>(futureValue[i] - currentValue[i], currentValue[i]));
                }
            }
            if (mxheap.size == 0) return 0;
            while (mxheap.size != 0&&saving>0)
            {
                if((saving- mxheap.Peek().Item2)>=0)
                {
                    saving -= mxheap.Peek().Item2;
                    maxprofit += mxheap.Peek().Item1;
                }
                mxheap.Poll();
            }
            return maxprofit;
        }
    }
}
