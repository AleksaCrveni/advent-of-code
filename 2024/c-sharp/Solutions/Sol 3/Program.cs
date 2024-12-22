bool test = false;
if (args.Length > 0)
  if (args[0] == "Real")
    test = false;

string filename = test ? "input_sample.txt" : "input.txt";

string mem = File.ReadAllText(filename);
int len = mem.Length;
P1(mem);
P2(mem);

Console.ReadKey();

// unclean solution
void CheckIfMul(ReadOnlySpan<char> buffer, ref int currPosition, ref int sum)
{
  string num1 = "";
  string num2 = "";
  AdvancePos(ref currPosition);
  if (buffer[currPosition] != 'u') return;
  AdvancePos(ref currPosition);
  if (buffer[currPosition] != 'l') return;
  AdvancePos(ref currPosition);
  if (buffer[currPosition] != '(') return;
  AdvancePos(ref currPosition);
  while (char.IsDigit(buffer[currPosition]))
  {
    num1 += buffer[currPosition];
    AdvancePos(ref currPosition);
  }

  if (num1.Length == 0 || num1.Length > 3 || buffer[currPosition] != ',') return;

  AdvancePos(ref currPosition);
  while (char.IsDigit(buffer[currPosition]))
  {
    num2 += buffer[currPosition];
    AdvancePos(ref currPosition);
  }

  if (num2.Length == 0 || num2.Length > 3 || buffer[currPosition] != ')') return;

  sum += (Convert.ToInt32(num1) * Convert.ToInt32(num2));
}
void AdvancePos(ref int currentPosition)
{
  if (++currentPosition == len) throw new Exception("EOF");
}


void P1(string mem)
{
  ReadOnlySpan<char> buffer = mem.AsSpan();
  int sum = 0;
  for (int i = 0; i < buffer.Length; i++)
  {
    try
    {
      if (buffer[i] == 'm')
      {
        CheckIfMul(buffer, ref i, ref sum);
      }
    } catch
    {

    }
  }
  Console.WriteLine($"Result is {sum}");

  
}
void P2(string mem)
{
}

