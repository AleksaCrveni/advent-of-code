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
  // from, (to, dist)
  Dictionary<string, List<(string city, int distance)>> dict = new();
  // juse use this b ecause i dont want to deal with KeyCollection from dict.Keys
  List<string> cities = new();
  // Load distances both ways
  foreach (string line in input)
  {
    string[] split = line.Split(' ');
    string from = split[0];
    string to = split[2];
    int dist = Convert.ToInt32(split[4]);
    if (!dict.ContainsKey(from))
    {
      List<(string city, int distance)> list = new();
      list.Add((to, dist));
      dict.Add(from, list);
    }
    else
    {
      dict[from].Add((to, dist));
    }

    if (!dict.ContainsKey(to))
    {
      List<(string city, int distance)> list = new();
      list.Add((from, dist));
      dict.Add(to, list);
    }
    else
    {
      dict[to].Add((from, dist));
    }

    if (!cities.Contains(from))
      cities.Add(from);
    if (!cities.Contains(to))
      cities.Add(to);
  }

  int minDistance = int.MaxValue;
  int count = 0;

  Permutations.ForAllPermutation(cities.ToArray(), (vals) =>
  {
    int dist = GetDistanceForList(vals, dict);
    if (dist < minDistance)
      minDistance = dist;
    count++;
    return false;
  });

  Console.WriteLine($"Part 1 result is {minDistance}");
}

void Part2(string[] input)
{
  // from, (to, dist)
  Dictionary<string, List<(string city, int distance)>> dict = new();
  // juse use this b ecause i dont want to deal with KeyCollection from dict.Keys
  List<string> cities = new();
  // Load distances both ways
  foreach (string line in input)
  {
    string[] split = line.Split(' ');
    string from = split[0];
    string to = split[2];
    int dist = Convert.ToInt32(split[4]);
    if (!dict.ContainsKey(from))
    {
      List<(string city, int distance)> list = new();
      list.Add((to, dist));
      dict.Add(from, list);
    }
    else
    {
      dict[from].Add((to, dist));
    }

    if (!dict.ContainsKey(to))
    {
      List<(string city, int distance)> list = new();
      list.Add((from, dist));
      dict.Add(to, list);
    }
    else
    {
      dict[to].Add((from, dist));
    }

    if (!cities.Contains(from))
      cities.Add(from);
    if (!cities.Contains(to))
      cities.Add(to);
  }

  int maxDistance = int.MinValue;
  int count = 0;

  Permutations.ForAllPermutation(cities.ToArray(), (vals) =>
  {
    int dist = GetDistanceForList(vals, dict);
    if (dist > maxDistance)
      maxDistance = dist;
    count++;
    return false;
  });

  Console.WriteLine($"Part 2 result is {maxDistance}");
}


int GetDistanceForList(string[] cities, Dictionary<string, List<(string city, int distance)>> distanceData)
{
  string start;
  string dest;
  int totalDistance = 0;
  for (int i =0; i < cities.Length - 1; i++)
  {
    start = cities[i];
    dest = cities[i + 1];

    foreach((string city, int distance) in distanceData[start])
    {
      if (city == dest)
      {
        totalDistance += distance;
        break;
      }
    }
  }

  return totalDistance;
}