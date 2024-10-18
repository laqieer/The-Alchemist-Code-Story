// Decompiled with JetBrains decompiler
// Type: SRPG.GachaTabListItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class GachaTabListItem : MonoBehaviour
  {
    public Text Value;
    public Text Fotter;
    private long mEndAt;
    private bool mDisabled;
    private Coroutine mUpdateCoroutine;
    private float mNextUpdateTime;
    private string mFormatkey = "sys.QUEST_TIMELIMIT_";
    private long mGachaStartAt;
    private long mGachaEndAt;
    private int mListIndex = -1;

    public long EndAt
    {
      get => this.mEndAt;
      set => this.mEndAt = value;
    }

    public bool Disabled
    {
      get => this.mDisabled;
      set => this.mDisabled = value;
    }

    public string FormatKey
    {
      get => this.mFormatkey;
      set => this.mFormatkey = value;
    }

    public long GachaStartAt
    {
      get => this.mGachaStartAt;
      set => this.mGachaStartAt = value;
    }

    public long GachaEndtAt
    {
      get => this.mGachaEndAt;
      set => this.mGachaEndAt = value;
    }

    public int ListIndex
    {
      get => this.mListIndex;
      set => this.mListIndex = value;
    }

    private void Start()
    {
    }

    private void OnEnable()
    {
      if (this.mUpdateCoroutine != null)
      {
        this.StopCoroutine(this.mUpdateCoroutine);
        this.mUpdateCoroutine = (Coroutine) null;
      }
      this.RefreshTimer();
    }

    [DebuggerHidden]
    private IEnumerator UpdateTimer()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GachaTabListItem.\u003CUpdateTimer\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    private void SetUpdateTimer(float interval)
    {
      if (!((Component) this).gameObject.activeInHierarchy)
        return;
      if ((double) interval <= 0.0)
      {
        if (this.mUpdateCoroutine == null)
          return;
        this.StopCoroutine(this.mUpdateCoroutine);
      }
      else
      {
        this.mNextUpdateTime = Time.time + interval;
        if (this.mUpdateCoroutine != null)
          return;
        this.mUpdateCoroutine = this.StartCoroutine(this.UpdateTimer());
      }
    }

    private void RefreshTimer()
    {
      DateTime serverTime = TimeManager.ServerTime;
      DateTime dateTime = TimeManager.FromUnixTime(this.mEndAt);
      TimeSpan timeSpan = dateTime - serverTime;
      if (this.Disabled && timeSpan.TotalSeconds < 0.0 && this.mGachaEndAt >= Network.GetServerTime())
      {
        this.mEndAt = TimeManager.FromDateTime(dateTime.AddDays(1.0));
        dateTime = TimeManager.FromUnixTime(this.mEndAt);
        timeSpan = dateTime - serverTime;
        SRPG_Button component = ((Component) this).GetComponent<SRPG_Button>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        {
          ((Selectable) component).interactable = true;
          this.Disabled = false;
        }
      }
      string empty = string.Empty;
      string str;
      if (timeSpan.TotalDays >= 1.0)
        str = LocalizedText.Get(this.FormatKey + "D", (object) timeSpan.Days);
      else if (timeSpan.TotalHours >= 1.0)
        str = LocalizedText.Get(this.FormatKey + "H", (object) timeSpan.Hours);
      else
        str = LocalizedText.Get(this.FormatKey + "M", (object) Mathf.Max(timeSpan.Minutes, 0));
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Value, (UnityEngine.Object) null) && this.Value.text != str)
        this.Value.text = str;
      this.SetUpdateTimer(1f);
    }
  }
}
