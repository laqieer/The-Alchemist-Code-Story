// Decompiled with JetBrains decompiler
// Type: SRPG.VersusDraftPartyEdit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "Initialize", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "Initialize Complete", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(10, "Reset", FlowNode.PinTypes.Input, 5)]
  [FlowNode.Pin(110, "Not Charged Party", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(111, "Charged Party", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(20, "Finish", FlowNode.PinTypes.Input, 20)]
  [FlowNode.Pin(120, "To Map", FlowNode.PinTypes.Output, 21)]
  public class VersusDraftPartyEdit : MonoBehaviour, IFlowInterface
  {
    private const int PARTY_SLOT_COUNT = 5;
    private const int PARTY_ENABLE_COUNT = 3;
    private const int INPUT_PIN_INITIALIZE = 1;
    private const int OUTPUT_PIN_INITIALIZE = 101;
    private const int INPUT_PIN_RESET = 10;
    private const int OUTPUT_PIN_NOT_CHARGE = 110;
    private const int OUTPUT_PIN_CHARGED = 111;
    private const int INPUT_PIN_FINISH = 20;
    private const int OUTPUT_PIN_TO_MAP = 120;
    [SerializeField]
    private Transform mPartyTransform;
    [SerializeField]
    private VersusDraftPartySlot mUnitSlotItem;
    [SerializeField]
    private Transform mUnitTransform;
    [SerializeField]
    private VersusDraftPartyUnit mUnitItem;
    [SerializeField]
    private Text mTotalAtk;
    [SerializeField]
    private GameObject mGOLeaderSkill;
    [SerializeField]
    private Text mTimerText;
    private DataSource mLSDataSource;
    private string mDefaultTotalAtkText;
    private float mOrganizeSec;
    private float mTimer;
    private bool mIsFinish;
    private List<VersusDraftPartySlot> mVersusDraftPartySlotList;
    private List<VersusDraftPartyUnit> mVersusDraftPartyUnitList;

    private void Start()
    {
      this.mUnitSlotItem.gameObject.SetActive(false);
      this.mUnitItem.gameObject.SetActive(false);
      this.mOrganizeSec = (float) (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.DraftOrganizeSeconds;
      this.mLSDataSource = DataSource.Create(this.mGOLeaderSkill);
      VersusDraftPartySlot.VersusDraftPartyEdit = this;
      VersusDraftPartyUnit.VersusDraftPartyEdit = this;
      this.mDefaultTotalAtkText = this.mTotalAtk.text;
    }

    private void Update()
    {
      if (this.mIsFinish)
        return;
      this.mTimer += Time.unscaledDeltaTime;
      this.mTimerText.text = ((int) ((double) this.mOrganizeSec - (double) this.mTimer)).ToString();
      if ((double) this.mTimer < (double) this.mOrganizeSec)
        return;
      this.Timeout();
    }

    private void Timeout()
    {
      for (int index = 0; index < 3; ++index)
      {
        if (!((UnityEngine.Object) this.mVersusDraftPartySlotList[index].PartyUnit != (UnityEngine.Object) null))
        {
          VersusDraftPartyUnit versusDraftPartyUnit = this.mVersusDraftPartyUnitList.Find((Predicate<VersusDraftPartyUnit>) (vdpUnit => !vdpUnit.IsSetSlot));
          if (!((UnityEngine.Object) versusDraftPartyUnit == (UnityEngine.Object) null))
            versusDraftPartyUnit.Select(this.mVersusDraftPartySlotList[index]);
        }
      }
      this.ToMap();
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 1:
          this.Initialize();
          break;
        case 10:
          this.AllReset();
          break;
        case 20:
          this.ToMap();
          break;
      }
    }

    private void Initialize()
    {
      this.mVersusDraftPartySlotList = new List<VersusDraftPartySlot>();
      for (int index = 0; index < 5; ++index)
      {
        VersusDraftPartySlot versusDraftPartySlot = UnityEngine.Object.Instantiate<VersusDraftPartySlot>(this.mUnitSlotItem);
        versusDraftPartySlot.transform.SetParent(this.mPartyTransform, false);
        versusDraftPartySlot.SetUp(index == 0, index >= 3);
        this.mVersusDraftPartySlotList.Add(versusDraftPartySlot);
      }
      this.mVersusDraftPartyUnitList = new List<VersusDraftPartyUnit>();
      for (int index = 0; index < VersusDraftList.VersusDraftUnitDataListPlayer.Count; ++index)
      {
        VersusDraftPartyUnit versusDraftPartyUnit = UnityEngine.Object.Instantiate<VersusDraftPartyUnit>(this.mUnitItem);
        versusDraftPartyUnit.transform.SetParent(this.mUnitTransform, false);
        versusDraftPartyUnit.SetUp(VersusDraftList.VersusDraftUnitDataListPlayer[index]);
        this.mVersusDraftPartyUnitList.Add(versusDraftPartyUnit);
      }
      this.UpdateParty(true);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
    }

    private void AllReset()
    {
      for (int index = 0; index < this.mVersusDraftPartySlotList.Count; ++index)
      {
        if (!((UnityEngine.Object) this.mVersusDraftPartySlotList[index].PartyUnit == (UnityEngine.Object) null) && this.mVersusDraftPartySlotList[index].PartyUnit.UnitData != null)
        {
          this.mVersusDraftPartySlotList[index].PartyUnit.Reset();
          this.mVersusDraftPartySlotList[index].SetUnit((VersusDraftPartyUnit) null);
        }
      }
      this.UpdateParty(true);
    }

    private void ToMap()
    {
      if (this.mIsFinish)
        return;
      this.mIsFinish = true;
      VersusDraftList.VersusDraftPartyUnits = new List<UnitData>();
      for (int index = 0; index < 3; ++index)
      {
        if (!((UnityEngine.Object) this.mVersusDraftPartySlotList[index].PartyUnit == (UnityEngine.Object) null) && this.mVersusDraftPartySlotList[index].PartyUnit.UnitData != null)
          VersusDraftList.VersusDraftPartyUnits.Add(this.mVersusDraftPartySlotList[index].PartyUnit.UnitData);
      }
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 120);
    }

    public void SelectNextSlot()
    {
      int index1 = -1;
      int num = -1;
      for (int index2 = 0; index2 < this.mVersusDraftPartySlotList.Count; ++index2)
      {
        if (!this.mVersusDraftPartySlotList[index2].IsLock && (UnityEngine.Object) this.mVersusDraftPartySlotList[index2] == (UnityEngine.Object) VersusDraftPartySlot.CurrentSelected)
        {
          num = index2;
          break;
        }
      }
      for (int index2 = 0; index2 < 2; ++index2)
      {
        for (int index3 = num + 1; index3 < this.mVersusDraftPartySlotList.Count; ++index3)
        {
          if (!this.mVersusDraftPartySlotList[index3].IsLock && (UnityEngine.Object) this.mVersusDraftPartySlotList[index3].PartyUnit == (UnityEngine.Object) null)
          {
            index1 = index3;
            break;
          }
        }
        if (index1 < 0)
          num = -1;
        else
          break;
      }
      if (index1 < 0)
        return;
      this.mVersusDraftPartySlotList[index1].SelectSlot(true);
    }

    public void UpdateParty(bool is_leader)
    {
      if (is_leader)
      {
        this.mLSDataSource.Clear();
        if (this.mVersusDraftPartySlotList.Count > 0 && (UnityEngine.Object) this.mVersusDraftPartySlotList[0].PartyUnit != (UnityEngine.Object) null && this.mVersusDraftPartySlotList[0].PartyUnit.UnitData != null)
          this.mLSDataSource.Add(typeof (SkillData), (object) this.mVersusDraftPartySlotList[0].PartyUnit.UnitData.LeaderSkill);
        GameParameter.UpdateAll(this.mGOLeaderSkill);
      }
      int num1 = 0;
      int num2 = 0;
      for (int index = 0; index < this.mVersusDraftPartySlotList.Count; ++index)
      {
        if (!((UnityEngine.Object) this.mVersusDraftPartySlotList[index].PartyUnit == (UnityEngine.Object) null) && this.mVersusDraftPartySlotList[index].PartyUnit.UnitData != null)
        {
          num2 = num2 + (int) this.mVersusDraftPartySlotList[index].PartyUnit.UnitData.Status.param.atk + (int) this.mVersusDraftPartySlotList[index].PartyUnit.UnitData.Status.param.mag;
          ++num1;
        }
      }
      this.mTotalAtk.text = num2 <= 0 ? this.mDefaultTotalAtkText : num2.ToString();
      if (num1 >= 3)
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 111);
      else
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 110);
    }
  }
}
