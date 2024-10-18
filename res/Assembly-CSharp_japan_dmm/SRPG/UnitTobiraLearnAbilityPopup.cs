// Decompiled with JetBrains decompiler
// Type: SRPG.UnitTobiraLearnAbilityPopup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(0, "次の表示はありますか？", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "次の表示はあります", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "次の表示はありません", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(200, "次の表示へ切り替える", FlowNode.PinTypes.Input, 200)]
  [FlowNode.Pin(201, "次の表示へ切り替え中", FlowNode.PinTypes.Output, 201)]
  [FlowNode.Pin(202, "次の表示へ切り替えた", FlowNode.PinTypes.Output, 202)]
  [FlowNode.Pin(300, "全ての表示完了", FlowNode.PinTypes.Output, 300)]
  public class UnitTobiraLearnAbilityPopup : MonoBehaviour, IFlowInterface
  {
    public const int PIN_INPUT_ASK_NEXT_VIEW = 0;
    public const int PIN_OUTPUT_HAS_NEXT_VIEW = 100;
    public const int PIN_OUTPUT_DONT_HAVE_NEXT_VIEW = 101;
    public const int PIN_INPUT_CHANGE_NEXT_VIEW = 200;
    public const int PIN_OUTPUT_CHANGING_NEXT_VIEW = 201;
    public const int PIN_OUTPUT_CHANGED_NEXT_VIEW = 202;
    public const int PIN_OUTPUT_COMPLETE = 300;
    [SerializeField]
    private Text mTitleText;
    [SerializeField]
    private Text mNameText;
    [SerializeField]
    private Text mDescText;
    [SerializeField]
    private Animator mAnimator;
    private Queue<UnitTobiraLearnAbilityPopup.ViewParam> mViewParams = new Queue<UnitTobiraLearnAbilityPopup.ViewParam>();
    private UnitTobiraLearnAbilityPopup.ViewParam mCurrentViewParam;
    private Coroutine mChangeViewRoutine;

    private bool HasNextView => this.mViewParams.Count > 0;

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 0:
          if (this.HasNextView)
          {
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
            break;
          }
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
          break;
        case 200:
          if (this.HasNextView)
          {
            this.RefreshCurrentViewParam(true);
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 202);
            break;
          }
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 300);
          break;
      }
    }

    private void ChangeNextView()
    {
      if (this.mChangeViewRoutine != null)
        return;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 201);
      this.mChangeViewRoutine = this.StartCoroutine(this.InternalChangeNextView());
    }

    [DebuggerHidden]
    private IEnumerator InternalChangeNextView()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitTobiraLearnAbilityPopup.\u003CInternalChangeNextView\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    public void Setup(UnitData unit, AbilityParam new_ability, AbilityParam old_ability)
    {
      if (unit == null || new_ability == null)
        return;
      this.AddViewParam(old_ability == null ? LocalizedText.Get("sys.TOBIRA_LEARN_NEW_ABILITY_TEXT") : LocalizedText.Get("sys.TOBIRA_LEARN_OVERRIDE_NEW_ABILITY_TEXT"), new_ability.name, new_ability.expr);
      this.RefreshCurrentViewParam(false);
      DataSource.Bind<UnitData>(((Component) this).gameObject, unit);
      GameParameter.UpdateAll(((Component) this).gameObject);
    }

    public void Setup(UnitData unit, SkillParam skill)
    {
      if (unit == null || skill == null)
        return;
      this.AddViewParam(LocalizedText.Get("sys.TOBIRA_LEARN_NEW_LEADER_SKILL_TEXT"), skill.name, skill.expr);
      this.RefreshCurrentViewParam(false);
      DataSource.Bind<UnitData>(((Component) this).gameObject, unit);
      GameParameter.UpdateAll(((Component) this).gameObject);
    }

    public void Setup(UnitData unit)
    {
      this.AddViewParam(LocalizedText.Get("sys.TOBIRA_MASTER_CONCEPT_CARD_SLOT2_UNLOCK_TEXT"), LocalizedText.Get("sys.TOBIRA_MASTER_CONCEPT_CARD_SLOT2_UNLOCK_TEXT_DESC"), string.Empty);
      this.RefreshCurrentViewParam(false);
      DataSource.Bind<UnitData>(((Component) this).gameObject, unit);
      GameParameter.UpdateAll(((Component) this).gameObject);
    }

    private void AddViewParam(string title, string name, string desc)
    {
      this.mViewParams.Enqueue(new UnitTobiraLearnAbilityPopup.ViewParam()
      {
        mTitle = title,
        mName = name,
        mDesc = desc
      });
    }

    private void RefreshCurrentViewParam(bool isForceRefresh)
    {
      if (!isForceRefresh && this.mCurrentViewParam != null)
        return;
      if (this.mViewParams.Count > 0)
        this.mCurrentViewParam = this.mViewParams.Dequeue();
      if (this.mCurrentViewParam == null)
        return;
      this.mTitleText.text = this.mCurrentViewParam.mTitle;
      this.mNameText.text = this.mCurrentViewParam.mName;
      this.mDescText.text = this.mCurrentViewParam.mDesc;
    }

    private class ViewParam
    {
      public string mTitle;
      public string mName;
      public string mDesc;
    }
  }
}
