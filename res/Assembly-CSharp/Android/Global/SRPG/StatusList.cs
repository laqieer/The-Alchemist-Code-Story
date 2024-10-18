// Decompiled with JetBrains decompiler
// Type: SRPG.StatusList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class StatusList : MonoBehaviour
  {
    private List<StatusListItem> mItems = new List<StatusListItem>();
    public StatusListItem ListItem;
    public bool ShowSign;

    private void Start()
    {
      if (!((UnityEngine.Object) this.ListItem != (UnityEngine.Object) null) || !this.ListItem.gameObject.activeInHierarchy)
        return;
      this.ListItem.gameObject.SetActive(false);
    }

    public void SetValues(BaseStatus paramAdd, BaseStatus paramMul, BaseStatus modAdd, BaseStatus modMul)
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
            this.AddValue(index1, names[index2], num1, bonus1, false);
            ++index1;
          }
          int num2 = (int) paramMul[(ParamTypes) values.GetValue(index2)];
          int bonus2 = (int) modMul[(ParamTypes) values.GetValue(index2)] - num2;
          if (num2 != 0 && index2 != 2)
          {
            this.AddValue(index1, names[index2], num2, bonus2, true);
            ++index1;
          }
        }
        for (; index1 < this.mItems.Count; ++index1)
          this.mItems[index1].gameObject.SetActive(false);
      }
    }

    public void SetValues(BaseStatus paramAdd, BaseStatus paramMul)
    {
      this.SetValues(paramAdd, paramMul, paramAdd, paramMul);
    }

    private void AddValue(int index, string type, int value, int bonus, bool multiply)
    {
      if (this.mItems.Count <= index)
      {
        StatusListItem statusListItem = UnityEngine.Object.Instantiate<StatusListItem>(this.ListItem);
        statusListItem.transform.SetParent(this.transform, false);
        this.mItems.Add(statusListItem);
      }
      StatusListItem mItem = this.mItems[index];
      mItem.gameObject.SetActive(true);
      if ((UnityEngine.Object) mItem.Label != (UnityEngine.Object) null)
        mItem.Label.text = LocalizedText.Get("sys." + type);
      if ((UnityEngine.Object) mItem.Value != (UnityEngine.Object) null)
      {
        string str = value.ToString();
        if (this.ShowSign && value > 0)
          str = "+" + str;
        if (multiply)
          str += "%";
        mItem.Value.text = str;
      }
      if (!((UnityEngine.Object) mItem.Bonus != (UnityEngine.Object) null))
        return;
      if (bonus != 0)
      {
        string str = bonus.ToString();
        if (this.ShowSign && bonus > 0)
          str = "+" + str;
        if (multiply)
          str += "%";
        mItem.Bonus.text = str;
        mItem.Bonus.gameObject.SetActive(true);
      }
      else
        mItem.Bonus.gameObject.SetActive(false);
    }

    public void SetDirectValues(BaseStatus old_status, BaseStatus new_status)
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
          int oldStatu = (int) old_status[(ParamTypes) values.GetValue(index2)];
          int newStatu = (int) new_status[(ParamTypes) values.GetValue(index2)];
          if (oldStatu != newStatu && index2 != 2)
          {
            this.AddValue(index1, names[index2], oldStatu, newStatu, false);
            ++index1;
          }
        }
        for (; index1 < this.mItems.Count; ++index1)
          this.mItems[index1].gameObject.SetActive(false);
      }
    }
  }
}
