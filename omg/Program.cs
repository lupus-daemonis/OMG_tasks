using System;
using System.Collections.Generic;
using System.Linq;

namespace TaskExam
{
    internal class TaskSolver
    {
        public static void Main(string[] args)
        {
            TestGenerateWordsFromWord();
            TestMaxLengthTwoChar();
            TestGetPreviousMaxDigital();
            TestSearchQueenOrHorse();
            TestCalculateMaxCoins();

            Console.WriteLine("All Test completed!");
        }


        /// задание 1) Слова из слова
        public static List<string> GenerateWordsFromWord(string word, List<string> wordDictionary)
        {
            List<string> generatedWords = new List<string>();
            foreach (string currentDictionaryWord in wordDictionary)
            {
                if (CheckWords(word, currentDictionaryWord))
                    generatedWords.Add(currentDictionaryWord);
            }
            generatedWords.Sort();
            return generatedWords;


            bool CheckWords(string keyWord, string dictionaryWord)
            {
                var keyWordToList = keyWord.ToList();
                foreach (char c in dictionaryWord)
                {
                    if (keyWordToList.IndexOf(c) < 0)
                        return false;
                    keyWordToList.Remove(c);
                }
                return true;
            }

        }

        /// задание 2) Два уникальных символа
        public static int MaxLengthTwoChar(string word)
        {
            String uniqueSymbols = new string(word.Distinct().ToArray());
            char c1, c2;
            String newWord;
            int maxLength = 0;
            for (int i = 0; i < uniqueSymbols.Length; i++)
            {
                for (int j = 1; j < uniqueSymbols.Length - i; j++)
                {
                    c1 = uniqueSymbols[i];
                    c2 = uniqueSymbols[i + j];

                    Removing();
                    if (!DoubleLetterFound())
                    {
                        if (newWord.Length > maxLength) maxLength = newWord.Length;
                    }
                }
            }
            return maxLength;


            void Removing()
            {
                newWord = word;
                foreach (char c in uniqueSymbols)
                {
                    if (c != c1 && c != c2)
                    {
                        newWord = word.Replace(c.ToString(), String.Empty);
                    }
                }
            }

            bool DoubleLetterFound()
            {
                for (int i = 0; i < newWord.Length - 1; i++)
                {
                    if (newWord[i] == newWord[i + 1])
                    {
                        return true;
                    }
                }
                return false;
            }

        }

        /// задание 3) Предыдущее число
        public static long GetPreviousMaxDigital(long value)
        {
            var digits = value.ToString().Select(digit => int.Parse(digit.ToString())).ToArray();
            if (!SwapDigits()) return -1;
            long newValue = 0;
            for (int i = 0; i < digits.Length; i++)
            {
                newValue += digits[i] * Convert.ToInt32(Math.Pow(10, digits.Length - i - 1));
            }
            return newValue;


            bool SwapDigits()
            {
                int tmp;
                for (int i = digits.Length - 1; i > 0; i--)
                {
                    for (int j = i - 1; j >= 0; j--)
                    {
                        if (digits[j] > digits[i] && digits[i] != 0)
                        {
                            tmp = digits[i];
                            digits[i] = digits[j];
                            digits[j] = tmp;

                            Array.Sort(digits, j + 1, digits.Length - j - 1);
                            Array.Reverse(digits, j + 1, digits.Length - j - 1);
                            return true;
                        }
                    }
                }
                return false;
            }
        }

