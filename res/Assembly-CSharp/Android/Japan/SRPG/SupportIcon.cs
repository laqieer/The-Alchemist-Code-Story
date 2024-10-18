// Decompiled with JetBrains decompiler
// Type: SRPG.SupportIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class SupportIcon : UnitIcon
  {
    private const string TooltipPath = "UI/SupportTooltip";
    public bool UseSelection;

    private SupportData GetSupportData()
    {
      if (this.UseSelection)
        return (SupportData) GlobalVars.SelectedSupport;
      return DataSource.FindDataOfClass<SupportData>(this.gameObject, (SupportData) null);
    }

    protected override UnitData GetInstanceData()
    {
      SupportData supportData = this.GetSupportData();
      if (supportData == null || supportData.Unit == null)
        return (UnitData) null;
      return supportData.Unit;
    }

    protected override void ShowTooltip(Vector2 screen)
    {
      if (!this.Tooltip)
        return;
      SupportData supportData = this.GetSupportData();
      if (supportData == null || supportData.Unit == null)
        return;
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(AssetManager.Load<GameObject>("UI/SupportTooltip"));
      DataSource.Bind<UnitData>(gameObject, supportData.Unit, false);
      DataSource.Bind<SupportData>(gameObject, supportData, false);
    }
  }
}
