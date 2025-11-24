bool sample = false;
string filename = "input.txt";
if (sample)
  filename = "input_sample.txt";

string input = File.ReadAllText(filename);

Part1(input);
Part2(input);
void Part1(string input)
{
  int floor = 0;
  foreach (char c in input)
    if (c == '(')
      floor++;
    else if (c == ')')
      floor--;
  Console.WriteLine($"Part 1 result is {floor}");
}

void Part2(string input)
{
  int floor = 0;
  int i = 0;
  for (; i < input.Length; i++)
  {
    if (input[i] == '(')
      floor++;
    else if (input[i] == ')')
      floor--;
    if (floor == -1)
      break;
  }

  Console.WriteLine($"Part 2 result is {i + 1}");
}
