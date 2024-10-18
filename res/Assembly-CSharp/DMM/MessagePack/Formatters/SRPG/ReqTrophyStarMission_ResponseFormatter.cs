// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.ReqTrophyStarMission_ResponseFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class ReqTrophyStarMission_ResponseFormatter : 
    IMessagePackFormatter<ReqTrophyStarMission.Response>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public ReqTrophyStarMission_ResponseFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "star_mission",
          0
        }
      };
      this.____stringByteKeys = new byte[1][]
      {
        MessagePackBinary.GetEncodedStringBytes("star_mission")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      ReqTrophyStarMission.Response value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 1);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<ReqTrophyStarMission.StarMission>().Serialize(ref bytes, offset, value.star_mission, formatterResolver);
      return offset - num;
    }

    public ReqTrophyStarMission.Response Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (ReqTrophyStarMission.Response) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      ReqTrophyStarMission.StarMission starMission = (ReqTrophyStarMission.StarMission) null;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num3;
        if (!this.____keyMapping.TryGetValueSafe(key, out num3))
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        else if (num3 == 0)
          starMission = formatterResolver.GetFormatterWithVerify<ReqTrophyStarMission.StarMission>().Deserialize(bytes, offset, formatterResolver, out readSize);
        else
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        offset += readSize;
      }
      readSize = offset - num1;
      return new ReqTrophyStarMission.Response()
      {
        star_mission = starMission
      };
    }
  }
}
