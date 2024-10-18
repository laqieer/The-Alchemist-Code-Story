// Decompiled with JetBrains decompiler
// Type: GR.JSONParser
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Reflection;
using System.Text;

namespace GR
{
  public class JSONParser
  {
    public static T[] parseJSONArray<T>(string src) where T : new()
    {
      JSONParser.Context ctx = new JSONParser.Context();
      ctx.src = src;
      ctx.pos = 0;
      ctx.sb = new StringBuilder(100);
      ctx.ab = new ArrayList(100);
      T[] array = (T[]) JSONParser.parseArray(ctx, typeof (T));
      JSONParser.skipSpaces(ctx);
      if (ctx.pos < ctx.src.Length)
        throw new JSONParser.InvalidCharacterException(ctx);
      return array;
    }

    public static T parseJSONObject<T>(string src) where T : new()
    {
      JSONParser.Context ctx = new JSONParser.Context();
      ctx.src = src;
      ctx.pos = 0;
      ctx.sb = new StringBuilder(100);
      ctx.ab = new ArrayList(100);
      T instance = Activator.CreateInstance<T>();
      JSONParser.parseObject(ctx, (object) instance);
      JSONParser.skipSpaces(ctx);
      if (ctx.pos < ctx.src.Length)
        throw new JSONParser.InvalidCharacterException(ctx);
      return instance;
    }

    private static bool readDigits(JSONParser.Context ctx)
    {
      if (ctx.pos >= ctx.src.Length || !char.IsDigit(ctx.src[ctx.pos]))
        return false;
      for (; ctx.pos < ctx.src.Length && char.IsDigit(ctx.src[ctx.pos]); ++ctx.pos)
        ctx.sb.Append(ctx.src[ctx.pos]);
      return true;
    }

    private static bool parseNumber(JSONParser.Context ctx, bool ignoreDecimal)
    {
      ctx.sb.Length = 0;
      if (ctx.src[ctx.pos] == '-')
      {
        ctx.sb.Append('-');
        ++ctx.pos;
      }
      if (!JSONParser.readDigits(ctx))
        throw new JSONParser.InvalidNumberException(ctx);
      int length = ctx.sb.Length;
      if (ctx.pos < ctx.src.Length)
      {
        if (ctx.src[ctx.pos] == '.')
        {
          ctx.sb.Append('.');
          ++ctx.pos;
          if (!JSONParser.readDigits(ctx))
            throw new JSONParser.InvalidNumberException(ctx);
        }
        if (ctx.pos < ctx.src.Length && (ctx.src[ctx.pos] == 'e' || ctx.src[ctx.pos] == 'E'))
        {
          ctx.sb.Append(ctx.src[ctx.pos]);
          ++ctx.pos;
          if (ctx.pos >= ctx.src.Length)
            throw new JSONParser.InvalidNumberException(ctx);
          if (ctx.src[ctx.pos] == '+' || ctx.src[ctx.pos] == '-')
          {
            ctx.sb.Append(ctx.src[ctx.pos]);
            ++ctx.pos;
          }
          else if (!char.IsDigit(ctx.src[ctx.pos]))
            throw new JSONParser.InvalidNumberException(ctx);
          if (!JSONParser.readDigits(ctx))
            throw new JSONParser.InvalidNumberException(ctx);
        }
      }
      if (ignoreDecimal)
        ctx.sb.Length = length;
      return true;
    }

    private static uint parseHex(JSONParser.Context ctx)
    {
      if (ctx.pos >= ctx.src.Length)
        throw new JSONParser.UnexpectedEOFException(ctx);
      char ch = ctx.src[ctx.pos];
      if ('0' <= ch && ch <= '9')
      {
        ++ctx.pos;
        return (uint) ch - 48U;
      }
      if ('A' <= ch && ch <= 'F')
      {
        ++ctx.pos;
        return (uint) ((int) ch - 65 + 10);
      }
      if ('a' > ch || ch > 'f')
        throw new JSONParser.InvalidCharacterException(ctx);
      ++ctx.pos;
      return (uint) ((int) ch - 97 + 10);
    }

