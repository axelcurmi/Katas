using System.Text.RegularExpressions;
using Katas.Lib;

namespace Katas.AdventOfCode.Y2023.Day01
{
    public class Solver : ISolver
    {
        public string Name => "Trebuchet?!";
        
        private int ParseNumber(string input) => input switch
            {
                "one" => 1,
                "two" => 2,
                "three" => 3,
                "four" => 4,
                "five" => 5,
                "six" => 6,
                "seven" => 7,
                "eight" => 8,
                "nine" => 9,
                "zero" => 0,
                _ => int.Parse(input)
            };

        private int Solve(string input, string regexPattern)
        {
            return (from line in input.Split("\n")
                let first = Regex.Match(line, regexPattern).Value
                let last = Regex.Match(line, regexPattern, RegexOptions.RightToLeft).Value
                select ParseNumber(first) * 10 + ParseNumber(last)).Sum();
        }
        
        public int Solve(string input)
        {
            var partOne = Solve(input, @"\d");
            Console.WriteLine($"- Part one: {partOne}");

            var partTwo = Solve(input, @"\d|one|two|three|four|five|six|seven|eight|nine|zero");
            Console.WriteLine($"- Part two: {partTwo}");
            
            return 0;
        }
    }
}
