// Decompiled with JetBrains decompiler
// Type: SRPG.ChallengeMissionCategoryButton
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class ChallengeMissionCategoryButton : MonoBehaviour
  {
    public Button Button;
    public Image Badge;
    public Image SelectionFrame;
    public GameObject TimerBase;
    public Text Timer;
    private float mRefreshInterval = 1f;
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
          str = LocalizedText.Get("sys.QUEST_TIMELIMIT_D", (object) timeSpan.Days);
        else if (timeSpan.TotalHours >= 1.0)
          str = LocalizedText.Get("sys.QUEST_TIMELIMIT_H", (object) timeSpan.Hours);
        else
          str = LocalizedText.Get("sys.QUEST_TIMELIMIT_M", (object) Mathf.Max(timeSpan.Minutes, 0));
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Timer, (UnityEngine.Object) null) || !(this.Timer.text != str))
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
