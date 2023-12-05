namespace _1;

public static class PartEntryPoint
{
  public static void PartOne()
  {
    int sum = 0;
    string? line = string.Empty;
    string currExecutableDir = Directory.GetCurrentDirectory();
    string toCutOff = "bin\\Debug\\net.8.0";
    string inputDir = currExecutableDir.Substring(0, currExecutableDir.Length - toCutOff.Length);

    StreamReader sr = new StreamReader(Path.Combine(inputDir, "input.txt"));
    line = sr.ReadLine()!;
    while (line != null)
    {
      sum += Functions.ExtractNumberFromLineP1(line!);
      line = sr.ReadLine();
    }
    sr.Close();
    Console.WriteLine($"Part One: {sum}");
  }

  public static void PartTwo()
  {
    int sum = 0;
    string? line = string.Empty;
    string currExecutableDir = Directory.GetCurrentDirectory();
    string toCutOff = "bin\\Debug\\net.8.0";
    string inputDir = currExecutableDir.Substring(0, currExecutableDir.Length - toCutOff.Length);

    StreamReader sr = new StreamReader(Path.Combine(inputDir, "input.txt"));
    line = sr.ReadLine()!;

    while (line != null)
    {
      sum += Functions.ExtractNumberFromLineP2(line!);
      line = sr.ReadLine();
    }

    sr.Close();
    Console.WriteLine($"Part Two: {sum}");


  }
}