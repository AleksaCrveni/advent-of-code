// See https://aka.ms/new-console-template for more information
using Core;
using System.Security.Cryptography;
void P1(string[] lines)
{
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

bool test = true;
if (args.Length > 0)
  if (args[0] == "Real")
    test = false;

string filename = test ? "input_sample.txt" : "input.txt";

string[] lines = File.ReadAllLines(filename);
P1(lines);
