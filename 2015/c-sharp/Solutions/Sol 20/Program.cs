bool sample = true;
string filename = "input.txt";
if (sample)
  filename = "input_sample.txt";

string input = File.ReadAllText(filename);

Part1();
Part2();
void Part1()
{
  int N = 34_000_000;
  if (sample)
    N = 130;
  int[] houses = new int[N / 10];
  for (int i = 1; i < N / 10; i++)
  {
    for (int j = i; j < N / 10; j++)
      houses[j] += i * 10;
  }

  int res = 0;
  for (int i =0; i < houses.Length; i++)
  {
    if (houses[i] >= N)
    {
      res = i;
    }
  }

  Console.WriteLine($"Part 1 result is {res}"); ;
}
void Part2()
{

}