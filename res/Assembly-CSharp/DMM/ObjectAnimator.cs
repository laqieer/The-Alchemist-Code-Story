// Decompiled with JetBrains decompiler
// Type: ObjectAnimator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
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

  public bool isMoving => ((Behaviour) this).enabled;

  public float NormalizedTime
  {
    get => (double) this.mDuration > 0.0 ? Mathf.Clamp01(this.mTime / this.mDuration) : 0.0f;
  }

  private void Update()
  {
    if ((double) this.mTime < (double) this.mDuration)
    {
      this.mTime = Mathf.Min(this.mTime + Time.deltaTime, this.mDuration);
      float t = this.mTime / this.mDuration;
      float num = this.mCurve == null ? this.mCurveType.Evaluate(t) : this.mCurve.Evaluate(t);
      Transform transform = ((Component) this).transform;
      if (this.mPositionSet)
        transform.position = Vector3.Lerp(this.mStartPos, this.mEndPos, num);
      if (this.mRotationSet)
        transform.rotation = Quaternion.Slerp(this.mStartRot, this.mEndRot, num);
      if (!this.mScaleSet)
        return;
      transform.localScale = Vector3.Lerp(this.mStartScale, this.mEndScale, num);
    }
    else
      ((Behaviour) this).enabled = false;
  }

  public void ScaleTo(Vector3 scale, float duration, ObjectAnimator.CurveType curveType)
  {
    this.mPositionSet = false;
    this.mRotationSet = false;
    this.mScaleSet = true;
    this.mTime = 0.0f;
    if ((double) duration > 0.0)
    {
      this.mStartScale = ((Component) this).transform.localScale;
      this.mEndScale = scale;
      this.mCurve = (AnimationCurve) null;
      this.mCurveType = curveType;
      this.mDuration = duration;
    }
    else
    {
      ((Component) this).transform.localScale = scale;
      this.mDuration = 0.0f;
    }
    ((Behaviour) this).enabled = true;
  }

  public void AnimateTo(
    Vector3 position,
    Quaternion rotation,
    float duration,
    AnimationCurve curve)
  {
    this.AnimateTo(position, rotation, duration, ObjectAnimator.CurveType.Linear);
    this.mCurve = curve;
  }

  public void AnimateTo(
    Vector3 position,
    Quaternion rotation,
    float duration,
    ObjectAnimator.CurveType curveType)
  {
    this.mPositionSet = true;
    this.mRotationSet = true;
    this.mScaleSet = false;
    this.mTime = 0.0f;
    if ((double) duration > 0.0)
    {
      this.mStartPos = ((Component) this).transform.position;
      this.mStartRot = ((Component) this).transform.rotation;
      this.mEndPos = position;
      this.mEndRot = rotation;
      this.mCurve = (AnimationCurve) null;
      this.mCurveType = curveType;
      this.mDuration = duration;
    }
    else
    {
      ((Component) this).transform.position = position;
      ((Component) this).transform.rotation = rotation;
      this.mDuration = 0.0f;
    }
    ((Behaviour) this).enabled = true;
  }

  public static ObjectAnimator Get(Component component) => ObjectAnimator.Get(component.gameObject);

  public static ObjectAnimator Get(GameObject obj)
  {
    ObjectAnimator objectAnimator = obj.GetComponent<ObjectAnimator>();
    if (Object.op_Equality((Object) objectAnimator, (Object) null))
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
