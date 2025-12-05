bool sample = false;
string filename = "input.txt";
if (sample)
  filename = "input_sample.txt";

string[] input = File.ReadAllLines(filename);

Part1(input);
Part2(input);
void Part1(string[] input)
{
  GiftFromSue giftToFind = new GiftFromSue();
  giftToFind.Children = 3;
  giftToFind.Cats = 7;
  giftToFind.Samoyeds = 2;
  giftToFind.Pomeranians = 3;
  giftToFind.Akitas = 0;
  giftToFind.Vizslas = 0;
  giftToFind.Goldfish = 5;
  giftToFind.Trees = 3;
  giftToFind.Cars = 2;
  giftToFind.Perfumes = 1;

  Dictionary<string, GiftFromSue> giftData = new();
  foreach (string line in input)
  {
    (string aunt, GiftFromSue g) gift = UnpackGiftFromLine(line);
    giftData.Add(gift.aunt, gift.g);
  }


  string sue = "";
  foreach (KeyValuePair<string, GiftFromSue> kvp in giftData)
  {
    bool match = true;
    GiftFromSue g = kvp.Value;
    if (g.Children >= 0 && g.Children != giftToFind.Children)
      match = false;
    if (g.Cats >= 0 && g.Cats != giftToFind.Cats)
      match = false;
    if (g.Samoyeds >= 0 && g.Samoyeds != giftToFind.Samoyeds)
      match = false;
    if (g.Pomeranians >= 0 && g.Pomeranians != giftToFind.Children)
      match = false;
    if (g.Akitas >= 0 && g.Akitas != giftToFind.Akitas)
      match = false;
    if (g.Vizslas >= 0 && g.Vizslas != giftToFind.Vizslas)
      match = false;
    if (g.Goldfish >= 0 && g.Goldfish != giftToFind.Goldfish)
      match = false;
    if (g.Trees >= 0 && g.Trees != giftToFind.Trees)
      match = false;
    if (g.Cars >= 0 && g.Cars != giftToFind.Cars)
      match = false;
    if (g.Perfumes >= 0 && g.Perfumes != giftToFind.Perfumes)
      match = false;

    if (match)
    {
      sue = kvp.Key;
      break;
    }
  }

  Console.WriteLine($"Part1 result {sue.Split(' ')[1]}");
}

(string aunt, GiftFromSue gift) UnpackGiftFromLine(string line)
{
  string aunt = line[0..line.IndexOf(':')];
  GiftFromSue gift = new GiftFromSue();
  // Sue 129: vizslas: 8, children: 5, perfumes: 8
  string[] gifts = line[(line.IndexOf(':') + 1)..].Split(',');
  foreach(string g in gifts)
  {
    string[] d = g.Trim().Split(':');
    int val = Convert.ToInt32(d[1]);
    switch (d[0])
    {
      case "children":
        gift.Children = val;
        break;
      case "cats":
        gift.Cats = val;
        break;
      case "samoyeds":
        gift.Samoyeds = val;
        break;
      case "pomeranians":
        gift.Pomeranians = val;
        break;
      case "akitas":
        gift.Akitas = val;
        break;
      case "vizslas":
        gift.Vizslas = val;
        break;
      case "goldfish":
        gift.Goldfish = val;
        break;
      case "trees":
        gift.Trees = val;
        break;
      case "cars":
        gift.Cars = val;
        break;
      case "perfumes":
        gift.Perfumes = val;
        break;
      default:
        break;
    }
  }
  return (aunt, gift);
}

void Part2(string[] input)
{
  GiftFromSue giftToFind = new GiftFromSue();
  giftToFind.Children = 3;
  giftToFind.Cats = 7;
  giftToFind.Samoyeds = 2;
  giftToFind.Pomeranians = 3;
  giftToFind.Akitas = 0;
  giftToFind.Vizslas = 0;
  giftToFind.Goldfish = 5;
  giftToFind.Trees = 3;
  giftToFind.Cars = 2;
  giftToFind.Perfumes = 1;

  Dictionary<string, GiftFromSue> giftData = new();
  foreach (string line in input)
  {
    (string aunt, GiftFromSue g) gift = UnpackGiftFromLine(line);
    giftData.Add(gift.aunt, gift.g);
  }


  string sue = "";
  foreach (KeyValuePair<string, GiftFromSue> kvp in giftData)
  {
    bool match = true;
    GiftFromSue g = kvp.Value;
    if (g.Children >= 0 && g.Children != giftToFind.Children)
      match = false;
    if (g.Cats >= 0 && g.Cats <= giftToFind.Cats)
      match = false;
    if (g.Samoyeds >= 0 && g.Samoyeds != giftToFind.Samoyeds)
      match = false;
    if (g.Pomeranians >= 0 && g.Pomeranians >= giftToFind.Children)
      match = false;
    if (g.Akitas >= 0 && g.Akitas != giftToFind.Akitas)
      match = false;
    if (g.Vizslas >= 0 && g.Vizslas != giftToFind.Vizslas)
      match = false;
    if (g.Goldfish >= 0 && g.Goldfish >= giftToFind.Goldfish)
      match = false;
    if (g.Trees >= 0 && g.Trees <= giftToFind.Trees)
      match = false;
    if (g.Cars >= 0 && g.Cars != giftToFind.Cars)
      match = false;
    if (g.Perfumes >= 0 && g.Perfumes != giftToFind.Perfumes)
      match = false;

    if (match)
    {
      sue = kvp.Key;
      break;
    }
  }

  Console.WriteLine($"Part 2 result {sue.Split(' ')[1]}");
}


class GiftFromSue()
{
  public int Children = -1;
  public int Cats = -1;
  public int Samoyeds = -1;
  public int Pomeranians = -1;
  public int Akitas = -1;
  public int Vizslas = -1;
  public int Goldfish = -1;
  public int Trees = -1;
  public int Cars = -1;
  public int Perfumes = -1;
}