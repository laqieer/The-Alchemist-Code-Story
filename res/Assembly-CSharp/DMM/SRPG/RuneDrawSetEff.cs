// Decompiled with JetBrains decompiler
// Type: SRPG.RuneDrawSetEff
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class RuneDrawSetEff : MonoBehaviour
  {
    [SerializeField]
    private GameObject mSetEffParentOn;
    [SerializeField]
    private GameObject mSetEffParentOff;
    [SerializeField]
    private Text mSetEffWakeNum;
    [SerializeField]
    private StatusList mSetEffStatusList;
    private BindRuneData mRuneData;

    public void Awake()
    {
      if (Object.op_Equality((Object) this.mSetEffParentOn, (Object) null))
        DebugUtility.LogError("mSetEffParentOn is unable to attach.");
      if (Object.op_Equality((Object) this.mSetEffParentOff, (Object) null))
        DebugUtility.LogError("mSetEffParentOff is unable to attach.");
      if (Object.op_Equality((Object) this.mSetEffWakeNum, (Object) null))
        DebugUtility.LogError("mSetEffWakeNum is unable to attach.");
      if (!Object.op_Equality((Object) this.mSetEffStatusList, (Object) null))
        return;
      DebugUtility.LogError("mSetEffStatusList is unable to attach.");
    }

    public void SetDrawParam(BindRuneData rune_data)
    {
      this.mRuneData = rune_data;
      this.Refresh();
    }

    public void Refresh()
    {
      if (this.mRuneData == null)
        return;
      RuneSetEff runeSetEff = this.mRuneData.RuneParam.RuneSetEff;
      GameUtility.SetGameObjectActive(this.mSetEffParentOn, 0 < runeSetEff.state.Length);
      GameUtility.SetGameObjectActive(this.mSetEffParentOff, 0 >= runeSetEff.state.Length);
      if (0 >= runeSetEff.state.Length)
        return;
      if (Object.op_Inequality((Object) this.mSetEffStatusList, (Object) null))
      {
        BaseStatus addStatus = (BaseStatus) new DrawBaseStatus();
        BaseStatus scaleStatus = (BaseStatus) new DrawBaseStatus();
        runeSetEff.AddRuneSetEffectBaseStatus(EElement.None, ref addStatus, ref scaleStatus, true);
        this.mSetEffStatusList.SetValues(addStatus, scaleStatus);
      }
      if (!Object.op_Inequality((Object) this.mSetEffWakeNum, (Object) null))
        return;
      this.mSetEffWakeNum.text = runeSetEff.cost.ToString();
    }
  }
}
