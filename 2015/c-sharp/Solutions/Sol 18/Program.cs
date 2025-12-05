bool sample = false;
string filename = "input.txt";
if (sample)
  filename = "input_sample.txt";

string[] inputLines = File.ReadAllLines(filename);
List<char[]> input = inputLines.Select(s => s.ToCharArray()).ToList();

PointOff[] adjacents = [
  new PointOff(-1, -1),
  new PointOff( 0, -1),
  new PointOff( 1, -1),
  new PointOff(-1,  0),
  new PointOff( 1,  0),
  new PointOff(-1,  1),
  new PointOff( 0,  1),
  new PointOff( 1,  1)];
Part1(input);
input = inputLines.Select(s => s.ToCharArray()).ToList();
Part2(input);
void Part1(List<char[]> input)
{
  int steps = 100;
  if (sample)
  {
    steps = 4;
    PrintState(input);

  }

  List<(PointOff, char)> toChange = new();
  for (int step = 0; step < steps; step++)
  {
    for (int y = 0; y < input[0].Length; y++)
    {
      for (int x = 0; x < input.Count; x++)
      {
        char c = input[y][x];
        (int onCount, int offCount) res = CountAdjacentLights(input, x, y);
        if (c == '#' && res.onCount != 2 && res.onCount != 3)
          toChange.Add((new PointOff(x, y), '.'));
        else if (c == '.' && res.onCount == 3)
          toChange.Add((new PointOff(x, y), '#'));
      }
    }
    foreach((PointOff pos , char val) chg in toChange)
    {
      input[chg.pos.y][chg.pos.x] = chg.val;
    }
    toChange.Clear();
    if (sample)
      PrintState(input);
  }

  (int onCount, int offCount) all = CountAllLights(input);
  Console.WriteLine($"Part 1 result is {all.onCount}");
}

void PrintState(List<char[]> input)
{
  for (int y = 0; y < input[0].Length; y++)
  {
    for (int x = 0; x < input.Count; x++)
    {
      char c = input[y][x];
      Console.Write(c);
    }
    Console.Write('\n');
  }
  Console.WriteLine();
}
(int onCount, int offCount) CountAllLights(List<char[]> input)
{
  int onCounter = 0;
  int offCounter = 0;
  for (int y = 0; y < input[0].Length; y++)
  {
    for (int x = 0; x < input.Count; x++)
    {
      char c = input[y][x];
      if (c == '#')
        onCounter++;
      else
        offCounter++;
    }
  }
  return (onCounter, offCounter);
}

(int onCount, int offCount) CountAdjacentLights(List<char[]> input, int x, int y)
{
  int onCounter = 0;
  int offCounter = 0;
  PointOff curr;
  foreach (PointOff p in adjacents)
  {
    curr = new PointOff(x + p.x, y + p.y);
    if (curr.x < 0 || curr.x >= input[0].Length || curr.y < 0 || curr.y >= input.Count)
      continue;
    if (input[curr.y][curr.x] == '#')
      onCounter++;
    else
      offCounter++;
  }
  return (onCounter, offCounter);
}


void Part2(List<char[]> input)
{
  int steps = 100;
  if (sample)
  {
    steps = 5;
    PrintState(input);

  }
  int maxY = input.Count - 1;
  int maxX = input[0].Length -1;

  input[0][0]       = '#';
  input[maxY][0]    = '#';
  input[0][maxX]    = '#';
  input[maxY][maxX] = '#';
  List<(PointOff, char)> toChange = new();
  for (int step = 0; step < steps; step++)
  {
    for (int y = 0; y < input[0].Length; y++)
    {
      for (int x = 0; x < input.Count; x++)
      {
        char c = input[y][x];
        (int onCount, int offCount) res = CountAdjacentLights(input, x, y);
        if (c == '#' && res.onCount != 2 && res.onCount != 3)
          toChange.Add((new PointOff(x, y), '.'));
        else if (c == '.' && res.onCount == 3)
          toChange.Add((new PointOff(x, y), '#'));
      }
    }
    foreach ((PointOff pos, char val) chg in toChange)
    {
      if ((chg.pos.y == 0 && chg.pos.x == 0) ||
          (chg.pos.y == maxY && chg.pos.x == 0) ||
          (chg.pos.y == 0 && chg.pos.x == maxX) ||
          (chg.pos.y == maxY && chg.pos.x == maxX))
        continue;

    input[chg.pos.y][chg.pos.x] = chg.val;
    }
    toChange.Clear();
    if (sample)
      PrintState(input);
  }

  (int onCount, int offCount) all = CountAllLights(input);
  Console.WriteLine($"Part 2 result is {all.onCount}");
}

record PointOff
{
  public PointOff(int x, int y)
  {
    this.x = x;
    this.y = y;
  }

  public int x;
  public int y;
}