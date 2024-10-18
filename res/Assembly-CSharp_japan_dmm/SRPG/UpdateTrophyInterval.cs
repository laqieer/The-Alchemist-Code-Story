// Decompiled with JetBrains decompiler
// Type: SRPG.UpdateTrophyInterval
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public class UpdateTrophyInterval
  {
    private float updat_torphy_interval;
    private const float NOW_TROPHY_INTERVAL_TIME = 0.0f;
    private const float UPDATE_TROPHY_INTERVAL_TIME = 5f;
    private const float SYNC_TROPHY_INTERVAL_TIME = 0.1f;

    public void SetSyncNow() => this.updat_torphy_interval = 0.0f;

    public void SetSyncInterval()
    {
      this.updat_torphy_interval = Mathf.Max(this.updat_torphy_interval, 0.1f);
    }

    public void SetUpdateInterval()
    {
      this.updat_torphy_interval = Mathf.Max(this.updat_torphy_interval, 5f);
    }

    public bool PlayCheckUpdate()
    {
      if (0.0 >= (double) this.updat_torphy_interval)
        return true;
      this.updat_torphy_interval -= Time.unscaledDeltaTime;
      return false;
    }
  }
}
