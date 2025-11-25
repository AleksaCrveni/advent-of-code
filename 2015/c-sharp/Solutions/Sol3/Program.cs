using Sol3;
using System.Diagnostics.CodeAnalysis;

bool sample = false;
string filename = "input.txt";
if (sample)
  filename = "input_sample.txt";

string input = File.ReadAllText(filename);

Part1(input);
Part2(input);

void Part1(string input)
{
  (int x, int y) currPos = (0, 0);
  ValueTupleComparer comparer = new ValueTupleComparer();
  Dictionary<ValueTuple<int,int>, int> houses = new Dictionary<(int x, int y), int>(comparer);
  houses.Add(currPos, 1);
  foreach (char c in input)
  {
    if (c == '^')
    {
      currPos.y += 1;  
    }
    else if (c == '<')
    {
      currPos.x -= 1;
    }
    else if (c == '>')
    {
      currPos.x += 1;
    }
    else if (c == 'v')
    {
      currPos.y -= 1;
    }

    if (houses.ContainsKey(currPos))
      houses[currPos] += 1;
    else
      houses.Add(currPos, 1);
  }

  Console.WriteLine($"Part 1 result is {houses.Count}");
}

void Part2(string input)
{
  // could have just used class instead of sturct here so i coulkd assign reference to one obj adn not have to do if each time but w/e
  (int x, int y) santaPos = (0, 0);
  (int x, int y) roboSantaPos = (0, 0);
  ValueTupleComparer comparer = new ValueTupleComparer();
  Dictionary<ValueTuple<int, int>, int> houses = new Dictionary<(int x, int y), int>(comparer);
  houses.Add(santaPos, 1);
  int step = 0;
  foreach (char c in input)
  {
    if (c == '^')
    {
      if (step % 2 == 0)
        santaPos.y += 1;
      else
        roboSantaPos.y += 1;
    }
    else if (c == '<')
    {
      if (step % 2 == 0)
        santaPos.x -= 1;
      else
        roboSantaPos.x -= 1;
    }
    else if (c == '>')
    {
      if (step % 2 == 0)
        santaPos.x += 1;
      else
        roboSantaPos.x += 1;
    }
    else if (c == 'v')
    {
      if (step % 2 == 0)
        santaPos.y -= 1;
      else
        roboSantaPos.y -= 1;
    }

    if (step % 2 == 0)
    {
      if (houses.ContainsKey(santaPos))
        houses[santaPos] += 1;
      else
        houses.Add(santaPos, 1);
    }
    else
    {
      if (houses.ContainsKey(roboSantaPos))
        houses[roboSantaPos] += 1;
      else
        houses.Add(roboSantaPos, 1);
    }
    step++;
  }

  Console.WriteLine($"Part 2 result is {houses.Count}");
}
