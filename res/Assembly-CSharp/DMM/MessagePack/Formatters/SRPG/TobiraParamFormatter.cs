// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.TobiraParamFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class TobiraParamFormatter : 
    IMessagePackFormatter<TobiraParam>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public TobiraParamFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "UnitIname",
          0
        },
        {
          "Enable",
          1
        },
        {
          "TobiraCategory",
          2
        },
        {
          "RecipeId",
          3
        },
        {
          "SkillIname",
          4
        },
        {
          "LeanAbilityParam",
          5
        },
        {
          "OverwriteLeaderSkillIname",
          6
        },
        {
          "OverwriteLeaderSkillLevel",
          7
        },
        {
          "Priority",
          8
        },
        {
          "HasLeaerSkill",
          9
        },
        {
          "IsUnlockConceptCardSlot2",
          10
        }
      };
      this.____stringByteKeys = new byte[11][]
      {
        MessagePackBinary.GetEncodedStringBytes("UnitIname"),
        MessagePackBinary.GetEncodedStringBytes("Enable"),
        MessagePackBinary.GetEncodedStringBytes("TobiraCategory"),
        MessagePackBinary.GetEncodedStringBytes("RecipeId"),
        MessagePackBinary.GetEncodedStringBytes("SkillIname"),
        MessagePackBinary.GetEncodedStringBytes("LeanAbilityParam"),
        MessagePackBinary.GetEncodedStringBytes("OverwriteLeaderSkillIname"),
        MessagePackBinary.GetEncodedStringBytes("OverwriteLeaderSkillLevel"),
        MessagePackBinary.GetEncodedStringBytes("Priority"),
        MessagePackBinary.GetEncodedStringBytes("HasLeaerSkill"),
        MessagePackBinary.GetEncodedStringBytes("IsUnlockConceptCardSlot2")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      TobiraParam value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 11);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.UnitIname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.Enable);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<TobiraParam.Category>().Serialize(ref bytes, offset, value.TobiraCategory, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.RecipeId, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.SkillIname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += formatterResolver.GetFormatterWithVerify<TobiraLearnAbilityParam[]>().Serialize(ref bytes, offset, value.LeanAbilityParam, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.OverwriteLeaderSkillIname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.OverwriteLeaderSkillLevel);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.Priority);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.HasLeaerSkill);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsUnlockConceptCardSlot2);
      return offset - num;
    }

    public TobiraParam Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (TobiraParam) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      string str1 = (string) null;
      bool flag1 = false;
      TobiraParam.Category category = TobiraParam.Category.START;
      string str2 = (string) null;
      string str3 = (string) null;
      TobiraLearnAbilityParam[] learnAbilityParamArray = (TobiraLearnAbilityParam[]) null;
      string str4 = (string) null;
      int num3 = 0;
      int num4 = 0;
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
              str1 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              flag1 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 2:
              category = formatterResolver.GetFormatterWithVerify<TobiraParam.Category>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              str2 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 4:
              str3 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 5:
              learnAbilityParamArray = formatterResolver.GetFormatterWithVerify<TobiraLearnAbilityParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 6:
              str4 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 7:
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 8:
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 9:
              flag2 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 10:
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
      return new TobiraParam();
    }
  }
}
