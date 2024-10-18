// Decompiled with JetBrains decompiler
// Type: SRPG.SGShowServerTime
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class SGShowServerTime : MonoBehaviour
  {
    [SerializeField]
    private Text dateText;
    [SerializeField]
    private Text timeText;
    private DateTime currentTime;

    private void Start()
    {
      this.UpdateDateTimeText();
    }

    private void UpdateDateTimeText()
    {
      this.currentTime = TimeManager.ServerTime;
      this.dateText.text = this.currentTime.ToString(GameUtility.CultureSetting.DateTimeFormat.MonthDayPattern.Replace("MMMM", "MMM"), (IFormatProvider) GameUtility.CultureSetting);
      this.timeText.text = this.currentTime.ToString("HH:mm");
      this.StartCoroutine(this.Tick());
    }

    [DebuggerHidden]
    private IEnumerator Tick()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new SGShowServerTime.\u003CTick\u003Ec__Iterator54() { \u003C\u003Ef__this = this };
    }
  }
}
