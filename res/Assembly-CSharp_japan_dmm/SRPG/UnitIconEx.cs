// Decompiled with JetBrains decompiler
// Type: SRPG.UnitIconEx
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
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
      GameObject gameObject = Object.Instantiate<GameObject>(AssetManager.Load<GameObject>(!string.IsNullOrEmpty(this.GeneralTooltipPath) ? this.GeneralTooltipPath : "UI/UnitTooltip.prefab"));
      this.BindData(gameObject, instanceData);
      GameParameter.UpdateAll(gameObject);
    }

    private void BindData(GameObject go, UnitData unitData)
    {
      PlayerPartyTypes dataOfClass = DataSource.FindDataOfClass<PlayerPartyTypes>(go, PlayerPartyTypes.Max);
      DataSource.Bind<UnitData>(go, unitData);
      DataSource.Bind<PlayerPartyTypes>(go, dataOfClass);
      UnitJobDropdown componentInChildren1 = go.GetComponentInChildren<UnitJobDropdown>();
      if (Object.op_Inequality((Object) componentInChildren1, (Object) null))
      {
        bool flag = (unitData.TempFlags & UnitData.TemporaryFlags.AllowJobChange) != (UnitData.TemporaryFlags) 0 && this.AllowJobChange && dataOfClass != PlayerPartyTypes.Max;
        ((Component) componentInChildren1).gameObject.SetActive(true);
        componentInChildren1.UpdateValue = (UnitJobDropdown.ParentObjectEvent) null;
        Selectable component1 = ((Component) componentInChildren1).gameObject.GetComponent<Selectable>();
        if (Object.op_Inequality((Object) component1, (Object) null))
          component1.interactable = flag;
        Image component2 = ((Component) componentInChildren1).gameObject.GetComponent<Image>();
        if (Object.op_Inequality((Object) component2, (Object) null))
          ((Graphic) component2).color = !flag ? new Color(0.5f, 0.5f, 0.5f) : Color.white;
      }
      ArtifactSlots componentInChildren2 = go.GetComponentInChildren<ArtifactSlots>();
      AbilitySlots componentInChildren3 = go.GetComponentInChildren<AbilitySlots>();
      if (Object.op_Inequality((Object) componentInChildren2, (Object) null) && Object.op_Inequality((Object) componentInChildren3, (Object) null))
      {
        bool enable = (unitData.TempFlags & UnitData.TemporaryFlags.AllowJobChange) != (UnitData.TemporaryFlags) 0 && this.AllowJobChange && dataOfClass != PlayerPartyTypes.Max;
        componentInChildren2.Refresh(enable);
        componentInChildren3.Refresh(enable);
      }
      ConceptCardSlots componentInChildren4 = go.GetComponentInChildren<ConceptCardSlots>();
      if (!Object.op_Inequality((Object) componentInChildren4, (Object) null))
        return;
      bool editMode = (unitData.TempFlags & UnitData.TemporaryFlags.AllowJobChange) != (UnitData.TemporaryFlags) 0 && this.AllowJobChange && dataOfClass != PlayerPartyTypes.Max;
      componentInChildren4.Refresh(editMode);
    }
  }
}
