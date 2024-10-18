// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.Unit_UnitShieldFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class Unit_UnitShieldFormatter : 
    IMessagePackFormatter<Unit.UnitShield>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public Unit_UnitShieldFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "shieldType",
          0
        },
        {
          "damageType",
          1
        },
        {
          "hp",
          2
        },
        {
          "hpMax",
          3
        },
        {
          "turn",
          4
        },
        {
          "turnMax",
          5
        },
        {
          "damage_rate",
          6
        },
        {
          "damage_value",
          7
        },
        {
          "skill_param",
          8
        },
        {
          "is_efficacy",
          9
        },
        {
          "is_ignore_efficacy",
          10
        }
      };
      this.____stringByteKeys = new byte[11][]
      {
        MessagePackBinary.GetEncodedStringBytes("shieldType"),
        MessagePackBinary.GetEncodedStringBytes("damageType"),
        MessagePackBinary.GetEncodedStringBytes("hp"),
        MessagePackBinary.GetEncodedStringBytes("hpMax"),
        MessagePackBinary.GetEncodedStringBytes("turn"),
        MessagePackBinary.GetEncodedStringBytes("turnMax"),
        MessagePackBinary.GetEncodedStringBytes("damage_rate"),
        MessagePackBinary.GetEncodedStringBytes("damage_value"),
        MessagePackBinary.GetEncodedStringBytes("skill_param"),
        MessagePackBinary.GetEncodedStringBytes("is_efficacy"),
        MessagePackBinary.GetEncodedStringBytes("is_ignore_efficacy")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      Unit.UnitShield value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 11);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<ShieldTypes>().Serialize(ref bytes, offset, value.shieldType, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<DamageTypes>().Serialize(ref bytes, offset, value.damageType, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.hp, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.hpMax, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.turn, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.turnMax, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.damage_rate, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.damage_value, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += formatterResolver.GetFormatterWithVerify<SkillParam>().Serialize(ref bytes, offset, value.skill_param, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.is_efficacy);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.is_ignore_efficacy);
      return offset - num;
    }

    public Unit.UnitShield Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (Unit.UnitShield) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      ShieldTypes shieldTypes = ShieldTypes.None;
      DamageTypes damageTypes = DamageTypes.None;
      OInt oint1 = new OInt();
      OInt oint2 = new OInt();
      OInt oint3 = new OInt();
      OInt oint4 = new OInt();
      OInt oint5 = new OInt();
      OInt oint6 = new OInt();
      SkillParam skillParam = (SkillParam) null;
      bool flag1 = false;
      bool flag2 = false;
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
              shieldTypes = formatterResolver.GetFormatterWithVerify<ShieldTypes>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              damageTypes = formatterResolver.GetFormatterWithVerify<DamageTypes>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              oint1 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              oint2 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 4:
              oint3 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 5:
              oint4 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 6:
              oint5 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 7:
              oint6 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 8:
              skillParam = formatterResolver.GetFormatterWithVerify<SkillParam>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 9:
              flag1 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 10:
              flag2 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new Unit.UnitShield()
      {
        shieldType = shieldTypes,
        damageType = damageTypes,
        hp = oint1,
        hpMax = oint2,
        turn = oint3,
        turnMax = oint4,
        damage_rate = oint5,
        damage_value = oint6,
        skill_param = skillParam,
        is_efficacy = flag1,
        is_ignore_efficacy = flag2
      };
    }
  }
}
