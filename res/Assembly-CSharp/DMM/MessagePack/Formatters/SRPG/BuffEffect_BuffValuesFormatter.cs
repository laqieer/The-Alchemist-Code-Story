// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.BuffEffect_BuffValuesFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class BuffEffect_BuffValuesFormatter : 
    IMessagePackFormatter<BuffEffect.BuffValues>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public BuffEffect_BuffValuesFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "param_type",
          0
        },
        {
          "method_type",
          1
        },
        {
          "value",
          2
        }
      };
      this.____stringByteKeys = new byte[3][]
      {
        MessagePackBinary.GetEncodedStringBytes("param_type"),
        MessagePackBinary.GetEncodedStringBytes("method_type"),
        MessagePackBinary.GetEncodedStringBytes("value")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      BuffEffect.BuffValues value,
      IFormatterResolver formatterResolver)
    {
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 3);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<ParamTypes>().Serialize(ref bytes, offset, value.param_type, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<BuffMethodTypes>().Serialize(ref bytes, offset, value.method_type, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.value);
      return offset - num;
    }

    public BuffEffect.BuffValues Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      int num1 = !MessagePackBinary.IsNil(bytes, offset) ? offset : throw new InvalidOperationException("typecode is null, struct not supported");
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      ParamTypes paramTypes = ParamTypes.None;
      BuffMethodTypes buffMethodTypes = BuffMethodTypes.Add;
      int num3 = 0;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num4;
        if (!this.____keyMapping.TryGetValueSafe(key, out num4))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num4)
          {
            case 0:
              paramTypes = formatterResolver.GetFormatterWithVerify<ParamTypes>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              buffMethodTypes = formatterResolver.GetFormatterWithVerify<BuffMethodTypes>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new BuffEffect.BuffValues()
      {
        param_type = paramTypes,
        method_type = buffMethodTypes,
        value = num3
      };
    }
  }
}
