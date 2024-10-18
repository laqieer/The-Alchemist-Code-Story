// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.ProtectSkillParamFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class ProtectSkillParamFormatter : 
    IMessagePackFormatter<ProtectSkillParam>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public ProtectSkillParamFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "Iname",
          0
        },
        {
          "Type",
          1
        },
        {
          "DamageType",
          2
        },
        {
          "Value",
          3
        },
        {
          "Range",
          4
        },
        {
          "Height",
          5
        },
        {
          "mIname",
          6
        },
        {
          "mType",
          7
        },
        {
          "mDamageType",
          8
        },
        {
          "mValue",
          9
        },
        {
          "mRange",
          10
        },
        {
          "mHeight",
          11
        },
        {
          "mInit",
          12
        },
        {
          "mMax",
          13
        }
      };
      this.____stringByteKeys = new byte[14][]
      {
        MessagePackBinary.GetEncodedStringBytes("Iname"),
        MessagePackBinary.GetEncodedStringBytes("Type"),
        MessagePackBinary.GetEncodedStringBytes("DamageType"),
        MessagePackBinary.GetEncodedStringBytes("Value"),
        MessagePackBinary.GetEncodedStringBytes("Range"),
        MessagePackBinary.GetEncodedStringBytes("Height"),
        MessagePackBinary.GetEncodedStringBytes("mIname"),
        MessagePackBinary.GetEncodedStringBytes("mType"),
        MessagePackBinary.GetEncodedStringBytes("mDamageType"),
        MessagePackBinary.GetEncodedStringBytes("mValue"),
        MessagePackBinary.GetEncodedStringBytes("mRange"),
        MessagePackBinary.GetEncodedStringBytes("mHeight"),
        MessagePackBinary.GetEncodedStringBytes("mInit"),
        MessagePackBinary.GetEncodedStringBytes("mMax")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      ProtectSkillParam value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 14);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.Iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<ProtectTypes>().Serialize(ref bytes, offset, value.Type, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<DamageTypes>().Serialize(ref bytes, offset, value.DamageType, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<SkillRankUpValue>().Serialize(ref bytes, offset, value.Value, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.Range);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.Height);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.mIname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += formatterResolver.GetFormatterWithVerify<ProtectTypes>().Serialize(ref bytes, offset, value.mType, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += formatterResolver.GetFormatterWithVerify<DamageTypes>().Serialize(ref bytes, offset, value.mDamageType, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += formatterResolver.GetFormatterWithVerify<SkillRankUpValue>().Serialize(ref bytes, offset, value.mValue, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mRange);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mHeight);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mInit);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mMax);
      return offset - num;
    }

    public ProtectSkillParam Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (ProtectSkillParam) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      string str1 = (string) null;
      ProtectTypes protectTypes1 = ProtectTypes.None;
      DamageTypes damageTypes1 = DamageTypes.None;
      SkillRankUpValue skillRankUpValue1 = (SkillRankUpValue) null;
      int num3 = 0;
      int num4 = 0;
      string str2 = (string) null;
      ProtectTypes protectTypes2 = ProtectTypes.None;
      DamageTypes damageTypes2 = DamageTypes.None;
      SkillRankUpValue skillRankUpValue2 = (SkillRankUpValue) null;
      int num5 = 0;
      int num6 = 0;
      int num7 = 0;
      int num8 = 0;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num9;
        if (!this.____keyMapping.TryGetValueSafe(key, out num9))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num9)
          {
            case 0:
              str1 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              protectTypes1 = formatterResolver.GetFormatterWithVerify<ProtectTypes>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              damageTypes1 = formatterResolver.GetFormatterWithVerify<DamageTypes>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              skillRankUpValue1 = formatterResolver.GetFormatterWithVerify<SkillRankUpValue>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 4:
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 5:
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 6:
              str2 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 7:
              protectTypes2 = formatterResolver.GetFormatterWithVerify<ProtectTypes>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 8:
              damageTypes2 = formatterResolver.GetFormatterWithVerify<DamageTypes>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 9:
              skillRankUpValue2 = formatterResolver.GetFormatterWithVerify<SkillRankUpValue>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 10:
              num5 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 11:
              num6 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 12:
              num7 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 13:
              num8 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new ProtectSkillParam()
      {
        mIname = str2,
        mType = protectTypes2,
        mDamageType = damageTypes2,
        mValue = skillRankUpValue2,
        mRange = num5,
        mHeight = num6,
        mInit = num7,
        mMax = num8
      };
    }
  }
}
