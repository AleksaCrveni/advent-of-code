// See https://aka.ms/new-console-template for more information

using System.Runtime.InteropServices;

void P1(string[] lines)
{
  int safe = 0;
  foreach (var line in lines)
  {
    var split = line.Split(' ');
    bool safeB = true;
    // if this is false then its descending
    bool ascending = true;

    if (split[0] == split[2])
      continue;
    if (Convert.ToInt32(split[0]) > Convert.ToInt32(split[1]))
      ascending = false;

    for (int i = 0; i < split.Length - 1; i++)
    {
      int curr = Convert.ToInt32(split[i]);
      int next = Convert.ToInt32(split[i + 1]);
      if (curr == next)
      {
        safeB = false;
        break;
      }
      // should be ascending order
      if (curr < next && !ascending)
      {
        safeB = false;
        break;
      }

      if (curr > next && ascending)
      {
        safeB = false;
        break;
      }

      if (Math.Abs(curr - next) > 3)
      {
        safeB = false;
        break;
      }
      
    }

    if (safeB) safe++;
  }
  Console.WriteLine($"Safe count is {safe}");
}

void P2(string[] lines)
{
  // Bruteforce solution, should look into more optimized solution
  List<List<string>> ReturnAllArrays(string[] chars)
  {
    List<List<string>> strings = new List<List<string>>();
    for (int i = 0; i < chars.Length; i++)
      strings.Add(new List<string>());

      for (int i = 0; i < chars.Length ; i++)
    {
      for (int j = 0; j < strings.Count; j++)
      {
        if (j != i)
          strings[j].Add(chars[i]);
      }
    }

    return strings;
  }

  bool CheckIfSafe(string[] level)
  {
    bool ascending = true;

    if (level[0] == level[1])
      return false;
    if (Convert.ToInt32(level[0]) > Convert.ToInt32(level[1]))
      ascending = false;

    for (int i = 0; i < level.Length - 1; i++)
    {
      int curr = Convert.ToInt32(level[i]);
      int next = Convert.ToInt32(level[i + 1]);
      if (curr == next)
        return false;
      // should be ascending order
      if (curr < next && !ascending)
        return false;

      if (curr > next && ascending)
        return false;

      if (Math.Abs(curr - next) > 3)
        return false;
    }

    return true;
  }

  int safe = 0;
  foreach (var line in lines)
  {
    var split = line.Split(' ');
    if (CheckIfSafe(split))
    {
      safe++;
      continue;
    }
    var data = ReturnAllArrays(split);
    var badCount = 0;
    bool safeB = false;
    foreach (var item in data)
    {
      if (CheckIfSafe(item.ToArray<string>()))
        safeB = true;
    }

    if (safeB) safe++;
  }
  Console.WriteLine($"Safe count is {safe}");
}

bool test = false;
if (args.Length > 0)
  if (args[0] == "Real")
    test = false;

string filename = test ? "input_sample.txt" : "input.txt";

string[] lines = File.ReadAllLines(filename);
P1(lines);
P2(lines);

Console.ReadKey();