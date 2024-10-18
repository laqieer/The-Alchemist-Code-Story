﻿// Decompiled with JetBrains decompiler
// Type: SRPG.DeathSentenceIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

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
      if (!(bool) ((UnityEngine.Object) this.DeathIconPrefab))
        return;
      this.mDeathIcon = UnityEngine.Object.Instantiate((UnityEngine.Object) this.DeathIconPrefab, this.transform.position, this.transform.rotation) as GameObject;
      if (!((UnityEngine.Object) this.mDeathIcon != (UnityEngine.Object) null))
        return;
      this.mCountDownText = this.mDeathIcon.GetComponentInChildren<Text>();
      this.mDeathIcon.transform.SetParent(this.transform);
      this.mDeathIcon.SetActive(false);
      DataSource.Bind<Unit>(this.gameObject, parent);
    }

    public void Open()
    {
      if (!((UnityEngine.Object) this.mDeathIcon != (UnityEngine.Object) null) || this.mDeathIcon.activeSelf)
        return;
      this.mDeathIcon.SetActive(true);
    }

    public void Close()
    {
      if (!((UnityEngine.Object) this.mDeathIcon != (UnityEngine.Object) null) || !this.mDeathIcon.activeSelf)
        return;
      this.mDeathIcon.SetActive(false);
    }

    public void UpdateCountDown(int currentCount)
    {
      if ((UnityEngine.Object) this.mDeathIcon == (UnityEngine.Object) null || !((UnityEngine.Object) this.mCountDownText != (UnityEngine.Object) null))
        return;
      this.mCountDownText.text = currentCount.ToString();
    }

    public bool IsDeathSentenceCountDownPlaying
    {
      get
      {
        return this.mIsDeathSentenceCountDownPlaying;
      }
      set
      {
      }
    }

    [DebuggerHidden]
    private IEnumerator CountdownInternal(int currentCount, float LifeSeconds)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new DeathSentenceIcon.\u003CCountdownInternal\u003Ec__IteratorF5() { currentCount = currentCount, LifeSeconds = LifeSeconds, \u003C\u0024\u003EcurrentCount = currentCount, \u003C\u0024\u003ELifeSeconds = LifeSeconds, \u003C\u003Ef__this = this };
    }

    public void Countdown(int currentCount, float LifeSeconds = 0.0f)
    {
      if (this.gameObject.activeInHierarchy && (double) LifeSeconds > 0.0)
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
