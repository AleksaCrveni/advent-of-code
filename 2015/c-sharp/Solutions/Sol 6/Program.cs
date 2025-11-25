bool sample = false;
string filename = "input.txt";
if (sample)
  filename = "input_sample.txt";

string[] input = File.ReadAllLines(filename);

Part1(input);
Part2(input);
void Part1(string[] input)
{
  Command cmd = new Command();
  int[,] matrix = new int[1000, 1000];
 // input = new string[] { "turn on 0,0 through 999,999" };
  foreach (string line in input)
  {
    ParseNextCommand(line, ref cmd);
    for (int x = cmd.StartPos.x; x <= cmd.EndPos.x; x++)
    {
      for (int y = cmd.StartPos.y; y <= cmd.EndPos.y; y++)
      {
        if (cmd.CmdType == CmdType.TOGGLE)
        {
          matrix[x, y] = matrix[x, y] == 0 ? 1 : 0;
        } else
        {
          matrix[x, y] = cmd.CmdType == CmdType.TURN_ON ? 1 : 0;
        }
      }
    }
  }

  int sum = 0;
  for (int i =0; i < matrix.GetLength(0); i++)
  {
    for (int j = 0; j < matrix.GetLength(1); j++)
    {
      sum += matrix[i, j];
    }
  }
  Console.WriteLine($"Part One result is {sum}");
}



void Part2(string[] input)
{
  Command cmd = new Command();
  int[,] matrix = new int[1000, 1000];
  // input = new string[] { "turn on 0,0 through 999,999" };
  foreach (string line in input)
  {
    ParseNextCommand(line, ref cmd);
    for (int x = cmd.StartPos.x; x <= cmd.EndPos.x; x++)
    {
      for (int y = cmd.StartPos.y; y <= cmd.EndPos.y; y++)
      {
        if (cmd.CmdType == CmdType.TOGGLE)
        {
          matrix[x, y] += 2;
        }
        else
        {
          if (cmd.CmdType == CmdType.TURN_ON)
            matrix[x, y] += 1;
          else
          {
            matrix[x, y] -= matrix[x, y] == 0 ? 0 : 1;
          }
        }
      }
    }
  }

  int sum = 0;
  for (int i = 0; i < matrix.GetLength(0); i++)
  {
    for (int j = 0; j < matrix.GetLength(1); j++)
    {
      sum += matrix[i, j];
    }
  }
  Console.WriteLine($"Part 2 result is {sum}");
}

void ParseNextCommand(string line, ref Command cmd)
{
  string[] split = line.Split(' ');
  int pos = 0;
  if (split[pos++] == "toggle")
  {
    cmd.CmdType = CmdType.TOGGLE;
  } else
  {
    if (split[pos++] == "off")
      cmd.CmdType = CmdType.TURN_OFF;
    else
      cmd.CmdType = CmdType.TURN_ON;
  }
  string[] nums = split[pos++].Split(',');
  (int x, int y) coord = (Convert.ToInt32(nums[0]), Convert.ToInt32(nums[1]));
  pos++;
  cmd.StartPos = coord;
  nums = split[pos++].Split(',');
  coord = (Convert.ToInt32(nums[0]), Convert.ToInt32(nums[1]));
  cmd.EndPos = coord;
}


struct Command
{
  public CmdType CmdType;
  public (int x, int y) StartPos;
  public (int x, int y) EndPos;
}


enum CmdType
{
  TOGGLE,
  TURN_OFF,
  TURN_ON
}

