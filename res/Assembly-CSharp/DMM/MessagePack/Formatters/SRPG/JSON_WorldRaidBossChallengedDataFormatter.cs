// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.JSON_WorldRaidBossChallengedDataFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class JSON_WorldRaidBossChallengedDataFormatter : 
    IMessagePackFormatter<JSON_WorldRaidBossChallengedData>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public JSON_WorldRaidBossChallengedDataFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "challenged",
          0
        },
        {
          "boss_iname",
          1
        },
        {
          "current_hp",
          2
        }
      };
      this.____stringByteKeys = new byte[3][]
      {
        MessagePackBinary.GetEncodedStringBytes("challenged"),
        MessagePackBinary.GetEncodedStringBytes("boss_iname"),
        MessagePackBinary.GetEncodedStringBytes("current_hp")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      JSON_WorldRaidBossChallengedData value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 3);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.challenged);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.boss_iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += MessagePackBinary.WriteInt64(ref bytes, offset, value.current_hp);
      return offset - num;
    }

    public JSON_WorldRaidBossChallengedData Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (JSON_WorldRaidBossChallengedData) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      int num3 = 0;
      string str = (string) null;
      long num4 = 0;
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
              str = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              num4 = MessagePackBinary.ReadInt64(bytes, offset, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      JSON_WorldRaidBossChallengedData bossChallengedData = new JSON_WorldRaidBossChallengedData();
      bossChallengedData.challenged = num3;
      bossChallengedData.boss_iname = str;
      bossChallengedData.current_hp = num4;
      return bossChallengedData;
    }
  }
}
