// Decompiled with JetBrains decompiler
// Type: SRPG.VersusDraftPopupList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "Generate List", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(11, "Generated", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(2, "Page Prev", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(3, "Page Next", FlowNode.PinTypes.Input, 4)]
  [FlowNode.Pin(21, "Is First Page", FlowNode.PinTypes.Output, 5)]
  [FlowNode.Pin(22, "Is Last Page", FlowNode.PinTypes.Output, 6)]
  public class VersusDraftPopupList : MonoBehaviour, IFlowInterface
  {
    private const int PIN_INPUT_GENERATE_LIST = 1;
    private const int PIN_INPUT_PAGE_PREV = 2;
    private const int PIN_INPUT_PAGE_NEXT = 3;
    private const int PIN_OUTPUT_GENERATED = 11;
    private const int PIN_OUTPUT_FIRST_PAGE = 21;
    private const int PIN_OUTPUT_LAST_PAGE = 22;
    private const int UNIT_COUNT_PER_PAGE = 28;
    private const string TAB_INDEX_SVB_KEY = "VDPL_TAB_INDEX_KEY";
    [SerializeField]
    private Transform mUnitParentTransform;
    [SerializeField]
    private GameObject mGOUnitItem;
    [SerializeField]
    private Text mPageMaxTxt;
    [SerializeField]
    private Text mPageNowTxt;
    [SerializeField]
    private ToggleGroup mTabGroup;
    [SerializeField]
    private Toggle mTabItem;
    private List<VersusDraftPopupList.DraftDeck> mDraftDeckList;
    private List<GameObject> mUnitList;
    private int mNowDeck;

    private void Awake()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      List<VersusDraftDeckParam> versusDraftDecksNow = instance.GetVersusDraftDecksNow(instance.mVersusEnableId);
      if (versusDraftDecksNow == null || versusDraftDecksNow.Count <= 0)
        return;
      this.mDraftDeckList = new List<VersusDraftPopupList.DraftDeck>(versusDraftDecksNow.Count);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mTabItem, (UnityEngine.Object) null))
        return;
      ((Component) this.mTabItem).gameObject.SetActive(false);
      for (int index = 0; index < versusDraftDecksNow.Count; ++index)
      {
        VersusDraftPopupList.DraftDeck draftDeck = new VersusDraftPopupList.DraftDeck()
        {
          mDraftUnitListCache = instance.GetVersusDraftUnits((long) versusDraftDecksNow[index].Id),
          mUnitList = new List<GameObject>(),
          mPage = 0
        };
        draftDeck.mPageMax = Mathf.CeilToInt((float) draftDeck.mDraftUnitListCache.Count / 28f);
        Toggle toggle = UnityEngine.Object.Instantiate<Toggle>(this.mTabItem, ((Component) this.mTabItem).transform.parent);
        // ISSUE: method pointer
        ((UnityEvent<bool>) toggle.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(OnChange)));
        ((Component) toggle).gameObject.AddComponent<SerializeValueBehaviour>().list.SetObject("VDPL_TAB_INDEX_KEY", (object) index);
        ((Component) toggle).gameObject.SetActive(true);
        Text componentInChildren = ((Component) ((Component) toggle).transform).GetComponentInChildren<Text>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren, (UnityEngine.Object) null))
          componentInChildren.text = versusDraftDecksNow[index].Name;
        draftDeck.mTab = toggle;
        this.mDraftDeckList.Add(draftDeck);
      }
      this.mUnitList = new List<GameObject>();
      this.mGOUnitItem.SetActive(false);
      this.mPageMaxTxt.text = this.mDraftDeckList[this.mNowDeck].mPageMax.ToString();
      GameUtility.SetToggle(this.mDraftDeckList[this.mNowDeck].mTab, true);
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 1:
          this.GenerateList();
          break;
        case 2:
          if (this.mDraftDeckList[this.mNowDeck].mPage - 1 < 0)
            break;
          this.GenerateList(--this.mDraftDeckList[this.mNowDeck].mPage);
          break;
        case 3:
          if (this.mDraftDeckList[this.mNowDeck].mPage + 1 >= this.mDraftDeckList[this.mNowDeck].mPageMax)
            break;
          this.GenerateList(++this.mDraftDeckList[this.mNowDeck].mPage);
          break;
      }
    }

    private void GenerateList(int page = 0)
    {
      this.mUnitList.ForEach((Action<GameObject>) (go => go.SetActive(false)));
      if (this.mDraftDeckList[this.mNowDeck].mUnitList.Count < (page + 1) * 28)
      {
        for (int index1 = 0; index1 < 28; ++index1)
        {
          UnitData data = (UnitData) null;
          int index2 = index1 + page * 28;
          if (index2 < this.mDraftDeckList[this.mNowDeck].mDraftUnitListCache.Count)
          {
            Json_Unit jsonUnit = this.mDraftDeckList[this.mNowDeck].mDraftUnitListCache[index2].GetJson_Unit();
            if (jsonUnit != null)
            {
              data = new UnitData();
              data.Deserialize(jsonUnit);
            }
          }
          GameObject root = UnityEngine.Object.Instantiate<GameObject>(this.mGOUnitItem);
          DataSource.Bind<UnitData>(root, data);
          GameParameter.UpdateAll(root);
          root.transform.SetParent(this.mUnitParentTransform, false);
          root.SetActive(true);
          this.mDraftDeckList[this.mNowDeck].mUnitList.Add(root);
          this.mUnitList.Add(root);
        }
      }
      for (int index = 0; index < 28; ++index)
        this.mDraftDeckList[this.mNowDeck].mUnitList[page * 28 + index].SetActive(true);
      this.mPageNowTxt.text = (this.mDraftDeckList[this.mNowDeck].mPage + 1).ToString();
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 11);
      if (this.mDraftDeckList[this.mNowDeck].mPage - 1 < 0)
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 21);
      if (this.mDraftDeckList[this.mNowDeck].mPage + 1 < this.mDraftDeckList[this.mNowDeck].mPageMax)
        return;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 22);
    }

    private void OnChange(bool on)
    {
      if (!on)
        return;
      SerializeValueBehaviour component = ((Component) this.mTabGroup.ActiveToggles().FirstOrDefault<Toggle>()).GetComponent<SerializeValueBehaviour>();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) component, (UnityEngine.Object) null))
        return;
      int num = component.list.GetObject<int>("VDPL_TAB_INDEX_KEY");
      if (this.mNowDeck == num)
        return;
      this.mNowDeck = num;
      this.mDraftDeckList[this.mNowDeck].mPage = 0;
      this.mPageMaxTxt.text = this.mDraftDeckList[this.mNowDeck].mPageMax.ToString();
      this.GenerateList();
    }

    private class DraftDeck
    {
      public List<VersusDraftUnitParam> mDraftUnitListCache;
      public List<GameObject> mUnitList;
      public int mPage;
      public int mPageMax;
      public Toggle mTab;
    }
  }
}
