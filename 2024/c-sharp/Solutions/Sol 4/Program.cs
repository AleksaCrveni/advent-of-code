using System;
using System.Security.Cryptography;

bool test = false;
if (args.Length > 0)
  if (args[0] == "Real")
    test = false;

string filename = test ? "input_sample.txt" : "input.txt";
string[] mem = File.ReadAllLines(filename);
int n = mem[0].Length;
int m = mem.Length;
char[,] grid = new char[mem[0].Length, mem.Length];

for (int i = 0; i < mem.Length; i++)
{
  for (int j = 0; j < mem[i].Length; j++)
  {
    grid[i, j] = mem[i][j];
  }
}

P1(grid);
P2(grid);
Console.ReadKey();

void P1(char[,] grid)
{
  List<(int, int)> directions = new List<(int, int)>();
 
  for (int i = -1; i < 2; i++)
  {
    for (int j = -1; j < 2; j++)
    {
      if (i != 0 ||  j != 0)
        directions.Add((i, j));
    }
  }
  int sum = 0;

  for (int i = 0; i < n; i++)
  {
    for (int j = 0; j < m; j++)
    {
      foreach((int,int) d in directions)
      {
        sum += Convert.ToInt32(IsXMAS(i, j, d));
      }
    }
  }

  Console.WriteLine($"P1 sum is {sum}");

}

void P2(char[,] grid)
{
  int sum = 0;
  for (int i = 0; i < n; i++)
  {
    for (int j = 0; j < m; j++)
    {
      sum += Convert.ToInt32(IsX_MAS(i, j));
    }
  }
  Console.WriteLine($"P2 sum is {sum}");
}

bool IsX_MAS(int i , int j)
{
  if (i - 1 < 0 || i + 1 >= n || j - 1 < 0 || j + 1 >= m)
    return false;

  if (grid[i, j] != 'A')
    return false;

  string diag1 = $"{grid[i - 1, j - 1]}{grid[i + 1, j + 1]}";
  string diag2 = $"{grid[i + 1, j - 1]}{grid[i - 1, j + 1]}";

  return (diag1.Contains("SM") || diag1.Contains("MS")) && (diag2.Contains("SM") || diag2.Contains("MS"));
}

bool IsXMAS(int i, int j, (int dx, int dy) d)
{
  int dx = d.dx;
  int dy = d.dy;
  string target = "XMAS";
  for (int ind = 0; ind < target.Length; ind++)
  {
    int ii = i + ind * dx;
    int jj = j + ind * dy;

    if (ii < 0 || ii >= n || jj < 0 || jj >= m)
      return false;

    char gc = grid[ii, jj];
    char tc = target[ind];
    //Console.WriteLine($"Grid char: {gc}. Target char {tc}");
    if (grid[ii,jj] != target[ind])
      return false;
  }
  return true;
}
