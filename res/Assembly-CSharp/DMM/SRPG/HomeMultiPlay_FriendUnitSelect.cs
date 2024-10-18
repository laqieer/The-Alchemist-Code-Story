// Decompiled with JetBrains decompiler
// Type: SRPG.HomeMultiPlay_FriendUnitSelect
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
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "List Show", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(3, "List Hide", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(4, "Chat Enable", FlowNode.PinTypes.Input, 4)]
  [FlowNode.Pin(5, "Chat Disable", FlowNode.PinTypes.Input, 5)]
  [FlowNode.Pin(10, "Select", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(11, "Selected", FlowNode.PinTypes.Output, 11)]
  public class HomeMultiPlay_FriendUnitSelect : MonoBehaviour, IFlowInterface
  {
    private const int PIN_IN_REFRESH_LIST = 1;
    private const int PIN_IN_LIST_SHOW = 2;
    private const int PIN_IN_LIST_HIDE = 3;
    private const int PIN_IN_CHAT_ENABLE = 4;
    private const int PIN_IN_CHAT_DISABLE = 5;
    private const int PIN_IN_SELECT_SUPPORT = 10;
    private const int PIN_OUT_SELECTED_SUPPORT = 11;
    private const float SUPPORT_REFRESH_LOCK_TIME = 10f;
    private const string SVB_KEY_EMPTY = "empty";
    private const string SVB_KEY_BODY = "body";
    private const string SVB_KEY_UNIT_1 = "unit1";
    private const string SVB_KEY_UNIT_2 = "unit2";
    private const string SVB_KEY_IS_FRIEND = "isFriend";
    private const string SVB_KEY_COST = "cost";
    [SerializeField]
    private GameObject ListSingle;
    [SerializeField]
    private GameObject ListPair;
    [SerializeField]
    private GameObject ListItemTemplateSingle;
    [SerializeField]
    private GameObject ListItemTemplatePair;
    [SerializeField]
    private SRPG_Button RefreshButton;
    private List<MultiSupportData> SupportList = new List<MultiSupportData>();
    private List<GameObject> ListItems = new List<GameObject>();
    private float SupportRefreshTime;

    public static HomeMultiPlay_FriendUnitSelect Instance { get; private set; }

    private void Awake() => HomeMultiPlay_FriendUnitSelect.Instance = this;

    private void OnDestroy()
    {
      HomeMultiPlay_FriendUnitSelect.Instance = (HomeMultiPlay_FriendUnitSelect) null;
    }

    private void Update()
    {
      if ((double) this.SupportRefreshTime < 10.0)
      {
        this.SupportRefreshTime += Time.deltaTime;
        ((Selectable) this.RefreshButton).interactable = false;
      }
      else
        ((Selectable) this.RefreshButton).interactable = true;
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 1:
          this.RefreshList();
          break;
        case 2:
          GameUtility.SetGameObjectActive(this.ListSingle, true);
          GameUtility.SetGameObjectActive(this.ListPair, true);
          break;
        case 3:
          GameUtility.SetGameObjectActive(this.ListSingle, false);
          GameUtility.SetGameObjectActive(this.ListPair, false);
          break;
        case 4:
          this.ChatEnable(true);
          break;
        case 5:
          this.ChatEnable(false);
          break;
        case 10:
          this.SelectSupport();
          break;
      }
    }

    private void Start()
    {
      GameUtility.SetGameObjectActive(this.ListSingle, false);
      GameUtility.SetGameObjectActive(this.ListPair, false);
      GameUtility.SetGameObjectActive(this.ListItemTemplateSingle, false);
      GameUtility.SetGameObjectActive(this.ListItemTemplatePair, false);
    }

    public void SetupSupportList(JSON_MultiSupport[] supports)
    {
      if (supports == null)
        return;
      this.SupportList.Clear();
      for (int index = 0; index < supports.Length; ++index)
      {
        MultiSupportData multiSupportData = new MultiSupportData();
        if (multiSupportData.Deserialize(supports[index]))
          this.SupportList.Add(multiSupportData);
      }
      this.SupportRefreshTime = 0.0f;
    }

    private void RefreshList()
    {
      MyPhoton.MyRoom room = PunMonoSingleton<MyPhoton>.Instance.GetCurrentRoom();
      if (room == null || room.param == null || string.IsNullOrEmpty(room.param.iname))
        return;
      QuestParam questParam = Array.Find<QuestParam>(MonoSingleton<GameManager>.Instance.Quests, (Predicate<QuestParam>) (q => q.iname == room.param.iname));
      if (questParam == null)
        return;
      GameObject gameObject1 = this.ListSingle;
      GameObject listItem = this.ListItemTemplateSingle;
      if ((int) questParam.unitNum == 2)
      {
        gameObject1 = this.ListPair;
        listItem = this.ListItemTemplatePair;
      }
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) gameObject1, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) listItem, (UnityEngine.Object) null))
        return;
      GameUtility.SetGameObjectActive(gameObject1, true);
      GameUtility.SetGameObjectActive(listItem, false);
      this.ListItems.ForEach((Action<GameObject>) (item => UnityEngine.Object.Destroy((UnityEngine.Object) item)));
      this.ListItems.Clear();
      this.SupportList.ForEach((Action<MultiSupportData>) (supportData =>
      {
        Json_Friend json = new Json_Friend();
        json.name = supportData.Name;
        json.lv = supportData.Lv;
        FriendData data = new FriendData();
        data.Deserialize(json);
        GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(listItem, listItem.transform.parent);
        SerializeValueBehaviour component1 = gameObject2.GetComponent<SerializeValueBehaviour>();
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) component1, (UnityEngine.Object) null) || component1.list == null)
          return;
        DataSource.Bind<MultiSupportData>(gameObject2, supportData);
        DataSource.Bind<FriendData>(gameObject2, data);
        GameObject gameObject3 = component1.list.GetGameObject("unit1", (GameObject) null);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject3, (UnityEngine.Object) null))
        {
          UnitData unit = supportData.Units.Count >= 1 ? supportData.Units[0] : (UnitData) null;
          DataSource.Bind<UnitData>(gameObject3, unit);
        }
        GameObject gameObject4 = component1.list.GetGameObject("unit2", (GameObject) null);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject4, (UnityEngine.Object) null))
        {
          UnitData unit = supportData.Units.Count >= 2 ? supportData.Units[1] : (UnitData) null;
          DataSource.Bind<UnitData>(gameObject4, unit);
        }
        GameUtility.SetGameObjectActive(component1.list.GetGameObject("isFriend", (GameObject) null), supportData.IsFriend);
        GameObject gameObject5 = component1.list.GetGameObject("cost", (GameObject) null);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject5, (UnityEngine.Object) null))
        {
          CurrencyBitmapText component2 = gameObject5.GetComponent<CurrencyBitmapText>();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component2, (UnityEngine.Object) null))
            ((Text) component2).text = supportData.Cost.ToString();
        }
        gameObject2.SetActive(true);
        this.ListItems.Add(gameObject2);
      }));
    }

    private void SelectSupport()
    {
      if (!(FlowNode_ButtonEvent.currentValue is SerializeValueList currentValue))
        return;
      GameObject gameObject = currentValue.GetGameObject("_self");
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
        return;
      int currentTotalCost = 0;
      RoomPlayerList.Instance.MultiSupportList.ForEach((Action<MultiSupportData>) (ms => currentTotalCost += ms.Cost));
      MultiSupportData dataOfClass = DataSource.FindDataOfClass<MultiSupportData>(gameObject, (MultiSupportData) null);
      if (dataOfClass != null && UnityEngine.Object.op_Inequality((UnityEngine.Object) MonoSingleton<GameManager>.Instance, (UnityEngine.Object) null) && MonoSingleton<GameManager>.Instance.Player != null && MonoSingleton<GameManager>.Instance.Player.Gold < currentTotalCost + dataOfClass.Cost)
      {
        UIUtility.SystemMessage(LocalizedText.Get("sys.GOLD_NOT_ENOUGH"), (UIUtility.DialogResultEvent) null, systemModal: true);
      }
      else
      {
        RoomPlayerList.Instance.SetFriend(dataOfClass);
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 11);
      }
    }

    private void ChatEnable(bool enable)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) MonoSingleton<ChatWindow>.Instance, (UnityEngine.Object) null))
        return;
      MonoSingleton<ChatWindow>.Instance.SetActiveOpenCloseButton(enable);
    }
  }
}
