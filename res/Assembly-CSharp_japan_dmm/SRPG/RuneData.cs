// Decompiled with JetBrains decompiler
// Type: SRPG.RuneData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using MessagePack;
using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  [Serializable]
  public class RuneData
  {
    public OLong mUniqueID = (OLong) 0L;
    public string iname;
    public int unit_id;
    public byte enforce;
    public byte evo;
    private byte slot_index;
    private bool favorite;
    public RuneStateData state = new RuneStateData();

    public bool Deserialize(Json_RuneData json)
    {
      if (json == null)
      {
        DebugUtility.LogError("RuneData deserialize failed.");
        return false;
      }
      this.mUniqueID = (OLong) json.iid;
      this.iname = json.iname;
      this.unit_id = json.unit_id;
      this.slot_index = (byte) RuneSlotIndex.CreateSlotToIndex(json.slot);
      this.enforce = (byte) json.enforce;
      this.evo = (byte) json.evo;
      this.favorite = json.IsFavorite;
      this.state.Deserialize(json.state);
      return true;
    }

    public Json_RuneData Serialize()
    {
      return new Json_RuneData()
      {
        iid = (long) this.mUniqueID,
        iname = this.iname,
        unit_id = this.unit_id,
        slot = RuneSlotIndex.IndexToSlot((int) this.slot_index),
        enforce = (int) this.enforce,
        evo = (int) this.evo,
        state = this.state.Serialize()
      };
    }

    [IgnoreMember]
    public OLong UniqueID => this.mUniqueID;

    [IgnoreMember]
    public RuneParam RuneParam
    {
      get => MonoSingleton<GameManager>.Instance.MasterParam.GetRuneParam(this.iname);
    }

    [IgnoreMember]
    public RuneMaterial RuneMaterialList
    {
      get
      {
        return MonoSingleton<GameManager>.Instance.MasterParam.GetRuneMaterial(this.RuneParam.ItemParam.rare);
      }
    }

    [IgnoreMember]
    public RuneCost EnhanceCost
    {
      get
      {
        RuneMaterial runeMaterialList = this.RuneMaterialList;
        if (runeMaterialList == null || runeMaterialList.enh_cost.Length <= 0)
          return (RuneCost) null;
        int index = (int) this.enforce;
        if (index >= runeMaterialList.enh_cost.Length)
          index = runeMaterialList.enh_cost.Length - 1;
        return runeMaterialList.enh_cost[index];
      }
    }

    [IgnoreMember]
    public int DisassemblyZeny
    {
      get
      {
        RuneMaterial runeMaterialList = this.RuneMaterialList;
        return runeMaterialList == null ? 0 : runeMaterialList.disassembly_zeny;
      }
    }

    [IgnoreMember]
    public RuneCost EvoCost
    {
      get
      {
        RuneMaterial runeMaterialList = this.RuneMaterialList;
        if (runeMaterialList == null || runeMaterialList.evo_cost.Length <= 0)
          return (RuneCost) null;
        int index = (int) this.evo;
        if (index >= runeMaterialList.evo_cost.Length)
          index = runeMaterialList.evo_cost.Length - 1;
        return runeMaterialList.evo_cost[index];
      }
    }

    [IgnoreMember]
    public RuneCost[] ResetParamBaseCost
    {
      get
      {
        RuneMaterial runeMaterialList = this.RuneMaterialList;
        return runeMaterialList == null || runeMaterialList.reset_base_param_costs == null || runeMaterialList.reset_base_param_costs.Length <= 0 ? (RuneCost[]) null : runeMaterialList.reset_base_param_costs;
      }
    }

    [IgnoreMember]
    public RuneCost[] ResetStatusEvoCost
    {
      get
      {
        RuneMaterial runeMaterialList = this.RuneMaterialList;
        return runeMaterialList == null || runeMaterialList.reset_evo_status_costs == null || runeMaterialList.reset_evo_status_costs.Length <= 0 ? (RuneCost[]) null : runeMaterialList.reset_evo_status_costs;
      }
    }

    [IgnoreMember]
    public RuneCost[] ParamEnhEvoCost
    {
      get
      {
        RuneMaterial runeMaterialList = this.RuneMaterialList;
        return runeMaterialList == null || runeMaterialList.param_enh_evo_costs == null || runeMaterialList.param_enh_evo_costs.Length <= 0 ? (RuneCost[]) null : runeMaterialList.param_enh_evo_costs;
      }
    }

    [IgnoreMember]
    public RuneMaterial RuneMaterial
    {
      get => MonoSingleton<GameManager>.Instance.MasterParam.GetRuneMaterial(this.Rarity);
    }

    [IgnoreMember]
    public UnitData UnitData
    {
      get => MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID((long) this.unit_id);
    }

    [IgnoreMember]
    public int EnhanceNum
    {
      get
      {
        return ((int) this.enforce - (int) this.evo * MonoSingleton<GameManager>.Instance.MasterParam.FixParam.RuneEnhNextNum) % (MonoSingleton<GameManager>.Instance.MasterParam.FixParam.RuneEnhNextNum + 1);
      }
    }

    [IgnoreMember]
    public bool IsEvoNext
    {
      get
      {
        return this.EnhanceNum == MonoSingleton<GameManager>.Instance.MasterParam.FixParam.RuneEnhNextNum;
      }
    }

    [IgnoreMember]
    public bool IsCanEvo
    {
      get => this.EvoNum < MonoSingleton<GameManager>.Instance.MasterParam.FixParam.RuneMaxEvoNum;
    }

    [IgnoreMember]
    public int EvoNum => (int) this.evo;

    [IgnoreMember]
    public ItemParam Item => this.RuneParam.ItemParam;

    [IgnoreMember]
    public int Rarity => this.Item != null ? this.Item.rare : 0;

    [IgnoreMember]
    public int Slot => (int) (byte) this.RuneParam.slot_index;

    [IgnoreMember]
    public bool IsFavorite
    {
      get => this.favorite;
      set => this.favorite = value;
    }

    public UnitData GetOwner()
    {
      UnitData unit;
      MonoSingleton<GameManager>.Instance.Player.FindRuneOwner(this, out unit);
      return unit;
    }

    public void CreateBaseStatusFromBaseParam(
      ref BaseStatus addStatus,
      ref BaseStatus scaleStatus,
      bool isDrawBaseStatus)
    {
      this.state.base_state.CreateBaseStatus((int) this.enforce, ref addStatus, ref scaleStatus, isDrawBaseStatus);
    }

    public void CreateBaseStatusFromOnlyBaseParam(
      ref BaseStatus addOnlyBaseStatus,
      ref BaseStatus scaleOnlyBaseStatus,
      bool isDrawBaseStatus)
    {
      this.state.base_state.CreateOnlyBaseStatus((int) this.enforce, ref addOnlyBaseStatus, ref scaleOnlyBaseStatus, isDrawBaseStatus);
    }

    public float PowerPercentageFromBaseParam() => this.state.base_state.PowerPercentage();

    public int GetLengthFromEvoParam() => this.state.evo_state.Count;

    public float PowerPercentageFromEvoParam(int index)
    {
      return index >= this.GetLengthFromEvoParam() ? 0.0f : this.state.evo_state[index].PowerPercentage();
    }

    public void CreateBaseStatusFromEvoParam(
      int index,
      ref BaseStatus addStatus,
      ref BaseStatus scaleStatus,
      bool isDrawBaseStatus)
    {
      if (index >= this.GetLengthFromEvoParam())
        return;
      this.state.evo_state[index].CreateBaseStatus(ref addStatus, ref scaleStatus, isDrawBaseStatus);
    }

    public int GetEvoSlot(int index)
    {
      return index >= this.GetLengthFromEvoParam() ? 0 : (int) this.state.evo_state[index].slot;
    }

    public int GetEvoIndex(int evo_slot_id)
    {
      return this.state.evo_state.FindIndex((Predicate<RuneBuffDataEvoState>) (data => (int) data.slot == evo_slot_id));
    }

    public long GetSortData(
      eRuneSortType type,
      bool isBaseParamSort,
      bool isEvoParamSort,
      bool isSetParamSort)
    {
      if (!isBaseParamSort && !isEvoParamSort && !isSetParamSort)
        return 0;
      BaseStatus baseStatus = new BaseStatus();
      if (isBaseParamSort)
      {
        BaseStatus addStatus = (BaseStatus) null;
        BaseStatus scaleStatus = (BaseStatus) null;
        this.CreateBaseStatusFromBaseParam(ref addStatus, ref scaleStatus, false);
        if (addStatus != null)
          baseStatus.Add(addStatus);
        if (scaleStatus != null)
          baseStatus.Add(scaleStatus);
      }
      if (isEvoParamSort)
      {
        for (int index = 0; index < this.state.evo_state.Count; ++index)
        {
          BaseStatus addStatus = (BaseStatus) null;
          BaseStatus scaleStatus = (BaseStatus) null;
          this.CreateBaseStatusFromEvoParam(index, ref addStatus, ref scaleStatus, false);
          if (addStatus != null)
            baseStatus.Add(addStatus);
          if (scaleStatus != null)
            baseStatus.Add(scaleStatus);
        }
      }
      if (isSetParamSort && this.RuneParam != null && this.RuneParam.RuneSetEff != null)
        this.RuneParam.RuneSetEff.AddRuneSetEffectBaseStatus(EElement.None, ref baseStatus, ref baseStatus, false);
      switch (type)
      {
        case eRuneSortType.Default:
          return (long) this.UniqueID;
        case eRuneSortType.Rarity:
          return (long) this.Rarity;
        case eRuneSortType.Enhance:
          return (long) this.enforce;
        case eRuneSortType.Evolution:
          return (long) this.evo;
        case eRuneSortType.Hp:
          return (long) (int) baseStatus.param.hp;
        case eRuneSortType.Mp:
          return (long) (int) baseStatus.param.mp;
        case eRuneSortType.MpIni:
          return (long) (int) baseStatus.param.imp;
        case eRuneSortType.Atk:
          return (long) (int) baseStatus.param.atk;
        case eRuneSortType.Def:
          return (long) (int) baseStatus.param.def;
        case eRuneSortType.Mag:
          return (long) (int) baseStatus.param.mag;
        case eRuneSortType.Mnd:
          return (long) (int) baseStatus.param.mnd;
        case eRuneSortType.Rec:
          return (long) (int) baseStatus.param.rec;
        case eRuneSortType.Dex:
          return (long) (int) baseStatus.param.dex;
        case eRuneSortType.Spd:
          return (long) (int) baseStatus.param.spd;
        case eRuneSortType.Cri:
          return (long) (int) baseStatus.param.cri;
        case eRuneSortType.Luk:
          return (long) (int) baseStatus.param.luk;
        case eRuneSortType.Mov:
          return (long) (int) baseStatus.param.mov;
        case eRuneSortType.Jmp:
          return (long) (int) baseStatus.param.jmp;
        default:
          return 0;
      }
    }

    public string DisplayRuneBaseParam()
    {
      BaseStatus baseStatus = new BaseStatus();
      BaseStatus addStatus = (BaseStatus) null;
      BaseStatus scaleStatus = (BaseStatus) null;
      this.CreateBaseStatusFromBaseParam(ref addStatus, ref scaleStatus, false);
      if (addStatus != null)
        baseStatus.Add(addStatus);
      if (scaleStatus != null)
        baseStatus.Add(scaleStatus);
      SortRuneConditionParam runeConditionParam = MonoSingleton<GameManager>.Instance.MasterParam.FindSortRuneConditionParam(SortUtility.Load_RuneSortFromCache().FindFirstOn());
      string str = LocalizedText.Get("sys.RUNE_STATUS_NONE");
      switch (runeConditionParam.sort_type)
      {
        case eRuneSortType.Enhance:
          return this.enforce.ToString();
        case eRuneSortType.Evolution:
          return this.evo.ToString();
        case eRuneSortType.Hp:
          if ((int) baseStatus.param.hp > 0)
            return string.Format(LocalizedText.Get("sys.RUNE_STATUS_PARAM"), (object) baseStatus.param.hp.ToString());
          break;
        case eRuneSortType.Mp:
          if ((int) baseStatus.param.mp > 0)
            return string.Format(LocalizedText.Get("sys.RUNE_STATUS_PARAM"), (object) baseStatus.param.mp.ToString());
          break;
        case eRuneSortType.MpIni:
          if ((int) baseStatus.param.imp > 0)
            return string.Format(LocalizedText.Get("sys.RUNE_STATUS_PARAM"), (object) baseStatus.param.imp.ToString());
          break;
        case eRuneSortType.Atk:
          if ((int) baseStatus.param.atk > 0)
            return string.Format(LocalizedText.Get("sys.RUNE_STATUS_PARAM"), (object) baseStatus.param.atk.ToString());
          break;
        case eRuneSortType.Def:
          if ((int) baseStatus.param.def > 0)
            return string.Format(LocalizedText.Get("sys.RUNE_STATUS_PARAM"), (object) baseStatus.param.def.ToString());
          break;
        case eRuneSortType.Mag:
          if ((int) baseStatus.param.mag > 0)
            return string.Format(LocalizedText.Get("sys.RUNE_STATUS_PARAM"), (object) baseStatus.param.mag.ToString());
          break;
        case eRuneSortType.Mnd:
          if ((int) baseStatus.param.mnd > 0)
            return string.Format(LocalizedText.Get("sys.RUNE_STATUS_PARAM"), (object) baseStatus.param.mnd.ToString());
          break;
        case eRuneSortType.Rec:
          if ((int) baseStatus.param.rec > 0)
            return string.Format(LocalizedText.Get("sys.RUNE_STATUS_PARAM"), (object) baseStatus.param.rec.ToString());
          break;
        case eRuneSortType.Dex:
          if ((int) baseStatus.param.dex > 0)
            return string.Format(LocalizedText.Get("sys.RUNE_STATUS_PARAM"), (object) baseStatus.param.dex.ToString());
          break;
        case eRuneSortType.Spd:
          if ((int) baseStatus.param.spd > 0)
            return string.Format(LocalizedText.Get("sys.RUNE_STATUS_PARAM"), (object) baseStatus.param.spd.ToString());
          break;
        case eRuneSortType.Cri:
          if ((int) baseStatus.param.cri > 0)
            return string.Format(LocalizedText.Get("sys.RUNE_STATUS_PARAM"), (object) baseStatus.param.cri.ToString());
          break;
        case eRuneSortType.Luk:
          if ((int) baseStatus.param.luk > 0)
            return string.Format(LocalizedText.Get("sys.RUNE_STATUS_PARAM"), (object) baseStatus.param.luk.ToString());
          break;
        case eRuneSortType.Mov:
          if ((int) baseStatus.param.mov > 0)
            return string.Format(LocalizedText.Get("sys.RUNE_STATUS_PARAM"), (object) baseStatus.param.mov.ToString());
          break;
        case eRuneSortType.Jmp:
          if ((int) baseStatus.param.jmp > 0)
            return string.Format(LocalizedText.Get("sys.RUNE_STATUS_PARAM"), (object) baseStatus.param.jmp.ToString());
          break;
      }
      return str;
    }

    public List<long> GetSortData(eRuneSortType type, bool _is_ascending)
    {
      BaseStatus baseStatus = new BaseStatus();
      BaseStatus addStatus = (BaseStatus) null;
      BaseStatus scaleStatus = (BaseStatus) null;
      this.CreateBaseStatusFromBaseParam(ref addStatus, ref scaleStatus, false);
      if (addStatus != null)
        baseStatus.Add(addStatus);
      if (scaleStatus != null)
        baseStatus.Add(scaleStatus);
      List<long> sortData = new List<long>();
      switch (type)
      {
        case eRuneSortType.Default:
          sortData.Add((long) this.UniqueID);
          break;
        case eRuneSortType.Rarity:
          sortData.Add((long) this.Rarity);
          sortData.Add((long) this.Slot);
          sortData.Add((long) this.UniqueID);
          break;
        case eRuneSortType.Enhance:
          sortData.Add((long) this.enforce);
          sortData.Add((long) this.evo);
          sortData.Add((long) this.UniqueID);
          break;
        case eRuneSortType.Evolution:
          sortData.Add((long) this.evo);
          sortData.Add((long) this.enforce);
          sortData.Add((long) this.UniqueID);
          break;
        case eRuneSortType.Hp:
          sortData.Add(this.CheckHasStatus((int) baseStatus.param.hp, _is_ascending));
          sortData.Add(this.CheckStatusParamCoefficient(baseStatus.param, _is_ascending));
          sortData.Add(this.GetStatusParamNum(baseStatus.param));
          sortData.Add((long) this.Rarity);
          sortData.Add((long) this.UniqueID);
          break;
        case eRuneSortType.Atk:
          sortData.Add(this.CheckHasStatus((int) baseStatus.param.atk, _is_ascending));
          sortData.Add(this.CheckStatusParamCoefficient(baseStatus.param, _is_ascending));
          sortData.Add(this.GetStatusParamNum(baseStatus.param));
          sortData.Add((long) this.Rarity);
          sortData.Add((long) this.UniqueID);
          break;
        case eRuneSortType.Def:
          sortData.Add(this.CheckHasStatus((int) baseStatus.param.def, _is_ascending));
          sortData.Add(this.CheckStatusParamCoefficient(baseStatus.param, _is_ascending));
          sortData.Add(this.GetStatusParamNum(baseStatus.param));
          sortData.Add((long) this.Rarity);
          sortData.Add((long) this.UniqueID);
          break;
        case eRuneSortType.Mag:
          sortData.Add(this.CheckHasStatus((int) baseStatus.param.mag, _is_ascending));
          sortData.Add(this.CheckStatusParamCoefficient(baseStatus.param, _is_ascending));
          sortData.Add(this.GetStatusParamNum(baseStatus.param));
          sortData.Add((long) this.Rarity);
          sortData.Add((long) this.UniqueID);
          break;
        case eRuneSortType.Mnd:
          sortData.Add(this.CheckHasStatus((int) baseStatus.param.mnd, _is_ascending));
          sortData.Add(this.CheckStatusParamCoefficient(baseStatus.param, _is_ascending));
          sortData.Add(this.GetStatusParamNum(baseStatus.param));
          sortData.Add((long) this.Rarity);
          sortData.Add((long) this.UniqueID);
          break;
        case eRuneSortType.Dex:
          sortData.Add(this.CheckHasStatus((int) baseStatus.param.dex, _is_ascending));
          sortData.Add(this.CheckStatusParamCoefficient(baseStatus.param, _is_ascending));
          sortData.Add(this.GetStatusParamNum(baseStatus.param));
          sortData.Add((long) this.Rarity);
          sortData.Add((long) this.UniqueID);
          break;
        case eRuneSortType.Spd:
          sortData.Add(this.CheckHasStatus((int) baseStatus.param.spd, _is_ascending));
          sortData.Add(this.CheckStatusParamCoefficient(baseStatus.param, _is_ascending));
          sortData.Add(this.GetStatusParamNum(baseStatus.param));
          sortData.Add((long) this.Rarity);
          sortData.Add((long) this.UniqueID);
          break;
        case eRuneSortType.Cri:
          sortData.Add(this.CheckHasStatus((int) baseStatus.param.cri, _is_ascending));
          sortData.Add(this.CheckStatusParamCoefficient(baseStatus.param, _is_ascending));
          sortData.Add(this.GetStatusParamNum(baseStatus.param));
          sortData.Add((long) this.Rarity);
          sortData.Add((long) this.UniqueID);
          break;
        case eRuneSortType.Luk:
          sortData.Add(this.CheckHasStatus((int) baseStatus.param.luk, _is_ascending));
          sortData.Add(this.CheckStatusParamCoefficient(baseStatus.param, _is_ascending));
          sortData.Add(this.GetStatusParamNum(baseStatus.param));
          sortData.Add((long) this.Rarity);
          sortData.Add((long) this.UniqueID);
          break;
      }
      return sortData;
    }

    private long CheckHasStatus(int _status, bool _is_ascending)
    {
      return _status > 0 ? (!_is_ascending ? 1L : 0L) : (!_is_ascending ? 0L : 1L);
    }

    private long CheckStatusParamCoefficient(StatusParam _param, bool _is_ascending)
    {
      long num = (int) _param.hp <= 0 ? ((int) _param.atk <= 0 ? ((int) _param.def <= 0 ? ((int) _param.mag <= 0 ? ((int) _param.mnd <= 0 ? ((int) _param.dex <= 0 ? ((int) _param.spd <= 0 ? ((int) _param.cri <= 0 ? ((int) _param.luk <= 0 ? 0L : 9L) : 8L) : 7L) : 6L) : 5L) : 4L) : 3L) : 2L) : 1L;
      return _is_ascending ? num : 10L - num;
    }

    private long GetStatusParamNum(StatusParam _param)
    {
      if ((int) _param.hp > 0)
        return (long) (int) _param.hp;
      if ((int) _param.atk > 0)
        return (long) (int) _param.atk;
      if ((int) _param.def > 0)
        return (long) (int) _param.def;
      if ((int) _param.mag > 0)
        return (long) (int) _param.mag;
      if ((int) _param.mnd > 0)
        return (long) (int) _param.mnd;
      if ((int) _param.dex > 0)
        return (long) (int) _param.dex;
      if ((int) _param.spd > 0)
        return (long) (int) _param.spd;
      if ((int) _param.cri > 0)
        return (long) (int) _param.cri;
      return (int) _param.luk > 0 ? (long) (int) _param.luk : 0L;
    }

    public bool Filter(FilterRunePrefs filter)
    {
      FilterRuneParam[] filterRuneParams = MonoSingleton<GameManager>.Instance.MasterParam.FilterRuneParams;
      if (filterRuneParams == null)
        return true;
      for (int index1 = 0; index1 < filterRuneParams.Length; ++index1)
      {
        if (!filter.IsDisableFilterAll(filterRuneParams[index1].iname))
        {
          bool flag1 = false;
          for (int index2 = 0; index2 < filterRuneParams[index1].conditions.Length; ++index2)
          {
            FilterRuneConditionParam condition = filterRuneParams[index1].conditions[index2];
            bool flag2 = filter.GetValue(condition.parent.iname, condition.cnds_iname);
            RuneParam runeParam = this.RuneParam;
            if (filterRuneParams[index1].IsEnableFilterType(eRuneFilterTypes.Rarity))
            {
              ItemParam itemParam = this.Item;
              if (itemParam != null && itemParam.rare == (int) condition.rarity)
              {
                flag1 = true;
                if (!flag2)
                  return false;
              }
            }
            if (filterRuneParams[index1].IsEnableFilterType(eRuneFilterTypes.Slot) && runeParam != null && (int) (byte) runeParam.slot_index == (int) condition.slot_index)
            {
              flag1 = true;
              if (!flag2)
                return false;
            }
            if (filterRuneParams[index1].IsEnableFilterType(eRuneFilterTypes.SetEff) && runeParam != null)
            {
              RuneSetEff runeSetEff = runeParam.RuneSetEff;
              if (runeSetEff != null && runeSetEff.seteff_type == (int) condition.set_eff)
              {
                flag1 = true;
                if (!flag2)
                  return false;
              }
            }
          }
          if (!flag1)
            return false;
        }
      }
      return true;
    }

    public void AddRuneBaseStatus(
      EElement buffTargetElement,
      ref BaseStatus addStatus,
      ref BaseStatus scaleStatus)
    {
      BaseStatus addStatus1 = (BaseStatus) null;
      BaseStatus scaleStatus1 = (BaseStatus) null;
      this.state.base_state.CreateBaseStatus((int) this.enforce, buffTargetElement, ref addStatus1, ref scaleStatus1, false);
      if (addStatus1 != null && addStatus != null)
        addStatus.Add(addStatus1);
      if (scaleStatus1 != null && scaleStatus != null)
        scaleStatus.Add(scaleStatus1);
      for (int index = 0; index < this.state.evo_state.Count; ++index)
      {
        BaseStatus addStatus2 = (BaseStatus) null;
        BaseStatus scaleStatus2 = (BaseStatus) null;
        this.state.evo_state[index].CreateBaseStatus(buffTargetElement, ref addStatus2, ref scaleStatus2, false);
        if (addStatus2 != null && addStatus != null)
          addStatus.Add(addStatus2);
        if (scaleStatus2 != null && scaleStatus != null)
          scaleStatus.Add(scaleStatus2);
      }
    }

    public bool IsCanEnhanceEvoParam(int evo_index)
    {
      return 1.0 > (double) this.PowerPercentageFromEvoParam(evo_index);
    }
  }
}
