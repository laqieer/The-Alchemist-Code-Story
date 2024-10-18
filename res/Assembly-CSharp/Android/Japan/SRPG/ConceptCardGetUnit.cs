// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardGetUnit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(0, "ユニットのレアリティ再設定", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "ユニットのレアリティ再設定完了", FlowNode.PinTypes.Output, 100)]
  public class ConceptCardGetUnit : MonoBehaviour, IFlowInterface
  {
    [SerializeField]
    private Animator m_ConceptCardAnimator;
    [SerializeField]
    private Animator m_UnitAnimator;
    [SerializeField]
    private RawImage m_ConceptCardImage;
    [SerializeField]
    private ImageArray m_ConceptCardFrame;
    [SerializeField]
    private RawImage m_UnitImage;
    [SerializeField]
    private RawImage m_UnitBlurImage;
    [SerializeField]
    private Text m_UnitTextDescription;
    private int m_UnitRarity;

    private AnimatorStateInfo animatorStateInfo
    {
      get
      {
        return this.m_ConceptCardAnimator.GetCurrentAnimatorStateInfo(0);
      }
    }

    public static LoadRequest StartLoadPrefab()
    {
      return AssetManager.LoadAsync<GameObject>(GameSettings.Instance.ConceptCard_GetUnit);
    }

    public void Setup(ConceptCardData conceptCardData)
    {
      try
      {
        UnitParam unitParam = MonoSingleton<GameManager>.Instance.GetUnitParam(conceptCardData.Param.first_get_unit);
        ConceptCardParam card = conceptCardData.Param;
        if ((UnityEngine.Object) this.m_ConceptCardImage != (UnityEngine.Object) null && card != null)
          MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.m_ConceptCardImage, AssetPath.ConceptCard(card));
        if ((UnityEngine.Object) this.m_UnitImage != (UnityEngine.Object) null && unitParam != null)
          MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.m_UnitImage, AssetPath.UnitImage(unitParam, unitParam.GetJobId(0)));
        if ((UnityEngine.Object) this.m_UnitBlurImage != (UnityEngine.Object) null && unitParam != null)
          MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.m_UnitBlurImage, AssetPath.UnitImage(unitParam, unitParam.GetJobId(0)));
        if ((UnityEngine.Object) this.m_ConceptCardFrame != (UnityEngine.Object) null)
          this.m_ConceptCardFrame.ImageIndex = card.rare;
        if ((UnityEngine.Object) this.m_UnitTextDescription != (UnityEngine.Object) null)
          this.m_UnitTextDescription.text = LocalizedText.Get("sys.CONCEPT_CARD_UNIT_GET_DESCRIPTION", (object) card.name, (object) unitParam.name);
        this.SetConceptCardRarity(card.rare + 1);
        this.SetUnitRarity((int) unitParam.rare + 1);
        this.m_UnitRarity = (int) unitParam.rare;
        this.StartCoroutine(this.WaitConceptCardEffectEnd());
      }
      catch
      {
        this.SetConceptCardRarity(1);
        this.SetUnitRarity(1);
      }
    }

    [DebuggerHidden]
    private IEnumerator WaitConceptCardEffectEnd()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ConceptCardGetUnit.\u003CWaitConceptCardEffectEnd\u003Ec__Iterator0() { \u0024this = this };
    }

    private void SetSummonAnimationType(ConceptCardGetUnit.SummonAnimationType type)
    {
      if ((UnityEngine.Object) this.m_ConceptCardAnimator == (UnityEngine.Object) null)
        return;
      this.m_ConceptCardAnimator.SetBool("shard", type == ConceptCardGetUnit.SummonAnimationType.Shard);
      this.m_ConceptCardAnimator.SetBool("item", type == ConceptCardGetUnit.SummonAnimationType.Item);
      this.m_ConceptCardAnimator.SetBool("ccard", type == ConceptCardGetUnit.SummonAnimationType.Card);
    }

    private void SetConceptCardRarity(int value)
    {
      if ((UnityEngine.Object) this.m_ConceptCardAnimator == (UnityEngine.Object) null)
        return;
      this.m_ConceptCardAnimator.SetInteger("rariry", value);
    }

    private void SetUnitRarity(int value)
    {
      if ((UnityEngine.Object) this.m_UnitAnimator == (UnityEngine.Object) null)
        return;
      this.m_UnitAnimator.SetInteger("rariry", value);
    }

    private void ResetConceptCardAnimationState()
    {
      if ((UnityEngine.Object) this.m_ConceptCardAnimator == (UnityEngine.Object) null)
        return;
      this.m_ConceptCardAnimator.SetInteger("rariry", 0);
      this.m_ConceptCardAnimator.SetBool("shard", false);
      this.m_ConceptCardAnimator.SetBool("item", false);
      this.m_ConceptCardAnimator.SetBool("reset", false);
      this.m_ConceptCardAnimator.SetBool("ccard", false);
    }

    public void Activated(int pinID)
    {
      if (pinID != 0)
        return;
      this.SetUnitRarity(this.m_UnitRarity + 1);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
    }

    private enum SummonAnimationType
    {
      Shard,
      Item,
      Card,
    }
  }
}
