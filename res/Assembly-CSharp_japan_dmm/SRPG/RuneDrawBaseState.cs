// Decompiled with JetBrains decompiler
// Type: SRPG.RuneDrawBaseState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class RuneDrawBaseState : MonoBehaviour
  {
    [SerializeField]
    private StatusList mBaseStatusList;
    [SerializeField]
    private Text mBaseStatusRange;
    [SerializeField]
    private RuneDrawEvoStateOneSetting mOneSetting;
    [SerializeField]
    private RuneDrawBaseState.eDrawMode mDrawMode;
    private BindRuneData mRuneData;
    private ColorBlock mKeepButtonColor = new ColorBlock();
    private bool mIsUseKeepButtonColor;

    private event RuneDrawBaseState.OnSelectedEvent mOnSelectedEvent = () => { };

    public void SetSelectedCallBack(RuneDrawBaseState.OnSelectedEvent selected)
    {
      this.mOnSelectedEvent = selected;
    }

    public void Awake()
    {
    }

    public void SetDrawParam(BindRuneData rune_data)
    {
      this.mRuneData = rune_data;
      if (Object.op_Inequality((Object) this.mOneSetting, (Object) null))
      {
        Button componentInChildren = ((Component) this.mOneSetting).gameObject.GetComponentInChildren<Button>();
        if (Object.op_Inequality((Object) componentInChildren, (Object) null))
        {
          // ISSUE: method pointer
          ((UnityEvent) componentInChildren.onClick).AddListener(new UnityAction((object) this, __methodptr(\u003CSetDrawParam\u003Em__0)));
        }
      }
      this.Refresh();
    }

    public void SetButtonInteractable(bool enable)
    {
      if (!Object.op_Inequality((Object) this.mOneSetting, (Object) null))
        return;
      Button componentInChildren = ((Component) this.mOneSetting).gameObject.GetComponentInChildren<Button>();
      if (!Object.op_Inequality((Object) componentInChildren, (Object) null))
        return;
      ((Selectable) componentInChildren).interactable = enable;
    }

    public void SetDisableColor(bool is_hilight)
    {
      if (Object.op_Equality((Object) this.mOneSetting, (Object) null))
        return;
      Button component = ((Component) this.mOneSetting).gameObject.GetComponent<Button>();
      if (Object.op_Equality((Object) component, (Object) null))
        return;
      if (is_hilight)
      {
        this.mKeepButtonColor = ((Selectable) component).colors;
        this.mIsUseKeepButtonColor = true;
        ColorBlock colorBlock = new ColorBlock();
        ref ColorBlock local1 = ref colorBlock;
        ColorBlock colors1 = ((Selectable) component).colors;
        Color normalColor = ((ColorBlock) ref colors1).normalColor;
        ((ColorBlock) ref local1).normalColor = normalColor;
        ref ColorBlock local2 = ref colorBlock;
        ColorBlock colors2 = ((Selectable) component).colors;
        Color highlightedColor1 = ((ColorBlock) ref colors2).highlightedColor;
        ((ColorBlock) ref local2).highlightedColor = highlightedColor1;
        ref ColorBlock local3 = ref colorBlock;
        ColorBlock colors3 = ((Selectable) component).colors;
        Color pressedColor = ((ColorBlock) ref colors3).pressedColor;
        ((ColorBlock) ref local3).pressedColor = pressedColor;
        ref ColorBlock local4 = ref colorBlock;
        ColorBlock colors4 = ((Selectable) component).colors;
        Color highlightedColor2 = ((ColorBlock) ref colors4).highlightedColor;
        ((ColorBlock) ref local4).disabledColor = highlightedColor2;
        ref ColorBlock local5 = ref colorBlock;
        ColorBlock colors5 = ((Selectable) component).colors;
        double colorMultiplier = (double) ((ColorBlock) ref colors5).colorMultiplier;
        ((ColorBlock) ref local5).colorMultiplier = (float) colorMultiplier;
        ref ColorBlock local6 = ref colorBlock;
        ColorBlock colors6 = ((Selectable) component).colors;
        double fadeDuration = (double) ((ColorBlock) ref colors6).fadeDuration;
        ((ColorBlock) ref local6).fadeDuration = (float) fadeDuration;
        ((Selectable) component).colors = colorBlock;
      }
      else
      {
        if (!this.mIsUseKeepButtonColor)
          return;
        ((Selectable) component).colors = this.mKeepButtonColor;
      }
    }

    public void OnClickBaseItem() => this.mOnSelectedEvent();

    public void ShowFrame(bool is_show)
    {
      if (!Object.op_Implicit((Object) this.mOneSetting))
        return;
      this.mOneSetting.SetShowFrame(is_show);
    }

    public void StartGaugeAnim()
    {
      if (!Object.op_Implicit((Object) this.mOneSetting))
        return;
      this.mOneSetting.StartGaugeAnim();
    }

    public void Refresh() => this.RefreshBaseStatus();

    private void GetBaseStatusLotteryWidth(RuneData rune, out int lot_min, out int lot_max)
    {
      lot_min = 0;
      lot_max = 0;
      RuneLotteryBaseState baseLot = rune.state.base_state.base_lot;
      if (baseLot == null)
        return;
      lot_min = (int) baseLot.lot_min;
      lot_max = (int) baseLot.lot_max;
    }

    private void RefreshBaseStatus()
    {
      if (this.mRuneData == null || Object.op_Equality((Object) this.mBaseStatusList, (Object) null))
        return;
      RuneData rune = this.mRuneData.Rune;
      if (rune == null)
        return;
      BaseStatus addStatus = (BaseStatus) null;
      BaseStatus scaleStatus = (BaseStatus) null;
      BaseStatus addOnlyBaseStatus = (BaseStatus) null;
      BaseStatus scaleOnlyBaseStatus = (BaseStatus) null;
      switch (this.mDrawMode)
      {
        case RuneDrawBaseState.eDrawMode.TotalStatus:
          rune.CreateBaseStatusFromBaseParam(ref addStatus, ref scaleStatus, true);
          break;
        case RuneDrawBaseState.eDrawMode.TotalStatusAndBonus:
          rune.CreateBaseStatusFromBaseParam(ref addStatus, ref scaleStatus, true);
          rune.CreateBaseStatusFromOnlyBaseParam(ref addOnlyBaseStatus, ref scaleOnlyBaseStatus, true);
          if (addOnlyBaseStatus == null)
            addOnlyBaseStatus = new BaseStatus();
          if (scaleOnlyBaseStatus == null)
          {
            scaleOnlyBaseStatus = new BaseStatus();
            break;
          }
          break;
        case RuneDrawBaseState.eDrawMode.OnlyBaseStatus:
          rune.CreateBaseStatusFromOnlyBaseParam(ref addOnlyBaseStatus, ref scaleOnlyBaseStatus, true);
          addStatus = addOnlyBaseStatus;
          scaleStatus = scaleOnlyBaseStatus;
          break;
      }
      if (addStatus == null)
        addStatus = new BaseStatus();
      if (scaleStatus == null)
        scaleStatus = new BaseStatus();
      if (Object.op_Implicit((Object) this.mBaseStatusRange))
      {
        int lot_min;
        int lot_max;
        this.GetBaseStatusLotteryWidth(rune, out lot_min, out lot_max);
        this.mBaseStatusRange.text = LocalizedText.Get("sys.RUNE_BASE_STATUS_RANGE", (object) lot_min.ToString(), (object) lot_max.ToString());
      }
      if (Object.op_Implicit((Object) this.mOneSetting))
      {
        float percentage = rune.PowerPercentageFromBaseParam();
        this.mOneSetting.SetStatus(addStatus, scaleStatus, percentage);
      }
      else
      {
        switch (this.mDrawMode)
        {
          case RuneDrawBaseState.eDrawMode.TotalStatus:
            BaseStatus paramMul1 = new BaseStatus();
            this.mBaseStatusList.SetValues(addStatus, paramMul1);
            break;
          case RuneDrawBaseState.eDrawMode.TotalStatusAndBonus:
            addStatus.Sub(addOnlyBaseStatus);
            scaleStatus.Sub(scaleOnlyBaseStatus);
            this.mBaseStatusList.SetValues_TotalAndBonus(addOnlyBaseStatus, scaleOnlyBaseStatus, addOnlyBaseStatus, scaleOnlyBaseStatus, addStatus, scaleStatus, addStatus, scaleStatus);
            break;
          case RuneDrawBaseState.eDrawMode.OnlyBaseStatus:
            BaseStatus paramMul2 = new BaseStatus();
            this.mBaseStatusList.SetValues(addStatus, paramMul2);
            break;
        }
      }
    }

    public enum eDrawMode
    {
      TotalStatus,
      TotalStatusAndBonus,
      OnlyBaseStatus,
    }

    public delegate void OnSelectedEvent();
  }
}
