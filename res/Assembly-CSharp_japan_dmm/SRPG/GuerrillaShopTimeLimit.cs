// Decompiled with JetBrains decompiler
// Type: SRPG.GuerrillaShopTimeLimit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class GuerrillaShopTimeLimit : MonoBehaviour, IGameParameter
  {
    public Text Hour;
    public Text Minute;
    public Text Second;
    private long mEndTime;
    private float mRefreshInterval = 1f;

    private void Start()
    {
      this.UpdateValue();
      this.Refresh();
    }

    private void Update()
    {
      this.mRefreshInterval -= Time.unscaledDeltaTime;
      if ((double) this.mRefreshInterval > 0.0)
        return;
      this.Refresh();
      this.mRefreshInterval = 1f;
    }

    private void Refresh()
    {
      if (this.mEndTime <= 0L)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Hour, (UnityEngine.Object) null))
          this.Hour.text = "00";
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Minute, (UnityEngine.Object) null))
          this.Minute.text = "00";
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Second, (UnityEngine.Object) null))
          return;
        this.Second.text = "00";
      }
      else
      {
        DateTime serverTime = TimeManager.ServerTime;
        TimeSpan timeSpan = TimeManager.FromUnixTime(this.mEndTime) - serverTime;
        int totalHours = (int) timeSpan.TotalHours;
        int totalMinutes = (int) timeSpan.TotalMinutes;
        int totalSeconds = (int) timeSpan.TotalSeconds;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Hour, (UnityEngine.Object) null))
          this.Hour.text = string.Format("{0:D2}", (object) totalHours);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Minute, (UnityEngine.Object) null))
          this.Minute.text = totalHours <= 0 ? string.Format("{0:D2}", (object) totalMinutes) : string.Format("{0:D2}", (object) (totalMinutes % (totalHours * 60)));
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Second, (UnityEngine.Object) null))
          this.Second.text = totalMinutes <= 0 ? string.Format("{0:D2}", (object) totalSeconds) : string.Format("{0:D2}", (object) (totalSeconds % (totalMinutes * 60)));
        if (!(timeSpan <= TimeSpan.Zero))
          return;
        GlobalEvent.Invoke("FINISH_GUERRILLA_SHOP_SHOW", (object) null);
      }
    }

    public void UpdateValue()
    {
      this.mEndTime = 0L;
      long guerrillaShopEnd = MonoSingleton<GameManager>.Instance.Player.GuerrillaShopEnd;
      if (guerrillaShopEnd <= 0L)
        return;
      this.mEndTime = guerrillaShopEnd;
      this.Refresh();
    }
  }
}
