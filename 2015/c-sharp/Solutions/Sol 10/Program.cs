using System.Text;

bool sample = false;
string filename = "input.txt";
if (sample)
  filename = "input_sample.txt";

string input = "1321131112";

Part1(input);
Part2(input);
void Part1(string input)
{
  StringBuilder sb = new StringBuilder();
  sb.Append(input);
  for (int i = 0; i < 40; i++)
  {
    input = ExpandString(sb);
    sb.Clear();
    sb.Append(input);
  }

  Console.WriteLine($"Part 1 solution is {sb.ToString().Length}");
}

void Part2(string input)
{
  StringBuilder sb = new StringBuilder();
  sb.Append(input);
  for (int i = 0; i < 50; i++)
  {
    input = ExpandString(sb);
    sb.Clear();
    sb.Append(input);
  }

  Console.WriteLine($"Part 2 solution is {sb.ToString().Length}");
}

string ExpandString(StringBuilder sb)
{
  string toProcess = sb.ToString();
  sb.Clear();
  int prevNum = -1;
  int currNum = 0;
  int count = 0;
  for (int i =0; i < toProcess.Length; i++)
  {
    currNum = Convert.ToInt32(toProcess[i].ToString());
    // last
    if (i +1 == toProcess.Length)
    {
      if (currNum == prevNum)
      {
        count++;
        sb.Append(count);
        sb.Append(currNum);
      } else
      {
        sb.Append(count);
        sb.Append(prevNum);
        sb.Append(1);
        sb.Append(currNum);
      }
        break;
    }
    if (currNum != prevNum && prevNum != -1)
    {
      sb.Append(count);
      sb.Append(prevNum);
      count = 1;
    }
    else
    {
      count++;
    }
    prevNum = currNum;
   
  }
 
  return sb.ToString();
}
