// Decompiled with JetBrains decompiler
// Type: SRPG.ArtifactIconParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public class ArtifactIconParam : ContentSource.Param
  {
    public ArtifactData Data;
    public ArtifactParam Param;
    public bool Select;
    public bool Enable = true;
    public bool IsRecommend;
    public bool IsEmpty;
    private ArtifactIconNode mNode;

    public override void OnEnable(ContentNode node)
    {
      base.OnEnable(node);
      this.mNode = node as ArtifactIconNode;
      this.Refresh();
    }

    public override void OnDisable(ContentNode node)
    {
      this.mNode = (ArtifactIconNode) null;
      base.OnDisable(node);
    }

    public void Refresh()
    {
      if (Object.op_Equality((Object) this.mNode, (Object) null))
        return;
      this.mNode.Empty(this.IsEmpty);
      if (this.IsEmpty)
        return;
      this.mNode.Select(this.Select);
      this.mNode.Enable(this.Enable);
      this.mNode.Recommend(this.IsRecommend);
      if (this.Data == null && this.Param != null)
        this.mNode.Setup(this.Param);
      else
        this.mNode.Setup(this.Data);
    }
  }
}
