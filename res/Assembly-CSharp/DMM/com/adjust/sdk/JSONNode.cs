// Decompiled with JetBrains decompiler
// Type: com.adjust.sdk.JSONNode
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

#nullable disable
namespace com.adjust.sdk
{
  public class JSONNode
  {
    public virtual void Add(string aKey, JSONNode aItem)
    {
    }

    public virtual JSONNode this[int aIndex]
    {
      get => (JSONNode) null;
      set
      {
      }
    }

    public virtual JSONNode this[string aKey]
    {
      get => (JSONNode) null;
      set
      {
      }
    }

    public virtual string Value
    {
      get => string.Empty;
      set
      {
      }
    }

    public virtual int Count => 0;

    public virtual void Add(JSONNode aItem) => this.Add(string.Empty, aItem);

    public virtual JSONNode Remove(string aKey) => (JSONNode) null;

    public virtual JSONNode Remove(int aIndex) => (JSONNode) null;

    public virtual JSONNode Remove(JSONNode aNode) => aNode;

    public virtual IEnumerable<JSONNode> Childs
    {
      get
      {
        JSONNode.\u003C\u003Ec__Iterator0 cIterator0 = new JSONNode.\u003C\u003Ec__Iterator0();
        JSONNode.\u003C\u003Ec__Iterator0 childs = cIterator0;
        childs.\u0024PC = -2;
        return (IEnumerable<JSONNode>) childs;
      }
    }

    public IEnumerable<JSONNode> DeepChilds
    {
      get
      {
        JSONNode.\u003C\u003Ec__Iterator1 deepChilds = new JSONNode.\u003C\u003Ec__Iterator1()
        {
          \u0024this = this
        };
        deepChilds.\u0024PC = -2;
        return (IEnumerable<JSONNode>) deepChilds;
      }
    }

    public override string ToString() => nameof (JSONNode);

    public virtual string ToString(string aPrefix) => nameof (JSONNode);

    public virtual int AsInt
    {
      get
      {
        int result = 0;
        return int.TryParse(this.Value, out result) ? result : 0;
      }
      set => this.Value = value.ToString();
    }

    public virtual float AsFloat
    {
      get
      {
        float result = 0.0f;
        return float.TryParse(this.Value, out result) ? result : 0.0f;
      }
      set => this.Value = value.ToString();
    }

    public virtual double AsDouble
    {
      get
      {
        double result = 0.0;
        return double.TryParse(this.Value, out result) ? result : 0.0;
      }
      set => this.Value = value.ToString();
    }

    public virtual bool AsBool
    {
      get
      {
        bool result = false;
        return bool.TryParse(this.Value, out result) ? result : !string.IsNullOrEmpty(this.Value);
      }
      set => this.Value = !value ? "false" : "true";
    }

    public virtual JSONArray AsArray => this as JSONArray;

    public virtual JSONClass AsObject => this as JSONClass;

    public static implicit operator JSONNode(string s) => (JSONNode) new JSONData(s);

    public static implicit operator string(JSONNode d)
    {
      return d == (object) null ? (string) null : d.Value;
    }

    public static bool operator ==(JSONNode a, object b)
    {
      return b == null && (object) (a as JSONLazyCreator) != null || object.ReferenceEquals((object) a, b);
    }

    public static bool operator !=(JSONNode a, object b) => !(a == b);

    public override bool Equals(object obj) => object.ReferenceEquals((object) this, obj);

    public override int GetHashCode() => base.GetHashCode();

    internal static string Escape(string aText)
    {
      string str = string.Empty;
      foreach (char ch in aText)
      {
        switch (ch)
        {
          case '\b':
            str += "\\b";
            break;
          case '\t':
            str += "\\t";
            break;
          case '\n':
            str += "\\n";
            break;
          case '\f':
            str += "\\f";
            break;
          case '\r':
            str += "\\r";
            break;
          default:
            str = ch == '"' ? str + "\\\"" : (ch == '\\' ? str + "\\\\" : str + (object) ch);
            break;
        }
      }
      return str;
    }

