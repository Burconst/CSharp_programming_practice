using System;
using System.IO;
using System.Diagnostics;
using System.Numerics;
using System.Text;
using System.Collections.Generic;
using GenericAlgorithms;

namespace GetStats
{
    class Program
    {
        static string generateNumber(uint digitsCount) 
        {
            if(digitsCount == 0) return "";
            Random random = new Random(); 
            StringBuilder numberBuilder = new StringBuilder();
            numberBuilder.Append(random.Next(1,9).ToString());
            for(uint i = 0; i < digitsCount-1;i++) 
            {
              numberBuilder.Append(random.Next(0,9).ToString());
            }
            return numberBuilder.ToString();
        }

        static T[] generateNumbersArray<T>(uint numbersCount, uint digitsCount, Func<string, T> converter)
        {
            var numbers = new List<T>();
            for(uint i = 0; i < numbersCount; i++)
            {
                numbers.Add(converter(generateNumber(digitsCount)));
            }
            return numbers.ToArray();
        }

        static void collectSortingStats<T>(int iterations, (uint numbersCount,uint digitsCount)[] arrayInfo, Func<string, T> converter, string outputFileName) where T : IComparable
        {
            foreach(var info in arrayInfo) 
            {
                Console.WriteLine("("+ info.numbersCount+","+ info.digitsCount+")");
                var numbers = generateNumbersArray<T>(info.numbersCount, info.digitsCount, converter);
                var stopwatch = new Stopwatch();

                Console.Write("--%");
                using(StreamWriter sw = new StreamWriter(outputFileName+"("+ info.numbersCount+","+ info.digitsCount+")")) 
                {
                    sw.WriteLine("BubbleSort"+","+"HeapSort"+","+"QuickSort");
                    for(int i = 0; i < iterations; i++) 
                    {
                        sw.WriteLine(
                            getMethodStats(numbers, x => Sorting.BubbleSort(x), stopwatch)+","
                            //getMethodStats(numbers, x => Sorting.HeapSort(x), stopwatch)+","
                            //getMethodStats(numbers, x => Sorting.QuickSort(x), stopwatch)
                        );
                        if(((int)100*(i+1)/iterations) < 10)
                        {
                            Console.Write("\b\b\b"+" "+((int)100*(i+1)/iterations)+"%");
                            continue;
                        }
                        Console.Write("\b\b\b"+((int)100*(i+1)/iterations)+"%");
                    }
                }
                Console.Write("\n");
            }
        }

        static string getMethodStats<T>(T[] numbers, Func<T[],T[]> sortingMethod, Stopwatch stopwatch) 
        {
            stopwatch.Reset();
            stopwatch.Start();
            sortingMethod(numbers);
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds.ToString();
        }


        static void collectStats(int iterations, uint numbersCount) 
        {
            Console.WriteLine("int");
            collectSortingStats<int>(iterations, new (uint,uint)[]{(numbersCount,9)}, x => Convert.ToInt32(x), "intResult");
            
            Console.WriteLine("long");
            collectSortingStats<long>(iterations, new (uint,uint)[]{(numbersCount,18)}, x => Convert.ToInt64(x), "longResult");

            Console.WriteLine("BigInteger");
            collectSortingStats<BigInteger>(iterations, new (uint,uint)[]{(numbersCount,9),(numbersCount,18), (numbersCount,27), (numbersCount, 36)},
                                              x => BigInteger.Parse(x), "BigIntegerResult");
            
            Console.WriteLine("string");
            collectSortingStats<string>(iterations, new (uint,uint)[]{(numbersCount,9),(numbersCount,18), (numbersCount,27), (numbersCount, 36)},
                                          x => x, "stringResult");
        }

        static void Main(string[] args)
        {
            collectStats(10,10000);
        }
    }
}
