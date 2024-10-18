// Decompiled with JetBrains decompiler
// Type: SRPG.TowerQuestList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(0, "更新", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "フロア選択", FlowNode.PinTypes.Output, 100)]
  public class TowerQuestList : MonoBehaviour, IFlowInterface, ScrollListSetUp
  {
    [SerializeField]
    private TowerQuestInfo info;
    [SerializeField]
    private TowerScrollListController mScrollListController;
    [SerializeField]
    private ListItemEvents mListItemTemplate;
    [SerializeField]
    private Button mChallenge;
    private List<TowerFloorParam> mFloorParams;
    private bool isInitialized;

    public void Activated(int pinID)
    {
      if (pinID != 0)
        return;
      if (this.isInitialized)
        this.Refresh();
      else
        this.Initialize();
    }

    public void ScrollToCurrentFloor()
    {
      this.mScrollListController.SetScrollTo((float) ((double) this.mScrollListController.ItemScaleMargin * (double) this.mFloorParams.IndexOf(MonoSingleton<GameManager>.Instance.TowerResuponse.GetCurrentFloor()) - (double) this.mScrollListController.ItemScaleMargin * 2.0));
    }

    public void ScrollToCurrentFloor(TowerFloorParam floorParam)
    {
      this.mScrollListController.SetAnchoredPosition((float) ((double) this.mScrollListController.ItemScaleMargin * (double) this.mFloorParams.IndexOf(floorParam) - (double) this.mScrollListController.ItemScaleMargin * 2.0));
    }

    private void Initialize()
    {
      this.isInitialized = true;
      this.mFloorParams = MonoSingleton<GameManager>.Instance.FindTowerFloors(GlobalVars.SelectedTowerID);
      this.mListItemTemplate.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelectItem);
      // ISSUE: method pointer
      this.mScrollListController.OnListItemFocus.AddListener(new UnityAction<GameObject>((object) this, __methodptr(OnScrollStop)));
      this.mScrollListController.UpdateList();
      if (MonoSingleton<GameManager>.Instance.TowerResuponse != null)
      {
        TowerFloorParam currentFloor = MonoSingleton<GameManager>.Instance.TowerResuponse.GetCurrentFloor();
        if (currentFloor == null)
          return;
        this.ScrollToCurrentFloor(currentFloor);
        GlobalVars.SelectedQuestID = currentFloor.iname;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
      }
      else
      {
        TowerFloorParam firstTowerFloor = MonoSingleton<GameManager>.Instance.FindFirstTowerFloor(GlobalVars.SelectedTowerID);
        if (firstTowerFloor == null)
          return;
        this.ScrollToCurrentFloor(firstTowerFloor);
        GlobalVars.SelectedQuestID = firstTowerFloor.iname;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
      }
    }

    private void Refresh()
    {
      this.mFloorParams = MonoSingleton<GameManager>.Instance.FindTowerFloors(GlobalVars.SelectedTowerID);
      this.mScrollListController.UpdateList();
      this.ScrollToCurrentFloor();
    }

    public void OnSetUpItems()
    {
      if (this.mFloorParams == null)
        this.mFloorParams = MonoSingleton<GameManager>.Instance.FindTowerFloors(GlobalVars.SelectedTowerID);
      // ISSUE: method pointer
      this.mScrollListController.OnItemUpdate.AddListener(new UnityAction<int, GameObject>((object) this, __methodptr(OnUpdateItems)));
      ((Component) this).GetComponentInParent<ScrollRect>().movementType = (ScrollRect.MovementType) 2;
      RectTransform component = ((Component) this).GetComponent<RectTransform>();
      Vector2 sizeDelta = component.sizeDelta;
      sizeDelta.y = this.mScrollListController.ItemScaleMargin * (float) this.mFloorParams.Count;
      component.sizeDelta = sizeDelta;
    }

    public void OnUpdateItems(int idx, GameObject obj)
    {
      if (this.mFloorParams == null)
        this.mFloorParams = MonoSingleton<GameManager>.Instance.FindTowerFloors(GlobalVars.SelectedTowerID);
      if (idx < 0)
        obj.SetActive(false);
      else if (idx >= this.mFloorParams.Count)
      {
        DataSource.Bind<TowerFloorParam>(obj, (TowerFloorParam) null);
        obj.SetActive(true);
        TowerQuestListItem component = obj.GetComponent<TowerQuestListItem>();
        if (!Object.op_Inequality((Object) component, (Object) null))
          return;
        component.UpdateParam((TowerFloorParam) null, 0);
      }
      else
      {
        obj.SetActive(true);
        DataSource.Bind<TowerFloorParam>(obj, this.mFloorParams[idx]);
        TowerQuestListItem component1 = obj.GetComponent<TowerQuestListItem>();
        if (Object.op_Inequality((Object) component1, (Object) null))
          component1.UpdateParam(this.mFloorParams[idx], (int) this.mFloorParams[idx].FloorIndex + 1);
        ListItemEvents component2 = obj.GetComponent<ListItemEvents>();
        if (!Object.op_Inequality((Object) component2, (Object) null))
          return;
        component2.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelectItem);
      }
    }

    private void OnScrollStop(GameObject go)
    {
      TowerFloorParam dataOfClass = DataSource.FindDataOfClass<TowerFloorParam>(go, (TowerFloorParam) null);
      if (dataOfClass == null)
        return;
      GlobalVars.SelectedQuestID = dataOfClass.iname;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
    }

    private void OnSelectItem(GameObject go)
    {
      TowerFloorParam dataOfClass = DataSource.FindDataOfClass<TowerFloorParam>(go, (TowerFloorParam) null);
      if (dataOfClass == null)
        return;
      this.mScrollListController.SetScrollTo((float) ((double) this.mScrollListController.ItemScaleMargin * (double) this.mFloorParams.IndexOf(dataOfClass) - (double) this.mScrollListController.ItemScaleMargin * 2.0));
      GlobalVars.SelectedQuestID = dataOfClass.iname;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
    }
  }
}
