// Decompiled with JetBrains decompiler
// Type: ObjectAnimator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

public class ObjectAnimator : MonoBehaviour
{
  private ObjectAnimator.CurveType mCurveType;
  private AnimationCurve mCurve;
  private Vector3 mStartPos;
  private Vector3 mEndPos;
  private Quaternion mStartRot;
  private Quaternion mEndRot;
  private Vector3 mStartScale;
  private Vector3 mEndScale;
  private float mTime;
  private float mDuration;
  private bool mPositionSet;
  private bool mRotationSet;
  private bool mScaleSet;

  public bool isMoving
  {
    get
    {
      return this.enabled;
    }
  }

  public float NormalizedTime
  {
    get
    {
      if ((double) this.mDuration > 0.0)
        return Mathf.Clamp01(this.mTime / this.mDuration);
      return 0.0f;
    }
  }

  private void Update()
  {
    if ((double) this.mTime < (double) this.mDuration)
    {
      this.mTime = Mathf.Min(this.mTime + Time.deltaTime, this.mDuration);
      float num = this.mTime / this.mDuration;
      float t = this.mCurve == null ? this.mCurveType.Evaluate(num) : this.mCurve.Evaluate(num);
      Transform transform = this.transform;
      if (this.mPositionSet)
        transform.position = Vector3.Lerp(this.mStartPos, this.mEndPos, t);
      if (this.mRotationSet)
        transform.rotation = Quaternion.Slerp(this.mStartRot, this.mEndRot, t);
      if (!this.mScaleSet)
        return;
      transform.localScale = Vector3.Lerp(this.mStartScale, this.mEndScale, t);
    }
    else
      this.enabled = false;
  }

  public void ScaleTo(Vector3 scale, float duration, ObjectAnimator.CurveType curveType)
  {
    this.mPositionSet = false;
    this.mRotationSet = false;
    this.mScaleSet = true;
    this.mTime = 0.0f;
    if ((double) duration > 0.0)
    {
      this.mStartScale = this.transform.localScale;
      this.mEndScale = scale;
      this.mCurve = (AnimationCurve) null;
      this.mCurveType = curveType;
      this.mDuration = duration;
    }
    else
    {
      this.transform.localScale = scale;
      this.mDuration = 0.0f;
    }
    this.enabled = true;
  }

  public void AnimateTo(Vector3 position, Quaternion rotation, float duration, AnimationCurve curve)
  {
    this.AnimateTo(position, rotation, duration, ObjectAnimator.CurveType.Linear);
    this.mCurve = curve;
  }

  public void AnimateTo(Vector3 position, Quaternion rotation, float duration, ObjectAnimator.CurveType curveType)
  {
    this.mPositionSet = true;
    this.mRotationSet = true;
    this.mScaleSet = false;
    this.mTime = 0.0f;
    if ((double) duration > 0.0)
    {
      this.mStartPos = this.transform.position;
      this.mStartRot = this.transform.rotation;
      this.mEndPos = position;
      this.mEndRot = rotation;
      this.mCurve = (AnimationCurve) null;
      this.mCurveType = curveType;
      this.mDuration = duration;
    }
    else
    {
      this.transform.position = position;
      this.transform.rotation = rotation;
      this.mDuration = 0.0f;
    }
    this.enabled = true;
  }

  public static ObjectAnimator Get(Component component)
  {
    return ObjectAnimator.Get(component.gameObject);
  }

  public static ObjectAnimator Get(GameObject obj)
  {
    ObjectAnimator objectAnimator = obj.GetComponent<ObjectAnimator>();
    if ((Object) objectAnimator == (Object) null)
      objectAnimator = obj.AddComponent<ObjectAnimator>();
    return objectAnimator;
  }

  public enum CurveType
  {
    Linear,
    EaseIn,
    EaseOut,
    EaseInOut,
  }
}
