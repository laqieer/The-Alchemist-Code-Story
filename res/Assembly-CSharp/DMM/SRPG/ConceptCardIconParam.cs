// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardIconParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public class ConceptCardIconParam : ContentSource.Param
  {
    public ConceptCardData ConceptCard;
    public bool Select;
    public bool Enable = true;
    public bool IsRecommend;
    public bool IsEmpty;
    public bool IsEnableDecreaseEffect;
    public int DecreaseEffectRate;
    private ConceptCardIconNode mNode;

    public override void OnEnable(ContentNode node)
    {
      base.OnEnable(node);
      this.mNode = node as ConceptCardIconNode;
      this.Refresh();
    }

    public override void OnDisable(ContentNode node)
    {
      this.mNode = (ConceptCardIconNode) null;
      base.OnDisable(node);
    }

    public void Refresh()
    {
      if (Object.op_Equality((Object) this.mNode, (Object) null))
        return;
      this.mNode.Setup(this.ConceptCard);
      this.mNode.Empty(this.IsEmpty);
      this.mNode.Select(this.Select);
      this.mNode.Enable(this.Enable);
      this.mNode.SetNotSellFlag(this.ConceptCard != null && this.ConceptCard.Param.not_sale);
      this.mNode.Recommend(this.IsRecommend);
      this.mNode.SetDecreaseEffectActive(this.IsEnableDecreaseEffect, this.DecreaseEffectRate);
    }
  }
}
