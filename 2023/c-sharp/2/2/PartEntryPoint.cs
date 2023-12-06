using System.Text;

namespace _2;

public static class PartEntryPoint
{
  public static void PartOne()
  {
    string? line = string.Empty;
    string currExecutableDir = Directory.GetCurrentDirectory();
    string toCutOff = "bin\\Debug\\net.8.0";
    string inputDir = currExecutableDir.Substring(0, currExecutableDir.Length - toCutOff.Length);
    int redA = 12;
    int greenA = 13;
    int blueA = 14;

    using StreamReader sr = new StreamReader(Path.Combine(inputDir, "input.txt"));
    line = sr.ReadLine()!;
    StringBuilder sb = new StringBuilder();
    int sum = 0;
    while (line != null)
    {
      string[] split = line.Split(' ');
      int gameId = Convert.ToInt16(split[1].Substring(0, split[1].Length - 1));
      bool valid = true;
      int amount = 0;
      // Its either , or ''
      char separator = ' ';

      for (int i = 2; i < split.Length; i++)
      {
        if (i % 2 == 0)
        {
          amount = Convert.ToInt32(split[i]);
        }
        else
        {
          sb.Clear();
          // if else we know its color
          // TODO: This could be substring?
          for (int j = 0; j < split[i].Length; j++)
          {
            if (split[i][j] == ',' || split[i][j] == ';')
            {
              // if this is true we also know its end of foor loop
              separator = split[i][j];
            }
            else
            {
              sb.Append(split[i][j]);
            }
          }

          string color = sb.ToString();
          if (color == "blue" && amount > blueA)
          {
            valid = false;
            break;
          } else if (color == "red" && amount > redA)
          {
            valid = false;
            break;
          } else if (color == "green" && amount > greenA)
          {
            valid = false;
            break;
          }
        }
      }

      if (valid)
        sum += gameId;
      line = sr.ReadLine();
    }

    Console.WriteLine($"Part One Result Is: {sum}");
  }

  public static void PartTwo()
  {
    string? line = string.Empty;
    string currExecutableDir = Directory.GetCurrentDirectory();
    string toCutOff = "bin\\Debug\\net.8.0";
    string inputDir = currExecutableDir.Substring(0, currExecutableDir.Length - toCutOff.Length);
    Int16 redA = 0;
    Int16 greenA = 0;
    Int16 blueA = 0;

    using StreamReader sr = new StreamReader(Path.Combine(inputDir, "input.txt"));
    line = sr.ReadLine()!;
    StringBuilder sb = new StringBuilder();
    int sum = 0;
    while (line != null)
    {
      string[] split = line.Split(' ');
      Int16 amount = 0;
      // Its either , or ''
      redA = 0;
      greenA = 0;
      blueA = 0;

      for (int i = 2; i < split.Length; i++)
      {
        if (i % 2 == 0)
        {
          amount = Convert.ToInt16(split[i]);
        }
        else
        {
          sb.Clear();
          // if else we know its color
          // TODO: This could be substring?
          for (int j = 0; j < split[i].Length; j++)
          {
            if (split[i][j] != ','  && split[i][j] != ';')
            {
              sb.Append(split[i][j]);
            }
          }

          string color = sb.ToString();
          if (color == "blue" && blueA < amount)
          {
            blueA = amount;
          }
          else if (color == "red" && redA < amount)
          {
            redA = amount;
          }
          else if (color == "green" && greenA < amount)
          {
            greenA = amount;
          }
        }
      }

      sum += (redA * greenA * blueA);
      line = sr.ReadLine();

    }

    Console.WriteLine($"Part Two Result Is: {sum}");
  }
}