        /// задание 4) Конь и Королева
        public static List<int> SearchQueenOrHorse(char[][] gridMap)
        {
            Queue<int> que = new Queue<int>();

            int stepNumber, sx = 0, sy = 0, ex = 0, ey = 0, x, y;
            bool found;

            int n = gridMap.Length;
            int m = gridMap[0].Length;

            bool[,] marked = new bool[n, m];

            List<int> steps = new List<int>();

            GetSteps(0);
            GetSteps(1);

            return steps;


            void GetSteps(int f)
            {
                StartProcess();
                found = (sx == ex && sy == ey);
                while (!found && que.Count > 0)
                {
                    Get();
                    stepNumber++;
                    if (f == 0)
                    {
                        HorsePutAll();
                    }
                    else
                    {
                        QueenPutAll();
                    }
                }
                if (!found) steps.Add(-1);
                else steps.Add(stepNumber);
            }


            void Put(int currentX, int currentY)
            {
                que.Enqueue(currentX);
                que.Enqueue(currentY);
                que.Enqueue(stepNumber);
                marked[currentX, currentY] = true;
            }

            void Get()
            {
                x = que.Dequeue();
                y = que.Dequeue();
                stepNumber = que.Dequeue();
            }

            void HorsePutAll()
            {
                int[,] horseSteps = new int[,] { { 1, -2 }, { 1, 2 }, { -1, -2 }, { -1, 2 },
                                            { 2, -1 }, { 2, 1 }, { -2, -1 }, { -2, 1 } };
                int currentX, currentY;
                int i = 0;
                while (!found && i < 8)
                {
                    currentX = x + horseSteps[i, 0];
                    currentY = y + horseSteps[i, 1];
                    found = (ex == currentX && ey == currentY);
                    if (!found &&
                        currentX >= 0 && currentX < n &&
                        currentY >= 0 && currentY < m &&
                        !marked[currentX, currentY] && gridMap[currentX][currentY] != 'x')
                    {
                        Put(currentX, currentY);
                    }
                    i++;
                }
            }

            void QueenPutAll()
            {
                int[,] queenSteps = new int[,] { { -1, 0 }, { -1, 1 }, { 0, 1 }, { 1, 1 },
                                            { 1, 0 }, { 1, -1 }, { 0, -1 }, { -1, -1 } };
                int currentX, currentY;
                int i = 0;

                while (!found && i < 8)
                {
                    currentX = x + queenSteps[i, 0];
                    currentY = y + queenSteps[i, 1];
                    found = (ex == currentX && ey == currentY);
                    while (!found &&
                        currentX >= 0 && currentX < n &&
                        currentY >= 0 && currentY < m &&
                        gridMap[currentX][currentY] != 'x')
                    {
                        if (!marked[currentX, currentY])
                        {
                            Put(currentX, currentY);
                        }
                        currentX += queenSteps[i, 0];
                        currentY += queenSteps[i, 1];
                        found = (ex == currentX && ey == currentY);
                    }
                    i++;
                }
            }

            void StartProcess()
            {
                que.Clear();
                stepNumber = 0;
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < m; j++)
                    {
                        marked[i, j] = false;

                        if (gridMap[i][j] == 's')
                        {
                            sx = i;
                            sy = j;
                        }
                        if (gridMap[i][j] == 'e')
                        {
                            ex = i;
                            ey = j;
                        }
                    }
                Put(sx, sy);
            }

        }

        /// задание 5) Жадина
        public static long CalculateMaxCoins(int[][] mapData, int idStart, int idFinish)
        {
            Stack<int> towns = new Stack<int>();
            Stack<int> coins = new Stack<int>();
            int money = 0, maxMoney = -1;
            int n = mapData.Length;

            List<int>[] connectedTowns = new List<int>[100];
            List<int>[] coinsOnRoad = new List<int>[100];
            for (int i = 0; i < connectedTowns.Length; i++)
            {
                connectedTowns[i] = new List<int>();
                coinsOnRoad[i] = new List<int>();
            }

            for (int i = 0; i < n; i++)
            {
                connectedTowns[mapData[i][0]].Add(mapData[i][1]);
                connectedTowns[mapData[i][1]].Add(mapData[i][0]);
                coinsOnRoad[mapData[i][0]].Add(mapData[i][2]);
                coinsOnRoad[mapData[i][1]].Add(mapData[i][2]);
            }
            for (int i = 0; i < n; i++)
            {
                connectedTowns[i].Add(-1);
            }

            FindWay(idStart);

            return maxMoney;


            void FindWay(int i)
            {
                towns.Push(i);
                if (i == idFinish)
                {
                    money = 0;
                    foreach (var sum in coins) money += +sum;
                    if (money > maxMoney) maxMoney = money;
                    coins.Pop();
                    towns.Pop();
                    return;

                }
                for (int k = 0; connectedTowns[i][k] != -1; ++k)
                {
                    if (!towns.Contains(connectedTowns[i][k]))
                    {
                        int j = connectedTowns[i][k];
                        coins.Push(coinsOnRoad[i][k]);
                        FindWay(j);
                    }
                }
                towns.Pop();
                if (coins.Count > 0)
                    coins.Pop();
                return;
            }
        }

        /// Тесты (можно/нужно добавлять свои тесты) 

        private static void TestGenerateWordsFromWord()
        {
            var wordsList = new List<string>
            {
                "кот", "ток", "око", "мимо", "гром", "ром", "мама",
                "рог", "морг", "огр", "мор", "порог", "бра", "раб", "зубр"
            };

            AssertSequenceEqual(GenerateWordsFromWord("арбуз", wordsList), new[] { "бра", "зубр", "раб" });
            AssertSequenceEqual(GenerateWordsFromWord("лист", wordsList), new List<string>());
            AssertSequenceEqual(GenerateWordsFromWord("маг", wordsList), new List<string>());
            AssertSequenceEqual(GenerateWordsFromWord("погром", wordsList), new List<string> { "гром", "мор", "морг", "огр", "порог", "рог", "ром" });
        }

        private static void TestMaxLengthTwoChar()
        {
            AssertEqual(MaxLengthTwoChar("beabeeab"), 5);
            AssertEqual(MaxLengthTwoChar("а"), 0);
            AssertEqual(MaxLengthTwoChar("ab"), 2);
        }

