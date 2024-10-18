// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.InspSkillParamFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;
using System.Collections.Generic;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class InspSkillParamFormatter : 
    IMessagePackFormatter<InspSkillParam>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public InspSkillParamFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "Ability",
          0
        },
        {
          "Skill",
          1
        },
        {
          "Iname",
          2
        },
        {
          "SkillID",
          3
        },
        {
          "AbilityID",
          4
        },
        {
          "Triggers",
          5
        },
        {
          "CtrMin",
          6
        },
        {
          "CtrMax",
          7
        },
        {
          "LvCap",
          8
        },
        {
          "GenId",
          9
        },
        {
          "Derivation",
          10
        },
        {
          "EnablePassive",
          11
        },
        {
          "Parent",
          12
        },
        {
          "IsBase",
          13
        }
      };
      this.____stringByteKeys = new byte[14][]
      {
        MessagePackBinary.GetEncodedStringBytes("Ability"),
        MessagePackBinary.GetEncodedStringBytes("Skill"),
        MessagePackBinary.GetEncodedStringBytes("Iname"),
        MessagePackBinary.GetEncodedStringBytes("SkillID"),
        MessagePackBinary.GetEncodedStringBytes("AbilityID"),
        MessagePackBinary.GetEncodedStringBytes("Triggers"),
        MessagePackBinary.GetEncodedStringBytes("CtrMin"),
        MessagePackBinary.GetEncodedStringBytes("CtrMax"),
        MessagePackBinary.GetEncodedStringBytes("LvCap"),
        MessagePackBinary.GetEncodedStringBytes("GenId"),
        MessagePackBinary.GetEncodedStringBytes("Derivation"),
        MessagePackBinary.GetEncodedStringBytes("EnablePassive"),
        MessagePackBinary.GetEncodedStringBytes("Parent"),
        MessagePackBinary.GetEncodedStringBytes("IsBase")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      InspSkillParam value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 14);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<AbilityParam>().Serialize(ref bytes, offset, value.Ability, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<SkillParam>().Serialize(ref bytes, offset, value.Skill, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.Iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.SkillID, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.AbilityID, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += formatterResolver.GetFormatterWithVerify<List<InspSkillTriggerParam>>().Serialize(ref bytes, offset, value.Triggers, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.CtrMin);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.CtrMax);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.LvCap);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.GenId);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += formatterResolver.GetFormatterWithVerify<List<InspSkillParam>>().Serialize(ref bytes, offset, value.Derivation, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.EnablePassive);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
      offset += formatterResolver.GetFormatterWithVerify<InspSkillParam>().Serialize(ref bytes, offset, value.Parent, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsBase);
      return offset - num;
    }

    public InspSkillParam Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (InspSkillParam) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      AbilityParam abilityParam = (AbilityParam) null;
      SkillParam skillParam = (SkillParam) null;
      string str1 = (string) null;
      string str2 = (string) null;
      string str3 = (string) null;
      List<InspSkillTriggerParam> skillTriggerParamList = (List<InspSkillTriggerParam>) null;
      int num3 = 0;
      int num4 = 0;
      int num5 = 0;
      int num6 = 0;
      List<InspSkillParam> inspSkillParamList = (List<InspSkillParam>) null;
      bool flag1 = false;
      InspSkillParam inspSkillParam = (InspSkillParam) null;
      bool flag2 = false;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num7;
        if (!this.____keyMapping.TryGetValueSafe(key, out num7))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num7)
          {
            case 0:
              abilityParam = formatterResolver.GetFormatterWithVerify<AbilityParam>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              skillParam = formatterResolver.GetFormatterWithVerify<SkillParam>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              str1 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              str2 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 4:
              str3 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 5:
              skillTriggerParamList = formatterResolver.GetFormatterWithVerify<List<InspSkillTriggerParam>>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 6:
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 7:
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 8:
              num5 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 9:
              num6 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 10:
              inspSkillParamList = formatterResolver.GetFormatterWithVerify<List<InspSkillParam>>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 11:
              flag1 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 12:
              inspSkillParam = formatterResolver.GetFormatterWithVerify<InspSkillParam>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 13:
              flag2 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new InspSkillParam();
    }
  }
}
