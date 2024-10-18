// Decompiled with JetBrains decompiler
// Type: SRPG.RuneDrawDisassemblyInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class RuneDrawDisassemblyInfo : MonoBehaviour
  {
    [SerializeField]
    private Text mTextZeny;
    [SerializeField]
    private Text mTextSelectedNum;
    [SerializeField]
    private Text mTextSelectedMax;
    [SerializeField]
    private Text mTextRarityWarning;
    [SerializeField]
    private GameObject[] mIconObj;
    [SerializeField]
    private Text[] mIconNum;
    private int mZeny;
    private int mSelectedCount;
    private int mSelectedMax;
    private bool mIsRarityWarning;
    private List<RuneDisassembly.Materials> mDisassemblyData = new List<RuneDisassembly.Materials>();

    private void Awake()
    {
    }

    public void SetParam(
      int zeny,
      int selected_count,
      int selected_max,
      bool is_rarity_warning,
      List<RuneDisassembly.Materials> disassembly_data)
    {
      this.mZeny = zeny;
      this.mSelectedCount = selected_count;
      this.mSelectedMax = selected_max;
      this.mIsRarityWarning = is_rarity_warning;
      this.mDisassemblyData = disassembly_data;
      this.Refresh();
    }

    public void Refresh()
    {
      if (Object.op_Implicit((Object) this.mTextZeny))
        this.mTextZeny.text = this.mZeny.ToString();
      if (Object.op_Implicit((Object) this.mTextSelectedNum))
        this.mTextSelectedNum.text = this.mSelectedCount.ToString();
      if (Object.op_Implicit((Object) this.mTextSelectedMax))
        this.mTextSelectedMax.text = this.mSelectedMax.ToString();
      if (Object.op_Implicit((Object) this.mTextRarityWarning))
        ((Component) this.mTextRarityWarning).gameObject.SetActive(this.mIsRarityWarning);
      for (int index = 0; index < this.mIconObj.Length; ++index)
      {
        if (!Object.op_Equality((Object) this.mIconObj[index], (Object) null))
        {
          if (this.mDisassemblyData.Count > index && this.mIconNum.Length > index)
          {
            this.mIconObj[index].SetActive(true);
            DataSource.Bind<ItemParam>(this.mIconObj[index], this.mDisassemblyData[index].item);
            GameParameter.UpdateAll(this.mIconObj[index]);
            if (Object.op_Inequality((Object) this.mIconNum[index], (Object) null))
              this.mIconNum[index].text = this.mDisassemblyData[index].num.ToString();
          }
          else
            this.mIconObj[index].SetActive(false);
        }
      }
    }
  }
}
