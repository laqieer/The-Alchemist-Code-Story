// Decompiled with JetBrains decompiler
// Type: MessagePack.TinyJsonReader
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Globalization;
using System.IO;
using System.Text;

#nullable disable
namespace MessagePack
{
  internal class TinyJsonReader : IDisposable
  {
    private readonly TextReader reader;
    private readonly bool disposeInnerReader;
    private StringBuilder reusableBuilder;

    public TinyJsonReader(TextReader reader, bool disposeInnerReader = true)
    {
      this.reader = reader;
      this.disposeInnerReader = disposeInnerReader;
    }

    public TinyJsonToken TokenType { get; private set; }

    public ValueType ValueType { get; private set; }

    public double DoubleValue { get; private set; }

    public long LongValue { get; private set; }

    public ulong ULongValue { get; private set; }

    public Decimal DecimalValue { get; private set; }

    public string StringValue { get; private set; }

    public bool Read()
    {
      this.ReadNextToken();
      this.ReadValue();
      return this.TokenType != TinyJsonToken.None;
    }

    public void Dispose()
    {
      if (this.reader != null && this.disposeInnerReader)
        this.reader.Dispose();
      this.TokenType = TinyJsonToken.None;
      this.ValueType = ValueType.Null;
    }

    private void SkipWhiteSpace()
    {
      for (int c = this.reader.Peek(); c != -1 && char.IsWhiteSpace((char) c); c = this.reader.Peek())
        this.reader.Read();
    }

    private char ReadChar() => (char) this.reader.Read();

    private static bool IsWordBreak(char c)
    {
      switch (c)
      {
        case ' ':
        case '"':
label_4:
          return true;
        default:
          switch (c)
          {
            case '[':
            case ']':
              goto label_4;
            default:
              switch (c)
              {
                case '{':
                case '}':
                  goto label_4;
                default:
                  if (c != ',' && c != ':')
                    return false;
                  goto label_4;
              }
          }
      }
    }

    private void ReadNextToken()
    {
      this.SkipWhiteSpace();
      int num = this.reader.Peek();
      if (num == -1)
      {
        this.TokenType = TinyJsonToken.None;
      }
      else
      {
        char ch = (char) num;
        switch (ch)
        {
          case ',':
          case ':':
            this.reader.Read();
            this.ReadNextToken();
            break;
          case '-':
          case '0':
          case '1':
          case '2':
          case '3':
          case '4':
          case '5':
          case '6':
          case '7':
          case '8':
          case '9':
            this.TokenType = TinyJsonToken.Number;
            break;
          default:
            switch (ch)
            {
              case '[':
                this.TokenType = TinyJsonToken.StartArray;
                return;
              case ']':
                this.TokenType = TinyJsonToken.EndArray;
                return;
              default:
                switch (ch)
                {
                  case '{':
                    this.TokenType = TinyJsonToken.StartObject;
                    return;
                  case '}':
                    this.TokenType = TinyJsonToken.EndObject;
                    return;
                  default:
                    switch (ch)
                    {
                      case '"':
                        this.TokenType = TinyJsonToken.String;
                        return;
                      case 'f':
                        this.TokenType = TinyJsonToken.False;
                        return;
                      case 'n':
                        this.TokenType = TinyJsonToken.Null;
                        return;
                      case 't':
                        this.TokenType = TinyJsonToken.True;
                        return;
                      default:
                        throw new TinyJsonException("Invalid String:" + (object) ch);
                    }
                }
            }
        }
      }
    }

    private void ReadValue()
    {
      this.ValueType = ValueType.Null;
      switch (this.TokenType)
      {
        case TinyJsonToken.None:
          break;
        case TinyJsonToken.StartObject:
        case TinyJsonToken.EndObject:
        case TinyJsonToken.StartArray:
        case TinyJsonToken.EndArray:
          this.reader.Read();
          break;
        case TinyJsonToken.Number:
          this.ReadNumber();
          break;
        case TinyJsonToken.String:
          this.ReadString();
          break;
        case TinyJsonToken.True:
          if (this.ReadChar() != 't')
            throw new TinyJsonException("Invalid Token");
          if (this.ReadChar() != 'r')
            throw new TinyJsonException("Invalid Token");
          if (this.ReadChar() != 'u')
            throw new TinyJsonException("Invalid Token");
          if (this.ReadChar() != 'e')
            throw new TinyJsonException("Invalid Token");
          this.ValueType = ValueType.True;
          break;
        case TinyJsonToken.False:
          if (this.ReadChar() != 'f')
            throw new TinyJsonException("Invalid Token");
          if (this.ReadChar() != 'a')
            throw new TinyJsonException("Invalid Token");
          if (this.ReadChar() != 'l')
            throw new TinyJsonException("Invalid Token");
          if (this.ReadChar() != 's')
            throw new TinyJsonException("Invalid Token");
          if (this.ReadChar() != 'e')
            throw new TinyJsonException("Invalid Token");
          this.ValueType = ValueType.False;
          break;
        case TinyJsonToken.Null:
          if (this.ReadChar() != 'n')
            throw new TinyJsonException("Invalid Token");
          if (this.ReadChar() != 'u')
            throw new TinyJsonException("Invalid Token");
          if (this.ReadChar() != 'l')
            throw new TinyJsonException("Invalid Token");
          if (this.ReadChar() != 'l')
            throw new TinyJsonException("Invalid Token");
          this.ValueType = ValueType.Null;
          break;
        default:
          throw new ArgumentException("InvalidTokenState:" + (object) this.TokenType);
      }
    }

