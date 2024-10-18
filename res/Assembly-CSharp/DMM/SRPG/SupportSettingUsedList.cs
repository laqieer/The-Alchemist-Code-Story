// Decompiled with JetBrains decompiler
// Type: SRPG.SupportSettingUsedList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class SupportSettingUsedList : MonoBehaviour
  {
    [SerializeField]
    private Text mTextForm;
    [SerializeField]
    private Text mTextTimes;
    [SerializeField]
    private Text mTextLast;
    [SerializeField]
    private Text mTextGold;
    [SerializeField]
    private ImageArray mElement;
    [SerializeField]
    private GameObject mLevel;
    [SerializeField]
    private GameObject mOverGold;

    private void Awake()
    {
      GameUtility.SetGameObjectActive(this.mOverGold, false);
      this.Refresh();
    }

    private void Refresh()
    {
      SupportUnitUsed dataOfClass = DataSource.FindDataOfClass<SupportUnitUsed>(((Component) this).gameObject, (SupportUnitUsed) null);
      if (dataOfClass == null)
        return;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mTextForm, (UnityEngine.Object) null) && dataOfClass.from != DateTime.MinValue)
        this.mTextForm.text = string.Format(LocalizedText.Get("sys.SUPPORT_SET_FROM"), (object) dataOfClass.from.Year, (object) dataOfClass.from.Month, (object) dataOfClass.from.Day);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mTextTimes, (UnityEngine.Object) null) && dataOfClass.times >= 0)
        this.mTextTimes.text = string.Format(LocalizedText.Get("sys.SUPPORT_SET_TIMES"), (object) dataOfClass.times);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mTextLast, (UnityEngine.Object) null) && dataOfClass.last != DateTime.MinValue)
        this.mTextLast.text = string.Format(LocalizedText.Get("sys.SUPPORT_SET_LAST"), (object) dataOfClass.last.Year, (object) dataOfClass.last.Month, (object) dataOfClass.last.Day);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mTextGold, (UnityEngine.Object) null))
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) SupportSettingUsedWindow.Instance, (UnityEngine.Object) null) && 999999999 <= dataOfClass.gold)
        {
          this.mTextGold.text = 999999999.ToString();
          GameUtility.SetGameObjectActive(this.mOverGold, true);
        }
        else
          this.mTextGold.text = dataOfClass.gold.ToString();
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mElement, (UnityEngine.Object) null))
        this.mElement.ImageIndex = (int) dataOfClass.element;
      if (dataOfClass.unit != null)
        DataSource.Bind<UnitData>(((Component) this).gameObject, dataOfClass.unit);
      else
        GameUtility.SetGameObjectActive(this.mLevel, false);
    }
  }
}
