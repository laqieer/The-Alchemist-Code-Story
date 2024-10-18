// Decompiled with JetBrains decompiler
// Type: SRPG.EventShopCoinList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "イベントショップが押下された", FlowNode.PinTypes.Output, 101)]
  public class EventShopCoinList : MonoBehaviour, IFlowInterface
  {
    private List<GameObject> mEventCoinListItems = new List<GameObject>();
    private const int PIN_ID_REFRESH = 1;
    private const int PIN_ID_SHOPBTN = 101;
    [SerializeField]
    private GameObject ItemTemplate;
    [SerializeField]
    private GameObject ArenaTemplate;
    [SerializeField]
    private GameObject MultiTemplate;
    [SerializeField]
    private ListExtras ScrollView;

    private void ActivateOutputLinks(int pinID)
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, pinID);
    }

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      this.OnRefresh();
    }

    private void Awake()
    {
      GlobalVars.SelectionEventShop = (EventShopListItem) null;
      GlobalVars.SelectionCoinListType = GlobalVars.CoinListSelectionType.None;
      if ((UnityEngine.Object) this.ItemTemplate != (UnityEngine.Object) null && this.ItemTemplate.activeInHierarchy)
        this.ItemTemplate.SetActive(false);
      if ((UnityEngine.Object) this.ArenaTemplate != (UnityEngine.Object) null && this.ArenaTemplate.activeInHierarchy)
        this.ArenaTemplate.SetActive(false);
      if (!((UnityEngine.Object) this.MultiTemplate != (UnityEngine.Object) null) || !this.MultiTemplate.activeInHierarchy)
        return;
      this.MultiTemplate.SetActive(false);
    }

    private GameObject CreateListItem(EventCoinData eventcoin_data)
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ItemTemplate);
      EventCoinListItem component1 = gameObject.GetComponent<EventCoinListItem>();
      Button component2 = component1.Button.GetComponent<Button>();
      ListItemEvents component3 = component1.Button.GetComponent<ListItemEvents>();
      if ((UnityEngine.Object) component2 != (UnityEngine.Object) null && (UnityEngine.Object) component3 != (UnityEngine.Object) null)
      {
        EventShopListItem eventShopListItem = GlobalVars.EventShopListItems.Find((Predicate<EventShopListItem>) (f => f.EventShopInfo.shop_cost_iname.Equals(eventcoin_data.iname)));
        bool flag = false;
        if ((UnityEngine.Object) eventShopListItem != (UnityEngine.Object) null && eventShopListItem.EventShopInfo.shops != null && eventShopListItem.EventShopInfo.shops.unlock != null && (eventShopListItem.EventShopInfo.shops.unlock.flg != 1 ? 0 : 1) != 0)
          flag = true;
        if (flag)
        {
          component3.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelect);
          component2.interactable = true;
        }
        else
          component2.interactable = false;
      }
      return gameObject;
    }

    private GameObject CreateOtherListItem(GameObject template, ListItemEvents.ListItemEvent func, bool is_button_enable)
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(template);
      EventCoinListItem component1 = gameObject.GetComponent<EventCoinListItem>();
      Button component2 = component1.Button.GetComponent<Button>();
      ListItemEvents component3 = component1.Button.GetComponent<ListItemEvents>();
      if ((UnityEngine.Object) component3 != (UnityEngine.Object) null && is_button_enable)
      {
        component3.OnSelect = func;
        component2.interactable = true;
      }
      else
        component2.interactable = false;
      return gameObject;
    }

    private void UpdateItems()
    {
      MonoSingleton<GameManager>.Instance.Player.UpdateEventCoin();
      if ((UnityEngine.Object) this.ItemTemplate == (UnityEngine.Object) null)
        return;
      List<EventCoinData> eventCoinList = MonoSingleton<GameManager>.Instance.Player.EventCoinList;
      Transform transform = this.transform;
      for (int index = 0; index < eventCoinList.Count; ++index)
      {
        GameObject listItem = this.CreateListItem(eventCoinList[index]);
        listItem.transform.SetParent(transform, false);
        this.mEventCoinListItems.Add(listItem);
        listItem.SetActive(true);
        EventCoinData data = eventCoinList[index];
        DataSource.Bind<EventCoinData>(listItem, data, false);
      }
      if ((UnityEngine.Object) this.ArenaTemplate != (UnityEngine.Object) null)
      {
        GameObject otherListItem = this.CreateOtherListItem(this.ArenaTemplate, new ListItemEvents.ListItemEvent(this.OnSelectArenaShop), MonoSingleton<GameManager>.Instance.Player.CheckUnlock(UnlockTargets.Arena));
        otherListItem.transform.SetParent(transform, false);
        this.mEventCoinListItems.Add(otherListItem);
        otherListItem.SetActive(true);
      }
      if ((UnityEngine.Object) this.MultiTemplate != (UnityEngine.Object) null)
      {
        GameObject otherListItem = this.CreateOtherListItem(this.MultiTemplate, new ListItemEvents.ListItemEvent(this.OnSelectMultiShop), true);
        otherListItem.transform.SetParent(transform, false);
        this.mEventCoinListItems.Add(otherListItem);
        otherListItem.SetActive(true);
      }
      GameParameter.UpdateAll(this.gameObject);
    }

    private void OnRefresh()
    {
      this.UpdateItems();
    }

    private void OnSelect(GameObject go)
    {
      EventCoinData coindata = DataSource.FindDataOfClass<EventCoinData>(go, (EventCoinData) null);
      if (coindata.iname == null)
        return;
      GlobalVars.SelectionCoinListType = GlobalVars.CoinListSelectionType.EventShop;
      GlobalVars.SelectionEventShop = GlobalVars.EventShopListItems.Find((Predicate<EventShopListItem>) (f => f.EventShopInfo.shop_cost_iname.Equals(coindata.iname)));
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
    }

    private void OnSelectArenaShop(GameObject go)
    {
      GlobalVars.SelectionCoinListType = GlobalVars.CoinListSelectionType.ArenaShop;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
    }

    private void OnSelectMultiShop(GameObject go)
    {
      GlobalVars.SelectionCoinListType = GlobalVars.CoinListSelectionType.MultiShop;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
    }
  }
}
