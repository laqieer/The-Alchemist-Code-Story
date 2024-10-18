// Decompiled with JetBrains decompiler
// Type: SRPG.DropItemIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class DropItemIcon : ItemIcon
  {
    protected override void ShowTooltip(Vector2 screen)
    {
      RectTransform transform = this.transform as RectTransform;
      Tooltip.TooltipPosition = screen + Vector2.up * transform.rect.height * 0.5f;
      Tooltip original = AssetManager.Load<Tooltip>("UI/ItemTooltip");
      if (!((UnityEngine.Object) original != (UnityEngine.Object) null))
        return;
      Tooltip tooltip = UnityEngine.Object.Instantiate<Tooltip>(original);
      ItemParam itemParam;
      int itemNum;
      this.InstanceType.GetInstanceData(this.InstanceIndex, this.gameObject, out itemParam, out itemNum);
      DataSource.Bind<ItemParam>(tooltip.gameObject, itemParam);
      CanvasStack component = tooltip.GetComponent<CanvasStack>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
        return;
      component.SystemModal = true;
      component.Priority = 1;
    }
  }
}
