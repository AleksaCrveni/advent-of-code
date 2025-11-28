using Sol_9;

bool sample = false;
string filename = "input.txt";
if (sample)
  filename = "input_sample.txt";

string[] input = File.ReadAllLines(filename);

Part1(input);
Part2(input);
void Part1(string[] input)
{
  Dictionary<string, Dictionary<string, int>> data = new();
  string mainName;
  string nextTo;
  string happy; // lose/gain
  int value = 0;
  List<string> players = new();
  foreach (string line in input)
  {
    string[] split = line.Trim('.').Split(' ');
    mainName = split[0];
    nextTo = split[10];
    happy = split[2];
    value = Convert.ToInt32(split[3]);

    if (!data.ContainsKey(mainName))
    {
      data.Add(mainName, new Dictionary<string, int>());
    }

    Dictionary<string, int> inData = data[mainName];
    value *= happy == "gain" ? 1 : -1;
    inData.Add(nextTo, value);

    if (!players.Contains(mainName))
      players.Add(mainName);

  }
  int max = int.MinValue;
  Permutations.ForAllPermutation(players.ToArray(), (vals) =>
  {

    int res = CalculateHappines(vals, data);
    if (res > max)
      max = res;
    return false;
  });

  Console.WriteLine($"Part 1 result is {max}");
}
int CalculateHappines(string[] vals, Dictionary<string, Dictionary<string, int>> data)
{
  int sum = 0;
  sum += data[vals[0]][vals[1]] + data[vals[0]][vals[vals.Length - 1]];
  for (int i = 1; i < vals.Length - 1; i++)
  {
    sum += data[vals[i]][vals[i + 1]] + data[vals[i]][vals[i - 1]];
  }

  sum += data[vals[vals.Length - 1]][vals[0]] + data[vals[vals.Length - 1]][vals[vals.Length - 2]];
  return sum;
}


void Part2(string[] input)
{
  Dictionary<string, Dictionary<string, int>> data = new();
  string mainName;
  string nextTo;
  string happy; // lose/gain
  int value = 0;
  List<string> players = new();
  Dictionary<string, int> inData = new Dictionary<string, int>();
  foreach (string line in input)
  {
    string[] split = line.Trim('.').Split(' ');
    mainName = split[0];
    nextTo = split[10];
    happy = split[2];
    value = Convert.ToInt32(split[3]);

    if (!data.ContainsKey(mainName))
    {
      data.Add(mainName, new Dictionary<string, int>());
    }

    inData = data[mainName];
    value *= happy == "gain" ? 1 : -1;
    inData.Add(nextTo, value);

    if (!players.Contains(mainName))
      players.Add(mainName);
  }
  string myName = "Aleksa";
  players.Add(myName);

 
  // add from others POV
  foreach(KeyValuePair<string, Dictionary<string, int>> kvp in data)
  {
    kvp.Value.Add(myName, 0);
  }

  // add from my POV
  inData = new Dictionary<string, int>();

  foreach (string player in players)
    inData.Add(player, 0);

  data.Add(myName, inData);

  int max = int.MinValue;
  Permutations.ForAllPermutation(players.ToArray(), (vals) =>
  {
    int res = CalculateHappines(vals, data);
    if (res > max)
      max = res;
    return false;
  });

  Console.WriteLine($"Part 2 result is {max}");
}
