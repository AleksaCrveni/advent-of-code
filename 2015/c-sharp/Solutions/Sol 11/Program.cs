bool sample = false;
string filename = "input.txt";
if (sample)
  filename = "input_sample.txt";

string input = "hepxcrrq";

Part1(input);
Part2(input);
void Part1(string input)
{
  bool validPwd = false;
  Span<char> pwd = new Span<char>(input.ToCharArray());
  while (!validPwd)
  {
    GenerateNextPassword(ref pwd);
    validPwd = VerifyPassword(ref pwd);
  }

  Console.WriteLine($"Part 1 result is {new string(pwd)}");
}

void Part2(string input)
{
  bool validPwd = false;
  Span<char> pwd = new Span<char>(input.ToCharArray());
  while (!validPwd)
  {
    GenerateNextPassword(ref pwd);
    validPwd = VerifyPassword(ref pwd);
  }

  validPwd = false;
  while (!validPwd)
  {
    GenerateNextPassword(ref pwd);
    validPwd = VerifyPassword(ref pwd);
  }

  Console.WriteLine($"Part 2 result is {new string(pwd)}");
}

void GenerateNextPassword(ref Span<char> pwd)
{
  char c;
  for (int i = pwd.Length -1; i >= 0; i--)
  {
    c = GetNextChar(pwd[i]);
    pwd[i] = c;
    if (c != 'a')
      break;
  }

}

bool VerifyPassword(ref Span<char> pwd)
{
  char c;
  bool threeInARow = false;
  for (int i =0; i < pwd.Length - 2; i++)
  {
    c = pwd[i];
    if (c == 'i' || c == 'l' || c == 'o')
      return false;

    threeInARow = !threeInARow ? pwd[i + 1] - c == 1 && pwd[i + 2] - pwd[i + 1] == 1
      : threeInARow;
  }
  // shortcut
  if (!threeInARow)
    return false;
  // check last 2 characters if they are i l o because we didnt iterate fully
  char secondToLast = pwd[pwd.Length - 2];
  if (secondToLast == 'i' || secondToLast == 'l' || secondToLast == 'o')
    return false;

  char last = pwd[pwd.Length - 1];
  if (last == 'i' || last == 'l' || last == 'o')
    return false;

  int pairCount = 0;
  for (int i = 0; i < pwd.Length - 1; i++)
  {
    if (pwd[i] == pwd[i + 1])
    {
      pairCount++;
      i++; // cant overlap so skip next
    }
  }

  if (pairCount < 2)
    return false;

  return true;
}

char GetNextChar(char c)
{
  int num = (++c);
  if (num > 'z')
    return 'a';
  return (char)num;
}