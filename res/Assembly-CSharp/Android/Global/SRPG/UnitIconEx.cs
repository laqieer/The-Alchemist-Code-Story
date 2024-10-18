// Decompiled with JetBrains decompiler
// Type: SRPG.UnitIconEx
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class UnitIconEx : UnitIcon
  {
    private const string DefaultTootTipPath = "UI/UnitTooltip.prefab";
    public string GeneralTooltipPath;

    protected override void ShowTooltip(Vector2 screen)
    {
      if (!this.Tooltip)
        return;
      UnitData instanceData = this.GetInstanceData();
      if (instanceData == null)
        return;
      GameObject root = UnityEngine.Object.Instantiate<GameObject>(AssetManager.Load<GameObject>(!string.IsNullOrEmpty(this.GeneralTooltipPath) ? this.GeneralTooltipPath : "UI/UnitTooltip.prefab"));
      DataSource.Bind<UnitData>(root, instanceData);
      GameParameter.UpdateAll(root);
    }
  }
}