    private void ReadNumber()
    {
      StringBuilder reusableBuilder;
      if (this.reusableBuilder == null)
      {
        this.reusableBuilder = new StringBuilder();
        reusableBuilder = this.reusableBuilder;
      }
      else
      {
        reusableBuilder = this.reusableBuilder;
        reusableBuilder.Length = 0;
      }
      bool flag = false;
      for (int c = this.reader.Peek(); c != -1 && !TinyJsonReader.IsWordBreak((char) c); c = this.reader.Peek())
      {
        char ch = this.ReadChar();
        reusableBuilder.Append(ch);
        if (ch == '.' || ch == 'e' || ch == 'E')
          flag = true;
      }
      string s = reusableBuilder.ToString();
      if (flag)
      {
        double result;
        double.TryParse(s, NumberStyles.Float | NumberStyles.AllowThousands, (IFormatProvider) CultureInfo.InvariantCulture, out result);
        this.ValueType = ValueType.Double;
        this.DoubleValue = result;
      }
      else
      {
        long result1;
        if (long.TryParse(s, NumberStyles.Integer, (IFormatProvider) CultureInfo.InvariantCulture, out result1))
        {
          this.ValueType = ValueType.Long;
          this.LongValue = result1;
        }
        else
        {
          ulong result2;
          if (ulong.TryParse(s, NumberStyles.Integer, (IFormatProvider) CultureInfo.InvariantCulture, out result2))
          {
            this.ValueType = ValueType.ULong;
            this.ULongValue = result2;
          }
          else
          {
            Decimal result3;
            if (!Decimal.TryParse(s, NumberStyles.Number, (IFormatProvider) CultureInfo.InvariantCulture, out result3))
              return;
            this.ValueType = ValueType.Decimal;
            this.DecimalValue = result3;
          }
        }
      }
    }

    private void ReadString()
    {
      this.reader.Read();
      StringBuilder reusableBuilder;
      if (this.reusableBuilder == null)
      {
        this.reusableBuilder = new StringBuilder();
        reusableBuilder = this.reusableBuilder;
      }
      else
      {
        reusableBuilder = this.reusableBuilder;
        reusableBuilder.Length = 0;
      }
      while (this.reader.Peek() != -1)
      {
        char ch1 = this.ReadChar();
        switch (ch1)
        {
          case '"':
            this.ValueType = ValueType.String;
            this.StringValue = reusableBuilder.ToString();
            return;
          case '\\':
            if (this.reader.Peek() == -1)
              throw new TinyJsonException("Invalid Json String");
            char ch2 = this.ReadChar();
            switch (ch2)
            {
              case 'r':
                reusableBuilder.Append('\r');
                continue;
              case 't':
                reusableBuilder.Append('\t');
                continue;
              case 'u':
                char[] chArray = new char[4]
                {
                  this.ReadChar(),
                  this.ReadChar(),
                  this.ReadChar(),
                  this.ReadChar()
                };
                reusableBuilder.Append((char) Convert.ToInt32(new string(chArray), 16));
                continue;
              default:
                if (ch2 != '"' && ch2 != '/' && ch2 != '\\')
                {
                  switch (ch2)
                  {
                    case 'b':
                      reusableBuilder.Append('\b');
                      continue;
                    case 'f':
                      reusableBuilder.Append('\f');
                      continue;
                    case 'n':
                      reusableBuilder.Append('\n');
                      continue;
                    default:
                      continue;
                  }
                }
                else
                {
                  reusableBuilder.Append(ch2);
                  continue;
                }
            }
          default:
            reusableBuilder.Append(ch1);
            continue;
        }
      }
      throw new TinyJsonException("Invalid Json String");
    }
  }
}
