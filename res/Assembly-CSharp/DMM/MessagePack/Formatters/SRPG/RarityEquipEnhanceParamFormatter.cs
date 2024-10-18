// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.RarityEquipEnhanceParamFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class RarityEquipEnhanceParamFormatter : 
    IMessagePackFormatter<RarityEquipEnhanceParam>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public RarityEquipEnhanceParamFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "rankcap",
          0
        },
        {
          "cost_scale",
          1
        },
        {
          "ranks",
          2
        }
      };
      this.____stringByteKeys = new byte[3][]
      {
        MessagePackBinary.GetEncodedStringBytes("rankcap"),
        MessagePackBinary.GetEncodedStringBytes("cost_scale"),
        MessagePackBinary.GetEncodedStringBytes("ranks")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      RarityEquipEnhanceParam value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 3);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.rankcap, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.cost_scale, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<RarityEquipEnhanceParam.RankParam[]>().Serialize(ref bytes, offset, value.ranks, formatterResolver);
      return offset - num;
    }

    public RarityEquipEnhanceParam Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (RarityEquipEnhanceParam) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      OInt oint1 = new OInt();
      OInt oint2 = new OInt();
      RarityEquipEnhanceParam.RankParam[] rankParamArray = (RarityEquipEnhanceParam.RankParam[]) null;
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
              oint1 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              oint2 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              rankParamArray = formatterResolver.GetFormatterWithVerify<RarityEquipEnhanceParam.RankParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new RarityEquipEnhanceParam()
      {
        rankcap = oint1,
        cost_scale = oint2,
        ranks = rankParamArray
      };
    }
  }
}