    private static bool parseString(JSONParser.Context ctx)
    {
      if (ctx.pos >= ctx.src.Length || ctx.src[ctx.pos] != '"')
        throw new JSONParser.InvalidStringException(ctx);
      ++ctx.pos;
      ctx.sb.Length = 0;
      for (; ctx.pos < ctx.src.Length; ++ctx.pos)
      {
        if (char.IsControl(ctx.src[ctx.pos]))
          throw new JSONParser.InvalidCharacterException(ctx);
        if (ctx.src[ctx.pos] == '\\')
        {
          ++ctx.pos;
          if (ctx.pos >= ctx.src.Length)
            throw new JSONParser.InvalidStringException(ctx);
          char ch = ctx.src[ctx.pos];
          switch (ch)
          {
            case 'r':
              ctx.sb.Append('\r');
              continue;
            case 't':
              ctx.sb.Append('\t');
              continue;
            case 'u':
              ++ctx.pos;
              uint num = (uint) ((int) JSONParser.parseHex(ctx) << 12 | (int) JSONParser.parseHex(ctx) << 8 | (int) JSONParser.parseHex(ctx) << 4) | JSONParser.parseHex(ctx);
              ctx.sb.Append((char) num);
              --ctx.pos;
              continue;
            default:
              switch (ch)
              {
                case '"':
                  ctx.sb.Append('"');
                  continue;
                case '/':
                  ctx.sb.Append('/');
                  continue;
                case '\\':
                  ctx.sb.Append('\\');
                  continue;
                case 'b':
                  ctx.sb.Append('\b');
                  continue;
                case 'f':
                  ctx.sb.Append('\f');
                  continue;
                case 'n':
                  ctx.sb.Append('\n');
                  continue;
                default:
                  throw new JSONParser.InvalidStringException(ctx);
              }
          }
        }
        else
        {
          if (ctx.src[ctx.pos] == '"')
          {
            ++ctx.pos;
            break;
          }
          ctx.sb.Append(ctx.src[ctx.pos]);
        }
      }
      return true;
    }

    private static bool skipSpaces(JSONParser.Context ctx)
    {
      while (ctx.pos < ctx.src.Length && char.IsWhiteSpace(ctx.src[ctx.pos]))
        ++ctx.pos;
      return ctx.pos < ctx.src.Length;
    }

    private static void forward(JSONParser.Context ctx)
    {
      if (!JSONParser.skipSpaces(ctx))
        throw new JSONParser.UnexpectedEOFException(ctx);
    }

    private static bool match(JSONParser.Context ctx, char ch)
    {
      if ((int) ctx.src[ctx.pos] != (int) ch)
        return false;
      ++ctx.pos;
      return true;
    }

    private static string stringParser(JSONParser.Context ctx, Type type)
    {
      JSONParser.parseString(ctx);
      return ctx.sb.ToString();
    }

    private static object objectParser(JSONParser.Context ctx, Type type)
    {
      JSONParser.forward(ctx);
      if (JSONParser.match(ctx, 'n'))
      {
        if (ctx.pos + 3 > ctx.src.Length)
          throw new JSONParser.UnexpectedEOFException(ctx);
        if (ctx.src[ctx.pos] != 'u' || ctx.src[ctx.pos + 1] != 'l' || ctx.src[ctx.pos + 2] != 'l')
          throw new JSONParser.UnexpectedCharacterException(ctx);
        ctx.pos += 3;
        return (object) null;
      }
      object obj = (object) null;
      if (type != null)
        obj = Activator.CreateInstance(type);
      JSONParser.parseObject(ctx, obj);
      return obj;
    }

    private static byte byteParser(JSONParser.Context ctx, Type type)
    {
      JSONParser.parseNumber(ctx, true);
      return byte.Parse(ctx.sb.ToString());
    }

    private static short shortParser(JSONParser.Context ctx, Type type)
    {
      JSONParser.parseNumber(ctx, true);
      return short.Parse(ctx.sb.ToString());
    }

    private static int intParser(JSONParser.Context ctx, Type type)
    {
      JSONParser.parseNumber(ctx, true);
      return int.Parse(ctx.sb.ToString());
    }

    private static long longParser(JSONParser.Context ctx, Type type)
    {
      JSONParser.parseNumber(ctx, true);
      return long.Parse(ctx.sb.ToString());
    }

    private static float floatParser(JSONParser.Context ctx, Type type)
    {
      JSONParser.parseNumber(ctx, false);
      return float.Parse(ctx.sb.ToString());
    }

