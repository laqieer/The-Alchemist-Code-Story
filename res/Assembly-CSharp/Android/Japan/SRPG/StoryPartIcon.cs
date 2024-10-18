// Decompiled with JetBrains decompiler
// Type: SRPG.StoryPartIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class StoryPartIcon : MonoBehaviour
  {
    private const string ANIMATION_RELEASE_NAME = "open";
    private const float FADE_TIME = 0.1f;
    [SerializeField]
    private GameObject IconGo;
    [SerializeField]
    private GameObject LockGo;
    [SerializeField]
    private ImageArray LockCover;
    [SerializeField]
    private ImageArray LockCoverPart;
    [SerializeField]
    private ImageArray LockOpen;
    [SerializeField]
    private ImageArray BlurIcon;
    [SerializeField]
    private ImageArray BlurIcon2;
    [SerializeField]
    private Text TxtConditions;
    [SerializeField]
    private GameObject IconMask;
    [SerializeField]
    private GameObject LockMask;
    [SerializeField]
    private GameObject LockTitleMask;
    [SerializeField]
    private CanvasGroup mCanvasGroup;
    private int mStoryPartNum;
    private bool mLockFlag;
    private float mCountDelat;
    private StoryPartIcon.AlphaState mAlphaState;

    public int StoryPartNum
    {
      get
      {
        return this.mStoryPartNum;
      }
    }

    public bool LockFlag
    {
      get
      {
        return this.mLockFlag;
      }
    }

    private void Start()
    {
    }

    private void Update()
    {
      if ((UnityEngine.Object) this.mCanvasGroup == (UnityEngine.Object) null)
        return;
      switch (this.mAlphaState)
      {
        case StoryPartIcon.AlphaState.Fadeout:
          this.mCountDelat += Time.deltaTime;
          if ((double) this.mCountDelat > 0.100000001490116)
          {
            this.mCanvasGroup.alpha = 1f;
            break;
          }
          this.mCanvasGroup.alpha = this.mCountDelat / 0.1f;
          break;
        case StoryPartIcon.AlphaState.Fadein:
          this.mCountDelat += Time.deltaTime;
          if ((double) this.mCountDelat > 0.100000001490116)
          {
            this.mCanvasGroup.alpha = 0.0f;
            break;
          }
          this.mCanvasGroup.alpha = (float) (1.0 - (double) this.mCountDelat / 0.100000001490116);
          break;
      }
    }

    public bool Setup(bool lock_flag, int story_part)
    {
      this.mStoryPartNum = 1;
      this.mLockFlag = false;
      this.mAlphaState = StoryPartIcon.AlphaState.None;
      if ((UnityEngine.Object) this.IconGo == (UnityEngine.Object) null || (UnityEngine.Object) this.LockGo == (UnityEngine.Object) null || ((UnityEngine.Object) this.LockCover == (UnityEngine.Object) null || (UnityEngine.Object) this.LockCoverPart == (UnityEngine.Object) null) || ((UnityEngine.Object) this.LockOpen == (UnityEngine.Object) null || (UnityEngine.Object) this.BlurIcon == (UnityEngine.Object) null || (UnityEngine.Object) this.BlurIcon2 == (UnityEngine.Object) null))
        return false;
      ImageArray component = this.IconGo.GetComponent<ImageArray>();
      if ((UnityEngine.Object) component == (UnityEngine.Object) null || story_part > component.Images.Length)
        return false;
      int num = story_part - 1;
      this.mStoryPartNum = story_part;
      this.mLockFlag = lock_flag;
      if (!lock_flag)
      {
        this.IconGo.SetActive(true);
        this.LockGo.SetActive(false);
        this.IconMask.SetActive(true);
        this.LockMask.SetActive(false);
        this.LockTitleMask.SetActive(false);
        component.ImageIndex = num;
      }
      else
      {
        component.ImageIndex = num;
        this.IconGo.SetActive(false);
        this.LockGo.SetActive(true);
        this.IconMask.SetActive(false);
        this.LockMask.SetActive(true);
        this.LockTitleMask.SetActive(true);
        this.LockCover.ImageIndex = num;
        this.LockCoverPart.ImageIndex = num;
        this.LockOpen.ImageIndex = num;
        this.BlurIcon.ImageIndex = num;
        this.BlurIcon2.ImageIndex = num;
        if ((UnityEngine.Object) this.TxtConditions != (UnityEngine.Object) null)
        {
          string partMessageSysId = MonoSingleton<GameManager>.Instance.GetReleaseStoryPartMessageSysID(this.mStoryPartNum);
          if (!string.IsNullOrEmpty(partMessageSysId))
            this.TxtConditions.text = LocalizedText.Get("sys." + partMessageSysId);
        }
      }
      this.IconMask.GetComponent<ImageArray>().ImageIndex = num;
      this.LockMask.GetComponent<ImageArray>().ImageIndex = num;
      this.LockTitleMask.GetComponent<ImageArray>().ImageIndex = num;
      return true;
    }

    public bool PlayReleaseAnim()
    {
      if (!this.mLockFlag)
        return false;
      this.LockGo.GetComponent<Animator>().Play("open");
      return true;
    }

    public bool IsPlayingReleaseAnim()
    {
      if (!this.mLockFlag)
        return false;
      bool flag = false;
      if ((double) this.LockGo.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0)
        flag = true;
      return flag;
    }

    public void ReleaseIcon()
    {
      this.IconGo.SetActive(true);
      this.IconMask.SetActive(true);
      this.LockGo.SetActive(false);
      this.LockMask.SetActive(false);
      this.LockTitleMask.SetActive(false);
      this.mLockFlag = false;
    }

    public void SetMask(bool mask_flag)
    {
      if (mask_flag)
      {
        if (this.mAlphaState != StoryPartIcon.AlphaState.Fadeout)
          this.mCountDelat = 0.0f;
        this.mAlphaState = StoryPartIcon.AlphaState.Fadeout;
      }
      else
      {
        if (this.mAlphaState != StoryPartIcon.AlphaState.Fadein)
          this.mCountDelat = 0.0f;
        this.mAlphaState = StoryPartIcon.AlphaState.Fadein;
      }
    }

    private enum AlphaState
    {
      None,
      Fadeout,
      Fadein,
    }
  }
}
