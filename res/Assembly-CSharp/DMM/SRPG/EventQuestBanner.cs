// Decompiled with JetBrains decompiler
// Type: SRPG.EventQuestBanner
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class EventQuestBanner : MonoBehaviour, IGameParameter
  {
    public GameObject Lock;
    public GameObject Counter;
    public Text CounterText;
    public Text CounterLimitText;
    private bool m_HideChallengeCounter;

    private void Start() => this.UpdateValue();

    public void UpdateValue()
    {
      ChapterParam dataOfClass = DataSource.FindDataOfClass<ChapterParam>(((Component) this).gameObject, (ChapterParam) null);
      if (dataOfClass == null)
        return;
      if (dataOfClass.ChallengeLimitCount() <= 0 || this.m_HideChallengeCounter)
      {
        if (Object.op_Inequality((Object) this.Lock, (Object) null))
          this.Lock.SetActive(false);
        if (!Object.op_Inequality((Object) this.Counter, (Object) null))
          return;
        this.Counter.SetActive(false);
      }
      else
      {
        ChapterParam chapter;
        bool flag = dataOfClass.CheckEnableChallange(out chapter);
        if (Object.op_Inequality((Object) this.Lock, (Object) null))
          this.Lock.SetActive(!flag);
        if (Object.op_Inequality((Object) this.Counter, (Object) null))
          this.Counter.SetActive(true);
        if (Object.op_Inequality((Object) this.CounterText, (Object) null))
          this.CounterText.text = chapter.challengeCount.ToString();
        if (!Object.op_Inequality((Object) this.CounterLimitText, (Object) null))
          return;
        this.CounterLimitText.text = chapter.ChallengeLimitCount().ToString();
      }
    }

    public void SetHideChallengeCounter(bool hide) => this.m_HideChallengeCounter = hide;
  }
}
