// Decompiled with JetBrains decompiler
// Type: SRPG.SupportElementList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class SupportElementList : MonoBehaviour
  {
    [StringIsResourcePath(typeof (GameObject))]
    [SerializeField]
    private string CARDLIST_WINDOW_PATH = "UI/ConceptCardSelect";
    [SerializeField]
    private GameObject[] m_SupportUnits;
    [SerializeField]
    private GenericSlot[] mConceptCards;
    private GameObject mCardSelectWindow;

    private void Awake()
    {
      for (int index = 0; index < this.mConceptCards.Length; ++index)
      {
        if (!UnityEngine.Object.op_Equality((UnityEngine.Object) this.mConceptCards[index], (UnityEngine.Object) null))
        {
          PartySlotData data = new PartySlotData(PartySlotType.Free, (string) null, PartySlotIndex.Main1);
          this.mConceptCards[index].SetSlotData<PartySlotData>(data);
        }
      }
    }

    public void Clear()
    {
      if (this.m_SupportUnits == null)
      {
        DebugUtility.LogError("m_SupportUnitsがnullです。");
      }
      else
      {
        for (int element = 0; element < this.m_SupportUnits.Length; ++element)
          this.Refresh(element, (UnitData) null);
      }
    }

    public void Refresh(int element, UnitData unit)
    {
      if (this.m_SupportUnits == null)
        DebugUtility.LogError("m_SupportUnitsがnullです。");
      else if (this.m_SupportUnits.Length < Enum.GetValues(typeof (EElement)).Length)
        DebugUtility.LogError("m_SupportUnitsの数が足りません。Inspectorからの設定を確認してください。");
      else if (element >= this.m_SupportUnits.Length)
      {
        DebugUtility.LogError("unitsの数が足りません。");
      }
      else
      {
        if (unit != null)
        {
          UnitData unit1 = new UnitData();
          unit1.Setup(unit);
          unit1.TempFlags |= UnitData.TemporaryFlags.TemporaryUnitData | UnitData.TemporaryFlags.AllowJobChange;
          eOverWritePartyType party_type = UnitOverWriteUtility.Element2OverWritePartyType((EElement) element);
          unit = UnitOverWriteUtility.Apply(unit1, party_type);
        }
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_SupportUnits[element], (UnityEngine.Object) null))
          return;
        GameObject gameObject = this.m_SupportUnits[element].gameObject;
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
          return;
        SerializeValueBehaviour component = gameObject.GetComponent<SerializeValueBehaviour>();
        DataSource dataSource = DataSource.Create(gameObject);
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) dataSource, (UnityEngine.Object) null))
          return;
        dataSource.Clear();
        if (unit != null)
        {
          dataSource.Add(typeof (UnitData), (object) unit);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          {
            component.list.SetInteractable("btn", true);
            component.list.SetActive(1, true);
          }
        }
        else if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        {
          component.list.SetInteractable("btn", false);
          component.list.SetActive(1, false);
        }
        ConceptCardData mainConceptCard = unit == null ? (ConceptCardData) null : unit.MainConceptCard;
        ConceptCardIcon componentInChildren = gameObject.GetComponentInChildren<ConceptCardIcon>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren, (UnityEngine.Object) null))
          componentInChildren.Setup(mainConceptCard);
        if (element <= this.mConceptCards.Length - 1)
          this.mConceptCards[element].OnSelect = new GenericSlot.SelectEvent(this.OnCardSlotClick);
        eOverWritePartyType data = UnitOverWriteUtility.Element2OverWritePartyType((EElement) element);
        DataSource.Bind<eOverWritePartyType>(gameObject, data);
        GameParameter.UpdateAll(gameObject);
      }
    }

    public void Refresh(long[] uniqs)
    {
      if (this.m_SupportUnits == null)
        DebugUtility.LogError("m_SupportUnitsがnullです。");
      else if (uniqs == null)
        DebugUtility.LogError("unitsがnullです。");
      else if (this.m_SupportUnits.Length < Enum.GetValues(typeof (EElement)).Length)
        DebugUtility.LogError("m_SupportUnitsの数が足りません。Inspectorからの設定を確認してください。");
      else if (uniqs.Length < this.m_SupportUnits.Length)
      {
        DebugUtility.LogError("unitsの数が足りません。");
      }
      else
      {
        for (int element = 0; element < uniqs.Length; ++element)
          this.Refresh(element, MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(uniqs[element]));
        GameParameter.UpdateAll(((Component) this).gameObject);
      }
    }

    public void Refresh(UnitData[] units)
    {
      if (this.m_SupportUnits == null)
        DebugUtility.LogError("m_SupportUnitsがnullです。");
      else if (units == null)
        DebugUtility.LogError("unitsがnullです。");
      else if (this.m_SupportUnits.Length < Enum.GetValues(typeof (EElement)).Length)
        DebugUtility.LogError("m_SupportUnitsの数が足りません。Inspectorからの設定を確認してください。");
      else if (units.Length < this.m_SupportUnits.Length)
      {
        DebugUtility.LogError("unitsの数が足りません。");
      }
      else
      {
        for (int element = 0; element < units.Length; ++element)
          this.Refresh(element, units[element]);
        GameParameter.UpdateAll(((Component) this).gameObject);
      }
    }

    private void OnCardSlotClick(GenericSlot slot, bool interactable)
    {
      int element_index = Array.IndexOf<GenericSlot>(this.mConceptCards, slot);
      UnitData unit_data = DataSource.FindDataOfClass<UnitData>(((Component) slot).gameObject, (UnitData) null);
      if (unit_data == null)
        return;
      GameObject gameObject = AssetManager.Load<GameObject>(this.CARDLIST_WINDOW_PATH);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
        return;
      this.mCardSelectWindow = UnityEngine.Object.Instantiate<GameObject>(gameObject);
      ConceptCardEquipWindow component = this.mCardSelectWindow.GetComponent<ConceptCardEquipWindow>();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) component, (UnityEngine.Object) null))
        return;
      eOverWritePartyType overWritePartyType = UnitOverWriteUtility.Element2OverWritePartyType((EElement) element_index);
      GlobalVars.OverWritePartyType.Set(overWritePartyType);
      component.Init(unit_data, 0);
      component.OnChangeAction = (Action) (() => this.mCardSelectWindow.AddComponent<DestroyEventListener>().Listeners += (DestroyEventListener.DestroyEvent) (go =>
      {
        GlobalVars.OverWritePartyType.Set(eOverWritePartyType.None);
        this.Refresh(element_index, unit_data);
      }));
    }
  }
}
