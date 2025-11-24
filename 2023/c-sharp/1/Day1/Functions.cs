using System.Text;

namespace _1;

public static class Functions
{
  private static int startI = 0;
  private static int endI = 0;
  private static bool foundFirst = false;
  private static bool foundLast = false;
  private static string firstChar = "";
  private static string lastChar = "";
  private static StringBuilder firstText = new StringBuilder();
  private static StringBuilder lastText = new StringBuilder();
  public static int ExtractNumberFromLineP1(string line)
  {
    startI = 0;
    endI = line.Length - 1;
    foundFirst = false;
    foundLast = false;
    firstChar = "";
    lastChar = "";
    while (!foundFirst || !foundLast)
    {
      if (!foundFirst)
      {
        if (Char.IsDigit(line[startI]))
        {
          foundFirst = true;
          firstChar = line[startI].ToString();
        }
        else
        {
          startI++;
        }
      }

      if (!foundLast)
      {
        if (Char.IsDigit(line[endI]))
        {
          foundLast = true;
          lastChar = line[endI].ToString();
        }
        else
        {
          endI--;
        }
      }
    }

    return Convert.ToInt32($"{firstChar}{lastChar}");
  }

  public static int ExtractNumberFromLineP2(string line)
  {
    startI = 0;
    endI = line.Length - 1;
    foundFirst = false;
    foundLast = false;
    firstChar = "";
    lastChar = "";
    firstText.Clear();
    lastText.Clear();
    while (!foundFirst || !foundLast)
    {
      if (!foundFirst)
      {
        firstText.Append(line[startI]);
        if (Char.IsDigit(line[startI]))
        {
          foundFirst = true;
          firstChar = line[startI].ToString();
        }
        else
        {
          int d = GetDigitFromText(firstText.ToString());
          if (d != -1)
          {
            foundFirst = true;
            firstChar = d.ToString();
          }

          startI++;
        }
      }

      if (!foundLast)
      {
        lastText.Insert(0, line[endI]);
        if (Char.IsDigit(line[endI]))
        {
          foundLast = true;
          lastChar = line[endI].ToString();
        }
        else
        {
          int d = GetDigitFromText(lastText.ToString());
          if (d != -1)
          {
            foundLast = true;
            lastChar = d.ToString();
          }

          endI--;
        }
      }
    }

    return Convert.ToInt32($"{firstChar.ToString()}{lastChar.ToString()}");
  }

  private static int GetDigitFromText(string text)
  {
    // TODO: Optimize this
    if (text.Contains("one"))
      return 1;
    if (text.Contains("two"))
      return 2;
    if (text.Contains("three"))
      return 3;
    if (text.Contains("four"))
      return 4;
    if (text.Contains("five"))
      return 5;
    if (text.Contains("six"))
      return 6;
    if (text.Contains("seven"))
      return 7;
    if (text.Contains("eight"))
      return 8;
    if (text.Contains("nine"))
      return 9;

    return -1;
    /*
    return text switch
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
      _ => -1
    };*/
  }
}