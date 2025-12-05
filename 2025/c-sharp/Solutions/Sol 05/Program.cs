
bool sample = false;
string path = "input.txt";
if (sample)
  path = "input_sample.txt";

string[] input = File.ReadAllLines(path);

Part1(input);

Part2(input);


void Part1(string[] input)
{
  long i = 0;
  // could have used Range struct
  List<(long s, long e)> ranges = new();
  for (; i < input.Length; i++)
  {
    if (input[i] == "")
    {
      i++;
      break;
    }
    long[] split = input[i].Split('-').Select(long.Parse).ToArray();
    ranges.Add((split[0], split[1]));
  }

  int count = 0;
  for (; i < input.Length; i++)
  {
    long ID = long.Parse(input[i]);
    foreach ((long s, long e) range in ranges)
    {
      if (ID >= range.s && ID <= range.e)
      {
        count++;
        break;
      }
        
    }
  }

  Console.WriteLine($"Part 1 result is {count}");
}

void Part2(string[] input)
{
  long sum = 0;
  // could have used Range struct
  int i = 0;
  // could have used Range struct
  List<(long s, long e)> ranges = new();
  for (; i < input.Length; i++)
  {
    if (input[i] == "")
      break;
    long[] split = input[i].Split('-').Select(long.Parse).ToArray();
    ranges.Add((split[0], split[1]));
  }

  ranges = ranges.OrderBy(x => x.s).ToList();

  List<(long s, long e)> combinedRanges = CombineRanges(ranges);
  for (i = 0; i < combinedRanges.Count; i++)
  {
    File.WriteAllLines("myOUt.txt", combinedRanges.Select(r => $"{r.s}-{r.e}").ToList<string>());
    (long s, long e) curr = combinedRanges[i];
    sum += curr.e - curr.s + 1;
  }

  Console.WriteLine($"Part 2 result is {sum}");
}



List<(long s, long e)> CombineRanges(List<(long s, long e)> ranges)
{
  List<(long s, long e)> combinedRanges = new List<(long s, long e)>();
  for (int i = 0; i < ranges.Count; i++)
  {
    (long s, long e) curr = ranges[i];
    if (i == ranges.Count - 1)
    {
      combinedRanges.Add(ranges[i]);
      break;
    }
    (long s, long e) next = ranges[i + 1];
    if (curr.s <= next.e && next.s <= curr.e)
    {
      combinedRanges.Add((Math.Min(curr.s, next.s), Math.Max(curr.e, next.e)));
      i++;
    }
    else
    {
      combinedRanges.Add(curr);
    }
  }

  if (combinedRanges.Count == ranges.Count)
    return combinedRanges;
  else
    return CombineRanges(combinedRanges);
}