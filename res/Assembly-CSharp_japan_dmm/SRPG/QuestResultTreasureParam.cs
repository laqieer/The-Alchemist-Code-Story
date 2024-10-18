// Decompiled with JetBrains decompiler
// Type: SRPG.QuestResultTreasureParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public class QuestResultTreasureParam : ContentSource.Param
  {
    public QuestResult.DropItemData ItemData;
    public ArtifactParam ArtfactParam;
    public int ArtfactNum;
    public int GoldNum;
    public Texture2D GoldTex;
    public Sprite GoldFrame;
    public GameObject mSelectObj;
    public bool IsEndAnim;
    private QuestResultTreasureNode mNode;

    public override void OnEnable(ContentNode node)
    {
      base.OnEnable(node);
      this.mNode = node as QuestResultTreasureNode;
      this.Refresh();
    }

    public override void OnDisable(ContentNode node)
    {
      base.OnDisable(node);
      this.mNode = (QuestResultTreasureNode) null;
    }

    public void Refresh()
    {
      if (Object.op_Equality((Object) this.mNode, (Object) null))
        return;
      this.mNode.Setup(this);
    }
  }
}
