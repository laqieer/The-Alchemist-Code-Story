// Decompiled with JetBrains decompiler
// Type: SRPG.GachaPickupSelectWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "初期化", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "表示更新", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(3, "決定ボタンをタップ", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(11, "確認", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(12, "決定", FlowNode.PinTypes.Output, 12)]
  public class GachaPickupSelectWindow : MonoBehaviour, IFlowInterface
  {
    private const int PIN_IN_INIT = 1;
    private const int PIN_IN_REFRESH = 2;
    private const int PIN_IN_DECIDE = 3;
    private const int PIN_OUT_NOT_SELECTED = 10;
    private const int PIN_OUT_CONFIRM = 11;
    private const int PIN_OUT_DECIDE = 12;
    [SerializeField]
    private GameObject mPickupUnitList;
    [SerializeField]
    private ContentController mContentControllerUnit;
    [SerializeField]
    private GameObject mPickupCardList;
    [SerializeField]
    private ContentController mContentControllerCard;
    [SerializeField]
    private Transform SelectedListParent;
    [SerializeField]
    private GameObject SelectedListItem;
    [SerializeField]
    private Button DecideButton;
    private List<GachaDropData> mDropDatas = new List<GachaDropData>();
    private List<GachaPickupIconParam> mIconParams = new List<GachaPickupIconParam>();
    private List<GachaDropData> mSelectedDatas = new List<GachaDropData>();
    private List<GameObject> mSelectedLists = new List<GameObject>();
    private ContentController mMainContentController;
    private int mSelectMax = 1;
    private GachaPickupSelectType mSelectType;
    private bool mIsInteractable = true;

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 1:
          this.Init();
          break;
        case 2:
          this.Refresh();
          break;
        case 3:
          this.OnDecide();
          break;
      }
    }

    private void Awake()
    {
      GameUtility.SetGameObjectActive(this.SelectedListItem, false);
      GameUtility.SetGameObjectActive(this.mPickupUnitList, false);
      GameUtility.SetGameObjectActive(this.mPickupCardList, false);
      GameUtility.SetButtonIntaractable(this.DecideButton, false);
    }

    public void Init()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) GachaWindow.Instance, (UnityEngine.Object) null))
        return;
      GachaWindow instance = GachaWindow.Instance;
      GachaTopParamNew[] currentGacha = instance.GetCurrentGacha();
      if (currentGacha == null || currentGacha.Length <= 0)
        return;
      this.mSelectType = currentGacha[0].PickupSelectType;
      GachaPickupSelectParam pickupSelectParam = instance.GetCurrentGachaPickupSelectParam();
      if (pickupSelectParam == null)
        return;
      this.mDropDatas = pickupSelectParam.pickup_select_list;
      if (this.mDropDatas == null || this.mDropDatas.Count <= 0)
        return;
      this.mSelectMax = pickupSelectParam.select_pickup_num;
      for (int index1 = 0; index1 < currentGacha[0].SelectedPickupList.Count; ++index1)
      {
        GachaDropData gdata = currentGacha[0].SelectedPickupList[index1];
        if (gdata != null)
        {
          int index2 = this.mDropDatas.FindIndex((Predicate<GachaDropData>) (d => d.Iname == gdata.Iname));
          if (index2 >= 0)
            this.mSelectedDatas.Add(this.mDropDatas[index2]);
        }
      }
      if (this.mDropDatas[0].type == GachaDropData.Type.ConceptCard)
      {
        this.mMainContentController = this.mContentControllerCard;
        GameUtility.SetGameObjectActive(this.mPickupCardList, true);
      }
      else
      {
        this.mMainContentController = this.mContentControllerUnit;
        GameUtility.SetGameObjectActive(this.mPickupUnitList, true);
      }
      if (this.mSelectType == GachaPickupSelectType.UnChangeble)
        this.mIsInteractable = (this.mSelectedDatas == null ? 0 : (this.mSelectedDatas.Count == this.mSelectMax ? 1 : 0)) == 0;
      this.Initalize();
      this.InitalizeSelectedList();
      this.Refresh();
    }

    private void Initalize()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mMainContentController, (UnityEngine.Object) null))
        return;
      this.mMainContentController.Release();
      ContentSource source = new ContentSource();
      this.mIconParams.Clear();
      if (this.mDropDatas != null)
      {
        for (int index = 0; index < this.mDropDatas.Count; ++index)
        {
          GachaPickupIconParam gachaPickupIconParam = new GachaPickupIconParam();
          gachaPickupIconParam.drop_data = this.mDropDatas[index];
          gachaPickupIconParam.interactable = this.mIsInteractable;
          gachaPickupIconParam.Initialize(source);
          this.mIconParams.Add(gachaPickupIconParam);
        }
      }
      source.SetTable((ContentSource.Param[]) this.mIconParams.ToArray());
      this.mMainContentController.Initialize(source, Vector2.zero);
    }

    private void Refresh()
    {
      if (this.mIconParams != null)
      {
        for (int index = 0; index < this.mIconParams.Count; ++index)
        {
          if (this.mIconParams[index] != null)
          {
            GachaPickupIconParam param = this.mIconParams[index];
            param.select = param.drop_data != null && this.mSelectedDatas != null && this.mSelectedDatas.FindIndex((Predicate<GachaDropData>) (d => d.Iname == param.drop_data.Iname)) != -1;
            param.Refresh();
          }
        }
      }
      this.RefreshSelectedList();
      bool flag = this.mSelectedDatas != null && this.mSelectedDatas.Count > 0 && this.mSelectedDatas.Count == this.mSelectMax;
      GameUtility.SetButtonIntaractable(this.DecideButton, this.mIsInteractable && flag);
    }

    public void OnClickNodeSelectedList(GachaDropIconNode node)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) node, (UnityEngine.Object) null))
        return;
      GachaDropData dropData = node.DropData;
      if (dropData == null)
        return;
      this.SetSelectPickupData(dropData);
      this.Refresh();
    }

    public void OnClickNode(GachaDropIconNode node)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) node, (UnityEngine.Object) null) || !(node.GetParam() is GachaPickupIconParam gachaPickupIconParam))
        return;
      this.SetSelectPickupData(gachaPickupIconParam.drop_data);
      this.Refresh();
    }

    private void SetSelectPickupData(GachaDropData select)
    {
      if (select == null || this.mSelectedDatas == null)
        return;
      int index = this.mSelectedDatas.FindIndex((Predicate<GachaDropData>) (d => d.Iname == select.Iname));
      if (index != -1)
      {
        this.mSelectedDatas.RemoveAt(index);
      }
      else
      {
        this.mSelectedDatas.Add(select);
        if (this.mSelectedDatas.Count <= this.mSelectMax)
          return;
        this.mSelectedDatas.RemoveAt(0);
      }
    }

    private void InitalizeSelectedList()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.SelectedListParent, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.SelectedListItem, (UnityEngine.Object) null))
        return;
      this.mSelectedLists.Clear();
      for (int index = 0; index < this.mSelectMax; ++index)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.SelectedListItem);
        gameObject.transform.SetParent(this.SelectedListParent, false);
        GameUtility.SetButtonIntaractable(gameObject.GetComponent<Button>(), this.mIsInteractable);
        GachaDropIconNode component = gameObject.GetComponent<GachaDropIconNode>();
        if (!UnityEngine.Object.op_Equality((UnityEngine.Object) component, (UnityEngine.Object) null))
        {
          component.Reset();
          GameUtility.SetGameObjectActive(gameObject, true);
          this.mSelectedLists.Add(gameObject);
        }
      }
    }

    private void RefreshSelectedList()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.SelectedListParent, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.SelectedListItem, (UnityEngine.Object) null))
        return;
      this.ResetSelectedListIcon();
      if (this.mSelectedDatas == null || this.mSelectedDatas.Count <= 0)
        return;
      for (int index = 0; index < this.mSelectedDatas.Count && index < this.mSelectedLists.Count; ++index)
      {
        GachaDropData mSelectedData = this.mSelectedDatas[index];
        if (mSelectedData != null)
        {
          GameObject mSelectedList = this.mSelectedLists[index];
          if (!UnityEngine.Object.op_Equality((UnityEngine.Object) mSelectedList, (UnityEngine.Object) null))
          {
            GachaDropIconNode component = mSelectedList.GetComponent<GachaDropIconNode>();
            if (!UnityEngine.Object.op_Equality((UnityEngine.Object) component, (UnityEngine.Object) null))
              component.Setup(mSelectedData, true);
          }
        }
      }
    }

    private void ResetSelectedListIcon()
    {
      if (this.mSelectedLists == null || this.mSelectedLists.Count <= 0)
        return;
      for (int index = 0; index < this.mSelectedLists.Count; ++index)
      {
        GachaDropIconNode component = this.mSelectedLists[index].GetComponent<GachaDropIconNode>();
        if (!UnityEngine.Object.op_Equality((UnityEngine.Object) component, (UnityEngine.Object) null))
          component.Reset();
      }
    }

    private void OnDecide()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) GachaWindow.Instance, (UnityEngine.Object) null))
        return;
      GachaWindow.Instance.SetRequestPickupSelectData(this.mSelectedDatas);
      switch (this.mSelectType)
      {
        case GachaPickupSelectType.Changeble:
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 12);
          break;
        case GachaPickupSelectType.UnChangeble:
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 11);
          break;
      }
    }
  }
}
