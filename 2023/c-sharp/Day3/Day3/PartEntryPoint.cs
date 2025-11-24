using System.Text;
using BenchmarkDotNet.Attributes;

namespace Day3;

public class PartEntryPoint
{
  public static void PartOne()
  {
    // TODO: Optimize this
    // NOTE: Part one of this is solution was looked up, I couldn't solve it
    // I was looking it from point of the sign then the number..

    string currExecutableDir = Directory.GetCurrentDirectory();
    string toCutOff = "bin\\Debug\\net.8.0";
    string inputDir = currExecutableDir.Substring(0, currExecutableDir.Length - toCutOff.Length);
    string? line;
    using StreamReader sr = new StreamReader(Path.Combine(inputDir, "input.txt"));
    List<string> input = new List<string>();
    line = sr.ReadLine()!;
    while (line != null)
    {
      input.Add(line);
      line = sr.ReadLine();
    }

    int width = input[0].Length;
    int height = input[0].Length;
    char[,] map = new char[width, height];
    for (int x = 0; x < width; x++)
    {
      for (int y = 0; y < height; y++)
      {
        map[x, y] = input[y][x];
      }
    }

    int runningTotal = 0;
    var currentNumber = 0;
    bool hasNeighboringSymbol = false;
    for (int y = 0; y < height; y++)
    {
      void EndCurrentNumber()
      {
        if (currentNumber != 0 && hasNeighboringSymbol)
        {
          runningTotal += currentNumber;
        }

        currentNumber = 0;
        hasNeighboringSymbol = false;
      }

      for (int x = 0; x < width; x++)
      {
        // Check if we are reading a number
        // add it to already in progress number
        // check if there is neighbor symbol
        // store it
        // encounter non-number
        // end current number and add it to running total if flag is set

        char currChar = map[x, y];
        if (char.IsDigit(currChar))
        {
          int value = currChar - '0';
          currentNumber = currentNumber * 10 + value;
          foreach (var direction in Directions.WithDiagonals)
          {
            int neighborX = x + direction.X;
            int neighborY = y + direction.Y;
            // skip if neighbour is out of bounds
            if (neighborX < 0 || neighborX >= width || neighborY < 0 || neighborY >= height)
              continue;

            var neighborChar = map[neighborX, neighborY];
            if (!char.IsDigit(neighborChar) && neighborChar != '.')
            {
              hasNeighboringSymbol = true;
            }
          }
        }
        else
        {
          EndCurrentNumber();
        }
      }
      EndCurrentNumber();
    }

    Console.WriteLine(runningTotal);
  }

  public static void PartTwo()
  {
    string currExecutableDir = Directory.GetCurrentDirectory();
    string toCutOff = "bin\\Debug\\net.8.0";
    string inputDir = currExecutableDir.Substring(0, currExecutableDir.Length - toCutOff.Length);
    string? line;
    using StreamReader sr = new StreamReader(Path.Combine(inputDir, "input.txt"));
    List<string> input = new List<string>();
    line = sr.ReadLine()!;
    while (line != null)
    {
      input.Add(line);
      line = sr.ReadLine();
    }

    int width = input[0].Length;
    int height = input[0].Length;
    char[,] map = new char[width, height];
    for (int x = 0; x < width; x++)
    {
      for (int y = 0; y < height; y++)
      {
        map[x, y] = input[y][x];
      }
    }

    int runningTotal = 0;
    var currentNumber = 0;
    bool hasNeighboringAsterisk = false;
    int asteriskX = -1;
    int asteriskY = -1;
    // should have used Point instead of string
    var asteriskMap = new Dictionary<string, List<int>>();
    for (int y = 0; y < height; y++)
    {
      void EndCurrentNumber()
      {
        if (currentNumber != 0 && hasNeighboringAsterisk)
        {
          string key = $"{asteriskX}{asteriskY}";
          List<int>? list;
          bool exists = asteriskMap.TryGetValue(key, out list);
          if (exists)
          {
            list!.Add(currentNumber);
            asteriskMap[key] = list;
          }
          else
          {
            list = new List<int>() { currentNumber };
            asteriskMap.Add(key, list);
          }
        }

        currentNumber = 0;
        asteriskX = -1;
        asteriskY = -1;
        hasNeighboringAsterisk = false;
      }

      for (int x = 0; x < width; x++)
      {
        // Check if we are reading a number
        // add it to already in progress number
        // check if there is neighbor symbol
        // store it
        // encounter non-number
        // end current number and add it to running total if flag is set

        char currChar = map[x, y];
        if (char.IsDigit(currChar))
        {
          int value = currChar - '0';
          currentNumber = currentNumber * 10 + value;
          foreach (var direction in Directions.WithDiagonals)
          {
            int neighborX = x + direction.X;
            int neighborY = y + direction.Y;
            // skip if neighbour is out of bounds
            if (neighborX < 0 || neighborX >= width || neighborY < 0 || neighborY >= height)
              continue;

            var neighborChar = map[neighborX, neighborY];
            if (neighborChar == '*')
            {
              hasNeighboringAsterisk = true;
              asteriskX = neighborX;
              asteriskY = neighborY;
            }
          }
        }
        else
        {
          EndCurrentNumber();
        }
      }
      EndCurrentNumber();
    }

    foreach (KeyValuePair<string, List<int>> kvp in asteriskMap)
    {
      var list = kvp.Value;
      if (list.Count == 2)
      {
        runningTotal += list[0] * list[1];
      }
    }

    Console.WriteLine(runningTotal);
  }
}