// Decompiled with JetBrains decompiler
// Type: SRPG.RuneDrawEvoState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class RuneDrawEvoState : MonoBehaviour
  {
    [SerializeField]
    private GameObject mEvoStatusParentOn;
    [SerializeField]
    private GameObject mEvoStatusParentOff;
    [SerializeField]
    private RuneDrawEvoStateOneSetting mEvoStateOne;
    private BindRuneData mRuneData;
    private List<RuneDrawEvoStateOneSetting> mRuneEvoStatusList = new List<RuneDrawEvoStateOneSetting>();
    private ColorBlock mKeepButtonColor = new ColorBlock();
    private bool mIsUseKeepButtonColor;

    private event RuneDrawEvoState.OnSelectedEvent mOnSelectedEvent = _param0 => { };

    public void SetSelectedCallBack(RuneDrawEvoState.OnSelectedEvent selected)
    {
      this.mOnSelectedEvent = selected;
    }

    public void OnClickEvoItem(int index) => this.mOnSelectedEvent(index);

    public void Awake()
    {
    }

    public void SetDrawParam(BindRuneData rune_data)
    {
      this.mRuneData = rune_data;
      this.CloneEvoStatusList();
      this.Refresh();
    }

    public void ShowFrame(bool is_show, int index = 0)
    {
      this.RefreshFrame();
      for (int index1 = 0; index1 < this.mRuneEvoStatusList.Count; ++index1)
      {
        if (index1 == index)
          this.mRuneEvoStatusList[index].SetShowFrame(is_show);
        else
          this.mRuneEvoStatusList[index1].SetShowFrame(false);
      }
    }

    public void SetDisableColor(bool is_hilight, int index)
    {
      if (index >= this.mRuneEvoStatusList.Count)
        return;
      Button component = ((Component) this.mRuneEvoStatusList[index]).GetComponent<Button>();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) component, (UnityEngine.Object) null))
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

    public void StartGaugeAnim(int index)
    {
      if (index >= this.mRuneEvoStatusList.Count)
        return;
      this.mRuneEvoStatusList[index].StartGaugeAnim();
    }

    public void Refresh()
    {
      this.RefreshEvoStatus();
      this.RefreshFrame();
    }

    public void RefreshFrame()
    {
      foreach (RuneDrawEvoStateOneSetting mRuneEvoStatus in this.mRuneEvoStatusList)
        mRuneEvoStatus.Refresh();
    }

    public void RefreshEvoStatus()
    {
      if (this.mRuneData == null)
        return;
      RuneData rune = this.mRuneData.Rune;
      if (rune == null)
        return;
      GameUtility.SetGameObjectActive(this.mEvoStatusParentOn, rune.state.evo_state.Count > 0);
      GameUtility.SetGameObjectActive(this.mEvoStatusParentOff, rune.state.evo_state.Count <= 0);
      this.DrawEvoStatusList();
    }

    public void CloneEvoStatusList()
    {
      if (this.mRuneData == null || UnityEngine.Object.op_Equality((UnityEngine.Object) this.mEvoStateOne, (UnityEngine.Object) null))
        return;
      RuneData rune = this.mRuneData.Rune;
      if (rune == null)
        return;
      ((Component) this.mEvoStateOne).gameObject.SetActive(false);
      this.mRuneEvoStatusList.ForEach((Action<RuneDrawEvoStateOneSetting>) (evo => UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) evo).gameObject)));
      this.mRuneEvoStatusList.Clear();
      for (int index = 0; index < MonoSingleton<GameManager>.Instance.MasterParam.FixParam.RuneMaxEvoNum; ++index)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(((Component) this.mEvoStateOne).gameObject);
        gameObject.transform.SetParent(((Component) this.mEvoStateOne).transform.parent, false);
        gameObject.SetActive(true);
        RuneDrawEvoStateOneSetting component = gameObject.GetComponent<RuneDrawEvoStateOneSetting>();
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) component, (UnityEngine.Object) null))
          break;
        Button componentInChildren = gameObject.GetComponentInChildren<Button>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren, (UnityEngine.Object) null))
        {
          if (rune != null && index < rune.GetLengthFromEvoParam())
          {
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: variable of a compiler-generated type
            RuneDrawEvoState.\u003CCloneEvoStatusList\u003Ec__AnonStorey0 listCAnonStorey0 = new RuneDrawEvoState.\u003CCloneEvoStatusList\u003Ec__AnonStorey0();
            // ISSUE: reference to a compiler-generated field
            listCAnonStorey0.\u0024this = this;
            // ISSUE: reference to a compiler-generated field
            listCAnonStorey0.number = index;
            ((Selectable) componentInChildren).interactable = true;
            // ISSUE: method pointer
            ((UnityEvent) componentInChildren.onClick).AddListener(new UnityAction((object) listCAnonStorey0, __methodptr(\u003C\u003Em__0)));
          }
          else
            ((Selectable) componentInChildren).interactable = false;
        }
        component.Refresh();
        this.mRuneEvoStatusList.Add(component);
      }
    }

    public void DrawEvoStatusList()
    {
      if (this.mRuneData == null)
        return;
      RuneData rune = this.mRuneData.Rune;
      if (rune == null)
        return;
      for (int index = 0; index < this.mRuneEvoStatusList.Count; ++index)
      {
        BaseStatus addStatus = (BaseStatus) null;
        BaseStatus scaleStatus = (BaseStatus) null;
        if (index < rune.GetLengthFromEvoParam())
        {
          rune.CreateBaseStatusFromEvoParam(index, ref addStatus, ref scaleStatus, true);
          if (addStatus == null)
            addStatus = new BaseStatus();
          if (scaleStatus == null)
            scaleStatus = new BaseStatus();
          float percentage = rune.PowerPercentageFromEvoParam(index);
          this.mRuneEvoStatusList[index].SetStatus(addStatus, scaleStatus, percentage);
        }
        else
          this.mRuneEvoStatusList[index].SetStatus((BaseStatus) null, (BaseStatus) null, 0.0f);
      }
    }

    public delegate void OnSelectedEvent(int index);
  }
}
