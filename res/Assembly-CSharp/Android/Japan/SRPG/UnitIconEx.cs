// Decompiled with JetBrains decompiler
// Type: SRPG.UnitIconEx
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class UnitIconEx : UnitIcon
  {
    public string GeneralTooltipPath;
    private const string DefaultTootTipPath = "UI/UnitTooltip.prefab";

    protected override void ShowTooltip(Vector2 screen)
    {
      if (!this.Tooltip)
        return;
      UnitData instanceData = this.GetInstanceData();
      if (instanceData == null)
        return;
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(AssetManager.Load<GameObject>(!string.IsNullOrEmpty(this.GeneralTooltipPath) ? this.GeneralTooltipPath : "UI/UnitTooltip.prefab"));
      this.BindData(gameObject, instanceData);
      GameParameter.UpdateAll(gameObject);
    }

    private void BindData(GameObject go, UnitData unitData)
    {
      PlayerPartyTypes dataOfClass = DataSource.FindDataOfClass<PlayerPartyTypes>(go, PlayerPartyTypes.Max);
      DataSource.Bind<UnitData>(go, unitData, false);
      DataSource.Bind<PlayerPartyTypes>(go, dataOfClass, false);
      UnitJobDropdown componentInChildren1 = go.GetComponentInChildren<UnitJobDropdown>();
      if ((UnityEngine.Object) componentInChildren1 != (UnityEngine.Object) null)
      {
        bool flag = (unitData.TempFlags & UnitData.TemporaryFlags.AllowJobChange) != (UnitData.TemporaryFlags) 0 && this.AllowJobChange && dataOfClass != PlayerPartyTypes.Max;
        componentInChildren1.gameObject.SetActive(true);
        componentInChildren1.UpdateValue = (UnitJobDropdown.ParentObjectEvent) null;
        Selectable component1 = componentInChildren1.gameObject.GetComponent<Selectable>();
        if ((UnityEngine.Object) component1 != (UnityEngine.Object) null)
          component1.interactable = flag;
        Image component2 = componentInChildren1.gameObject.GetComponent<Image>();
        if ((UnityEngine.Object) component2 != (UnityEngine.Object) null)
          component2.color = !flag ? new Color(0.5f, 0.5f, 0.5f) : Color.white;
      }
      ArtifactSlots componentInChildren2 = go.GetComponentInChildren<ArtifactSlots>();
      AbilitySlots componentInChildren3 = go.GetComponentInChildren<AbilitySlots>();
      if ((UnityEngine.Object) componentInChildren2 != (UnityEngine.Object) null && (UnityEngine.Object) componentInChildren3 != (UnityEngine.Object) null)
      {
        bool enable = (unitData.TempFlags & UnitData.TemporaryFlags.AllowJobChange) != (UnitData.TemporaryFlags) 0 && this.AllowJobChange && dataOfClass != PlayerPartyTypes.Max;
        componentInChildren2.Refresh(enable);
        componentInChildren3.Refresh(enable);
      }
      ConceptCardSlots componentInChildren4 = go.GetComponentInChildren<ConceptCardSlots>();
      if (!((UnityEngine.Object) componentInChildren4 != (UnityEngine.Object) null))
        return;
      bool enable1 = (unitData.TempFlags & UnitData.TemporaryFlags.AllowJobChange) != (UnitData.TemporaryFlags) 0 && this.AllowJobChange && dataOfClass != PlayerPartyTypes.Max;
      componentInChildren4.Refresh(enable1);
    }
  }
}
