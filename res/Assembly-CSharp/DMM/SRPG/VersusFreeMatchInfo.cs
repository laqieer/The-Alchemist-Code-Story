// Decompiled with JetBrains decompiler
// Type: SRPG.VersusFreeMatchInfo
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
  public class VersusFreeMatchInfo : MonoBehaviour
  {
    private readonly float UPDATE_WAIT_TIME = 1f;
    [Description("開催中のときに表示するオブジェクトルート")]
    public GameObject OpenRoot;
    [Description("開催していないときに表示するオブジェクトルート")]
    public GameObject CloseRoot;
    [Description("連勝数オブジェクト")]
    public GameObject StreakWin;
    [Description("獲得コイン回数倍率オブジェクト")]
    public GameObject CoinRate;
    [Description("開催残り時間")]
    public Text RemainTime;
    [Description("次回開催までのテキスト")]
    public Text ScheduleTxt;
    [Description("連勝数")]
    public Text StreakWinTxt;
    [Description("コイン倍率")]
    public Text CoinRateTxt;
    [Description("フリー対戦のボタン")]
    public Button FreeBtn;
    private float mWaitTime = 1f;
    private long mEndTime;

    private void Start() => this.RefreshData();

    private void RefreshData()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      long vsFreeExpiredTime = instance.VSFreeExpiredTime;
      long num = TimeManager.FromDateTime(TimeManager.ServerTime);
      int vsStreakWinCntNow = instance.VS_StreakWinCnt_Now;
      int vsGetCoinRate = instance.GetVSGetCoinRate();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.OpenRoot, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CloseRoot, (UnityEngine.Object) null))
      {
        if (num < vsFreeExpiredTime)
        {
          this.mEndTime = vsFreeExpiredTime;
          this.OpenRoot.SetActive(true);
          this.CloseRoot.SetActive(false);
          this.CountDown();
        }
        else
          this.Close();
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.StreakWin, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.StreakWinTxt, (UnityEngine.Object) null))
      {
        this.StreakWin.SetActive(vsStreakWinCntNow > 1);
        this.StreakWinTxt.text = vsStreakWinCntNow.ToString();
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CoinRate, (UnityEngine.Object) null) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CoinRateTxt, (UnityEngine.Object) null))
        return;
      this.CoinRate.SetActive(vsGetCoinRate > 1);
      this.CoinRateTxt.text = vsGetCoinRate.ToString();
    }

    private void Update()
    {
      this.mWaitTime -= Time.deltaTime;
      if ((double) this.mWaitTime >= 0.0)
        return;
      this.CountDown();
    }

    private void CountDown()
    {
      if (this.mEndTime <= 0L)
        return;
      DateTime dateTime = TimeManager.FromUnixTime(this.mEndTime);
      DateTime serverTime = TimeManager.ServerTime;
      if (serverTime > dateTime)
      {
        this.Close();
      }
      else
      {
        TimeSpan timeSpan = dateTime - serverTime;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RemainTime, (UnityEngine.Object) null))
        {
          if (timeSpan.TotalDays >= 1.0)
            this.RemainTime.text = LocalizedText.Get("sys.MULTI_VERSUS_REMAIN_AT_DAY", (object) timeSpan.Days);
          else if (timeSpan.TotalHours >= 1.0)
            this.RemainTime.text = LocalizedText.Get("sys.MULTI_VERSUS_REMAIN_AT_HOUR", (object) timeSpan.Hours);
          else
            this.RemainTime.text = LocalizedText.Get("sys.MULTI_VERSUS_REMAIN_AT_MINUTE", (object) Mathf.Max(timeSpan.Minutes, 0));
        }
        this.mWaitTime = this.UPDATE_WAIT_TIME;
      }
    }

    private void Close()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.OpenRoot, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CloseRoot, (UnityEngine.Object) null))
      {
        this.OpenRoot.SetActive(false);
        this.CloseRoot.SetActive(true);
      }
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ScheduleTxt, (UnityEngine.Object) null))
      {
        if (instance.VSFreeNextTime == 0L)
        {
          this.ScheduleTxt.text = LocalizedText.Get("sys.MULTI_VERSUS_NEXT_NONE");
        }
        else
        {
          DateTime dateTime = TimeManager.FromUnixTime(instance.VSFreeNextTime);
          this.ScheduleTxt.text = string.Format(LocalizedText.Get("sys.MULTI_VERSUS_NEXT_AT"), (object) dateTime.Year, (object) dateTime.Month, (object) dateTime.Day, (object) dateTime.Hour, (object) dateTime.Minute);
        }
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.FreeBtn, (UnityEngine.Object) null))
        ((Selectable) this.FreeBtn).interactable = false;
      this.mEndTime = 0L;
    }
  }
}
