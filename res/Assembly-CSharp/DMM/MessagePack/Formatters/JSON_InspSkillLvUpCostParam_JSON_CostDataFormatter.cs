// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.JSON_InspSkillLvUpCostParam_JSON_CostDataFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using System;

#nullable disable
namespace MessagePack.Formatters
{
  public sealed class JSON_InspSkillLvUpCostParam_JSON_CostDataFormatter : 
    IMessagePackFormatter<JSON_InspSkillLvUpCostParam.JSON_CostData>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public JSON_InspSkillLvUpCostParam_JSON_CostDataFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "gold",
          0
        }
      };
      this.____stringByteKeys = new byte[1][]
      {
        MessagePackBinary.GetEncodedStringBytes("gold")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      JSON_InspSkillLvUpCostParam.JSON_CostData value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 1);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.gold);
      return offset - num;
    }

    public JSON_InspSkillLvUpCostParam.JSON_CostData Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (JSON_InspSkillLvUpCostParam.JSON_CostData) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      int num3 = 0;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num4;
        if (!this.____keyMapping.TryGetValueSafe(key, out num4))
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        else if (num4 == 0)
          num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
        else
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        offset += readSize;
      }
      readSize = offset - num1;
      return new JSON_InspSkillLvUpCostParam.JSON_CostData()
      {
        gold = num3
      };
    }
  }
}
