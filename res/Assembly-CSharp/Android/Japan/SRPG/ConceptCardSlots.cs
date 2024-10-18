// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardSlots
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.Events;

namespace SRPG
{
  public class ConceptCardSlots : MonoBehaviour
  {
    private static readonly string CONCEPT_CARD_EQUIP_WINDOW_PREFAB_PATH = "UI/ConceptCardSelect";
    [SerializeField]
    private GenericSlot mConceptCardSlot;
    [SerializeField]
    private ConceptCardIcon mConceptCardIcon;
    [SerializeField]
    private GameObject mToolTipRoot;
    private UnitData mUnit;
    private bool mIsButtonEnable;

    public void Refresh(bool enable)
    {
      UnitData dataOfClass = DataSource.FindDataOfClass<UnitData>(this.transform.parent.gameObject, (UnitData) null);
      if (dataOfClass != null)
        this.mUnit = dataOfClass;
      if (this.mUnit == null)
        return;
      bool is_locked = false;
      this.mIsButtonEnable = enable;
      this.RefreshSlots(this.mConceptCardSlot, this.mConceptCardIcon, this.mUnit.ConceptCard, is_locked, this.mIsButtonEnable);
    }

    private void RefreshSlots(GenericSlot slot, ConceptCardIcon icon, ConceptCardData card, bool is_locked, bool enable)
    {
      if ((UnityEngine.Object) slot == (UnityEngine.Object) null || (UnityEngine.Object) icon == (UnityEngine.Object) null)
        return;
      slot.SetLocked(is_locked);
      slot.SetSlotData<ConceptCardData>(card);
      icon.Setup(card);
      SRPG_Button componentInChildren = slot.gameObject.GetComponentInChildren<SRPG_Button>();
      if (!((UnityEngine.Object) componentInChildren != (UnityEngine.Object) null))
        return;
      componentInChildren.enabled = enable;
      componentInChildren.onClick.RemoveAllListeners();
      componentInChildren.onClick.AddListener(new UnityAction(this.OnEquipCardSlot));
    }

    private void OnEquipCardSlot()
    {
      if (this.mUnit == null)
        return;
      GameObject original = AssetManager.Load<GameObject>(ConceptCardSlots.CONCEPT_CARD_EQUIP_WINDOW_PREFAB_PATH);
      if ((UnityEngine.Object) original == (UnityEngine.Object) null)
        return;
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original);
      ConceptCardEquipWindow component = gameObject.GetComponent<ConceptCardEquipWindow>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
        return;
      component.Init(this.mUnit);
      gameObject.AddComponent<DestroyEventListener>().Listeners += (DestroyEventListener.DestroyEvent) (go => this.OnCloseEquipConceptCardWindow());
    }

    private void OnCloseEquipConceptCardWindow()
    {
      if ((UnityEngine.Object) this.mToolTipRoot == (UnityEngine.Object) null)
        return;
      this.mUnit = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(this.mUnit.UniqueID);
      UnitData dataOfClass = DataSource.FindDataOfClass<UnitData>(this.mToolTipRoot, (UnitData) null);
      dataOfClass.ConceptCard = this.mUnit.ConceptCard;
      dataOfClass.CalcStatus();
      this.Refresh(this.mIsButtonEnable);
      GameParameter.UpdateAll(this.mToolTipRoot);
    }
  }
}
