// Decompiled with JetBrains decompiler
// Type: SRPG.WorldRaidBossDirection
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class WorldRaidBossDirection : MonoBehaviour
  {
    private void Start()
    {
      WorldRaidParam currentWorldRaidParam = WorldRaidManager.GetCurrentWorldRaidParam();
      if (currentWorldRaidParam == null)
        return;
      WorldRaidNoticeData notice_data = WorldRaidNoticeData.Load(currentWorldRaidParam);
      if (notice_data == null)
        return;
      List<WorldRaidBossChallengedData> boss_data = WorldRaidManager.GetAllBossesData();
      if (boss_data == null)
        return;
      for (int i = 0; i < boss_data.Count; ++i)
      {
        if (boss_data[i].CurrentHP <= 0L && boss_data[i].Challenged && notice_data.IsNeedNotice_BeatBoss(i))
        {
          notice_data.SetBoss(true, i);
          WorldRaidNoticeData.Save(notice_data);
          WorldRaidParam.BossInfo bossInfo = currentWorldRaidParam.BossInfoList.Find((Predicate<WorldRaidParam.BossInfo>) (x => x.BossId == boss_data[i].BossIname));
          if (bossInfo != null)
          {
            DataSource.Bind<UnitParam>(((Component) this).gameObject, MonoSingleton<GameManager>.Instance.GetUnitParam(bossInfo.BossParam.UnitId));
            break;
          }
          break;
        }
      }
      GameParameter.UpdateAll(((Component) this).gameObject);
    }
  }
}
