using System;
using System.Threading;
using System.Diagnostics;

namespace Program
{
    internal abstract class Program
    {
        public static void Main()
        {
            Console.WriteLine("Сортування змiшуванням:");
            CompareSortingMethods(StandardShakerSort, StudentShakerSort);
            
            Console.WriteLine("\nСортування вставками:");
            CompareSortingMethods(StandardInsertionSort, StudentInsertionSort);
        }
        
        private static void CompareSortingMethods(Func<int[], int[]> standard, Func<int[], int[]> student)
        {
            int[] array = GenerateRandom(10000);

            double standardTime, studentTime;
            
            int[] arrayForStandard = new int[array.Length], arrayForStudent = new int[array.Length];

            using CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(5));

            array.CopyTo(arrayForStandard, 0);
            array.CopyTo(arrayForStudent, 0);

            try
            {
                standardTime = MeasureTime(standard, arrayForStandard);
                Console.WriteLine($"Час виконання стандартного алгоритму: {standardTime} мс");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Помилка у стандартному методі: {e.Message}");
                return;
            }

            try
            {
                studentTime = MeasureTime(student, arrayForStudent);
                Console.WriteLine($"Час виконання студентського алгоритму: {studentTime} мс");

                if (!IsArraySorted(arrayForStudent))
                {
                    Console.WriteLine("Метод студента неправильно відсортував масив)");
                    return;
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Час виконання методу студента перевищив 5 секунд)");
                return;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Помилка у студентському методі: {e.Message}");
                return;
            }

            if (standardTime - 200 <= studentTime && studentTime <= standardTime + 200)
            {
                Console.WriteLine("Алгоритми мають однаковий час виконання!");
            }
            else
            {
                Console.WriteLine("Алгоритми мають різний час виконання!");
            }
        }
        
        private static double MeasureTime(Func<int[], int[]> sortingAlgorithm, int[] array)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            sortingAlgorithm(array);
            stopwatch.Stop();

            return stopwatch.Elapsed.TotalMilliseconds;
        }
        
        private static int[] StandardInsertionSort(int[] array)
        {
            for (int i = 1; i < array.Length; ++i)
            {
                int key = array[i];
                int j = i - 1;

                while (j >= 0 && array[j] > key)
                {
                    array[j + 1] = array[j];
                    j = j - 1;
                }

                array[j + 1] = key;
            }

            return array;
        }
        
        private static int[] StudentInsertionSort(int[] array)
        {
            for (int i = 1; i < array.Length; ++i)
            {
                int key = array[i];
                int j = i - 1;

                while (j >= 0 && array[j] > key)
                {
                    array[j + 1] = array[j];
                    j = j - 1;
                }

                array[j + 1] = key;
            }

            return array;
        }
        
        private static int[] StandardShakerSort(int[] array)
        {
            int length = array.Length;
            bool swapped;

            do
            {
                swapped = false;
                
                for (int i = 0; i < length - 1; i++)
                {
                    if (array[i] > array[i + 1])
                    {
                        (array[i], array[i + 1]) = (array[i + 1], array[i]);
                        swapped = true;
                    }
                }
                
                if (!swapped) break;
                
                swapped = false;
                
                for (int i = length - 1; i > 0; i--)
                {
                    if (array[i - 1] > array[i])
                    {
                        
                        (array[i - 1], array[i]) = (array[i], array[i - 1]);
                        swapped = true;
                    }
                }

            } while (swapped);

            return array;
        }
        
        private static int[] StudentShakerSort(int[] array)
        {
            int length = array.Length;
            bool swapped;

            do
            {
                swapped = false;
                
                for (int i = 0; i < length - 1; i++)
                {
                    if (array[i] > array[i + 1])
                    {
                        (array[i], array[i + 1]) = (array[i + 1], array[i]);
                        swapped = true;
                    }
                }
                
                if (!swapped) break;
                
                swapped = false;
                
                for (int i = length - 1; i > 0; i--)
                {
                    if (array[i - 1] > array[i])
                    {
                        
                        (array[i - 1], array[i]) = (array[i], array[i - 1]);
                        swapped = true;
                    }
                }

            } while (swapped);

            return array;
        }
        private static int[] GenerateRandom(int size)
        {
            var random = new Random();
            var array = new int[size];

            for (int i = 0; i < size; i++)
            {
                array[i] = random.Next(10000);
            }

            return array;
        }

        private static bool IsArraySorted(int[] arr)
        {
            for (int i = 0; i < arr.Length - 1; i++)
            {
                if (arr[i] > arr[i + 1]) return false;
            }

            return true;
        }
    }
}