// Decompiled with JetBrains decompiler
// Type: SRPG.StatusList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class StatusList : MonoBehaviour
  {
    private List<StatusListItem> mItems = new List<StatusListItem>();
    public StatusListItem ListItem;
    public bool ShowSign;
    private Color mDefaultValueColor;
    private Color mDefaultBonusColor;

    private void Awake()
    {
      if ((UnityEngine.Object) this.ListItem != (UnityEngine.Object) null && (UnityEngine.Object) this.ListItem.Value != (UnityEngine.Object) null)
        this.mDefaultValueColor = this.ListItem.Value.color;
      if (!((UnityEngine.Object) this.ListItem != (UnityEngine.Object) null) || !((UnityEngine.Object) this.ListItem.Bonus != (UnityEngine.Object) null))
        return;
      this.mDefaultBonusColor = this.ListItem.Bonus.color;
    }

    private void Start()
    {
      if (!((UnityEngine.Object) this.ListItem != (UnityEngine.Object) null) || !this.ListItem.gameObject.activeInHierarchy)
        return;
      this.ListItem.gameObject.SetActive(false);
    }

    public void SetValues(BaseStatus paramAdd, BaseStatus paramMul, BaseStatus modAdd, BaseStatus modMul, bool isSecret = false)
    {
      if ((UnityEngine.Object) this.ListItem == (UnityEngine.Object) null)
      {
        DebugUtility.LogWarning(this.gameObject.GetPath((GameObject) null) + ": ListItem not set");
      }
      else
      {
        int index1 = 0;
        string[] names = Enum.GetNames(typeof (ParamTypes));
        Array values = Enum.GetValues(typeof (ParamTypes));
        for (int index2 = 0; index2 < values.Length; ++index2)
        {
          int num1 = (int) paramAdd[(ParamTypes) values.GetValue(index2)];
          int bonus1 = (int) modAdd[(ParamTypes) values.GetValue(index2)] - num1;
          if (num1 != 0 && index2 != 2)
          {
            this.AddValue(index1, names[index2], num1, bonus1, false, isSecret, false, (string) null);
            ++index1;
          }
          int num2 = (int) paramMul[(ParamTypes) values.GetValue(index2)];
          int bonus2 = (int) modMul[(ParamTypes) values.GetValue(index2)] - num2;
          if (num2 != 0 && index2 != 2)
          {
            this.AddValue(index1, names[index2], num2, bonus2, true, isSecret, false, (string) null);
            ++index1;
          }
        }
        List<string> stringList = new List<string>();
        for (int index2 = 0; index2 < paramAdd.tokkou.Count; ++index2)
        {
          TokkouValue tokkouValue = paramAdd.tokkou[index2];
          if (!stringList.Contains((string) tokkouValue.tag))
            stringList.Add((string) tokkouValue.tag);
        }
        for (int index2 = 0; index2 < modAdd.tokkou.Count; ++index2)
        {
          TokkouValue tokkouValue = modAdd.tokkou[index2];
          if (!stringList.Contains((string) tokkouValue.tag))
            stringList.Add((string) tokkouValue.tag);
        }
        for (int index2 = 0; index2 < stringList.Count; ++index2)
        {
          string str = stringList[index2];
          TokkouValue tokkouValue1 = paramAdd.tokkou.Find(str);
          TokkouValue tokkouValue2 = modAdd.tokkou.Find(str);
          int num = tokkouValue1 == null ? 0 : (int) tokkouValue1.value;
          int bonus = (tokkouValue2 == null ? 0 : (int) tokkouValue2.value) - num;
          this.AddValue(index1, names[153], num, bonus, false, isSecret, false, str);
          ++index1;
        }
        for (; index1 < this.mItems.Count; ++index1)
          this.mItems[index1].gameObject.SetActive(false);
      }
    }

    public void SetValues(BaseStatus paramAdd, BaseStatus paramMul, bool isSecret = false)
    {
      this.SetValues(paramAdd, paramMul, paramAdd, paramMul, isSecret);
    }

    private void AddValue(int index, string type, int value, int bonus, bool multiply, bool isSecret = false, bool use_bonus_color = false, string str_tk = null)
    {
      if (this.mItems.Count <= index)
      {
        StatusListItem statusListItem = UnityEngine.Object.Instantiate<StatusListItem>(this.ListItem);
        statusListItem.transform.SetParent(this.transform, false);
        this.mItems.Add(statusListItem);
      }
      StatusListItem mItem = this.mItems[index];
      mItem.gameObject.SetActive(true);
      if ((UnityEngine.Object) mItem.Value != (UnityEngine.Object) null)
      {
        mItem.Value.gameObject.SetActive(false);
        mItem.Value.color = !use_bonus_color || bonus == 0 ? this.mDefaultValueColor : this.mDefaultBonusColor;
      }
      if ((UnityEngine.Object) mItem.Bonus != (UnityEngine.Object) null)
        mItem.Bonus.gameObject.SetActive(false);
      if ((UnityEngine.Object) mItem.Label != (UnityEngine.Object) null)
        mItem.Label.text = string.IsNullOrEmpty(str_tk) ? LocalizedText.Get("sys." + type) : string.Format(LocalizedText.Get("sys." + type), (object) str_tk);
      if ((UnityEngine.Object) mItem.Value != (UnityEngine.Object) null)
      {
        string str = !isSecret ? value.ToString() : "???";
        if (this.ShowSign && value > 0)
          str = "+" + str;
        if (multiply)
          str += "%";
        mItem.Value.text = str;
        mItem.Value.gameObject.SetActive(true);
      }
      if (!((UnityEngine.Object) mItem.Bonus != (UnityEngine.Object) null) || bonus == 0 || use_bonus_color)
        return;
      string str1 = bonus.ToString();
      if (this.ShowSign && bonus > 0)
        str1 = "+" + str1;
      if (multiply)
        str1 += "%";
      mItem.Bonus.text = str1;
      mItem.Bonus.gameObject.SetActive(true);
    }

    public void SetValuesAfterOnly(BaseStatus paramAdd, BaseStatus paramMul, BaseStatus modAdd, BaseStatus modMul, bool isSecret = false, bool use_bonus_color = false)
    {
      if ((UnityEngine.Object) this.ListItem == (UnityEngine.Object) null)
      {
        DebugUtility.LogWarning(this.gameObject.GetPath((GameObject) null) + ": ListItem not set");
      }
      else
      {
        int index1 = 0;
        string[] names = Enum.GetNames(typeof (ParamTypes));
        Array values = Enum.GetValues(typeof (ParamTypes));
        for (int index2 = 0; index2 < values.Length; ++index2)
        {
          int num1 = (int) modAdd[(ParamTypes) values.GetValue(index2)];
          int bonus1 = num1 - (int) paramAdd[(ParamTypes) values.GetValue(index2)];
          if (num1 != 0 && index2 != 2)
          {
            this.AddValue(index1, names[index2], num1, bonus1, false, isSecret, use_bonus_color, (string) null);
            ++index1;
          }
          int num2 = (int) modMul[(ParamTypes) values.GetValue(index2)];
          int bonus2 = num2 - (int) paramMul[(ParamTypes) values.GetValue(index2)];
          if (num2 != 0 && index2 != 2)
          {
            this.AddValue(index1, names[index2], num2, bonus2, true, isSecret, use_bonus_color, (string) null);
            ++index1;
          }
        }
        List<string> stringList = new List<string>();
        for (int index2 = 0; index2 < paramAdd.tokkou.Count; ++index2)
        {
          TokkouValue tokkouValue = paramAdd.tokkou[index2];
          if (!stringList.Contains((string) tokkouValue.tag))
            stringList.Add((string) tokkouValue.tag);
        }
        for (int index2 = 0; index2 < modAdd.tokkou.Count; ++index2)
        {
          TokkouValue tokkouValue = modAdd.tokkou[index2];
          if (!stringList.Contains((string) tokkouValue.tag))
            stringList.Add((string) tokkouValue.tag);
        }
        for (int index2 = 0; index2 < stringList.Count; ++index2)
        {
          string str = stringList[index2];
          TokkouValue tokkouValue1 = paramAdd.tokkou.Find(str);
          TokkouValue tokkouValue2 = modAdd.tokkou.Find(str);
          int num = tokkouValue2 == null ? 0 : (int) tokkouValue2.value;
          int bonus = num - (tokkouValue1 == null ? 0 : (int) tokkouValue1.value);
          this.AddValue(index1, names[153], num, bonus, false, isSecret, false, str);
          ++index1;
        }
        for (; index1 < this.mItems.Count; ++index1)
          this.mItems[index1].gameObject.SetActive(false);
      }
    }

    public void SetValues_TotalAndBonus(BaseStatus paramAdd, BaseStatus paramMul, BaseStatus modAdd, BaseStatus modMul, BaseStatus paramBonusAdd, BaseStatus paramBonusMul, BaseStatus modBonusAdd, BaseStatus modBonusMul)
    {
      if ((UnityEngine.Object) this.ListItem == (UnityEngine.Object) null)
      {
        DebugUtility.LogWarning(this.gameObject.GetPath((GameObject) null) + ": ListItem not set");
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
          ParamTypes key = (ParamTypes) values.GetValue(index2);
          if (key != ParamTypes.HpMax)
          {
            int num1 = (int) modAdd[key] + (int) modBonusAdd[key];
            int num2 = num1 - ((int) paramAdd[key] + (int) paramBonusAdd[key]);
            if (num1 != 0)
            {
              if (!dictionary1.ContainsKey(key))
                dictionary1.Add(key, new StatusList.ParamValues());
              dictionary1[key].main_value += num1;
              dictionary1[key].is_def_main = num2 != 0;
            }
            int num3 = (int) modBonusAdd[key];
            int num4 = num3 - (int) paramBonusAdd[key];
            if (num3 != 0)
            {
              if (!dictionary1.ContainsKey(key))
                dictionary1.Add(key, new StatusList.ParamValues());
              dictionary1[key].bonus_value += num3;
              dictionary1[key].is_def_bonus = num4 != 0;
            }
          }
        }
        for (int index2 = 0; index2 < values.Length; ++index2)
        {
          ParamTypes key = (ParamTypes) values.GetValue(index2);
          if (key != ParamTypes.HpMax)
          {
            int num1 = (int) modMul[key] + (int) modBonusMul[key];
            int num2 = num1 - ((int) paramMul[key] + (int) paramBonusMul[key]);
            if (num1 != 0)
            {
              if (!dictionary2.ContainsKey(key))
                dictionary2.Add(key, new StatusList.ParamValues());
              dictionary2[key].main_value += num1;
              dictionary2[key].is_def_main = num2 != 0;
            }
            int num3 = (int) modBonusMul[key];
            int num4 = num3 - (int) paramBonusMul[key];
            if (num3 != 0)
            {
              if (!dictionary2.ContainsKey(key))
                dictionary2.Add(key, new StatusList.ParamValues());
              dictionary2[key].bonus_value += num3;
              dictionary2[key].is_def_bonus = num4 != 0;
            }
          }
        }
        foreach (ParamTypes key in dictionary1.Keys)
        {
          StatusList.ParamValues paramValues = dictionary1[key];
          string type = names[(int) key];
          this.AddValue_TotalAndBonus(index1, type, paramValues.main_value, paramValues.bonus_value, paramValues.is_def_main, paramValues.is_def_bonus, false, (string) null);
          ++index1;
        }
        foreach (ParamTypes key in dictionary2.Keys)
        {
          StatusList.ParamValues paramValues = dictionary2[key];
          string type = names[(int) key];
          this.AddValue_TotalAndBonus(index1, type, paramValues.main_value, paramValues.bonus_value, paramValues.is_def_main, paramValues.is_def_bonus, true, (string) null);
          ++index1;
        }
        List<string> stringList = new List<string>();
        for (int index2 = 0; index2 < paramAdd.tokkou.Count; ++index2)
        {
          TokkouValue tokkouValue = paramAdd.tokkou[index2];
          if (!stringList.Contains((string) tokkouValue.tag))
            stringList.Add((string) tokkouValue.tag);
        }
        for (int index2 = 0; index2 < paramBonusAdd.tokkou.Count; ++index2)
        {
          TokkouValue tokkouValue = paramBonusAdd.tokkou[index2];
          if (!stringList.Contains((string) tokkouValue.tag))
            stringList.Add((string) tokkouValue.tag);
        }
        for (int index2 = 0; index2 < modAdd.tokkou.Count; ++index2)
        {
          TokkouValue tokkouValue = modAdd.tokkou[index2];
          if (!stringList.Contains((string) tokkouValue.tag))
            stringList.Add((string) tokkouValue.tag);
        }
        for (int index2 = 0; index2 < modBonusAdd.tokkou.Count; ++index2)
        {
          TokkouValue tokkouValue = modBonusAdd.tokkou[index2];
          if (!stringList.Contains((string) tokkouValue.tag))
            stringList.Add((string) tokkouValue.tag);
        }
        for (int index2 = 0; index2 < stringList.Count; ++index2)
        {
          string str = stringList[index2];
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
        for (; index1 < this.mItems.Count; ++index1)
          this.mItems[index1].gameObject.SetActive(false);
      }
    }

    private void AddValue_TotalAndBonus(int index, string type, int main_value, int bonus_value, bool is_def_main, bool is_def_bonus, bool multiply, string str_tk = null)
    {
      Color color1 = !is_def_main ? this.mDefaultValueColor : this.mDefaultBonusColor;
      Color color2 = !is_def_main ? this.mDefaultBonusColor : this.mDefaultBonusColor;
      if (this.mItems.Count <= index)
      {
        StatusListItem statusListItem = UnityEngine.Object.Instantiate<StatusListItem>(this.ListItem);
        statusListItem.transform.SetParent(this.transform, false);
        this.mItems.Add(statusListItem);
      }
      StatusListItem mItem = this.mItems[index];
      mItem.gameObject.SetActive(true);
      Text text = mItem.Value;
      Text bonus = mItem.Bonus;
      text.gameObject.SetActive(false);
      bonus.gameObject.SetActive(false);
      if ((UnityEngine.Object) mItem.Label != (UnityEngine.Object) null)
        mItem.Label.text = string.IsNullOrEmpty(str_tk) ? LocalizedText.Get("sys." + type) : string.Format(LocalizedText.Get("sys." + type), (object) str_tk);
      if ((UnityEngine.Object) text != (UnityEngine.Object) null)
      {
        string str = main_value.ToString();
        if (this.ShowSign && main_value > 0)
          str = "+" + str;
        if (multiply)
          str += "%";
        text.text = str;
        text.color = color1;
        text.gameObject.SetActive(true);
      }
      if (!((UnityEngine.Object) bonus != (UnityEngine.Object) null) || bonus_value == 0)
        return;
      string str1 = bonus_value.ToString();
      if (this.ShowSign && bonus_value > 0)
        str1 = "+" + str1;
      if (multiply)
        str1 += "%";
      bonus.text = string.Format(LocalizedText.Get("sys.STATUS_FORMAT_PARAM_BONUS"), (object) str1);
      bonus.color = color2;
      bonus.gameObject.SetActive(true);
    }

    public void SetValues_Restrict(BaseStatus paramBaseAdd, BaseStatus paramBaseMul, BaseStatus paramBonusAdd, BaseStatus paramBonusMul, bool new_param_only)
    {
      if ((UnityEngine.Object) this.ListItem == (UnityEngine.Object) null)
      {
        DebugUtility.LogWarning(this.gameObject.GetPath((GameObject) null) + ": ListItem not set");
      }
      else
      {
        int index1 = 0;
        string[] names = Enum.GetNames(typeof (ParamTypes));
        Array values = Enum.GetValues(typeof (ParamTypes));
        for (int index2 = 0; index2 < values.Length; ++index2)
        {
          ParamTypes index3 = (ParamTypes) values.GetValue(index2);
          if (index3 != ParamTypes.HpMax)
          {
            int num1 = (int) paramBaseAdd[index3];
            int bonus1 = (int) paramBonusAdd[index3];
            if (bonus1 != 0)
            {
              if (num1 == 0 && new_param_only)
              {
                this.AddValue(index1, names[index2], bonus1, bonus1, false, false, false, (string) null);
                ++index1;
              }
              if (num1 != 0 && !new_param_only)
              {
                this.AddValue(index1, names[index2], bonus1, bonus1, false, false, false, (string) null);
                ++index1;
              }
            }
            int num2 = (int) paramBaseMul[index3];
            int bonus2 = (int) paramBonusMul[index3];
            if (bonus2 != 0)
            {
              if (num2 == 0 && new_param_only)
              {
                this.AddValue(index1, names[index2], bonus2, bonus2, true, false, false, (string) null);
                ++index1;
              }
              if (num2 != 0 && !new_param_only)
              {
                this.AddValue(index1, names[index2], bonus2, bonus2, true, false, false, (string) null);
                ++index1;
              }
            }
          }
        }
        List<string> stringList = new List<string>();
        for (int index2 = 0; index2 < paramBaseAdd.tokkou.Count; ++index2)
        {
          TokkouValue tokkouValue = paramBaseAdd.tokkou[index2];
          if (!stringList.Contains((string) tokkouValue.tag))
            stringList.Add((string) tokkouValue.tag);
        }
        for (int index2 = 0; index2 < paramBonusAdd.tokkou.Count; ++index2)
        {
          TokkouValue tokkouValue = paramBonusAdd.tokkou[index2];
          if (!stringList.Contains((string) tokkouValue.tag))
            stringList.Add((string) tokkouValue.tag);
        }
        for (int index2 = 0; index2 < stringList.Count; ++index2)
        {
          string str = stringList[index2];
          TokkouValue tokkouValue1 = paramBaseAdd.tokkou.Find(str);
          TokkouValue tokkouValue2 = paramBonusAdd.tokkou.Find(str);
          int num = tokkouValue1 == null ? 0 : (int) tokkouValue1.value;
          int bonus = tokkouValue2 == null ? 0 : (int) tokkouValue2.value;
          if (bonus != 0 && (num == 0 && new_param_only || num != 0 && !new_param_only))
          {
            this.AddValue(index1, names[153], bonus, bonus, false, false, false, str);
            ++index1;
          }
        }
        for (; index1 < this.mItems.Count; ++index1)
          this.mItems[index1].gameObject.SetActive(false);
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
