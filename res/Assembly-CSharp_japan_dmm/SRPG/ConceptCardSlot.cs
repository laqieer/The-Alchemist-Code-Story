// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardSlot
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Linq;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class ConceptCardSlot : MonoBehaviour, IGameParameter
  {
    [SerializeField]
    private GameObject Disabled;
    [Space(10f)]
    [SerializeField]
    private GameObject OverlayImage;
    [SerializeField]
    private GameObject EnableGroupEffect;
    [SerializeField]
    private GameObject SettableIcon;
    private UnitData LeaderUnit;

    private void OnEnable() => this.UpdateValue();

    public void UpdateValue()
    {
      PartySlotData slotData = DataSource.FindDataOfClass<PartySlotData>(((Component) this).gameObject, (PartySlotData) null);
      ConceptCardData dataOfClass1 = DataSource.FindDataOfClass<ConceptCardData>(((Component) this).gameObject, (ConceptCardData) null);
      if (slotData == null)
        return;
      if (slotData.Type == PartySlotType.Locked)
      {
        if (slotData.IsSettable)
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.OverlayImage, (UnityEngine.Object) null))
            this.OverlayImage.SetActive(true);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Disabled, (UnityEngine.Object) null))
            this.Disabled.SetActive(false);
        }
        else
          this.SetSlotDisabled();
      }
      else if (slotData.Type == PartySlotType.Npc || slotData.Type == PartySlotType.NpcHero)
        this.SetSlotDisabled();
      else if ((slotData.Type == PartySlotType.Forced || slotData.Type == PartySlotType.ForcedHero) && !MonoSingleton<GameManager>.Instance.Player.Units.Any<UnitData>((Func<UnitData, bool>) (unit => unit.UnitID == slotData.UnitName)))
        this.SetSlotDisabled();
      else if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Disabled, (UnityEngine.Object) null))
      {
        this.Disabled.SetActive(false);
        UnitData dataOfClass2 = DataSource.FindDataOfClass<UnitData>(((Component) this).gameObject, (UnitData) null);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SettableIcon, (UnityEngine.Object) null))
          this.SettableIcon.SetActive(dataOfClass2 != null && dataOfClass1 == null && slotData.Index != PartySlotIndex.Support);
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.EnableGroupEffect, (UnityEngine.Object) null))
        return;
      bool flag = false;
      if (dataOfClass1 != null)
      {
        UnitData unit = this.LeaderUnit;
        if (unit == null)
        {
          unit = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID((long) GlobalVars.SelectedLSChangeUnitUniqueID);
          if (UnitOverWriteUtility.IsNeedOverWrite((eOverWritePartyType) GlobalVars.OverWritePartyType))
            unit = UnitOverWriteUtility.Apply(unit, (eOverWritePartyType) GlobalVars.OverWritePartyType);
        }
        if (unit != null && unit.IsEquipConceptLeaderSkill() && unit.MainConceptCard != null && unit.MainConceptCard.Param.concept_card_groups != null)
        {
          for (int index = 0; index < unit.MainConceptCard.Param.concept_card_groups.Length; ++index)
            flag |= MonoSingleton<GameManager>.Instance.MasterParam.CheckConceptCardgroup(unit.MainConceptCard.Param.concept_card_groups[index], dataOfClass1.Param);
        }
      }
      this.EnableGroupEffect.SetActive(false);
      this.EnableGroupEffect.SetActive(flag);
    }

    private void SetSlotDisabled()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Disabled, (UnityEngine.Object) null))
        return;
      this.Disabled.SetActive(true);
    }

    public void SetLeaderUnit(UnitData leader) => this.LeaderUnit = leader;
  }
}
