namespace AdventOfCode
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("yolo");
            firstDay();
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
    }
}