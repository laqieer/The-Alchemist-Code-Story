// Decompiled with JetBrains decompiler
// Type: SRPG.EventShopTimeLimit
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
  public class EventShopTimeLimit : MonoBehaviour, IGameParameter
  {
    public GameObject Body;
    public Text Timer;
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
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Body, (UnityEngine.Object) null))
          return;
        this.Body.SetActive(false);
      }
      else
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Body, (UnityEngine.Object) null))
          this.Body.SetActive(true);
        DateTime serverTime = TimeManager.ServerTime;
        TimeSpan timeSpan = TimeManager.FromUnixTime(this.mEndTime) - serverTime;
        string str;
        if (timeSpan.TotalDays >= 1.0)
          str = LocalizedText.Get("sys.QUEST_TIMELIMIT_D", (object) timeSpan.Days);
        else if (timeSpan.TotalHours >= 1.0)
          str = LocalizedText.Get("sys.QUEST_TIMELIMIT_H", (object) timeSpan.Hours);
        else
          str = LocalizedText.Get("sys.QUEST_TIMELIMIT_M", (object) Mathf.Max(timeSpan.Minutes, 0));
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Timer, (UnityEngine.Object) null) || !(this.Timer.text != str))
          return;
        this.Timer.text = str;
      }
    }

    public void UpdateValue()
    {
      this.mEndTime = 0L;
      if (MonoSingleton<GameManager>.Instance.Player.GetEventShopData() == null)
        return;
      this.mEndTime = GlobalVars.EventShopItem.shops.end;
      this.Refresh();
    }
  }
}
