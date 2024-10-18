// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.ReqTrophyStarMission_StarMissionFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class ReqTrophyStarMission_StarMissionFormatter : 
    IMessagePackFormatter<ReqTrophyStarMission.StarMission>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public ReqTrophyStarMission_StarMissionFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "daily",
          0
        },
        {
          "weekly",
          1
        }
      };
      this.____stringByteKeys = new byte[2][]
      {
        MessagePackBinary.GetEncodedStringBytes("daily"),
        MessagePackBinary.GetEncodedStringBytes("weekly")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      ReqTrophyStarMission.StarMission value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 2);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<ReqTrophyStarMission.StarMission.Info>().Serialize(ref bytes, offset, value.daily, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<ReqTrophyStarMission.StarMission.Info>().Serialize(ref bytes, offset, value.weekly, formatterResolver);
      return offset - num;
    }

    public ReqTrophyStarMission.StarMission Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (ReqTrophyStarMission.StarMission) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      ReqTrophyStarMission.StarMission.Info info1 = (ReqTrophyStarMission.StarMission.Info) null;
      ReqTrophyStarMission.StarMission.Info info2 = (ReqTrophyStarMission.StarMission.Info) null;
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
              info1 = formatterResolver.GetFormatterWithVerify<ReqTrophyStarMission.StarMission.Info>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              info2 = formatterResolver.GetFormatterWithVerify<ReqTrophyStarMission.StarMission.Info>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new ReqTrophyStarMission.StarMission()
      {
        daily = info1,
        weekly = info2
      };
    }
  }
}
