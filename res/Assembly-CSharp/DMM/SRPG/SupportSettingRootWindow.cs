// Decompiled with JetBrains decompiler
// Type: SRPG.SupportSettingRootWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "Is Change Unit", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "Refresh", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(100, "Changed", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "No Change", FlowNode.PinTypes.Output, 101)]
  public class SupportSettingRootWindow : MonoBehaviour, IFlowInterface
  {
    private const int INPUT_CHANGE_SUPPORT = 1;
    private const int INPUT_REFRESH = 2;
    private const int OUTPUT_CHANGE_SUPPORT_SET = 100;
    private const int OUTPUT_CHANGE_SUPPORT_NONE = 101;
    [SerializeField]
    private SupportElementList m_UnitElementList;
    private long[] m_UnitUniequeIDs = new long[Enum.GetValues(typeof (EElement)).Length];
    private long[] m_UnitDefaultUniequeIDs = new long[Enum.GetValues(typeof (EElement)).Length];
    private const int DEFAULT_UNITDATA = 0;

    private void Awake()
    {
      DataSource.Bind<PlayerPartyTypes>(((Component) this).gameObject, PlayerPartyTypes.Support);
    }

    private void OnDestroy() => GlobalVars.OverWritePartyType.Set(eOverWritePartyType.None);

    private void DebugRefreshSupportUnitList()
    {
      long[] iids = new long[Enum.GetValues(typeof (EElement)).Length];
      iids[0] = (long) GlobalVars.SelectedSupportUnitUniqueID;
      List<UnitData> units = MonoSingleton<GameManager>.Instance.Player.Units;
      for (int index1 = 1; index1 < iids.Length; ++index1)
      {
        for (int index2 = 0; index2 < units.Count; ++index2)
        {
          if (units[index2].Element == (EElement) index1)
          {
            iids[index1] = units[index2].UniqueID;
            break;
          }
        }
      }
      this.SetSupportUnitData(iids);
    }

    public void Clear()
    {
      for (int index = 1; index < this.m_UnitUniequeIDs.Length; ++index)
        this.m_UnitUniequeIDs[index] = 0L;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_UnitElementList, (UnityEngine.Object) null))
        return;
      this.m_UnitElementList.Refresh(this.m_UnitUniequeIDs);
    }

    public void SetSupportUnitData(long[] iids)
    {
      UnitData[] units = new UnitData[Enum.GetValues(typeof (EElement)).Length];
      for (int index = 0; index < iids.Length; ++index)
        units[index] = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(iids[index]);
      this.SetSupportUnitData(units);
      this.m_UnitUniequeIDs.CopyTo((Array) this.m_UnitDefaultUniequeIDs, 0);
    }

    public void SetSupportUnitData(UnitData[] units)
    {
      if (units == null)
      {
        this.Clear();
      }
      else
      {
        for (int index = 0; index < units.Length; ++index)
          this.m_UnitUniequeIDs[index] = units[index] != null ? units[index].UniqueID : 0L;
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_UnitElementList, (UnityEngine.Object) null))
          return;
        this.m_UnitElementList.Refresh(units);
      }
    }

    public void SetSupportUnitData(int element, long uniqId)
    {
      if (element < 0 || element >= this.m_UnitUniequeIDs.Length)
        return;
      this.m_UnitUniequeIDs[element] = uniqId;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_UnitElementList, (UnityEngine.Object) null))
        return;
      this.m_UnitElementList.Refresh(element, MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(uniqId));
    }

    public SupportSettingRootWindow.OwnSupportData[] GetSupportUnitData()
    {
      SupportSettingRootWindow.OwnSupportData[] supportUnitData = new SupportSettingRootWindow.OwnSupportData[Enum.GetValues(typeof (EElement)).Length];
      for (int index = 0; index < this.m_UnitUniequeIDs.Length; ++index)
      {
        if (this.m_UnitUniequeIDs[index] == 0L)
        {
          supportUnitData[index] = (SupportSettingRootWindow.OwnSupportData) null;
        }
        else
        {
          supportUnitData[index] = new SupportSettingRootWindow.OwnSupportData();
          supportUnitData[index].m_Element = (EElement) index;
          supportUnitData[index].m_UniqueID = this.m_UnitUniequeIDs[index];
        }
      }
      return supportUnitData;
    }

    public void OnEvent(string key, string value)
    {
      switch (key)
      {
        case "SET":
          if (!UnitListRootWindow.hasInstance)
            break;
          this.SetSupportUnitData(UnitListRootWindow.instance.GetData<int>("data_element"), GlobalVars.SelectedUnitUniqueID.Get());
          break;
        case "REMOVE":
          if (!UnitListRootWindow.hasInstance)
            break;
          this.SetSupportUnitData(UnitListRootWindow.instance.GetData<int>("data_element"), 0L);
          break;
        case "REMOVEALL":
          this.Clear();
          break;
        case "SYNC_SUPPORT_IDS":
          this.m_UnitUniequeIDs.CopyTo((Array) this.m_UnitDefaultUniequeIDs, 0);
          break;
      }
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 1:
          bool flag = this.m_UnitUniequeIDs.Length == this.m_UnitUniequeIDs.Length;
          if (flag)
          {
            for (int index = 0; index < this.m_UnitDefaultUniequeIDs.Length; ++index)
            {
              if (this.m_UnitDefaultUniequeIDs[index] != this.m_UnitUniequeIDs[index])
              {
                flag = false;
                break;
              }
            }
          }
          if (!flag)
          {
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
            break;
          }
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
          break;
        case 2:
          this.SetSupportUnitData(this.m_UnitDefaultUniequeIDs);
          break;
      }
    }

    public class OwnSupportData
    {
      public long m_UniqueID;
      public EElement m_Element;
    }
  }
}
