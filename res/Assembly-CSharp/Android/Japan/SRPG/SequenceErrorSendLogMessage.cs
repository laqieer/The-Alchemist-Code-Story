// Decompiled with JetBrains decompiler
// Type: SRPG.SequenceErrorSendLogMessage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class SequenceErrorSendLogMessage
  {
    private SequenceErrorSendLogMessage.Message message = new SequenceErrorSendLogMessage.Message();

    public void Add(SceneBattle.MultiPlayCheck my, SceneBattle.MultiPlayCheck host)
    {
      this.message.Add(new SequenceErrorSendLogMessage.MessagePack(my, host));
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
    private class Message
    {
      public List<SequenceErrorSendLogMessage.MessagePack> messages;

      public Message()
      {
        this.messages = new List<SequenceErrorSendLogMessage.MessagePack>();
      }

      public void Add(SequenceErrorSendLogMessage.MessagePack mp)
      {
        this.messages.Add(mp);
      }
    }

    [Serializable]
    private class MessagePack
    {
      public SequenceErrorSendLogMessage.MessageData my;
      public SequenceErrorSendLogMessage.MessageData host;

      public MessagePack(SceneBattle.MultiPlayCheck m, SceneBattle.MultiPlayCheck h)
      {
        this.my = new SequenceErrorSendLogMessage.MessageData(m);
        this.host = new SequenceErrorSendLogMessage.MessageData(h);
      }
    }

    [Serializable]
    private class MessageData
    {
      public int playerID;
      public int playerIndex;
      public int turn;
      public SequenceErrorSendLogMessage.MessageUnit[] units;
      public string rnd;

      public MessageData(SceneBattle.MultiPlayCheck d)
      {
        this.playerID = d.playerID;
        this.playerIndex = d.playerIndex;
        this.turn = d.battleTurn;
        this.rnd = d.rnd;
        int length = Mathf.Max(d.hp != null ? d.hp.Length : 0, d.gx != null ? d.gx.Length : 0, d.gy != null ? d.gy.Length : 0, d.dir != null ? d.dir.Length : 0);
        this.units = new SequenceErrorSendLogMessage.MessageUnit[length];
        for (int index = 0; index < length; ++index)
        {
          int _hp = d.hp == null || d.hp.Length <= index ? 0 : d.hp[index];
          int _x = d.gx == null || d.gx.Length <= index ? 0 : d.gx[index];
          int _y = d.gy == null || d.gy.Length <= index ? 0 : d.gy[index];
          int _dir = d.dir == null || d.dir.Length <= index ? 0 : d.dir[index];
          this.units[index] = new SequenceErrorSendLogMessage.MessageUnit(_hp, _x, _y, _dir);
        }
      }
    }

    [Serializable]
    private class MessageUnit
    {
      public int hp;
      public int x;
      public int y;
      public int dir;

      public MessageUnit(int _hp, int _x, int _y, int _dir)
      {
        this.hp = _hp;
        this.x = _x;
        this.y = _y;
        this.dir = _dir;
      }
    }
  }
}
