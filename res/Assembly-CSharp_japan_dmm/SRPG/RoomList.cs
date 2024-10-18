// Decompiled with JetBrains decompiler
// Type: SRPG.RoomList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [AddComponentMenu("Multi/参加募集中の部屋一覧")]
  [FlowNode.Pin(0, "ノーマル表示", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "イベント表示", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "現状表示", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(3, "選択可能ものを表示", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(101, "選択された", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(200, "選択可能な部屋がない", FlowNode.PinTypes.Output, 200)]
  [FlowNode.Pin(201, "選択可能な部屋がある", FlowNode.PinTypes.Output, 201)]
  public class RoomList : MonoBehaviour, IFlowInterface
  {
    private const int PININ_DRAW_NORMAL = 0;
    private const int PININ_DRAW_EVENT = 1;
    private const int PININ_DRAW_CURRENT = 2;
    private const int PININ_DRAW_SELECTABLE = 3;
    private const int PINOUT_SELECTED = 101;
    private const int PINOUT_ROOM_EXISTS = 200;
    private const int PINOUT_ROOM_NOT_EXISTS = 201;
    private readonly Color EnableColor = new Color(1f, 1f, 1f);
    private readonly Color DisableColor = new Color(0.392156869f, 0.392156869f, 0.392156869f);
    [Description("リストアイテムとして使用するゲームオブジェクト")]
    public GameObject ItemTemplate;
    [Description("詳細画面として使用するゲームオブジェクト")]
    public GameObject DetailTemplate;
    private GameObject mDetailInfo;
    public ScrollRect ScrollRect;
    private List<GameObject> mRoomList = new List<GameObject>();

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 0:
          this.Refresh(false);
          break;
        case 1:
          this.Refresh(true);
          break;
        case 2:
          this.Refresh(GlobalVars.SelectedMultiPlayQuestIsEvent);
          break;
        case 3:
          this.Refresh(false, true);
          break;
      }
    }

    private void Awake()
    {
      GlobalVars.SelectedMultiPlayQuestIsEvent = false;
      ((Behaviour) this).enabled = true;
      if (Object.op_Inequality((Object) this.ItemTemplate, (Object) null) && this.ItemTemplate.activeInHierarchy)
        this.ItemTemplate.SetActive(false);
      if (!Object.op_Inequality((Object) this.DetailTemplate, (Object) null) || !this.DetailTemplate.activeInHierarchy)
        return;
      this.DetailTemplate.SetActive(false);
    }

    private void Start()
    {
    }

    public void Refresh(bool isEvent, bool isSelect = false)
    {
      GlobalVars.SelectedMultiPlayQuestIsEvent = isEvent;
      this.RefreshItems(isSelect);
      if (!Object.op_Inequality((Object) this.ScrollRect, (Object) null))
        return;
      ListExtras component = ((Component) this.ScrollRect).GetComponent<ListExtras>();
      if (Object.op_Inequality((Object) component, (Object) null))
        component.SetScrollPos(1f);
      else
        this.ScrollRect.normalizedPosition = Vector2.one;
    }

    private bool IsChooseRoom(MultiPlayAPIRoom room)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      PlayerData player = instance.Player;
      PartyData party = player.Partys[2];
      QuestParam quest = instance.FindQuest(room.quest.iname);
      bool flag = false;
      if (room.limit == 0)
        return true;
      if (party != null)
      {
        flag = true;
        for (int index = 0; index < (int) quest.unitNum; ++index)
        {
          long unitUniqueId = party.GetUnitUniqueID(index);
          if (unitUniqueId <= 0L)
          {
            flag = false;
            break;
          }
          UnitData unitDataByUniqueId = player.FindUnitDataByUniqueID(unitUniqueId);
          if (unitDataByUniqueId == null)
          {
            flag = false;
            break;
          }
          flag &= unitDataByUniqueId.CalcLevel() >= room.unitlv;
        }
      }
      return flag;
    }

    private void RefreshItems(bool isSelect)
    {
      Transform transform = ((Component) this).transform;
      int num = 0;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      for (int index = 0; index < this.mRoomList.Count; ++index)
        Object.DestroyImmediate((Object) this.mRoomList[index]);
      this.mRoomList.Clear();
      if (Object.op_Equality((Object) this.ItemTemplate, (Object) null))
        return;
      MultiPlayAPIRoom[] rooms = FlowNode_MultiPlayAPI.RoomList != null ? FlowNode_MultiPlayAPI.RoomList.rooms : (MultiPlayAPIRoom[]) null;
      if (rooms == null)
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 200);
      else if (rooms.Length == 0)
      {
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 200);
      }
      else
      {
        RoomList.MultiTowerFilterMode multiTowerFilterMode = RoomList.MultiTowerFilterMode.Default;
        string s = FlowNode_Variable.Get("MT_ROOM_FILTER_MODE");
        if (!string.IsNullOrEmpty(s))
          multiTowerFilterMode = (RoomList.MultiTowerFilterMode) int.Parse(s);
        for (int index = 0; index < rooms.Length; ++index)
        {
          MultiPlayAPIRoom multiPlayApiRoom = rooms[index];
          if (multiPlayApiRoom != null && multiPlayApiRoom.quest != null && !string.IsNullOrEmpty(multiPlayApiRoom.quest.iname))
          {
            QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(multiPlayApiRoom.quest.iname);
            if (quest != null && quest.IsMultiEvent == GlobalVars.SelectedMultiPlayQuestIsEvent)
            {
              switch (multiTowerFilterMode)
              {
                case RoomList.MultiTowerFilterMode.Cleared:
                  if (multiPlayApiRoom.is_clear != 0)
                    break;
                  continue;
                case RoomList.MultiTowerFilterMode.NotClear:
                  if (multiPlayApiRoom.is_clear == 0)
                    break;
                  continue;
              }
              bool isChoose = this.IsChooseRoom(multiPlayApiRoom);
              if ((isChoose || !isSelect) && (multiPlayApiRoom.clear == 0 || instance.FindQuest(multiPlayApiRoom.quest.iname).state == QuestStates.Cleared))
              {
                GameObject gameObject = Object.Instantiate<GameObject>(this.ItemTemplate);
                ((Object) gameObject).hideFlags = (HideFlags) 52;
                Json_Unit[] units = multiPlayApiRoom.owner != null ? multiPlayApiRoom.owner.units : (Json_Unit[]) null;
                if (units != null && units.Length > 0)
                {
                  UnitData data = new UnitData();
                  data.Deserialize(units[0]);
                  DataSource.Bind<UnitData>(gameObject, data);
                }
                DataSource.Bind<MultiPlayAPIRoom>(gameObject, multiPlayApiRoom);
                DataSource.Bind<QuestParam>(gameObject, quest);
                DebugUtility.Log("found roomid:" + (object) multiPlayApiRoom.roomid + " room:" + multiPlayApiRoom.comment + " iname:" + quest.iname + " playerNum:" + (object) quest.playerNum + " unitNum:" + (object) quest.unitNum);
                ListItemEvents component = gameObject.GetComponent<ListItemEvents>();
                if (Object.op_Inequality((Object) component, (Object) null))
                {
                  component.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelectItem);
                  component.OnOpenDetail = new ListItemEvents.ListItemEvent(this.OnOpenItemDetail);
                  component.OnCloseDetail = new ListItemEvents.ListItemEvent(this.OnCloseItemDetail);
                }
                gameObject.transform.SetParent(transform, false);
                gameObject.gameObject.SetActive(true);
                this.mRoomList.Add(gameObject);
                this.SetSelectablParam(gameObject, multiPlayApiRoom, isChoose);
                ++num;
              }
            }
          }
        }
        GameParameter.UpdateAll(((Component) transform).gameObject);
        FlowNode_GameObject.ActivateOutputLinks((Component) this, num != 0 ? 201 : 200);
      }
    }

    private void SetSelectablParam(GameObject obj, MultiPlayAPIRoom room, bool isChoose)
    {
      SRPG_Button component1 = obj.GetComponent<SRPG_Button>();
      Transform transform1 = obj.transform.Find("fil");
      Transform transform2 = obj.transform.Find("basewindow");
      if (isChoose)
      {
        if (Object.op_Inequality((Object) component1, (Object) null))
          ((Selectable) component1).interactable = true;
        if (Object.op_Inequality((Object) transform1, (Object) null))
          ((Component) transform1).gameObject.SetActive(false);
        if (!Object.op_Inequality((Object) transform2, (Object) null))
          return;
        Image component2 = ((Component) transform2).GetComponent<Image>();
        if (!Object.op_Inequality((Object) component2, (Object) null))
          return;
        ((Graphic) component2).color = this.EnableColor;
      }
      else
      {
        if (Object.op_Inequality((Object) component1, (Object) null))
          ((Selectable) component1).interactable = false;
        if (Object.op_Inequality((Object) transform1, (Object) null))
          ((Component) transform1).gameObject.SetActive(true);
        if (!Object.op_Inequality((Object) transform2, (Object) null))
          return;
        Image component3 = ((Component) transform2).GetComponent<Image>();
        if (!Object.op_Inequality((Object) component3, (Object) null))
          return;
        ((Graphic) component3).color = this.DisableColor;
      }
    }

    private void OnSelectItem(GameObject go)
    {
      MultiPlayAPIRoom dataOfClass = DataSource.FindDataOfClass<MultiPlayAPIRoom>(go, (MultiPlayAPIRoom) null);
      if (dataOfClass == null)
        return;
      GlobalVars.SelectedMultiPlayRoomID = dataOfClass.roomid;
      GlobalVars.SelectedMultiPlayRoomPassCodeHash = dataOfClass.pwd_hash;
      GlobalVars.SelectedMultiTowerFloor = dataOfClass.floor;
      DebugUtility.Log("Select RoomID:" + (object) GlobalVars.SelectedMultiPlayRoomID + " PassCodeHash:" + GlobalVars.SelectedMultiPlayRoomPassCodeHash);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
      Transform transform1 = go.transform.Find("cursor");
      if (!Object.op_Inequality((Object) transform1, (Object) null))
        return;
      for (int index = 0; index < this.mRoomList.Count; ++index)
      {
        Transform transform2 = this.mRoomList[index].transform.Find("cursor");
        if (Object.op_Inequality((Object) transform2, (Object) null))
          ((Component) transform2).gameObject.SetActive(false);
      }
      ((Component) transform1).gameObject.SetActive(true);
    }

    private void OnCloseItemDetail(GameObject go)
    {
      if (!Object.op_Inequality((Object) this.mDetailInfo, (Object) null))
        return;
      Object.DestroyImmediate((Object) this.mDetailInfo.gameObject);
      this.mDetailInfo = (GameObject) null;
    }

    private void OnOpenItemDetail(GameObject go)
    {
      QuestParam dataOfClass = DataSource.FindDataOfClass<QuestParam>(go, (QuestParam) null);
      if (!Object.op_Equality((Object) this.mDetailInfo, (Object) null) || dataOfClass == null)
        return;
      this.mDetailInfo = Object.Instantiate<GameObject>(this.DetailTemplate);
      DataSource.Bind<QuestParam>(this.mDetailInfo, dataOfClass);
      this.mDetailInfo.SetActive(true);
    }

    private void Update()
    {
    }

    private enum MultiTowerFilterMode
    {
      Default,
      Cleared,
      NotClear,
    }
  }
}
