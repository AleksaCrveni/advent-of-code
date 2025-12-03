using System.Globalization;
using System.Text;

bool sample = false;
string path = "input.txt";
if (sample)
  path = "input_sample.txt";

string[] input = File.ReadAllText(path).Split('\n');

Part1(input);
Part2(input);


void Part1(string[] input)
{
  int sum = 0;
  foreach (string b in input)
  {
    
    string bank = b.Trim('\r');
    char c1 = '0';
    char c2 = '0';
    int ind = 0;
    for (int i = 0; i < bank.Length -1; i++)
    {
      if (bank[i] > c1)
      {
        c1 = bank[i];
        ind = i;
      }
    }

    for (int i = ind + 1; i < bank.Length; i++)
    {
      if (bank[i] > c2)
        c2 = bank[i];
    }

    sum += CharUnicodeInfo.GetDecimalDigitValue(c1) * 10 + CharUnicodeInfo.GetDecimalDigitValue(c2);
  }
  Console.WriteLine($"Part 1 result is {sum}");
}
void Part2(string[] input)
{
  ulong sum = 0;
  StringBuilder sb = new StringBuilder();
  foreach (string b in input)
  {

    ReadOnlySpan<char> bank = b.Trim('\r').AsSpan();
    char c1 = '0';
    int ind = 0;
    int toFind = 12;
    for (int i = 0; i < bank.Length - toFind; i++)
    {
      if (bank[i] > c1)
      {
        c1 = bank[i];
        ind = i;
      }
    }
    sb.Append(c1);
    toFind--;
    int remainder = bank.Length - toFind - ind - 1;
    for (int j = 1; j < 12; j++)
    {
      c1 = '0';
      int prevInd = ind;
      int prevRemainder = remainder;
      int i = 0;
      for (i = ind + 1; i < bank.Length; i++)
      {
        
        if (bank[i] > c1)
        {
          c1 = bank[i];
          ind = i;
        }
        if (remainder-- == 0 && toFind + i >= bank.Length)
          break;
      }

      if (remainder < 0)
        remainder = 0;
      remainder += prevRemainder - (ind - prevInd -1);
      if (remainder == 0 && toFind == 0)
      {
        sb.Append(bank.Slice(ind));
        break;
      }
      sb.Append(c1);
      toFind--;
    }

    sum += Convert.ToUInt64(sb.ToString());
    sb.Clear();
  }
  Console.WriteLine($"Part 1 result is {sum}");
}
