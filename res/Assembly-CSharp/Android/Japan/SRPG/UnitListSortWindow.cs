﻿// Decompiled with JetBrains decompiler
// Type: SRPG.UnitListSortWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class UnitListSortWindow : FlowWindowBase
  {
    private Dictionary<UnitListSortWindow.SelectType, Toggle> m_Toggles = new Dictionary<UnitListSortWindow.SelectType, Toggle>();
    private const string SAVEKEY = "UNITLIST_SORT";
    private const string SAVEKEY_OLD = "UNITLIST";
    public const UnitListSortWindow.SelectType MASK_SECTION = (UnitListSortWindow.SelectType) 16777215;
    public const UnitListSortWindow.SelectType MASK_ALIGNMENT = (UnitListSortWindow.SelectType) 251658240;
    private UnitListSortWindow.SerializeParam m_Param;
    private SerializeValueList m_ValueList;
    private UnitListWindow m_Root;
    private UnitListSortWindow.Result m_Result;
    private UnitListSortWindow.SelectType m_SelectTypeReg;
    private UnitListSortWindow.SelectType m_SelectType;

    public override string name
    {
      get
      {
        return nameof (UnitListSortWindow);
      }
    }

    public override void Initialize(FlowWindowBase.SerializeParamBase param)
    {
      base.Initialize(param);
      this.m_Param = param as UnitListSortWindow.SerializeParam;
      if (this.m_Param == null)
        throw new Exception(this.ToString() + " > Failed serializeParam null.");
      SerializeValueBehaviour childComponent = this.GetChildComponent<SerializeValueBehaviour>("sort");
      this.m_ValueList = !((UnityEngine.Object) childComponent != (UnityEngine.Object) null) ? new SerializeValueList() : childComponent.list;
      if ((UnityEngine.Object) this.m_Window != (UnityEngine.Object) null && (UnityEngine.Object) this.m_Animator != (UnityEngine.Object) null)
        this.CacheToggleParam(this.m_Animator.gameObject);
      this.LoadSelectType();
      this.Close(true);
    }

    private void CreateInstance()
    {
      if ((UnityEngine.Object) this.m_Window != (UnityEngine.Object) null && (UnityEngine.Object) this.m_Animator != (UnityEngine.Object) null)
        return;
      string name = this.m_ValueList.GetString("path");
      if (string.IsNullOrEmpty(name))
        return;
      GameObject original = AssetManager.Load<GameObject>(name);
      if ((UnityEngine.Object) original == (UnityEngine.Object) null)
        return;
      GameObject toggle_parent_obj = (GameObject) null;
      if ((UnityEngine.Object) this.m_Animator == (UnityEngine.Object) null)
      {
        toggle_parent_obj = UnityEngine.Object.Instantiate<GameObject>(original);
        toggle_parent_obj.transform.SetParent(this.m_Window.transform, false);
        this.m_Animator = toggle_parent_obj.GetComponent<Animator>();
      }
      this.CacheToggleParam(toggle_parent_obj);
    }

    private void CacheToggleParam(GameObject toggle_parent_obj)
    {
      Toggle[] componentsInChildren = toggle_parent_obj.GetComponentsInChildren<Toggle>();
      for (int index = 0; index < componentsInChildren.Length; ++index)
      {
        try
        {
          UnitListSortWindow.SelectType key = (UnitListSortWindow.SelectType) Enum.Parse(typeof (UnitListSortWindow.SelectType), componentsInChildren[index].name);
          ButtonEvent component = componentsInChildren[index].GetComponent<ButtonEvent>();
          if ((UnityEngine.Object) component != (UnityEngine.Object) null)
          {
            ButtonEvent.Event @event = component.GetEvent("UNITSORT_TGL_CHANGE");
            if (@event != null)
            {
              if ((key & (UnitListSortWindow.SelectType) 16777215) != UnitListSortWindow.SelectType.NONE)
                @event.valueList.SetField("section", (int) key);
              if ((key & (UnitListSortWindow.SelectType) 251658240) != UnitListSortWindow.SelectType.NONE)
                @event.valueList.SetField("alignment", (int) key);
            }
          }
          this.m_Toggles.Add(key, componentsInChildren[index]);
        }
        catch (Exception ex)
        {
          Debug.LogError((object) ("UnitSortWindow トグル名からSelectTypeを取得できない！ > " + componentsInChildren[index].name + " ( Exception > " + ex.ToString() + " )"));
        }
      }
    }

    public override void Release()
    {
      base.Release();
    }

    public override int Update()
    {
      base.Update();
      if (this.m_Result != UnitListSortWindow.Result.NONE)
      {
        if (this.isClosed)
          this.SetActiveChild(false);
      }
      else if (this.isClosed)
        this.SetActiveChild(false);
      return -1;
    }

    private void InitializeToggleContent()
    {
      foreach (KeyValuePair<UnitListSortWindow.SelectType, Toggle> toggle in this.m_Toggles)
        toggle.Value.isOn = (this.m_SelectType & toggle.Key) != UnitListSortWindow.SelectType.NONE;
      this.m_SelectTypeReg = this.m_SelectType;
    }

    private void ReleaseToggleContent()
    {
    }

    public void SetRoot(UnitListWindow root)
    {
      this.m_Root = root;
    }

    private void SetSection(UnitListSortWindow.SelectType selectType)
    {
      this.m_SelectType &= (UnitListSortWindow.SelectType) -16777216;
      this.m_SelectType |= selectType & (UnitListSortWindow.SelectType) 16777215;
    }

    private void ResetSection(UnitListSortWindow.SelectType selectType)
    {
      this.m_SelectType &= ~(selectType & (UnitListSortWindow.SelectType) 16777215);
    }

    public UnitListSortWindow.SelectType GetSection()
    {
      return this.m_SelectType & (UnitListSortWindow.SelectType) 16777215;
    }

    public UnitListSortWindow.SelectType GetSectionReg()
    {
      return this.m_SelectTypeReg & (UnitListSortWindow.SelectType) 16777215;
    }

    private void SetAlignment(UnitListSortWindow.SelectType selectType)
    {
      this.m_SelectType &= (UnitListSortWindow.SelectType) -251658241;
      this.m_SelectType |= selectType & (UnitListSortWindow.SelectType) 251658240;
    }

    private void ResetAlignment(UnitListSortWindow.SelectType selectType)
    {
      this.m_SelectType &= ~(selectType & (UnitListSortWindow.SelectType) 251658240);
    }

    public UnitListSortWindow.SelectType GetAlignment()
    {
      return this.m_SelectType & (UnitListSortWindow.SelectType) 251658240;
    }

    public void CalcUnit(List<UnitListWindow.Data> list)
    {
      if (this.IsType(UnitListSortWindow.SelectType.NAME))
        SortUtility.StableSort<UnitListWindow.Data>(list, (Comparison<UnitListWindow.Data>) ((p1, p2) => string.Compare(p1.param == null ? string.Empty : p1.param.name, p2.param == null ? string.Empty : p2.param.name, CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth)));
      else if (!this.IsType(UnitListSortWindow.SelectType.TIME))
      {
        UnitListSortWindow.SelectType section = this.GetSection();
        for (int index = 0; index < list.Count; ++index)
          list[index].RefreshSortPriority(section);
        SortUtility.StableSort<UnitListWindow.Data>(list, (Comparison<UnitListWindow.Data>) ((p1, p2) =>
        {
          int num = p1.sortPriority.CompareTo(p2.sortPriority);
          if (num == 0)
            num = (p1.param == null ? string.Empty : p1.param.iname).CompareTo(p2.param == null ? string.Empty : p2.param.iname);
          return num;
        }));
      }
      if (this.GetAlignment() != UnitListSortWindow.SelectType.KOUJYUN)
        return;
      list.Reverse();
    }

    public bool IsType(UnitListSortWindow.SelectType value)
    {
      return (this.m_SelectType & value) != UnitListSortWindow.SelectType.NONE;
    }

    public void LoadSelectType()
    {
      this.m_SelectType = UnitListSortWindow.SelectType.NONE;
      string key1 = "UNITLIST_SORT";
      if (!string.IsNullOrEmpty(key1))
      {
        if (PlayerPrefsUtility.HasKey(key1))
        {
          string s = PlayerPrefsUtility.GetString(key1, string.Empty);
          int result = 0;
          if (!string.IsNullOrEmpty(s) && int.TryParse(s, out result))
            this.m_SelectType = (UnitListSortWindow.SelectType) result;
        }
        else
        {
          string key2 = "UNITLIST";
          if (!string.IsNullOrEmpty(key2))
          {
            this.m_SelectType = UnitListSortWindow.SelectType.NONE;
            if (PlayerPrefsUtility.HasKey(key2))
            {
              GameUtility.UnitSortModes oldMode = GameUtility.UnitSortModes.Time;
              string str = PlayerPrefsUtility.GetString(key2, string.Empty);
              try
              {
                if (!string.IsNullOrEmpty(str))
                  oldMode = (GameUtility.UnitSortModes) Enum.Parse(typeof (GameUtility.UnitSortModes), str, true);
              }
              catch (Exception ex)
              {
                DebugUtility.LogError("Unknown sort mode:" + str);
              }
              this.SetSection(UnitListSortWindow.ConvertSortMode(oldMode));
            }
            if (PlayerPrefsUtility.HasKey(key2 + "#"))
              this.SetAlignment(UnitListSortWindow.ConvertReverse(PlayerPrefsUtility.GetInt(key2 + "#", 0) != 0));
            this.SaveSelectType();
          }
        }
      }
      if (this.GetSection() == UnitListSortWindow.SelectType.NONE)
        this.SetSection(UnitListSortWindow.SelectType.TIME);
      if (this.GetAlignment() == UnitListSortWindow.SelectType.NONE)
        this.SetAlignment(UnitListSortWindow.SelectType.SYOJYUN);
      this.m_SelectTypeReg = this.m_SelectType;
    }

    public void SaveSelectType()
    {
      string key = "UNITLIST_SORT";
      if (string.IsNullOrEmpty(key))
        return;
      PlayerPrefsUtility.SetString(key, ((int) this.m_SelectType).ToString(), true);
    }

    public override int OnActivate(int pinId)
    {
      switch (pinId)
      {
        case 500:
          this.CreateInstance();
          this.InitializeToggleContent();
          this.Open();
          break;
        case 510:
          this.ReleaseToggleContent();
          this.m_SelectType = this.m_SelectTypeReg;
          this.m_Result = UnitListSortWindow.Result.CANCEL;
          this.Close(false);
          return 591;
        case 520:
          this.SaveSelectType();
          this.ReleaseToggleContent();
          this.m_SelectTypeReg = this.m_SelectType;
          this.m_Result = UnitListSortWindow.Result.CONFIRM;
          this.Close(false);
          return 590;
        case 530:
          SerializeValueList currentValue = FlowNode_ButtonEvent.currentValue as SerializeValueList;
          if (currentValue != null)
          {
            Toggle uiToggle = currentValue.GetUIToggle("_self");
            if ((UnityEngine.Object) uiToggle != (UnityEngine.Object) null)
            {
              int num1 = currentValue.GetInt("section", 0);
              if (num1 > 0)
              {
                if (uiToggle.isOn)
                {
                  this.SetSection((UnitListSortWindow.SelectType) num1);
                  break;
                }
                break;
              }
              int num2 = currentValue.GetInt("alignment", 0);
              if (num2 > 0 && uiToggle.isOn)
              {
                this.SetAlignment((UnitListSortWindow.SelectType) num2);
                break;
              }
              break;
            }
            break;
          }
          break;
      }
      return -1;
    }

    public static long GetSortPriority(int main, int pri1, int pri2, int pri3, int pri4, int pri5)
    {
      return (long) (main & (int) ushort.MaxValue) << 40 | (long) (pri1 & (int) byte.MaxValue) << 32 | (long) (pri2 & (int) byte.MaxValue) << 24 | (long) (pri3 & (int) byte.MaxValue) << 16 | (long) (pri4 & (int) byte.MaxValue) << 8 | (long) (pri5 & (int) byte.MaxValue);
    }

    public static long GetSortPriority(UnitListWindow.Data data, UnitListSortWindow.SelectType type)
    {
      if (data.param == null)
        return 0;
      UnitParam unitParam = data.param;
      UnitData unit = data.unit;
      switch (type)
      {
        case UnitListSortWindow.SelectType.TIME:
          return 0;
        case UnitListSortWindow.SelectType.RARITY:
          return UnitListSortWindow.GetSortPriority(UnitListSortWindow.GetSortStatus(data, type), unit.Lv, 0, unit.CurrentJob.Rank, (int) unitParam.raremax, (int) unitParam.rare);
        case UnitListSortWindow.SelectType.LEVEL:
          return UnitListSortWindow.GetSortPriority(UnitListSortWindow.GetSortStatus(data, type), 0, unit.Rarity, unit.CurrentJob.Rank, (int) unitParam.raremax, (int) unitParam.rare);
        case UnitListSortWindow.SelectType.TOTAL:
          return UnitListSortWindow.GetSortPriority(UnitListSortWindow.GetSortStatus(data, type), unit.Lv, unit.Rarity, unit.CurrentJob.Rank, (int) unitParam.raremax, (int) unitParam.rare);
        default:
          if (type != UnitListSortWindow.SelectType.ATK && type != UnitListSortWindow.SelectType.DEF && (type != UnitListSortWindow.SelectType.MAG && type != UnitListSortWindow.SelectType.MND) && (type != UnitListSortWindow.SelectType.HP && type != UnitListSortWindow.SelectType.SPD && type != UnitListSortWindow.SelectType.COMBINATION))
          {
            if (type == UnitListSortWindow.SelectType.JOBRANK)
              return UnitListSortWindow.GetSortPriority(UnitListSortWindow.GetSortStatus(data, type), unit.Lv, unit.Rarity, 0, (int) unitParam.raremax, (int) unitParam.rare);
            if (type == UnitListSortWindow.SelectType.AWAKE)
              goto case UnitListSortWindow.SelectType.TOTAL;
            else
              goto case UnitListSortWindow.SelectType.TIME;
          }
          else
            goto case UnitListSortWindow.SelectType.TOTAL;
      }
    }

    public static int GetSortStatus(UnitListWindow.Data data, UnitListSortWindow.SelectType type)
    {
      if (data.param == null)
        return 0;
      UnitParam unitParam = data.param;
      UnitData unit = data.unit;
      StatusParam statusParam = unitParam.ini_status.param;
      if (unit != null)
        statusParam = unit.Status.param;
      switch (type)
      {
        case UnitListSortWindow.SelectType.TIME:
          return 0;
        case UnitListSortWindow.SelectType.RARITY:
          if (unit != null)
            return unit.Rarity;
          return 1;
        case UnitListSortWindow.SelectType.LEVEL:
          if (unit != null)
            return unit.Lv;
          return 1;
        case UnitListSortWindow.SelectType.TOTAL:
          return (int) statusParam.atk + (int) statusParam.def + (int) statusParam.mag + (int) statusParam.mnd + (int) statusParam.spd + (int) statusParam.dex + (int) statusParam.cri + (int) statusParam.luk;
        default:
          if (type == UnitListSortWindow.SelectType.ATK)
            return (int) statusParam.atk;
          if (type == UnitListSortWindow.SelectType.DEF)
            return (int) statusParam.def;
          if (type == UnitListSortWindow.SelectType.MAG)
            return (int) statusParam.mag;
          if (type == UnitListSortWindow.SelectType.MND)
            return (int) statusParam.mnd;
          if (type == UnitListSortWindow.SelectType.HP)
            return (int) statusParam.hp;
          if (type == UnitListSortWindow.SelectType.SPD)
            return (int) statusParam.spd;
          if (type != UnitListSortWindow.SelectType.COMBINATION)
          {
            if (type != UnitListSortWindow.SelectType.JOBRANK)
            {
              if (type == UnitListSortWindow.SelectType.AWAKE)
              {
                if (unit != null)
                  return unit.AwakeLv;
                return 1;
              }
              goto case UnitListSortWindow.SelectType.TIME;
            }
            else
            {
              if (unit != null)
                return unit.CurrentJob.Rank;
              return 1;
            }
          }
          else
          {
            if (unit != null)
              return unit.GetCombination();
            return 1;
          }
      }
    }

    public static UnitListSortWindow.SelectType ConvertSortMode(GameUtility.UnitSortModes oldMode)
    {
      switch (oldMode)
      {
        case GameUtility.UnitSortModes.Time:
          return UnitListSortWindow.SelectType.TIME;
        case GameUtility.UnitSortModes.Level:
          return UnitListSortWindow.SelectType.LEVEL;
        case GameUtility.UnitSortModes.JobRank:
          return UnitListSortWindow.SelectType.JOBRANK;
        case GameUtility.UnitSortModes.HP:
          return UnitListSortWindow.SelectType.HP;
        case GameUtility.UnitSortModes.Atk:
          return UnitListSortWindow.SelectType.ATK;
        case GameUtility.UnitSortModes.Def:
          return UnitListSortWindow.SelectType.DEF;
        case GameUtility.UnitSortModes.Mag:
          return UnitListSortWindow.SelectType.MAG;
        case GameUtility.UnitSortModes.Mnd:
          return UnitListSortWindow.SelectType.MND;
        case GameUtility.UnitSortModes.Spd:
          return UnitListSortWindow.SelectType.SPD;
        case GameUtility.UnitSortModes.Total:
          return UnitListSortWindow.SelectType.TOTAL;
        case GameUtility.UnitSortModes.Awake:
          return UnitListSortWindow.SelectType.AWAKE;
        case GameUtility.UnitSortModes.Combination:
          return UnitListSortWindow.SelectType.COMBINATION;
        case GameUtility.UnitSortModes.Rarity:
          return UnitListSortWindow.SelectType.RARITY;
        case GameUtility.UnitSortModes.Name:
          return UnitListSortWindow.SelectType.NAME;
        default:
          return UnitListSortWindow.SelectType.NONE;
      }
    }

    public static UnitListSortWindow.SelectType ConvertReverse(bool isReverse)
    {
      return isReverse ? UnitListSortWindow.SelectType.SYOJYUN : UnitListSortWindow.SelectType.KOUJYUN;
    }

    public static GameUtility.UnitSortModes ConvertSelectType(UnitListSortWindow.SelectType selectType)
    {
      switch (selectType)
      {
        case UnitListSortWindow.SelectType.RARITY:
          return GameUtility.UnitSortModes.Rarity;
        case UnitListSortWindow.SelectType.LEVEL:
          return GameUtility.UnitSortModes.Level;
        default:
          if (selectType == UnitListSortWindow.SelectType.TOTAL)
            return GameUtility.UnitSortModes.Total;
          if (selectType == UnitListSortWindow.SelectType.ATK)
            return GameUtility.UnitSortModes.Atk;
          if (selectType == UnitListSortWindow.SelectType.DEF)
            return GameUtility.UnitSortModes.Def;
          if (selectType == UnitListSortWindow.SelectType.MAG)
            return GameUtility.UnitSortModes.Mag;
          if (selectType == UnitListSortWindow.SelectType.MND)
            return GameUtility.UnitSortModes.Mnd;
          if (selectType == UnitListSortWindow.SelectType.HP)
            return GameUtility.UnitSortModes.HP;
          if (selectType == UnitListSortWindow.SelectType.SPD)
            return GameUtility.UnitSortModes.Spd;
          if (selectType == UnitListSortWindow.SelectType.COMBINATION)
            return GameUtility.UnitSortModes.Combination;
          if (selectType == UnitListSortWindow.SelectType.JOBRANK)
            return GameUtility.UnitSortModes.JobRank;
          if (selectType == UnitListSortWindow.SelectType.AWAKE)
            return GameUtility.UnitSortModes.Awake;
          return selectType == UnitListSortWindow.SelectType.NAME ? GameUtility.UnitSortModes.Name : GameUtility.UnitSortModes.Time;
      }
    }

    public static Sprite GetIcon(UnitListSortWindow.SelectType selectType)
    {
      return GameSettings.Instance.GetUnitSortModeIcon(UnitListSortWindow.ConvertSelectType(selectType));
    }

    public static string GetText(UnitListSortWindow.SelectType selectType)
    {
      return LocalizedText.Get("sys.SORT_" + selectType.ToString());
    }

    public enum Result
    {
      NONE,
      CONFIRM,
      CANCEL,
    }

    public enum SelectType
    {
      NONE = 0,
      TIME = 1,
      RARITY = 2,
      LEVEL = 4,
      TOTAL = 8,
      ATK = 16, // 0x00000010
      DEF = 32, // 0x00000020
      MAG = 64, // 0x00000040
      MND = 128, // 0x00000080
      HP = 256, // 0x00000100
      SPD = 512, // 0x00000200
      COMBINATION = 1024, // 0x00000400
      JOBRANK = 2048, // 0x00000800
      AWAKE = 4096, // 0x00001000
      NAME = 8192, // 0x00002000
      SYOJYUN = 16777216, // 0x01000000
      KOUJYUN = 33554432, // 0x02000000
    }

    [Serializable]
    public class SerializeParam : FlowWindowBase.SerializeParamBase
    {
      public override System.Type type
      {
        get
        {
          return typeof (UnitListSortWindow);
        }
      }
    }
  }
}
