using NUnit.Framework;
using GenericAlgorithms;
using System.Collections.Generic;
using System.Numerics;

namespace GenericAlgorithmsTesting
{
    public class Tests
    {
        
        List<(int[] Unsorted,int[] Sorted)> intArrayes = new List<(int[] Unsorted, int[] Sorted)>();
        List<(BigInteger[] Unsorted,BigInteger[] Sorted)> bigIntegerArrayes = new List<(BigInteger[] Unsorted, BigInteger[] Sorted)>();

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
            bigIntegerArrayes.AddRange(new []
                    {
                    (new []{ BigInteger.Parse("412341234909342340"),
                             BigInteger.Parse("897987423480899321048921"),
                             BigInteger.Parse("7979792348209348"),
                             BigInteger.Parse("2350239840128301298402348"),
                             BigInteger.Parse("4432424566434244634"),
                             BigInteger.Parse("4432424566434244634")
                         },
                     new []{ BigInteger.Parse("7979792348209348"),
                             BigInteger.Parse("412341234909342340"),
                             BigInteger.Parse("4432424566434244634"),
                             BigInteger.Parse("4432424566434244634"),
                             BigInteger.Parse("897987423480899321048921"),
                             BigInteger.Parse("2350239840128301298402348")
                         })
                    }
                ); 
        }

        [Test]
        public void Sorting_BubbleSort_AreEqual()
        {
            foreach (var arrayes in intArrayes)
            {
                Assert.AreEqual(arrayes.Sorted, Sorting.BubbleSort(arrayes.Unsorted));   
            }
            foreach (var arrayes in bigIntegerArrayes)
            {
                Assert.AreEqual(arrayes.Sorted, Sorting.BubbleSort(arrayes.Unsorted));   
            }
        }

        [Test]
        public void Sorting_QuickSort_AreEqual() 
        {
            foreach (var arrayes in intArrayes)
            {
                Assert.AreEqual(arrayes.Sorted, Sorting.QuickSort(arrayes.Unsorted));   
            }
            foreach (var arrayes in intArrayes)
            {
                Assert.AreEqual(arrayes.Sorted, Sorting.QuickSort(arrayes.Unsorted));   
            }
        }

        [Test]
        public void Sorting_HeapSort_AreEqual()
        {
            foreach (var arrayes in intArrayes)
            {
                Assert.AreEqual(arrayes.Sorted, Sorting.HeapSort(arrayes.Unsorted));   
            }
            foreach (var arrayes in intArrayes)
            {
                Assert.AreEqual(arrayes.Sorted, Sorting.HeapSort(arrayes.Unsorted));   
            }
        }

    }
}