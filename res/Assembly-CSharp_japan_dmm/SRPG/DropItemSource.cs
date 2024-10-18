// Decompiled with JetBrains decompiler
// Type: SRPG.DropItemSource
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public class DropItemSource : ContentSource
  {
    public override void Initialize(ContentController controller) => base.Initialize(controller);

    public override void Release() => base.Release();

    public class DropItemParam : ContentSource.Param
    {
      private QuestResult.DropItemData mDropItem;

      public DropItemParam(QuestResult.DropItemData drop_item) => this.mDropItem = drop_item;

      public override bool IsValid() => this.mDropItem != null;

      public override void OnEnable(ContentNode node)
      {
        base.OnEnable(node);
        DropItemNode dropItemNode = node as DropItemNode;
        DataSource.Bind<QuestResult.DropItemData>(((Component) dropItemNode.DropItemIcon).gameObject, this.mDropItem, true);
        dropItemNode.DropItemIcon.UpdateValue();
      }
    }
  }
}