    private static double doubleParser(JSONParser.Context ctx, Type type)
    {
      JSONParser.parseNumber(ctx, false);
      return double.Parse(ctx.sb.ToString());
    }

    private static object parseValueArray<T>(JSONParser.Context ctx, JSONParser.ParseElement<T> parser)
    {
      JSONParser.forward(ctx);
      if (ctx.src[ctx.pos] != '[')
        throw new JSONParser.InvalidCharacterException(ctx);
      ++ctx.pos;
      Type type = typeof (T);
      int count = ctx.ab.Count;
      while (true)
      {
        JSONParser.forward(ctx);
        ctx.ab.Add((object) parser(ctx, type));
        JSONParser.forward(ctx);
        if (ctx.src[ctx.pos] == ',')
          ++ctx.pos;
        else
          break;
      }
      if (ctx.src[ctx.pos] != ']')
        throw new JSONParser.UnexpectedCharacterException(ctx);
      ++ctx.pos;
      T[] objArray = new T[ctx.ab.Count - count];
      for (int index = 0; index < objArray.Length; ++index)
        objArray[index] = (T) ctx.ab[index + count];
      ctx.ab.RemoveRange(count, ctx.ab.Count - count);
      return (object) objArray;
    }

    private static object parseObjectArray(JSONParser.Context ctx, JSONParser.ParseElement<object> parser, Type type)
    {
      JSONParser.forward(ctx);
      if (ctx.src[ctx.pos] != '[')
        throw new JSONParser.InvalidCharacterException(ctx);
      ++ctx.pos;
      int count = ctx.ab.Count;
      while (true)
      {
        JSONParser.forward(ctx);
        ctx.ab.Add(parser(ctx, type));
        JSONParser.forward(ctx);
        if (ctx.src[ctx.pos] == ',')
          ++ctx.pos;
        else
          break;
      }
      if (ctx.src[ctx.pos] != ']')
        throw new JSONParser.UnexpectedCharacterException(ctx);
      ++ctx.pos;
      Array array = (Array) null;
      if (type != null)
      {
        array = Array.CreateInstance(type, ctx.ab.Count - count);
        for (int index = 0; index < array.Length; ++index)
          array.SetValue(ctx.ab[index + count], index);
      }
      ctx.ab.RemoveRange(count, ctx.ab.Count - count);
      return (object) array;
    }

