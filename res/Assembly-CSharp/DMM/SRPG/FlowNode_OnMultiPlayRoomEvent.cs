// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_OnMultiPlayRoomEvent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [AddComponentMenu("")]
  [FlowNode.NodeType("Multi/OnMultiPlayRoomEvent", 58751)]
  [FlowNode.Pin(100, "Ignore On", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "Ignore Off", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(200, "Ignore Full On", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(201, "Ignore Full Off", FlowNode.PinTypes.Input, 4)]
  [FlowNode.Pin(300, "Reset", FlowNode.PinTypes.Input, 16)]
  [FlowNode.Pin(1, "OnDisconnected", FlowNode.PinTypes.Output, 5)]
  [FlowNode.Pin(2, "OnPlayerChanged", FlowNode.PinTypes.Output, 6)]
  [FlowNode.Pin(3, "OnAllPlayerReady", FlowNode.PinTypes.Output, 7)]
  [FlowNode.Pin(4, "OnAllPlayerNotReady", FlowNode.PinTypes.Output, 8)]
  [FlowNode.Pin(5, "OnRoomClosed", FlowNode.PinTypes.Output, 9)]
  [FlowNode.Pin(6, "OnRoomCommentChanged", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(7, "OnRoomCreatorOut", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(8, "OnRoomFullMember", FlowNode.PinTypes.Output, 12)]
  [FlowNode.Pin(9, "OnRoomOnlyMember", FlowNode.PinTypes.Output, 13)]
  [FlowNode.Pin(10, "OnRoomParam", FlowNode.PinTypes.Output, 14)]
  [FlowNode.Pin(11, "OnRoomBattleSpeedChanged", FlowNode.PinTypes.Output, 15)]
  [FlowNode.Pin(12, "OnRoomAutoAllowedChanged", FlowNode.PinTypes.Output, 16)]
  [FlowNode.Pin(50, "OnRoomPassChanged", FlowNode.PinTypes.Output, 50)]
  public class FlowNode_OnMultiPlayRoomEvent : FlowNodePersistent
  {
    private const int PIN_INPUT_IGNORE_ON = 100;
    private const int PIN_INPUT_IGNORE_OFF = 101;
    private const int PIN_INPUT_IGNORE_ON_FULL = 200;
    private const int PIN_INPUT_IGNORE_OFF_FULL = 201;
    private const int PIN_INPUT_RESET = 300;
    private const int PIN_OUTPUT_ON_DISCONNECTED = 1;
    private const int PIN_OUTPUT_ON_PLAYER_CHANGED = 2;
    private const int PIN_OUTPUT_ON_ALL_PLAYERS_READY = 3;
    private const int PIN_OUTPUT_ON_ALL_PLAYER_NOT_READY = 4;
    private const int PIN_OUTPUT_ON_ROOM_CLOSED = 5;
    private const int PIN_OUTPUT_ON_ROOM_COMMENT_CHANGED = 6;
    private const int PIN_OUTPUT_ON_ROOM_CREATOR_OUT = 7;
    private const int PIN_OUTPUT_ON_ROOM_FULL_MEMBER = 8;
    private const int PIN_OUTPUT_ON_ROOM_ONLY_MEMBER = 9;
    private const int PIN_OUTPUT_ON_ROOM_PARAM = 10;
    private const int PIN_OUTPUT_ON_ROOM_BTL_SPEED_CHANGED = 11;
    private const int PIN_OUTPUT_ON_ROOM_AUTO_ALLOWED_CHANGED = 12;
    private const int PIN_OUTPUT_ON_ROOM_PASS_CHANGED = 50;
    private List<MyPhoton.MyPlayer> mPlayers;
    private string mRoomPass = string.Empty;
    private string mRoomComment = string.Empty;
    private string mQuestName = string.Empty;
    private bool mIgnore;
    private bool mIgnoreFullMember = true;
    private int mMemberCnt;
    private int mBattleSpeed = 1;
    private bool mAutoAllowed;

    private void Start()
    {
      this.mIgnore = false;
      this.mIgnoreFullMember = true;
      this.mQuestName = GlobalVars.SelectedQuestID;
    }

    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 100:
          this.mIgnore = true;
          break;
        case 101:
          this.mIgnore = false;
          break;
        case 200:
          this.mIgnoreFullMember = true;
          break;
        case 201:
          this.mIgnoreFullMember = false;
          break;
        case 300:
          this.Reset();
          break;
      }
    }

    private void Update()
    {
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      if (this.mIgnore)
        return;
      if (instance.CurrentState != MyPhoton.MyState.ROOM)
      {
        if (instance.CurrentState != MyPhoton.MyState.NOP)
          instance.Disconnect();
        this.ActivateOutputLinks(1);
      }
      else
      {
        List<MyPhoton.MyPlayer> roomPlayerList = instance.GetRoomPlayerList(true);
        bool flag1 = GlobalVars.SelectedMultiPlayRoomType == JSON_MyPhotonRoomParam.EType.RAID;
        bool flag2 = GlobalVars.SelectedMultiPlayRoomType == JSON_MyPhotonRoomParam.EType.VERSUS && GlobalVars.SelectedMultiPlayVersusType == VERSUS_TYPE.Friend;
        bool flag3 = GlobalVars.SelectedMultiPlayRoomType == JSON_MyPhotonRoomParam.EType.TOWER;
        if (roomPlayerList.Find((Predicate<MyPhoton.MyPlayer>) (p => p.playerID == 1)) == null && (flag1 || flag2 || flag3))
        {
          this.ActivateOutputLinks(7);
        }
        else
        {
          MyPhoton.MyRoom currentRoom = instance.GetCurrentRoom();
          if (instance.IsUpdateRoomProperty)
          {
            if (currentRoom.start)
            {
              this.ActivateOutputLinks(5);
              return;
            }
            instance.IsUpdateRoomProperty = false;
          }
          JSON_MyPhotonRoomParam myPhotonRoomParam = currentRoom.param;
          string str1 = myPhotonRoomParam != null ? myPhotonRoomParam.comment : string.Empty;
          if (!this.mRoomComment.Equals(str1))
          {
            DebugUtility.Log("change room comment");
            this.ActivateOutputLinks(6);
          }
          this.mRoomComment = str1;
          string str2 = myPhotonRoomParam != null ? myPhotonRoomParam.passCode : string.Empty;
          if (!this.mRoomPass.Equals(str2))
          {
            DebugUtility.Log("change room pass");
            this.ActivateOutputLinks(50);
          }
          this.mRoomPass = str2;
          if (this.mBattleSpeed != myPhotonRoomParam.btlSpd)
          {
            DebugUtility.Log("change room battle speed");
            this.ActivateOutputLinks(11);
          }
          this.mBattleSpeed = myPhotonRoomParam.btlSpd;
          if (this.mAutoAllowed != (myPhotonRoomParam.autoAllowed != 0))
          {
            DebugUtility.Log("change room auto allowed");
            this.ActivateOutputLinks(12);
          }
          this.mAutoAllowed = myPhotonRoomParam.autoAllowed != 0;
          if (MonoSingleton<GameManager>.Instance.VSDraftId != (long) myPhotonRoomParam.draft_deck_id)
            MonoSingleton<GameManager>.Instance.VSDraftId = (long) myPhotonRoomParam.draft_deck_id;
          bool flag4 = false;
          if (roomPlayerList == null)
            instance.Disconnect();
          else if (this.mPlayers == null)
            flag4 = true;
          else if (this.mPlayers.Count != roomPlayerList.Count)
          {
            flag4 = true;
          }
          else
          {
            for (int index1 = 0; index1 < this.mPlayers.Count; ++index1)
            {
              int index2 = -1;
              for (int index3 = 0; index3 < roomPlayerList.Count; ++index3)
              {
                if (roomPlayerList[index3] != null && roomPlayerList[index3].playerID == this.mPlayers[index1].playerID)
                {
                  index2 = index3;
                  break;
                }
              }
              if (index2 < 0)
              {
                flag4 = true;
                break;
              }
              if (!this.mPlayers[index1].json.Equals(roomPlayerList[index2].json))
              {
                flag4 = true;
                break;
              }
            }
          }
          if (!string.IsNullOrEmpty(this.mQuestName) && !this.mQuestName.Equals(myPhotonRoomParam.iname))
          {
            DebugUtility.Log("change quest iname" + myPhotonRoomParam.iname);
            this.ActivateOutputLinks(10);
          }
          this.mQuestName = myPhotonRoomParam.iname;
          if (flag4)
          {
            this.mPlayers = new List<MyPhoton.MyPlayer>((IEnumerable<MyPhoton.MyPlayer>) roomPlayerList);
            this.ActivateOutputLinks(2);
            if (instance.IsOldestPlayer())
            {
              JSON_MyPhotonPlayerParam[] photonPlayerParamArray = new JSON_MyPhotonPlayerParam[roomPlayerList.Count];
              for (int index = 0; index < roomPlayerList.Count; ++index)
                photonPlayerParamArray[index] = roomPlayerList[index].param == null ? JSON_MyPhotonPlayerParam.Parse(roomPlayerList[index].json) : roomPlayerList[index].param;
              myPhotonRoomParam.players = photonPlayerParamArray;
              instance.SetRoomParam(myPhotonRoomParam.Serialize());
            }
            bool flag5 = true;
            foreach (MyPhoton.MyPlayer mPlayer in this.mPlayers)
            {
              JSON_MyPhotonPlayerParam photonPlayerParam = JSON_MyPhotonPlayerParam.Parse(mPlayer.json);
              if (photonPlayerParam.state == 0 || photonPlayerParam.state == 4 || photonPlayerParam.state == 5)
              {
                flag5 = false;
                break;
              }
            }
            if (flag5)
              this.ActivateOutputLinks(3);
            else
              this.ActivateOutputLinks(4);
          }
          else
          {
            int count = roomPlayerList.Count;
            if (count == 1 && this.mMemberCnt != count)
              this.ActivateOutputLinks(9);
            this.mMemberCnt = count;
            if (this.mIgnoreFullMember || currentRoom.maxPlayers - 1 != count)
              return;
            this.ActivateOutputLinks(8);
          }
        }
      }
    }

    private void Reset()
    {
      this.mPlayers = (List<MyPhoton.MyPlayer>) null;
      this.mRoomPass = string.Empty;
      this.mRoomComment = string.Empty;
      this.mQuestName = string.Empty;
      this.mMemberCnt = 0;
      this.mBattleSpeed = 1;
    }
  }
}
