// Decompiled with JetBrains decompiler
// Type: SRPG.VersusDraftInfo
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
  public class VersusDraftInfo : MonoBehaviour
  {
    [SerializeField]
    private Text mDateText;

    private void Start()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mDateText, (UnityEngine.Object) null))
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (instance.VSDraftType != VersusDraftType.Draft)
        return;
      VersusEnableTimeParam versusEnableTime = instance.GetVersusEnableTime(instance.mVersusEnableId);
      if (versusEnableTime == null)
        return;
      DateTime beginAt = versusEnableTime.BeginAt;
      DateTime endAt = versusEnableTime.EndAt;
      DateTime dateTime = DateTime.Today;
      if (versusEnableTime.Schedule != null && versusEnableTime.Schedule.Count > 0)
      {
        VersusEnableTimeScheduleParam timeScheduleParam = versusEnableTime.Schedule[versusEnableTime.Schedule.Count - 1];
        dateTime = DateTime.Parse(TimeManager.ServerTime.ToShortDateString() + " " + timeScheduleParam.Begin) + TimeSpan.Parse(timeScheduleParam.Open);
      }
      this.mDateText.text = string.Format(LocalizedText.Get("sys.DRAFT_PERIOD"), (object) beginAt.Month, (object) beginAt.Day, (object) endAt.Month, (object) endAt.Day, (object) dateTime.Hour, (object) dateTime.Minute);
    }
  }
}
