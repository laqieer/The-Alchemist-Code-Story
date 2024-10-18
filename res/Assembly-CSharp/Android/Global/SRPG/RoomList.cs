// Decompiled with JetBrains decompiler
// Type: SRPG.RoomList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(101, "選択された", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(201, "選択可能な部屋がある", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(200, "選択可能な部屋がない", FlowNode.PinTypes.Output, 0)]
  [AddComponentMenu("Multi/参加募集中の部屋一覧")]
  [FlowNode.Pin(3, "選択可能ものを表示", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(0, "ノーマル表示", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "イベント表示", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(2, "現状表示", FlowNode.PinTypes.Input, 0)]
  public class RoomList : MonoBehaviour, IFlowInterface
  {
    private readonly Color EnableColor = new Color(1f, 1f, 1f);
    private readonly Color DisableColor = new Color(0.3921569f, 0.3921569f, 0.3921569f);
    [Description("リストアイテムとして使用するゲームオブジェクト")]
    public GameObject ItemTemplate;
    [Description("詳細画面として使用するゲームオブジェクト")]
    public GameObject DetailTemplate;
    private GameObject mDetailInfo;
    public ScrollRect ScrollRect;

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 0:
          this.Refresh(false, false);
          break;
        case 1:
          this.Refresh(true, false);
          break;
        case 2:
          this.Refresh(GlobalVars.SelectedMultiPlayQuestIsEvent, false);
          break;
        case 3:
          this.Refresh(false, true);
          break;
      }
    }

    private void Awake()
    {
      GlobalVars.SelectedMultiPlayQuestIsEvent = false;
      this.enabled = true;
      if ((UnityEngine.Object) this.ItemTemplate != (UnityEngine.Object) null && this.ItemTemplate.activeInHierarchy)
        this.ItemTemplate.SetActive(false);
      if (!((UnityEngine.Object) this.DetailTemplate != (UnityEngine.Object) null) || !this.DetailTemplate.activeInHierarchy)
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
      if (!((UnityEngine.Object) this.ScrollRect != (UnityEngine.Object) null))
        return;
      ListExtras component = this.ScrollRect.GetComponent<ListExtras>();
      if ((UnityEngine.Object) component != (UnityEngine.Object) null)
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
      Transform transform = this.transform;
      int num = 0;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      for (int index = transform.childCount - 1; index >= 0; --index)
      {
        Transform child = transform.GetChild(index);
        if (!((UnityEngine.Object) child == (UnityEngine.Object) null) && child.gameObject.activeSelf)
          UnityEngine.Object.DestroyImmediate((UnityEngine.Object) child.gameObject);
      }
      if ((UnityEngine.Object) this.ItemTemplate == (UnityEngine.Object) null)
        return;
      MultiPlayAPIRoom[] multiPlayApiRoomArray = FlowNode_MultiPlayAPI.RoomList != null ? FlowNode_MultiPlayAPI.RoomList.rooms : (MultiPlayAPIRoom[]) null;
      if (multiPlayApiRoomArray == null)
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 200);
      else if (multiPlayApiRoomArray.Length == 0)
      {
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 200);
      }
      else
      {
        for (int index = 0; index < multiPlayApiRoomArray.Length; ++index)
        {
          MultiPlayAPIRoom multiPlayApiRoom = multiPlayApiRoomArray[index];
          if (multiPlayApiRoom != null && multiPlayApiRoom.quest != null && !string.IsNullOrEmpty(multiPlayApiRoom.quest.iname))
          {
            QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(multiPlayApiRoom.quest.iname);
            if (quest != null && quest.IsMultiEvent == GlobalVars.SelectedMultiPlayQuestIsEvent)
            {
              bool isChoose = this.IsChooseRoom(multiPlayApiRoom);
              if ((isChoose || !isSelect) && (multiPlayApiRoom.clear == 0 || instance.FindQuest(multiPlayApiRoom.quest.iname).state == QuestStates.Cleared))
              {
                GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ItemTemplate);
                gameObject.hideFlags = HideFlags.DontSave;
                Json_Unit[] jsonUnitArray = multiPlayApiRoom.owner != null ? multiPlayApiRoom.owner.units : (Json_Unit[]) null;
                if (jsonUnitArray != null && jsonUnitArray.Length > 0)
                {
                  UnitData data = new UnitData();
                  data.Deserialize(jsonUnitArray[0]);
                  DataSource.Bind<UnitData>(gameObject, data);
                }
                DataSource.Bind<MultiPlayAPIRoom>(gameObject, multiPlayApiRoom);
                DataSource.Bind<QuestParam>(gameObject, quest);
                DebugUtility.Log("found roomid:" + (object) multiPlayApiRoom.roomid + " room:" + multiPlayApiRoom.comment + " iname:" + quest.iname + " playerNum:" + (object) quest.playerNum + " unitNum:" + (object) quest.unitNum);
                ListItemEvents component = gameObject.GetComponent<ListItemEvents>();
                if ((UnityEngine.Object) component != (UnityEngine.Object) null)
                {
                  component.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelectItem);
                  component.OnOpenDetail = new ListItemEvents.ListItemEvent(this.OnOpenItemDetail);
                  component.OnCloseDetail = new ListItemEvents.ListItemEvent(this.OnCloseItemDetail);
                }
                gameObject.transform.SetParent(transform, false);
                gameObject.gameObject.SetActive(true);
                this.SetSelectablParam(gameObject, multiPlayApiRoom, isChoose);
                ++num;
              }
            }
          }
        }
        GameParameter.UpdateAll(transform.gameObject);
        FlowNode_GameObject.ActivateOutputLinks((Component) this, num != 0 ? 201 : 200);
      }
    }

    private void SetSelectablParam(GameObject obj, MultiPlayAPIRoom room, bool isChoose)
    {
      SRPG_Button component1 = obj.GetComponent<SRPG_Button>();
      Transform child1 = obj.transform.FindChild("fil");
      Transform child2 = obj.transform.FindChild("basewindow");
      if (isChoose)
      {
        if ((UnityEngine.Object) component1 != (UnityEngine.Object) null)
          component1.interactable = true;
        if ((UnityEngine.Object) child1 != (UnityEngine.Object) null)
          child1.gameObject.SetActive(false);
        if (!((UnityEngine.Object) child2 != (UnityEngine.Object) null))
          return;
        Image component2 = child2.GetComponent<Image>();
        if (!((UnityEngine.Object) component2 != (UnityEngine.Object) null))
          return;
        component2.color = this.EnableColor;
      }
      else
      {
        if ((UnityEngine.Object) component1 != (UnityEngine.Object) null)
          component1.interactable = false;
        if ((UnityEngine.Object) child1 != (UnityEngine.Object) null)
          child1.gameObject.SetActive(true);
        if (!((UnityEngine.Object) child2 != (UnityEngine.Object) null))
          return;
        Image component2 = child2.GetComponent<Image>();
        if (!((UnityEngine.Object) component2 != (UnityEngine.Object) null))
          return;
        component2.color = this.DisableColor;
      }
    }

    private void OnSelectItem(GameObject go)
    {
      MultiPlayAPIRoom dataOfClass = DataSource.FindDataOfClass<MultiPlayAPIRoom>(go, (MultiPlayAPIRoom) null);
      if (dataOfClass == null)
        return;
      GlobalVars.SelectedMultiPlayRoomID = dataOfClass.roomid;
      GlobalVars.SelectedMultiPlayRoomPassCodeHash = dataOfClass.pwd_hash;
      DebugUtility.Log("Select RoomID:" + (object) GlobalVars.SelectedMultiPlayRoomID + " PassCodeHash:" + GlobalVars.SelectedMultiPlayRoomPassCodeHash);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
    }

    private void OnCloseItemDetail(GameObject go)
    {
      if (!((UnityEngine.Object) this.mDetailInfo != (UnityEngine.Object) null))
        return;
      UnityEngine.Object.DestroyImmediate((UnityEngine.Object) this.mDetailInfo.gameObject);
      this.mDetailInfo = (GameObject) null;
    }

    private void OnOpenItemDetail(GameObject go)
    {
      QuestParam dataOfClass = DataSource.FindDataOfClass<QuestParam>(go, (QuestParam) null);
      if (!((UnityEngine.Object) this.mDetailInfo == (UnityEngine.Object) null) || dataOfClass == null)
        return;
      this.mDetailInfo = UnityEngine.Object.Instantiate<GameObject>(this.DetailTemplate);
      DataSource.Bind<QuestParam>(this.mDetailInfo, dataOfClass);
      this.mDetailInfo.SetActive(true);
    }

    private void Update()
    {
    }
  }
}
