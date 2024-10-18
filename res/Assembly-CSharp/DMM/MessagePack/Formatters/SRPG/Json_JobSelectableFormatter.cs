// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.Json_JobSelectableFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class Json_JobSelectableFormatter : 
    IMessagePackFormatter<Json_JobSelectable>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public Json_JobSelectableFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "abils",
          0
        },
        {
          "artifacts",
          1
        },
        {
          "inspiration_skills",
          2
        }
      };
      this.____stringByteKeys = new byte[3][]
      {
        MessagePackBinary.GetEncodedStringBytes("abils"),
        MessagePackBinary.GetEncodedStringBytes("artifacts"),
        MessagePackBinary.GetEncodedStringBytes("inspiration_skills")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      Json_JobSelectable value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 3);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<long[]>().Serialize(ref bytes, offset, value.abils, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<long[]>().Serialize(ref bytes, offset, value.artifacts, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<Json_InspirationSkillExt[]>().Serialize(ref bytes, offset, value.inspiration_skills, formatterResolver);
      return offset - num;
    }

    public Json_JobSelectable Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (Json_JobSelectable) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      long[] numArray1 = (long[]) null;
      long[] numArray2 = (long[]) null;
      Json_InspirationSkillExt[] inspirationSkillExtArray = (Json_InspirationSkillExt[]) null;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num3;
        if (!this.____keyMapping.TryGetValueSafe(key, out num3))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num3)
          {
            case 0:
              numArray1 = formatterResolver.GetFormatterWithVerify<long[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              numArray2 = formatterResolver.GetFormatterWithVerify<long[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              inspirationSkillExtArray = formatterResolver.GetFormatterWithVerify<Json_InspirationSkillExt[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new Json_JobSelectable()
      {
        abils = numArray1,
        artifacts = numArray2,
        inspiration_skills = inspirationSkillExtArray
      };
    }
  }
}
