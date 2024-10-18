// Decompiled with JetBrains decompiler
// Type: SRPG.AutoEquipConceptCard
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(0, "初期化", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(11, "装備する準備開始", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(101, "初期化完了", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(111, "詳細表示", FlowNode.PinTypes.Output, 111)]
  [FlowNode.Pin(121, "装備する準備終了", FlowNode.PinTypes.Output, 121)]
  public class AutoEquipConceptCard : MonoBehaviour, IFlowInterface
  {
    private const int PIN_IN_INIT = 0;
    private const int PIN_IN_EQUIP = 11;
    private const int PIN_OUT_INIT = 101;
    private const int PIN_OUT_SHOW_DETAIL = 111;
    private const int PIN_OUT_EQUIP = 121;
    [SerializeField]
    private ConceptCardIcon IconTemplate;
    [SerializeField]
    private GameObject IconParent;
    private ConceptCardData mCurrentConceptCard;

    private bool Init()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) ConceptCardEquipWindow.Instance, (UnityEngine.Object) null) || ConceptCardEquipWindow.Instance.SelectedUnit == null || UnityEngine.Object.op_Equality((UnityEngine.Object) this.IconTemplate, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.IconParent, (UnityEngine.Object) null))
        return false;
      GameUtility.SetGameObjectActive((Component) this.IconTemplate, false);
      UnitData selectedUnit = ConceptCardEquipWindow.Instance.SelectedUnit;
      int selectedCardIndex = ConceptCardEquipWindow.Instance.SelectedCardIndex;
      List<ConceptCardData> equipConceptCard = AutoEquipConceptCard.CreateAutoEquipConceptCard(selectedUnit, selectedCardIndex);
      long[] equip_card_iids = new long[equipConceptCard.Count];
      for (int index = 0; index < equipConceptCard.Count; ++index)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        AutoEquipConceptCard.\u003CInit\u003Ec__AnonStorey0 initCAnonStorey0 = new AutoEquipConceptCard.\u003CInit\u003Ec__AnonStorey0();
        // ISSUE: reference to a compiler-generated field
        initCAnonStorey0.\u0024this = this;
        ConceptCardData conceptCardData = equipConceptCard[index];
        if (conceptCardData != null && selectedCardIndex == index)
        {
          // ISSUE: reference to a compiler-generated field
          initCAnonStorey0.icon = this.CreateConceptCardIcon(conceptCardData);
          // ISSUE: reference to a compiler-generated field
          if (!UnityEngine.Object.op_Equality((UnityEngine.Object) initCAnonStorey0.icon, (UnityEngine.Object) null))
          {
            // ISSUE: reference to a compiler-generated field
            Button component = ((Component) initCAnonStorey0.icon).GetComponent<Button>();
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
            {
              // ISSUE: method pointer
              ((UnityEvent) component.onClick).AddListener(new UnityAction((object) initCAnonStorey0, __methodptr(\u003C\u003Em__0)));
            }
          }
          else
            break;
        }
      }
      for (int index = 0; index < equipConceptCard.Count; ++index)
      {
        ConceptCardData conceptCardData = equipConceptCard[index];
        if (conceptCardData != null)
          equip_card_iids[index] = (long) conceptCardData.UniqueID;
      }
      ((Component) this).GetComponent<FlowNode_ReqConceptCardSet>().SetEquipParam(equip_card_iids, selectedUnit.UniqueID);
      GlobalVars.SelectedConceptCardData.Set(this.mCurrentConceptCard);
      return true;
    }

    public void Activated(int PinID)
    {
      if (PinID != 0)
      {
        if (PinID != 11)
          return;
        this.InvokeConceptCardEquipWindow_OnChangeAction();
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 121);
      }
      else
      {
        this.Init();
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
      }
    }

    private void InvokeConceptCardEquipWindow_OnChangeAction()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) ConceptCardEquipWindow.Instance, (UnityEngine.Object) null) || ConceptCardEquipWindow.Instance.OnChangeAction == null)
        return;
      ConceptCardEquipWindow.Instance.OnChangeAction();
    }

    public static List<ConceptCardData> CreateAutoEquipConceptCard(
      UnitData unit,
      int targetSlotIndex = -1,
      ConceptCardData[] ignorelist = null)
    {
      if (unit == null)
        return (List<ConceptCardData>) null;
      List<ConceptCardData> concept_cards = new List<ConceptCardData>((IEnumerable<ConceptCardData>) MonoSingleton<GameManager>.Instance.Player.ConceptCards);
      List<string> stringList = (List<string>) null;
      if (targetSlotIndex != -1)
      {
        stringList = new List<string>();
        for (int index = 0; index < 2; ++index)
        {
          ConceptCardData conceptCardByIndex = unit.GetConceptCardByIndex(index);
          if (conceptCardByIndex != null && index != targetSlotIndex)
            stringList.Add(conceptCardByIndex.Param.iname);
        }
      }
      for (int i = 0; i < concept_cards.Count; ++i)
      {
        UnitData owner = concept_cards[i].GetOwner();
        if (owner != null && owner.UniqueID != unit.UniqueID)
        {
          List<ConceptCardData> conceptCardDataList = concept_cards;
          int num;
          i = (num = i) - 1;
          int index = num;
          conceptCardDataList.RemoveAt(index);
        }
        else if (ignorelist != null && ignorelist.Length > 0 && Array.Find<ConceptCardData>(ignorelist, (Predicate<ConceptCardData>) (c => (long) c.UniqueID == (long) concept_cards[i].UniqueID)) != null)
        {
          List<ConceptCardData> conceptCardDataList = concept_cards;
          int num;
          i = (num = i) - 1;
          int index = num;
          conceptCardDataList.RemoveAt(index);
        }
        else if (stringList != null && stringList.FindIndex((Predicate<string>) (iname => iname == concept_cards[i].Param.iname)) != -1)
        {
          List<ConceptCardData> conceptCardDataList = concept_cards;
          int num;
          i = (num = i) - 1;
          int index = num;
          conceptCardDataList.RemoveAt(index);
        }
      }
      if (concept_cards.Count <= 0)
        return (List<ConceptCardData>) null;
      if (concept_cards.Count > 1)
        concept_cards.Sort((Comparison<ConceptCardData>) ((a, b) => AutoEquipConceptCard.CompareConceptCardData(unit, a, b)));
      int index1 = 0;
      List<ConceptCardData> conceptCardDataList1 = new List<ConceptCardData>();
      List<ConceptCardData> equipConceptCard;
      if (targetSlotIndex == -1)
      {
        equipConceptCard = new List<ConceptCardData>();
        for (int index2 = 0; index2 < 2; ++index2)
          equipConceptCard.Add((ConceptCardData) null);
        for (int i = 0; i < concept_cards.Count && index1 < 2; ++i)
        {
          if (equipConceptCard.FindIndex((Predicate<ConceptCardData>) (card => card != null && card.Param.iname == concept_cards[i].Param.iname)) == -1)
          {
            equipConceptCard[index1] = concept_cards[i];
            ++index1;
          }
        }
      }
      else
      {
        equipConceptCard = new List<ConceptCardData>((IEnumerable<ConceptCardData>) unit.ConceptCards);
        for (int i = 0; i < concept_cards.Count; ++i)
        {
          if (equipConceptCard.FindIndex((Predicate<ConceptCardData>) (card => card != null && card.Param.iname == concept_cards[i].Param.iname)) == -1)
          {
            equipConceptCard[targetSlotIndex] = concept_cards[i];
            break;
          }
        }
      }
      return equipConceptCard;
    }

    private static int CompareConceptCardData(UnitData unit, ConceptCardData a, ConceptCardData b)
    {
      int conceptCardConditions1 = MonoSingleton<GameManager>.Instance.MasterParam.FindUnitConceptCardConditions(unit, a, true);
      int conceptCardConditions2 = MonoSingleton<GameManager>.Instance.MasterParam.FindUnitConceptCardConditions(unit, b, true);
      if (conceptCardConditions1 > 0 && conceptCardConditions2 > 0)
      {
        if (conceptCardConditions1 < conceptCardConditions2)
          return -1;
        if (conceptCardConditions1 > conceptCardConditions2)
          return 1;
        if ((int) a.AwakeCount > (int) b.AwakeCount)
          return -1;
        if ((int) a.AwakeCount < (int) b.AwakeCount)
          return 1;
      }
      else
      {
        if (conceptCardConditions1 > 0)
          return -1;
        if (conceptCardConditions2 > 0)
          return 1;
      }
      if (a.Param.rare > b.Param.rare)
        return -1;
      if (a.Param.rare < b.Param.rare)
        return 1;
      if ((int) a.Lv > (int) b.Lv)
        return -1;
      return (int) a.Lv < (int) b.Lv ? 1 : 0;
    }

    public void OnClickItem(ConceptCardIcon icon)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) icon, (UnityEngine.Object) null))
        return;
      GlobalVars.SelectedConceptCardData.Set(icon.ConceptCard);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 111);
    }

    private ConceptCardIcon CreateConceptCardIcon(ConceptCardData conceptCardData)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.IconTemplate, (UnityEngine.Object) null))
        return (ConceptCardIcon) null;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.IconParent, (UnityEngine.Object) null))
        return (ConceptCardIcon) null;
      ConceptCardIcon go = UnityEngine.Object.Instantiate<ConceptCardIcon>(this.IconTemplate, this.IconParent.transform);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) go, (UnityEngine.Object) null))
        return (ConceptCardIcon) null;
      GameUtility.SetGameObjectActive((Component) go, true);
      DataSource.Bind<ConceptCardData>(((Component) go).gameObject, conceptCardData);
      go.Setup(conceptCardData);
      return go;
    }
  }
}
