// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardSlots
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class ConceptCardSlots : MonoBehaviour
  {
    private static readonly string CONCEPT_CARD_EQUIP_WINDOW_PREFAB_PATH = "UI/ConceptCardSelect";
    [SerializeField]
    private GenericSlot[] mConceptCardSlots;
    [SerializeField]
    private ConceptCardIcon[] mConceptCardIcons;
    [SerializeField]
    private GameObject mToolTipRoot;
    private UnitData mUnit;
    private bool mIsEditMode;
    private GameObject mConceptCardSelector;

    private void Start() => this.InitializeSlots();

    private void InitializeSlots()
    {
      for (int index = 0; index < this.mConceptCardSlots.Length; ++index)
      {
        if (!UnityEngine.Object.op_Equality((UnityEngine.Object) this.mConceptCardSlots[index], (UnityEngine.Object) null))
          this.mConceptCardSlots[index].OnSelect = new GenericSlot.SelectEvent(this.OnEquipCardSlot);
      }
    }

    private void OnDestroy() => GameUtility.DestroyGameObject(this.mConceptCardSelector);

    public void Refresh(bool editMode)
    {
      this.mUnit = DataSource.FindDataOfClass<UnitData>(((Component) ((Component) this).transform.parent).gameObject, (UnitData) null);
      if (this.mUnit == null)
      {
        for (int index = 0; index < this.mConceptCardSlots.Length; ++index)
          this.RefreshSlots(this.mConceptCardSlots[index], this.mConceptCardIcons[index], (ConceptCardData) null, true, false);
      }
      else
      {
        this.mIsEditMode = editMode;
        for (int index = 0; index < this.mConceptCardSlots.Length; ++index)
        {
          ConceptCardData conceptCardByIndex = this.mUnit.GetConceptCardByIndex(index);
          bool is_locked = !this.mUnit.IsUnlockConceptCardSlot(index);
          this.RefreshSlots(this.mConceptCardSlots[index], this.mConceptCardIcons[index], conceptCardByIndex, is_locked, editMode);
        }
      }
    }

    private void RefreshSlots(
      GenericSlot slot,
      ConceptCardIcon icon,
      ConceptCardData card,
      bool is_locked,
      bool editMode)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) slot, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) icon, (UnityEngine.Object) null))
        return;
      slot.SetLocked(is_locked);
      slot.SetSlotData<ConceptCardData>(card);
      icon.Setup(card);
      SRPG_Button componentInChildren = ((Component) slot).gameObject.GetComponentInChildren<SRPG_Button>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren, (UnityEngine.Object) null))
        return;
      bool flag = !is_locked;
      ((Selectable) componentInChildren).interactable = editMode && flag;
    }

    private void OnEquipCardSlot(GenericSlot slot, bool interactable)
    {
      if (!interactable || this.mUnit == null || this.mConceptCardSlots == null)
        return;
      int index = Array.FindIndex<GenericSlot>(this.mConceptCardSlots, (Predicate<GenericSlot>) (s => UnityEngine.Object.op_Equality((UnityEngine.Object) s, (UnityEngine.Object) slot)));
      if (index < 0 || this.mConceptCardSlots.Length <= index)
        return;
      GameObject gameObject = AssetManager.Load<GameObject>(ConceptCardSlots.CONCEPT_CARD_EQUIP_WINDOW_PREFAB_PATH);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
        return;
      this.mConceptCardSelector = UnityEngine.Object.Instantiate<GameObject>(gameObject);
      ConceptCardEquipWindow component = this.mConceptCardSelector.GetComponent<ConceptCardEquipWindow>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        return;
      component.Init(this.mUnit, index);
      this.mConceptCardSelector.AddComponent<DestroyEventListener>().Listeners += (DestroyEventListener.DestroyEvent) (go => this.OnCloseEquipConceptCardWindow());
    }

    private void OnCloseEquipConceptCardWindow()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mToolTipRoot, (UnityEngine.Object) null))
        return;
      if (UnitOverWriteUtility.IsNeedOverWrite((eOverWritePartyType) GlobalVars.OverWritePartyType))
      {
        UnitData data = UnitOverWriteUtility.Apply(DataSource.FindDataOfClass<UnitData>(this.mToolTipRoot, (UnitData) null), (eOverWritePartyType) GlobalVars.OverWritePartyType);
        data.CalcStatus();
        DataSource.Bind<UnitData>(this.mToolTipRoot, data);
      }
      else
      {
        this.mUnit = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(this.mUnit.UniqueID);
        UnitData dataOfClass = DataSource.FindDataOfClass<UnitData>(this.mToolTipRoot, (UnitData) null);
        dataOfClass.ConceptCards = this.mUnit.ConceptCards;
        dataOfClass.CalcStatus();
      }
      this.Refresh(this.mIsEditMode);
      GameParameter.UpdateAll(this.mToolTipRoot);
    }
  }
}
