// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.ReqMultiSupportList_ResponseFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class ReqMultiSupportList_ResponseFormatter : 
    IMessagePackFormatter<ReqMultiSupportList.Response>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public ReqMultiSupportList_ResponseFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "supports",
          0
        }
      };
      this.____stringByteKeys = new byte[1][]
      {
        MessagePackBinary.GetEncodedStringBytes("supports")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      ReqMultiSupportList.Response value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 1);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_MultiSupport[]>().Serialize(ref bytes, offset, value.supports, formatterResolver);
      return offset - num;
    }

    public ReqMultiSupportList.Response Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (ReqMultiSupportList.Response) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      JSON_MultiSupport[] jsonMultiSupportArray = (JSON_MultiSupport[]) null;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num3;
        if (!this.____keyMapping.TryGetValueSafe(key, out num3))
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        else if (num3 == 0)
          jsonMultiSupportArray = formatterResolver.GetFormatterWithVerify<JSON_MultiSupport[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
        else
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        offset += readSize;
      }
      readSize = offset - num1;
      return new ReqMultiSupportList.Response()
      {
        supports = jsonMultiSupportArray
      };
    }
  }
}
