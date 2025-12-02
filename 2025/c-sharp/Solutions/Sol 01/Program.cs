
bool sample = false;
string path = "input.txt";
if (sample)
  path = "input_sample.txt";

string[] input = File.ReadAllLines(path);

Part1(input);
Part2(input);


void Part1(string[] input)
{
  ReadOnlySpan<char> span;
  char side;
  int valToMove;
  int val = 50;
  int counter = 0;
  foreach(string line in input)
  {
    span = line.AsSpan();
    side = span[0];
    valToMove = Convert.ToInt32(span.Slice(1).ToString());
    if (side == 'L')
    {
      val -= valToMove;
      if (val < 0)
      {
        // i.e -1 = 100 -1 == 99
        while (val < 0)
          val = 100 + val;
      }
    }
    else
    {
      val += valToMove;
      if (val > 99)
      {
        while (val > 99)
          val = val - 100;
      }
    }

    if (val == 0)
      counter++;
  }
  Console.WriteLine($"Part 1 Result is {counter}");
}

void Part2(string[] input)
{
  ReadOnlySpan<char> span;
  char side;
  int valToMove;
  int val = 50;
  int counter = 0;
  int add = 0;
  List<float> vals = new List<float>();
  int i = 0;
  foreach (string line in input)
  {
    span = line.AsSpan();
    side = span[0];
    valToMove = Convert.ToInt32(span.Slice(1).ToString());
    counter += valToMove % 100;
    valToMove %= 100;
    if (side == 'L')
      valToMove = -valToMove;
    var newVal = val + valToMove;
    // looked up
    if (newVal > 99)
    {
      newVal -= 100;
      counter++;
    }
    else if (newVal == 0)
    {
      counter++;
    }
    else if (newVal < 0 && val != 0)
    {
      counter++;
      newVal += 100;
    }
    val = (newVal + 100) % 100;
    i++;
  }

  File.WriteAllLines("out.txt", vals.Select(x => x.ToString()));
  Console.WriteLine($"Part 2 Result is {counter}");
}