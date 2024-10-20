﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ChallengeMissionCategoryButton
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class ChallengeMissionCategoryButton : MonoBehaviour
  {
    private float mRefreshInterval = 1f;
    public Button Button;
    public Image Badge;
    public Image SelectionFrame;
    public GameObject TimerBase;
    public Text Timer;
    private DateTime mEndTime;
    private ChallengeCategoryParam mCategoryParam;

    private void Update()
    {
      if (this.mCategoryParam == null)
        return;
      this.mRefreshInterval -= Time.unscaledDeltaTime;
      if ((double) this.mRefreshInterval > 0.0)
        return;
      this.Refresh();
      this.mRefreshInterval = 1f;
    }

    private void Refresh()
    {
      DateTime serverTime = TimeManager.ServerTime;
      DateTime mEndTime = this.mEndTime;
      TimeSpan timeSpan = mEndTime - serverTime;
      string str = (string) null;
      if (mEndTime < DateTime.MaxValue)
      {
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
      }
      if (!((UnityEngine.Object) this.Timer != (UnityEngine.Object) null) || !(this.Timer.text != str))
        return;
      this.Timer.text = str;
    }

    public void SetChallengeCategory(ChallengeCategoryParam category)
    {
      if (category.end_at.DateTimes < DateTime.MaxValue)
      {
        this.TimerBase.SetActive(true);
        this.mCategoryParam = category;
        this.mEndTime = this.mCategoryParam.end_at.DateTimes;
        this.Refresh();
      }
      else
        this.TimerBase.SetActive(false);
    }
  }
}