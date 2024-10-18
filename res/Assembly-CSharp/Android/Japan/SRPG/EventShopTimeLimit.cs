﻿// Decompiled with JetBrains decompiler
// Type: SRPG.EventShopTimeLimit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class EventShopTimeLimit : MonoBehaviour, IGameParameter
  {
    private float mRefreshInterval = 1f;
    public GameObject Body;
    public Text Timer;
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
        if (!((UnityEngine.Object) this.Body != (UnityEngine.Object) null))
          return;
        this.Body.SetActive(false);
      }
      else
      {
        if ((UnityEngine.Object) this.Body != (UnityEngine.Object) null)
          this.Body.SetActive(true);
        TimeSpan timeSpan = TimeManager.FromUnixTime(this.mEndTime) - TimeManager.ServerTime;
        string str;
        if (timeSpan.TotalDays >= 1.0)
          str = LocalizedText.Get("sys.QUEST_TIMELIMIT_D", new object[1]
          {
            (object) timeSpan.Days
          });
        else if (timeSpan.TotalHours >= 1.0)
          str = LocalizedText.Get("sys.QUEST_TIMELIMIT_H", new object[1]
          {
            (object) timeSpan.Hours
          });
        else
          str = LocalizedText.Get("sys.QUEST_TIMELIMIT_M", new object[1]
          {
            (object) Mathf.Max(timeSpan.Minutes, 0)
          });
        if (!((UnityEngine.Object) this.Timer != (UnityEngine.Object) null) || !(this.Timer.text != str))
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
