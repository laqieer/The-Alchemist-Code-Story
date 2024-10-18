// Decompiled with JetBrains decompiler
// Type: SRPG.RuneDrawEnhanceLevel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class RuneDrawEnhanceLevel : MonoBehaviour
  {
    [SerializeField]
    private Text mLevelNow;
    [SerializeField]
    private Text mLevelNext;
    private BindRuneData mRuneData;
    private int mAddLevel;

    public void Awake()
    {
    }

    public void SetDrawParam(BindRuneData rune_data, int add_level = 0)
    {
      this.mRuneData = rune_data;
      this.mAddLevel = add_level;
      this.Refresh();
    }

    public void Refresh()
    {
      if (this.mRuneData == null)
        return;
      if (Object.op_Inequality((Object) this.mLevelNow, (Object) null))
        this.mLevelNow.text = this.mRuneData.EnhanceNum.ToString();
      if (!Object.op_Inequality((Object) this.mLevelNext, (Object) null))
        return;
      this.mLevelNext.text = Mathf.Min(this.mRuneData.EnhanceNum + this.mAddLevel, MonoSingleton<GameManager>.Instance.MasterParam.FixParam.RuneEnhNextNum).ToString();
    }
  }
}
