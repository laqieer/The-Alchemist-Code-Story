// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.ReqGvGBattleCapture_ResponseFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class ReqGvGBattleCapture_ResponseFormatter : 
    IMessagePackFormatter<ReqGvGBattleCapture.Response>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public ReqGvGBattleCapture_ResponseFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "nodes",
          0
        },
        {
          "used_units",
          1
        },
        {
          "refresh_wait_sec",
          2
        },
        {
          "auto_refresh_wait_sec",
          3
        }
      };
      this.____stringByteKeys = new byte[4][]
      {
        MessagePackBinary.GetEncodedStringBytes("nodes"),
        MessagePackBinary.GetEncodedStringBytes("used_units"),
        MessagePackBinary.GetEncodedStringBytes("refresh_wait_sec"),
        MessagePackBinary.GetEncodedStringBytes("auto_refresh_wait_sec")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      ReqGvGBattleCapture.Response value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 4);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GvGNodeData[]>().Serialize(ref bytes, offset, value.nodes, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GvGUsedUnitData[]>().Serialize(ref bytes, offset, value.used_units, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.refresh_wait_sec);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.auto_refresh_wait_sec);
      return offset - num;
    }

    public ReqGvGBattleCapture.Response Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (ReqGvGBattleCapture.Response) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      JSON_GvGNodeData[] jsonGvGnodeDataArray = (JSON_GvGNodeData[]) null;
      JSON_GvGUsedUnitData[] jsonGvGusedUnitDataArray = (JSON_GvGUsedUnitData[]) null;
      int num3 = 0;
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
              jsonGvGnodeDataArray = formatterResolver.GetFormatterWithVerify<JSON_GvGNodeData[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              jsonGvGusedUnitDataArray = formatterResolver.GetFormatterWithVerify<JSON_GvGUsedUnitData[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
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
      return new ReqGvGBattleCapture.Response()
      {
        nodes = jsonGvGnodeDataArray,
        used_units = jsonGvGusedUnitDataArray,
        refresh_wait_sec = num3,
        auto_refresh_wait_sec = num4
      };
    }
  }
}
