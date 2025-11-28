using System.Globalization;
using System.Text;

namespace Sol_12
{
  public static class FakeJSONParser
  {
    // Delimiters
    public readonly static byte LEFT_CURLY_BRACKET = (byte)'{';
    public readonly static byte RIGHT_CURLY_BRACKET = (byte)'}';
    public readonly static byte LEFT_SQUARE_BRACKET = (byte)'[';
    public readonly static byte RIGHT_SQUARE_BRACKET = (byte)']';
    public readonly static byte COLON = (byte)':';
    public readonly static byte COMMA = (byte)',';
    public readonly static byte DQUOTE = (byte)'\"';

    private readonly static byte[] _delimiters = [LEFT_CURLY_BRACKET, RIGHT_CURLY_BRACKET, LEFT_SQUARE_BRACKET, RIGHT_SQUARE_BRACKET, COLON, COMMA];
    public static Token Parse(byte[] buffer)
    {
      State state = new State();
      state.Buffer = buffer.AsSpan();
      state.Pos = 0;
      state.ReadPos = 0;
      Token t = ParseNextToken(ref state)!;
      return t;
    }

    // I dont think there is space in input but w/e
    public static void SkipWhiteSpace(ref State state)
    {
      while (state.C == ' ' && state.C != 0)
        ReadChar(ref state);
    }

    public static Token? ParseNextToken(ref State state)
    {
      SkipWhiteSpace(ref state);
      ReadChar(ref state);
      byte c = state.C;
      // EOF
      if (c == 0)
        return null;
      Token t = new Token();
      if (c == LEFT_SQUARE_BRACKET)
      {
        t.Type = TokenType.ARRAY;
        t.Array = ParseArray(ref state);
      } else if (c == LEFT_CURLY_BRACKET)
      {
        t.Type = TokenType.OBJECT;
        t.Dictionary = ParseObject(ref state);
      }
      else if (char.IsDigit((char)c) || c == '-')
      {
        t.Type = TokenType.INT;
        t.IntLit = ParseInt(ref state);
      }
      else
      {
        t.Type = TokenType.STRING;
        t.StringLit = ParseString(ref state);
      }
      return t;
    }
    public static int ParseInt(ref State state)
    {
      int num = 0;
      int multiplier = 1;
      byte c;

      // EOF
      if (state.Pos == state.Buffer.Length)
        return 0;

      if (state.Buffer[state.Pos] == '-')
      {
        multiplier = -1;
        state.Pos++;
      }


      for (; state.Pos < state.Buffer.Length; state.Pos++)
      {
        c = state.Buffer[state.Pos];
        if (c < '0' || c > '9')
          break;

        num = num * 10 + CharUnicodeInfo.GetDigitValue((char)c);
      }
      state.ReadPos = state.Pos++;
      return num * multiplier;
    }
    public static string ParseString(ref State state)
    {
      ReadChar(ref state);
      // this is fine for now but it wont work on escaped characters , but threre arent any on input so its w/e
      int start = state.Pos;
      while (state.C != '"' && state.C != 0)
        ReadChar(ref state);
      return Encoding.Default.GetString(state.Buffer.Slice(start, state.Pos - start));
    }
    public static Dictionary<string, Token> ParseObject(ref State state)
    {

      Dictionary<string, Token> dict = new Dictionary<string, Token>();
      Token? t;
      string key = "";
      while (state.C != '}')
      {
        //bandaid but w/e , in reality i should have hanadled all these stuff in ParseSTring
        if (state.C != '\"')
          ReadChar(ref state);
        key = ParseString(ref state);
        if (key == "")
          return dict;
        ReadChar(ref state); // move off " to :
        t = ParseNextToken(ref state);
        if (t != null)
          dict.Add(key, t);
        else
          break;
        if (state.C == '}' && t.Type != TokenType.OBJECT)
          break;
        ReadChar(ref state);
      }
      return dict;
    }

    public static List<Token> ParseArray(ref State state)
    {
      List<Token> list = new List<Token>();
      Token? t;
      while (state.C != ']')
      {
        t = ParseNextToken(ref state);
        if (t != null)
          list.Add(t);
        else
          break;

        if (state.C == ']' && t.Type != TokenType.ARRAY)
          break;
        ReadChar(ref state);
      }
      return list;
    }
    public static void ReadChar(ref State state)
    {
      if (state.ReadPos >= state.Buffer.Length)
        state.C = 0;
      else
        state.C = state.Buffer[state.ReadPos];

      // set curr and go next
      state.Pos = state.ReadPos++;
    }

    public static bool IsDelimiter(byte c)
    {
      return _delimiters.Contains(c);
    }
  }
  

  public ref struct State()
  {
    public ReadOnlySpan<byte> Buffer;
    public int Pos;
    public int ReadPos;
    public byte C;
  }



  public class Token
  {
    public TokenType Type;
    public int? IntLit = null;
    public string? StringLit = null;
    public Dictionary<string, Token>? Dictionary;
    public List<Token>? Array;
  }

  
  public enum TokenType
  {
    OBJECT,
    ARRAY,
    STRING,
    INT
  }

  
}
