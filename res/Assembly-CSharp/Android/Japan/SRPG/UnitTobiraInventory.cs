// Decompiled with JetBrains decompiler
// Type: SRPG.UnitTobiraInventory
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(0, "表示更新", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "扉選択", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(2, "扉の開放ボタンクリック", FlowNode.PinTypes.Input, 20)]
  [FlowNode.Pin(11, "扉の開放リクエスト", FlowNode.PinTypes.Output, 21)]
  [FlowNode.Pin(3, "扉の強化ボタンクリック", FlowNode.PinTypes.Input, 30)]
  [FlowNode.Pin(12, "扉の強化リクエスト", FlowNode.PinTypes.Output, 31)]
  [FlowNode.Pin(4, "扉の開放条件詳細クリック", FlowNode.PinTypes.Input, 40)]
  [FlowNode.Pin(5, "扉の強化成功", FlowNode.PinTypes.Input, 50)]
  [FlowNode.Pin(6, "扉の解放成功", FlowNode.PinTypes.Input, 60)]
  [FlowNode.Pin(7, "扉の強化画面を閉じる", FlowNode.PinTypes.Input, 70)]
  public class UnitTobiraInventory : MonoBehaviour, IFlowInterface
  {
    [SerializeField]
    private string PREFAB_PATH_TOBIRA_ENHANCE_WINDOW = "UI/TobiraEnhanceItemWindow";
    private List<UnitTobiraItem> mTobiraList = new List<UnitTobiraItem>();
    private const int INPUT_TOBIRA_REFRESH = 0;
    private const int INPUT_TOBIRA_SELECT = 1;
    private const int INPUT_TOBIRA_OPEN = 2;
    private const int INPUT_TOBIRA_ENHANCE = 3;
    private const int INPUT_TOBIRA_OPEN_DETAIL = 4;
    private const int INPUT_TOBIRA_SUCCESS_ENHANCE = 5;
    private const int INPUT_TOBIRA_SUCCESS_OPEN = 6;
    private const int INPUT_TOBIRA_CLOSE = 7;
    private const int OUTPUT_TOBIRA_OPEN = 11;
    private const int OUTPUT_TOBIRA_ENHANCE = 12;
    [SerializeField]
    private GameObject UnitAttachTarget;
    [SerializeField]
    private GameObject TobiraTemplate;
    [SerializeField]
    private UnitTobiraParamWindow TobiraParamWindow;
    [SerializeField]
    private CanvasGroup MyCanvas;
    private UnitData mCurrentUnit;
    private TobiraData mCurrentTobira;
    private UnitTobiraEnhanceWindow mTobiraEnhanceWindow;
    private TobiraParam.Category mSelectedTobiraCategory;
    private static long mInitTimeUniqueID;

    public static long InitTimeUniqueID
    {
      get
      {
        return UnitTobiraInventory.mInitTimeUniqueID;
      }
    }

    public void Init(bool is_restore)
    {
      if ((UnityEngine.Object) this.UnitAttachTarget != (UnityEngine.Object) null && !this.RefreshCurrentUnit())
        return;
      GlobalVars.SelectedEquipmentSlot.Set(-1);
      this.RefreshStatus();
      if (is_restore)
      {
        this.RefreshSelected();
        this.OnTobiraEnhanceBtn();
      }
      else if (this.mTobiraList.Count > 0)
      {
        TobiraParam.Category default_category = this.mTobiraList[0].Category;
        this.mSelectedTobiraCategory = default_category;
        GlobalVars.PreBattleUnitTobiraCategory.Set(default_category);
        this.mTobiraList.ForEach((Action<UnitTobiraItem>) (tobira => tobira.Select(tobira.Category == default_category)));
        this.mCurrentTobira = this.mCurrentUnit.TobiraData.Find((Predicate<TobiraData>) (tobira => tobira.Param.TobiraCategory == default_category));
        this.TobiraParamWindow.Refresh(this.mCurrentUnit, this.mCurrentTobira, this.mTobiraList[0].Param);
      }
      UnitEnhanceV3.Instance.RefreshTobiraBgAnimation(this.mCurrentTobira, true);
      UnitTobiraInventory.mInitTimeUniqueID = (long) GlobalVars.SelectedUnitUniqueID;
    }

    private void OnDestroy()
    {
      if (!((UnityEngine.Object) this.mTobiraEnhanceWindow != (UnityEngine.Object) null))
        return;
      UnityEngine.Object.Destroy((UnityEngine.Object) this.mTobiraEnhanceWindow.gameObject);
      this.mTobiraEnhanceWindow = (UnitTobiraEnhanceWindow) null;
    }

    private bool RefreshCurrentUnit()
    {
      UnitData unitDataByUniqueId = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID((long) GlobalVars.SelectedUnitUniqueID);
      if (unitDataByUniqueId == null)
        return false;
      this.mCurrentUnit = new UnitData();
      this.mCurrentUnit.Setup(unitDataByUniqueId);
      return true;
    }

    private void RefreshAll()
    {
      this.RefreshStatus();
      this.RefreshSelected();
      UnitEnhanceV3.Instance.RefreshTobiraBgAnimation(this.mCurrentTobira, false);
    }

    private void RefreshStatus()
    {
      if (this.mCurrentUnit == null || (UnityEngine.Object) this.TobiraTemplate == (UnityEngine.Object) null)
        return;
      DataSource.Bind<UnitData>(this.UnitAttachTarget, this.mCurrentUnit, false);
      GameParameter.UpdateAll(this.UnitAttachTarget);
      this.TobiraTemplate.SetActive(false);
      this.mTobiraList.ForEach((Action<UnitTobiraItem>) (tobira => UnityEngine.Object.Destroy((UnityEngine.Object) tobira.gameObject)));
      this.mTobiraList.Clear();
      for (int index = 1; index < 8; ++index)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.TobiraTemplate);
        gameObject.transform.SetParent(this.TobiraTemplate.transform.parent, false);
        gameObject.SetActive(true);
        UnitTobiraItem component = gameObject.GetComponent<UnitTobiraItem>();
        if (!((UnityEngine.Object) component == (UnityEngine.Object) null))
        {
          component.Initialize(this.mCurrentUnit, (TobiraParam.Category) index);
          this.mTobiraList.Add(component);
        }
      }
    }

    private void ResetSelectedUnitUniqueID()
    {
      if (this.mCurrentUnit == null)
        return;
      GlobalVars.SelectedUnitUniqueID.Set(UnitTobiraInventory.mInitTimeUniqueID);
    }

    private void RefreshSelected()
    {
      this.mCurrentTobira = this.mCurrentUnit.TobiraData.Find((Predicate<TobiraData>) (tobira => tobira.Param.TobiraCategory == (TobiraParam.Category) GlobalVars.PreBattleUnitTobiraCategory));
      this.mTobiraList.ForEach((Action<UnitTobiraItem>) (tobira => tobira.Select(tobira.Category == this.mCurrentTobira.Param.TobiraCategory)));
      this.TobiraParamWindow.Refresh(this.mCurrentUnit, this.mCurrentTobira, this.mCurrentTobira.Param);
    }

    private void OnTobiraOpenBtn()
    {
      if ((UnityEngine.Object) UnitEnhanceV3.Instance != (UnityEngine.Object) null)
        UnitEnhanceV3.Instance.BeginStatusChangeCheck();
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 11);
    }

    private void OnTobiraEnhanceBtn()
    {
      this.ResetSelectedUnitUniqueID();
      GameObject original = AssetManager.Load<GameObject>(this.PREFAB_PATH_TOBIRA_ENHANCE_WINDOW);
      if (!((UnityEngine.Object) original != (UnityEngine.Object) null))
        return;
      this.mTobiraEnhanceWindow = UnityEngine.Object.Instantiate<GameObject>(original).GetComponent<UnitTobiraEnhanceWindow>();
      this.mTobiraEnhanceWindow.Initialize(this.mCurrentUnit, this.mCurrentTobira);
      this.mTobiraEnhanceWindow.OnCallback = new UnitTobiraEnhanceWindow.CallbackEvent(this.RequestTobiraEnhance);
    }

    private void RequestTobiraEnhance()
    {
      this.ResetSelectedUnitUniqueID();
      if ((UnityEngine.Object) UnitEnhanceV3.Instance != (UnityEngine.Object) null)
        UnitEnhanceV3.Instance.BeginStatusChangeCheck();
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 12);
    }

    private void SuccessTobiraEnhance()
    {
      this.StartCoroutine(this.PlayLevelupEffect());
    }

    private void SuccessTobiraOpen()
    {
      this.StartCoroutine(this.PlayOpenEffect());
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 0:
          this.RefreshAll();
          break;
        case 1:
          SerializeValueList currentValue = FlowNode_ButtonEvent.currentValue as SerializeValueList;
          if (currentValue == null)
            break;
          UnitTobiraItem item = currentValue.GetComponent<UnitTobiraItem>("_self");
          if ((UnityEngine.Object) item == (UnityEngine.Object) null)
            break;
          this.mSelectedTobiraCategory = item.Category;
          GlobalVars.PreBattleUnitTobiraCategory.Set(this.mSelectedTobiraCategory);
          this.mCurrentTobira = this.mCurrentUnit.TobiraData.Find((Predicate<TobiraData>) (tobira => tobira.Param.TobiraCategory == item.Category));
          this.mTobiraList.ForEach((Action<UnitTobiraItem>) (tobira => tobira.Select(tobira.Category == item.Category)));
          this.TobiraParamWindow.Refresh(this.mCurrentUnit, this.mCurrentTobira, item.Param);
          UnitEnhanceV3.Instance.RefreshTobiraBgAnimation(this.mCurrentTobira, false);
          break;
        case 2:
          this.OnTobiraOpenBtn();
          break;
        case 3:
          this.OnTobiraEnhanceBtn();
          break;
        case 5:
          this.RefreshCurrentUnit();
          this.SuccessTobiraEnhance();
          break;
        case 6:
          this.RefreshCurrentUnit();
          this.SuccessTobiraOpen();
          break;
        case 7:
          UnitEnhanceV3.Instance.TobiraUIActive(false, false);
          break;
      }
    }

    [DebuggerHidden]
    private IEnumerator PlayOpenEffect()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitTobiraInventory.\u003CPlayOpenEffect\u003Ec__Iterator0() { \u0024this = this };
    }

    [DebuggerHidden]
    private IEnumerator PlayLevelupEffect()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitTobiraInventory.\u003CPlayLevelupEffect\u003Ec__Iterator1() { \u0024this = this };
    }
  }
}
