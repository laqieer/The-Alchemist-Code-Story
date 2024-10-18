// Decompiled with JetBrains decompiler
// Type: SRPG.AutoRepeatQuestBoxData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;

#nullable disable
namespace SRPG
{
  public class AutoRepeatQuestBoxData
  {
    private bool mIsInitialized;
    private int mAddCount;
    private int mExpansionAddCount;

    public bool IsInitialized => this.mIsInitialized;

    public int AddCount => this.mAddCount;

    public int ExpansionAddCount => this.mExpansionAddCount;

    public int Size
    {
      get
      {
        AutoRepeatQuestBoxParam[] repeatQuestBoxParams = MonoSingleton<GameManager>.Instance.MasterParam.AutoRepeatQuestBoxParams;
        return repeatQuestBoxParams != null && repeatQuestBoxParams.Length > this.mAddCount && repeatQuestBoxParams[this.mAddCount] != null ? repeatQuestBoxParams[this.mAddCount].Size + this.mExpansionAddCount : 0;
      }
    }

    public void Setup(int box_add_count)
    {
      this.mAddCount = box_add_count;
      this.mIsInitialized = true;
    }

    public void SetupExpansion(int box_add_count) => this.mExpansionAddCount = box_add_count;
  }
}
