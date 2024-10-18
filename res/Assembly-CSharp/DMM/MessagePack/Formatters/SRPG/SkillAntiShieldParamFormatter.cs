// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.SkillAntiShieldParamFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class SkillAntiShieldParamFormatter : 
    IMessagePackFormatter<SkillAntiShieldParam>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public SkillAntiShieldParamFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "Iname",
          0
        },
        {
          "IsIgnore",
          1
        },
        {
          "IgnoreRate",
          2
        },
        {
          "IsDestroy",
          3
        },
        {
          "DestroyRate",
          4
        }
      };
      this.____stringByteKeys = new byte[5][]
      {
        MessagePackBinary.GetEncodedStringBytes("Iname"),
        MessagePackBinary.GetEncodedStringBytes("IsIgnore"),
        MessagePackBinary.GetEncodedStringBytes("IgnoreRate"),
        MessagePackBinary.GetEncodedStringBytes("IsDestroy"),
        MessagePackBinary.GetEncodedStringBytes("DestroyRate")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      SkillAntiShieldParam value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 5);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.Iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsIgnore);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<SkillRankUpValueShort>().Serialize(ref bytes, offset, value.IgnoreRate, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsDestroy);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<SkillRankUpValueShort>().Serialize(ref bytes, offset, value.DestroyRate, formatterResolver);
      return offset - num;
    }

    public SkillAntiShieldParam Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (SkillAntiShieldParam) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      string str = (string) null;
      bool flag1 = false;
      SkillRankUpValueShort rankUpValueShort1 = (SkillRankUpValueShort) null;
      bool flag2 = false;
      SkillRankUpValueShort rankUpValueShort2 = (SkillRankUpValueShort) null;
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
              str = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              flag1 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 2:
              rankUpValueShort1 = formatterResolver.GetFormatterWithVerify<SkillRankUpValueShort>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              flag2 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 4:
              rankUpValueShort2 = formatterResolver.GetFormatterWithVerify<SkillRankUpValueShort>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new SkillAntiShieldParam();
    }
  }
}
