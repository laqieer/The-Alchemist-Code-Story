// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.Unit_UnitProtectFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class Unit_UnitProtectFormatter : 
    IMessagePackFormatter<Unit.UnitProtect>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public Unit_UnitProtectFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "target",
          0
        },
        {
          "value",
          1
        },
        {
          "damageType",
          2
        },
        {
          "type",
          3
        },
        {
          "range",
          4
        },
        {
          "height",
          5
        },
        {
          "eternal",
          6
        },
        {
          "passive",
          7
        },
        {
          "skillIname",
          8
        }
      };
      this.____stringByteKeys = new byte[9][]
      {
        MessagePackBinary.GetEncodedStringBytes("target"),
        MessagePackBinary.GetEncodedStringBytes("value"),
        MessagePackBinary.GetEncodedStringBytes("damageType"),
        MessagePackBinary.GetEncodedStringBytes("type"),
        MessagePackBinary.GetEncodedStringBytes("range"),
        MessagePackBinary.GetEncodedStringBytes("height"),
        MessagePackBinary.GetEncodedStringBytes("eternal"),
        MessagePackBinary.GetEncodedStringBytes("passive"),
        MessagePackBinary.GetEncodedStringBytes("skillIname")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      Unit.UnitProtect value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 9);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<Unit>().Serialize(ref bytes, offset, value.target, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.value, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<DamageTypes>().Serialize(ref bytes, offset, value.damageType, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<ProtectTypes>().Serialize(ref bytes, offset, value.type, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.range, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.height, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.eternal);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.passive);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.skillIname, formatterResolver);
      return offset - num;
    }

    public Unit.UnitProtect Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (Unit.UnitProtect) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      Unit unit = (Unit) null;
      OInt oint1 = new OInt();
      DamageTypes damageTypes = DamageTypes.None;
      ProtectTypes protectTypes = ProtectTypes.None;
      OInt oint2 = new OInt();
      OInt oint3 = new OInt();
      bool flag1 = false;
      bool flag2 = false;
      string str = (string) null;
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
              unit = formatterResolver.GetFormatterWithVerify<Unit>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              oint1 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              damageTypes = formatterResolver.GetFormatterWithVerify<DamageTypes>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              protectTypes = formatterResolver.GetFormatterWithVerify<ProtectTypes>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 4:
              oint2 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 5:
              oint3 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 6:
              flag1 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 7:
              flag2 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 8:
              str = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new Unit.UnitProtect()
      {
        target = unit,
        value = oint1,
        damageType = damageTypes,
        type = protectTypes,
        range = oint2,
        height = oint3,
        eternal = flag1,
        passive = flag2,
        skillIname = str
      };
    }
  }
}
