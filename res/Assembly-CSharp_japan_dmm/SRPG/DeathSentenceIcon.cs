// Decompiled with JetBrains decompiler
// Type: SRPG.DeathSentenceIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class DeathSentenceIcon : MonoBehaviour
  {
    public GameObject DeathIconPrefab;
    private Text mCountDownText;
    private GameObject mDeathIcon;
    private bool mIsDeathSentenceCountDownPlaying;

    public void Init(Unit parent)
    {
      if (!Object.op_Implicit((Object) this.DeathIconPrefab))
        return;
      this.mDeathIcon = Object.Instantiate<GameObject>(this.DeathIconPrefab, ((Component) this).transform.position, ((Component) this).transform.rotation);
      if (!Object.op_Inequality((Object) this.mDeathIcon, (Object) null))
        return;
      this.mCountDownText = this.mDeathIcon.GetComponentInChildren<Text>();
      this.mDeathIcon.transform.SetParent(((Component) this).transform);
      this.mDeathIcon.SetActive(false);
      DataSource.Bind<Unit>(((Component) this).gameObject, parent);
    }

    public void Open()
    {
      if (!Object.op_Inequality((Object) this.mDeathIcon, (Object) null) || this.mDeathIcon.activeSelf)
        return;
      this.mDeathIcon.SetActive(true);
    }

    public void Close()
    {
      if (!Object.op_Inequality((Object) this.mDeathIcon, (Object) null) || !this.mDeathIcon.activeSelf)
        return;
      this.mDeathIcon.SetActive(false);
    }

    public void UpdateCountDown(int currentCount)
    {
      if (Object.op_Equality((Object) this.mDeathIcon, (Object) null) || !Object.op_Inequality((Object) this.mCountDownText, (Object) null))
        return;
      this.mCountDownText.text = currentCount.ToString();
    }

    public bool IsDeathSentenceCountDownPlaying
    {
      get => this.mIsDeathSentenceCountDownPlaying;
      set
      {
      }
    }

    [DebuggerHidden]
    private IEnumerator CountdownInternal(int currentCount, float LifeSeconds)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new DeathSentenceIcon.\u003CCountdownInternal\u003Ec__Iterator0()
      {
        currentCount = currentCount,
        LifeSeconds = LifeSeconds,
        \u0024this = this
      };
    }

    public void Countdown(int currentCount, float LifeSeconds = 0.0f)
    {
      if (((Component) this).gameObject.activeInHierarchy && (double) LifeSeconds > 0.0)
      {
        this.StartCoroutine(this.CountdownInternal(currentCount, LifeSeconds));
      }
      else
      {
        this.mIsDeathSentenceCountDownPlaying = false;
        this.UpdateCountDown(currentCount);
      }
    }
  }
}
