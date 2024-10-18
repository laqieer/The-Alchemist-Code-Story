// Decompiled with JetBrains decompiler
// Type: SRPG.SupportIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class SupportIcon : UnitIcon
  {
    [SerializeField]
    private GameObject ConceptCardLSEnableEffect;
    private const string TooltipPath = "UI/SupportTooltip";
    public bool UseSelection;

    public override void UpdateValue()
    {
      base.UpdateValue();
      bool flag1 = false;
      UnitData unitDataByUniqueId = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID((long) GlobalVars.SelectedLSChangeUnitUniqueID);
      SupportData supportData = this.GetSupportData();
      ConceptCardData mainConceptCard1 = unitDataByUniqueId == null ? (ConceptCardData) null : unitDataByUniqueId.MainConceptCard;
      ConceptCardData mainConceptCard2 = supportData == null || supportData.Unit == null ? (ConceptCardData) null : supportData.Unit.MainConceptCard;
      if (unitDataByUniqueId != null && mainConceptCard1 != null && unitDataByUniqueId.IsEquipConceptLeaderSkill() && mainConceptCard1.Param.concept_card_groups != null && supportData != null && supportData.Unit != null && supportData.IsFriend() && mainConceptCard2 != null)
      {
        bool flag2 = false;
        for (int index = 0; index < mainConceptCard1.Param.concept_card_groups.Length; ++index)
          flag2 |= MonoSingleton<GameManager>.Instance.MasterParam.CheckConceptCardgroup(mainConceptCard1.Param.concept_card_groups[index], mainConceptCard2.Param);
        if (flag2)
          flag1 = true;
      }
      if (!Object.op_Inequality((Object) this.ConceptCardLSEnableEffect, (Object) null))
        return;
      this.ConceptCardLSEnableEffect.SetActive(flag1);
    }

    private SupportData GetSupportData()
    {
      return this.UseSelection ? (SupportData) GlobalVars.SelectedSupport : DataSource.FindDataOfClass<SupportData>(((Component) this).gameObject, (SupportData) null);
    }

    protected override UnitData GetInstanceData()
    {
      SupportData supportData = this.GetSupportData();
      return supportData == null || supportData.Unit == null ? (UnitData) null : supportData.Unit;
    }

    protected override void ShowTooltip(Vector2 screen)
    {
      if (!this.Tooltip)
        return;
      SupportData supportData = this.GetSupportData();
      if (supportData == null || supportData.Unit == null)
        return;
      GameObject gameObject = Object.Instantiate<GameObject>(AssetManager.Load<GameObject>("UI/SupportTooltip"));
      DataSource.Bind<UnitData>(gameObject, supportData.Unit);
      DataSource.Bind<SupportData>(gameObject, supportData);
    }
  }
}
