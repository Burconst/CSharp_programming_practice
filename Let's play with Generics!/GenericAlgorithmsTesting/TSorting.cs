using NUnit.Framework;
using GenericAlgorithms;
using System.Collections.Generic;

namespace GenericAlgorithmsTesting
{
    public class Tests
    {
        
        List<(int[] Unsorted,int[] Sorted)> intArrayes = new List<(int[] Unsorted, int[] Sorted)>();

        [SetUp]
        public void Setup()
        {
            intArrayes.AddRange(new []
                    {
                    (new []{ 1, 2, 3 }, new []{ 1, 2, 3 }),
                    (new []{ 321, 234, 534, 11, 0, -12, 999, 2 }, new []{ -12, 0, 2, 11, 234, 321, 534, 999 }),
                    (new []{ -123, 33, 0, 0, 0, 33 }, new []{  -123, 0, 0, 0, 33, 33 })
                    }
                ); 

        }

        [Test]
        public void Sorting_BubbleSort_AreEqual()
        {
            foreach (var arrPair in intArrayes)
            {
                Assert.AreEqual(arrPair.Sorted, Sorting.BubbleSort(arrPair.Unsorted));   
            }
        }

        [Test]
        public void Sorting_QuickSort_AreEqual() 
        {
            foreach (var arrPair in intArrayes)
            {
                Assert.AreEqual(arrPair.Sorted, Sorting.QuickSort(arrPair.Unsorted));   
            }
        }

        [Test]
        public void Sorting_HeapSort_AreEqual()
        {
            foreach (var arrPair in intArrayes)
            {
                Assert.AreEqual(arrPair.Sorted, Sorting.HeapSort(arrPair.Unsorted));   
            }
        }

    }
}