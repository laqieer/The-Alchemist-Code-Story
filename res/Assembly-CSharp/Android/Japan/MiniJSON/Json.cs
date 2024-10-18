﻿// Decompiled with JetBrains decompiler
// Type: MiniJSON.Json
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MiniJSON
{
  public static class Json
  {
    public static object Deserialize(string json)
    {
      if (json == null)
        return (object) null;
      return Json.Parser.Parse(json);
    }

    public static string Serialize(object obj)
    {
      return Json.Serializer.Serialize(obj);
    }

    private sealed class Parser : IDisposable
    {
      private const string WORD_BREAK = "{}[],:\"";
      private StringReader json;

      private Parser(string jsonString)
      {
        this.json = new StringReader(jsonString);
      }

      public static bool IsWordBreak(char c)
      {
        if (!char.IsWhiteSpace(c))
          return "{}[],:\"".IndexOf(c) != -1;
        return true;
      }

      public static object Parse(string jsonString)
      {
        using (Json.Parser parser = new Json.Parser(jsonString))
          return parser.ParseValue();
      }

      public void Dispose()
      {
        this.json.Dispose();
        this.json = (StringReader) null;
      }

      private Dictionary<string, object> ParseObject()
      {
        Dictionary<string, object> dictionary = new Dictionary<string, object>();
        this.json.Read();
        while (true)
        {
          Json.Parser.TOKEN nextToken;
          do
          {
            nextToken = this.NextToken;
            switch (nextToken)
            {
              case Json.Parser.TOKEN.NONE:
                goto label_3;
              case Json.Parser.TOKEN.CURLY_CLOSE:
                goto label_4;
              default:
                continue;
            }
          }
          while (nextToken == Json.Parser.TOKEN.COMMA);
          string index = this.ParseString();
          if (index != null)
          {
            if (this.NextToken == Json.Parser.TOKEN.COLON)
            {
              this.json.Read();
              dictionary[index] = this.ParseValue();
            }
            else
              goto label_8;
          }
          else
            goto label_6;
        }
label_3:
        return (Dictionary<string, object>) null;
label_4:
        return dictionary;
label_6:
        return (Dictionary<string, object>) null;
label_8:
        return (Dictionary<string, object>) null;
      }

      private List<object> ParseArray()
      {
        List<object> objectList = new List<object>();
        this.json.Read();
        bool flag = true;
        while (flag)
        {
          Json.Parser.TOKEN nextToken = this.NextToken;
          switch (nextToken)
          {
            case Json.Parser.TOKEN.SQUARED_CLOSE:
              flag = false;
              continue;
            case Json.Parser.TOKEN.COMMA:
              continue;
            default:
              if (nextToken == Json.Parser.TOKEN.NONE)
                return (List<object>) null;
              object byToken = this.ParseByToken(nextToken);
              objectList.Add(byToken);
              continue;
          }
        }
        return objectList;
      }

      private object ParseValue()
      {
        return this.ParseByToken(this.NextToken);
      }

      private object ParseByToken(Json.Parser.TOKEN token)
      {
        switch (token)
        {
          case Json.Parser.TOKEN.CURLY_OPEN:
            return (object) this.ParseObject();
          case Json.Parser.TOKEN.SQUARED_OPEN:
            return (object) this.ParseArray();
          case Json.Parser.TOKEN.STRING:
            return (object) this.ParseString();
          case Json.Parser.TOKEN.NUMBER:
            return this.ParseNumber();
          case Json.Parser.TOKEN.TRUE:
            return (object) true;
          case Json.Parser.TOKEN.FALSE:
            return (object) false;
          case Json.Parser.TOKEN.NULL:
            return (object) null;
          default:
            return (object) null;
        }
      }

      private string ParseString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        this.json.Read();
        bool flag = true;
        while (flag)
        {
          if (this.json.Peek() == -1)
            break;
          char nextChar1 = this.NextChar;
          switch (nextChar1)
          {
            case '"':
              flag = false;
              continue;
            case '\\':
              if (this.json.Peek() == -1)
              {
                flag = false;
                continue;
              }
              char nextChar2 = this.NextChar;
              switch (nextChar2)
              {
                case 'r':
                  stringBuilder.Append('\r');
                  continue;
                case 't':
                  stringBuilder.Append('\t');
                  continue;
                case 'u':
                  char[] chArray = new char[4];
                  for (int index = 0; index < 4; ++index)
                    chArray[index] = this.NextChar;
                  stringBuilder.Append((char) Convert.ToInt32(new string(chArray), 16));
                  continue;
                default:
                  if (nextChar2 != '"' && nextChar2 != '/' && nextChar2 != '\\')
                  {
                    switch (nextChar2)
                    {
                      case 'b':
                        stringBuilder.Append('\b');
                        continue;
                      case 'f':
                        stringBuilder.Append('\f');
                        continue;
                      case 'n':
                        stringBuilder.Append('\n');
                        continue;
                      default:
                        continue;
                    }
                  }
                  else
                  {
                    stringBuilder.Append(nextChar2);
                    continue;
                  }
              }
            default:
              stringBuilder.Append(nextChar1);
              continue;
          }
        }
        return stringBuilder.ToString();
      }

      private object ParseNumber()
      {
        string nextWord = this.NextWord;
        if (nextWord.IndexOf('.') == -1)
        {
          long result;
          long.TryParse(nextWord, out result);
          return (object) result;
        }
        double result1;
        double.TryParse(nextWord, out result1);
        return (object) result1;
      }

      private void EatWhitespace()
      {
        while (char.IsWhiteSpace(this.PeekChar))
        {
          this.json.Read();
          if (this.json.Peek() == -1)
            break;
        }
      }

      private char PeekChar
      {
        get
        {
          return Convert.ToChar(this.json.Peek());
        }
      }

      private char NextChar
      {
        get
        {
          return Convert.ToChar(this.json.Read());
        }
      }

      private string NextWord
      {
        get
        {
          StringBuilder stringBuilder = new StringBuilder();
          while (!Json.Parser.IsWordBreak(this.PeekChar))
          {
            stringBuilder.Append(this.NextChar);
            if (this.json.Peek() == -1)
              break;
          }
          return stringBuilder.ToString();
        }
      }

      private Json.Parser.TOKEN NextToken
      {
        get
        {
          this.EatWhitespace();
          if (this.json.Peek() == -1)
            return Json.Parser.TOKEN.NONE;
          char peekChar = this.PeekChar;
          switch (peekChar)
          {
            case ',':
              this.json.Read();
              return Json.Parser.TOKEN.COMMA;
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
              return Json.Parser.TOKEN.NUMBER;
            case ':':
              return Json.Parser.TOKEN.COLON;
            default:
              switch (peekChar)
              {
                case '[':
                  return Json.Parser.TOKEN.SQUARED_OPEN;
                case ']':
                  this.json.Read();
                  return Json.Parser.TOKEN.SQUARED_CLOSE;
                default:
                  switch (peekChar)
                  {
                    case '{':
                      return Json.Parser.TOKEN.CURLY_OPEN;
                    case '}':
                      this.json.Read();
                      return Json.Parser.TOKEN.CURLY_CLOSE;
                    default:
                      if (peekChar == '"')
                        return Json.Parser.TOKEN.STRING;
                      switch (this.NextWord)
                      {
                        case "false":
                          return Json.Parser.TOKEN.FALSE;
                        case "true":
                          return Json.Parser.TOKEN.TRUE;
                        case "null":
                          return Json.Parser.TOKEN.NULL;
                        default:
                          return Json.Parser.TOKEN.NONE;
                      }
                  }
              }
          }
        }
      }

      private enum TOKEN
      {
        NONE,
        CURLY_OPEN,
        CURLY_CLOSE,
        SQUARED_OPEN,
        SQUARED_CLOSE,
        COLON,
        COMMA,
        STRING,
        NUMBER,
        TRUE,
        FALSE,
        NULL,
      }
    }

    private sealed class Serializer
    {
      private StringBuilder builder;

      private Serializer()
      {
        this.builder = new StringBuilder();
      }

      public static string Serialize(object obj)
      {
        Json.Serializer serializer = new Json.Serializer();
        serializer.SerializeValue(obj);
        return serializer.builder.ToString();
      }

      private void SerializeValue(object value)
      {
        if (value == null)
        {
          this.builder.Append("null");
        }
        else
        {
          string str;
          if ((str = value as string) != null)
            this.SerializeString(str);
          else if (value is bool)
          {
            this.builder.Append(!(bool) value ? "false" : "true");
          }
          else
          {
            IList anArray;
            if ((anArray = value as IList) != null)
            {
              this.SerializeArray(anArray);
            }
            else
            {
              IDictionary dictionary;
              if ((dictionary = value as IDictionary) != null)
                this.SerializeObject(dictionary);
              else if (value is char)
                this.SerializeString(new string((char) value, 1));
              else
                this.SerializeOther(value);
            }
          }
        }
      }

      private void SerializeObject(IDictionary obj)
      {
        bool flag = true;
        this.builder.Append('{');
        IEnumerator enumerator = obj.Keys.GetEnumerator();
        try
        {
          while (enumerator.MoveNext())
          {
            object current = enumerator.Current;
            if (!flag)
              this.builder.Append(',');
            this.SerializeString(current.ToString());
            this.builder.Append(':');
            this.SerializeValue(obj[current]);
            flag = false;
          }
        }
        finally
        {
          IDisposable disposable;
          if ((disposable = enumerator as IDisposable) != null)
            disposable.Dispose();
        }
        this.builder.Append('}');
      }

      private void SerializeArray(IList anArray)
      {
        this.builder.Append('[');
        bool flag = true;
        IEnumerator enumerator = anArray.GetEnumerator();
        try
        {
          while (enumerator.MoveNext())
          {
            object current = enumerator.Current;
            if (!flag)
              this.builder.Append(',');
            this.SerializeValue(current);
            flag = false;
          }
        }
        finally
        {
          IDisposable disposable;
          if ((disposable = enumerator as IDisposable) != null)
            disposable.Dispose();
        }
        this.builder.Append(']');
      }

      private void SerializeString(string str)
      {
        this.builder.Append('"');
        foreach (char ch in str.ToCharArray())
        {
          switch (ch)
          {
            case '\b':
              this.builder.Append("\\b");
              break;
            case '\t':
              this.builder.Append("\\t");
              break;
            case '\n':
              this.builder.Append("\\n");
              break;
            case '\f':
              this.builder.Append("\\f");
              break;
            case '\r':
              this.builder.Append("\\r");
              break;
            default:
              switch (ch)
              {
                case '"':
                  this.builder.Append("\\\"");
                  continue;
                case '\\':
                  this.builder.Append("\\\\");
                  continue;
                default:
                  int int32 = Convert.ToInt32(ch);
                  if (int32 >= 32 && int32 <= 126)
                  {
                    this.builder.Append(ch);
                    continue;
                  }
                  this.builder.Append("\\u");
                  this.builder.Append(int32.ToString("x4"));
                  continue;
              }
          }
        }
        this.builder.Append('"');
      }

      private void SerializeOther(object value)
      {
        if (value is float)
          this.builder.Append(((float) value).ToString("R"));
        else if (value is int || value is uint || (value is long || value is sbyte) || (value is byte || value is short || (value is ushort || value is ulong)))
          this.builder.Append(value);
        else if (value is double || value is Decimal)
          this.builder.Append(Convert.ToDouble(value).ToString("R"));
        else
          this.SerializeString(value.ToString());
      }
    }
  }
}
