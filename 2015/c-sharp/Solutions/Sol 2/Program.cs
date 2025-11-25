bool sample = false;
string filename = "input.txt";
if (sample)
  filename = "input_sample.txt";

string[] input = File.ReadAllLines(filename);

Part1(input);
Part2(input);
void Part1(string[] input)
{
  int sum = 0;
  int curr = 0;
  int min = 0;
  foreach (string line in input)
  {
    string[] data = line.Split('x');
    int l = Convert.ToInt32(data[0]);
    int w = Convert.ToInt32(data[1]);
    int h = Convert.ToInt32(data[2]);
    int s1 = l * w;
    int s2 = w * h;
    int s3 = h * l;
    curr = 2 * s1 + 2 * s2 + 2 * s3;
    if (s1 < s2)
      if (s1 < s3)
        curr += s1;
      else
        curr += s3;
    else if (s2 < s3)
      curr += s2;
    else
      curr += s3;
    sum += curr;
  }

  Console.WriteLine($"Part 1 Res is {sum}");
}

void Part2(string[] input)
{
  int sum = 0;
  int curr = 0;
  int min = 0;
  foreach (string line in input)
  {
    string[] data = line.Split('x');
    int l = Convert.ToInt32(data[0]);
    int w = Convert.ToInt32(data[1]);
    int h = Convert.ToInt32(data[2]);
    (int min1, int min2) minData;
    if (l < w)
    {
      minData.min1 = l;
      if (w < h)
      {
        minData.min2 = w;
      } else
      {
        minData.min2 = h;
      }
    } else
    {
      minData.min1 = w;
      if (l < h)
      {
        minData.min2 = l;
      } else
      {
        minData.min2 = h;
      }
    }
    curr += 2 * minData.min1 + 2 * minData.min2;
    curr += l * w * h;
    sum += curr;
    curr = 0;
  }

  Console.WriteLine($"Part 2 Res is {sum}");
}
