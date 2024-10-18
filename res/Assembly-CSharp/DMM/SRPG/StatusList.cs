// Decompiled with JetBrains decompiler
// Type: SRPG.StatusList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class StatusList : MonoBehaviour
  {
    public StatusListItem ListItem;
    public bool ShowSign;
    private List<StatusListItem> mItems = new List<StatusListItem>();
    private Color mDefaultValueColor;
    private Color mDefaultBonusColor;

    private void Awake()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ListItem, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ListItem.Value, (UnityEngine.Object) null))
        this.mDefaultValueColor = ((Graphic) this.ListItem.Value).color;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ListItem, (UnityEngine.Object) null) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ListItem.Bonus, (UnityEngine.Object) null))
        return;
      this.mDefaultBonusColor = ((Graphic) this.ListItem.Bonus).color;
    }

    private void Start()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ListItem, (UnityEngine.Object) null) || !((Component) this.ListItem).gameObject.activeInHierarchy)
        return;
      ((Component) this.ListItem).gameObject.SetActive(false);
    }

    public void SetValues(
      BaseStatus paramAdd,
      BaseStatus paramMul,
      BaseStatus modAdd,
      BaseStatus modMul,
      bool isSecret = false)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ListItem, (UnityEngine.Object) null))
      {
        DebugUtility.LogWarning(((Component) this).gameObject.GetPath() + ": ListItem not set");
      }
      else
      {
        int index1 = 0;
        string[] names = Enum.GetNames(typeof (ParamTypes));
        Array values = Enum.GetValues(typeof (ParamTypes));
        for (int index2 = 0; index2 < values.Length; ++index2)
        {
          ParamTypes type = (ParamTypes) values.GetValue(index2);
          int num1 = paramAdd[type];
          int bonus1 = modAdd[type] - num1;
          if (num1 != 0 && index2 != 2)
          {
            this.AddValue(index1, names[index2], num1, bonus1, false, isSecret);
            ++index1;
          }
          int num2 = paramMul[type];
          int bonus2 = modMul[type] - num2;
          if (num2 != 0 && index2 != 2)
          {
            this.AddValue(index1, names[index2], num2, bonus2, true, isSecret);
            ++index1;
          }
        }
        List<string> stringList1 = new List<string>();
        for (int index3 = 0; index3 < paramAdd.tokkou.Count; ++index3)
        {
          TokkouValue tokkouValue = paramAdd.tokkou[index3];
          if (!stringList1.Contains(tokkouValue.tag))
            stringList1.Add(tokkouValue.tag);
        }
        for (int index4 = 0; index4 < modAdd.tokkou.Count; ++index4)
        {
          TokkouValue tokkouValue = modAdd.tokkou[index4];
          if (!stringList1.Contains(tokkouValue.tag))
            stringList1.Add(tokkouValue.tag);
        }
        for (int index5 = 0; index5 < stringList1.Count; ++index5)
        {
          string str = stringList1[index5];
          TokkouValue tokkouValue1 = paramAdd.tokkou.Find(str);
          TokkouValue tokkouValue2 = modAdd.tokkou.Find(str);
          int num = tokkouValue1 == null ? 0 : (int) tokkouValue1.value;
          int bonus = (tokkouValue2 == null ? 0 : (int) tokkouValue2.value) - num;
          this.AddValue(index1, names[153], num, bonus, false, isSecret, str_tk: str);
          ++index1;
        }
        List<string> stringList2 = new List<string>();
        for (int index6 = 0; index6 < paramAdd.tokubou.Count; ++index6)
        {
          TokkouValue tokkouValue = paramAdd.tokubou[index6];
          if (!stringList2.Contains(tokkouValue.tag))
            stringList2.Add(tokkouValue.tag);
        }
        for (int index7 = 0; index7 < modAdd.tokubou.Count; ++index7)
        {
          TokkouValue tokkouValue = modAdd.tokubou[index7];
          if (!stringList2.Contains(tokkouValue.tag))
            stringList2.Add(tokkouValue.tag);
        }
        for (int index8 = 0; index8 < stringList2.Count; ++index8)
        {
          string str = stringList2[index8];
          TokkouValue tokkouValue3 = paramAdd.tokubou.Find(str);
          TokkouValue tokkouValue4 = modAdd.tokubou.Find(str);
          int num = tokkouValue3 == null ? 0 : (int) tokkouValue3.value;
          int bonus = (tokkouValue4 == null ? 0 : (int) tokkouValue4.value) - num;
          this.AddValue(index1, names[190], num, bonus, false, isSecret, str_tk: str);
          ++index1;
        }
        for (; index1 < this.mItems.Count; ++index1)
          ((Component) this.mItems[index1]).gameObject.SetActive(false);
      }
    }

    public void SetValues(BaseStatus paramAdd, BaseStatus paramMul, bool isSecret = false)
    {
      this.SetValues(paramAdd, paramMul, paramAdd, paramMul, isSecret);
    }

    private void AddValue(
      int index,
      string type,
      int value,
      int bonus,
      bool multiply,
      bool isSecret = false,
      bool use_bonus_color = false,
      string str_tk = null)
    {
      if (this.mItems.Count <= index)
      {
        StatusListItem statusListItem = UnityEngine.Object.Instantiate<StatusListItem>(this.ListItem);
        ((Component) statusListItem).transform.SetParent(((Component) this).transform, false);
        this.mItems.Add(statusListItem);
      }
      StatusListItem mItem = this.mItems[index];
      ((Component) mItem).gameObject.SetActive(true);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) mItem.Value, (UnityEngine.Object) null))
      {
        ((Component) mItem.Value).gameObject.SetActive(false);
        ((Graphic) mItem.Value).color = !use_bonus_color || bonus == 0 ? this.mDefaultValueColor : this.mDefaultBonusColor;
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) mItem.Bonus, (UnityEngine.Object) null))
        ((Component) mItem.Bonus).gameObject.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) mItem.Label, (UnityEngine.Object) null))
        mItem.Label.text = string.IsNullOrEmpty(str_tk) ? LocalizedText.Get("sys." + type) : string.Format(LocalizedText.Get("sys." + type), (object) str_tk);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) mItem.Value, (UnityEngine.Object) null))
      {
        string str = !isSecret ? value.ToString() : "???";
        if (this.ShowSign && value > 0)
          str = "+" + str;
        if (multiply)
          str += "%";
        mItem.Value.text = str;
        ((Component) mItem.Value).gameObject.SetActive(true);
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) mItem.Bonus, (UnityEngine.Object) null) || bonus == 0 || use_bonus_color)
        return;
      string str1 = bonus.ToString();
      if (this.ShowSign && bonus > 0)
        str1 = "+" + str1;
      if (multiply)
        str1 += "%";
      mItem.Bonus.text = str1;
      ((Component) mItem.Bonus).gameObject.SetActive(true);
    }

    public void SetValuesAfterOnly(
      BaseStatus paramAdd,
      BaseStatus paramMul,
      BaseStatus modAdd,
      BaseStatus modMul,
      bool isSecret = false,
      bool use_bonus_color = false)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ListItem, (UnityEngine.Object) null))
      {
        DebugUtility.LogWarning(((Component) this).gameObject.GetPath() + ": ListItem not set");
      }
      else
      {
        int index1 = 0;
        string[] names = Enum.GetNames(typeof (ParamTypes));
        Array values = Enum.GetValues(typeof (ParamTypes));
        for (int index2 = 0; index2 < values.Length; ++index2)
        {
          int num1 = modAdd[(ParamTypes) values.GetValue(index2)];
          int bonus1 = num1 - paramAdd[(ParamTypes) values.GetValue(index2)];
          if (num1 != 0 && index2 != 2)
          {
            this.AddValue(index1, names[index2], num1, bonus1, false, isSecret, use_bonus_color);
            ++index1;
          }
          int num2 = modMul[(ParamTypes) values.GetValue(index2)];
          int bonus2 = num2 - paramMul[(ParamTypes) values.GetValue(index2)];
          if (num2 != 0 && index2 != 2)
          {
            this.AddValue(index1, names[index2], num2, bonus2, true, isSecret, use_bonus_color);
            ++index1;
          }
        }
        List<string> stringList1 = new List<string>();
        for (int index3 = 0; index3 < paramAdd.tokkou.Count; ++index3)
        {
          TokkouValue tokkouValue = paramAdd.tokkou[index3];
          if (!stringList1.Contains(tokkouValue.tag))
            stringList1.Add(tokkouValue.tag);
        }
        for (int index4 = 0; index4 < modAdd.tokkou.Count; ++index4)
        {
          TokkouValue tokkouValue = modAdd.tokkou[index4];
          if (!stringList1.Contains(tokkouValue.tag))
            stringList1.Add(tokkouValue.tag);
        }
        for (int index5 = 0; index5 < stringList1.Count; ++index5)
        {
          string str = stringList1[index5];
          TokkouValue tokkouValue1 = paramAdd.tokkou.Find(str);
          TokkouValue tokkouValue2 = modAdd.tokkou.Find(str);
          int num = tokkouValue2 == null ? 0 : (int) tokkouValue2.value;
          int bonus = num - (tokkouValue1 == null ? 0 : (int) tokkouValue1.value);
          this.AddValue(index1, names[153], num, bonus, false, isSecret, str_tk: str);
          ++index1;
        }
        List<string> stringList2 = new List<string>();
        for (int index6 = 0; index6 < paramAdd.tokubou.Count; ++index6)
        {
          TokkouValue tokkouValue = paramAdd.tokubou[index6];
          if (!stringList2.Contains(tokkouValue.tag))
            stringList2.Add(tokkouValue.tag);
        }
        for (int index7 = 0; index7 < modAdd.tokubou.Count; ++index7)
        {
          TokkouValue tokkouValue = modAdd.tokubou[index7];
          if (!stringList2.Contains(tokkouValue.tag))
            stringList2.Add(tokkouValue.tag);
        }
        for (int index8 = 0; index8 < stringList2.Count; ++index8)
        {
          string str = stringList2[index8];
          TokkouValue tokkouValue3 = paramAdd.tokubou.Find(str);
          TokkouValue tokkouValue4 = modAdd.tokubou.Find(str);
          int num = tokkouValue4 == null ? 0 : (int) tokkouValue4.value;
          int bonus = num - (tokkouValue3 == null ? 0 : (int) tokkouValue3.value);
          this.AddValue(index1, names[190], num, bonus, false, isSecret, str_tk: str);
          ++index1;
        }
        for (; index1 < this.mItems.Count; ++index1)
          ((Component) this.mItems[index1]).gameObject.SetActive(false);
      }
    }

    public void SetValues_TotalAndBonus(
      BaseStatus paramAdd,
      BaseStatus paramMul,
      BaseStatus modAdd,
      BaseStatus modMul,
      BaseStatus paramBonusAdd,
      BaseStatus paramBonusMul,
      BaseStatus modBonusAdd,
      BaseStatus modBonusMul)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ListItem, (UnityEngine.Object) null))
      {
        DebugUtility.LogWarning(((Component) this).gameObject.GetPath() + ": ListItem not set");
      }
      else
      {
        int index1 = 0;
        string[] names = Enum.GetNames(typeof (ParamTypes));
        Array values = Enum.GetValues(typeof (ParamTypes));
        Dictionary<ParamTypes, StatusList.ParamValues> dictionary1 = new Dictionary<ParamTypes, StatusList.ParamValues>();
        Dictionary<ParamTypes, StatusList.ParamValues> dictionary2 = new Dictionary<ParamTypes, StatusList.ParamValues>();
        for (int index2 = 0; index2 < values.Length; ++index2)
        {
          ParamTypes paramTypes = (ParamTypes) values.GetValue(index2);
          if (paramTypes != ParamTypes.HpMax)
          {
            int num1 = modAdd[paramTypes] + modBonusAdd[paramTypes];
            int num2 = num1 - (paramAdd[paramTypes] + paramBonusAdd[paramTypes]);
            if (num1 != 0)
            {
              if (!dictionary1.ContainsKey(paramTypes))
                dictionary1.Add(paramTypes, new StatusList.ParamValues());
              dictionary1[paramTypes].main_value += num1;
              dictionary1[paramTypes].is_def_main = num2 != 0;
            }
            int num3 = modBonusAdd[paramTypes];
            int num4 = num3 - paramBonusAdd[paramTypes];
            if (num3 != 0)
            {
              if (!dictionary1.ContainsKey(paramTypes))
                dictionary1.Add(paramTypes, new StatusList.ParamValues());
              dictionary1[paramTypes].bonus_value += num3;
              dictionary1[paramTypes].is_def_bonus = num4 != 0;
            }
          }
        }
        for (int index3 = 0; index3 < values.Length; ++index3)
        {
          ParamTypes paramTypes = (ParamTypes) values.GetValue(index3);
          if (paramTypes != ParamTypes.HpMax)
          {
            int num5 = modMul[paramTypes] + modBonusMul[paramTypes];
            int num6 = num5 - (paramMul[paramTypes] + paramBonusMul[paramTypes]);
            if (num5 != 0)
            {
              if (!dictionary2.ContainsKey(paramTypes))
                dictionary2.Add(paramTypes, new StatusList.ParamValues());
              dictionary2[paramTypes].main_value += num5;
              dictionary2[paramTypes].is_def_main = num6 != 0;
            }
            int num7 = modBonusMul[paramTypes];
            int num8 = num7 - paramBonusMul[paramTypes];
            if (num7 != 0)
            {
              if (!dictionary2.ContainsKey(paramTypes))
                dictionary2.Add(paramTypes, new StatusList.ParamValues());
              dictionary2[paramTypes].bonus_value += num7;
              dictionary2[paramTypes].is_def_bonus = num8 != 0;
            }
          }
        }
        foreach (ParamTypes key in dictionary1.Keys)
        {
          StatusList.ParamValues paramValues = dictionary1[key];
          string type = names[(int) key];
          this.AddValue_TotalAndBonus(index1, type, paramValues.main_value, paramValues.bonus_value, paramValues.is_def_main, paramValues.is_def_bonus, false);
          ++index1;
        }
        foreach (ParamTypes key in dictionary2.Keys)
        {
          StatusList.ParamValues paramValues = dictionary2[key];
          string type = names[(int) key];
          this.AddValue_TotalAndBonus(index1, type, paramValues.main_value, paramValues.bonus_value, paramValues.is_def_main, paramValues.is_def_bonus, true);
          ++index1;
        }
        List<string> stringList1 = new List<string>();
        for (int index4 = 0; index4 < paramAdd.tokkou.Count; ++index4)
        {
          TokkouValue tokkouValue = paramAdd.tokkou[index4];
          if (!stringList1.Contains(tokkouValue.tag))
            stringList1.Add(tokkouValue.tag);
        }
        for (int index5 = 0; index5 < paramBonusAdd.tokkou.Count; ++index5)
        {
          TokkouValue tokkouValue = paramBonusAdd.tokkou[index5];
          if (!stringList1.Contains(tokkouValue.tag))
            stringList1.Add(tokkouValue.tag);
        }
        for (int index6 = 0; index6 < modAdd.tokkou.Count; ++index6)
        {
          TokkouValue tokkouValue = modAdd.tokkou[index6];
          if (!stringList1.Contains(tokkouValue.tag))
            stringList1.Add(tokkouValue.tag);
        }
        for (int index7 = 0; index7 < modBonusAdd.tokkou.Count; ++index7)
        {
          TokkouValue tokkouValue = modBonusAdd.tokkou[index7];
          if (!stringList1.Contains(tokkouValue.tag))
            stringList1.Add(tokkouValue.tag);
        }
        for (int index8 = 0; index8 < stringList1.Count; ++index8)
        {
          string str = stringList1[index8];
          TokkouValue tokkouValue1 = paramAdd.tokkou.Find(str);
          TokkouValue tokkouValue2 = paramBonusAdd.tokkou.Find(str);
          TokkouValue tokkouValue3 = modAdd.tokkou.Find(str);
          TokkouValue tokkouValue4 = modBonusAdd.tokkou.Find(str);
          int main_value = (tokkouValue3 == null ? 0 : (int) tokkouValue3.value) + (tokkouValue4 == null ? 0 : (int) tokkouValue4.value);
          bool is_def_main = main_value - ((tokkouValue1 == null ? 0 : (int) tokkouValue1.value) + (tokkouValue2 == null ? 0 : (int) tokkouValue2.value)) != 0;
          int bonus_value = tokkouValue4 == null ? 0 : (int) tokkouValue4.value;
          bool is_def_bonus = bonus_value - (tokkouValue2 == null ? 0 : (int) tokkouValue2.value) != 0;
          this.AddValue_TotalAndBonus(index1, names[153], main_value, bonus_value, is_def_main, is_def_bonus, false, str);
          ++index1;
        }
        List<string> stringList2 = new List<string>();
        for (int index9 = 0; index9 < paramAdd.tokubou.Count; ++index9)
        {
          TokkouValue tokkouValue = paramAdd.tokubou[index9];
          if (!stringList2.Contains(tokkouValue.tag))
            stringList2.Add(tokkouValue.tag);
        }
        for (int index10 = 0; index10 < paramBonusAdd.tokubou.Count; ++index10)
        {
          TokkouValue tokkouValue = paramBonusAdd.tokubou[index10];
          if (!stringList2.Contains(tokkouValue.tag))
            stringList2.Add(tokkouValue.tag);
        }
        for (int index11 = 0; index11 < modAdd.tokubou.Count; ++index11)
        {
          TokkouValue tokkouValue = modAdd.tokubou[index11];
          if (!stringList2.Contains(tokkouValue.tag))
            stringList2.Add(tokkouValue.tag);
        }
        for (int index12 = 0; index12 < modBonusAdd.tokubou.Count; ++index12)
        {
          TokkouValue tokkouValue = modBonusAdd.tokubou[index12];
          if (!stringList2.Contains(tokkouValue.tag))
            stringList2.Add(tokkouValue.tag);
        }
        for (int index13 = 0; index13 < stringList2.Count; ++index13)
        {
          string str = stringList2[index13];
          TokkouValue tokkouValue5 = paramAdd.tokubou.Find(str);
          TokkouValue tokkouValue6 = paramBonusAdd.tokubou.Find(str);
          TokkouValue tokkouValue7 = modAdd.tokubou.Find(str);
          TokkouValue tokkouValue8 = modBonusAdd.tokubou.Find(str);
          int main_value = (tokkouValue7 == null ? 0 : (int) tokkouValue7.value) + (tokkouValue8 == null ? 0 : (int) tokkouValue8.value);
          bool is_def_main = main_value - ((tokkouValue5 == null ? 0 : (int) tokkouValue5.value) + (tokkouValue6 == null ? 0 : (int) tokkouValue6.value)) != 0;
          int bonus_value = tokkouValue8 == null ? 0 : (int) tokkouValue8.value;
          bool is_def_bonus = bonus_value - (tokkouValue6 == null ? 0 : (int) tokkouValue6.value) != 0;
          this.AddValue_TotalAndBonus(index1, names[190], main_value, bonus_value, is_def_main, is_def_bonus, false, str);
          ++index1;
        }
        for (; index1 < this.mItems.Count; ++index1)
          ((Component) this.mItems[index1]).gameObject.SetActive(false);
      }
    }

    private void AddValue_TotalAndBonus(
      int index,
      string type,
      int main_value,
      int bonus_value,
      bool is_def_main,
      bool is_def_bonus,
      bool multiply,
      string str_tk = null)
    {
      Color color1 = !is_def_main ? this.mDefaultValueColor : this.mDefaultBonusColor;
      Color color2 = !is_def_main ? this.mDefaultBonusColor : this.mDefaultBonusColor;
      if (this.mItems.Count <= index)
      {
        StatusListItem statusListItem = UnityEngine.Object.Instantiate<StatusListItem>(this.ListItem);
        ((Component) statusListItem).transform.SetParent(((Component) this).transform, false);
        this.mItems.Add(statusListItem);
      }
      StatusListItem mItem = this.mItems[index];
      ((Component) mItem).gameObject.SetActive(true);
      Text text = mItem.Value;
      Text bonus = mItem.Bonus;
      ((Component) text).gameObject.SetActive(false);
      ((Component) bonus).gameObject.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) mItem.Label, (UnityEngine.Object) null))
        mItem.Label.text = string.IsNullOrEmpty(str_tk) ? LocalizedText.Get("sys." + type) : string.Format(LocalizedText.Get("sys." + type), (object) str_tk);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) text, (UnityEngine.Object) null))
      {
        string str = main_value.ToString();
        if (this.ShowSign && main_value > 0)
          str = "+" + str;
        if (multiply)
          str += "%";
        text.text = str;
        ((Graphic) text).color = color1;
        ((Component) text).gameObject.SetActive(true);
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) bonus, (UnityEngine.Object) null) || bonus_value == 0)
        return;
      string str1 = bonus_value.ToString();
      if (this.ShowSign && bonus_value > 0)
        str1 = "+" + str1;
      if (multiply)
        str1 += "%";
      bonus.text = string.Format(LocalizedText.Get("sys.STATUS_FORMAT_PARAM_BONUS"), (object) str1);
      ((Graphic) bonus).color = color2;
      ((Component) bonus).gameObject.SetActive(true);
    }

    public void SetValues_Restrict(
      BaseStatus paramBaseAdd,
      BaseStatus paramBaseMul,
      BaseStatus paramBonusAdd,
      BaseStatus paramBonusMul,
      bool new_param_only)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ListItem, (UnityEngine.Object) null))
      {
        DebugUtility.LogWarning(((Component) this).gameObject.GetPath() + ": ListItem not set");
      }
      else
      {
        int index1 = 0;
        string[] names = Enum.GetNames(typeof (ParamTypes));
        Array values = Enum.GetValues(typeof (ParamTypes));
        for (int index2 = 0; index2 < values.Length; ++index2)
        {
          ParamTypes type = (ParamTypes) values.GetValue(index2);
          if (type != ParamTypes.HpMax)
          {
            int num1 = paramBaseAdd[type];
            int bonus1 = paramBonusAdd[type];
            if (bonus1 != 0)
            {
              if (num1 == 0 && new_param_only)
              {
                this.AddValue(index1, names[index2], bonus1, bonus1, false);
                ++index1;
              }
              if (num1 != 0 && !new_param_only)
              {
                this.AddValue(index1, names[index2], bonus1, bonus1, false);
                ++index1;
              }
            }
            int num2 = paramBaseMul[type];
            int bonus2 = paramBonusMul[type];
            if (bonus2 != 0)
            {
              if (num2 == 0 && new_param_only)
              {
                this.AddValue(index1, names[index2], bonus2, bonus2, true);
                ++index1;
              }
              if (num2 != 0 && !new_param_only)
              {
                this.AddValue(index1, names[index2], bonus2, bonus2, true);
                ++index1;
              }
            }
          }
        }
        List<string> stringList1 = new List<string>();
        for (int index3 = 0; index3 < paramBaseAdd.tokkou.Count; ++index3)
        {
          TokkouValue tokkouValue = paramBaseAdd.tokkou[index3];
          if (!stringList1.Contains(tokkouValue.tag))
            stringList1.Add(tokkouValue.tag);
        }
        for (int index4 = 0; index4 < paramBonusAdd.tokkou.Count; ++index4)
        {
          TokkouValue tokkouValue = paramBonusAdd.tokkou[index4];
          if (!stringList1.Contains(tokkouValue.tag))
            stringList1.Add(tokkouValue.tag);
        }
        for (int index5 = 0; index5 < stringList1.Count; ++index5)
        {
          string str = stringList1[index5];
          TokkouValue tokkouValue1 = paramBaseAdd.tokkou.Find(str);
          TokkouValue tokkouValue2 = paramBonusAdd.tokkou.Find(str);
          int num = tokkouValue1 == null ? 0 : (int) tokkouValue1.value;
          int bonus = tokkouValue2 == null ? 0 : (int) tokkouValue2.value;
          if (bonus != 0 && (num == 0 && new_param_only || num != 0 && !new_param_only))
          {
            this.AddValue(index1, names[153], bonus, bonus, false, str_tk: str);
            ++index1;
          }
        }
        List<string> stringList2 = new List<string>();
        for (int index6 = 0; index6 < paramBaseAdd.tokubou.Count; ++index6)
        {
          TokkouValue tokkouValue = paramBaseAdd.tokubou[index6];
          if (!stringList2.Contains(tokkouValue.tag))
            stringList2.Add(tokkouValue.tag);
        }
        for (int index7 = 0; index7 < paramBonusAdd.tokubou.Count; ++index7)
        {
          TokkouValue tokkouValue = paramBonusAdd.tokubou[index7];
          if (!stringList2.Contains(tokkouValue.tag))
            stringList2.Add(tokkouValue.tag);
        }
        for (int index8 = 0; index8 < stringList2.Count; ++index8)
        {
          string str = stringList2[index8];
          TokkouValue tokkouValue3 = paramBaseAdd.tokubou.Find(str);
          TokkouValue tokkouValue4 = paramBonusAdd.tokubou.Find(str);
          int num = tokkouValue3 == null ? 0 : (int) tokkouValue3.value;
          int bonus = tokkouValue4 == null ? 0 : (int) tokkouValue4.value;
          if (bonus != 0 && (num == 0 && new_param_only || num != 0 && !new_param_only))
          {
            this.AddValue(index1, names[190], bonus, bonus, false, str_tk: str);
            ++index1;
          }
        }
        for (; index1 < this.mItems.Count; ++index1)
          ((Component) this.mItems[index1]).gameObject.SetActive(false);
      }
    }

    private class ParamValues
    {
      public bool is_def_main;
      public bool is_def_bonus;
      public int main_value;
      public int bonus_value;
    }
  }
}
