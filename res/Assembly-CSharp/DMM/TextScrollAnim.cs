// Decompiled with JetBrains decompiler
// Type: TextScrollAnim
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
public class TextScrollAnim : MonoBehaviour
{
  public float MoveSpeed = 30f;
  public float ResetSpeed = 50f;
  public float ResetWaitTime = 1.5f;
  public float mStopTime = 3f;
  private Vector3 mPrePosition;
  private Vector3 mBasePosition;
  private RectTransform rectTrans;
  private float mParentWidth;
  private float mTextWidth;
  private string mPreText;
  private TextScrollAnim.State mState;

  private void Start()
  {
    try
    {
      this.rectTrans = ((Component) this).GetComponent<RectTransform>();
      this.mParentWidth = ((Component) ((Component) this).transform.parent).gameObject.GetComponent<RectTransform>().sizeDelta.x;
      this.mBasePosition = this.rectTrans.anchoredPosition3D;
      this.mState = TextScrollAnim.State.INANIM_WAIT;
    }
    catch
    {
      this.mState = TextScrollAnim.State.NONE;
    }
  }

  private void Update()
  {
    switch (this.mState)
    {
      case TextScrollAnim.State.NONE:
      case TextScrollAnim.State.MOVE_ANIM:
      case TextScrollAnim.State.RESET_ANIM:
      case TextScrollAnim.State.WAIT_ANIM:
        if (this.IsTextChangeCheck())
        {
          this.rectTrans.anchoredPosition3D = new Vector3(this.mBasePosition.x, this.rectTrans.anchoredPosition3D.y, this.rectTrans.anchoredPosition3D.z);
          this.mState = TextScrollAnim.State.START_CHECK;
          break;
        }
        break;
    }
    switch (this.mState)
    {
      case TextScrollAnim.State.INANIM_WAIT:
        this.WaitInAnim();
        break;
      case TextScrollAnim.State.START_CHECK:
        this.StartCheck();
        break;
      case TextScrollAnim.State.MOVE_ANIM:
        this.MoveAnim();
        break;
      case TextScrollAnim.State.RESET_ANIM:
        this.ResetAnim();
        break;
      case TextScrollAnim.State.WAIT_ANIM:
        this.WaitAnim();
        break;
    }
  }

  private bool IsTextChangeCheck()
  {
    try
    {
      Text component = ((Component) this).GetComponent<Text>();
      if (this.mPreText != component.text)
      {
        this.mPreText = component.text;
        return true;
      }
    }
    catch
    {
    }
    return false;
  }

  private void WaitInAnim()
  {
    if (!((Vector3) ref this.mPrePosition).Equals(this.rectTrans.anchoredPosition3D))
    {
      this.mPrePosition = this.rectTrans.anchoredPosition3D;
    }
    else
    {
      this.mStopTime -= Time.deltaTime;
      if (0.0 <= (double) this.mStopTime)
        return;
      this.mState = TextScrollAnim.State.START_CHECK;
    }
  }

  private void StartCheck()
  {
    try
    {
      this.mTextWidth = ((Component) this).GetComponent<Text>().preferredWidth;
    }
    catch
    {
      this.mState = TextScrollAnim.State.NONE;
      return;
    }
    if ((double) this.mTextWidth >= (double) this.mParentWidth)
    {
      this.mStopTime = this.ResetWaitTime;
      this.mState = TextScrollAnim.State.WAIT_ANIM;
    }
    else
      this.mState = TextScrollAnim.State.NONE;
  }

  private void MoveAnim()
  {
    ((Transform) this.rectTrans).Translate(-(Time.deltaTime * this.MoveSpeed), 0.0f, 0.0f, (Space) 1);
    if (0.0 <= (double) this.mTextWidth + (double) this.rectTrans.anchoredPosition3D.x)
      return;
    this.rectTrans.anchoredPosition3D = new Vector3(this.mParentWidth, this.rectTrans.anchoredPosition3D.y, this.rectTrans.anchoredPosition3D.z);
    this.mState = TextScrollAnim.State.RESET_ANIM;
  }

  private void ResetAnim()
  {
    ((Transform) this.rectTrans).Translate(-(Time.deltaTime * this.MoveSpeed * this.ResetSpeed), 0.0f, 0.0f, (Space) 1);
    if ((double) this.mBasePosition.x <= (double) this.rectTrans.anchoredPosition3D.x)
      return;
    this.rectTrans.anchoredPosition3D = new Vector3(this.mBasePosition.x, this.rectTrans.anchoredPosition3D.y, this.rectTrans.anchoredPosition3D.z);
    this.mStopTime = this.ResetWaitTime;
    this.mState = TextScrollAnim.State.WAIT_ANIM;
  }

  private void WaitAnim()
  {
    this.mStopTime -= Time.deltaTime;
    if (0.0 <= (double) this.mStopTime)
      return;
    this.mState = TextScrollAnim.State.MOVE_ANIM;
  }

  private enum State
  {
    NONE,
    INANIM_WAIT,
    START_CHECK,
    MOVE_ANIM,
    RESET_ANIM,
    WAIT_ANIM,
  }
}
