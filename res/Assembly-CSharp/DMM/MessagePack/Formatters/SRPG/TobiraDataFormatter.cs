// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.TobiraDataFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class TobiraDataFormatter : IMessagePackFormatter<TobiraData>, IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public TobiraDataFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "Lv",
          0
        },
        {
          "ViewLv",
          1
        },
        {
          "ParameterBuffSkill",
          2
        },
        {
          "LearnedLeaderSkillIname",
          3
        },
        {
          "IsUnlocked",
          4
        },
        {
          "Param",
          5
        },
        {
          "IsLearnedLeaderSkill",
          6
        },
        {
          "IsMaxLv",
          7
        }
      };
      this.____stringByteKeys = new byte[8][]
      {
        MessagePackBinary.GetEncodedStringBytes("Lv"),
        MessagePackBinary.GetEncodedStringBytes("ViewLv"),
        MessagePackBinary.GetEncodedStringBytes("ParameterBuffSkill"),
        MessagePackBinary.GetEncodedStringBytes("LearnedLeaderSkillIname"),
        MessagePackBinary.GetEncodedStringBytes("IsUnlocked"),
        MessagePackBinary.GetEncodedStringBytes("Param"),
        MessagePackBinary.GetEncodedStringBytes("IsLearnedLeaderSkill"),
        MessagePackBinary.GetEncodedStringBytes("IsMaxLv")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      TobiraData value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 8);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.Lv);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ViewLv);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<SkillData>().Serialize(ref bytes, offset, value.ParameterBuffSkill, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.LearnedLeaderSkillIname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsUnlocked);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += formatterResolver.GetFormatterWithVerify<TobiraParam>().Serialize(ref bytes, offset, value.Param, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsLearnedLeaderSkill);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsMaxLv);
      return offset - num;
    }

    public TobiraData Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (TobiraData) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      int num3 = 0;
      int num4 = 0;
      SkillData skillData = (SkillData) null;
      string str = (string) null;
      bool flag1 = false;
      TobiraParam tobiraParam = (TobiraParam) null;
      bool flag2 = false;
      bool flag3 = false;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num5;
        if (!this.____keyMapping.TryGetValueSafe(key, out num5))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num5)
          {
            case 0:
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 1:
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 2:
              skillData = formatterResolver.GetFormatterWithVerify<SkillData>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              str = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 4:
              flag1 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 5:
              tobiraParam = formatterResolver.GetFormatterWithVerify<TobiraParam>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 6:
              flag2 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 7:
              flag3 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new TobiraData() { Lv = num3 };
    }
  }
}
