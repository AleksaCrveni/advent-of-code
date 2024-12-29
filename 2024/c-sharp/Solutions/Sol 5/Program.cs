using System;
bool test = false;
if (args.Length > 0)
  if (args[0] == "Real")
    test = false;

string filename = test ? "input_sample.txt" : "input.txt";
string[] lines = File.ReadAllLines(filename);

(var badUpdates, var rules) = P1(lines);
P2(badUpdates, rules);
Console.ReadKey();

(List<List<int>>, Dictionary<int, List<int>>) P1(string[] lines)
{
  Dictionary<int, List<int>> rules = new Dictionary<int, List<int>>();
  int i = LoadRules(rules);
  List<List<int>> badUpdates = new List<List<int>>();
  int sum = 0;
  // processing updates
  for (++i; i < lines.Length; i++)
  {
    List<int> update = lines[i].Split(',').Select(x => int.Parse(x)).ToList();
    sum += ProcessUpdate(update, rules, badUpdates);
  }

  Console.WriteLine($"P1 result is {sum}");
  return (badUpdates, rules);
  
}

void P2(List<List<int>> badUpdates, Dictionary<int, List<int>> rules)
{
  int sum = 0;
  // processing updates
  
  foreach (var update in badUpdates)
  {
    sum += ReorderAndProcessUpdateList(update, rules);
  }
  Console.WriteLine($"P2 result is {sum}");
}

int ProcessUpdate(List<int> update, Dictionary<int, List<int>> rules, List<List<int>>? badUpdates = default(List<List<int>>))
{
  for (int i = 0; i < update.Count; i++)
  {
    int curr = update[i];
    // is not in rules
    if (!rules.ContainsKey(curr))
      continue;

    // check before
    for (int j = i -1; j >= 0; j--)
    {
      // curr number is after number in rule
      if (rules[curr].Contains(update[j]))
      {
        badUpdates?.Add(update);
        return 0;
      }
        
    }
  }

  return update[update.Count/2];
}
int ReorderAndProcessUpdateList(List<int> update, Dictionary<int, List<int>> rules)
{
  while (ProcessUpdate(update, rules) == 0)
  {
    for (int i = 0; i < update.Count; i++)
    {
      int curr = update[i];
      // is not in rules
      if (!rules.ContainsKey(curr))
        continue;

      for (int j = i - 1; j >= 0; j--)
      {
        // curr number is after number in rule
        if (rules[curr].Contains(update[j]))
        {
          int orig = update[i];
          update[i] = update[j];
          update[j] = orig;
        }

      }
    }
  }
  return update[update.Count / 2];

}

int LoadRules(Dictionary<int, List<int>> rules)
{
  int i = 0;
  // processing rules
  for (i = i; i < lines.Length; i++)
  {
    if (lines[i] == "")
      break;

    string[] split = lines[i].Split('|');
    int before = int.Parse(split[0]);
    int after = int.Parse(split[1]);
    if (rules.ContainsKey(before))
      rules[before].Add(after);
    else
      rules.Add(before, new List<int>() { after });
  }

  return i;
}

