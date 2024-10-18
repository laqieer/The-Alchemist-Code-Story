// Decompiled with JetBrains decompiler
// Type: SRPG.RoomPlayerList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [AddComponentMenu("Multi/部屋に参加中のプレイヤー一覧")]
  [FlowNode.Pin(0, "表示", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "表示更新", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(2, "階層更新", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(3, "階層更新完了", FlowNode.PinTypes.Output, 3)]
  [FlowNode.Pin(10, "データソース更新", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(101, "情報を見る", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(200, "チーム編成ボタンが押された", FlowNode.PinTypes.Output, 200)]
  [FlowNode.Pin(300, "部屋のプレイヤーがキックされた", FlowNode.PinTypes.Input, 300)]
  [FlowNode.Pin(310, "フレンドAI選択ボタン押下", FlowNode.PinTypes.Input, 310)]
  [FlowNode.Pin(311, "フレンドAI選択ポップアップ開く", FlowNode.PinTypes.Output, 311)]
  [FlowNode.Pin(312, "フレンドAIのキック完了", FlowNode.PinTypes.Output, 312)]
  public class RoomPlayerList : MonoBehaviour, IFlowInterface
  {
    private const int PIN_IN_VIEW = 0;
    private const int PIN_IN_UPDATE_FLOOR = 2;
    private const int PIN_IN_UPDATE_DATA_SOURCE = 10;
    private const int PIN_IN_ON_KICK_ROOM_MEMBER = 300;
    private const int PIN_IN_ON_SUPPORT_FRIEND = 310;
    private const int PIN_OUT_OPEN_SUPPORT_FRIEND = 311;
    private const int PIN_OUT_KICKED_SUPPORT = 312;
    private const int PIN_OUT_UPDATE_VIEW = 1;
    private const int PIN_OUT_UPDATE_FLOOR_COMPLETE = 3;
    private const int PIN_OUT_SHOW_DETAIL = 101;
    private const int PIN_OUT_ON_EDIT_TEAM = 200;
    [Description("リストアイテムとして使用するゲームオブジェクト")]
    public GameObject ItemTemplate;
    [Description("リストアイテムとして使用するゲームオブジェクト(ペアマルチ)")]
    public GameObject ItemTemplatePair;
    [Description("大本ゲームオブジェクト")]
    public GameObject Root;
    [Description("編成ランキングボタン")]
    public GameObject RankButton;
    [Description("スキル名")]
    public GameObject SkillObj;
    [Description("スキル詳細用プレハブ")]
    public GameObject Prefab_LeaderSkillDetail;
    public ScrollRect ScrollRect;
    public List<GameObject> UIItemList = new List<GameObject>();
    public List<MultiSupportData> MultiSupportList = new List<MultiSupportData>();
    public bool SupportRefresIsSkip;
    private int mSupportAIIndex = 1;
    [Description("プレイヤーのパーティ情報表示用のゲームオブジェクト")]
    public GameObject PlayerInfo;
    private List<GameObject> PlayerInfoList = new List<GameObject>();

    public static RoomPlayerList Instance { get; private set; }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 0:
          this.Refresh();
          break;
        case 2:
          this.RefreshFloorQuest();
          break;
        default:
          if (pinID != 10)
          {
            if (pinID != 300)
            {
              if (pinID != 310)
                break;
              this.OpenFriendPopup();
              break;
            }
            this.KickMember();
            break;
          }
          DataSource.Bind<JSON_MyPhotonRoomParam>(this.Root, JSON_MyPhotonRoomParam.Parse(PunMonoSingleton<MyPhoton>.Instance.GetCurrentRoom().json));
          break;
      }
    }

    private void Awake()
    {
      RoomPlayerList.Instance = this;
      this.SupportRefresIsSkip = false;
      this.mSupportAIIndex = 1;
      ((Behaviour) this).enabled = true;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ItemTemplate, (UnityEngine.Object) null))
        this.ItemTemplate.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ItemTemplatePair, (UnityEngine.Object) null))
      {
        this.ItemTemplatePair.SetActive(false);
        ((Component) this.ItemTemplatePair.transform.parent).gameObject.SetActive(false);
      }
      JSON_MyPhotonRoomParam data = JSON_MyPhotonRoomParam.Parse(PunMonoSingleton<MyPhoton>.Instance.GetCurrentRoom().json);
      QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(data.iname);
      DataSource.Bind<JSON_MyPhotonRoomParam>(this.Root, data);
      DataSource.Bind<QuestParam>(this.Root, quest);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RankButton, (UnityEngine.Object) null))
        this.RankButton.gameObject.SetActive(quest.IsJigen);
      GameParameter.UpdateAll(this.Root);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 1);
    }

    private void OnDestroy() => RoomPlayerList.Instance = (RoomPlayerList) null;

    private void Start() => this.RefreshItems();

    public void Refresh()
    {
      this.RefreshItems();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ScrollRect, (UnityEngine.Object) null))
        return;
      ListExtras component = ((Component) this.ScrollRect).GetComponent<ListExtras>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        component.SetScrollPos(1f);
      else
        this.ScrollRect.normalizedPosition = Vector2.one;
    }

    public void RefreshFloorQuest()
    {
      JSON_MyPhotonRoomParam data = JSON_MyPhotonRoomParam.Parse(PunMonoSingleton<MyPhoton>.Instance.GetCurrentRoom().json);
      QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(data.iname);
      DataSource.Bind<JSON_MyPhotonRoomParam>(this.Root, data);
      DataSource.Bind<QuestParam>(this.Root, quest);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 3);
    }

    private void RefreshItems()
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      RoomPlayerList.\u003CRefreshItems\u003Ec__AnonStorey2 itemsCAnonStorey2_1 = new RoomPlayerList.\u003CRefreshItems\u003Ec__AnonStorey2();
      // ISSUE: reference to a compiler-generated field
      itemsCAnonStorey2_1.\u0024this = this;
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      MyPhoton.MyRoom currentRoom = instance.GetCurrentRoom();
      if (currentRoom == null)
        return;
      JSON_MyPhotonRoomParam data1 = JSON_MyPhotonRoomParam.Parse(currentRoom.json);
      QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(data1.iname);
      Transform transform = ((Component) this).transform;
      GameObject gameObject = this.ItemTemplate;
      if ((quest.type == QuestTypes.Multi || quest.type == QuestTypes.MultiGps) && currentRoom.maxPlayers == 2)
      {
        gameObject = this.ItemTemplatePair;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ItemTemplate, (UnityEngine.Object) null))
        {
          GameUtility.SetGameObjectActive((Component) this.ItemTemplatePair.transform.parent, true);
          GameUtility.SetGameObjectActive((Component) this.ItemTemplate.transform.parent, false);
        }
      }
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
        return;
      Transform parent = gameObject.transform.parent;
      JSON_MyPhotonPlayerParam owner = data1?.GetOwner();
      int playerIndex1 = owner != null ? owner.playerIndex : 0;
      List<MyPhoton.MyPlayer> roomPlayerList1 = instance.GetRoomPlayerList();
      for (int count = this.UIItemList.Count; count < currentRoom.maxPlayers; ++count)
      {
        this.UIItemList.Add(UnityEngine.Object.Instantiate<GameObject>(gameObject));
        if (GlobalVars.SelectedMultiPlayRoomType == JSON_MyPhotonRoomParam.EType.TOWER)
        {
          this.PlayerInfo.SetActive(false);
          this.PlayerInfoList.Add(UnityEngine.Object.Instantiate<GameObject>(this.PlayerInfo));
        }
      }
      int maxPlayers = currentRoom.maxPlayers;
      if (GlobalVars.SelectedMultiPlayRoomType == JSON_MyPhotonRoomParam.EType.VERSUS)
        --maxPlayers;
      // ISSUE: reference to a compiler-generated field
      itemsCAnonStorey2_1.supportList = new List<JSON_MyPhotonPlayerParam>();
      if (data1.support != null)
      {
        // ISSUE: reference to a compiler-generated field
        itemsCAnonStorey2_1.supportList.AddRange((IEnumerable<JSON_MyPhotonPlayerParam>) data1.support);
      }
      for (int index1 = 0; index1 < maxPlayers; ++index1)
      {
        JSON_MyPhotonPlayerParam data2 = (JSON_MyPhotonPlayerParam) null;
        int playerIndex = index1 + 1;
        if (playerIndex1 > 0)
        {
          if (index1 == 0)
            playerIndex = playerIndex1;
          else if (index1 < playerIndex1)
            playerIndex = index1;
        }
        List<MyPhoton.MyPlayer> all = roomPlayerList1.FindAll((Predicate<MyPhoton.MyPlayer>) (p =>
        {
          if (p.param == null)
          {
            if (p.json == null)
              return false;
            p.param = JSON_MyPhotonPlayerParam.Parse(p.json);
            if (p.param == null)
              return false;
          }
          return p.param.playerIndex == playerIndex;
        }));
        if (all.Count > 1)
          all.RemoveAll((Predicate<MyPhoton.MyPlayer>) (p =>
          {
            // ISSUE: variable of a compiler-generated type
            RoomPlayerList.\u003CRefreshItems\u003Ec__AnonStorey2 itemsCAnonStorey2 = itemsCAnonStorey2_1;
            // ISSUE: variable of a compiler-generated type
            RoomPlayerList.\u003CRefreshItems\u003Ec__AnonStorey0 itemsCAnonStorey0 = this;
            MyPhoton.MyPlayer p1 = p;
            if (p1.param.isSupportAI == 0)
              return false;
            MultiSupportData rs = this.MultiSupportList.Find((Predicate<MultiSupportData>) (s => s.UID == p1.param.UID));
            if (rs == null)
              return false;
            supportList.RemoveAll((Predicate<JSON_MyPhotonPlayerParam>) (s => s.UID == rs.UID));
            this.MultiSupportList.Remove(rs);
            return true;
          }));
        if (all.Count > 0)
          data2 = all[0].param;
        if (data2 == null)
        {
          data2 = new JSON_MyPhotonPlayerParam();
          data2.playerIndex = playerIndex;
        }
        GameObject uiItem = this.UIItemList[index1];
        ((UnityEngine.Object) uiItem).hideFlags = (HideFlags) 52;
        DataSource.Bind<JSON_MyPhotonPlayerParam>(uiItem, data2);
        DataSource.Bind<JSON_MyPhotonRoomParam>(uiItem, data1);
        if (GlobalVars.SelectedMultiPlayRoomType == JSON_MyPhotonRoomParam.EType.TOWER)
        {
          DataSource.Bind<JSON_MyPhotonPlayerParam>(this.PlayerInfoList[index1], data2);
          DataSource.Bind<JSON_MyPhotonRoomParam>(this.PlayerInfoList[index1], data1);
          this.PlayerInfoList[index1].transform.SetParent(this.PlayerInfo.transform.parent, false);
          this.PlayerInfoList[index1].gameObject.SetActive(true);
        }
        GameObjectID[] componentsInChildren = uiItem.GetComponentsInChildren<GameObjectID>(true);
        if (data2 != null && componentsInChildren != null)
        {
          for (int index2 = 0; index2 < componentsInChildren.Length; ++index2)
          {
            GameObjectID gameObjectId = componentsInChildren[index2];
            if (gameObjectId.ID != null)
            {
              if (gameObjectId.ID.StartsWith("unit"))
                DataSource.Bind<UnitData>(((Component) gameObjectId).gameObject, (UnitData) null, true);
              else if (gameObjectId.ID.StartsWith("card"))
              {
                ConceptCardSlots component = ((Component) gameObjectId).GetComponent<ConceptCardSlots>();
                if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
                {
                  DataSource.Bind<UnitData>(((Component) ((Component) gameObjectId).transform.parent).gameObject, (UnitData) null, true);
                  component.Refresh(false);
                }
              }
              else if (gameObjectId.ID.StartsWith("arti"))
              {
                ArtifactSlots component = ((Component) gameObjectId).GetComponent<ArtifactSlots>();
                if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
                {
                  DataSource.Bind<UnitData>(((Component) ((Component) gameObjectId).transform.parent).gameObject, (UnitData) null, true);
                  component.Refresh(false);
                }
              }
              if (GlobalVars.SelectedMultiPlayRoomType == JSON_MyPhotonRoomParam.EType.TOWER)
                DataSource.Bind<UnitSameGroupParam>(((Component) gameObjectId).gameObject, (UnitSameGroupParam) null);
            }
          }
          if (data1.draft_type == 1 && !string.IsNullOrEmpty(data2.support_unit))
          {
            GameObjectID gameObjectId = Array.Find<GameObjectID>(componentsInChildren, (Predicate<GameObjectID>) (goID => goID.ID.Equals("unit0")));
            Json_Unit jsonObject = JSONParser.parseJSONObject<Json_Unit>(data2.support_unit);
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObjectId, (UnityEngine.Object) null) && jsonObject != null)
            {
              UnitData data3 = new UnitData();
              data3.Deserialize(jsonObject);
              DataSource.Bind<UnitData>(((Component) gameObjectId).gameObject, data3);
            }
          }
          else if (data2.units != null)
          {
            List<UnitData> unitDataList = new List<UnitData>();
            for (int index3 = 0; index3 < data2.units.Length; ++index3)
            {
              UnitData unit = data2.units[index3].unit;
              if (unit != null)
                unitDataList.Add(unit);
            }
            for (int index4 = 0; index4 < data2.units.Length; ++index4)
            {
              int slotID = data2.units[index4].slotID;
              bool flag1 = data2.units[index4].sub == 1;
              UnitData data4 = data2.units[index4].unit;
              if (data4 != null)
              {
                if (GlobalVars.SelectedMultiPlayRoomType == JSON_MyPhotonRoomParam.EType.TOWER)
                {
                  for (int index5 = 0; index5 < componentsInChildren.Length; ++index5)
                  {
                    GameObjectID gameObjectId = componentsInChildren[index5];
                    if (gameObjectId.ID != null && (gameObjectId.ID.Equals("unit" + (object) slotID) || flag1) && (!flag1 || index5 == componentsInChildren.Length - 1))
                    {
                      data4.TempFlags |= UnitData.TemporaryFlags.AllowJobChange;
                      DataSource.Bind<UnitData>(((Component) gameObjectId).gameObject, data4);
                      UnitIcon component = ((Component) gameObjectId).gameObject.GetComponent<UnitIcon>();
                      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null) && data2.playerIndex > 0)
                      {
                        DataSource.Bind<PlayerPartyTypes>(((Component) gameObjectId).gameObject, PlayerPartyTypes.MultiTower);
                        bool flag2 = data2.playerIndex == PunMonoSingleton<MyPhoton>.Instance.MyPlayerIndex;
                        component.AllowJobChange = flag2;
                        if (flag2)
                        {
                          UnitData unitDataByUnitId = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUnitID(data4.UnitParam.iname);
                          if (unitDataByUnitId != null)
                          {
                            unitDataByUnitId.TempFlags |= UnitData.TemporaryFlags.AllowJobChange;
                            DataSource.Bind<UnitData>(((Component) gameObjectId).gameObject, unitDataByUnitId);
                            data4 = unitDataByUnitId;
                            if (unitDataList != null && UnitSameGroupParam.IsSameUnitInParty(unitDataList.ToArray(), data4.UnitID) && UnityEngine.Object.op_Inequality((UnityEngine.Object) MonoSingleton<GameManager>.Instance, (UnityEngine.Object) null) && MonoSingleton<GameManager>.Instance.MasterParam != null)
                            {
                              UnitSameGroupParam unitSameGroup = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitSameGroup(data4.UnitID);
                              if (unitSameGroup != null)
                                DataSource.Bind<UnitSameGroupParam>(((Component) gameObjectId).gameObject, unitSameGroup);
                            }
                          }
                        }
                      }
                      if (index5 == 0 && this.PlayerInfoList.Count > index1)
                      {
                        DataSource.Bind<UnitData>(this.PlayerInfoList[index1], data4);
                        break;
                      }
                      break;
                    }
                  }
                }
                else
                {
                  GameObjectID gameObjectId1 = Array.Find<GameObjectID>(componentsInChildren, (Predicate<GameObjectID>) (goId => ("unit" + (object) slotID).Equals(goId.ID)));
                  if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObjectId1, (UnityEngine.Object) null))
                    DataSource.Bind<UnitData>(((Component) gameObjectId1).gameObject, data4);
                  GameObjectID gameObjectId2 = Array.Find<GameObjectID>(componentsInChildren, (Predicate<GameObjectID>) (goId => ("card" + (object) slotID).Equals(goId.ID)));
                  if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObjectId2, (UnityEngine.Object) null))
                  {
                    ConceptCardSlots component = ((Component) gameObjectId2).GetComponent<ConceptCardSlots>();
                    DataSource.Bind<UnitData>(((Component) ((Component) gameObjectId2).transform.parent).gameObject, data4);
                    component.Refresh(false);
                  }
                  GameObjectID gameObjectId3 = Array.Find<GameObjectID>(componentsInChildren, (Predicate<GameObjectID>) (goId => ("arti" + (object) slotID).Equals(goId.ID)));
                  if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObjectId3, (UnityEngine.Object) null))
                  {
                    ArtifactSlots component = ((Component) gameObjectId3).GetComponent<ArtifactSlots>();
                    DataSource.Bind<UnitData>(((Component) ((Component) gameObjectId3).transform.parent).gameObject, data4);
                    component.Refresh(false);
                  }
                }
              }
            }
          }
        }
        ListItemEvents component1 = uiItem.GetComponent<ListItemEvents>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component1, (UnityEngine.Object) null))
        {
          component1.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelectItem);
          component1.OnOpenDetail = new ListItemEvents.ListItemEvent(this.OnOpenItemDetail);
          component1.OnCloseDetail = new ListItemEvents.ListItemEvent(this.OnCloseItemDetail);
        }
        uiItem.transform.SetParent(parent, false);
        uiItem.gameObject.SetActive(true);
      }
      DataSource.Bind<QuestParam>(this.Root, quest);
      bool flag = false;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SkillObj, (UnityEngine.Object) null) && quest != null)
      {
        if (quest.IsMultiLeaderSkill)
        {
          List<MyPhoton.MyPlayer> roomPlayerList2 = instance.GetRoomPlayerList();
          if (roomPlayerList2 != null)
          {
            MyPhoton.MyPlayer myPlayer = roomPlayerList2.Find((Predicate<MyPhoton.MyPlayer>) (member => member.playerID == 1));
            if (myPlayer != null)
            {
              JSON_MyPhotonPlayerParam photonPlayerParam = JSON_MyPhotonPlayerParam.Parse(myPlayer.json);
              if (photonPlayerParam != null && photonPlayerParam.units != null && photonPlayerParam.units.Length > 0)
              {
                UnitData data5 = new UnitData();
                if (data5 != null)
                {
                  data5.Deserialize(photonPlayerParam.units[0].unitJson);
                  DataSource.Bind<UnitData>(this.SkillObj, data5);
                  flag = data5.LeaderSkill != null;
                }
              }
            }
          }
        }
        SRPG_Button component = this.SkillObj.GetComponent<SRPG_Button>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          ((Selectable) component).interactable = quest.IsMultiLeaderSkill && flag;
      }
      GameParameter.UpdateAll(this.Root);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 1);
    }

    private void OnSelectItem(GameObject go)
    {
    }

    private void OnCloseItemDetail(GameObject go)
    {
    }

    public void OnOpenItemDetail(GameObject go)
    {
      JSON_MyPhotonPlayerParam dataOfClass = DataSource.FindDataOfClass<JSON_MyPhotonPlayerParam>(go, (JSON_MyPhotonPlayerParam) null);
      if (dataOfClass == null || dataOfClass.playerID <= 0)
        return;
      MyPhoton.MyRoom currentRoom = PunMonoSingleton<MyPhoton>.Instance.GetCurrentRoom();
      if (currentRoom != null)
      {
        JSON_MyPhotonRoomParam myPhotonRoomParam = JSON_MyPhotonRoomParam.Parse(currentRoom.json);
        if (myPhotonRoomParam != null)
        {
          QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(myPhotonRoomParam.iname);
          if (quest != null && (quest.type == QuestTypes.Multi || quest.type == QuestTypes.MultiGps))
          {
            int index = 0;
            SerializeValueBehaviour component = go.GetComponent<SerializeValueBehaviour>();
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
              index = component.list.GetInt("index", -1);
            if (0 <= index && index < dataOfClass.units.Length)
            {
              UnitData unit = dataOfClass.units[index].unit;
              if (unit != null)
              {
                unit.ShowTooltip(((Component) this).gameObject);
                return;
              }
            }
          }
        }
      }
      GlobalVars.SelectedMultiPlayerParam = dataOfClass;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
    }

    private void Update()
    {
    }

    public void OnEditTeam() => FlowNode_GameObject.ActivateOutputLinks((Component) this, 200);

    public void OnEditTeamMultiTower(int index)
    {
      int[] numArray = new int[3]{ 0, 1, 2 };
      if (index >= 0 && index < numArray.Length)
        GlobalVars.SelectedTowerMultiPartyIndex = numArray[index];
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 200);
    }

    public void OpenLeaderSkillDetail()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SkillObj, (UnityEngine.Object) null) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Prefab_LeaderSkillDetail, (UnityEngine.Object) null))
        return;
      UnitData dataOfClass = DataSource.FindDataOfClass<UnitData>(this.SkillObj, (UnitData) null);
      if (dataOfClass == null)
        return;
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Prefab_LeaderSkillDetail);
      DataSource.Bind<UnitData>(gameObject, dataOfClass);
      SceneRoot componentInParent = ((Component) this).GetComponentInParent<SceneRoot>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInParent, (UnityEngine.Object) null))
        return;
      gameObject.transform.SetParent(((Component) componentInParent).transform, false);
    }

    private void KickMember()
    {
      if (!(FlowNode_ButtonEvent.currentValue is SerializeValueList currentValue))
        return;
      GameObject item = currentValue.GetGameObject("_self");
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) item, (UnityEngine.Object) null))
        return;
      JSON_MyPhotonPlayerParam param = DataSource.FindDataOfClass<JSON_MyPhotonPlayerParam>(item, (JSON_MyPhotonPlayerParam) null);
      if (param == null || param.playerID <= 0)
        return;
      UIUtility.ConfirmBox(string.Format(LocalizedText.Get("sys.MULTI_KICK_CONFIRM"), (object) param.playerName), (UIUtility.DialogResultEvent) (dialog =>
      {
        if (param.isSupportAI != 0)
        {
          this.TargetItem = item;
          this.SetFriend((MultiSupportData) null);
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 312);
        }
        else
          PunMonoSingleton<MyPhoton>.Instance.KickMember(param.playerID);
      }), (UIUtility.DialogResultEvent) null);
    }

    public GameObject TargetItem { get; private set; }

    private void OpenFriendPopup()
    {
      if (!(FlowNode_ButtonEvent.currentValue is SerializeValueList currentValue))
        return;
      GameObject gameObject = currentValue.GetGameObject("_self");
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
        return;
      this.TargetItem = gameObject;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 311);
    }

    public void SetFriend(MultiSupportData multiSupportData)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.TargetItem, (UnityEngine.Object) null))
        return;
      JSON_MyPhotonPlayerParam old = DataSource.FindDataOfClass<JSON_MyPhotonPlayerParam>(this.TargetItem, (JSON_MyPhotonPlayerParam) null);
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      JSON_MyPhotonRoomParam myPhotonRoomParam = JSON_MyPhotonRoomParam.Parse(instance.GetCurrentRoom().json);
      List<JSON_MyPhotonPlayerParam> photonPlayerParamList = new List<JSON_MyPhotonPlayerParam>();
      if (myPhotonRoomParam.support != null)
        photonPlayerParamList.AddRange((IEnumerable<JSON_MyPhotonPlayerParam>) myPhotonRoomParam.support);
      if (old != null && !string.IsNullOrEmpty(old.UID))
      {
        MultiSupportData rs = this.MultiSupportList.Find((Predicate<MultiSupportData>) (s => s.UID == old.UID));
        if (rs == null)
          return;
        photonPlayerParamList.RemoveAll((Predicate<JSON_MyPhotonPlayerParam>) (s => s.UID == rs.UID));
        this.MultiSupportList.Remove(rs);
      }
      if (multiSupportData != null)
      {
        JSON_MyPhotonPlayerParam supportAi = JSON_MyPhotonPlayerParam.CreateSupportAI(multiSupportData, old.playerIndex, this.mSupportAIIndex);
        photonPlayerParamList.Add(supportAi);
        this.MultiSupportList.Add(multiSupportData);
      }
      myPhotonRoomParam.support = photonPlayerParamList.ToArray();
      instance.SetRoomParam(myPhotonRoomParam.Serialize());
      ++this.mSupportAIIndex;
    }
  }
}
