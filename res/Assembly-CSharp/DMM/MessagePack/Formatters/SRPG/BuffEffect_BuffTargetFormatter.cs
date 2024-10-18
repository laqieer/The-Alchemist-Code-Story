// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.BuffEffect_BuffTargetFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class BuffEffect_BuffTargetFormatter : 
    IMessagePackFormatter<BuffEffect.BuffTarget>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public BuffEffect_BuffTargetFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "buffType",
          0
        },
        {
          "calcType",
          1
        },
        {
          "paramType",
          2
        },
        {
          "value",
          3
        },
        {
          "value_one",
          4
        },
        {
          "tokkou",
          5
        }
      };
      this.____stringByteKeys = new byte[6][]
      {
        MessagePackBinary.GetEncodedStringBytes("buffType"),
        MessagePackBinary.GetEncodedStringBytes("calcType"),
        MessagePackBinary.GetEncodedStringBytes("paramType"),
        MessagePackBinary.GetEncodedStringBytes("value"),
        MessagePackBinary.GetEncodedStringBytes("value_one"),
        MessagePackBinary.GetEncodedStringBytes("tokkou")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      BuffEffect.BuffTarget value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 6);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<BuffTypes>().Serialize(ref bytes, offset, value.buffType, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<SkillParamCalcTypes>().Serialize(ref bytes, offset, value.calcType, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<ParamTypes>().Serialize(ref bytes, offset, value.paramType, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.value, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.value_one, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += formatterResolver.GetFormatterWithVerify<OString>().Serialize(ref bytes, offset, value.tokkou, formatterResolver);
      return offset - num;
    }

    public BuffEffect.BuffTarget Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (BuffEffect.BuffTarget) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      BuffTypes buffTypes = BuffTypes.Buff;
      SkillParamCalcTypes skillParamCalcTypes = SkillParamCalcTypes.Add;
      ParamTypes paramTypes = ParamTypes.None;
      OInt oint1 = new OInt();
      OInt oint2 = new OInt();
      OString ostring = new OString();
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
              buffTypes = formatterResolver.GetFormatterWithVerify<BuffTypes>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              skillParamCalcTypes = formatterResolver.GetFormatterWithVerify<SkillParamCalcTypes>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              paramTypes = formatterResolver.GetFormatterWithVerify<ParamTypes>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              oint1 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 4:
              oint2 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 5:
              ostring = formatterResolver.GetFormatterWithVerify<OString>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new BuffEffect.BuffTarget()
      {
        buffType = buffTypes,
        calcType = skillParamCalcTypes,
        paramType = paramTypes,
        value = oint1,
        value_one = oint2,
        tokkou = ostring
      };
    }
  }
}
