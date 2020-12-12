using System;
using System.Collections.Generic;

namespace GenericAlgorithms
{
    public static class Sorting 
    { 
        static void swap<T>(ref T x, ref T y)
        {
            var t = x;
            x = y;
            y = t;
        }

        public static T[] BubbleSort<T>(T[] collection) where T : IComparable
        {
            for (int i = 0; i < collection.Length; i++)
            {
                for (int j = i+1; j < collection.Length; j++)
                {
                    if (collection[i].CompareTo(collection[j]) > 0)
                    {
                        swap(ref collection[i],ref collection[j]);
                    }
                }
            }
            return collection; 
        }

        #region QuickSort

        public static T[] QuickSort<T>(T[] collection) where T : IComparable
        {
            return QuickSort(collection, 0, collection.Length - 1);
        } 

        static int Partition<T>(T[] collection, int minIndex, int maxIndex) where T : IComparable
        {
            var pivot = minIndex - 1;
            for (var i = minIndex; i < maxIndex; i++)
            {
                if (collection[maxIndex].CompareTo(collection[i]) > 0)
                {
                    pivot++;
                    swap(ref collection[pivot], ref collection[i]);
                }
            }

            pivot++;
            swap(ref collection[pivot], ref collection[maxIndex]);
            return pivot;
        }

        static T[] QuickSort<T>(T[] collection, int minIndex, int maxIndex) where T : IComparable
        {
            if (minIndex >= maxIndex)
            {
                return collection;
            }

            var pivotIndex = Partition(collection, minIndex, maxIndex);
            QuickSort(collection, minIndex, pivotIndex - 1);
            QuickSort(collection, pivotIndex + 1, maxIndex);

            return collection;
        }
 
        #endregion


        #region  HeapSort
        public static T[] HeapSort<T>(T[] collection) where T : IComparable
        {
            var length = collection.Length;
            for (int i = length / 2 - 1; i >= 0; i--)
            {
                Heapify(collection, length, i);
            }
            for (int i = length - 1; i >= 0; i--)
            {
                swap(ref collection[0], ref collection[i]);
                Heapify(collection, i, 0);
            }

            return collection;
        }

        static void Heapify<T>(T[] collection, int length, int i) where T : IComparable
        {
            int largest = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;
            if (left < length && collection[left].CompareTo(collection[largest]) > 0)
            {
                largest = left;
            }
            if (right < length && collection[right].CompareTo(collection[largest]) > 0)
            {
                largest = right;
            }
            if (largest != i)
            {
                swap(ref collection[i], ref collection[largest]);
                Heapify(collection, length, largest);
            }
        }

    #endregion

    }
}