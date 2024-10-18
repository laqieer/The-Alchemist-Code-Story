// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardMaxPowerUp
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(0, "タップ入力", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "全ページ表示終了", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "表示コンテンツなし", FlowNode.PinTypes.Output, 101)]
  public class ConceptCardMaxPowerUp : MonoBehaviour, IFlowInterface
  {
    private const int PIN_PAGE_NEXT = 0;
    private const int PIN_FINISHED = 100;
    private const int PIN_NO_CONTENTS = 101;
    private const string mGroupMaxAbilityResultPrefabPath = "UI/ConceptCardMaxPowerUpVisionAbility";
    [SerializeField]
    private Transform resultRoot;
    [SerializeField]
    private ConceptCardTrustMaster conceptCardTrustMaster;
    private int prevAwakeCount;
    private ConceptCardData currentCardData;
    private SkillPowerUpResult skillPowerUpResult;
    private AbilityPowerUpResult abilityPowerUpResult;
    private bool isEnd;

    public Transform ResultRoot => this.resultRoot;

    public void SetData(
      SkillPowerUpResult inSkillPowerUpResult,
      ConceptCardData inCurrentCardData,
      int inPrevAwakeCount,
      int inPrevLevel)
    {
      this.prevAwakeCount = inPrevAwakeCount;
      this.skillPowerUpResult = inSkillPowerUpResult;
      this.currentCardData = inCurrentCardData;
      this.skillPowerUpResult.SetData(this.currentCardData, this.prevAwakeCount, inPrevLevel, true);
      this.conceptCardTrustMaster.SetData(this.currentCardData);
      if (this.skillPowerUpResult.IsEnd)
      {
        Object.Destroy((Object) ((Component) this.skillPowerUpResult).gameObject);
        this.skillPowerUpResult = (SkillPowerUpResult) null;
        if (!this.HasAbilityPowerUp())
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
        else
          this.StartCoroutine(this.CreateAbilityResultCroutine());
      }
      else
        this.CheckPages();
    }

    public void Activated(int pinID)
    {
      if (pinID != 0)
        return;
      this.CheckPages();
    }

    private void CheckPages()
    {
      if (this.isEnd)
        return;
      if (Object.op_Inequality((Object) this.skillPowerUpResult, (Object) null))
      {
        if (this.skillPowerUpResult.IsEnd)
        {
          if (!this.HasAbilityPowerUp())
          {
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
            this.isEnd = true;
          }
          else
            this.StartCoroutine(this.CreateAbilityResultCroutine());
          Object.Destroy((Object) ((Component) this.skillPowerUpResult).gameObject);
          this.skillPowerUpResult = (SkillPowerUpResult) null;
        }
        else
          this.skillPowerUpResult.ApplyContent();
      }
      else if (Object.op_Inequality((Object) this.abilityPowerUpResult, (Object) null))
      {
        if (this.abilityPowerUpResult.IsEnd)
        {
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
          this.isEnd = true;
        }
        else
          this.abilityPowerUpResult.ApplyContent();
      }
      else
      {
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
        this.isEnd = true;
      }
    }

    private bool HasAbilityPowerUp()
    {
      bool flag = false;
      if (this.currentCardData != null && this.currentCardData.EquipEffects != null)
      {
        foreach (ConceptCardEquipEffect equipEffect in this.currentCardData.EquipEffects)
        {
          if (equipEffect.IsExistAbilityLvMax)
          {
            flag = true;
            break;
          }
        }
      }
      return flag;
    }

    [DebuggerHidden]
    private IEnumerator CreateAbilityResultCroutine()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ConceptCardMaxPowerUp.\u003CCreateAbilityResultCroutine\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }
  }
}
