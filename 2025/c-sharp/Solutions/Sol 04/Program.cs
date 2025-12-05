
bool sample = false;
string path = "input.txt";
if (sample)
  path = "input_sample.txt";

string[] input = File.ReadAllLines(path);

(int xOff, int yOff)[] adjacents = [
  (-1, -1),
  ( 0, -1),
  ( 1, -1),
  (-1,  0),
  ( 1,  0),
  (-1,  1),
  ( 0,  1),
  ( 1,  1)];
Part1(input);
Part2(input);


void Part1(string[] input)
{
  // no need to convert to matrix
  int sum = 0;
  for (int y = 0; y < input.Length; y++)
  {
    for (int x = 0; x < input[0].Length; x++)
    {
      char c = input[y][x];
      if (c == '.')
        continue;
      
      sum += IsValidP1(input, (x, y)) ? 1 : 0;
    }
  }
  Console.WriteLine($"Part 1 result is {sum}");
}


bool IsValidP1(string[] input, (int x, int y) curr)
{

  int counter = 0;
  foreach ((int xOff, int yOff) offSets in adjacents)
  {
    (int x, int y) adj = (curr.x + offSets.xOff, curr.y + offSets.yOff);
    if (adj.x < 0 || adj.y < 0 || adj.x >= input[0].Length || adj.y >= input.Length)
      continue;

    counter += input[adj.y][adj.x] == '@' ? 1 : 0;
    if (counter >= 4)
      return false;
  }
  return true;
}

int RemoveRolls(string[] input)
{
  int sum = 0;
  for (int y = 0; y < input.Length; y++)
  {
    for (int x = 0; x < input[0].Length; x++)
    {
      char c = input[y][x];
      if (c == '.')
        continue;
      bool isValid = IsValidP1(input, (x, y));
      if (isValid)
      {
        // hack beacuse i use strings because im stupid
        char[] cArr = input[y].ToCharArray();
        cArr[x] = '.';
        input[y] = new string(cArr);
        sum++;
      }
    }
  }
  return sum;
}

void Part2(string[] input)
{
  int sum = 0;
  int removed = RemoveRolls(input);
  sum += removed;
  while (removed > 0)
  {
    removed = RemoveRolls(input);
    sum += removed;
  }

  Console.WriteLine($"Part 2 result is {sum}");
}