        private static void TestGetPreviousMaxDigital()
        {
            AssertEqual(GetPreviousMaxDigital(21), 12l);
            AssertEqual(GetPreviousMaxDigital(531), 513l);
            AssertEqual(GetPreviousMaxDigital(1027), -1l);
            AssertEqual(GetPreviousMaxDigital(2071), 2017l);
            AssertEqual(GetPreviousMaxDigital(207034), 204730l);
            AssertEqual(GetPreviousMaxDigital(135), -1l);
        }

        private static void TestSearchQueenOrHorse()
        {
            char[][] gridA =
            {
                new[] {'s', '#', '#', '#', '#', '#'},
                new[] {'#', 'x', 'x', 'x', 'x', '#'},
                new[] {'#', '#', '#', '#', 'x', '#'},
                new[] {'#', '#', '#', '#', 'x', '#'},
                new[] {'#', '#', '#', '#', '#', 'e'},
            };

            AssertSequenceEqual(SearchQueenOrHorse(gridA), new[] { 3, 2 });

            char[][] gridB =
            {
                new[] {'s', '#', '#', '#', '#', 'x'},
                new[] {'#', 'x', 'x', 'x', 'x', '#'},
                new[] {'#', 'x', '#', '#', 'x', '#'},
                new[] {'#', '#', '#', '#', 'x', '#'},
                new[] {'x', '#', '#', '#', '#', 'e'},
            };

            AssertSequenceEqual(SearchQueenOrHorse(gridB), new[] { -1, 3 });

            char[][] gridC =
            {
                new[] {'s', '#', '#', '#', '#', 'x'},
                new[] {'x', 'x', 'x', 'x', 'x', 'x'},
                new[] {'#', '#', '#', '#', 'x', '#'},
                new[] {'#', '#', '#', 'e', 'x', '#'},
                new[] {'x', '#', '#', '#', '#', '#'},
            };

            AssertSequenceEqual(SearchQueenOrHorse(gridC), new[] { 2, -1 });


            char[][] gridD =
            {
                new[] {'e', '#'},
                new[] {'x', 's'},
            };

            AssertSequenceEqual(SearchQueenOrHorse(gridD), new[] { -1, 1 });

            char[][] gridE =
            {
                new[] {'e', '#'},
                new[] {'x', 'x'},
                new[] {'#', 's'},
            };

            AssertSequenceEqual(SearchQueenOrHorse(gridE), new[] { 1, -1 });

            char[][] gridF =
            {
                new[] {'x', '#', '#', 'x'},
                new[] {'#', 'x', 'x', '#'},
                new[] {'#', 'x', '#', 'x'},
                new[] {'e', 'x', 'x', 's'},
                new[] {'#', 'x', 'x', '#'},
                new[] {'x', '#', '#', 'x'},
            };

            AssertSequenceEqual(SearchQueenOrHorse(gridF), new[] { -1, 5 });
        }

        private static void TestCalculateMaxCoins()
        {
            var mapA = new[]
            {
                new []{0, 1, 1},
                new []{0, 2, 4},
                new []{0, 3, 3},
                new []{1, 3, 10},
                new []{2, 3, 6},
            };

            AssertEqual(CalculateMaxCoins(mapA, 0, 3), 11l);

            var mapB = new[]
            {
                new []{0, 1, 1},
                new []{1, 2, 53},
                new []{2, 3, 5},
                new []{5, 4, 10}
            };

            AssertEqual(CalculateMaxCoins(mapB, 0, 5), -1l);

            var mapC = new[]
            {
                new []{0, 1, 1},
                new []{0, 3, 2},
                new []{0, 5, 10},
                new []{1, 2, 3},
                new []{2, 3, 2},
                new []{2, 4, 7},
                new []{3, 5, 3},
                new []{4, 5, 8}
            };

            AssertEqual(CalculateMaxCoins(mapC, 0, 5), 19l);
        }

        /// Тестирующая система, лучше не трогать этот код

        private static void Assert(bool value)
        {
            if (value)
            {
                return;
            }

            throw new Exception("Assertion failed");
        }

        private static void AssertEqual(object value, object expectedValue)
        {
            if (value.Equals(expectedValue))
            {
                return;
            }

            throw new Exception($"Assertion failed expected = {expectedValue} actual = {value}");
        }

        private static void AssertSequenceEqual<T>(IEnumerable<T> value, IEnumerable<T> expectedValue)
        {
            if (ReferenceEquals(value, expectedValue))
            {
                return;
            }

            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (expectedValue is null)
            {
                throw new ArgumentNullException(nameof(expectedValue));
            }

            var valueList = value.ToList();
            var expectedValueList = expectedValue.ToList();

            if (valueList.Count != expectedValueList.Count)
            {
                throw new Exception($"Assertion failed expected count = {expectedValueList.Count} actual count = {valueList.Count}");
            }

            for (var i = 0; i < valueList.Count; i++)
            {
                if (!valueList[i].Equals(expectedValueList[i]))
                {
                    throw new Exception($"Assertion failed expected value at {i} = {expectedValueList[i]} actual = {valueList[i]}");
                }
            }
        }

    }

}
