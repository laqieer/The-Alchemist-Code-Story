// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.RuneSetEffStateFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class RuneSetEffStateFormatter : 
    IMessagePackFormatter<RuneSetEffState>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public RuneSetEffStateFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "type",
          0
        },
        {
          "calc",
          1
        },
        {
          "vone",
          2
        }
      };
      this.____stringByteKeys = new byte[3][]
      {
        MessagePackBinary.GetEncodedStringBytes("type"),
        MessagePackBinary.GetEncodedStringBytes("calc"),
        MessagePackBinary.GetEncodedStringBytes("vone")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      RuneSetEffState value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 3);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<ParamTypes>().Serialize(ref bytes, offset, value.type, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<SkillParamCalcTypes>().Serialize(ref bytes, offset, value.calc, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.vone);
      return offset - num;
    }

    public RuneSetEffState Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (RuneSetEffState) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      ParamTypes paramTypes = ParamTypes.None;
      SkillParamCalcTypes skillParamCalcTypes = SkillParamCalcTypes.Add;
      short num3 = 0;
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
              skillParamCalcTypes = formatterResolver.GetFormatterWithVerify<SkillParamCalcTypes>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              num3 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new RuneSetEffState()
      {
        type = paramTypes,
        calc = skillParamCalcTypes,
        vone = num3
      };
    }
  }
}
