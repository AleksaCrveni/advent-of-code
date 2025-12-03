using Sol_15;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.InteropServices.Marshalling;
using System.Text;

bool sample = false;
string filename = "input.txt";
if (sample)
  filename = "input_sample.txt";

string[] input = File.ReadAllLines(filename);

int Combinations(int target, int numOfSets, List<string> ingredients, Dictionary<string, Teaspoon> data)
{
  // target 3 numOfsets 3
  // 0, 0, 3 #0 OK
  // 0, 1, 2 #1 OK
  // 1, 1, 1 #2 OK
  // 1, 2, 0 #3 NOT OK

  // t4 sets 4
  // 4, 0, 0, 0
  // 3, 1, 0, 0
  // 2, 2, 0, 0
  // 2, 1, 1, 0
  // 1, 1, 1, 1
  // 0, 1, 1, 2

  int[] set = new int[numOfSets];
  ReadOnlySpan<int> span = set.AsSpan();
  int last = numOfSets - 1;
  set[last] = target;
  List<Dictionary<int, int>> seenBeforeList = new();
  bool same = false;
  int[] permSet = new int[numOfSets];
  int max = int.MinValue;
  Permute(set, ingredients, data, ref max);
  int count = 0;
  while (set[last] != 0)
  {
    if (last == 1)
    {
      set[1]--;
      set[0]++;
    } else
    {
      for (int i = 1; i < last; i++)
      {
        CombineNext(set, i);
        same = SeenBefore(set, seenBeforeList);
        if (!same)
          break;
      }
    }



    int[] test = new int[numOfSets];
    // do permuation
    if (!same)
    {
      count++;
      set.CopyTo(permSet, 0);
      Permutations.ForAllPermutation(permSet, (vals) =>
      {
        bool s = true;
        for(int i =0; i < numOfSets; i++)
        {
          if (vals[i] != test[i])
            s = false;
        }
        //Debug.Assert(s == false);
        vals.CopyTo(test,0);
        return Permute(vals, ingredients, data, ref max);
      });
    }
    
  }
  return max;
}


bool Permute(int[] vals, List<string> ingridients, Dictionary<string, Teaspoon> data, ref int max)
{   
  int capacity = 0;
  int durability = 0;
  int flavor = 0;
  int texture = 0;
  for (int i = 0; i < vals.Length; i++)
  {
    int multi = vals[i];
    string ingridient = ingridients[i];
    capacity   += (multi * data[ingridient].Capacity);
    durability += (multi * data[ingridient].Durability);
    flavor     += (multi * data[ingridient].Flavor);
    texture    +=  (multi * data[ingridient].Texture);
  }
  if (File.Exists("out.txt"))
  {
    var fs = File.Open("out.txt", FileMode.Append);
    fs.Write(Encoding.Default.GetBytes($"\n{vals[0]} {vals[1]} {vals[2]} {vals[3]}"));
    fs.Close();
  }
  else
  {
    var fs = File.Open("out.txt", FileMode.Create);
    fs.Write(Encoding.Default.GetBytes($"{vals[0]} {vals[1]} {vals[2]} {vals[3]}"));
    fs.Close();

  }

    capacity = capacity > 0 ? capacity : 0;
  durability = durability > 0 ? durability : 0;
  flavor     = flavor > 0 ? flavor : 0;
  texture    = texture > 0 ? texture : 0;

  int sum = capacity * durability * flavor * texture;
  if (sum > max)
    max = sum;
  return false;
}
void CombineNext(int[] arr, int indexAt)
{
  for (int i = indexAt; i < arr.Length; i++)
  {
    if (arr[i] != 0)
    {
      arr[i - 1]++;
      arr[i]--;
      return;
    }
  }
}
// false if its new combination, true if its not new combination
bool SeenBefore(int[] currSet, List<Dictionary<int, int>> list)
{
  Dictionary<int, int> currSetDict = new();
  for (int i = 0; i < currSet.Length; i++)
  {
    if (currSetDict.ContainsKey(currSet[i]))
      currSetDict[currSet[i]]++;
    else
      currSetDict.Add(currSet[i], 1);
  }
  // shortcut
  if (list.Count == 0)
  {
    list.Add(currSetDict);
    return false;
  }
  bool same = true;
  foreach (Dictionary<int, int> dict in list)
  {
    same = true;
    foreach (KeyValuePair<int, int> kvp in dict)
    {
      if (!currSetDict.ContainsKey(kvp.Key))
      {
        same = false;
        break;
      } else if (currSetDict[kvp.Key] != kvp.Value)
      {
        same = false;
        break;
      }
    }
    if (same == true)
      break;
  }

  if (!same)
    list.Add(currSetDict);

  return same;
}

Part1(input);
//Part2(input);
void Part1(string[] input)
{
  Dictionary<string, Teaspoon> data = new();
  foreach(string line in input)
  {
    Teaspoon t = new Teaspoon();
    string[] split = line.Split(' ');
    t.Capacity   = Convert.ToInt32(split[2].Substring(0, split[2].Length - 1));
    t.Durability = Convert.ToInt32(split[4].Substring(0, split[4].Length - 1));
    t.Flavor     = Convert.ToInt32(split[6].Substring(0, split[6].Length - 1));
    t.Texture    = Convert.ToInt32(split[8].Substring(0, split[8].Length - 1));
    t.Calories   = Convert.ToInt32(split[10]);

    data.Add(split[0], t);
    
  }
  List<string> ingredients = data.Keys.ToList();
  int sum = Combinations(100, ingredients.Count, ingredients, data);

  Console.WriteLine($"Part1 result is {sum}");
}

void ProcessAllSets(List<string> ingredients, int teaSpoonsCount, Action<List<string>, int[]> func)
{
  int[] sets = new int[ingredients.Count];
  for (int i = 0; i < sets.Length; i++)
    sets[i] = 0;

  // [0, 0, 0, 100]
  sets[sets.Length - 1] = teaSpoonsCount;
  func(ingredients, sets);
  int[] permCopy = new int[ingredients.Count];
  int sum = 0;

  while (sets[0] != teaSpoonsCount)
  {
    Debug.Assert(sets.Sum() == teaSpoonsCount);
    sets.CopyTo(permCopy, 0);
    Permutations.ForAllPermutation(permCopy, (values) =>
    {
      func(ingredients, values);
      return false;
    });


  }  
}




void Part2(string[] input)
{

}

class Teaspoon
{
  public int Capacity;
  public int Durability;
  public int Flavor;
  public int Texture;
  public int Calories;
}