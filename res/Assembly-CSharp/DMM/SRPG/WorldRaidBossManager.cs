// Decompiled with JetBrains decompiler
// Type: SRPG.WorldRaidBossManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class WorldRaidBossManager : MonoBehaviour
  {
    private static WorldRaidBossManager mInstance;
    private static int mBossIndex;
    private WorldRaidParam mCurrentWorldRaidParam;
    private WorldRaidParam.BossInfo mCurrentWorldRaidBossInfo;
    private WorldRaidBossDetailData mBoss = new WorldRaidBossDetailData();

    public static WorldRaidBossManager Instance => WorldRaidBossManager.mInstance;

    private void OnEnable()
    {
      if (!Object.op_Equality((Object) WorldRaidBossManager.mInstance, (Object) null))
        return;
      WorldRaidBossManager.mInstance = this;
    }

    private void OnDisable()
    {
      if (!Object.op_Equality((Object) WorldRaidBossManager.mInstance, (Object) this))
        return;
      WorldRaidBossManager.mInstance = (WorldRaidBossManager) null;
    }

    private void RefreshParam()
    {
      this.mCurrentWorldRaidParam = WorldRaidManager.GetCurrentWorldRaidParam();
      if (this.mCurrentWorldRaidParam == null)
      {
        DebugUtility.LogError("WorldRaidBossManager not found CurrentWorldRaidParam!");
      }
      else
      {
        this.mCurrentWorldRaidBossInfo = (WorldRaidParam.BossInfo) null;
        List<WorldRaidParam.BossInfo> bossInfoList = this.mCurrentWorldRaidParam.BossInfoList;
        if (WorldRaidBossManager.mBossIndex < bossInfoList.Count)
          this.mCurrentWorldRaidBossInfo = bossInfoList[WorldRaidBossManager.mBossIndex];
        else
          DebugUtility.LogError("WorldRaidBossManager array index out of bounds BossInfoList! index=" + (object) WorldRaidBossManager.mBossIndex);
      }
    }

    private void Awake() => this.RefreshParam();

    public WorldRaidBossParam GetCurrentWorldRaidBossParam()
    {
      return this.mCurrentWorldRaidBossInfo == null ? (WorldRaidBossParam) null : this.mCurrentWorldRaidBossInfo.BossParam;
    }

    public bool IsLastBoss()
    {
      return this.mCurrentWorldRaidBossInfo != null && this.mCurrentWorldRaidBossInfo.IsLastBoss;
    }

    public WorldRaidParam.BossInfo GetCurrentBossInfo() => this.mCurrentWorldRaidBossInfo;

    public static void SetBossIndex(int boss_idx)
    {
      WorldRaidBossManager.mBossIndex = boss_idx;
      if (!Object.op_Implicit((Object) WorldRaidBossManager.mInstance))
        return;
      WorldRaidBossManager.mInstance.RefreshParam();
    }

    public static int GetBossIndex() => WorldRaidBossManager.mBossIndex;

    public void BossIndexSub(bool _is_last_boss)
    {
      int mBossIndex = WorldRaidBossManager.mBossIndex;
      do
      {
        if (WorldRaidBossManager.mBossIndex - 1 < 0)
        {
          if (WorldRaidManager.GetAllBossesData() != null)
            WorldRaidBossManager.mBossIndex = WorldRaidManager.GetAllBossesData().Count - 1;
        }
        else
          --WorldRaidBossManager.mBossIndex;
        this.RefreshParam();
      }
      while (this.GetCurrentBossInfo().IsLastBoss != _is_last_boss && mBossIndex != WorldRaidBossManager.mBossIndex);
    }

    public void BossIndexAdd(bool _is_last_boss)
    {
      int mBossIndex = WorldRaidBossManager.mBossIndex;
      do
      {
        if (WorldRaidManager.GetAllBossesData() != null && WorldRaidBossManager.mBossIndex + 1 > WorldRaidManager.GetAllBossesData().Count - 1)
          WorldRaidBossManager.mBossIndex = 0;
        else
          ++WorldRaidBossManager.mBossIndex;
        this.RefreshParam();
      }
      while (this.GetCurrentBossInfo().IsLastBoss != _is_last_boss && mBossIndex != WorldRaidBossManager.mBossIndex);
    }

    public static BuffEffectParam GetBossBuff(string wr_iname, string wrb_iname)
    {
      if (string.IsNullOrEmpty(wr_iname) || string.IsNullOrEmpty(wrb_iname))
        return (BuffEffectParam) null;
      return WorldRaidParam.GetParam(wr_iname)?.GetBossInfo(wrb_iname)?.BossBuffParam;
    }

    public static WorldRaidBossDetailData GetBossData()
    {
      return Object.op_Equality((Object) WorldRaidBossManager.mInstance, (Object) null) ? new WorldRaidBossDetailData() : WorldRaidBossManager.mInstance.mBoss;
    }

    public static List<string> GetCurrentBossUsedUnitInameList()
    {
      return Object.op_Equality((Object) WorldRaidBossManager.mInstance, (Object) null) ? new List<string>() : WorldRaidBossManager.mInstance.mBoss.SelectedUnitInameList;
    }

    public static bool SetBossData(JSON_WorldRaidBossDetailData boss)
    {
      return !Object.op_Equality((Object) WorldRaidBossManager.mInstance, (Object) null) && boss != null && WorldRaidBossManager.mInstance.mBoss.Deserialize(boss);
    }

    public static Sprite GetWorldRaidBossIcon(string _boss_iname)
    {
      Sprite worldRaidBossIcon = (Sprite) null;
      if (!string.IsNullOrEmpty(_boss_iname))
      {
        SpriteSheet spriteSheet = AssetManager.Load<SpriteSheet>("UI/WorldRaid/WorldRaidBossImage");
        if (Object.op_Inequality((Object) spriteSheet, (Object) null))
          worldRaidBossIcon = spriteSheet.GetSprite(_boss_iname);
      }
      return worldRaidBossIcon;
    }
  }
}
