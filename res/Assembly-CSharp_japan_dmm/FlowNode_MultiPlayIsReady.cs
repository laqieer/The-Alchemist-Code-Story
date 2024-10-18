// Decompiled with JetBrains decompiler
// Type: FlowNode_MultiPlayIsReady
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using SRPG;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
[FlowNode.NodeType("Multi/MultiPlayIsReady", 32741)]
[FlowNode.Pin(100, "自分が準備OKか", FlowNode.PinTypes.Input, 100)]
[FlowNode.Pin(101, "全員が準備OKか", FlowNode.PinTypes.Input, 101)]
[FlowNode.Pin(102, "部屋主以外の全員が準備OKか", FlowNode.PinTypes.Input, 102)]
[FlowNode.Pin(200, "部屋が満員か", FlowNode.PinTypes.Input, 200)]
[FlowNode.Pin(300, "スタミナは足りているか", FlowNode.PinTypes.Input, 300)]
[FlowNode.Pin(301, "マルチタワーのスタミナは足りているか", FlowNode.PinTypes.Input, 301)]
[FlowNode.Pin(400, "ユニット配置が被っていないか？", FlowNode.PinTypes.Input, 400)]
[FlowNode.Pin(401, "出撃制限に引っかかっていないか？", FlowNode.PinTypes.Input, 401)]
[FlowNode.Pin(402, "自身の出撃制限に引っかかっていないか？", FlowNode.PinTypes.Input, 402)]
[FlowNode.Pin(403, "挑戦できる階層か？", FlowNode.PinTypes.Input, 403)]
[FlowNode.Pin(404, "自身ユニットが同時ユニット出撃制限", FlowNode.PinTypes.Input, 404)]
[FlowNode.Pin(1, "Yes", FlowNode.PinTypes.Output, 2)]
[FlowNode.Pin(2, "No", FlowNode.PinTypes.Output, 3)]
public class FlowNode_MultiPlayIsReady : FlowNode
{
  public override void OnActivate(int pinID)
  {
    switch (pinID)
    {
      case 100:
        MyPhoton.MyPlayer myPlayer1 = PunMonoSingleton<MyPhoton>.Instance.GetMyPlayer();
        JSON_MyPhotonPlayerParam photonPlayerParam1 = myPlayer1 != null ? JSON_MyPhotonPlayerParam.Parse(myPlayer1.json) : (JSON_MyPhotonPlayerParam) null;
        if (photonPlayerParam1 != null && photonPlayerParam1.state != 0 && photonPlayerParam1.state != 4)
        {
          this.ActivateOutputLinks(1);
          break;
        }
        this.ActivateOutputLinks(2);
        break;
      case 101:
        List<MyPhoton.MyPlayer> roomPlayerList1 = PunMonoSingleton<MyPhoton>.Instance.GetRoomPlayerList();
        if (roomPlayerList1 != null)
        {
          foreach (MyPhoton.MyPlayer myPlayer2 in roomPlayerList1)
          {
            JSON_MyPhotonPlayerParam photonPlayerParam2 = myPlayer2 != null ? JSON_MyPhotonPlayerParam.Parse(myPlayer2.json) : (JSON_MyPhotonPlayerParam) null;
            if (photonPlayerParam2 == null || photonPlayerParam2.state == 0 || photonPlayerParam2.state == 4 || photonPlayerParam2.state == 5)
            {
              this.ActivateOutputLinks(2);
              return;
            }
          }
        }
        this.ActivateOutputLinks(1);
        break;
      case 102:
        MyPhoton instance1 = PunMonoSingleton<MyPhoton>.Instance;
        List<MyPhoton.MyPlayer> roomPlayerList2 = instance1.GetRoomPlayerList();
        QuestParam quest1 = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
        if (quest1 != null && quest1.EntryCondition != null && quest1.EntryCondition.is_not_solo && roomPlayerList2.Count <= 1)
        {
          this.ActivateOutputLinks(2);
          break;
        }
        MyPhoton.MyPlayer myPlayer3 = instance1.GetMyPlayer();
        if (roomPlayerList2 != null && myPlayer3 != null)
        {
          foreach (MyPhoton.MyPlayer myPlayer4 in roomPlayerList2)
          {
            if (myPlayer4.playerID != myPlayer3.playerID)
            {
              JSON_MyPhotonPlayerParam photonPlayerParam3 = myPlayer4 != null ? JSON_MyPhotonPlayerParam.Parse(myPlayer4.json) : (JSON_MyPhotonPlayerParam) null;
              if (photonPlayerParam3 == null || photonPlayerParam3.state == 0 || photonPlayerParam3.state == 4 || photonPlayerParam3.state == 5 || myPlayer4.start)
              {
                this.ActivateOutputLinks(2);
                return;
              }
            }
          }
        }
        this.ActivateOutputLinks(1);
        break;
      case 200:
        MyPhoton.MyRoom currentRoom1 = PunMonoSingleton<MyPhoton>.Instance.GetCurrentRoom();
        int maxPlayers = currentRoom1.maxPlayers;
        if (currentRoom1 == null || currentRoom1.playerCount < maxPlayers)
        {
          this.ActivateOutputLinks(2);
          break;
        }
        this.ActivateOutputLinks(1);
        break;
      case 300:
        if (string.IsNullOrEmpty(GlobalVars.SelectedQuestID))
          this.ActivateOutputLinks(2);
        QuestParam quest2 = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
        PlayerData player1 = MonoSingleton<GameManager>.Instance.Player;
        if (quest2 != null && player1 != null)
        {
          if (player1.Stamina >= quest2.RequiredApWithPlayerLv(player1.Lv))
          {
            this.ActivateOutputLinks(1);
            break;
          }
          MonoSingleton<GameManager>.Instance.StartBuyStaminaSequence(true);
          this.ActivateOutputLinks(2);
          break;
        }
        this.ActivateOutputLinks(2);
        break;
      case 301:
        MyPhoton instance2 = PunMonoSingleton<MyPhoton>.Instance;
        if (Object.op_Inequality((Object) instance2, (Object) null))
        {
          List<MyPhoton.MyPlayer> roomPlayerList3 = instance2.GetRoomPlayerList();
          MyPhoton.MyRoom currentRoom2 = instance2.GetCurrentRoom();
          if (currentRoom2 == null)
          {
            this.ActivateOutputLinks(2);
            break;
          }
          JSON_MyPhotonRoomParam myPhotonRoomParam = JSON_MyPhotonRoomParam.Parse(currentRoom2.json);
          if (roomPlayerList3 == null || myPhotonRoomParam == null)
          {
            this.ActivateOutputLinks(2);
            break;
          }
          MultiTowerFloorParam mtFloorParam = MonoSingleton<GameManager>.Instance.GetMTFloorParam(GlobalVars.SelectedMultiTowerID, myPhotonRoomParam.challegedMTFloor);
          if (mtFloorParam == null)
          {
            this.ActivateOutputLinks(2);
            break;
          }
          QuestParam questParam = mtFloorParam.GetQuestParam();
          PlayerData player2 = MonoSingleton<GameManager>.Instance.Player;
          if (questParam != null && player2 != null)
          {
            if (player2.Stamina >= questParam.RequiredApWithPlayerLv(player2.Lv))
            {
              this.ActivateOutputLinks(1);
              break;
            }
            MonoSingleton<GameManager>.Instance.StartBuyStaminaSequence(true);
            this.ActivateOutputLinks(2);
            break;
          }
        }
        this.ActivateOutputLinks(2);
        break;
      case 400:
        MyPhoton instance3 = PunMonoSingleton<MyPhoton>.Instance;
        if (Object.op_Inequality((Object) instance3, (Object) null))
        {
          List<MyPhoton.MyPlayer> roomPlayerList4 = instance3.GetRoomPlayerList();
          MyPhoton.MyRoom currentRoom3 = instance3.GetCurrentRoom();
          if (currentRoom3 == null)
          {
            this.ActivateOutputLinks(2);
            break;
          }
          JSON_MyPhotonRoomParam myPhotonRoomParam = JSON_MyPhotonRoomParam.Parse(currentRoom3.json);
          if (roomPlayerList4 == null || myPhotonRoomParam == null)
          {
            this.ActivateOutputLinks(2);
            break;
          }
          MultiTowerFloorParam mtFloorParam = MonoSingleton<GameManager>.Instance.GetMTFloorParam(GlobalVars.SelectedMultiTowerID, myPhotonRoomParam.challegedMTFloor);
          if (mtFloorParam == null)
          {
            this.ActivateOutputLinks(2);
            break;
          }
          int totalUnitNum = JSON_MyPhotonRoomParam.GetTotalUnitNum(mtFloorParam.GetQuestParam());
          int[] numArray = new int[totalUnitNum];
          int num = 0;
          for (int index = 0; index < totalUnitNum; ++index)
            numArray[index] = -1;
          for (int index1 = 0; index1 < roomPlayerList4.Count; ++index1)
          {
            JSON_MyPhotonPlayerParam photonPlayerParam4 = JSON_MyPhotonPlayerParam.Parse(roomPlayerList4[index1].json);
            for (int index2 = 0; index2 < photonPlayerParam4.units.Length; ++index2)
            {
              if (photonPlayerParam4.units[index2] != null && photonPlayerParam4.units[index2].sub == 0 && photonPlayerParam4.units[index2].slotID >= 0)
                numArray[num++] = photonPlayerParam4.units[index2].place;
            }
          }
          for (int index3 = 0; index3 < totalUnitNum; ++index3)
          {
            if (numArray[index3] != -1)
            {
              for (int index4 = index3 + 1; index4 < totalUnitNum; ++index4)
              {
                if (numArray[index4] != -1 && numArray[index3] == numArray[index4])
                {
                  this.ActivateOutputLinks(2);
                  return;
                }
              }
            }
          }
          this.ActivateOutputLinks(1);
          break;
        }
        this.ActivateOutputLinks(2);
        break;
      case 401:
        MyPhoton instance4 = PunMonoSingleton<MyPhoton>.Instance;
        if (Object.op_Inequality((Object) instance4, (Object) null))
        {
          List<MyPhoton.MyPlayer> roomPlayerList5 = instance4.GetRoomPlayerList();
          MyPhoton.MyRoom currentRoom4 = instance4.GetCurrentRoom();
          if (currentRoom4 == null)
          {
            this.ActivateOutputLinks(2);
            break;
          }
          JSON_MyPhotonRoomParam myPhotonRoomParam = JSON_MyPhotonRoomParam.Parse(currentRoom4.json);
          if (roomPlayerList5 == null || myPhotonRoomParam == null)
          {
            this.ActivateOutputLinks(2);
            break;
          }
          MultiTowerFloorParam mtFloorParam = MonoSingleton<GameManager>.Instance.GetMTFloorParam(GlobalVars.SelectedMultiTowerID, myPhotonRoomParam.challegedMTFloor);
          if (mtFloorParam == null)
          {
            this.ActivateOutputLinks(2);
            break;
          }
          QuestParam questParam = mtFloorParam.GetQuestParam();
          for (int index5 = 0; index5 < roomPlayerList5.Count; ++index5)
          {
            JSON_MyPhotonPlayerParam photonPlayerParam5 = JSON_MyPhotonPlayerParam.Parse(roomPlayerList5[index5].json);
            for (int index6 = 0; index6 < photonPlayerParam5.units.Length; ++index6)
            {
              if (photonPlayerParam5.units[index6] != null)
              {
                UnitData unit = new UnitData();
                unit.Deserialize(photonPlayerParam5.units[index6].unitJson);
                if (!questParam.IsEntryQuestCondition(unit))
                {
                  this.ActivateOutputLinks(2);
                  return;
                }
              }
            }
          }
          this.ActivateOutputLinks(1);
          break;
        }
        this.ActivateOutputLinks(2);
        break;
      case 402:
        MyPhoton instance5 = PunMonoSingleton<MyPhoton>.Instance;
        if (Object.op_Inequality((Object) instance5, (Object) null))
        {
          MyPhoton.MyRoom currentRoom5 = instance5.GetCurrentRoom();
          if (currentRoom5 == null)
          {
            this.ActivateOutputLinks(2);
            break;
          }
          JSON_MyPhotonRoomParam myPhotonRoomParam = JSON_MyPhotonRoomParam.Parse(currentRoom5.json);
          if (myPhotonRoomParam == null)
          {
            this.ActivateOutputLinks(2);
            break;
          }
          MultiTowerFloorParam mtFloorParam = MonoSingleton<GameManager>.Instance.GetMTFloorParam(GlobalVars.SelectedMultiTowerID, myPhotonRoomParam.challegedMTFloor);
          if (mtFloorParam == null)
          {
            this.ActivateOutputLinks(2);
            break;
          }
          MyPhoton.MyPlayer myPlayer5 = instance5.GetMyPlayer();
          if (myPlayer5 == null)
          {
            this.ActivateOutputLinks(2);
            break;
          }
          JSON_MyPhotonPlayerParam photonPlayerParam6 = JSON_MyPhotonPlayerParam.Parse(myPlayer5.json);
          QuestParam questParam = mtFloorParam.GetQuestParam();
          if (photonPlayerParam6 == null)
          {
            this.ActivateOutputLinks(2);
            break;
          }
          for (int index = 0; index < photonPlayerParam6.units.Length; ++index)
          {
            if (photonPlayerParam6.units[index] != null)
            {
              UnitData unit = new UnitData();
              unit.Deserialize(photonPlayerParam6.units[index].unitJson);
              if (!questParam.IsEntryQuestCondition(unit))
              {
                this.ActivateOutputLinks(2);
                return;
              }
            }
          }
          this.ActivateOutputLinks(1);
          break;
        }
        this.ActivateOutputLinks(2);
        break;
      case 403:
        MyPhoton instance6 = PunMonoSingleton<MyPhoton>.Instance;
        if (Object.op_Inequality((Object) instance6, (Object) null))
        {
          List<MyPhoton.MyPlayer> roomPlayerList6 = instance6.GetRoomPlayerList();
          MyPhoton.MyRoom currentRoom6 = instance6.GetCurrentRoom();
          if (currentRoom6 == null)
          {
            this.ActivateOutputLinks(2);
            break;
          }
          JSON_MyPhotonRoomParam myPhotonRoomParam = JSON_MyPhotonRoomParam.Parse(currentRoom6.json);
          if (myPhotonRoomParam == null)
          {
            this.ActivateOutputLinks(2);
            break;
          }
          MultiTowerFloorParam mtFloorParam = MonoSingleton<GameManager>.Instance.GetMTFloorParam(GlobalVars.SelectedMultiTowerID, myPhotonRoomParam.challegedMTFloor);
          if (mtFloorParam == null)
          {
            this.ActivateOutputLinks(2);
            break;
          }
          if (mtFloorParam.GetQuestParam() == null)
          {
            this.ActivateOutputLinks(2);
            break;
          }
          for (int index = 0; index < roomPlayerList6.Count; ++index)
          {
            JSON_MyPhotonPlayerParam photonPlayerParam7 = JSON_MyPhotonPlayerParam.Parse(roomPlayerList6[index].json);
            if (photonPlayerParam7 != null && myPhotonRoomParam.challegedMTFloor > photonPlayerParam7.mtChallengeFloor)
            {
              this.ActivateOutputLinks(2);
              return;
            }
          }
          this.ActivateOutputLinks(1);
          break;
        }
        this.ActivateOutputLinks(2);
        break;
      case 404:
        MyPhoton instance7 = PunMonoSingleton<MyPhoton>.Instance;
        if (Object.op_Inequality((Object) instance7, (Object) null))
        {
          MyPhoton.MyRoom currentRoom7 = instance7.GetCurrentRoom();
          if (currentRoom7 == null)
          {
            this.ActivateOutputLinks(2);
            break;
          }
          if (JSON_MyPhotonRoomParam.Parse(currentRoom7.json) == null)
          {
            this.ActivateOutputLinks(2);
            break;
          }
          MyPhoton.MyPlayer myPlayer6 = instance7.GetMyPlayer();
          if (myPlayer6 == null)
          {
            this.ActivateOutputLinks(2);
            break;
          }
          JSON_MyPhotonPlayerParam photonPlayerParam8 = JSON_MyPhotonPlayerParam.Parse(myPlayer6.json);
          GameManager instance8 = MonoSingleton<GameManager>.Instance;
          if (photonPlayerParam8 == null || Object.op_Equality((Object) instance8, (Object) null))
          {
            this.ActivateOutputLinks(2);
            break;
          }
          if (photonPlayerParam8.units.Length > 1)
          {
            UnitData[] units = new UnitData[photonPlayerParam8.units.Length];
            for (int index = 0; index < photonPlayerParam8.units.Length; ++index)
            {
              if (photonPlayerParam8.units[index] != null)
              {
                UnitData unitData = new UnitData();
                unitData.Deserialize(photonPlayerParam8.units[index].unitJson);
                units[index] = unitData;
              }
            }
            for (int index = 0; index < units.Length; ++index)
            {
              if (UnitSameGroupParam.IsSameUnitInParty(units, units[index].UnitID))
              {
                UnitSameGroupParam unitSameGroup = instance8.MasterParam.GetUnitSameGroup(units[index].UnitID);
                if (unitSameGroup != null)
                {
                  UIUtility.NegativeSystemMessage((string) null, string.Format(LocalizedText.Get("sys.PARTY_SAMEUNIT_INPARTY"), (object) unitSameGroup.GetGroupUnitAllNameText()), (UIUtility.DialogResultEvent) (dialog => { }));
                  this.ActivateOutputLinks(2);
                  return;
                }
              }
            }
          }
          this.ActivateOutputLinks(1);
          break;
        }
        this.ActivateOutputLinks(2);
        break;
    }
  }
}
