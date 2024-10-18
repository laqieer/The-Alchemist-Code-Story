// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_StartMultiPlay
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using MessagePack;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Multi/StartMultiPlay", 32741)]
  [FlowNode.Pin(100, "Start", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(101, "ResumeStart", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(2, "Failure", FlowNode.PinTypes.Output, 3)]
  public class FlowNode_StartMultiPlay : FlowNode
  {
    private StateMachine<FlowNode_StartMultiPlay> mStateMachine;

    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 100:
          if (this.mStateMachine != null)
            break;
          this.mStateMachine = new StateMachine<FlowNode_StartMultiPlay>(this);
          this.mStateMachine.GotoState<FlowNode_StartMultiPlay.State_Start>();
          ((Behaviour) this).enabled = true;
          break;
        case 101:
          if (this.mStateMachine != null)
            break;
          this.mStateMachine = new StateMachine<FlowNode_StartMultiPlay>(this);
          this.mStateMachine.GotoState<FlowNode_StartMultiPlay.State_ResumeStart>();
          ((Behaviour) this).enabled = true;
          break;
      }
    }

    private void Update()
    {
      if (this.mStateMachine == null)
        return;
      this.mStateMachine.Update();
    }

    private void Success()
    {
      this.mStateMachine = (StateMachine<FlowNode_StartMultiPlay>) null;
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(1);
      DebugUtility.Log("StartMultiPlay success");
    }

    private void Failure()
    {
      this.mStateMachine = (StateMachine<FlowNode_StartMultiPlay>) null;
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(2);
      DebugUtility.Log("StartMultiPlay failure");
    }

    private void FailureStartMulti()
    {
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) instance, (UnityEngine.Object) null) && instance.IsOldestPlayer())
      {
        MyPhoton.MyRoom currentRoom = instance.GetCurrentRoom();
        if (currentRoom != null)
        {
          JSON_MyPhotonRoomParam myPhotonRoomParam = !string.IsNullOrEmpty(currentRoom.json) ? JSON_MyPhotonRoomParam.Parse(currentRoom.json) : (JSON_MyPhotonRoomParam) null;
          myPhotonRoomParam.started = 0;
          instance.SetRoomParam(myPhotonRoomParam.Serialize());
        }
        instance.OpenRoom();
      }
      this.Failure();
    }

    public void GotoState<StateType>() where StateType : State<FlowNode_StartMultiPlay>, new()
    {
      if (this.mStateMachine == null)
        return;
      this.mStateMachine.GotoState<StateType>();
    }

    public class PlayerList
    {
      public JSON_MyPhotonPlayerParam[] players;

      public string Serialize()
      {
        string str = "{\"players\":[";
        if (this.players != null)
        {
          bool flag = true;
          foreach (JSON_MyPhotonPlayerParam player in this.players)
          {
            if (flag)
              flag = false;
            else
              str += ",";
            str += player.Serialize();
          }
        }
        return str + "]}";
      }
    }

    [MessagePackObject(true)]
    public class RecvData
    {
      public int senderPlayerID;
      public int version;
      public Json_MyPhotonPlayerBinaryParam[] playerList;
      public string playerListJson;
    }

    private class State_Start : State<FlowNode_StartMultiPlay>
    {
      private int mPlayerNum;

      public override void Begin(FlowNode_StartMultiPlay self)
      {
        MyPhoton.MyRoom currentRoom = PunMonoSingleton<MyPhoton>.Instance.GetCurrentRoom();
        if (currentRoom == null)
          return;
        this.mPlayerNum = currentRoom.playerCount;
      }

      public override void Update(FlowNode_StartMultiPlay self)
      {
        DebugUtility.Log("StartMultiPlay State_Start Update");
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        if (instance.CurrentState != MyPhoton.MyState.ROOM)
        {
          self.Failure();
        }
        else
        {
          MyPhoton.MyRoom currentRoom = instance.GetCurrentRoom();
          if (currentRoom == null)
            self.Failure();
          else if (this.mPlayerNum != currentRoom.playerCount)
            self.FailureStartMulti();
          else if (!instance.IsOldestPlayer() && !currentRoom.start)
          {
            self.Failure();
          }
          else
          {
            bool flag = true;
            foreach (MyPhoton.MyPlayer roomPlayer in instance.GetRoomPlayerList())
            {
              JSON_MyPhotonPlayerParam photonPlayerParam = JSON_MyPhotonPlayerParam.Parse(roomPlayer.json);
              if (photonPlayerParam.state != 2 && photonPlayerParam.isSupportAI == 0)
              {
                flag = false;
                if (instance.IsOldestPlayer())
                {
                  if (!instance.IsOldestPlayer(photonPlayerParam.playerIndex))
                  {
                    if (photonPlayerParam.state != 1)
                    {
                      self.FailureStartMulti();
                      return;
                    }
                  }
                  else
                    continue;
                }
                DebugUtility.Log("StartMultiPlay State_Start Update allStart is false");
                break;
              }
            }
            if (flag)
            {
              DebugUtility.Log("StartMultiPlay State_Start Update change state to game start");
              self.GotoState<FlowNode_StartMultiPlay.State_GameStart>();
            }
            else if (currentRoom.start)
            {
              DebugUtility.Log("StartMultiPlay State_Start Update room is closed");
              JSON_MyPhotonPlayerParam photonPlayerParam = JSON_MyPhotonPlayerParam.Parse(instance.GetMyPlayer().json);
              if (photonPlayerParam.state == 2)
                return;
              photonPlayerParam.state = 2;
              instance.SetMyPlayerParam(photonPlayerParam.Serialize());
              DebugUtility.Log("StartMultiPlay State_Start Update update my state");
            }
            else
            {
              if (!instance.IsOldestPlayer())
                return;
              DebugUtility.Log("StartMultiPlay State_Start Update close room");
              instance.CloseRoom();
            }
          }
        }
      }

      public override void End(FlowNode_StartMultiPlay self)
      {
      }
    }

    private class State_ResumeStart : State<FlowNode_StartMultiPlay>
    {
      public override void Begin(FlowNode_StartMultiPlay self)
      {
      }

      public override void Update(FlowNode_StartMultiPlay self)
      {
        DebugUtility.Log("StartMultiPlay State_ResumeStart Update");
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        if (instance.CurrentState != MyPhoton.MyState.ROOM)
        {
          Debug.Log((object) ("MyState:" + instance.CurrentState.ToString() + "error:" + (object) instance.LastError));
          Debug.LogError((object) "state not Room");
          self.Failure();
        }
        else
        {
          MyPhoton.MyRoom currentRoom = instance.GetCurrentRoom();
          if (currentRoom == null)
          {
            Debug.Log((object) ("MyState:" + instance.CurrentState.ToString() + "error:" + (object) instance.LastError));
            Debug.LogError((object) "room null");
            self.Failure();
          }
          else
          {
            JSON_MyPhotonPlayerParam photonPlayerParam1 = JSON_MyPhotonPlayerParam.Parse(instance.GetMyPlayer().json);
            if (photonPlayerParam1.state != 2)
            {
              photonPlayerParam1.state = 2;
              instance.SetMyPlayerParam(photonPlayerParam1.Serialize());
            }
            JSON_MyPhotonRoomParam myPhotonRoomParam = !string.IsNullOrEmpty(currentRoom.json) ? JSON_MyPhotonRoomParam.Parse(currentRoom.json) : (JSON_MyPhotonRoomParam) null;
            if (myPhotonRoomParam == null)
            {
              Debug.Log((object) ("MyState:" + instance.CurrentState.ToString() + "error:" + (object) instance.LastError));
              Debug.LogError((object) "roomParam null");
              self.Failure();
            }
            else
            {
              GlobalVars.SelectedQuestID = myPhotonRoomParam.iname;
              GlobalVars.SelectedFriendID = (string) null;
              GlobalVars.SelectedFriend = (FriendData) null;
              GlobalVars.SelectedSupport.Set((SupportData) null);
              self.Success();
              DebugUtility.Log("StartMultiPlay: " + myPhotonRoomParam.Serialize());
              string roomParam = instance.GetRoomParam("started");
              if (roomParam != null)
              {
                FlowNode_StartMultiPlay.PlayerList jsonObject = JSONParser.parseJSONObject<FlowNode_StartMultiPlay.PlayerList>(roomParam);
                instance.SetPlayersStarted(jsonObject.players);
              }
              else
              {
                List<MyPhoton.MyPlayer> roomPlayerList = instance.GetRoomPlayerList();
                List<JSON_MyPhotonPlayerParam> photonPlayerParamList = new List<JSON_MyPhotonPlayerParam>();
                for (int index = 0; index < roomPlayerList.Count; ++index)
                {
                  JSON_MyPhotonPlayerParam photonPlayerParam2 = JSON_MyPhotonPlayerParam.Parse(roomPlayerList[index].json);
                  photonPlayerParam2.playerID = roomPlayerList[index].playerID;
                  photonPlayerParamList.Add(photonPlayerParam2);
                }
                photonPlayerParamList.Sort((Comparison<JSON_MyPhotonPlayerParam>) ((a, b) => a.playerIndex - b.playerIndex));
                instance.SetPlayersStarted(photonPlayerParamList.ToArray());
              }
              photonPlayerParam1.state = 3;
              instance.SetMyPlayerParam(photonPlayerParam1.Serialize());
              instance.SetResumeMyPlayer(GlobalVars.ResumeMultiplayPlayerID);
              instance.MyPlayerIndex = GlobalVars.ResumeMultiplaySeatID;
            }
          }
        }
      }

      public override void End(FlowNode_StartMultiPlay self)
      {
      }
    }

    private class State_GameStart : State<FlowNode_StartMultiPlay>
    {
      private FlowNode_StartMultiPlay.RecvData mSend = new FlowNode_StartMultiPlay.RecvData();
      private List<FlowNode_StartMultiPlay.RecvData> mRecvList = new List<FlowNode_StartMultiPlay.RecvData>();
      private float mWait;
      private bool mConfirm;
      private float mStartWait;

      public override void Begin(FlowNode_StartMultiPlay self)
      {
        foreach (MyPhoton.MyPlayer roomPlayer in PunMonoSingleton<MyPhoton>.Instance.GetRoomPlayerList())
        {
          JSON_MyPhotonPlayerParam photonPlayerParam = JSON_MyPhotonPlayerParam.Parse(roomPlayer.json);
          if (photonPlayerParam.state != 2 && photonPlayerParam.isSupportAI == 0)
          {
            self.FailureStartMulti();
            break;
          }
        }
      }

      public override void Update(FlowNode_StartMultiPlay self)
      {
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        if (instance.CurrentState != MyPhoton.MyState.ROOM)
        {
          self.Failure();
        }
        else
        {
          MyPhoton.MyRoom currentRoom = instance.GetCurrentRoom();
          if (currentRoom == null)
          {
            self.Failure();
          }
          else
          {
            JSON_MyPhotonRoomParam myPhotonRoomParam = !string.IsNullOrEmpty(currentRoom.json) ? JSON_MyPhotonRoomParam.Parse(currentRoom.json) : (JSON_MyPhotonRoomParam) null;
            if (myPhotonRoomParam == null)
            {
              self.Failure();
            }
            else
            {
              if (myPhotonRoomParam.started == 0)
              {
                myPhotonRoomParam.started = 1;
                instance.SetRoomParam(myPhotonRoomParam.Serialize());
              }
              if ((double) this.mStartWait > 0.0)
              {
                this.mStartWait -= Time.deltaTime;
                if ((double) this.mStartWait > 0.0)
                  return;
                GlobalVars.SelectedQuestID = myPhotonRoomParam.iname;
                GlobalVars.SelectedFriendID = (string) null;
                GlobalVars.SelectedFriend = (FriendData) null;
                GlobalVars.SelectedSupport.Set((SupportData) null);
                self.Success();
                DebugUtility.Log("StartMultiPlay: " + myPhotonRoomParam.Serialize());
              }
              else if ((double) this.mWait > 0.0)
              {
                this.mWait -= Time.deltaTime;
              }
              else
              {
                List<MyPhoton.MyPlayer> roomPlayerList = instance.GetRoomPlayerList();
                if ((myPhotonRoomParam.type == 1 || myPhotonRoomParam.type == 3) && roomPlayerList.Count == 1)
                  self.FailureStartMulti();
                else if (this.mConfirm)
                {
                  foreach (MyPhoton.MyPlayer myPlayer in roomPlayerList)
                  {
                    JSON_MyPhotonPlayerParam photonPlayerParam = JSON_MyPhotonPlayerParam.Parse(myPlayer.json);
                    if (photonPlayerParam.state != 3 && photonPlayerParam.isSupportAI == 0)
                      return;
                    if (photonPlayerParam.state < 2 && photonPlayerParam.isSupportAI == 0)
                    {
                      self.Failure();
                      return;
                    }
                  }
                  this.mStartWait = 0.1f;
                }
                else
                {
                  MyPhoton.MyPlayer myPlayer1 = instance.GetMyPlayer();
                  if (this.mRecvList.Count <= 0)
                  {
                    this.mSend.senderPlayerID = myPlayer1.photonPlayerID;
                    this.mSend.playerListJson = (string) null;
                    List<JSON_MyPhotonPlayerParam> photonPlayerParamList = new List<JSON_MyPhotonPlayerParam>();
                    for (int index = 0; index < roomPlayerList.Count; ++index)
                    {
                      JSON_MyPhotonPlayerParam photonPlayerParam = JSON_MyPhotonPlayerParam.Parse(roomPlayerList[index].json);
                      photonPlayerParamList.Add(photonPlayerParam);
                    }
                    photonPlayerParamList.Sort((Comparison<JSON_MyPhotonPlayerParam>) ((a, b) => a.playerIndex - b.playerIndex));
                    FlowNode_StartMultiPlay.PlayerList playerList = new FlowNode_StartMultiPlay.PlayerList();
                    playerList.players = photonPlayerParamList.ToArray();
                    Json_MyPhotonPlayerBinaryParam[] playerBinaryParamArray = new Json_MyPhotonPlayerBinaryParam[playerList.players.Length];
                    for (int index = 0; index < playerList.players.Length; ++index)
                    {
                      playerList.players[index].CreateJsonUnitData();
                      playerBinaryParamArray[index] = Json_MyPhotonPlayerBinaryParam.Create(playerList.players[index]);
                    }
                    this.mSend.playerList = playerBinaryParamArray;
                    byte[] msg = GameUtility.Object2Binary<FlowNode_StartMultiPlay.RecvData>(this.mSend);
                    instance.SendRoomMessageBinary(true, msg, MyPhoton.SEND_TYPE.Sync);
                    this.mRecvList.Add(this.mSend);
                    this.mSend.playerListJson = playerList.Serialize();
                  }
                  List<MyPhoton.MyEvent> events = instance.GetEvents();
                  for (int index = events.Count - 1; index >= 0; --index)
                  {
                    FlowNode_StartMultiPlay.RecvData buffer = (FlowNode_StartMultiPlay.RecvData) null;
                    if (!GameUtility.Binary2Object<FlowNode_StartMultiPlay.RecvData>(out buffer, events[index].binary))
                    {
                      DebugUtility.LogError("[PUN] started player list version error: " + events[index].json);
                      instance.Disconnect();
                      return;
                    }
                    if (buffer == null || buffer.version < this.mSend.version)
                    {
                      DebugUtility.LogError("[PUN] started player list version error: " + events[index].json);
                      instance.Disconnect();
                      return;
                    }
                    if (buffer.version <= this.mSend.version)
                    {
                      buffer.senderPlayerID = events[index].playerID;
                      DebugUtility.Log("[PUN] recv started player list: " + events[index].json);
                      this.mRecvList.Add(buffer);
                      events.Remove(events[index]);
                    }
                  }
                  foreach (MyPhoton.MyPlayer myPlayer2 in roomPlayerList)
                  {
                    MyPhoton.MyPlayer p = myPlayer2;
                    if (JSON_MyPhotonPlayerParam.Parse(p.json).isSupportAI == 0 && this.mRecvList.FindIndex((Predicate<FlowNode_StartMultiPlay.RecvData>) (r => r.senderPlayerID == p.photonPlayerID)) < 0)
                      return;
                  }
                  bool flag = true;
                  foreach (FlowNode_StartMultiPlay.RecvData mRecv in this.mRecvList)
                  {
                    if (mRecv.playerList.Length == this.mSend.playerList.Length)
                    {
                      for (int index = 0; index < this.mSend.playerList.Length; ++index)
                      {
                        if (!Json_MyPhotonPlayerBinaryParam.IsEqual(mRecv.playerList[index], this.mSend.playerList[index]))
                          flag = false;
                      }
                      if (!flag)
                        break;
                    }
                    else
                    {
                      flag = false;
                      break;
                    }
                  }
                  if (!flag)
                  {
                    DebugUtility.Log("[PUN] started player list is not equal. ver:" + (object) this.mSend.version);
                    this.mRecvList.Clear();
                    ++this.mSend.version;
                    this.mWait = 1f;
                  }
                  else
                  {
                    DebugUtility.Log("[PUN]started player list decided. ver:" + (object) this.mSend.version);
                    List<JSON_MyPhotonPlayerParam> myPlayersStarted = instance.GetMyPlayersStarted();
                    myPlayersStarted.Clear();
                    foreach (JSON_MyPhotonPlayerParam player in JSONParser.parseJSONObject<FlowNode_StartMultiPlay.PlayerList>(this.mSend.playerListJson).players)
                    {
                      player.SetupUnits();
                      myPlayersStarted.Add(player);
                    }
                    if (instance.IsOldestPlayer())
                      instance.UpdateRoomParam("started", (object) this.mSend.playerListJson);
                    if (events.Count > 0)
                      DebugUtility.LogError("[PUN] event must be empty.");
                    events.Clear();
                    JSON_MyPhotonPlayerParam photonPlayerParam = JSON_MyPhotonPlayerParam.Parse(myPlayer1.json);
                    photonPlayerParam.state = 3;
                    instance.SetMyPlayerParam(photonPlayerParam.Serialize());
                    this.mConfirm = true;
                  }
                }
              }
            }
          }
        }
      }

      public override void End(FlowNode_StartMultiPlay self)
      {
      }
    }
  }
}
