// Decompiled with JetBrains decompiler
// Type: SRPG.StoryExChallengeCountData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;

#nullable disable
namespace SRPG
{
  public class StoryExChallengeCountData
  {
    private int mNum;
    private int mReset;

    public int Num => this.mNum;

    public int Reset => this.mReset;

    public int RestChallengeCount
    {
      get
      {
        return Math.Max(0, MonoSingleton<GameManager>.Instance.Player.GetChallengeLimitCount(ExpansionPurchaseParam.eExpansionType.ExtraCount, (string) null, MonoSingleton<GameManager>.Instance.MasterParam.FixParam.StoryExChallengeMax) - this.mNum);
      }
    }

    public int RestResetCount
    {
      get
      {
        return Math.Max(0, MonoSingleton<GameManager>.Instance.MasterParam.FixParam.StoryExResetMax - this.mReset);
      }
    }

    public void Deserialize(JSON_StoryExChallengeCount json)
    {
      this.mNum = json.num;
      this.mReset = json.reset;
    }

    public bool IsRestChallengeCount_Zero()
    {
      return MonoSingleton<GameManager>.Instance.MasterParam.FixParam.StoryExChallengeMax > 0 && this.mNum >= MonoSingleton<GameManager>.Instance.Player.GetChallengeLimitCount(ExpansionPurchaseParam.eExpansionType.ExtraCount, (string) null, MonoSingleton<GameManager>.Instance.MasterParam.FixParam.StoryExChallengeMax);
    }

    public bool IsRestResetCount_Zero()
    {
      return this.mReset >= MonoSingleton<GameManager>.Instance.MasterParam.FixParam.StoryExResetMax;
    }
  }
}
