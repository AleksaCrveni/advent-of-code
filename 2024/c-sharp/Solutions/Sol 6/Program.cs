using System.Dynamic;

bool test = false;
if (args.Length > 0)
  if (args[0] == "Real")
    test = false;

string filename = test ? "input_sample.txt" : "input.txt";
string[] lines = File.ReadAllLines(filename);
int n = lines[0].Length;
int m = lines.Length;
char[,] grid = new char[n,m];
for (int y = 0; y < m; y++)
{
  for (int x = 0; x < n; x++)
  {
    grid[y, x] = lines[y][x];
  }
}

P1(grid);
P2(grid);

Console.ReadKey();
void P1(char[,] grid)
{
  HashSet<(int, int)> traveledPositions = new HashSet<(int, int)>();
  DIR dir = DIR.TOP;
  (int i , int j) = GetStartingPosition(grid);
  traveledPositions.Add((i, j));
  bool stuck = false;
  bool justChanged = false;
  while (!stuck)
  {
    (i, j) = MoveNext(i, j, ref dir, grid);
    if (i == -1 && j == -1)
      break;

    if (!traveledPositions.Contains((i, j)))
      traveledPositions.Add((i, j));
  }
  Console.WriteLine($"P1 Result is {traveledPositions.Count -1}");
}
// Returns position after moving
(int, int) MoveNext(int i , int j, ref DIR dir, char[,] grid)
{
  (int ii, int jj) = dir switch
  {
    DIR.TOP => (i - 1, j),
    DIR.RIGHT => (i, j + 1),
    DIR.BOTTOM => (i + 1, j),
    DIR.LEFT => (i, j - 1),
  };

  if (ii < 0 || ii >= n || jj < 0 || jj >= m)
    ChangeDirection(ref dir);
  else if (grid[ii, jj] == '#')
    ChangeDirection(ref dir);
  else
    return (ii, jj);


  (ii, jj) = dir switch
  {
    DIR.TOP => (i - 1, j),
    DIR.RIGHT => (i, j + 1),
    DIR.BOTTOM => (i + 1, j),
    DIR.LEFT => (i, j - 1),
  };
  if (ii < 0 || ii >= n || jj < 0 || jj >= m)
    return (-1, -1);
  else if (grid[ii, jj] == '#')
    return (-1, -1);
  else
    return (ii, jj);
}

void P2(char[,] grid)
{

}
void ChangeDirection(ref DIR dir)
{
  dir = dir switch
  {
    DIR.TOP => DIR.RIGHT,
    DIR.RIGHT => DIR.BOTTOM,
    DIR.BOTTOM => DIR.LEFT,
    DIR.LEFT => DIR.TOP
  } ;
}
(int i, int j) GetStartingPosition(char[,] grid)
{
  for (int i = 0; i < m; i++)
  {
    for (int j =0; j < n; j++)
    {
      if (grid[i, j] == '^')
        return (i, j);
    }
  }

  return (-1, -1);
}


enum DIR
{
  TOP,
  RIGHT,
  BOTTOM,
  LEFT
}

