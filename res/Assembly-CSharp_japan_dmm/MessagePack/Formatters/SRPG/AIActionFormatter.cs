// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.AIActionFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class AIActionFormatter : IMessagePackFormatter<AIAction>, IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public AIActionFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "skill",
          0
        },
        {
          "type",
          1
        },
        {
          "turn",
          2
        },
        {
          "notBlock",
          3
        },
        {
          "noExecAct",
          4
        },
        {
          "nextActIdx",
          5
        },
        {
          "nextTurnAct",
          6
        },
        {
          "turnActIdx",
          7
        },
        {
          "cond",
          8
        }
      };
      this.____stringByteKeys = new byte[9][]
      {
        MessagePackBinary.GetEncodedStringBytes("skill"),
        MessagePackBinary.GetEncodedStringBytes("type"),
        MessagePackBinary.GetEncodedStringBytes("turn"),
        MessagePackBinary.GetEncodedStringBytes("notBlock"),
        MessagePackBinary.GetEncodedStringBytes("noExecAct"),
        MessagePackBinary.GetEncodedStringBytes("nextActIdx"),
        MessagePackBinary.GetEncodedStringBytes("nextTurnAct"),
        MessagePackBinary.GetEncodedStringBytes("turnActIdx"),
        MessagePackBinary.GetEncodedStringBytes("cond")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      AIAction value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 9);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<OString>().Serialize(ref bytes, offset, value.skill, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.type, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.turn, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<OBool>().Serialize(ref bytes, offset, value.notBlock, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<eAIActionNoExecAct>().Serialize(ref bytes, offset, value.noExecAct, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.nextActIdx);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += formatterResolver.GetFormatterWithVerify<eAIActionNextTurnAct>().Serialize(ref bytes, offset, value.nextTurnAct, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.turnActIdx);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += formatterResolver.GetFormatterWithVerify<SkillLockCondition>().Serialize(ref bytes, offset, value.cond, formatterResolver);
      return offset - num;
    }

    public AIAction Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (AIAction) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      OString ostring = new OString();
      OInt oint1 = new OInt();
      OInt oint2 = new OInt();
      OBool obool = new OBool();
      eAIActionNoExecAct aiActionNoExecAct = eAIActionNoExecAct.NONE;
      int num3 = 0;
      eAIActionNextTurnAct actionNextTurnAct = eAIActionNextTurnAct.NONE;
      int num4 = 0;
      SkillLockCondition skillLockCondition = (SkillLockCondition) null;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num5;
        if (!this.____keyMapping.TryGetValueSafe(key, out num5))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num5)
          {
            case 0:
              ostring = formatterResolver.GetFormatterWithVerify<OString>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              oint1 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              oint2 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              obool = formatterResolver.GetFormatterWithVerify<OBool>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 4:
              aiActionNoExecAct = formatterResolver.GetFormatterWithVerify<eAIActionNoExecAct>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 5:
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 6:
              actionNextTurnAct = formatterResolver.GetFormatterWithVerify<eAIActionNextTurnAct>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 7:
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 8:
              skillLockCondition = formatterResolver.GetFormatterWithVerify<SkillLockCondition>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new AIAction()
      {
        skill = ostring,
        type = oint1,
        turn = oint2,
        notBlock = obool,
        noExecAct = aiActionNoExecAct,
        nextActIdx = num3,
        nextTurnAct = actionNextTurnAct,
        turnActIdx = num4,
        cond = skillLockCondition
      };
    }
  }
}
