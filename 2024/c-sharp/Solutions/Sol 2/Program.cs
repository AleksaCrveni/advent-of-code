// See https://aka.ms/new-console-template for more information

void P1(string[] lines)
{
  int safe = 0;
  foreach (var line in lines)
  {
    var split = line.Split(' ');
    bool safeB = true;
    // if this is false then its descending
    bool ascending = true;

    if (split[0] == split[2])
      continue;
    if (Convert.ToInt32(split[0]) > Convert.ToInt32(split[2]))
      ascending = false;

    for (int i = 0; i < split.Length - 1; i++)
    {
      int curr = Convert.ToInt32(split[i]);
      int next = Convert.ToInt32(split[i + 1]);
      if (curr == next)
      {
        safeB = false;
        break;
      }
      // should be ascending order
      if (curr < next && !ascending)
      {
        safeB = false;
        break;
      }

      if (curr > next && ascending)
      {
        safeB = false;
        break;
      }

      if (Math.Abs(curr - next) > 3)
      {
        safeB = false;
        break;
      }
      
    }

    if (safeB) safe++;
  }
  Console.WriteLine($"Safe count is {safe}");
}

void P2(string[] lines)
{

}

bool test = false;
if (args.Length > 0)
  if (args[0] == "Real")
    test = false;

string filename = test ? "input_sample.txt" : "input.txt";

string[] lines = File.ReadAllLines(filename);
P1(lines);
P2(lines);
