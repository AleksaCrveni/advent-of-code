bool sample = false  ;
string filename = "input.txt";
if (sample)
  filename = "input_sample.txt";

int[] input = File.ReadAllLines(filename).Select(int.Parse).ToArray();

Part1(input);
Part2(input);

void Part1(int[] input)
{
  int counter = 0;
  int liters = 150;
  if (sample)
    liters = 25;
  for (long i = 0; i < (1 << input.Length); i++)
  {
    long t = i;
    long s = 0;
    for (int j =0; j < input.Length; j++)
    {
      if (t % 2 == 1)
        s += input[j];
      t /= 2;
      if (t == 0)
        break;
    }

    if (s == liters)
      counter++;
  }
  Console.WriteLine($"Part1 result is {counter}");
}



void Part2(int[] input)
{
  int counter = 0;
  int liters = 150;
  if (sample)
    liters = 25;
  Dictionary<int, int> counterDict = new();
  for (long i = 0; i < (1 << input.Length); i++)
  {
    long t = i;
    long s = 0;
    int c = 0;
    for (int j = 0; j < input.Length; j++)
    {
      if (t % 2 == 1)
      {
        s += input[j];
        c++;
      }
        
      t /= 2;
      if (t == 0)
        break;
    }

    if (s == liters)
    {
      if (counterDict.ContainsKey(c))
        counterDict[c]++;
      else
        counterDict[c] = 1;
    }
  }

  int min = Int32.MaxValue;
  foreach (KeyValuePair<int, int> kvp in counterDict)
  {
    if (kvp.Key < min)
      min = kvp.Key;
  }

  Console.WriteLine($"Part 2 result is {counterDict[min]}");
}