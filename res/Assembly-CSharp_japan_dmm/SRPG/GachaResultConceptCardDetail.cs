// Decompiled with JetBrains decompiler
// Type: SRPG.GachaResultConceptCardDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class GachaResultConceptCardDetail : MonoBehaviour, IFlowInterface
  {
    [SerializeField]
    private GameObject Icon;
    [SerializeField]
    private Text ExprText;
    [SerializeField]
    private Text FlavorText;
    [SerializeField]
    private Text NameText;
    [SerializeField]
    private ScrollRect ScrollParent;
    [SerializeField]
    private Transform FloavorArea;
    private ConceptCardData m_Data;
    private float mDecelerationRate;

    public void Activated(int pinID)
    {
    }

    private void Refresh()
    {
      if (this.m_Data == null)
      {
        DebugUtility.LogError("真理念装のデータがセットされていません");
      }
      else
      {
        ConceptCardIcon component = this.Icon.GetComponent<ConceptCardIcon>();
        if (Object.op_Inequality((Object) component, (Object) null))
          component.Setup(this.m_Data);
        if (Object.op_Inequality((Object) this.NameText, (Object) null))
          this.NameText.text = this.m_Data.Param.name;
        if (Object.op_Inequality((Object) this.ExprText, (Object) null))
          this.ExprText.text = this.m_Data.Param.expr;
        if (!Object.op_Inequality((Object) this.FlavorText, (Object) null))
          return;
        this.FlavorText.text = this.m_Data.Param.GetLocalizedTextFlavor();
      }
    }

    public void Setup(ConceptCardData _data)
    {
      this.m_Data = _data;
      this.Refresh();
    }

    private void ResetScrollPosition()
    {
      if (Object.op_Equality((Object) this.ScrollParent, (Object) null))
        return;
      this.mDecelerationRate = this.ScrollParent.decelerationRate;
      this.ScrollParent.decelerationRate = 0.0f;
      RectTransform floavorArea = this.FloavorArea as RectTransform;
      floavorArea.anchoredPosition = new Vector2(floavorArea.anchoredPosition.x, 0.0f);
      this.StartCoroutine(this.RefreshScrollRect());
    }

    [DebuggerHidden]
    private IEnumerator RefreshScrollRect()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GachaResultConceptCardDetail.\u003CRefreshScrollRect\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }
  }
}
