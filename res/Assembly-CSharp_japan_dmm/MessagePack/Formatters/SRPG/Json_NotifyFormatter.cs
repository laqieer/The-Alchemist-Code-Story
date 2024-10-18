// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.Json_NotifyFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class Json_NotifyFormatter : 
    IMessagePackFormatter<Json_Notify>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public Json_NotifyFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "bonus",
          0
        },
        {
          "logbonus",
          1
        },
        {
          "logbotables",
          2
        },
        {
          "supgold",
          3
        }
      };
      this.____stringByteKeys = new byte[4][]
      {
        MessagePackBinary.GetEncodedStringBytes("bonus"),
        MessagePackBinary.GetEncodedStringBytes("logbonus"),
        MessagePackBinary.GetEncodedStringBytes("logbotables"),
        MessagePackBinary.GetEncodedStringBytes("supgold")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      Json_Notify value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 4);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.bonus);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<Json_LoginBonus[]>().Serialize(ref bytes, offset, value.logbonus, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<Json_LoginBonusTable[]>().Serialize(ref bytes, offset, value.logbotables, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.supgold);
      return offset - num;
    }

    public Json_Notify Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (Json_Notify) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      int num3 = 0;
      Json_LoginBonus[] jsonLoginBonusArray = (Json_LoginBonus[]) null;
      Json_LoginBonusTable[] jsonLoginBonusTableArray = (Json_LoginBonusTable[]) null;
      int num4 = 0;
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
              jsonLoginBonusArray = formatterResolver.GetFormatterWithVerify<Json_LoginBonus[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              jsonLoginBonusTableArray = formatterResolver.GetFormatterWithVerify<Json_LoginBonusTable[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new Json_Notify()
      {
        bonus = num3,
        logbonus = jsonLoginBonusArray,
        logbotables = jsonLoginBonusTableArray,
        supgold = num4
      };
    }
  }
}
