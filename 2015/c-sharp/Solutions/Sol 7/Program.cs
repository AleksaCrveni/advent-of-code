using System.Diagnostics;

bool sample = false;
string filename = "input.txt";
if (sample)
  filename = "input_sample.txt";

string[] input = File.ReadAllLines(filename);

Part1(input);
Part2(input);
void Part1(string[] input)
{
  Dictionary<string, Command> commands = new Dictionary<string, Command>();
  Command cmd = new Command();
  foreach (string line in input)
  {
    ParseCommand(line, ref cmd);
    commands.Add(cmd.Dest, cmd);
  }
  HashSet<string> lookupList = new();
  ushort res = GetFinalVal("a", commands, lookupList);
  Console.WriteLine($"Part one solution is {res}");
}

void Part2(string[] input)
{
  Dictionary<string, Command> commands = new Dictionary<string, Command>();
  Command cmd = new Command();
  foreach (string line in input)
  {
    ParseCommand(line, ref cmd);
    commands.Add(cmd.Dest, cmd);
  }
  HashSet<string> lookupList = new();
  ushort res = GetFinalVal("a", commands, lookupList);
  // I REALLY SHOULD HAVE JUST USED CLASS

  cmd = commands["b"];
  cmd.Op = Operation.SET;
  cmd.LeftOperand = res.ToString();
  commands["b"] = cmd;
  foreach (KeyValuePair<string, Command> kvp in commands)
  {
    Command c = kvp.Value;
    c.Computed = null;
    commands[kvp.Key] = c;
  }
  lookupList.Clear();
  res = GetFinalVal("a", commands, lookupList);
  Console.WriteLine($"Part two solution is {res}");
}


ushort GetFinalVal(string key, Dictionary<string, Command> commands, HashSet<string> lookupList )
{
  bool exists = lookupList.Add(key);
  
  // special case for AND wheere 1 AND can happen
  if (char.IsDigit(key[0]))
    return Convert.ToUInt16(key);
  
  // WRITE DEBUG LOOKUP TABLE
  Command cmd = commands[key];
  if (cmd.Computed != null)
    return (ushort)cmd.Computed;
  Debug.Assert(exists);
  ushort res = 0;
  switch (cmd.Op)
  {
    case Operation.SET:
      res = Convert.ToUInt16(cmd.LeftOperand);
      break;
    case Operation.COPY:
      res = GetFinalVal(cmd.LeftOperand, commands, lookupList);
      break;
    case Operation.NOT:
      res = (ushort)(~GetFinalVal(cmd.RightOperand, commands, lookupList));
      break;
    case Operation.AND:
      res = (ushort)(GetFinalVal(cmd.LeftOperand, commands, lookupList) & GetFinalVal(cmd.RightOperand, commands, lookupList));
      break;
    case Operation.OR:
      res = (ushort)(GetFinalVal(cmd.LeftOperand, commands, lookupList) | GetFinalVal(cmd.RightOperand, commands, lookupList));
      break;
    case Operation.LSHIFT:
      res = (ushort)(GetFinalVal(cmd.LeftOperand, commands, lookupList) << Convert.ToUInt16(cmd.RightOperand));
      break;
    case Operation.RSHIFT:
      res = (ushort)(GetFinalVal(cmd.LeftOperand, commands, lookupList) >> Convert.ToUInt16(cmd.RightOperand));
      break;
    default:
      throw new Exception("WTF");
  }
  cmd.Computed = res;
  commands[key] = cmd;
  return (ushort)cmd.Computed;
}

void ParseCommand(string line, ref Command cmd)
{
  string[] split = line.Split(' ');
  if (split.Length == 3)// either SET or COPY
  {
    if (char.IsDigit(split[0][0]))
      cmd.Op = Operation.SET;
    else
      cmd.Op = Operation.COPY;

    cmd.LeftOperand = split[0];
    cmd.Dest = split[2];
    return;
  }

  if (split.Length == 4)
  {
    cmd.Op = Operation.NOT;
    cmd.RightOperand = split[1];
    cmd.Dest = split[3];
    return;
  }

  cmd.Op = Enum.Parse<Operation>(split[1]);
  cmd.LeftOperand = split[0];
  cmd.RightOperand = split[2];
  cmd.Dest = split[4];
}
struct Command
{
  public Command() { }
  public Operation Op;
  public string LeftOperand;
  public string RightOperand;
  public string Dest;
  public ushort? Computed;
}

enum Operation
{
  SET, // left is number
  COPY, // left is another dest
  AND,
  OR,
  LSHIFT,
  RSHIFT,
  NOT // right is value
}