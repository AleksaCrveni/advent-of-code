using System.Security.Cryptography;
using System.Text;

bool sample = false;
string filename = "input.txt";
if (sample)
  filename = "input_sample.txt";

//string input = File.ReadAllText(filename);

Part1();
Part2();
void Part1()
{
  string input = "yzbqklnj";
  int num = 1;
  string toHash;
  byte[] hash;
  string strHash;
  while (true)
  {
    toHash = $"{input}{num}";
    hash = MD5.HashData(Encoding.UTF8.GetBytes(toHash));
    strHash = BitConverter.ToString(hash).Replace("-", "");
    if (strHash.StartsWith("00000"))
      break;
    num++;
  }
  Console.WriteLine($"Part 1 result is :{num}");
}

void Part2()
{
  string input = "yzbqklnj";
  int num = 1;
  string toHash;
  byte[] hash;
  string strHash;
  while (true)
  {
    toHash = $"{input}{num}";
    hash = MD5.HashData(Encoding.UTF8.GetBytes(toHash));
    strHash = BitConverter.ToString(hash).Replace("-", "");
    if (strHash.StartsWith("000000"))
      break;
    num++;
  }
  Console.WriteLine($"Part 1 result is :{num}");
}
