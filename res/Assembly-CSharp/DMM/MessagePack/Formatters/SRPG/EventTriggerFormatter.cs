// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.EventTriggerFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class EventTriggerFormatter : 
    IMessagePackFormatter<EventTrigger>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public EventTriggerFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "Trigger",
          0
        },
        {
          "EventType",
          1
        },
        {
          "GimmickType",
          2
        },
        {
          "StrValue",
          3
        },
        {
          "IntValue",
          4
        },
        {
          "Count",
          5
        },
        {
          "Tag",
          6
        },
        {
          "IsTriggerWithdraw",
          7
        }
      };
      this.____stringByteKeys = new byte[8][]
      {
        MessagePackBinary.GetEncodedStringBytes("Trigger"),
        MessagePackBinary.GetEncodedStringBytes("EventType"),
        MessagePackBinary.GetEncodedStringBytes("GimmickType"),
        MessagePackBinary.GetEncodedStringBytes("StrValue"),
        MessagePackBinary.GetEncodedStringBytes("IntValue"),
        MessagePackBinary.GetEncodedStringBytes("Count"),
        MessagePackBinary.GetEncodedStringBytes("Tag"),
        MessagePackBinary.GetEncodedStringBytes("IsTriggerWithdraw")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      EventTrigger value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 8);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<EEventTrigger>().Serialize(ref bytes, offset, value.Trigger, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<EEventType>().Serialize(ref bytes, offset, value.EventType, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<EEventGimmick>().Serialize(ref bytes, offset, value.GimmickType, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.StrValue, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.IntValue);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.Count);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.Tag, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsTriggerWithdraw);
      return offset - num;
    }

    public EventTrigger Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (EventTrigger) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      EEventTrigger eeventTrigger = EEventTrigger.Stop;
      EEventType eeventType = EEventType.None;
      EEventGimmick eeventGimmick = EEventGimmick.None;
      string str1 = (string) null;
      int num3 = 0;
      int num4 = 0;
      string str2 = (string) null;
      bool flag = false;
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
              eeventTrigger = formatterResolver.GetFormatterWithVerify<EEventTrigger>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              eeventType = formatterResolver.GetFormatterWithVerify<EEventType>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              eeventGimmick = formatterResolver.GetFormatterWithVerify<EEventGimmick>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              str1 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
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
              flag = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new EventTrigger() { Count = num4 };
    }
  }
}
