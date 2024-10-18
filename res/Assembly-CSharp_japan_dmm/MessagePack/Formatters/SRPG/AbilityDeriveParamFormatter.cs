// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.AbilityDeriveParamFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class AbilityDeriveParamFormatter : 
    IMessagePackFormatter<AbilityDeriveParam>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public AbilityDeriveParamFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "BaseAbilityIname",
          0
        },
        {
          "DeriveAbilityIname",
          1
        },
        {
          "BaseAbilityName",
          2
        },
        {
          "DeriveAbilityName",
          3
        },
        {
          "MasterIndex",
          4
        },
        {
          "m_SkillAbilityDeriveParam",
          5
        },
        {
          "m_BaseParam",
          6
        },
        {
          "m_DeriveParam",
          7
        }
      };
      this.____stringByteKeys = new byte[8][]
      {
        MessagePackBinary.GetEncodedStringBytes("BaseAbilityIname"),
        MessagePackBinary.GetEncodedStringBytes("DeriveAbilityIname"),
        MessagePackBinary.GetEncodedStringBytes("BaseAbilityName"),
        MessagePackBinary.GetEncodedStringBytes("DeriveAbilityName"),
        MessagePackBinary.GetEncodedStringBytes("MasterIndex"),
        MessagePackBinary.GetEncodedStringBytes("m_SkillAbilityDeriveParam"),
        MessagePackBinary.GetEncodedStringBytes("m_BaseParam"),
        MessagePackBinary.GetEncodedStringBytes("m_DeriveParam")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      AbilityDeriveParam value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 8);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.BaseAbilityIname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.DeriveAbilityIname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.BaseAbilityName, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.DeriveAbilityName, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.MasterIndex);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += formatterResolver.GetFormatterWithVerify<SkillAbilityDeriveParam>().Serialize(ref bytes, offset, value.m_SkillAbilityDeriveParam, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += formatterResolver.GetFormatterWithVerify<AbilityParam>().Serialize(ref bytes, offset, value.m_BaseParam, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += formatterResolver.GetFormatterWithVerify<AbilityParam>().Serialize(ref bytes, offset, value.m_DeriveParam, formatterResolver);
      return offset - num;
    }

    public AbilityDeriveParam Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (AbilityDeriveParam) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      string str1 = (string) null;
      string str2 = (string) null;
      string str3 = (string) null;
      string str4 = (string) null;
      int num3 = 0;
      SkillAbilityDeriveParam abilityDeriveParam1 = (SkillAbilityDeriveParam) null;
      AbilityParam abilityParam1 = (AbilityParam) null;
      AbilityParam abilityParam2 = (AbilityParam) null;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num4;
        if (!this.____keyMapping.TryGetValueSafe(key, out num4))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num4)
          {
            case 0:
              str1 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              str2 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              str3 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              str4 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 4:
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 5:
              abilityDeriveParam1 = formatterResolver.GetFormatterWithVerify<SkillAbilityDeriveParam>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 6:
              abilityParam1 = formatterResolver.GetFormatterWithVerify<AbilityParam>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 7:
              abilityParam2 = formatterResolver.GetFormatterWithVerify<AbilityParam>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      AbilityDeriveParam abilityDeriveParam2 = new AbilityDeriveParam();
      abilityDeriveParam2.m_SkillAbilityDeriveParam = abilityDeriveParam1;
      abilityDeriveParam2.m_BaseParam = abilityParam1;
      abilityDeriveParam2.m_DeriveParam = abilityParam2;
      return abilityDeriveParam2;
    }
  }
}
