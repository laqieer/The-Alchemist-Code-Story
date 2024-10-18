// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.InspirationSkillDataFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class InspirationSkillDataFormatter : 
    IMessagePackFormatter<InspirationSkillData>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public InspirationSkillDataFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "UniqueID",
          0
        },
        {
          "Slot",
          1
        },
        {
          "InspSkillParam",
          2
        },
        {
          "Lv",
          3
        },
        {
          "IsSet",
          4
        },
        {
          "AbilityData",
          5
        }
      };
      this.____stringByteKeys = new byte[6][]
      {
        MessagePackBinary.GetEncodedStringBytes("UniqueID"),
        MessagePackBinary.GetEncodedStringBytes("Slot"),
        MessagePackBinary.GetEncodedStringBytes("InspSkillParam"),
        MessagePackBinary.GetEncodedStringBytes("Lv"),
        MessagePackBinary.GetEncodedStringBytes("IsSet"),
        MessagePackBinary.GetEncodedStringBytes("AbilityData")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      InspirationSkillData value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 6);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<OLong>().Serialize(ref bytes, offset, value.UniqueID, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.Slot, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<InspSkillParam>().Serialize(ref bytes, offset, value.InspSkillParam, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.Lv, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<OBool>().Serialize(ref bytes, offset, value.IsSet, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += formatterResolver.GetFormatterWithVerify<AbilityData>().Serialize(ref bytes, offset, value.AbilityData, formatterResolver);
      return offset - num;
    }

    public InspirationSkillData Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (InspirationSkillData) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      OLong olong = new OLong();
      OInt oint1 = new OInt();
      InspSkillParam inspSkillParam = (InspSkillParam) null;
      OInt oint2 = new OInt();
      OBool obool = new OBool();
      AbilityData abilityData = (AbilityData) null;
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
              olong = formatterResolver.GetFormatterWithVerify<OLong>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              oint1 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              inspSkillParam = formatterResolver.GetFormatterWithVerify<InspSkillParam>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              oint2 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 4:
              obool = formatterResolver.GetFormatterWithVerify<OBool>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 5:
              abilityData = formatterResolver.GetFormatterWithVerify<AbilityData>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new InspirationSkillData();
    }
  }
}
