// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.BaseStatusFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class BaseStatusFormatter : IMessagePackFormatter<BaseStatus>, IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public BaseStatusFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "param",
          0
        },
        {
          "element_assist",
          1
        },
        {
          "element_resist",
          2
        },
        {
          "enchant_assist",
          3
        },
        {
          "enchant_resist",
          4
        },
        {
          "bonus",
          5
        },
        {
          "tokkou",
          6
        },
        {
          "tokubou",
          7
        }
      };
      this.____stringByteKeys = new byte[8][]
      {
        MessagePackBinary.GetEncodedStringBytes("param"),
        MessagePackBinary.GetEncodedStringBytes("element_assist"),
        MessagePackBinary.GetEncodedStringBytes("element_resist"),
        MessagePackBinary.GetEncodedStringBytes("enchant_assist"),
        MessagePackBinary.GetEncodedStringBytes("enchant_resist"),
        MessagePackBinary.GetEncodedStringBytes("bonus"),
        MessagePackBinary.GetEncodedStringBytes("tokkou"),
        MessagePackBinary.GetEncodedStringBytes("tokubou")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      BaseStatus value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 8);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<StatusParam>().Serialize(ref bytes, offset, value.param, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<ElementParam>().Serialize(ref bytes, offset, value.element_assist, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<ElementParam>().Serialize(ref bytes, offset, value.element_resist, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<EnchantParam>().Serialize(ref bytes, offset, value.enchant_assist, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<EnchantParam>().Serialize(ref bytes, offset, value.enchant_resist, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += formatterResolver.GetFormatterWithVerify<BattleBonusParam>().Serialize(ref bytes, offset, value.bonus, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += formatterResolver.GetFormatterWithVerify<TokkouParam>().Serialize(ref bytes, offset, value.tokkou, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += formatterResolver.GetFormatterWithVerify<TokkouParam>().Serialize(ref bytes, offset, value.tokubou, formatterResolver);
      return offset - num;
    }

    public BaseStatus Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (BaseStatus) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      StatusParam statusParam = (StatusParam) null;
      ElementParam elementParam1 = (ElementParam) null;
      ElementParam elementParam2 = (ElementParam) null;
      EnchantParam enchantParam1 = (EnchantParam) null;
      EnchantParam enchantParam2 = (EnchantParam) null;
      BattleBonusParam battleBonusParam = (BattleBonusParam) null;
      TokkouParam tokkouParam1 = (TokkouParam) null;
      TokkouParam tokkouParam2 = (TokkouParam) null;
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
              statusParam = formatterResolver.GetFormatterWithVerify<StatusParam>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              elementParam1 = formatterResolver.GetFormatterWithVerify<ElementParam>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              elementParam2 = formatterResolver.GetFormatterWithVerify<ElementParam>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              enchantParam1 = formatterResolver.GetFormatterWithVerify<EnchantParam>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 4:
              enchantParam2 = formatterResolver.GetFormatterWithVerify<EnchantParam>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 5:
              battleBonusParam = formatterResolver.GetFormatterWithVerify<BattleBonusParam>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 6:
              tokkouParam1 = formatterResolver.GetFormatterWithVerify<TokkouParam>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 7:
              tokkouParam2 = formatterResolver.GetFormatterWithVerify<TokkouParam>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new BaseStatus()
      {
        param = statusParam,
        element_assist = elementParam1,
        element_resist = elementParam2,
        enchant_assist = enchantParam1,
        enchant_resist = enchantParam2,
        bonus = battleBonusParam,
        tokkou = tokkouParam1,
        tokubou = tokkouParam2
      };
    }
  }
}
