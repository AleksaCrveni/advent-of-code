// See https://aka.ms/new-console-template for more information
using Core;
using System.Security.AccessControl;
using System.Security.Cryptography;
void P1(string[] lines)
{
  // not fastests but interesting to write 
  MySortedList<int> left = new(lines.Length);
  MySortedList<int> right = new(lines.Length);

  for (int i = 0; i < lines.Length; i++)
  {
    var split = lines[i].Split("  ");
    left.Add(Convert.ToInt32(split[0]));
    right.Add(Convert.ToInt32(split[1]));
  }

  int sum = 0;
  for (int i = 0; i < left.Count; i++)
  {
    sum += Math.Abs(left[i] - right[i]);
  }
  Console.WriteLine($"Result is: {sum}");
}

void P2(string[] lines)
{
  // Not fastest sol
  int sum = 0;
  Dictionary<int, int> countTracker = new Dictionary<int, int>();
  List<int> leftList= new List<int>();

  for (int i = 0; i < lines.Length; i++)
  {
    var split = lines[i].Split("  ");
    int right = Convert.ToInt32(split[1]);
    leftList.Add(Convert.ToInt32(split[0]));
    if (countTracker.ContainsKey(right))
      countTracker[right]++;
    else
      countTracker[right] = 1;
  }

  for (int i =0; i < leftList.Count; i++)
  {
    int count = countTracker.GetValueOrDefault(leftList[i], 0);
    sum += leftList[i] * count;
  }

  Console.WriteLine($"Result is: {sum}");
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
