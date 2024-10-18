// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardSkillPowerUp
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

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

    public Transform ResultRoot
    {
      get
      {
        return this.resultRoot;
      }
    }

    public void SetData(SkillPowerUpResult inSkillPowerUpResult, ConceptCardData currentCardData, int prevAwakeCount, int prevLevel)
    {
      this.skillPowerUpResult = inSkillPowerUpResult;
      this.skillPowerUpResult.SetData(currentCardData, prevAwakeCount, prevLevel, false);
      if (this.skillPowerUpResult.IsEnd)
      {
        this.skillPowerUpResult.gameObject.SetActive(false);
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
