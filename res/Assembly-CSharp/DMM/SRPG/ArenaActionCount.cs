﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ArenaActionCount
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
  public class ArenaActionCount : MonoBehaviour
  {
    public GameObject GoWhiteFont;
    public GameObject GoYellowFont;
    public GameObject GoRedFont;
    private const uint VALUE_OF_DISPLAY_IN_YELLOW_FONT = 20;
    private const uint VALUE_OF_DISPLAY_IN_RED_FONT = 5;
    private ArenaActionCount.AnmCtrl mAnmCtrl = new ArenaActionCount.AnmCtrl();
    private uint mActionCount;
    private uint mOldActionCount = uint.MaxValue;
    private bool mIsInitialized;

    public uint ActionCount
    {
      get => this.mActionCount;
      set
      {
        this.mActionCount = value;
        if ((int) this.mOldActionCount == (int) this.mActionCount)
          return;
        this.dispActionCount((int) this.mActionCount);
        this.mOldActionCount = this.mActionCount;
      }
    }

    private void dispActionCount(int count)
    {
      if (!this.mIsInitialized)
        return;
      if (count < 0)
        count = 0;
      this.GoWhiteFont.SetActive(false);
      this.GoYellowFont.SetActive(false);
      this.GoRedFont.SetActive(false);
      GameObject gameObject = this.GoWhiteFont;
      if (count <= 5)
        gameObject = this.GoRedFont;
      else if (count <= 20)
        gameObject = this.GoYellowFont;
      gameObject.SetActive(true);
      BitmapText componentInChildren = gameObject.GetComponentInChildren<BitmapText>(true);
      if (!Object.op_Implicit((Object) componentInChildren))
        return;
      ((Text) componentInChildren).text = count.ToString();
    }

    public void PlayEffect() => this.StartCoroutine(this.playEffect());

    [DebuggerHidden]
    private IEnumerator playEffect()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ArenaActionCount.\u003CplayEffect\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    private void Start()
    {
      this.mIsInitialized = false;
      if (Object.op_Implicit((Object) this.GoWhiteFont) && Object.op_Implicit((Object) this.GoYellowFont) && Object.op_Implicit((Object) this.GoRedFont))
        this.mIsInitialized = true;
      this.ActionCount = 0U;
    }

    private enum eAnmState
    {
      IDLE,
      INIT,
      WAIT_FRAME,
      PLAY_DROP,
      WAIT_DROP,
      PLAY_BEAT,
      WAIT_BEAT,
    }

    private class AnmCtrl
    {
      public ArenaActionCount.eAnmState mAnmState;
      public uint mWaitFrameCtr;
      public uint mPlayBeatCtr = 1;
      public GameObject mGoSelf;
      public Animator mAnmSelf;
      public GameObject mGoEffect;
      public Animator mAnmEffect;
    }
  }
}
