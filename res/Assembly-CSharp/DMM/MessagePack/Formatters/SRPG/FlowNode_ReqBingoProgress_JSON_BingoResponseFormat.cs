// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.FlowNode_ReqBingoProgress_JSON_BingoResponseFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class FlowNode_ReqBingoProgress_JSON_BingoResponseFormatter : 
    IMessagePackFormatter<FlowNode_ReqBingoProgress.JSON_BingoResponse>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public FlowNode_ReqBingoProgress_JSON_BingoResponseFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "bingoprogs",
          0
        }
      };
      this.____stringByteKeys = new byte[1][]
      {
        MessagePackBinary.GetEncodedStringBytes("bingoprogs")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      FlowNode_ReqBingoProgress.JSON_BingoResponse value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 1);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_TrophyProgress[]>().Serialize(ref bytes, offset, value.bingoprogs, formatterResolver);
      return offset - num;
    }

    public FlowNode_ReqBingoProgress.JSON_BingoResponse Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (FlowNode_ReqBingoProgress.JSON_BingoResponse) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      JSON_TrophyProgress[] jsonTrophyProgressArray = (JSON_TrophyProgress[]) null;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num3;
        if (!this.____keyMapping.TryGetValueSafe(key, out num3))
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        else if (num3 == 0)
          jsonTrophyProgressArray = formatterResolver.GetFormatterWithVerify<JSON_TrophyProgress[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
        else
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        offset += readSize;
      }
      readSize = offset - num1;
      return new FlowNode_ReqBingoProgress.JSON_BingoResponse()
      {
        bingoprogs = jsonTrophyProgressArray
      };
    }
  }
}
