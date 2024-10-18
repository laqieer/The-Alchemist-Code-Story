// Decompiled with JetBrains decompiler
// Type: SRPG.InGameMenu_Audience
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "Start Debug", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(2, "Give Up", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "Close Give Up Window", FlowNode.PinTypes.Input, 0)]
  public class InGameMenu_Audience : MonoBehaviour, IFlowInterface
  {
    public GenericSlot[] Units_1P = new GenericSlot[0];
    public GenericSlot[] Units_2P = new GenericSlot[0];
    private List<InGameMenu_Audience.DispUnit> mDispUnitList = new List<InGameMenu_Audience.DispUnit>();
    public const int PINID_DEBUG = 1;
    public const int PINID_GIVEUP = 2;
    public const int PINID_CLOSE_GIVEUP_WINDOW = 100;
    public GameObject ExitButton;
    public Text Name1P;
    public Text Name2P;
    public Text TotalAtk1P;
    public Text TotalAtk2P;
    public Text Lv1P;
    public Text Lv2P;
    private GameObject mExitWindow;
    private uint FrameCtr;

    private void Start()
    {
      SceneBattle instance = SceneBattle.Instance;
      this.mDispUnitList.Clear();
      if ((UnityEngine.Object) this.ExitButton != (UnityEngine.Object) null)
        this.ExitButton.SetActive(true);
      if (!((UnityEngine.Object) instance != (UnityEngine.Object) null))
        return;
      List<JSON_MyPhotonPlayerParam> audiencePlayer = instance.AudiencePlayer;
      List<Unit> allUnits = instance.Battle.AllUnits;
      for (int i = 0; i < audiencePlayer.Count; ++i)
      {
        JSON_MyPhotonPlayerParam.UnitDataElem[] units = audiencePlayer[i].units;
        GenericSlot[] genericSlotArray = i != 0 ? this.Units_2P : this.Units_1P;
        if (genericSlotArray != null)
        {
          for (int j = 0; j < genericSlotArray.Length; ++j)
          {
            if (j < units.Length)
            {
              bool flag = false;
              Button component1 = genericSlotArray[j].GetComponent<Button>();
              if ((UnityEngine.Object) component1 != (UnityEngine.Object) null)
              {
                Unit unit = allUnits.Find((Predicate<Unit>) (un =>
                {
                  if (un.OwnerPlayerIndex == i + 1)
                    return un.UnitData.UnitParam.iname == units[j].unit.UnitParam.iname;
                  return false;
                }));
                if (unit != null)
                {
                  Unit dynamicTransformUnit = this.FindDynamicTransformUnit(unit);
                  genericSlotArray[j].SetSlotData<UnitData>(dynamicTransformUnit.UnitData);
                  flag = true;
                  DataSource component2 = component1.gameObject.GetComponent<DataSource>();
                  if ((bool) ((UnityEngine.Object) component2))
                    component2.Clear();
                  DataSource.Bind<Unit>(component1.gameObject, dynamicTransformUnit, false);
                  component1.interactable = !dynamicTransformUnit.IsDeadCondition();
                  this.mDispUnitList.Add(new InGameMenu_Audience.DispUnit(genericSlotArray[j], unit, dynamicTransformUnit, component1.gameObject));
                }
              }
              if (!flag)
                genericSlotArray[j].SetSlotData<UnitData>(units[j].unit);
            }
            else
              genericSlotArray[j].SetSlotData<UnitData>((UnitData) null);
          }
        }
        Text text1 = i != 0 ? this.Name2P : this.Name1P;
        if ((UnityEngine.Object) text1 != (UnityEngine.Object) null)
          text1.text = audiencePlayer[i].playerName;
        Text text2 = i != 0 ? this.TotalAtk2P : this.TotalAtk1P;
        if ((UnityEngine.Object) text2 != (UnityEngine.Object) null)
          text2.text = audiencePlayer[i].totalAtk.ToString();
        Text text3 = i != 0 ? this.Lv2P : this.Lv1P;
        if ((UnityEngine.Object) text2 != (UnityEngine.Object) null)
          text3.text = audiencePlayer[i].playerLevel.ToString();
      }
    }

    private Unit FindDynamicTransformUnit(Unit act_unit)
    {
      SceneBattle instance = SceneBattle.Instance;
      BattleCore battleCore = (BattleCore) null;
      if ((bool) ((UnityEngine.Object) instance))
        battleCore = instance.Battle;
      if (battleCore == null)
        return act_unit;
      return battleCore.DtuFindActUnit(act_unit);
    }

    private void OnDestroy()
    {
      if (!((UnityEngine.Object) SceneBattle.Instance != (UnityEngine.Object) null))
        return;
      SceneBattle.Instance.OnQuestEnd -= new SceneBattle.QuestEndEvent(this.OnQuestEnd);
    }

    private void OnQuestEnd()
    {
      this.Activated(100);
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 2:
          this.mExitWindow = UIUtility.ConfirmBox(LocalizedText.Get("sys.MULTI_VERSUS_COMFIRM_EXIT"), new UIUtility.DialogResultEvent(this.OnGiveUp), (UIUtility.DialogResultEvent) null, (GameObject) null, true, 1, (string) null, (string) null);
          break;
        case 100:
          if (!((UnityEngine.Object) this.mExitWindow != (UnityEngine.Object) null))
            break;
          Win_Btn_DecideCancel_FL_C component = this.mExitWindow.GetComponent<Win_Btn_DecideCancel_FL_C>();
          if ((UnityEngine.Object) component != (UnityEngine.Object) null)
            component.BeginClose();
          this.mExitWindow = (GameObject) null;
          break;
      }
    }

    private void OnGiveUp(GameObject go)
    {
      Network.Abort();
      if ((UnityEngine.Object) SceneBattle.Instance != (UnityEngine.Object) null)
        SceneBattle.Instance.ForceEndQuest();
      CanvasGroup component = this.GetComponent<CanvasGroup>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
        return;
      component.blocksRaycasts = false;
    }

    private void Update()
    {
      ++this.FrameCtr;
      if (this.FrameCtr % 10U != 0U)
        return;
      for (int index = 0; index < this.mDispUnitList.Count; ++index)
      {
        InGameMenu_Audience.DispUnit mDispUnit = this.mDispUnitList[index];
        Unit dynamicTransformUnit = this.FindDynamicTransformUnit(mDispUnit.mCurrentUnit);
        if (mDispUnit.mActiveUnit != dynamicTransformUnit)
        {
          DataSource component = mDispUnit.mSlot.gameObject.GetComponent<DataSource>();
          if ((bool) ((UnityEngine.Object) component))
            component.Clear();
          mDispUnit.mSlot.SetSlotData<UnitData>(dynamicTransformUnit.UnitData);
          mDispUnit.mActiveUnit = dynamicTransformUnit;
        }
        if (dynamicTransformUnit != null)
        {
          Button component = mDispUnit.mButtonObj.GetComponent<Button>();
          if ((UnityEngine.Object) component != (UnityEngine.Object) null)
            component.interactable = !dynamicTransformUnit.IsDeadCondition();
        }
      }
    }

    private class DispUnit
    {
      public GenericSlot mSlot;
      public Unit mCurrentUnit;
      public Unit mActiveUnit;
      public GameObject mButtonObj;

      public DispUnit(GenericSlot slot, Unit current_unit, Unit active_unit, GameObject button_obj)
      {
        this.mSlot = slot;
        this.mCurrentUnit = current_unit;
        this.mActiveUnit = active_unit;
        this.mButtonObj = button_obj;
      }
    }
  }
}