    private static object parseArray(JSONParser.Context ctx, Type elementType)
    {
      if (elementType == typeof (int))
      {
        JSONParser.Context ctx1 = ctx;
        // ISSUE: reference to a compiler-generated field
        if (JSONParser.\u003C\u003Ef__mg\u0024cache0 == null)
        {
          // ISSUE: reference to a compiler-generated field
          JSONParser.\u003C\u003Ef__mg\u0024cache0 = new JSONParser.ParseElement<int>(JSONParser.intParser);
        }
        // ISSUE: reference to a compiler-generated field
        JSONParser.ParseElement<int> fMgCache0 = JSONParser.\u003C\u003Ef__mg\u0024cache0;
        return JSONParser.parseValueArray<int>(ctx1, fMgCache0);
      }
      if (elementType == typeof (long))
      {
        JSONParser.Context ctx1 = ctx;
        // ISSUE: reference to a compiler-generated field
        if (JSONParser.\u003C\u003Ef__mg\u0024cache1 == null)
        {
          // ISSUE: reference to a compiler-generated field
          JSONParser.\u003C\u003Ef__mg\u0024cache1 = new JSONParser.ParseElement<long>(JSONParser.longParser);
        }
        // ISSUE: reference to a compiler-generated field
        JSONParser.ParseElement<long> fMgCache1 = JSONParser.\u003C\u003Ef__mg\u0024cache1;
        return JSONParser.parseValueArray<long>(ctx1, fMgCache1);
      }
      if (elementType == typeof (float))
      {
        JSONParser.Context ctx1 = ctx;
        // ISSUE: reference to a compiler-generated field
        if (JSONParser.\u003C\u003Ef__mg\u0024cache2 == null)
        {
          // ISSUE: reference to a compiler-generated field
          JSONParser.\u003C\u003Ef__mg\u0024cache2 = new JSONParser.ParseElement<float>(JSONParser.floatParser);
        }
        // ISSUE: reference to a compiler-generated field
        JSONParser.ParseElement<float> fMgCache2 = JSONParser.\u003C\u003Ef__mg\u0024cache2;
        return JSONParser.parseValueArray<float>(ctx1, fMgCache2);
      }
      if (elementType == typeof (double))
      {
        JSONParser.Context ctx1 = ctx;
        // ISSUE: reference to a compiler-generated field
        if (JSONParser.\u003C\u003Ef__mg\u0024cache3 == null)
        {
          // ISSUE: reference to a compiler-generated field
          JSONParser.\u003C\u003Ef__mg\u0024cache3 = new JSONParser.ParseElement<double>(JSONParser.doubleParser);
        }
        // ISSUE: reference to a compiler-generated field
        JSONParser.ParseElement<double> fMgCache3 = JSONParser.\u003C\u003Ef__mg\u0024cache3;
        return JSONParser.parseValueArray<double>(ctx1, fMgCache3);
      }
      if (elementType == typeof (short))
      {
        JSONParser.Context ctx1 = ctx;
        // ISSUE: reference to a compiler-generated field
        if (JSONParser.\u003C\u003Ef__mg\u0024cache4 == null)
        {
          // ISSUE: reference to a compiler-generated field
          JSONParser.\u003C\u003Ef__mg\u0024cache4 = new JSONParser.ParseElement<short>(JSONParser.shortParser);
        }
        // ISSUE: reference to a compiler-generated field
        JSONParser.ParseElement<short> fMgCache4 = JSONParser.\u003C\u003Ef__mg\u0024cache4;
        return JSONParser.parseValueArray<short>(ctx1, fMgCache4);
      }
      if (elementType == typeof (byte))
      {
        JSONParser.Context ctx1 = ctx;
        // ISSUE: reference to a compiler-generated field
        if (JSONParser.\u003C\u003Ef__mg\u0024cache5 == null)
        {
          // ISSUE: reference to a compiler-generated field
          JSONParser.\u003C\u003Ef__mg\u0024cache5 = new JSONParser.ParseElement<byte>(JSONParser.byteParser);
        }
        // ISSUE: reference to a compiler-generated field
        JSONParser.ParseElement<byte> fMgCache5 = JSONParser.\u003C\u003Ef__mg\u0024cache5;
        return JSONParser.parseValueArray<byte>(ctx1, fMgCache5);
      }
      if (elementType == typeof (string))
      {
        JSONParser.Context ctx1 = ctx;
        // ISSUE: reference to a compiler-generated field
        if (JSONParser.\u003C\u003Ef__mg\u0024cache6 == null)
        {
          // ISSUE: reference to a compiler-generated field
          JSONParser.\u003C\u003Ef__mg\u0024cache6 = new JSONParser.ParseElement<object>(JSONParser.stringParser);
        }
        // ISSUE: reference to a compiler-generated field
        JSONParser.ParseElement<object> fMgCache6 = JSONParser.\u003C\u003Ef__mg\u0024cache6;
        Type type = elementType;
        return JSONParser.parseObjectArray(ctx1, fMgCache6, type);
      }
      if (elementType != null && elementType.IsValueType)
        throw new JSONParser.UnsupportedTypeException(ctx);
      if (elementType == null)
      {
        JSONParser.Context ctx1 = ctx;
        // ISSUE: reference to a compiler-generated field
        if (JSONParser.\u003C\u003Ef__mg\u0024cache7 == null)
        {
          // ISSUE: reference to a compiler-generated field
          JSONParser.\u003C\u003Ef__mg\u0024cache7 = new JSONParser.ParseElement<object>(JSONParser.ignoreValue);
        }
        // ISSUE: reference to a compiler-generated field
        JSONParser.ParseElement<object> fMgCache7 = JSONParser.\u003C\u003Ef__mg\u0024cache7;
        // ISSUE: variable of the null type
        __Null local = null;
        return JSONParser.parseObjectArray(ctx1, fMgCache7, (Type) local);
      }
      JSONParser.Context ctx2 = ctx;
      // ISSUE: reference to a compiler-generated field
      if (JSONParser.\u003C\u003Ef__mg\u0024cache8 == null)
      {
        // ISSUE: reference to a compiler-generated field
        JSONParser.\u003C\u003Ef__mg\u0024cache8 = new JSONParser.ParseElement<object>(JSONParser.objectParser);
      }
      // ISSUE: reference to a compiler-generated field
      JSONParser.ParseElement<object> fMgCache8 = JSONParser.\u003C\u003Ef__mg\u0024cache8;
      Type type1 = elementType;
      return JSONParser.parseObjectArray(ctx2, fMgCache8, type1);
    }

