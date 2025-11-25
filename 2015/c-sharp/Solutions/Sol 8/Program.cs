bool sample = false;
string filename = "input.txt";
if (sample)
  filename = "input_sample.txt";

string[] input = File.ReadAllLines(filename);

Part1(input);
Part2(input);
void Part1(string[] input)
{
  int sum = 0;
  foreach (string line in input)
  {
    int inMemCount = CountCodeRepresentation(line);
    sum += line.Length - inMemCount;
  }

  Console.WriteLine($"Part 1 result is {sum}");
}

void Part2(string[] input)
{
  int sum = 0;
  char[] esc = { '\\', '\"' };
  foreach (string line in input)
  {
    int codeCount = 2; // account for new "" added 
    int inMemCount = CountCodeRepresentation(line);
    for (int i =0; i < line.Length; i++)
    {
      char c = line[i];
      if (c == '\\' || c == '\"')
        codeCount++;
      codeCount++;
    }
    sum += codeCount - line.Length;
  }

  Console.WriteLine($"Part 2 result is {sum}");
}

int CountCodeRepresentation(string line)
{
  int inMemCount = 0;
  ReadOnlySpan<char> l = line.AsSpan().Slice(1, line.Length - 2);
  for (int i = 0; i < l.Length; i++)
  {
    char c = l[i];
    if (c == '\\')
    {
      if (l[i + 1] == 'x')
      {
        i += 3;
      }
      else
      {
        i++;
      }
    }
    inMemCount++;
  }
  return inMemCount;
}