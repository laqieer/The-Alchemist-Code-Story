// Decompiled with JetBrains decompiler
// Type: SGTextScrollAnim
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class SGTextScrollAnim : MonoBehaviour
{
  public float MoveSpeed = 30f;
  public float ResetSpeed = 50f;
  public float ResetWaitTime = 1.5f;
  public float XPositionToReset = -300f;
  public float XPositionToAppear = 500f;
  private float mStopTime = 3f;
  private Vector3 mPrePosition;
  private Vector3 mBasePosition;
  private RectTransform rectTrans;
  private string mPreText;
  private SGTextScrollAnim.State mState;

  private void Start()
  {
    try
    {
      this.rectTrans = this.GetComponent<RectTransform>();
      this.transform.parent.gameObject.GetComponent<RectTransform>();
      this.mBasePosition = this.rectTrans.anchoredPosition3D;
      this.mState = SGTextScrollAnim.State.INANIM_WAIT;
    }
    catch
    {
      this.mState = SGTextScrollAnim.State.NONE;
    }
  }

  private void Update()
  {
    switch (this.mState)
    {
      case SGTextScrollAnim.State.NONE:
      case SGTextScrollAnim.State.MOVE_ANIM:
      case SGTextScrollAnim.State.RESET_ANIM:
      case SGTextScrollAnim.State.WAIT_ANIM:
        if (this.IsTextChangeCheck())
        {
          this.rectTrans.anchoredPosition3D = new Vector3(this.mBasePosition.x, this.rectTrans.anchoredPosition3D.y, this.rectTrans.anchoredPosition3D.z);
          this.mStopTime = this.ResetWaitTime;
          this.mState = SGTextScrollAnim.State.INANIM_WAIT;
          break;
        }
        break;
    }
    switch (this.mState)
    {
      case SGTextScrollAnim.State.INANIM_WAIT:
        this.WaitInAnim();
        break;
      case SGTextScrollAnim.State.MOVE_ANIM:
        this.MoveAnim();
        break;
      case SGTextScrollAnim.State.RESET_ANIM:
        this.ResetAnim();
        break;
      case SGTextScrollAnim.State.WAIT_ANIM:
        this.WaitAnim();
        break;
    }
  }

  private bool IsTextChangeCheck()
  {
    try
    {
      Text component = this.GetComponent<Text>();
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
    this.mStopTime -= Time.deltaTime;
    if (0.0 <= (double) this.mStopTime)
      return;
    this.mState = SGTextScrollAnim.State.MOVE_ANIM;
  }

  private void MoveAnim()
  {
    this.rectTrans.Translate(-(Time.deltaTime * this.MoveSpeed), 0.0f, 0.0f, Space.Self);
    if ((double) this.rectTrans.anchoredPosition3D.x >= (double) this.XPositionToReset)
      return;
    this.rectTrans.anchoredPosition3D = new Vector3(this.XPositionToAppear, this.rectTrans.anchoredPosition3D.y, this.rectTrans.anchoredPosition3D.z);
    this.mState = SGTextScrollAnim.State.RESET_ANIM;
  }

  private void ResetAnim()
  {
    this.rectTrans.Translate(-(Time.deltaTime * this.MoveSpeed * this.ResetSpeed), 0.0f, 0.0f, Space.Self);
    if ((double) this.mBasePosition.x <= (double) this.rectTrans.anchoredPosition3D.x)
      return;
    this.rectTrans.anchoredPosition3D = new Vector3(this.mBasePosition.x, this.rectTrans.anchoredPosition3D.y, this.rectTrans.anchoredPosition3D.z);
    this.mStopTime = this.ResetWaitTime;
    this.mState = SGTextScrollAnim.State.WAIT_ANIM;
  }

  private void WaitAnim()
  {
    this.mStopTime -= Time.deltaTime;
    if (0.0 <= (double) this.mStopTime)
      return;
    this.mState = SGTextScrollAnim.State.MOVE_ANIM;
  }

  private enum State
  {
    NONE,
    INANIM_WAIT,
    MOVE_ANIM,
    RESET_ANIM,
    WAIT_ANIM,
  }
}
