// Decompiled with JetBrains decompiler
// Type: SRPG.WorldRaidNoticeData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [Serializable]
  public class WorldRaidNoticeData
  {
    [SerializeField]
    private string iname;
    [SerializeField]
    private int home;
    [SerializeField]
    private int lastboss;
    [SerializeField]
    private int[] boss;
    private bool is_dirty;
    private WorldRaidParam world_raid_param;

    private WorldRaidNoticeData(WorldRaidParam param)
    {
      if (param == null)
      {
        DebugUtility.LogError("[WorldRaidNoticeData] : WorldRaidParam is NULL => 正常に演出再生済みか判定できないインスタンスが生成されました");
      }
      else
      {
        this.iname = param.Iname;
        this.home = 0;
        this.lastboss = 0;
        this.boss = new int[param.BossInfoList.Count];
        this.world_raid_param = param;
      }
    }

    public static WorldRaidNoticeData Load(WorldRaidParam param)
    {
      if (param == null)
        return (WorldRaidNoticeData) null;
      string str = PlayerPrefsUtility.GetString(PlayerPrefsUtility.PREFS_KEY_WORLD_RAID_NOTICE, string.Empty);
      if (string.IsNullOrEmpty(str))
        return new WorldRaidNoticeData(param);
      WorldRaidNoticeData worldRaidNoticeData;
      try
      {
        worldRaidNoticeData = JsonUtility.FromJson<WorldRaidNoticeData>(str);
        worldRaidNoticeData.world_raid_param = param;
        if (worldRaidNoticeData.iname != param.Iname)
          worldRaidNoticeData = new WorldRaidNoticeData(param);
      }
      catch (Exception ex)
      {
        DebugUtility.LogException(ex);
        return (WorldRaidNoticeData) null;
      }
      return worldRaidNoticeData;
    }

    public static bool Save(WorldRaidNoticeData notice_data)
    {
      if (notice_data == null)
        return false;
      if (!notice_data.is_dirty)
        return true;
      try
      {
        string json = JsonUtility.ToJson((object) notice_data);
        if (!PlayerPrefsUtility.SetString(PlayerPrefsUtility.PREFS_KEY_WORLD_RAID_NOTICE, json, true))
          return false;
      }
      catch (Exception ex)
      {
        DebugUtility.LogException(ex);
        return false;
      }
      notice_data.is_dirty = false;
      return true;
    }

    public void SetHome(bool value)
    {
      int num = !value ? 0 : 1;
      if (this.home != num)
        this.is_dirty = true;
      this.home = num;
    }

    public void SetLastBoss(bool value)
    {
      int num = !value ? 0 : 1;
      if (this.lastboss != num)
        this.is_dirty = true;
      this.lastboss = num;
    }

    public void SetBoss(bool value, int index)
    {
      if (this.boss == null || this.boss.Length <= index)
        return;
      int num = !value ? 0 : 1;
      if (this.boss[index] != num)
        this.is_dirty = true;
      this.boss[index] = num;
    }

    public void SetBoss(bool value, string boss_iname)
    {
      if (this.world_raid_param == null || this.boss == null)
        return;
      int index = this.world_raid_param.BossInfoList.FindIndex((Predicate<WorldRaidParam.BossInfo>) (info => info.BossId == boss_iname));
      if (index <= -1 || this.boss.Length <= index)
        return;
      int num = !value ? 0 : 1;
      if (this.boss[index] != num)
        this.is_dirty = true;
      this.boss[index] = num;
    }

    public bool IsNeedNotice_Home() => this.home == 0;

    public bool IsNeedNotice_LastBoss() => this.lastboss == 0;

    public bool IsNeedNotice_BeatBoss(int index)
    {
      return this.boss != null && this.boss.Length > index && this.boss[index] == 0;
    }

    public bool IsNeedNotice_BeatBoss(string boss_iname)
    {
      if (this.world_raid_param == null || this.boss == null)
        return false;
      int index = this.world_raid_param.BossInfoList.FindIndex((Predicate<WorldRaidParam.BossInfo>) (info => info.BossId == boss_iname));
      return index > -1 && this.boss.Length > index && this.boss[index] == 0;
    }
  }
}
