using System.Linq;

namespace AdventOfCode
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("yolo");
            //firstDay();
            //secondDay();
            //dayThree();
            //dayFour();
            dayFive();
        }

        static void firstDay()
        {
            var currDir = System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            var pathToDay1Data = Path.Combine(currDir, "InputData", "DayOneData.txt");

            var elveDict = new Dictionary<int, int>();

            var rawCalorieLines = File.ReadAllLines(pathToDay1Data);

            var tempCalorieTotal = 0;
            var elfKey = 1;

            foreach (var calories in rawCalorieLines)
            {
                if (string.IsNullOrEmpty(calories))
                {
                    // stop, next elf
                    elveDict.Add(elfKey, tempCalorieTotal);
                    elfKey++;
                    tempCalorieTotal = 0;
                }
                else
                {
                    tempCalorieTotal += Convert.ToInt32(calories);
                }
            }

            var orderdElvDict = elveDict.OrderByDescending(x => x.Value);

            var top3 = orderdElvDict.Take(3).Sum(x => x.Value);

            var maxCal = elveDict.Values.Max();
            var elf = elveDict.SingleOrDefault(x => x.Value == maxCal);

            //foreach (var keyValuePair in orderdElvDict)
            //{
            //    Console.WriteLine($"elf {keyValuePair.Key} cal count is {keyValuePair.Value}");
            //}

            Console.WriteLine($"the elf with the highest calorie count: {elf.Key} with a cal count of {maxCal}");
            Console.WriteLine($"the top 3 sum is {top3}");

        }

        static void secondDay()
        {
            var currDir = System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            var pathToDay2Data = Path.Combine(currDir, "InputData", "DayTwoData.txt");

            var rawData = File.ReadAllLines(pathToDay2Data);
            var totalScore = 0;
            var shapeScore = 0; // paper
            var optimal = false;

            Console.WriteLine($"START");

            for (var index = 0; index < rawData.Length; index++)
            {
                var round = rawData[index];
                var throws = round.Split(" ");
                var oppThrow = throws[0];
                var myThrow = throws[1];

                if (optimal)
                {
                    var roundScore = 0;

                    shapeScore = myThrow switch
                    {
                        //rock
                        "X" => 1,
                        //paper
                        "Y" => 2,
                        //scissors
                        "Z" => 3,
                        _ => shapeScore
                    };

                    if (didIWLorD(oppThrow, myThrow) == "w")
                    {
                        roundScore = 6 + shapeScore;
                    }
                    else if (didIWLorD(oppThrow, myThrow) == "l")
                    {
                        roundScore = 0 + shapeScore;
                    }
                    else
                    {
                        roundScore = 3 + shapeScore;
                    }

                    totalScore += roundScore;

                    Console.WriteLine($"{didIWLorD(oppThrow, myThrow)} - round score : {roundScore} - running total score is {totalScore}");

                }
                else
                {
                    var roundScore = 0;
                    var endResult = myThrow;

                    shapeScore = whatToPlay(oppThrow, endResult) switch
                    {
                        //rock
                        "X" => 1,
                        //paper
                        "Y" => 2,
                        //scissors
                        "Z" => 3,
                        _ => shapeScore
                    };

                    if (endResult == "Z") // win
                    {
                        roundScore = 6 + shapeScore;
                    }
                    else if (endResult == "Y") // draw
                    {
                        roundScore = 3 + shapeScore;
                    }
                    else //lose
                    {
                        roundScore = 0 + shapeScore;
                    }

                    totalScore += roundScore;

                    Console.WriteLine($"i need to: {endResult} so i play: {whatToPlay(oppThrow, endResult)} - round score : {roundScore} - running total score is {totalScore}");
                }
            }

            Console.WriteLine($"END: THE total score is {totalScore}");

        }

        static void dayThree()
        {
            var currDir = System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            var pathToDay2Data = Path.Combine(currDir, "InputData", "DayThreeData.txt");

            var rawData = File.ReadAllLines(pathToDay2Data);
            Console.WriteLine($"START");
            List<int> total = new();
            List<char> lowerLoopUp = new List<char>()
            {
                'a',
                'b',
                'c',
                'd',
                'e',
                'f',
                'g',
                'h',
                'i',
                'j',
                'k',
                'l',
                'm',
                'n',
                'o',
                'p',
                'q',
                'r',
                's',
                't',
                'u',
                'v',
                'w',
                'x',
                'y',
                'z'
            };
            List<char> allLoopUp = new List<char>();
            allLoopUp.AddRange(lowerLoopUp);
            foreach (var c in lowerLoopUp)
            {
                allLoopUp.Add(char.ToUpper(c));
            }

            var totalPriority = 0;

            // part 1
            //foreach (var item in rawData)
            //{
            //    var length = item.Length;
            //    var compartmentLength = item.Length / 2;
            //    var firstComp = item.Substring(0, compartmentLength);
            //    var secondComp = item.Substring(compartmentLength);
            //    Console.WriteLine($"firstComp {firstComp}");
            //    Console.WriteLine($"secondComp {secondComp}");
            //    var inter = firstComp.Intersect(secondComp).SingleOrDefault();
            //    Console.WriteLine($"\t {inter} : value - {allLoopUp.IndexOf(inter) + 1}");

            //    totalPriority += allLoopUp.IndexOf(inter) + 1;
            //}

            //Console.WriteLine($"total priority {totalPriority}");

            // part 2
            var startTake = 0;
            var endTake = 0;
            string firstRuck, secondRuck, thirdRuck = "";
            for (int i = 0; i < rawData.Length; i += 3)
            {
                char inter = '0';

                firstRuck = rawData[i];
                secondRuck = rawData[i + 1];
                thirdRuck = rawData[i + 2];

                inter = firstRuck.Intersect(secondRuck).Intersect(thirdRuck).SingleOrDefault();
                Console.WriteLine($"\t {inter} : value - {allLoopUp.IndexOf(inter) + 1}");
                totalPriority += allLoopUp.IndexOf(inter) + 1;
            }

            Console.WriteLine($"total priority {totalPriority}");

        }

        static void dayFour()
        {
            var currDir = System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            var pathToDay2Data = Path.Combine(currDir, "InputData", "DayFourData.txt");

            var rawData = File.ReadAllLines(pathToDay2Data);

            List<Tuple<string, string>> overLaps = new List<Tuple<string, string>>();

            foreach (var pair in rawData)
            {
                var splitPairs = pair.Split(",");
                var f = splitPairs[0];
                var s = splitPairs[1];
                if (checkOverlapTwo(f, s))
                {

                    Console.WriteLine($"overlap found {new string(f)} - {new string(s)}");
                    overLaps.Add(new Tuple<string, string>(f.ToString(), s.ToString()));
                }
            }

            Console.WriteLine($"TOtal overlaps found {overLaps.Count}");
        }

        static void dayFive()
        {
            var currDir = System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            var pathToDay2Data = Path.Combine(currDir, "InputData", "DayFiveData.txt");

            var rawData = File.ReadAllLines(pathToDay2Data);

            var moveIns = rawData.Where(x => x.StartsWith("move"));

            var dataOnly = rawData.Where(x => x.StartsWith("move") == false && string.IsNullOrEmpty(x) == false).ToList();
            dataOnly.Reverse();

            //for (int i = 0; i < dataOnly.Count(); i++)
            //{
            //    Console.WriteLine(dataOnly[i]);
            //}

            Dictionary<int, List<string>> crateStackDictionary = new Dictionary<int, List<string>>();

            var numberOfCrateStacks = dataOnly.First().Split(" ").Count(x => string.IsNullOrEmpty(x) == false);

            for (int i = 1; i < numberOfCrateStacks + 1; i++)
            {
                crateStackDictionary.Add(i, new List<string>());
            }

            for (var index = 0; index < dataOnly.Count; index++)
            {
                if (index == 0)
                {
                    continue;
                }

                var line = dataOnly[index];
                var dicCounter = 1;
                for (int lineIndex = 0; lineIndex < line.Length; lineIndex += 3)
                {
                    var firstSet = line[lineIndex];
                    var secondSet = line[lineIndex + 1];
                    var thirdSet = line[lineIndex + 2];

                    if (firstSet != ' ' && secondSet != ' ' && thirdSet != ' ')
                    {
                        crateStackDictionary[dicCounter].Insert(0, new string(new char[] { firstSet, secondSet, thirdSet }));
                    }

                    dicCounter++;
                    lineIndex++;
                }

            }

            foreach (var crateStack in crateStackDictionary)
            {
                Console.WriteLine($"Crate {crateStack.Key} has items {string.Join(",", crateStack.Value)}");
            }

            Console.WriteLine();
            Console.WriteLine("insert crane noises here");
            Console.WriteLine();

            foreach (var moveInstruction in moveIns)
            {
                var test = moveInstruction.Split(" ").Where(x => int.TryParse(x, out int res) == true);

                var nums = test.Select(x => int.TryParse(x, out int res) ? res : 0).ToList();

                var numberOfCratesToMove = nums[0];
                var sourceCrate = nums[1];
                var destinationCrate = nums[2];

                var theCratesToMove = crateStackDictionary[sourceCrate].Take(numberOfCratesToMove).ToList();

                // part 1
                //foreach (var crate in theCratesToMove)
                //{
                //    crateStackDictionary[destinationCrate].Insert(0, crate);
                //}

                // part 2
                crateStackDictionary[destinationCrate].InsertRange(0, theCratesToMove);

                crateStackDictionary[sourceCrate].RemoveRange(0, theCratesToMove.Count);


                Console.WriteLine($"---- After Move");

                foreach (var crateStack in crateStackDictionary)
                {
                    Console.WriteLine($"Crate {crateStack.Key} has items {string.Join(",", crateStack.Value)}");
                }
            }

            Console.WriteLine($"---- DONE ----");


            foreach (var crateStack in crateStackDictionary)
            {
                Console.WriteLine($"Crate {crateStack.Key} has items {string.Join(",", crateStack.Value)}");
            }

            List<string> output1 = new List<string>();
            

            foreach (var crateStack in crateStackDictionary)
            {
                output1.Add(crateStack.Value.First());
            }


            Console.WriteLine($"output 1 {string.Join("", output1).Replace("[","").Replace("]", "")}");
        }

        static bool checkOverlapOne(string f, string s)
        {
            bool passes = false;

            var firstFirstNumber = Convert.ToInt32(f.Split("-")[0]);
            var firstSecondNumber = Convert.ToInt32(f.Split("-")[1]);
            var secondFirstNumber = Convert.ToInt32(s.Split("-")[0]);
            var secondSecondNumber = Convert.ToInt32(s.Split("-")[1]);

            if (firstFirstNumber >= secondFirstNumber && firstSecondNumber <= secondSecondNumber)
            {
                return true;
            }

            if (secondFirstNumber >= firstFirstNumber && secondSecondNumber <= firstSecondNumber)
            {
                return true;
            }

            if (firstFirstNumber == firstSecondNumber)
            {
                if (firstFirstNumber >= secondFirstNumber && firstFirstNumber <= secondSecondNumber)
                {
                    return true;
                }
            }

            if (secondFirstNumber == secondSecondNumber)
            {
                if (secondFirstNumber >= firstFirstNumber && secondFirstNumber <= firstSecondNumber)
                {
                    return true;
                }
            }

            return passes;
        }


        static bool checkOverlapTwo(string f, string s)
        {
            bool passes = false;

            var firstFirstNumber = Convert.ToInt32(f.Split("-")[0]);
            var firstSecondNumber = Convert.ToInt32(f.Split("-")[1]);
            var secondFirstNumber = Convert.ToInt32(s.Split("-")[0]);
            var secondSecondNumber = Convert.ToInt32(s.Split("-")[1]);

            if (firstFirstNumber >= secondFirstNumber && firstFirstNumber <= secondSecondNumber)
            {
                return true;
            }

            if (firstSecondNumber >= secondFirstNumber && firstSecondNumber <= secondSecondNumber)
            {
                return true;
            }

            if (secondFirstNumber >= firstFirstNumber && secondFirstNumber <= firstSecondNumber)
            {
                return true;
            }

            if (secondSecondNumber >= firstFirstNumber && secondSecondNumber <= firstSecondNumber)
            {
                return true;
            }

            return passes;
        }

        // W, L, D
        static string didIWLorD(string oppThrow, string myThrow)
        {
            // A = rock / 1
            // B = paper / 2
            // C = scissors / 3

            // X = rock / 1
            // Y = paper / 2
            // Z = scissors / 3
            if (oppThrow == "A" && myThrow == "X")
            {
                return "d";
            }
            else if (oppThrow == "A" && myThrow == "Y")
            {
                return "w";
            }
            else if (oppThrow == "A" && myThrow == "Z")
            {
                return "l";
            }
            else if (oppThrow == "B" && myThrow == "X")
            {
                return "l";
            }
            else if (oppThrow == "B" && myThrow == "Y")
            {
                return "d";
            }
            else if (oppThrow == "B" && myThrow == "Z")
            {
                return "w";
            }
            else if (oppThrow == "C" && myThrow == "X")
            {
                return "w";
            }
            else if (oppThrow == "C" && myThrow == "Y")
            {
                return "l";
            }
            else //if (oppThrow == "C" && myThrow == "Z")
            {
                return "d";
            }
        }

        static string whatToPlay(string oppThrow, string endResult)
        {
            // A = rock / 1
            // B = paper / 2
            // C = scissors / 3

            // X = rock / 1
            // Y = paper / 2
            // Z = scissors / 3
            if (endResult == "Y") // draw
            {
                if (oppThrow == "A")
                {
                    return "X";
                }
                else if (oppThrow == "C")
                {
                    return "Z";
                }
                else //if (oppThrow == "C" && myThrow == "Z")
                {
                    return "Y";
                }
            }
            else if (endResult == "Z") //win
            {
                if (oppThrow == "A")
                {
                    return "Y";
                }
                else if (oppThrow == "C")
                {
                    return "X";
                }
                else //if (oppThrow == "C" && myThrow == "Z")
                {
                    return "Z";
                }
            }
            else // lose
            {
                if (oppThrow == "A")
                {
                    return "Z";
                }
                else if (oppThrow == "C")
                {
                    return "Y";
                }
                else //if (oppThrow == "C" && myThrow == "Z")
                {
                    return "X";
                }
            }
        }
    }
}