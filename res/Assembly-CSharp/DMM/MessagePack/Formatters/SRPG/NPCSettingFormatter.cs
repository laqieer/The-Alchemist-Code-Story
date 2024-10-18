// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.NPCSettingFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;
using System.Collections.Generic;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class NPCSettingFormatter : IMessagePackFormatter<NPCSetting>, IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public NPCSettingFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "iname",
          0
        },
        {
          "lv",
          1
        },
        {
          "rare",
          2
        },
        {
          "awake",
          3
        },
        {
          "elem",
          4
        },
        {
          "exp",
          5
        },
        {
          "gems",
          6
        },
        {
          "gold",
          7
        },
        {
          "search",
          8
        },
        {
          "control",
          9
        },
        {
          "abilities",
          10
        },
        {
          "acttbl",
          11
        },
        {
          "patrol",
          12
        },
        {
          "fskl",
          13
        },
        {
          "notice_damage",
          14
        },
        {
          "notice_members",
          15
        },
        {
          "break_obj",
          16
        },
        {
          "is_raid_boss",
          17
        },
        {
          "need_dead",
          18
        },
        {
          "withdraw_drop",
          19
        },
        {
          "drop",
          20
        },
        {
          "uniqname",
          21
        },
        {
          "ai",
          22
        },
        {
          "pos",
          23
        },
        {
          "dir",
          24
        },
        {
          "side",
          25
        },
        {
          "waitEntryClock",
          26
        },
        {
          "waitMoveTurn",
          27
        },
        {
          "waitExitTurn",
          28
        },
        {
          "startCtCalc",
          29
        },
        {
          "startCtVal",
          30
        },
        {
          "DisableFirceVoice",
          31
        },
        {
          "ai_type",
          32
        },
        {
          "ai_pos",
          33
        },
        {
          "ai_len",
          34
        },
        {
          "trigger",
          35
        },
        {
          "entries",
          36
        },
        {
          "entries_and",
          37
        },
        {
          "parent",
          38
        }
      };
      this.____stringByteKeys = new byte[39][]
      {
        MessagePackBinary.GetEncodedStringBytes("iname"),
        MessagePackBinary.GetEncodedStringBytes("lv"),
        MessagePackBinary.GetEncodedStringBytes("rare"),
        MessagePackBinary.GetEncodedStringBytes("awake"),
        MessagePackBinary.GetEncodedStringBytes("elem"),
        MessagePackBinary.GetEncodedStringBytes("exp"),
        MessagePackBinary.GetEncodedStringBytes("gems"),
        MessagePackBinary.GetEncodedStringBytes("gold"),
        MessagePackBinary.GetEncodedStringBytes("search"),
        MessagePackBinary.GetEncodedStringBytes("control"),
        MessagePackBinary.GetEncodedStringBytes("abilities"),
        MessagePackBinary.GetEncodedStringBytes("acttbl"),
        MessagePackBinary.GetEncodedStringBytes("patrol"),
        MessagePackBinary.GetEncodedStringBytes("fskl"),
        MessagePackBinary.GetEncodedStringBytes("notice_damage"),
        MessagePackBinary.GetEncodedStringBytes("notice_members"),
        MessagePackBinary.GetEncodedStringBytes("break_obj"),
        MessagePackBinary.GetEncodedStringBytes("is_raid_boss"),
        MessagePackBinary.GetEncodedStringBytes("need_dead"),
        MessagePackBinary.GetEncodedStringBytes("withdraw_drop"),
        MessagePackBinary.GetEncodedStringBytes("drop"),
        MessagePackBinary.GetEncodedStringBytes("uniqname"),
        MessagePackBinary.GetEncodedStringBytes("ai"),
        MessagePackBinary.GetEncodedStringBytes("pos"),
        MessagePackBinary.GetEncodedStringBytes("dir"),
        MessagePackBinary.GetEncodedStringBytes("side"),
        MessagePackBinary.GetEncodedStringBytes("waitEntryClock"),
        MessagePackBinary.GetEncodedStringBytes("waitMoveTurn"),
        MessagePackBinary.GetEncodedStringBytes("waitExitTurn"),
        MessagePackBinary.GetEncodedStringBytes("startCtCalc"),
        MessagePackBinary.GetEncodedStringBytes("startCtVal"),
        MessagePackBinary.GetEncodedStringBytes("DisableFirceVoice"),
        MessagePackBinary.GetEncodedStringBytes("ai_type"),
        MessagePackBinary.GetEncodedStringBytes("ai_pos"),
        MessagePackBinary.GetEncodedStringBytes("ai_len"),
        MessagePackBinary.GetEncodedStringBytes("trigger"),
        MessagePackBinary.GetEncodedStringBytes("entries"),
        MessagePackBinary.GetEncodedStringBytes("entries_and"),
        MessagePackBinary.GetEncodedStringBytes("parent")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      NPCSetting value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteMapHeader(ref bytes, offset, 39);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<OString>().Serialize(ref bytes, offset, value.iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.lv, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.rare, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.awake, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.elem, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.exp, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.gems, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.gold, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.search, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += formatterResolver.GetFormatterWithVerify<OBool>().Serialize(ref bytes, offset, value.control, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += formatterResolver.GetFormatterWithVerify<EquipAbilitySetting[]>().Serialize(ref bytes, offset, value.abilities, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += formatterResolver.GetFormatterWithVerify<AIActionTable>().Serialize(ref bytes, offset, value.acttbl, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
      offset += formatterResolver.GetFormatterWithVerify<AIPatrolTable>().Serialize(ref bytes, offset, value.patrol, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
      offset += formatterResolver.GetFormatterWithVerify<OString>().Serialize(ref bytes, offset, value.fskl, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[14]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.notice_damage, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[15]);
      offset += formatterResolver.GetFormatterWithVerify<List<OString>>().Serialize(ref bytes, offset, value.notice_members, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[16]);
      offset += formatterResolver.GetFormatterWithVerify<MapBreakObj>().Serialize(ref bytes, offset, value.break_obj, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[17]);
      offset += formatterResolver.GetFormatterWithVerify<OBool>().Serialize(ref bytes, offset, value.is_raid_boss, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[18]);
      offset += formatterResolver.GetFormatterWithVerify<OBool>().Serialize(ref bytes, offset, value.need_dead, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[19]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.withdraw_drop);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[20]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.drop, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[21]);
      offset += formatterResolver.GetFormatterWithVerify<OString>().Serialize(ref bytes, offset, value.uniqname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[22]);
      offset += formatterResolver.GetFormatterWithVerify<OString>().Serialize(ref bytes, offset, value.ai, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[23]);
      offset += formatterResolver.GetFormatterWithVerify<OIntVector2>().Serialize(ref bytes, offset, value.pos, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[24]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.dir, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[25]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.side, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[26]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.waitEntryClock, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[27]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.waitMoveTurn, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[28]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.waitExitTurn, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[29]);
      offset += formatterResolver.GetFormatterWithVerify<eMapUnitCtCalcType>().Serialize(ref bytes, offset, value.startCtCalc, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[30]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.startCtVal, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[31]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.DisableFirceVoice);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[32]);
      offset += formatterResolver.GetFormatterWithVerify<AIActionType>().Serialize(ref bytes, offset, value.ai_type, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[33]);
      offset += formatterResolver.GetFormatterWithVerify<OIntVector2>().Serialize(ref bytes, offset, value.ai_pos, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[34]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.ai_len, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[35]);
      offset += formatterResolver.GetFormatterWithVerify<EventTrigger>().Serialize(ref bytes, offset, value.trigger, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[36]);
      offset += formatterResolver.GetFormatterWithVerify<List<UnitEntryTrigger>>().Serialize(ref bytes, offset, value.entries, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[37]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.entries_and, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[38]);
      offset += formatterResolver.GetFormatterWithVerify<OString>().Serialize(ref bytes, offset, value.parent, formatterResolver);
      return offset - num;
    }

    public NPCSetting Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (NPCSetting) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      OString ostring1 = new OString();
      OInt oint1 = new OInt();
      OInt oint2 = new OInt();
      OInt oint3 = new OInt();
      OInt oint4 = new OInt();
      OInt oint5 = new OInt();
      OInt oint6 = new OInt();
      OInt oint7 = new OInt();
      OInt oint8 = new OInt();
      OBool obool1 = new OBool();
      EquipAbilitySetting[] equipAbilitySettingArray = (EquipAbilitySetting[]) null;
      AIActionTable aiActionTable = (AIActionTable) null;
      AIPatrolTable aiPatrolTable = (AIPatrolTable) null;
      OString ostring2 = new OString();
      OInt oint9 = new OInt();
      List<OString> ostringList = (List<OString>) null;
      MapBreakObj mapBreakObj = (MapBreakObj) null;
      OBool obool2 = new OBool();
      OBool obool3 = new OBool();
      bool flag1 = false;
      string str = (string) null;
      OString ostring3 = new OString();
      OString ostring4 = new OString();
      OIntVector2 ointVector2_1 = new OIntVector2();
      OInt oint10 = new OInt();
      OInt oint11 = new OInt();
      OInt oint12 = new OInt();
      OInt oint13 = new OInt();
      OInt oint14 = new OInt();
      eMapUnitCtCalcType mapUnitCtCalcType = eMapUnitCtCalcType.FIXED;
      OInt oint15 = new OInt();
      bool flag2 = false;
      AIActionType aiActionType = AIActionType.None;
      OIntVector2 ointVector2_2 = new OIntVector2();
      OInt oint16 = new OInt();
      EventTrigger eventTrigger = (EventTrigger) null;
      List<UnitEntryTrigger> unitEntryTriggerList = (List<UnitEntryTrigger>) null;
      OInt oint17 = new OInt();
      OString ostring5 = new OString();
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
              ostring1 = formatterResolver.GetFormatterWithVerify<OString>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              oint1 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              oint2 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              oint3 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 4:
              oint4 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 5:
              oint5 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 6:
              oint6 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 7:
              oint7 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 8:
              oint8 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 9:
              obool1 = formatterResolver.GetFormatterWithVerify<OBool>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 10:
              equipAbilitySettingArray = formatterResolver.GetFormatterWithVerify<EquipAbilitySetting[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 11:
              aiActionTable = formatterResolver.GetFormatterWithVerify<AIActionTable>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 12:
              aiPatrolTable = formatterResolver.GetFormatterWithVerify<AIPatrolTable>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 13:
              ostring2 = formatterResolver.GetFormatterWithVerify<OString>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 14:
              oint9 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 15:
              ostringList = formatterResolver.GetFormatterWithVerify<List<OString>>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 16:
              mapBreakObj = formatterResolver.GetFormatterWithVerify<MapBreakObj>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 17:
              obool2 = formatterResolver.GetFormatterWithVerify<OBool>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 18:
              obool3 = formatterResolver.GetFormatterWithVerify<OBool>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 19:
              flag1 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 20:
              str = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 21:
              ostring3 = formatterResolver.GetFormatterWithVerify<OString>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 22:
              ostring4 = formatterResolver.GetFormatterWithVerify<OString>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 23:
              ointVector2_1 = formatterResolver.GetFormatterWithVerify<OIntVector2>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 24:
              oint10 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 25:
              oint11 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 26:
              oint12 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 27:
              oint13 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 28:
              oint14 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 29:
              mapUnitCtCalcType = formatterResolver.GetFormatterWithVerify<eMapUnitCtCalcType>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 30:
              oint15 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 31:
              flag2 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 32:
              aiActionType = formatterResolver.GetFormatterWithVerify<AIActionType>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 33:
              ointVector2_2 = formatterResolver.GetFormatterWithVerify<OIntVector2>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 34:
              oint16 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 35:
              eventTrigger = formatterResolver.GetFormatterWithVerify<EventTrigger>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 36:
              unitEntryTriggerList = formatterResolver.GetFormatterWithVerify<List<UnitEntryTrigger>>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 37:
              oint17 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 38:
              ostring5 = formatterResolver.GetFormatterWithVerify<OString>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      NPCSetting npcSetting = new NPCSetting();
      npcSetting.iname = ostring1;
      npcSetting.lv = oint1;
      npcSetting.rare = oint2;
      npcSetting.awake = oint3;
      npcSetting.elem = oint4;
      npcSetting.exp = oint5;
      npcSetting.gems = oint6;
      npcSetting.gold = oint7;
      npcSetting.search = oint8;
      npcSetting.control = obool1;
      npcSetting.abilities = equipAbilitySettingArray;
      npcSetting.acttbl = aiActionTable;
      npcSetting.patrol = aiPatrolTable;
      npcSetting.fskl = ostring2;
      npcSetting.notice_damage = oint9;
      npcSetting.notice_members = ostringList;
      npcSetting.break_obj = mapBreakObj;
      npcSetting.is_raid_boss = obool2;
      npcSetting.need_dead = obool3;
      npcSetting.withdraw_drop = flag1;
      npcSetting.drop = str;
      npcSetting.uniqname = ostring3;
      npcSetting.ai = ostring4;
      npcSetting.pos = ointVector2_1;
      npcSetting.dir = oint10;
      npcSetting.side = oint11;
      npcSetting.waitEntryClock = oint12;
      npcSetting.waitMoveTurn = oint13;
      npcSetting.waitExitTurn = oint14;
      npcSetting.startCtCalc = mapUnitCtCalcType;
      npcSetting.startCtVal = oint15;
      npcSetting.DisableFirceVoice = flag2;
      npcSetting.ai_type = aiActionType;
      npcSetting.ai_pos = ointVector2_2;
      npcSetting.ai_len = oint16;
      npcSetting.trigger = eventTrigger;
      npcSetting.entries = unitEntryTriggerList;
      npcSetting.entries_and = oint17;
      npcSetting.parent = ostring5;
      return npcSetting;
    }
  }
}
