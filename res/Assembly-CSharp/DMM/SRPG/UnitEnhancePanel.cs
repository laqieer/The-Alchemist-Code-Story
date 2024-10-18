// Decompiled with JetBrains decompiler
// Type: SRPG.UnitEnhancePanel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class UnitEnhancePanel : MonoBehaviour
  {
    public UnitEquipmentSlotEvents[] EquipmentSlots;
    public SRPG_Button JobRankUpButton;
    public SRPG_Button JobUnlockButton;
    public SRPG_Button EquipButton;
    public SRPG_Button AllEquipButton;
    public GameObject JobRankCapCaution;
    public SRPG_Button JobRankupAllIn;
    [Space(10f)]
    public GenericSlot ArtifactSlot;
    [Space(10f)]
    public GenericSlot ArtifactSlot2;
    [Space(10f)]
    public GenericSlot ArtifactSlot3;
    [Space(10f)]
    public RectTransform ExpItemList;
    public ListItemEvents ExpItemTemplate;
    public SRPG_Button UnitLevelupButton;
    [Space(10f)]
    public UnitAbilityList AbilityList;
    [Space(10f)]
    public UnitAbilityList AbilitySlots;
    [Space(10f)]
    public UnitEnhancePanel.ConceptCardSlotObjects[] mConceptCardSlots;

    private void Awake()
    {
      Canvas component = ((Component) this).GetComponent<Canvas>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        return;
      ((Behaviour) component).enabled = false;
    }

    private void Start()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ExpItemTemplate, (UnityEngine.Object) null))
        return;
      ((Component) this.ExpItemTemplate).gameObject.SetActive(false);
    }

    public void SetConceptCardOnSelect(GenericSlot.SelectEvent ev)
    {
      if (this.mConceptCardSlots == null)
        return;
      for (int index = 0; index < this.mConceptCardSlots.Length; ++index)
      {
        if (!UnityEngine.Object.op_Equality((UnityEngine.Object) this.mConceptCardSlots[index].m_ConceptCardSlot, (UnityEngine.Object) null))
        {
          this.mConceptCardSlots[index].m_ConceptCardSlot.slotIndex = index;
          this.mConceptCardSlots[index].m_ConceptCardSlot.OnSelect = ev;
        }
      }
    }

    [Serializable]
    public struct ConceptCardSlotObjects
    {
      [LabelPropertyName("Slot")]
      public EquipConceptCardSlot m_ConceptCardSlot;
      [LabelPropertyName("Icon")]
      public ConceptCardIcon m_EquipConceptCardIcon;
    }
  }
}
