﻿// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.JSON_VersusStreakWinBonusFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class JSON_VersusStreakWinBonusFormatter : 
    IMessagePackFormatter<JSON_VersusStreakWinBonus>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public JSON_VersusStreakWinBonusFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "id",
          0
        },
        {
          "wincnt",
          1
        },
        {
          "rewards",
          2
        }
      };
      this.____stringByteKeys = new byte[3][]
      {
        MessagePackBinary.GetEncodedStringBytes("id"),
        MessagePackBinary.GetEncodedStringBytes("wincnt"),
        MessagePackBinary.GetEncodedStringBytes("rewards")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      JSON_VersusStreakWinBonus value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 3);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.id);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.wincnt);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_VersusWinBonusReward[]>().Serialize(ref bytes, offset, value.rewards, formatterResolver);
      return offset - num;
    }

    public JSON_VersusStreakWinBonus Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (JSON_VersusStreakWinBonus) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      int num3 = 0;
      int num4 = 0;
      JSON_VersusWinBonusReward[] versusWinBonusRewardArray = (JSON_VersusWinBonusReward[]) null;
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
              versusWinBonusRewardArray = formatterResolver.GetFormatterWithVerify<JSON_VersusWinBonusReward[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new JSON_VersusStreakWinBonus()
      {
        id = num3,
        wincnt = num4,
        rewards = versusWinBonusRewardArray
      };
    }
  }
}
