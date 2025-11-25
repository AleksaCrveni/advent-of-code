using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

bool sample = false;
string filename = "input.txt";
if (sample)
  filename = "input_sample.txt";

string[] input = File.ReadAllLines(filename);

Part1(input);
Part2(input);
void Part1(string[] input)
{
  char[] vowels = ['a', 'e', 'i', 'o', 'u'];
  int res = 0;
  foreach (string str in input)
  {
    int vowelCount = 0;
    bool hasDoubleLetter = false;
    bool hasForeignSubsStrings = false;
    for (int i = 0; i < str.Length -1; i++)
    {
      char c = str[i];
      char nc = str[i + 1];
      if (vowels.Contains(c))
        vowelCount++;
      if (c == nc)
        hasDoubleLetter = true;
      else
      {
        if ((c == 'a' && nc == 'b') ||
            (c == 'c' && nc == 'd') ||
            (c == 'p' && nc == 'q') ||
            (c == 'x' && nc == 'y'))
        {
          hasForeignSubsStrings = true;
          break;
        }
      }
    }

    // check if last is vowel because we dont iterate wholes tring to avoid index out of bounds without checkinbg
    if (vowels.Contains(str[str.Length - 1]))
      vowelCount++;


    if (vowelCount >= 3 && hasDoubleLetter && !hasForeignSubsStrings)
      res++;
  }
  Console.WriteLine($"Part1 result is {res}");
}

void Part2(string[] input)
{
  int res = 0;
  //put = new string[] { "crxz`adpvdaccrsm" };
  int dp = 0;
  List<string> dbg = new();
  foreach (string str in input)
  {
    bool hasRepeat = false;
    bool hasInBetween = false;
    ReadOnlySpan<char> span = str.AsSpan();
    for (int i = 0; i < span.Length -1; i++)
    {
      ReadOnlySpan<char> sub = span.Slice(i, 2);
      for (int j = i +2; j < span.Length -1; j++)
      {
        if (sub[0] == span[j] && sub[1] == span[j + 1])
        {
          hasRepeat = true;
          break;
        }
      }
      if (hasRepeat)
        break;
    }

    for (int i =0; i < span.Length - 2; i++)
    {
      if (span[i] == span[i + 2])
      {
        hasInBetween = true;
        break;
      }
    }

    if (hasInBetween && hasRepeat)
      res++;
  }


  File.WriteAllLines("dbg.txt", dbg);
  Console.WriteLine($"Part2 result is {res}");
  Console.WriteLine(dp);
}