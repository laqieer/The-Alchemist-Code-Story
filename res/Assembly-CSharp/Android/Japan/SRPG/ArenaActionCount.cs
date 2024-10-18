// Decompiled with JetBrains decompiler
// Type: SRPG.ArenaActionCount
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  public class ArenaActionCount : MonoBehaviour
  {
    private ArenaActionCount.AnmCtrl mAnmCtrl = new ArenaActionCount.AnmCtrl();
    private uint mOldActionCount = uint.MaxValue;
    public GameObject GoWhiteFont;
    public GameObject GoYellowFont;
    public GameObject GoRedFont;
    private const uint VALUE_OF_DISPLAY_IN_YELLOW_FONT = 20;
    private const uint VALUE_OF_DISPLAY_IN_RED_FONT = 5;
    private uint mActionCount;
    private bool mIsInitialized;

    public uint ActionCount
    {
      get
      {
        return this.mActionCount;
      }
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
      if (!(bool) ((UnityEngine.Object) componentInChildren))
        return;
      componentInChildren.text = count.ToString();
    }

    public void PlayEffect()
    {
      this.StartCoroutine(this.playEffect());
    }

    [DebuggerHidden]
    private IEnumerator playEffect()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ArenaActionCount.\u003CplayEffect\u003Ec__Iterator0() { \u0024this = this };
    }

    private void Start()
    {
      this.mIsInitialized = false;
      if ((bool) ((UnityEngine.Object) this.GoWhiteFont) && (bool) ((UnityEngine.Object) this.GoYellowFont) && (bool) ((UnityEngine.Object) this.GoRedFont))
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
      public uint mPlayBeatCtr = 1;
      public ArenaActionCount.eAnmState mAnmState;
      public uint mWaitFrameCtr;
      public GameObject mGoSelf;
      public Animator mAnmSelf;
      public GameObject mGoEffect;
      public Animator mAnmEffect;
    }
  }
}
