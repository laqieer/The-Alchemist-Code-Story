// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.AIParamFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class AIParamFormatter : IMessagePackFormatter<AIParam>, IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public AIParamFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "iname",
          0
        },
        {
          "role",
          1
        },
        {
          "param",
          2
        },
        {
          "param_prio",
          3
        },
        {
          "flags",
          4
        },
        {
          "escape_border",
          5
        },
        {
          "heal_border",
          6
        },
        {
          "gems_border",
          7
        },
        {
          "buff_border",
          8
        },
        {
          "cond_border",
          9
        },
        {
          "safe_border",
          10
        },
        {
          "gosa_border",
          11
        },
        {
          "DisableSupportActionHpBorder",
          12
        },
        {
          "DisableSupportActionMemberBorder",
          13
        },
        {
          "SkillCategoryPriorities",
          14
        },
        {
          "BuffPriorities",
          15
        },
        {
          "ConditionPriorities",
          16
        }
      };
      this.____stringByteKeys = new byte[17][]
      {
        MessagePackBinary.GetEncodedStringBytes("iname"),
        MessagePackBinary.GetEncodedStringBytes("role"),
        MessagePackBinary.GetEncodedStringBytes("param"),
        MessagePackBinary.GetEncodedStringBytes("param_prio"),
        MessagePackBinary.GetEncodedStringBytes("flags"),
        MessagePackBinary.GetEncodedStringBytes("escape_border"),
        MessagePackBinary.GetEncodedStringBytes("heal_border"),
        MessagePackBinary.GetEncodedStringBytes("gems_border"),
        MessagePackBinary.GetEncodedStringBytes("buff_border"),
        MessagePackBinary.GetEncodedStringBytes("cond_border"),
        MessagePackBinary.GetEncodedStringBytes("safe_border"),
        MessagePackBinary.GetEncodedStringBytes("gosa_border"),
        MessagePackBinary.GetEncodedStringBytes("DisableSupportActionHpBorder"),
        MessagePackBinary.GetEncodedStringBytes("DisableSupportActionMemberBorder"),
        MessagePackBinary.GetEncodedStringBytes("SkillCategoryPriorities"),
        MessagePackBinary.GetEncodedStringBytes("BuffPriorities"),
        MessagePackBinary.GetEncodedStringBytes("ConditionPriorities")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      AIParam value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteMapHeader(ref bytes, offset, 17);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<RoleTypes>().Serialize(ref bytes, offset, value.role, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<ParamTypes>().Serialize(ref bytes, offset, value.param, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<ParamPriorities>().Serialize(ref bytes, offset, value.param_prio, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<OLong>().Serialize(ref bytes, offset, value.flags, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.escape_border, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.heal_border, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.gems_border, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.buff_border, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.cond_border, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.safe_border, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.gosa_border, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.DisableSupportActionHpBorder, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.DisableSupportActionMemberBorder, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[14]);
      offset += formatterResolver.GetFormatterWithVerify<SkillCategory[]>().Serialize(ref bytes, offset, value.SkillCategoryPriorities, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[15]);
      offset += formatterResolver.GetFormatterWithVerify<ParamTypes[]>().Serialize(ref bytes, offset, value.BuffPriorities, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[16]);
      offset += formatterResolver.GetFormatterWithVerify<EUnitCondition[]>().Serialize(ref bytes, offset, value.ConditionPriorities, formatterResolver);
      return offset - num;
    }

    public AIParam Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (AIParam) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      string str = (string) null;
      RoleTypes roleTypes = RoleTypes.None;
      ParamTypes paramTypes = ParamTypes.None;
      ParamPriorities paramPriorities = ParamPriorities.None;
      OLong olong = new OLong();
      OInt oint1 = new OInt();
      OInt oint2 = new OInt();
      OInt oint3 = new OInt();
      OInt oint4 = new OInt();
      OInt oint5 = new OInt();
      OInt oint6 = new OInt();
      OInt oint7 = new OInt();
      OInt oint8 = new OInt();
      OInt oint9 = new OInt();
      SkillCategory[] skillCategoryArray = (SkillCategory[]) null;
      ParamTypes[] paramTypesArray = (ParamTypes[]) null;
      EUnitCondition[] eunitConditionArray = (EUnitCondition[]) null;
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
              str = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              roleTypes = formatterResolver.GetFormatterWithVerify<RoleTypes>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              paramTypes = formatterResolver.GetFormatterWithVerify<ParamTypes>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              paramPriorities = formatterResolver.GetFormatterWithVerify<ParamPriorities>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 4:
              olong = formatterResolver.GetFormatterWithVerify<OLong>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 5:
              oint1 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 6:
              oint2 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 7:
              oint3 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 8:
              oint4 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 9:
              oint5 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 10:
              oint6 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 11:
              oint7 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 12:
              oint8 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 13:
              oint9 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 14:
              skillCategoryArray = formatterResolver.GetFormatterWithVerify<SkillCategory[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 15:
              paramTypesArray = formatterResolver.GetFormatterWithVerify<ParamTypes[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 16:
              eunitConditionArray = formatterResolver.GetFormatterWithVerify<EUnitCondition[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new AIParam()
      {
        iname = str,
        role = roleTypes,
        param = paramTypes,
        param_prio = paramPriorities,
        flags = olong,
        escape_border = oint1,
        heal_border = oint2,
        gems_border = oint3,
        buff_border = oint4,
        cond_border = oint5,
        safe_border = oint6,
        gosa_border = oint7,
        DisableSupportActionHpBorder = oint8,
        DisableSupportActionMemberBorder = oint9,
        SkillCategoryPriorities = skillCategoryArray,
        BuffPriorities = paramTypesArray,
        ConditionPriorities = eunitConditionArray
      };
    }
  }
}
