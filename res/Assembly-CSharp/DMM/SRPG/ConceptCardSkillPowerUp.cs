// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardSkillPowerUp
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(0, "タップ入力", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "全ページ表示終了", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "表示コンテンツなし", FlowNode.PinTypes.Output, 101)]
  public class ConceptCardSkillPowerUp : MonoBehaviour, IFlowInterface
  {
    private const int PIN_PAGE_NEXT = 0;
    private const int PIN_FINISHED = 100;
    private const int PIN_NO_CONTENTS = 101;
    [SerializeField]
    private Transform resultRoot;
    private SkillPowerUpResult skillPowerUpResult;

    public Transform ResultRoot => this.resultRoot;

    public void SetData(
      SkillPowerUpResult inSkillPowerUpResult,
      ConceptCardData currentCardData,
      int prevAwakeCount,
      int prevLevel)
    {
      this.skillPowerUpResult = inSkillPowerUpResult;
      this.skillPowerUpResult.SetData(currentCardData, prevAwakeCount, prevLevel, false);
      if (this.skillPowerUpResult.IsEnd)
      {
        ((Component) this.skillPowerUpResult).gameObject.SetActive(false);
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
      }
      else
        this.skillPowerUpResult.ApplyContent();
    }

    public void Activated(int pinID)
    {
      if (pinID != 0)
        return;
      this.CheckPages();
    }

    private void CheckPages()
    {
      if (this.skillPowerUpResult.IsEnd)
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
      else
        this.skillPowerUpResult.ApplyContent();
    }
  }
}