    public static JSONNode Parse(string aJSON)
    {
      Stack<JSONNode> jsonNodeStack = new Stack<JSONNode>();
      JSONNode jsonNode = (JSONNode) null;
      int index = 0;
      string aItem = string.Empty;
      string aKey1 = string.Empty;
      bool flag = false;
      for (; index < aJSON.Length; ++index)
      {
        char ch1 = aJSON[index];
        switch (ch1)
        {
          case '\t':
label_44:
            if (flag)
            {
              aItem += (string) (object) aJSON[index];
              continue;
            }
            continue;
          case '\n':
          case '\r':
            continue;
          default:
            switch (ch1)
            {
              case ' ':
                goto label_44;
              case '"':
                flag = ((flag ? 1 : 0) ^ 1) != 0;
                continue;
              case '[':
                if (flag)
                {
                  aItem += (string) (object) aJSON[index];
                  continue;
                }
                jsonNodeStack.Push((JSONNode) new JSONArray());
                if (jsonNode != (object) null)
                {
                  string aKey2 = aKey1.Trim();
                  if (jsonNode is JSONArray)
                    jsonNode.Add(jsonNodeStack.Peek());
                  else if (aKey2 != string.Empty)
                    jsonNode.Add(aKey2, jsonNodeStack.Peek());
                }
                aKey1 = string.Empty;
                aItem = string.Empty;
                jsonNode = jsonNodeStack.Peek();
                continue;
              case '\\':
                ++index;
                if (flag)
                {
                  char ch2 = aJSON[index];
                  switch (ch2)
                  {
                    case 'r':
                      aItem += (string) (object) '\r';
                      continue;
                    case 't':
                      aItem += (string) (object) '\t';
                      continue;
                    case 'u':
                      string s = aJSON.Substring(index + 1, 4);
                      aItem += (string) (object) (char) int.Parse(s, NumberStyles.AllowHexSpecifier);
                      index += 4;
                      continue;
                    default:
                      aItem = ch2 == 'b' ? aItem + (object) '\b' : (ch2 == 'f' ? aItem + (object) '\f' : (ch2 == 'n' ? aItem + (object) '\n' : aItem + (object) ch2));
                      continue;
                  }
                }
                else
                  continue;
              case ']':
label_21:
                if (flag)
                {
                  aItem += (string) (object) aJSON[index];
                  continue;
                }
                if (jsonNodeStack.Count == 0)
                  throw new Exception("JSON Parse: Too many closing brackets");
                jsonNodeStack.Pop();
                if (aItem != string.Empty)
                {
                  string aKey3 = aKey1.Trim();
                  if (jsonNode is JSONArray)
                    jsonNode.Add((JSONNode) aItem);
                  else if (aKey3 != string.Empty)
                    jsonNode.Add(aKey3, (JSONNode) aItem);
                }
                aKey1 = string.Empty;
                aItem = string.Empty;
                if (jsonNodeStack.Count > 0)
                {
                  jsonNode = jsonNodeStack.Peek();
                  continue;
                }
                continue;
              default:
                switch (ch1)
                {
                  case '{':
                    if (flag)
                    {
                      aItem += (string) (object) aJSON[index];
                      continue;
                    }
                    jsonNodeStack.Push((JSONNode) new JSONClass());
                    if (jsonNode != (object) null)
                    {
                      string aKey4 = aKey1.Trim();
                      if (jsonNode is JSONArray)
                        jsonNode.Add(jsonNodeStack.Peek());
                      else if (aKey4 != string.Empty)
                        jsonNode.Add(aKey4, jsonNodeStack.Peek());
                    }
                    aKey1 = string.Empty;
                    aItem = string.Empty;
                    jsonNode = jsonNodeStack.Peek();
                    continue;
                  case '}':
                    goto label_21;
                  default:
                    switch (ch1)
                    {
                      case ',':
                        if (flag)
                        {
                          aItem += (string) (object) aJSON[index];
                          continue;
                        }
                        if (aItem != string.Empty)
                        {
                          if (jsonNode is JSONArray)
                            jsonNode.Add((JSONNode) aItem);
                          else if (aKey1 != string.Empty)
                            jsonNode.Add(aKey1, (JSONNode) aItem);
                        }
                        aKey1 = string.Empty;
                        aItem = string.Empty;
                        continue;
                      case ':':
                        if (flag)
                        {
                          aItem += (string) (object) aJSON[index];
                          continue;
                        }
                        aKey1 = aItem;
                        aItem = string.Empty;
                        continue;
                      default:
                        aItem += (string) (object) aJSON[index];
                        continue;
                    }
                }
            }
        }
      }
      if (flag)
        throw new Exception("JSON Parse: Quotation marks seems to be messed up.");
      return jsonNode;
    }