    private static object ignoreValue(JSONParser.Context ctx, Type type)
    {
      if (ctx.src[ctx.pos] == '"')
        JSONParser.parseString(ctx);
      else if (ctx.src[ctx.pos] == '{')
        JSONParser.parseObject(ctx, (object) null);
      else if (ctx.src[ctx.pos] == '[')
        JSONParser.parseArray(ctx, (Type) null);
      else if (ctx.src[ctx.pos] == '-' || '0' <= ctx.src[ctx.pos] && ctx.src[ctx.pos] <= '9')
        JSONParser.parseNumber(ctx, false);
      return (object) false;
    }

    private static bool parseObject(JSONParser.Context ctx, object obj)
    {
      Type type = obj == null ? (Type) null : obj.GetType();
      JSONParser.forward(ctx);
      if (!JSONParser.match(ctx, '{'))
        throw new JSONParser.UnexpectedCharacterException(ctx);
      while (true)
      {
        JSONParser.forward(ctx);
        if (!JSONParser.match(ctx, '}'))
        {
          if (ctx.src[ctx.pos] == '"')
          {
            JSONParser.parseString(ctx);
            if (ctx.sb.Length > 0)
            {
              JSONParser.forward(ctx);
              if (ctx.src[ctx.pos] == ':')
              {
                ++ctx.pos;
                JSONParser.forward(ctx);
                FieldInfo fieldInfo = type == null ? (FieldInfo) null : type.GetField(ctx.sb.ToString());
                if (fieldInfo == null)
                {
                  if (ctx.src[ctx.pos] == '"')
                    JSONParser.parseString(ctx);
                  else if (ctx.src[ctx.pos] == '{')
                    JSONParser.parseObject(ctx, (object) null);
                  else if (ctx.src[ctx.pos] == '[')
                    JSONParser.parseArray(ctx, (Type) null);
                  else if (ctx.src[ctx.pos] == '-' || '0' <= ctx.src[ctx.pos] && ctx.src[ctx.pos] <= '9')
                    JSONParser.parseNumber(ctx, false);
                }
                else
                {
                  Type fieldType = fieldInfo.FieldType;
                  if (fieldType.IsValueType || fieldType == typeof (string))
                  {
                    if (fieldType == typeof (int))
                    {
                      JSONParser.parseNumber(ctx, true);
                      fieldInfo.SetValue(obj, (object) int.Parse(ctx.sb.ToString()));
                    }
                    else if (fieldType == typeof (long))
                    {
                      JSONParser.parseNumber(ctx, true);
                      fieldInfo.SetValue(obj, (object) long.Parse(ctx.sb.ToString()));
                    }
                    else if (fieldType == typeof (float))
                    {
                      JSONParser.parseNumber(ctx, false);
                      fieldInfo.SetValue(obj, (object) float.Parse(ctx.sb.ToString()));
                    }
                    else if (fieldType == typeof (double))
                    {
                      JSONParser.parseNumber(ctx, false);
                      fieldInfo.SetValue(obj, (object) double.Parse(ctx.sb.ToString()));
                    }
                    else if (fieldType == typeof (short))
                    {
                      JSONParser.parseNumber(ctx, true);
                      fieldInfo.SetValue(obj, (object) short.Parse(ctx.sb.ToString()));
                    }
                    else if (fieldType == typeof (byte))
                    {
                      JSONParser.parseNumber(ctx, true);
                      fieldInfo.SetValue(obj, (object) byte.Parse(ctx.sb.ToString()));
                    }
                    else if (fieldType == typeof (string))
                    {
                      JSONParser.parseString(ctx);
                      fieldInfo.SetValue(obj, (object) ctx.sb.ToString());
                    }
                    else
                      goto label_33;
                  }
                  else if (fieldType.IsArray)
                  {
                    if (ctx.src[ctx.pos] == '[' && ctx.src[ctx.pos + 1] == ']')
                    {
                      ctx.pos += 2;
                    }
                    else
                    {
                      Type elementType = fieldType.GetElementType();
                      object array = JSONParser.parseArray(ctx, elementType);
                      fieldInfo.SetValue(obj, array);
                    }
                  }
                  else if (ctx.src[ctx.pos] == '{' && ctx.src[ctx.pos + 1] == '}')
                  {
                    ctx.pos += 2;
                  }
                  else
                  {
                    object obj1;
                    if (JSONParser.match(ctx, 'n'))
                    {
                      if (ctx.pos + 3 <= ctx.src.Length)
                      {
                        if (ctx.src[ctx.pos] == 'u' && ctx.src[ctx.pos + 1] == 'l' && ctx.src[ctx.pos + 2] == 'l')
                        {
                          ctx.pos += 3;
                          obj1 = (object) null;
                        }
                        else
                          goto label_45;
                      }
                      else
                        goto label_42;
                    }
                    else
                    {
                      obj1 = Activator.CreateInstance(fieldType);
                      JSONParser.parseObject(ctx, obj1);
                    }
                    fieldInfo.SetValue(obj, obj1);
                  }
                }
                JSONParser.forward(ctx);
                if (ctx.src[ctx.pos] == ',')
                  ++ctx.pos;
                else
                  goto label_50;
              }
              else
                goto label_8;
            }
            else
              goto label_6;
          }
          else
            break;
        }
        else
          goto label_53;
      }
      throw new JSONParser.UnexpectedCharacterException(ctx);
label_6:
      throw new JSONParser.InvalidKeyException(ctx);
label_8:
      throw new JSONParser.UnexpectedCharacterException(ctx);
label_33:
      throw new JSONParser.UnsupportedTypeException(ctx);
label_42:
      throw new JSONParser.UnexpectedEOFException(ctx);
label_45:
      throw new JSONParser.UnexpectedCharacterException(ctx);
label_50:
      if (ctx.src[ctx.pos] != '}')
        throw new JSONParser.InvalidCharacterException(ctx);
      ++ctx.pos;
label_53:
      return true;
    }

