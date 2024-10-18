// Decompiled with JetBrains decompiler
// Type: SRPG.GachaResultConceptCardDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

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
        if ((UnityEngine.Object) component != (UnityEngine.Object) null)
          component.Setup(this.m_Data);
        if ((UnityEngine.Object) this.NameText != (UnityEngine.Object) null)
          this.NameText.text = this.m_Data.Param.name;
        if ((UnityEngine.Object) this.ExprText != (UnityEngine.Object) null)
          this.ExprText.text = this.m_Data.Param.expr;
        if (!((UnityEngine.Object) this.FlavorText != (UnityEngine.Object) null))
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
      if ((UnityEngine.Object) this.ScrollParent == (UnityEngine.Object) null)
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
      return (IEnumerator) new GachaResultConceptCardDetail.\u003CRefreshScrollRect\u003Ec__Iterator0() { \u0024this = this };
    }
  }
}
