// Decompiled with JetBrains decompiler
// Type: SRPG.SequenceErrorSendLogMessage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class SequenceErrorSendLogMessage
  {
    private SequenceErrorSendLogMessage.Message message = new SequenceErrorSendLogMessage.Message();

    public void Add(SceneBattle.MultiPlayCheck my, SceneBattle.MultiPlayCheck host)
    {
      this.message.Add(new SequenceErrorSendLogMessage.MessagePack(my, host));
    }

    public void AddLog(MultiSendLogBuffer logBuffer)
    {
      List<Unit> unitList = new List<Unit>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) SceneBattle.Instance, (UnityEngine.Object) null) && SceneBattle.Instance.Battle != null && SceneBattle.Instance.Battle.AllUnits != null)
        unitList = SceneBattle.Instance.Battle.AllUnits;
      for (SceneBattle.MultiPlayRecvData multiPlayRecvData = logBuffer.Get(); multiPlayRecvData != null; multiPlayRecvData = logBuffer.Get())
      {
        int num = Math.Max(Math.Max(Math.Max(Math.Max(Math.Max(Math.Max(Math.Max(Math.Max(0, multiPlayRecvData.c != null ? multiPlayRecvData.c.Length : 0), multiPlayRecvData.u != null ? multiPlayRecvData.u.Length : 0), multiPlayRecvData.s != null ? multiPlayRecvData.s.Length : 0), multiPlayRecvData.i != null ? multiPlayRecvData.i.Length : 0), multiPlayRecvData.gx != null ? multiPlayRecvData.gx.Length : 0), multiPlayRecvData.gy != null ? multiPlayRecvData.gy.Length : 0), multiPlayRecvData.ul != null ? multiPlayRecvData.ul.Length : 0), multiPlayRecvData.d != null ? multiPlayRecvData.d.Length : 0);
        for (int index = 0; index < num; ++index)
        {
          SceneBattle.EMultiPlayCommand emultiPlayCommand = multiPlayRecvData.c == null || index >= multiPlayRecvData.c.Length ? SceneBattle.EMultiPlayCommand.NOP : (SceneBattle.EMultiPlayCommand) multiPlayRecvData.c[index];
          switch (emultiPlayCommand)
          {
            case SceneBattle.EMultiPlayCommand.NOP:
            case SceneBattle.EMultiPlayCommand.MOVE_START:
            case SceneBattle.EMultiPlayCommand.MOVE:
            case SceneBattle.EMultiPlayCommand.GRID_XY:
            case SceneBattle.EMultiPlayCommand.GRID_EVENT:
            case SceneBattle.EMultiPlayCommand.UNIT_XYDIR:
              continue;
            default:
              SequenceErrorSendLogMessage.MessageLog messageLog = new SequenceErrorSendLogMessage.MessageLog()
              {
                c = emultiPlayCommand.ToString(),
                turn = multiPlayRecvData.b,
                ui = multiPlayRecvData.uid
              };
              messageLog.ti = multiPlayRecvData.u == null || index >= multiPlayRecvData.u.Length ? messageLog.ti : multiPlayRecvData.u[index];
              messageLog.s = multiPlayRecvData.s == null || index >= multiPlayRecvData.s.Length ? messageLog.s : multiPlayRecvData.s[index];
              messageLog.i = multiPlayRecvData.i == null || index >= multiPlayRecvData.i.Length ? messageLog.i : multiPlayRecvData.i[index];
              messageLog.gx = multiPlayRecvData.gx == null || index >= multiPlayRecvData.gx.Length ? messageLog.gx : multiPlayRecvData.gx[index];
              messageLog.gy = multiPlayRecvData.gy == null || index >= multiPlayRecvData.gy.Length ? messageLog.gy : multiPlayRecvData.gy[index];
              messageLog.ul = multiPlayRecvData.ul == null || index >= multiPlayRecvData.ul.Length ? messageLog.ul : multiPlayRecvData.ul[index] == 1;
              messageLog.d = multiPlayRecvData.d == null || index >= multiPlayRecvData.d.Length ? messageLog.d : multiPlayRecvData.d[index];
              this.message.logs.Add(messageLog);
              continue;
          }
        }
      }
    }

    public void Send()
    {
      string json;
      try
      {
        json = JsonUtility.ToJson((object) this.message);
      }
      catch (Exception ex)
      {
        return;
      }
      FlowNode_SendLogMessage.SendLogGenerator dict = new FlowNode_SendLogMessage.SendLogGenerator();
      dict.AddCommon(true, false, false, true);
      dict.Add("msg", json);
      FlowNode_SendLogMessage.SendLogMessage(dict, "MultiSequenceError");
    }

    [Serializable]
    public class Message
    {
      public string questId;
      public List<SequenceErrorSendLogMessage.MessagePack> messages;
      public List<SequenceErrorSendLogMessage.MessageLog> logs;

      public Message()
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) SceneBattle.Instance, (UnityEngine.Object) null) && SceneBattle.Instance.CurrentQuest != null)
          this.questId = SceneBattle.Instance.CurrentQuest.iname;
        this.messages = new List<SequenceErrorSendLogMessage.MessagePack>();
        this.logs = new List<SequenceErrorSendLogMessage.MessageLog>();
      }

      public void Add(SequenceErrorSendLogMessage.MessagePack mp) => this.messages.Add(mp);
    }

    [Serializable]
    public class MessagePack
    {
      public List<SequenceErrorSendLogMessage.MesageUnitDiff> diff;
      public SequenceErrorSendLogMessage.MessageData my;
      public SequenceErrorSendLogMessage.MessageData host;

      public MessagePack(SceneBattle.MultiPlayCheck m, SceneBattle.MultiPlayCheck h)
      {
        this.my = new SequenceErrorSendLogMessage.MessageData(m);
        this.host = new SequenceErrorSendLogMessage.MessageData(h);
        this.diff = new List<SequenceErrorSendLogMessage.MesageUnitDiff>();
        int num = Math.Min(this.my.units.Length, this.host.units.Length);
        for (int index = 0; index < num; ++index)
        {
          if (this.my.units[index].IsEqual(this.host.units[index]) == SequenceErrorSendLogMessage.MessageUnit.EqualType.NotEqual)
            this.diff.Add(new SequenceErrorSendLogMessage.MesageUnitDiff(this.my.units[index], this.host.units[index]));
        }
      }
    }

    [Serializable]
    public class MessageData
    {
      public int playerID;
      public int playerIndex;
      public int turn;
      public SequenceErrorSendLogMessage.MessageUnit[] units;
      public string rnd;

      public MessageData(SceneBattle.MultiPlayCheck d)
      {
        List<Unit> unitList = (List<Unit>) null;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) SceneBattle.Instance, (UnityEngine.Object) null) && SceneBattle.Instance.Battle != null && SceneBattle.Instance.Battle.AllUnits != null)
          unitList = SceneBattle.Instance.Battle.AllUnits;
        this.playerID = d.playerID;
        this.playerIndex = d.playerIndex;
        this.turn = d.battleTurn;
        this.rnd = d.rnd;
        int length = Mathf.Max(new int[4]
        {
          d.hp != null ? d.hp.Length : 0,
          d.gx != null ? d.gx.Length : 0,
          d.gy != null ? d.gy.Length : 0,
          d.dir != null ? d.dir.Length : 0
        });
        this.units = new SequenceErrorSendLogMessage.MessageUnit[length];
        for (int index = 0; index < length; ++index)
        {
          string _iname = string.Empty;
          long _iid = 0;
          if (unitList != null && unitList.Count > index)
          {
            _iname = unitList[index].UnitParam.iname;
            _iid = unitList[index].UnitData.UniqueID;
          }
          int _hp = d.hp == null || d.hp.Length <= index ? 0 : d.hp[index];
          int _x = d.gx == null || d.gx.Length <= index ? 0 : d.gx[index];
          int _y = d.gy == null || d.gy.Length <= index ? 0 : d.gy[index];
          int _dir = d.dir == null || d.dir.Length <= index ? 0 : d.dir[index];
          this.units[index] = new SequenceErrorSendLogMessage.MessageUnit(_iname, _iid, _hp, _x, _y, _dir);
        }
      }
    }

    [Serializable]
    public class MesageUnitDiff
    {
      public SequenceErrorSendLogMessage.MessageUnit my;
      public SequenceErrorSendLogMessage.MessageUnit host;

      public MesageUnitDiff(
        SequenceErrorSendLogMessage.MessageUnit _my,
        SequenceErrorSendLogMessage.MessageUnit _host)
      {
        this.my = _my;
        this.host = _host;
      }
    }

    [Serializable]
    public class MessageUnit
    {
      public string iname;
      public long iid;
      public int hp;
      public int x;
      public int y;
      public int dir;

      public MessageUnit(string _iname, long _iid, int _hp, int _x, int _y, int _dir)
      {
        this.iname = _iname;
        this.iid = _iid;
        this.hp = _hp;
        this.x = _x;
        this.y = _y;
        this.dir = _dir;
      }

      public SequenceErrorSendLogMessage.MessageUnit.EqualType IsEqual(
        SequenceErrorSendLogMessage.MessageUnit unit)
      {
        if (this.iid != unit.iid)
          return SequenceErrorSendLogMessage.MessageUnit.EqualType.FailedIID;
        return this.hp != unit.hp || this.x != unit.x || this.y != unit.y || this.dir != unit.dir ? SequenceErrorSendLogMessage.MessageUnit.EqualType.NotEqual : SequenceErrorSendLogMessage.MessageUnit.EqualType.Equal;
      }

      public enum EqualType
      {
        FailedIID,
        Equal,
        NotEqual,
      }
    }

    [Serializable]
    public class MessageLog
    {
      public string c;
      public int turn;
      public int ui = -1;
      public int ti = -1;
      public string s;
      public string i;
      public int gx;
      public int gy;
      public bool ul;
      public int d;
    }
  }
}
