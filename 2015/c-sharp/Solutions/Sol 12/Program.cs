
using Sol_12;
using System.Globalization;

bool sample = false;
string filename = "input.txt";
if (sample)
  filename = "input_sample.txt";

byte[] input = File.ReadAllBytes(filename);

Part1(input);
Part2(input);
void Part1(byte[] input)
{
  int sum = 0;
  int multiplier = 1;
  ReadOnlySpan<byte> buffer = input.AsSpan();
  int pos = 0;
  int count = 0;
  while (true)
  {
    MoveToNum(ref buffer, ref pos);
    int? num = GetNextInt(ref buffer, ref pos);
    count++;
    if (num == null)
      break;
    sum += (int)num;
  }

  Console.WriteLine($"Part 1 result is {sum}");
  
}

void Part2(byte[] input)
{
  Token root = FakeJSONParser.Parse(input);
  int sum = 0;
  Part2Calc(root, ref sum);
  Console.WriteLine($"Part 2 result is {sum}");

}

bool Part2Calc(Token t, ref int sum)
{
  if (t.Type == TokenType.INT)
    sum += t.IntLit ?? 0;
  else if (t.Type == TokenType.ARRAY)
  {
    for (int i = 0; i < t.Array!.Count; i++)
      Part2Calc(t.Array[i], ref sum);
  }
  else if (t.Type == TokenType.OBJECT)
  {
    int accum = 0;
    bool isRed = false;
    foreach (KeyValuePair<string, Token> kvp in t.Dictionary!)
    {
      isRed = Part2Calc(kvp.Value, ref accum);
      if (isRed)
      {
        accum = 0;
        break;
      }
    }
    sum += accum;
  }
  else if (t.Type == TokenType.STRING && t.StringLit! == "red")
    return true;
  return false;
}


int? GetNextInt(ref ReadOnlySpan<byte> buffer, ref int pos)
{
  int num = 0;
  int multiplier = 1;
  byte c;

  // EOF
  if (pos == buffer.Length)
    return null;

  if (buffer[pos] == '-')
  {
    multiplier = -1;
    pos++;
  }
    

  for (; pos < buffer.Length; pos++)
  {
    c = buffer[pos];
    if (c < '0' || c > '9')
      break;

    num = num * 10 + CharUnicodeInfo.GetDigitValue((char)c);
  }

  return num * multiplier;
}

void MoveToNum(ref ReadOnlySpan<byte> buffer, ref int pos)
{
  // skip to next number
  byte c;
  for (; pos < buffer.Length; pos++)
  {
    c = buffer[pos];
    if ((c >= '0' && c <= '9') || c == '-')
      break;
  }
}