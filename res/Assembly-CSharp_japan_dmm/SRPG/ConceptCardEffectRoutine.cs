// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardEffectRoutine
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class ConceptCardEffectRoutine
  {
    private const string mLeveUpPrefabPath = "UI/ConceptCardLevelup";
    private const string mAwakePrefabPath = "UI/ConceptCardLimitUp";
    private const string mTrustMasterPrefabPath = "UI/ConceptCardTrustMaster";
    private const string mTrustMasterRewardPrefabPath = "UI/ConceptCardTrustMasterReward";
    private const string mGroupSkillPowerUpPrefabPath = "UI/ConceptCardSkillPowerUp";
    private const string mGroupMaxPowerUpPrefabPath = "UI/ConceptCardMaxPowerUp";
    private const string mGroupMaxPowerResultPrefabPath = "UI/ConceptCardMaxPowerUpGroupSkillList";

    [DebuggerHidden]
    public IEnumerator PlayLevelup(Canvas overlayCanvas)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ConceptCardEffectRoutine.\u003CPlayLevelup\u003Ec__Iterator0()
      {
        overlayCanvas = overlayCanvas,
        \u0024this = this
      };
    }

    [DebuggerHidden]
    public IEnumerator PlayAwake(Canvas overlayCanvas)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ConceptCardEffectRoutine.\u003CPlayAwake\u003Ec__Iterator1()
      {
        overlayCanvas = overlayCanvas,
        \u0024this = this
      };
    }

    [DebuggerHidden]
    public IEnumerator PlayTrustMaster(Canvas overlayCanvas, ConceptCardData data)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ConceptCardEffectRoutine.\u003CPlayTrustMaster\u003Ec__Iterator2()
      {
        overlayCanvas = overlayCanvas,
        data = data,
        \u0024this = this
      };
    }

    [DebuggerHidden]
    public IEnumerator PlayTrustMasterReward(Canvas overlayCanvas, ConceptCardData data)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ConceptCardEffectRoutine.\u003CPlayTrustMasterReward\u003Ec__Iterator3()
      {
        overlayCanvas = overlayCanvas,
        data = data,
        \u0024this = this
      };
    }

    [DebuggerHidden]
    public IEnumerator PlayGroupSkilPowerUp(
      Canvas overlayCanvas,
      ConceptCardData data,
      ConceptCardEffectRoutine.EffectAltData altData)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ConceptCardEffectRoutine.\u003CPlayGroupSkilPowerUp\u003Ec__Iterator4()
      {
        overlayCanvas = overlayCanvas,
        data = data,
        altData = altData,
        \u0024this = this
      };
    }

    [DebuggerHidden]
    public IEnumerator PlayGroupSkilMaxPowerUp(
      Canvas overlayCanvas,
      ConceptCardData data,
      ConceptCardEffectRoutine.EffectAltData altData)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ConceptCardEffectRoutine.\u003CPlayGroupSkilMaxPowerUp\u003Ec__Iterator5()
      {
        overlayCanvas = overlayCanvas,
        data = data,
        altData = altData,
        \u0024this = this
      };
    }

    [DebuggerHidden]
    private IEnumerator EffectRoutine(
      Canvas overlayCanvas,
      string path,
      ConceptCardEffectRoutine.EffectType type = ConceptCardEffectRoutine.EffectType.DEFAULT,
      ConceptCardData data = null,
      ConceptCardEffectRoutine.EffectAltData altData = null)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ConceptCardEffectRoutine.\u003CEffectRoutine\u003Ec__Iterator6()
      {
        path = path,
        overlayCanvas = overlayCanvas,
        type = type,
        data = data,
        altData = altData
      };
    }

    public enum EffectType
    {
      DEFAULT,
      TRUST_MASTER,
      TRUST_MASTER_REWARD,
      GROUP_SKILL_POWER_UP,
      GROUP_SKILL_MAX_POWER_UP,
    }

    public class EffectAltData
    {
      public int prevAwakeCount;
      public int prevLevel;
    }
  }
}