    public virtual void Serialize(BinaryWriter aWriter)
    {
    }

    public void SaveToStream(Stream aData) => this.Serialize(new BinaryWriter(aData));

    public void SaveToCompressedStream(Stream aData)
    {
      throw new Exception("Can't use compressed functions. You need include the SharpZipLib and uncomment the define at the top of SimpleJSON");
    }

    public void SaveToCompressedFile(string aFileName)
    {
      throw new Exception("Can't use compressed functions. You need include the SharpZipLib and uncomment the define at the top of SimpleJSON");
    }

    public string SaveToCompressedBase64()
    {
      throw new Exception("Can't use compressed functions. You need include the SharpZipLib and uncomment the define at the top of SimpleJSON");
    }

    public static JSONNode Deserialize(BinaryReader aReader)
    {
      JSONBinaryTag jsonBinaryTag = (JSONBinaryTag) aReader.ReadByte();
      switch (jsonBinaryTag)
      {
        case JSONBinaryTag.Array:
          int num1 = aReader.ReadInt32();
          JSONArray jsonArray = new JSONArray();
          for (int index = 0; index < num1; ++index)
            jsonArray.Add(JSONNode.Deserialize(aReader));
          return (JSONNode) jsonArray;
        case JSONBinaryTag.Class:
          int num2 = aReader.ReadInt32();
          JSONClass jsonClass = new JSONClass();
          for (int index = 0; index < num2; ++index)
          {
            string aKey = aReader.ReadString();
            JSONNode aItem = JSONNode.Deserialize(aReader);
            jsonClass.Add(aKey, aItem);
          }
          return (JSONNode) jsonClass;
        case JSONBinaryTag.Value:
          return (JSONNode) new JSONData(aReader.ReadString());
        case JSONBinaryTag.IntValue:
          return (JSONNode) new JSONData(aReader.ReadInt32());
        case JSONBinaryTag.DoubleValue:
          return (JSONNode) new JSONData(aReader.ReadDouble());
        case JSONBinaryTag.BoolValue:
          return (JSONNode) new JSONData(aReader.ReadBoolean());
        case JSONBinaryTag.FloatValue:
          return (JSONNode) new JSONData(aReader.ReadSingle());
        default:
          throw new Exception("Error deserializing JSON. Unknown tag: " + (object) jsonBinaryTag);
      }
    }

    public static JSONNode LoadFromCompressedFile(string aFileName)
    {
      throw new Exception("Can't use compressed functions. You need include the SharpZipLib and uncomment the define at the top of SimpleJSON");
    }

    public static JSONNode LoadFromCompressedStream(Stream aData)
    {
      throw new Exception("Can't use compressed functions. You need include the SharpZipLib and uncomment the define at the top of SimpleJSON");
    }

    public static JSONNode LoadFromCompressedBase64(string aBase64)
    {
      throw new Exception("Can't use compressed functions. You need include the SharpZipLib and uncomment the define at the top of SimpleJSON");
    }

    public static JSONNode LoadFromStream(Stream aData)
    {
      using (BinaryReader aReader = new BinaryReader(aData))
        return JSONNode.Deserialize(aReader);
    }

    public static JSONNode LoadFromBase64(string aBase64)
    {
      MemoryStream aData = new MemoryStream(Convert.FromBase64String(aBase64));
      aData.Position = 0L;
      return JSONNode.LoadFromStream((Stream) aData);
    }
  }
}
