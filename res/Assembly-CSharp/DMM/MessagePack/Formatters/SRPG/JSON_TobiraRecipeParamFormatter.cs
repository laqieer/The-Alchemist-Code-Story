// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.JSON_TobiraRecipeParamFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class JSON_TobiraRecipeParamFormatter : 
    IMessagePackFormatter<JSON_TobiraRecipeParam>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public JSON_TobiraRecipeParamFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "recipe_iname",
          0
        },
        {
          "tobira_lv",
          1
        },
        {
          "cost",
          2
        },
        {
          "unit_piece_num",
          3
        },
        {
          "piece_elem_num",
          4
        },
        {
          "unlock_elem_num",
          5
        },
        {
          "unlock_birth_num",
          6
        },
        {
          "mats",
          7
        }
      };
      this.____stringByteKeys = new byte[8][]
      {
        MessagePackBinary.GetEncodedStringBytes("recipe_iname"),
        MessagePackBinary.GetEncodedStringBytes("tobira_lv"),
        MessagePackBinary.GetEncodedStringBytes("cost"),
        MessagePackBinary.GetEncodedStringBytes("unit_piece_num"),
        MessagePackBinary.GetEncodedStringBytes("piece_elem_num"),
        MessagePackBinary.GetEncodedStringBytes("unlock_elem_num"),
        MessagePackBinary.GetEncodedStringBytes("unlock_birth_num"),
        MessagePackBinary.GetEncodedStringBytes("mats")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      JSON_TobiraRecipeParam value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 8);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.recipe_iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.tobira_lv);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.cost);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.unit_piece_num);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.piece_elem_num);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.unlock_elem_num);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.unlock_birth_num);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_TobiraRecipeMaterialParam[]>().Serialize(ref bytes, offset, value.mats, formatterResolver);
      return offset - num;
    }

    public JSON_TobiraRecipeParam Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (JSON_TobiraRecipeParam) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      string str = (string) null;
      int num3 = 0;
      int num4 = 0;
      int num5 = 0;
      int num6 = 0;
      int num7 = 0;
      int num8 = 0;
      JSON_TobiraRecipeMaterialParam[] recipeMaterialParamArray = (JSON_TobiraRecipeMaterialParam[]) null;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num9;
        if (!this.____keyMapping.TryGetValueSafe(key, out num9))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num9)
          {
            case 0:
              str = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 2:
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 3:
              num5 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 4:
              num6 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 5:
              num7 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 6:
              num8 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 7:
              recipeMaterialParamArray = formatterResolver.GetFormatterWithVerify<JSON_TobiraRecipeMaterialParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new JSON_TobiraRecipeParam()
      {
        recipe_iname = str,
        tobira_lv = num3,
        cost = num4,
        unit_piece_num = num5,
        piece_elem_num = num6,
        unlock_elem_num = num7,
        unlock_birth_num = num8,
        mats = recipeMaterialParamArray
      };
    }
  }
}
