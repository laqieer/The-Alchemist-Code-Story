// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.DynamicTransformUnitParamFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class DynamicTransformUnitParamFormatter : 
    IMessagePackFormatter<DynamicTransformUnitParam>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public DynamicTransformUnitParamFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "Iname",
          0
        },
        {
          "TrUnitId",
          1
        },
        {
          "Turn",
          2
        },
        {
          "UpperToAbId",
          3
        },
        {
          "LowerToAbId",
          4
        },
        {
          "ReactToAbId",
          5
        },
        {
          "CancelEffect",
          6
        },
        {
          "CancelDisMs",
          7
        },
        {
          "CancelAppMs",
          8
        },
        {
          "IsNoWeaponAbility",
          9
        },
        {
          "IsNoVisionAbility",
          10
        },
        {
          "IsNoItems",
          11
        },
        {
          "IsTransHpFull",
          12
        },
        {
          "IsCancelHpFull",
          13
        },
        {
          "IsInheritSkin",
          14
        }
      };
      this.____stringByteKeys = new byte[15][]
      {
        MessagePackBinary.GetEncodedStringBytes("Iname"),
        MessagePackBinary.GetEncodedStringBytes("TrUnitId"),
        MessagePackBinary.GetEncodedStringBytes("Turn"),
        MessagePackBinary.GetEncodedStringBytes("UpperToAbId"),
        MessagePackBinary.GetEncodedStringBytes("LowerToAbId"),
        MessagePackBinary.GetEncodedStringBytes("ReactToAbId"),
        MessagePackBinary.GetEncodedStringBytes("CancelEffect"),
        MessagePackBinary.GetEncodedStringBytes("CancelDisMs"),
        MessagePackBinary.GetEncodedStringBytes("CancelAppMs"),
        MessagePackBinary.GetEncodedStringBytes("IsNoWeaponAbility"),
        MessagePackBinary.GetEncodedStringBytes("IsNoVisionAbility"),
        MessagePackBinary.GetEncodedStringBytes("IsNoItems"),
        MessagePackBinary.GetEncodedStringBytes("IsTransHpFull"),
        MessagePackBinary.GetEncodedStringBytes("IsCancelHpFull"),
        MessagePackBinary.GetEncodedStringBytes("IsInheritSkin")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      DynamicTransformUnitParam value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 15);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.Iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.TrUnitId, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.Turn);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.UpperToAbId, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.LowerToAbId, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.ReactToAbId, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.CancelEffect, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.CancelDisMs);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.CancelAppMs);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsNoWeaponAbility);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsNoVisionAbility);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsNoItems);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsTransHpFull);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsCancelHpFull);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[14]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsInheritSkin);
      return offset - num;
    }

    public DynamicTransformUnitParam Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (DynamicTransformUnitParam) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      string str1 = (string) null;
      string str2 = (string) null;
      int num3 = 0;
      string str3 = (string) null;
      string str4 = (string) null;
      string str5 = (string) null;
      string str6 = (string) null;
      int num4 = 0;
      int num5 = 0;
      bool flag1 = false;
      bool flag2 = false;
      bool flag3 = false;
      bool flag4 = false;
      bool flag5 = false;
      bool flag6 = false;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num6;
        if (!this.____keyMapping.TryGetValueSafe(key, out num6))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num6)
          {
            case 0:
              str1 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              str2 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 3:
              str3 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 4:
              str4 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 5:
              str5 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 6:
              str6 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 7:
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 8:
              num5 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 9:
              flag1 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 10:
              flag2 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 11:
              flag3 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 12:
              flag4 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 13:
              flag5 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 14:
              flag6 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new DynamicTransformUnitParam();
    }
  }
}
