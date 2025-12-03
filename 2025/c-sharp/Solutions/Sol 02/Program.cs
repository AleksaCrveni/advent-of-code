
using Sol_02;

bool sample = false;
string path = "input.txt";
if (sample)
  path = "input_sample.txt";

string[] input = File.ReadAllText(path).Split(',');

Part1(input);
Part2(input);


void Part1(string[] input)
{
  long sum = 0;
 
  foreach(string l in input)
  {
    string[] spl = l.Split('-');
    long startId = Convert.ToInt64(spl[0]);
    long endId = Convert.ToInt64(spl[1]);
    for (long i = startId; i <= endId; i++)
    {
      string str = i.ToString();
      if (str.Length % 2 == 1)
        continue;
      int x, y = str.Length / 2;
      bool valid = false;
      for (x = 0; x < str.Length / 2; x++)
      {
        if (str[x] != str[y])
        {
          valid = true;
          break;
        }
         
        y++;
      }

      if (!valid)
        sum += i;
    }
  }

  Console.WriteLine($"Part1 result is {sum}");
}
void Part2(string[] input)
{
  long sum = 0;
  long[] okResults = [11, 22, 99, 111, 999, 1010, 1188511885, 222222, 446446, 38593859, 565656, 824824824, 2121212121];
  foreach (string l in input)
  {
    string[] spl = l.Split('-');
    long startId = Convert.ToInt64(spl[0]);
    long endId = Convert.ToInt64(spl[1]);

    for (long num = startId; num <= endId; num++)
    {
      ReadOnlySpan<char> str = num.ToString().AsSpan();
      if (str.Length == 1)
        continue;

      // up to len/2 becase it has to consists only OF and and least 2
      
      int numOfPairs = 0;
      if (str.Length == 2)
      {
        if (str[0] == str[1])
          sum += num;
        continue;
      }
      if (str.Length == 3)
      {
        if (str[0] == str[1] && str[0] == str[2])
          sum += num;
        continue;
      }

      for (int i = 1; i<= str.Length / 2; i++)
      {
        ReadOnlySpan<char> left = str.Slice(0, i);
        bool valid = false;
        int j = i;
        // shortcut if we can't fit repeted substring
        if (str.Length % left.Length != 0)
          continue;
        for (; j < str.Length; j += left.Length)
        {
          ReadOnlySpan<char> right = str.Slice(j, left.Length);
          if (!left.Equals<char>(ref right))
          {
            valid = true;
            break;
          }
        }

        if (!valid)
        {
          sum += num;
          break;
        }
      } 
    }
  }

  Console.WriteLine($"Part2 result is {sum}");
}