    public class Context
    {
      public string src;
      public int pos;
      public StringBuilder sb;
      public ArrayList ab;

      public string history
      {
        get
        {
          this.sb.Length = 0;
          int pos = this.pos;
          if (pos < this.src.Length)
            ++pos;
          int num = pos - 20;
          if (num < 0)
            num = 0;
          for (int index = num; index < pos; ++index)
            this.sb.Append(this.src[index]);
          return this.sb.ToString();
        }
      }
    }

    public class JSONException : Exception
    {
      private JSONParser.Context mCtx;

      public JSONException(JSONParser.Context ctx)
        : base(ctx.history)
      {
        this.mCtx = ctx;
      }

      public override string Message
      {
        get
        {
          return "at position " + (object) this.mCtx.pos;
        }
      }
    }

    public class InvalidNumberException : JSONParser.JSONException
    {
      public InvalidNumberException(JSONParser.Context ctx)
        : base(ctx)
      {
      }
    }

    public class InvalidStringException : JSONParser.JSONException
    {
      public InvalidStringException(JSONParser.Context ctx)
        : base(ctx)
      {
      }
    }

    public class UnexpectedEOFException : JSONParser.JSONException
    {
      public UnexpectedEOFException(JSONParser.Context ctx)
        : base(ctx)
      {
      }
    }

    public class UnexpectedCharacterException : JSONParser.JSONException
    {
      public UnexpectedCharacterException(JSONParser.Context ctx)
        : base(ctx)
      {
      }
    }

    public class UnknownException : JSONParser.JSONException
    {
      public UnknownException(JSONParser.Context ctx)
        : base(ctx)
      {
      }
    }

    public class InvalidCharacterException : JSONParser.JSONException
    {
      public InvalidCharacterException(JSONParser.Context ctx)
        : base(ctx)
      {
      }
    }

    public class InvalidKeyException : JSONParser.JSONException
    {
      public InvalidKeyException(JSONParser.Context ctx)
        : base(ctx)
      {
      }
    }

    public class UnsupportedTypeException : JSONParser.JSONException
    {
      public UnsupportedTypeException(JSONParser.Context ctx)
        : base(ctx)
      {
      }
    }

    private delegate T ParseElement<T>(JSONParser.Context ctx, Type type);
  }
}
