// Decompiled with JetBrains decompiler
// Type: SRPG.GuerrillaShopTimeLimit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class GuerrillaShopTimeLimit : MonoBehaviour, IGameParameter
  {
    private float mRefreshInterval = 1f;
    public Text Hour;
    public Text Minute;
    public Text Second;
    private long mEndTime;

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
        if ((UnityEngine.Object) this.Hour != (UnityEngine.Object) null)
          this.Hour.text = "00";
        if ((UnityEngine.Object) this.Minute != (UnityEngine.Object) null)
          this.Minute.text = "00";
        if (!((UnityEngine.Object) this.Second != (UnityEngine.Object) null))
          return;
        this.Second.text = "00";
      }
      else
      {
        TimeSpan timeSpan = TimeManager.FromUnixTime(this.mEndTime) - TimeManager.ServerTime;
        int totalHours = (int) timeSpan.TotalHours;
        int totalMinutes = (int) timeSpan.TotalMinutes;
        int totalSeconds = (int) timeSpan.TotalSeconds;
        if ((UnityEngine.Object) this.Hour != (UnityEngine.Object) null)
          this.Hour.text = string.Format("{0:D2}", (object) totalHours);
        if ((UnityEngine.Object) this.Minute != (UnityEngine.Object) null)
          this.Minute.text = totalHours <= 0 ? string.Format("{0:D2}", (object) totalMinutes) : string.Format("{0:D2}", (object) (totalMinutes % (totalHours * 60)));
        if ((UnityEngine.Object) this.Second != (UnityEngine.Object) null)
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
