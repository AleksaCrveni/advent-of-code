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
  int raceDuration = 2503;
  Dictionary<string, DeerMovInfo> data = new Dictionary<string, DeerMovInfo>();
  foreach(string line in input)
  {
    DeerMovInfo dInfo = new DeerMovInfo();
    string[] split = line.Split(' ');
    string name = split[0];
    dInfo.Speed = Convert.ToInt32(split[3]);
    dInfo.MovingDuration = Convert.ToInt32(split[6]);
    dInfo.RestDuration = Convert.ToInt32(split[13]);
    data.Add(name, dInfo);
  }

  int max = Int32.MinValue;

  foreach(KeyValuePair<string, DeerMovInfo> kvp in data)
  {
    int d = CalculateDistanceTraveledPart1(kvp.Value, raceDuration);
    if (d > max)
      max = d;
  }

  Console.WriteLine($"Part 1 result is {max}"); ;
}


void Part2(string[] input)
{
  int raceDuration = 2503;
  Dictionary<string, DeerState> data = new Dictionary<string, DeerState>();
  foreach (string line in input)
  {
    DeerState state = new DeerState();
    DeerMovInfo dInfo = new DeerMovInfo();
    string[] split = line.Split(' ');
    string name = split[0];
    dInfo.Speed = Convert.ToInt32(split[3]);
    dInfo.MovingDuration = Convert.ToInt32(split[6]);
    dInfo.RestDuration = Convert.ToInt32(split[13]);
    state.Counter = dInfo.MovingDuration;
    state.Add = dInfo.Speed;
    state.Resting = false;
    state.movInfo = dInfo;
    state.Distance = 0;
    data.Add(name, state);
  }

  int maxDist = 0;
  for (int i = 1; i <= raceDuration; i++)
  {
    foreach (KeyValuePair<string, DeerState> kvp in data)
    {
      kvp.Value.Counter--;
      kvp.Value.Distance += kvp.Value.Add;
      if (kvp.Value.Counter == 0)
      {
        kvp.Value.Resting = !kvp.Value.Resting;
        kvp.Value.Counter = kvp.Value.Resting ? kvp.Value.movInfo.RestDuration : kvp.Value.movInfo.MovingDuration;
        kvp.Value.Add = kvp.Value.Resting ? 0 : kvp.Value.movInfo.Speed;
      }
      if (kvp.Value.Distance > maxDist)
        maxDist = kvp.Value.Distance;
    }

    // add rewards
    foreach (KeyValuePair<string, DeerState> kvp in data)
    {
      Debug.Assert(kvp.Value.Distance <= maxDist);
      if (kvp.Value.Distance == maxDist)
        kvp.Value.Points++;
    }
  }

  maxDist = 0;// cant reuse because dsistance might be incremented on last second
  foreach (KeyValuePair<string, DeerState> kvp in data)
  {

    if (kvp.Value.Points > maxDist)
      maxDist = kvp.Value.Points;
  }

  Console.WriteLine($"Part 2 result is {maxDist}");
}


int CalculateDistanceTraveledPart1(DeerMovInfo info, int raceDuration)
{
  int dist = 0;
  int cycle = info.MovingDuration + info.RestDuration;
  int numOfCycles = raceDuration / cycle;
  dist = numOfCycles * (info.MovingDuration * info.Speed);

  int leftOver = raceDuration % cycle;
  // get leftover moving duration
  int toMove = leftOver <= info.MovingDuration ? leftOver : info.MovingDuration;

  dist += toMove * info.Speed;
  return dist;
}

class DeerState
{
  public DeerMovInfo movInfo;
  public int Distance = 0;
  public int Counter;
  public int Add = 0;
  public bool Resting = false;
  public int Points = 0;
}
class DeerMovInfo
{
  public int Speed;
  public int MovingDuration;
  public int RestDuration;
}