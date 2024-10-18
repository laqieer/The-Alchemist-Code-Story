// Decompiled with JetBrains decompiler
// Type: AnimationPlayer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using SRPG;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class AnimationPlayer : MonoBehaviour
{
  private static List<AnimationPlayer> mInstances = new List<AnimationPlayer>(16);
  public static long MaxUpdateTime = 10000000;
  private List<AnimationPlayer.AnimationStateSource> mAnimationStates = new List<AnimationPlayer.AnimationStateSource>();
  public EUnitSide UnitSide = EUnitSide.Neutral;
  [NonSerialized]
  public float RootMotionScale = 1f;
  public string DefaultAnimId = "DEFAULT";
  private List<AnimationPlayer.AnimLoadRequest> mAnimLoadRequests = new List<AnimationPlayer.AnimLoadRequest>();
  private Dictionary<string, AnimDef> mLoadedAnimations = new Dictionary<string, AnimDef>();
  private List<AnimationPlayer.EntryAnimStateInfo> mEntryAnimStateInfoList = new List<AnimationPlayer.EntryAnimStateInfo>();
  private Animation mAnimation;
  private AnimationPlayer.RootMotionModes mRootMotionMode;
  private Stopwatch mSW;
  private bool mCheckLayerHidden;
  [NonSerialized]
  public string RootMotionBoneName;
  private bool mLoadError;
  [NonSerialized]
  public Vector3 RootMotionInverse;
  public AnimDef DefaultAnim;
  public bool DefaultAnimLoop;
  public AnimationPlayer.AnimationUpdateEvent OnAnimationUpdate;
  private float mResampleTimer;
  public bool AlwaysUpdate;
  private static bool mAllAnimationsUpdated;
  private bool mUpdated;

  public AnimationPlayer.RootMotionModes RootMotionMode
  {
    get
    {
      return this.mRootMotionMode;
    }
    set
    {
      if (value == AnimationPlayer.RootMotionModes.Translate && value != this.mRootMotionMode)
      {
        Transform childRecursively = GameUtility.findChildRecursively(this.transform, this.RootMotionBoneName);
        if ((UnityEngine.Object) childRecursively != (UnityEngine.Object) null)
        {
          this.transform.position += childRecursively.parent.localToWorldMatrix.MultiplyPoint(childRecursively.localPosition) - childRecursively.parent.localToWorldMatrix.MultiplyPoint(Vector3.zero);
          childRecursively.localPosition = Vector3.zero;
        }
      }
      this.mRootMotionMode = value;
    }
  }

  protected void SetLoadError()
  {
    this.mLoadError = true;
  }

  public void ClearLoadError()
  {
    this.mLoadError = false;
  }

  public bool HasLoadError
  {
    get
    {
      return this.mLoadError;
    }
  }

  public Animation AnimationComponent
  {
    get
    {
      return this.mAnimation;
    }
  }

  protected virtual void Start()
  {
    this.mSW = new Stopwatch();
    if ((UnityEngine.Object) this.mAnimation == (UnityEngine.Object) null)
    {
      Animation component = this.GetComponent<Animation>();
      if ((UnityEngine.Object) component != (UnityEngine.Object) null)
        this.SetAnimationComponent(component);
    }
    if (!((UnityEngine.Object) this.DefaultAnim != (UnityEngine.Object) null) || string.IsNullOrEmpty(this.DefaultAnimId))
      return;
    this.AddAnimation(this.DefaultAnimId, this.DefaultAnim);
    this.PlayAnimation(this.DefaultAnimId, this.DefaultAnimLoop);
  }

  protected virtual void OnEnable()
  {
    if ((UnityEngine.Object) this.mAnimation != (UnityEngine.Object) null)
    {
      IEnumerator enumerator = this.mAnimation.GetEnumerator();
      try
      {
        while (enumerator.MoveNext())
          ((AnimationState) enumerator.Current).enabled = true;
      }
      finally
      {
        IDisposable disposable;
        if ((disposable = enumerator as IDisposable) != null)
          disposable.Dispose();
      }
    }
    AnimationPlayer.mInstances.Add(this);
  }

  protected virtual void OnDisable()
  {
    AnimationPlayer.mInstances.Remove(this);
  }

  protected virtual void OnDestroy()
  {
  }

  private float EvaluateCurveValue(AnimationCurve curve, float startTime, float endTime, float length)
  {
    startTime %= length;
    endTime %= length;
    if ((double) startTime > (double) endTime)
      return curve.Evaluate(length) - curve.Evaluate(startTime) + (curve.Evaluate(endTime) - curve.Evaluate(0.0f));
    return curve.Evaluate(endTime) - curve.Evaluate(startTime);
  }

  protected virtual void OnEventStart(AnimEvent e, float weight)
  {
  }

  protected virtual void OnEvent(AnimEvent e, float time, float weight)
  {
  }

  protected virtual void OnEventEnd(AnimEvent e, float weight)
  {
  }

  protected virtual bool IsEventAllowed(AnimEvent e)
  {
    return true;
  }

  protected void UpdateAnimationStates(float dt, bool forceUpdate)
  {
    if ((UnityEngine.Object) this.mAnimation == (UnityEngine.Object) null)
      return;
    if ((double) dt > 0.0)
      this.mResampleTimer += dt;
    Vector3 zero1 = Vector3.zero;
    Transform transform = this.transform;
    Vector3 zero2 = Vector3.zero;
    if (this.mAnimation.GetClipCount() > this.mAnimationStates.Count)
    {
      bool flag = false;
      IEnumerator enumerator = this.mAnimation.GetEnumerator();
      try
      {
        while (enumerator.MoveNext())
        {
          AnimationState current = (AnimationState) enumerator.Current;
          for (int index = 0; index < this.mAnimationStates.Count; ++index)
          {
            if ((UnityEngine.Object) this.mAnimationStates[index].Clip.animation != (UnityEngine.Object) current.clip)
            {
              this.mAnimation.RemoveClip(current.clip);
              flag = true;
              break;
            }
          }
          if (flag)
            break;
        }
      }
      finally
      {
        IDisposable disposable;
        if ((disposable = enumerator as IDisposable) != null)
          disposable.Dispose();
      }
    }
    float mResampleTimer = this.mResampleTimer;
    this.mResampleTimer = 0.0f;
    float num1 = 0.0f;
    for (int index = 0; index < this.mAnimationStates.Count; ++index)
    {
      AnimationPlayer.AnimationStateSource mAnimationState = this.mAnimationStates[index];
      mAnimationState.Weight = Mathf.MoveTowards(mAnimationState.Weight, mAnimationState.DesiredWeight, mAnimationState.BlendRate * mResampleTimer);
      num1 += mAnimationState.Weight;
    }
    for (int index = 0; index < this.mAnimationStates.Count; ++index)
    {
      AnimationPlayer.AnimationStateSource mAnimationState = this.mAnimationStates[index];
      AnimDef clip = mAnimationState.Clip;
      float length = clip.Length;
      if ((double) mAnimationState.Weight <= 0.0 && (double) mAnimationState.DesiredWeight <= 0.0)
      {
        this.mAnimationStates.RemoveAt(index);
        --index;
      }
      else
      {
        float time = mAnimationState.Time;
        mAnimationState.TimeOld = time;
        mAnimationState.Time += mResampleTimer * mAnimationState.Speed;
        float num2 = mAnimationState.Weight / num1;
        if (mAnimationState.WrapMode == WrapMode.Loop)
          mAnimationState.Time %= length;
        else if ((double) mAnimationState.Time > (double) length)
          mAnimationState.Time = length;
        if (clip.UseRootMotion && this.mRootMotionMode == AnimationPlayer.RootMotionModes.Velocity && this.RootMotionBoneName == clip.rootBoneName)
        {
          if (clip.rootTranslationX != null)
          {
            zero1.x += this.EvaluateCurveValue(clip.rootTranslationX, time, mAnimationState.Time, length) * num2;
            zero2.x -= clip.rootTranslationX.Evaluate(mAnimationState.Time) * num2;
          }
          if (clip.rootTranslationY != null)
          {
            zero1.y += this.EvaluateCurveValue(clip.rootTranslationY, time, mAnimationState.Time, length) * num2;
            zero2.y -= clip.rootTranslationY.Evaluate(mAnimationState.Time) * num2;
          }
          if (clip.rootTranslationZ != null)
          {
            zero1.z += this.EvaluateCurveValue(clip.rootTranslationZ, time, mAnimationState.Time, length) * num2;
            zero2.z -= clip.rootTranslationZ.Evaluate(mAnimationState.Time) * num2;
          }
        }
        this.AddClip(mAnimationState);
        if (this.gameObject.layer == GameUtility.LayerHidden)
          this.mCheckLayerHidden = true;
        string name = mAnimationState.Clip.animation.name;
        AnimationState animationState = this.mAnimation[mAnimationState.Name];
        if ((TrackedReference) animationState == (TrackedReference) null)
        {
          mAnimationState.Clip.animation.name = mAnimationState.Name;
          this.mAnimation.AddClip(mAnimationState.Clip.animation, mAnimationState.Name);
          animationState = this.mAnimation[mAnimationState.Name];
        }
        else if (this.mCheckLayerHidden && this.gameObject.layer != GameUtility.LayerHidden)
        {
          this.mAnimation.RemoveClip(mAnimationState.Clip.animation);
          mAnimationState.Clip.animation.name = mAnimationState.Name;
          this.mAnimation.AddClip(mAnimationState.Clip.animation, mAnimationState.Name);
          animationState = this.mAnimation[mAnimationState.Name];
          this.mCheckLayerHidden = false;
        }
        animationState.time = mAnimationState.Time;
        animationState.weight = mAnimationState.Weight;
        animationState.enabled = true;
        mAnimationState.CopiedStateRef = animationState;
        mAnimationState.Clip.animation.name = name;
      }
    }
    this.Sample();
    this.RootMotionInverse = Vector3.zero;
    if (this.mRootMotionMode == AnimationPlayer.RootMotionModes.Velocity)
    {
      if ((double) zero1.sqrMagnitude > 0.0)
      {
        zero1 *= this.RootMotionScale;
        if ((double) Mathf.Sign(transform.lossyScale.x) < 0.0)
          zero1.x *= -1f;
        if ((double) Mathf.Sign(transform.lossyScale.z) < 0.0)
          zero1.z *= -1f;
        zero1.y = 0.0f;
        transform.position += transform.rotation * zero1;
      }
      Transform childRecursively = GameUtility.findChildRecursively(transform, this.RootMotionBoneName);
      if ((UnityEngine.Object) childRecursively != (UnityEngine.Object) null)
      {
        childRecursively.localPosition += zero2;
        this.RootMotionInverse = childRecursively.parent.rotation * zero2;
      }
    }
    else if (this.mRootMotionMode == AnimationPlayer.RootMotionModes.Discard)
    {
      Transform childRecursively = GameUtility.findChildRecursively(transform, this.RootMotionBoneName);
      if ((UnityEngine.Object) childRecursively != (UnityEngine.Object) null)
        childRecursively.localPosition = new Vector3(0.0f, childRecursively.localPosition.y, 0.0f);
    }
    this.ProcessAnimationEvents();
  }

  private void AddClip(AnimationPlayer.AnimationStateSource state)
  {
    float length = state.Clip.Length;
    if (this.gameObject.layer == GameUtility.LayerHidden)
      this.mCheckLayerHidden = true;
    string name = state.Clip.animation.name;
    AnimationState animationState = this.mAnimation[state.Name];
    if ((TrackedReference) animationState == (TrackedReference) null)
    {
      state.Clip.animation.name = state.Name;
      this.mAnimation.AddClip(state.Clip.animation, state.Name);
      animationState = this.mAnimation[state.Name];
    }
    else if (this.mCheckLayerHidden && this.gameObject.layer != GameUtility.LayerHidden)
    {
      this.mAnimation.RemoveClip(state.Clip.animation);
      state.Clip.animation.name = state.Name;
      this.mAnimation.AddClip(state.Clip.animation, state.Name);
      animationState = this.mAnimation[state.Name];
      this.mCheckLayerHidden = false;
    }
    if (!((TrackedReference) animationState != (TrackedReference) null))
      return;
    animationState.time = state.Time;
    animationState.weight = state.Weight;
    animationState.enabled = true;
    state.CopiedStateRef = animationState;
    state.Clip.animation.name = name;
  }

  private void Sample()
  {
    if (this.mAnimationStates.Count <= 0)
      return;
    this.mAnimation.Sample();
    for (int index = 0; index < this.mAnimationStates.Count; ++index)
      this.mAnimationStates[index].CopiedStateRef.enabled = false;
    if (this.OnAnimationUpdate == null)
      return;
    this.OnAnimationUpdate(this.gameObject);
  }

  public void SkipToAnimationEnd()
  {
    Vector3 zero1 = Vector3.zero;
    Transform transform = this.transform;
    Vector3 zero2 = Vector3.zero;
    for (int index = 0; index < this.mAnimationStates.Count; ++index)
    {
      AnimationPlayer.AnimationStateSource mAnimationState = this.mAnimationStates[index];
      AnimDef clip = mAnimationState.Clip;
      float length = clip.Length;
      if ((double) mAnimationState.Weight <= 0.0 && (double) mAnimationState.DesiredWeight <= 0.0)
      {
        this.mAnimationStates.RemoveAt(index);
        --index;
      }
      else
      {
        if (mAnimationState.WrapMode == WrapMode.Loop)
          mAnimationState.Time %= length;
        else if ((double) mAnimationState.Time > (double) length)
          mAnimationState.Time = length;
        if (clip.UseRootMotion && this.mRootMotionMode == AnimationPlayer.RootMotionModes.Velocity && this.RootMotionBoneName == clip.rootBoneName)
        {
          if (clip.rootTranslationX != null)
          {
            zero1.x += clip.rootTranslationX.Evaluate(length) - clip.rootTranslationX.Evaluate(0.0f);
            zero2.x -= clip.rootTranslationX.Evaluate(length);
          }
          if (clip.rootTranslationY != null)
          {
            zero1.y += clip.rootTranslationY.Evaluate(length) - clip.rootTranslationY.Evaluate(0.0f);
            zero2.y -= clip.rootTranslationY.Evaluate(length);
          }
          if (clip.rootTranslationZ != null)
          {
            zero1.z += clip.rootTranslationZ.Evaluate(length) - clip.rootTranslationZ.Evaluate(0.0f);
            zero2.z -= clip.rootTranslationZ.Evaluate(length);
          }
        }
      }
    }
    this.UpdateAnimationStates(0.0f, true);
    if (this.mRootMotionMode != AnimationPlayer.RootMotionModes.Velocity)
      return;
    if ((double) zero1.sqrMagnitude > 0.0)
    {
      Vector3 vector3 = zero1 * this.RootMotionScale;
      if ((double) Mathf.Sign(transform.lossyScale.x) < 0.0)
        vector3.x *= -1f;
      if ((double) Mathf.Sign(transform.lossyScale.z) < 0.0)
        vector3.z *= -1f;
      vector3.y = 0.0f;
      transform.position += transform.rotation * vector3;
    }
    Transform childRecursively = GameUtility.findChildRecursively(transform, this.RootMotionBoneName);
    if (!((UnityEngine.Object) childRecursively != (UnityEngine.Object) null))
      return;
    childRecursively.localPosition += zero2;
    this.RootMotionInverse = childRecursively.parent.rotation * zero2;
  }

  private void ProcessAnimationEvents()
  {
    for (int index1 = 0; index1 < this.mAnimationStates.Count; ++index1)
    {
      AnimationPlayer.AnimationStateSource mAnimationState = this.mAnimationStates[index1];
      AnimDef clip = mAnimationState.Clip;
      float length = clip.Length;
      if ((double) mAnimationState.Weight > 0.0 && clip.events != null)
      {
        for (int index2 = 0; index2 < clip.events.Length; ++index2)
        {
          AnimEvent e = clip.events[index2];
          if (!((UnityEngine.Object) e == (UnityEngine.Object) null) && this.IsEventAllowed(e))
          {
            float num1 = Mathf.Min(e.Start, length);
            float end = e.End;
            float num2 = mAnimationState.WrapMode != WrapMode.Loop || (double) mAnimationState.Time >= (double) mAnimationState.TimeOld ? mAnimationState.TimeOld : mAnimationState.TimeOld - length;
            if ((double) num1 < (double) length)
            {
              if ((double) num2 <= (double) num1 && (double) num1 < (double) mAnimationState.Time)
              {
                this.OnEventStart(e, mAnimationState.Weight);
                e.OnStart(this.gameObject);
              }
            }
            else if ((double) num2 < (double) num1 && (double) num1 <= (double) mAnimationState.Time)
            {
              this.OnEventStart(e, mAnimationState.Weight);
              e.OnStart(this.gameObject);
            }
            if ((double) num1 <= (double) mAnimationState.Time && (double) mAnimationState.Time < (double) end)
            {
              float num3 = e.End - e.Start;
              this.OnEvent(e, mAnimationState.Time, mAnimationState.Weight);
              e.OnTick(this.gameObject, (double) num3 <= 0.0 ? 0.0f : (mAnimationState.Time - e.Start) / num3);
            }
            if ((double) num2 < (double) end && (double) end <= (double) mAnimationState.Time)
            {
              this.OnEventEnd(e, mAnimationState.Weight);
              e.OnEnd(this.gameObject);
            }
          }
        }
      }
    }
  }

  public virtual float LoadProgress
  {
    get
    {
      int count1 = this.mLoadedAnimations.Count;
      int count2 = this.mAnimLoadRequests.Count;
      int num = count1 + count2;
      if (count1 + count2 <= 0)
        return 0.0f;
      return (float) (1.0 - (double) count2 / (double) num);
    }
  }

  public virtual bool IsLoading
  {
    get
    {
      return this.mAnimLoadRequests.Count > 0;
    }
  }

  public int LoadingAnimationCount
  {
    get
    {
      return this.mAnimLoadRequests.Count;
    }
  }

  protected virtual void Update()
  {
    if (!AnimationPlayer.mAllAnimationsUpdated)
    {
      AnimationPlayer.mAllAnimationsUpdated = true;
      long num = 0;
      if (this.mSW == null)
        this.mSW = new Stopwatch();
      float deltaTime = Time.deltaTime;
      for (int index = AnimationPlayer.mInstances.Count - 1; index >= 0; --index)
        AnimationPlayer.mInstances[index].mResampleTimer += deltaTime;
      for (int count = AnimationPlayer.mInstances.Count; count > 0 && num < AnimationPlayer.MaxUpdateTime; --count)
      {
        this.mSW.Reset();
        this.mSW.Start();
        AnimationPlayer.mInstances[0].UpdateAnimationStates(0.0f, false);
        AnimationPlayer.mInstances[0].mUpdated = true;
        AnimationPlayer.mInstances.Add(AnimationPlayer.mInstances[0]);
        AnimationPlayer.mInstances.RemoveAt(0);
        this.mSW.Stop();
        num += this.mSW.ElapsedTicks;
      }
    }
    if (this.mUpdated || !this.AlwaysUpdate)
      return;
    this.UpdateAnimationStates(0.0f, false);
    this.mUpdated = true;
  }

  protected virtual void LateUpdate()
  {
    AnimationPlayer.mAllAnimationsUpdated = false;
    this.mUpdated = false;
  }

  public void StopAll()
  {
    this.mAnimationStates.Clear();
  }

  public void StopAnimation(string id)
  {
    AnimationPlayer.AnimationStateSource state = this.FindState(id);
    if (state == null)
      return;
    this.mAnimationStates.Remove(state);
  }

  private AnimationPlayer.AnimationStateSource FindState(string id)
  {
    for (int index = this.mAnimationStates.Count - 1; index >= 0; --index)
    {
      if (this.mAnimationStates[index].Name == id)
        return this.mAnimationStates[index];
    }
    return (AnimationPlayer.AnimationStateSource) null;
  }

  public bool IsAnimationPlaying(string id)
  {
    return this.FindState(id) != null;
  }

  public float GetRemainingTime(string id)
  {
    AnimationPlayer.AnimationStateSource state = this.FindState(id);
    if (state == null || state.WrapMode == WrapMode.Loop)
      return 0.0f;
    return state.Clip.Length - state.Time;
  }

  public void SetSpeed(string id, float speed)
  {
    AnimationPlayer.AnimationStateSource state = this.FindState(id);
    if (state == null)
      DebugUtility.LogError("Animation ID " + id + " not found");
    else
      state.Speed = speed;
  }

  public void SetAnimationComponent(Animation animComponent)
  {
    this.mAnimation = animComponent;
    if (!((UnityEngine.Object) this.mAnimation != (UnityEngine.Object) null))
      return;
    this.mAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
  }

  public float GetNormalizedTime(string id)
  {
    AnimationPlayer.AnimationStateSource state = this.FindState(id);
    if (state == null)
      return 0.0f;
    return state.Time / state.Clip.Length;
  }

  public float GetTargetWeight(string id)
  {
    AnimationPlayer.AnimationStateSource state = this.FindState(id);
    if (state == null)
      return 0.0f;
    return state.DesiredWeight;
  }

  public void PlayAnimation(string id, bool loop, float interpTime, float startTime = 0.0f)
  {
    if ((double) interpTime <= 0.0)
      this.StopAll();
    if (!this.mLoadedAnimations.ContainsKey(id))
    {
      DebugUtility.LogError("Unknown animation ID: " + id);
    }
    else
    {
      AnimDef mLoadedAnimation = this.mLoadedAnimations[id];
      if ((UnityEngine.Object) mLoadedAnimation == (UnityEngine.Object) null || (UnityEngine.Object) mLoadedAnimation.animation == (UnityEngine.Object) null)
      {
        DebugUtility.LogError("Animation not loaded: " + id);
      }
      else
      {
        AnimationPlayer.AnimationStateSource state = (AnimationPlayer.AnimationStateSource) null;
        for (int index = 0; index < this.mAnimationStates.Count; ++index)
        {
          if (this.mAnimationStates[index].Name == id)
          {
            state = this.mAnimationStates[index];
            break;
          }
        }
        if (state == null)
        {
          state = new AnimationPlayer.AnimationStateSource();
          state.Clip = mLoadedAnimation;
          this.mAnimationStates.Add(state);
        }
        state.Time = startTime;
        state.Name = id;
        state.WrapMode = !loop ? WrapMode.Default : WrapMode.Loop;
        if ((double) interpTime > 0.0)
        {
          state.Weight = 0.0f;
          state.DesiredWeight = 1f;
          state.BlendRate = 1f / interpTime;
          for (int index = 0; index < this.mAnimationStates.Count; ++index)
          {
            if (this.mAnimationStates[index] != state)
            {
              this.mAnimationStates[index].DesiredWeight = 0.0f;
              this.mAnimationStates[index].BlendRate = state.BlendRate;
            }
          }
        }
        else
        {
          state.Weight = 1f;
          state.DesiredWeight = 1f;
        }
        this.AddClip(state);
        this.Sample();
      }
    }
  }

  protected AnimDef FindAnimation(string id)
  {
    if (!this.mLoadedAnimations.ContainsKey(id))
      return (AnimDef) null;
    AnimDef mLoadedAnimation = this.mLoadedAnimations[id];
    if ((UnityEngine.Object) mLoadedAnimation == (UnityEngine.Object) null || (UnityEngine.Object) mLoadedAnimation.animation == (UnityEngine.Object) null)
      return (AnimDef) null;
    return mLoadedAnimation;
  }

  public float GetLength(string id)
  {
    AnimDef animation = this.FindAnimation(id);
    if ((UnityEngine.Object) animation == (UnityEngine.Object) null)
      return 0.0f;
    return animation.Length;
  }

  private static void HACK_Animation_AddClip(Animation animation, AnimationClip clip, string newName)
  {
    string name = clip.name;
    clip.name = newName;
    animation.AddClip(clip, newName);
    AnimationState animationState = animation[newName];
    animationState.name = animationState.name;
    clip.name = name;
  }

  public void PlayAnimation(string id, bool loop)
  {
    if ((UnityEngine.Object) this.mAnimation == (UnityEngine.Object) null)
      return;
    this.StopAll();
    this.PlayAnimation(id, loop, 0.0f, 0.0f);
  }

  public AnimDef GetActiveAnimation(out float position)
  {
    if ((UnityEngine.Object) this.mAnimation != (UnityEngine.Object) null)
    {
      this.EntryAnimState();
      for (int index = 0; index < this.mEntryAnimStateInfoList.Count; ++index)
      {
        AnimDef animation = this.FindAnimation(this.mEntryAnimStateInfoList[index].AnimName);
        if (!((UnityEngine.Object) animation == (UnityEngine.Object) null))
        {
          position = this.mEntryAnimStateInfoList[index].AnimState.wrapMode != WrapMode.Loop ? this.mEntryAnimStateInfoList[index].AnimState.time : this.mEntryAnimStateInfoList[index].AnimState.time % this.mEntryAnimStateInfoList[index].AnimState.length;
          return animation;
        }
      }
    }
    position = 0.0f;
    return (AnimDef) null;
  }

  private void EntryAnimState()
  {
    bool flag = false;
    if (this.mEntryAnimStateInfoList.Count == 0)
      flag = true;
    else if (this.mAnimation.GetClipCount() > 0)
    {
      if (this.mAnimation.GetClipCount() != this.mEntryAnimStateInfoList.Count)
      {
        flag = true;
      }
      else
      {
        for (int index = 0; index < this.mEntryAnimStateInfoList.Count; ++index)
        {
          if ((TrackedReference) this.mAnimation[this.mEntryAnimStateInfoList[index].AnimName] == (TrackedReference) null || (TrackedReference) this.mEntryAnimStateInfoList[index].AnimState == (TrackedReference) null)
          {
            flag = true;
            break;
          }
        }
      }
    }
    if (!flag)
      return;
    this.mEntryAnimStateInfoList.Clear();
    IEnumerator enumerator = this.mAnimation.GetEnumerator();
    try
    {
      while (enumerator.MoveNext())
        this.mEntryAnimStateInfoList.Add(new AnimationPlayer.EntryAnimStateInfo((AnimationState) enumerator.Current));
    }
    finally
    {
      IDisposable disposable;
      if ((disposable = enumerator as IDisposable) != null)
        disposable.Dispose();
    }
  }

  public AnimDef GetAnimation(string id)
  {
    if (this.mLoadedAnimations.ContainsKey(id))
      return this.mLoadedAnimations[id];
    return (AnimDef) null;
  }

  public void LoadAnimationAsync(string id, string path)
  {
    this.mAnimLoadRequests.Add(new AnimationPlayer.AnimLoadRequest()
    {
      id = id,
      path = path,
      request = AssetManager.LoadAsync(path, typeof (AnimDef))
    });
    if (this.mAnimLoadRequests.Count < 1)
      return;
    this.StartCoroutine(this.AsyncLoadAnimation());
  }

  public void AddAnimation(string id, AnimDef anim)
  {
    this.mLoadedAnimations[id] = anim;
  }

  public void UnloadAnimation(string id)
  {
    this.mLoadedAnimations.Remove(id);
  }

  [DebuggerHidden]
  private IEnumerator AsyncLoadAnimation()
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new AnimationPlayer.\u003CAsyncLoadAnimation\u003Ec__Iterator0() { \u0024this = this };
  }

  private class AnimationStateSource
  {
    public float Speed = 1f;
    public float BlendRate = 1f;
    public string Name;
    public AnimDef Clip;
    public float Time;
    public float TimeOld;
    public float Weight;
    public WrapMode WrapMode;
    public AnimationState CopiedStateRef;
    public float DesiredWeight;
  }

  public enum RootMotionModes
  {
    Translate,
    Velocity,
    Discard,
  }

  public delegate void AnimationUpdateEvent(GameObject go);

  private class AnimLoadRequest
  {
    public string id;
    public string path;
    public LoadRequest request;
  }

  private class EntryAnimStateInfo
  {
    private string name;
    private AnimationState state;

    public EntryAnimStateInfo(AnimationState _state)
    {
      this.state = _state;
      this.name = _state.name;
    }

    public string AnimName
    {
      get
      {
        return this.name;
      }
    }

    public AnimationState AnimState
    {
      get
      {
        return this.state;
      }
    }
  }
}
