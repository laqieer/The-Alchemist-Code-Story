// Decompiled with JetBrains decompiler
// Type: SRPG.RuneDrawEnhancedBaseState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public class RuneDrawEnhancedBaseState : MonoBehaviour
  {
    [SerializeField]
    private StatusList mBaseStatusBefore;
    [SerializeField]
    private StatusList mBaseStatusAfter;
    private BindRuneData mRuneDataBefore;
    private BindRuneData mRuneDataAfter;

    public void Awake()
    {
    }

    public void SetDrawParam(BindRuneData before, BindRuneData after)
    {
      this.mRuneDataBefore = before;
      this.mRuneDataAfter = after;
      this.Refresh();
    }

    public void Refresh()
    {
      if (this.mRuneDataBefore == null || this.mRuneDataAfter == null)
        return;
      BaseStatus addStatus1 = (BaseStatus) null;
      BaseStatus scaleStatus1 = (BaseStatus) null;
      BaseStatus addStatus2 = (BaseStatus) null;
      BaseStatus scaleStatus2 = (BaseStatus) null;
      this.mRuneDataBefore.Rune.CreateBaseStatusFromBaseParam(ref addStatus1, ref scaleStatus1, true);
      this.mRuneDataAfter.Rune.CreateBaseStatusFromBaseParam(ref addStatus2, ref scaleStatus2, true);
      if (addStatus1 == null)
        addStatus1 = new BaseStatus();
      if (scaleStatus1 == null)
        scaleStatus1 = new BaseStatus();
      if (addStatus2 == null)
        addStatus2 = new BaseStatus();
      if (scaleStatus2 == null)
        scaleStatus2 = new BaseStatus();
      if (Object.op_Inequality((Object) this.mBaseStatusBefore, (Object) null))
        this.mBaseStatusBefore.SetValues(addStatus1, scaleStatus1);
      if (!Object.op_Inequality((Object) this.mBaseStatusAfter, (Object) null))
        return;
      this.mBaseStatusAfter.SetValues(addStatus2, scaleStatus2);
    }
  }
}
