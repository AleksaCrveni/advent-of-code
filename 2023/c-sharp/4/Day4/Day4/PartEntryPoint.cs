namespace Day4;

public static class PartEntryPoint
{
  public static void PartOne()
  {
    string currExecutableDir = Directory.GetCurrentDirectory();
    string toCutOff = "bin\\Debug\\net.8.0";
    string inputDir = currExecutableDir.Substring(0, currExecutableDir.Length - toCutOff.Length);
    string? line;
    using StreamReader sr = new StreamReader(Path.Combine(inputDir, "input.txt"));
    line = sr.ReadLine()!;
    HashSet<string> winningNumbers = new HashSet<string>();
    int totalSum = 0;
    int count = 0;
    while (line != null)
    {
      count = 0;
      winningNumbers.Clear();
      string[] nums = line.Split(':')[1].Split('|');
      // If i wanted to do this more efficently i would check char for char and use something like num * 10 + val
      string[] winningNums = nums[0].Trim().Split(' ');
      for (int i = 0; i < winningNums.Length; i++)
      {
        if (winningNums[i] == " " || winningNums[i] == "")
          continue;
        winningNumbers.Add(winningNums[i]);
      }

      string[] chosenNums = nums[1].Trim().Split(' ');
      for (int i = 0; i < chosenNums.Length; i++)
      {
        if (chosenNums[i] == " " || chosenNums[i] == "")
          continue;
        if (winningNumbers.Contains(chosenNums[i]))
          count++;
      }

      if (count == 1)
        totalSum++;
      if (count > 1)
        totalSum += Convert.ToInt32(Math.Pow(2, count - 1));

      line = sr.ReadLine();
    }
    Console.WriteLine(totalSum);
  }

  public static void PartTwo()
  {
    string currExecutableDir = Directory.GetCurrentDirectory();
    string toCutOff = "bin\\Debug\\net.8.0";
    string inputDir = currExecutableDir.Substring(0, currExecutableDir.Length - toCutOff.Length);
    string? line;
    using StreamReader sr = new StreamReader(Path.Combine(inputDir, "input.txt"));
    line = sr.ReadLine()!;

    // card - copy count
    Dictionary<string, int> cards = new Dictionary<string, int>();
    HashSet<string> winningNumbers = new HashSet<string>();
    int totalSum = 0;
    int winCount = 0;
    string currentCard = "";
    while (line != null)
    {
      winCount = 0;
      winningNumbers.Clear();
      // this is such a mess lol
      string[] split = line.Split(":");
      string[] nums = split[1].Split('|');
      string[] splitCardName = split[0].Trim().Split(' ');
      currentCard = splitCardName[splitCardName.Length -1];

      // If i wanted to do this more efficently i would check char for char and use something like num * 10 + val
      string[] winningNums = nums[0].Trim().Split(' ');
      for (int i = 0; i < winningNums.Length; i++)
      {
        if (winningNums[i] == " " || winningNums[i] == "")
          continue;
        winningNumbers.Add(winningNums[i]);
      }

      string[] chosenNums = nums[1].Trim().Split(' ');
      for (int i = 0; i < chosenNums.Length; i++)
      {
        if (chosenNums[i] == " " || chosenNums[i] == "")
          continue;
        if (winningNumbers.Contains(chosenNums[i]))
          winCount++;
      }

      if (cards.ContainsKey(currentCard))
      {
        int currCount = cards[currentCard];
        cards[currentCard]++;
        currCount++;
        int j = 0;
        while (j < winCount)
        {
          string nextCard = (Convert.ToInt32(currentCard) + j+1).ToString();
          if (cards.ContainsKey(nextCard))
          {
            cards[nextCard] += currCount;
          }
          else
          {
            cards.Add(nextCard,1);
          }
          j++;
        }

      }
      else
      {
        cards.Add(currentCard, 1);
        int j = 0;
        while (j < winCount)
        {
          if (currentCard == "")
          {
            string s = "urcina";
          }

          string nextCard = (Convert.ToInt32(currentCard) + j + 1).ToString();
          if (cards.ContainsKey(nextCard))
          {
            cards[nextCard]++;
          }
          else
          {
            cards.Add(nextCard,1);
          }
          j++;
        }
      }

      line = sr.ReadLine();
      if (winCount == 0) {}

        line = null;
    }

    foreach (int count in cards.Values)
      totalSum += count;
    Console.WriteLine(totalSum);
  }
}

public class Card
{
  public int TotalCount { get; set; }
  public int WinningCount { get; set; }
};