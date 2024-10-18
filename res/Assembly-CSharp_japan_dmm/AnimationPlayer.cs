// Decompiled with JetBrains decompiler
// Type: AnimationPlayer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using SRPG;
using SRPG.AnimEvents;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

#nullable disable
public class AnimationPlayer : MonoBehaviour
{
  private List<AnimationPlayer.AnimationStateSource> mAnimationStates = new List<AnimationPlayer.AnimationStateSource>();
  private Animation mAnimation;
  public EUnitSide UnitSide = EUnitSide.Neutral;
  private AnimationPlayer.RootMotionModes mRootMotionMode;
  private Stopwatch mSW;
  private bool mCheckLayerHidden;
  [NonSerialized]
  public float RootMotionScale = 1f;
  [NonSerialized]
  public string RootMotionBoneName;
  private bool mLoadError;
  [NonSerialized]
  public Vector3 RootMotionInverse;
  public string DefaultAnimId = "DEFAULT";
  public AnimDef DefaultAnim;
  public bool DefaultAnimLoop;
  private static List<AnimationPlayer> mInstances = new List<AnimationPlayer>(16);
  public AnimationPlayer.AnimationUpdateEvent OnAnimationUpdate;
  private float mResampleTimer;
  public bool AlwaysUpdate;
  private static bool mAllAnimationsUpdated;
  private bool mUpdated;
  public static long MaxUpdateTime = 10000000;
  private List<AnimationPlayer.AnimLoadRequest> mAnimLoadRequests = new List<AnimationPlayer.AnimLoadRequest>();
  private Dictionary<string, AnimDef> mLoadedAnimations = new Dictionary<string, AnimDef>();
  private List<AnimationPlayer.EntryAnimStateInfo> mEntryAnimStateInfoList = new List<AnimationPlayer.EntryAnimStateInfo>();

  public AnimationPlayer.RootMotionModes RootMotionMode
  {
    get => this.mRootMotionMode;
    set
    {
      if (value == AnimationPlayer.RootMotionModes.Translate && value != this.mRootMotionMode)
      {
        Transform childRecursively = GameUtility.findChildRecursively(((Component) this).transform, this.RootMotionBoneName);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) childRecursively, (UnityEngine.Object) null))
        {
          Matrix4x4 localToWorldMatrix1 = childRecursively.parent.localToWorldMatrix;
          Vector3 vector3_1 = ((Matrix4x4) ref localToWorldMatrix1).MultiplyPoint(childRecursively.localPosition);
          Matrix4x4 localToWorldMatrix2 = childRecursively.parent.localToWorldMatrix;
          Vector3 vector3_2 = ((Matrix4x4) ref localToWorldMatrix2).MultiplyPoint(Vector3.zero);
          Vector3 vector3_3 = Vector3.op_Subtraction(vector3_1, vector3_2);
          Transform transform = ((Component) this).transform;
          transform.position = Vector3.op_Addition(transform.position, vector3_3);
          childRecursively.localPosition = Vector3.zero;
        }
      }
      this.mRootMotionMode = value;
    }
  }

  protected void SetLoadError() => this.mLoadError = true;

  public void ClearLoadError() => this.mLoadError = false;

  public bool HasLoadError => this.mLoadError;

  public Animation AnimationComponent => this.mAnimation;

  protected virtual void Start()
  {
    this.mSW = new Stopwatch();
    if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mAnimation, (UnityEngine.Object) null))
    {
      Animation component = ((Component) this).GetComponent<Animation>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        this.SetAnimationComponent(component);
    }
    if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.DefaultAnim, (UnityEngine.Object) null) || string.IsNullOrEmpty(this.DefaultAnimId))
      return;
    this.AddAnimation(this.DefaultAnimId, this.DefaultAnim);
    this.PlayAnimation(this.DefaultAnimId, this.DefaultAnimLoop);
  }

  protected virtual void OnEnable()
  {
    if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mAnimation, (UnityEngine.Object) null))
    {
      foreach (AnimationState animationState in this.mAnimation)
        animationState.enabled = true;
    }
    AnimationPlayer.mInstances.Add(this);
  }

  protected virtual void OnDisable() => AnimationPlayer.mInstances.Remove(this);

  protected virtual void OnDestroy()
  {
  }

  private float EvaluateCurveValue(
    AnimationCurve curve,
    float startTime,
    float endTime,
    float length)
  {
    startTime %= length;
    endTime %= length;
    return (double) startTime > (double) endTime ? curve.Evaluate(length) - curve.Evaluate(startTime) + (curve.Evaluate(endTime) - curve.Evaluate(0.0f)) : curve.Evaluate(endTime) - curve.Evaluate(startTime);
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

  protected virtual bool IsEventAllowed(AnimEvent e) => true;

  protected void UpdateAnimationStates(float dt, bool forceUpdate)
  {
    if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mAnimation, (UnityEngine.Object) null))
      return;
    if ((double) dt > 0.0)
      this.mResampleTimer += dt;
    Vector3 vector3 = Vector3.zero;
    Transform transform1 = ((Component) this).transform;
    Vector3 zero = Vector3.zero;
    if (this.mAnimation.GetClipCount() > this.mAnimationStates.Count)
    {
      bool flag = false;
      foreach (AnimationState animationState in this.mAnimation)
      {
        for (int index = 0; index < this.mAnimationStates.Count; ++index)
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mAnimationStates[index].Clip.animation, (UnityEngine.Object) animationState.clip))
          {
            this.mAnimation.RemoveClip(animationState.clip);
            flag = true;
            break;
          }
        }
        if (flag)
          break;
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
        if (mAnimationState.WrapMode == 2)
          mAnimationState.Time %= length;
        else if ((double) mAnimationState.Time > (double) length)
          mAnimationState.Time = length;
        if (clip.UseRootMotion && this.mRootMotionMode == AnimationPlayer.RootMotionModes.Velocity && this.RootMotionBoneName == clip.rootBoneName)
        {
          if (clip.rootTranslationX != null)
          {
            vector3.x += this.EvaluateCurveValue(clip.rootTranslationX, time, mAnimationState.Time, length) * num2;
            zero.x -= clip.rootTranslationX.Evaluate(mAnimationState.Time) * num2;
          }
          if (clip.rootTranslationY != null)
          {
            vector3.y += this.EvaluateCurveValue(clip.rootTranslationY, time, mAnimationState.Time, length) * num2;
            zero.y -= clip.rootTranslationY.Evaluate(mAnimationState.Time) * num2;
          }
          if (clip.rootTranslationZ != null)
          {
            vector3.z += this.EvaluateCurveValue(clip.rootTranslationZ, time, mAnimationState.Time, length) * num2;
            zero.z -= clip.rootTranslationZ.Evaluate(mAnimationState.Time) * num2;
          }
        }
        this.AddClip(mAnimationState);
        if (((Component) this).gameObject.layer == GameUtility.LayerHidden)
          this.mCheckLayerHidden = true;
        string name = ((UnityEngine.Object) mAnimationState.Clip.animation).name;
        AnimationState animationState = this.mAnimation[mAnimationState.Name];
        if (TrackedReference.op_Equality((TrackedReference) animationState, (TrackedReference) null))
        {
          ((UnityEngine.Object) mAnimationState.Clip.animation).name = mAnimationState.Name;
          this.mAnimation.AddClip(mAnimationState.Clip.animation, mAnimationState.Name);
          animationState = this.mAnimation[mAnimationState.Name];
        }
        else if (this.mCheckLayerHidden && ((Component) this).gameObject.layer != GameUtility.LayerHidden)
        {
          this.mAnimation.RemoveClip(mAnimationState.Clip.animation);
          ((UnityEngine.Object) mAnimationState.Clip.animation).name = mAnimationState.Name;
          this.mAnimation.AddClip(mAnimationState.Clip.animation, mAnimationState.Name);
          animationState = this.mAnimation[mAnimationState.Name];
          this.mCheckLayerHidden = false;
        }
        animationState.time = mAnimationState.Time;
        animationState.weight = mAnimationState.Weight;
        animationState.enabled = true;
        mAnimationState.CopiedStateRef = animationState;
        ((UnityEngine.Object) mAnimationState.Clip.animation).name = name;
      }
    }
    this.Sample();
    this.RootMotionInverse = Vector3.zero;
    if (this.mRootMotionMode == AnimationPlayer.RootMotionModes.Velocity)
    {
      if ((double) ((Vector3) ref vector3).sqrMagnitude > 0.0)
      {
        vector3 = Vector3.op_Multiply(vector3, this.RootMotionScale);
        if ((double) Mathf.Sign(transform1.lossyScale.x) < 0.0)
          vector3.x *= -1f;
        if ((double) Mathf.Sign(transform1.lossyScale.z) < 0.0)
          vector3.z *= -1f;
        vector3.y = 0.0f;
        Transform transform2 = transform1;
        transform2.position = Vector3.op_Addition(transform2.position, Quaternion.op_Multiply(transform1.rotation, vector3));
      }
      Transform childRecursively = GameUtility.findChildRecursively(transform1, this.RootMotionBoneName);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) childRecursively, (UnityEngine.Object) null))
      {
        Transform transform3 = childRecursively;
        transform3.localPosition = Vector3.op_Addition(transform3.localPosition, zero);
        this.RootMotionInverse = Quaternion.op_Multiply(childRecursively.parent.rotation, zero);
      }
    }
    else if (this.mRootMotionMode == AnimationPlayer.RootMotionModes.Discard)
    {
      Transform childRecursively = GameUtility.findChildRecursively(transform1, this.RootMotionBoneName);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) childRecursively, (UnityEngine.Object) null))
        childRecursively.localPosition = new Vector3(0.0f, childRecursively.localPosition.y, 0.0f);
    }
    this.ProcessAnimationEvents();
  }

  private void AddClip(AnimationPlayer.AnimationStateSource state)
  {
    float length = state.Clip.Length;
    if (((Component) this).gameObject.layer == GameUtility.LayerHidden)
      this.mCheckLayerHidden = true;
    string name = ((UnityEngine.Object) state.Clip.animation).name;
    AnimationState animationState = this.mAnimation[state.Name];
    if (TrackedReference.op_Equality((TrackedReference) animationState, (TrackedReference) null))
    {
      ((UnityEngine.Object) state.Clip.animation).name = state.Name;
      this.mAnimation.AddClip(state.Clip.animation, state.Name);
      animationState = this.mAnimation[state.Name];
    }
    else if (this.mCheckLayerHidden && ((Component) this).gameObject.layer != GameUtility.LayerHidden)
    {
      this.mAnimation.RemoveClip(state.Clip.animation);
      ((UnityEngine.Object) state.Clip.animation).name = state.Name;
      this.mAnimation.AddClip(state.Clip.animation, state.Name);
      animationState = this.mAnimation[state.Name];
      this.mCheckLayerHidden = false;
    }
    if (!TrackedReference.op_Inequality((TrackedReference) animationState, (TrackedReference) null))
      return;
    animationState.time = state.Time;
    animationState.weight = state.Weight;
    animationState.enabled = true;
    state.CopiedStateRef = animationState;
    ((UnityEngine.Object) state.Clip.animation).name = name;
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
    this.OnAnimationUpdate(((Component) this).gameObject);
  }

  public void SkipToAnimationEnd()
  {
    Vector3 zero1 = Vector3.zero;
    Transform transform1 = ((Component) this).transform;
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
        if (mAnimationState.WrapMode == 2)
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
    if ((double) ((Vector3) ref zero1).sqrMagnitude > 0.0)
    {
      Vector3 vector3 = Vector3.op_Multiply(zero1, this.RootMotionScale);
      if ((double) Mathf.Sign(transform1.lossyScale.x) < 0.0)
        vector3.x *= -1f;
      if ((double) Mathf.Sign(transform1.lossyScale.z) < 0.0)
        vector3.z *= -1f;
      vector3.y = 0.0f;
      Transform transform2 = transform1;
      transform2.position = Vector3.op_Addition(transform2.position, Quaternion.op_Multiply(transform1.rotation, vector3));
    }
    Transform childRecursively = GameUtility.findChildRecursively(transform1, this.RootMotionBoneName);
    if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) childRecursively, (UnityEngine.Object) null))
      return;
    Transform transform3 = childRecursively;
    transform3.localPosition = Vector3.op_Addition(transform3.localPosition, zero2);
    this.RootMotionInverse = Quaternion.op_Multiply(childRecursively.parent.rotation, zero2);
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
          if (!UnityEngine.Object.op_Equality((UnityEngine.Object) e, (UnityEngine.Object) null) && this.IsEventAllowed(e))
          {
            float num1 = Mathf.Min(e.Start, length);
            float num2 = Mathf.Min(e.End, length);
            if (e is CharacterGenerator)
              num2 = e.End;
            float num3 = mAnimationState.WrapMode != 2 || (double) mAnimationState.Time >= (double) mAnimationState.TimeOld ? mAnimationState.TimeOld : mAnimationState.TimeOld - length;
            if ((double) num1 < (double) length)
            {
              if ((double) num3 <= (double) num1 && (double) num1 < (double) mAnimationState.Time)
              {
                this.OnEventStart(e, mAnimationState.Weight);
                e.OnStart(((Component) this).gameObject);
              }
            }
            else if ((double) num3 < (double) num1 && (double) num1 <= (double) mAnimationState.Time)
            {
              this.OnEventStart(e, mAnimationState.Weight);
              e.OnStart(((Component) this).gameObject);
            }
            if ((double) num1 <= (double) mAnimationState.Time && (double) mAnimationState.Time < (double) num2)
            {
              float num4 = e.End - e.Start;
              this.OnEvent(e, mAnimationState.Time, mAnimationState.Weight);
              e.OnTick(((Component) this).gameObject, (double) num4 <= 0.0 ? 0.0f : (mAnimationState.Time - e.Start) / num4);
            }
            if ((double) num3 < (double) num2 && (double) num2 <= (double) mAnimationState.Time)
            {
              this.OnEventEnd(e, mAnimationState.Weight);
              e.OnEnd(((Component) this).gameObject);
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
      return count1 + count2 <= 0 ? 0.0f : (float) (1.0 - (double) count2 / (double) num);
    }
  }

  public virtual bool IsLoading => this.mAnimLoadRequests.Count > 0;

  public int LoadingAnimationCount => this.mAnimLoadRequests.Count;

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

  public void StopAll() => this.mAnimationStates.Clear();

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

  public bool IsAnimationPlaying(string id) => this.FindState(id) != null;

  public float GetRemainingTime(string id)
  {
    AnimationPlayer.AnimationStateSource state = this.FindState(id);
    return state == null || state.WrapMode == 2 ? 0.0f : state.Clip.Length - state.Time;
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
    if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mAnimation, (UnityEngine.Object) null))
      return;
    this.mAnimation.cullingType = (AnimationCullingType) 0;
  }

  public float GetNormalizedTime(string id)
  {
    AnimationPlayer.AnimationStateSource state = this.FindState(id);
    return state == null ? 0.0f : state.Time / state.Clip.Length;
  }

  public float GetTargetWeight(string id)
  {
    AnimationPlayer.AnimationStateSource state = this.FindState(id);
    return state == null ? 0.0f : state.DesiredWeight;
  }

  public void ResetAnimation()
  {
    this.StopAll();
    if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mAnimation, (UnityEngine.Object) null))
      return;
    OneShotParticle[] componentsInChildren = ((Component) this).GetComponentsInChildren<OneShotParticle>(true);
    if (componentsInChildren == null)
      return;
    foreach (Component component in componentsInChildren)
      UnityEngine.Object.Destroy((UnityEngine.Object) component.gameObject);
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
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) mLoadedAnimation, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) mLoadedAnimation.animation, (UnityEngine.Object) null))
      {
        DebugUtility.LogError("Animation not loaded: " + id);
      }
      else
      {
        AnimEvent[] events = mLoadedAnimation.events;
        if (events != null)
        {
          foreach (AnimEvent animEvent in events)
          {
            ParticleGenerator particleGenerator = animEvent as ParticleGenerator;
            if (!UnityEngine.Object.op_Equality((UnityEngine.Object) particleGenerator, (UnityEngine.Object) null))
              particleGenerator.PreLoad(((Component) this).gameObject);
          }
        }
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
        state.WrapMode = !loop ? (WrapMode) 0 : (WrapMode) 2;
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
    return UnityEngine.Object.op_Equality((UnityEngine.Object) mLoadedAnimation, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) mLoadedAnimation.animation, (UnityEngine.Object) null) ? (AnimDef) null : mLoadedAnimation;
  }

  public float GetLength(string id)
  {
    AnimDef animation = this.FindAnimation(id);
    return UnityEngine.Object.op_Equality((UnityEngine.Object) animation, (UnityEngine.Object) null) ? 0.0f : animation.Length;
  }

  private static void HACK_Animation_AddClip(
    Animation animation,
    AnimationClip clip,
    string newName)
  {
    string name = ((UnityEngine.Object) clip).name;
    ((UnityEngine.Object) clip).name = newName;
    animation.AddClip(clip, newName);
    AnimationState animationState = animation[newName];
    animationState.name = animationState.name;
    ((UnityEngine.Object) clip).name = name;
  }

  public void PlayAnimation(string id, bool loop)
  {
    if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mAnimation, (UnityEngine.Object) null))
      return;
    this.StopAll();
    this.PlayAnimation(id, loop, 0.0f);
  }

  public AnimDef GetActiveAnimation(out float position)
  {
    if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mAnimation, (UnityEngine.Object) null))
    {
      this.EntryAnimState();
      for (int index = 0; index < this.mEntryAnimStateInfoList.Count; ++index)
      {
        AnimDef animation = this.FindAnimation(this.mEntryAnimStateInfoList[index].AnimName);
        if (!UnityEngine.Object.op_Equality((UnityEngine.Object) animation, (UnityEngine.Object) null))
        {
          position = this.mEntryAnimStateInfoList[index].AnimState.wrapMode != 2 ? this.mEntryAnimStateInfoList[index].AnimState.time : this.mEntryAnimStateInfoList[index].AnimState.time % this.mEntryAnimStateInfoList[index].AnimState.length;
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
          if (TrackedReference.op_Equality((TrackedReference) this.mAnimation[this.mEntryAnimStateInfoList[index].AnimName], (TrackedReference) null) || TrackedReference.op_Equality((TrackedReference) this.mEntryAnimStateInfoList[index].AnimState, (TrackedReference) null))
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
    foreach (AnimationState _state in this.mAnimation)
      this.mEntryAnimStateInfoList.Add(new AnimationPlayer.EntryAnimStateInfo(_state));
  }

  public AnimDef GetAnimation(string id)
  {
    return this.mLoadedAnimations.ContainsKey(id) ? this.mLoadedAnimations[id] : (AnimDef) null;
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

  public void AddAnimation(string id, AnimDef anim) => this.mLoadedAnimations[id] = anim;

  public void UnloadAnimation(string id) => this.mLoadedAnimations.Remove(id);

  [DebuggerHidden]
  private IEnumerator AsyncLoadAnimation()
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new AnimationPlayer.\u003CAsyncLoadAnimation\u003Ec__Iterator0()
    {
      \u0024this = this
    };
  }

  private AnimDef CreateInstance(AnimDef asset)
  {
    AnimDef instance = UnityEngine.Object.Instantiate<AnimDef>(asset);
    ((UnityEngine.Object) instance).name = ((UnityEngine.Object) asset).name;
    if (instance.events != null && instance.events.Length > 0)
    {
      List<AnimEvent> animEventList = new List<AnimEvent>();
      foreach (AnimEvent animEvent1 in asset.events)
      {
        if (!UnityEngine.Object.op_Equality((UnityEngine.Object) animEvent1, (UnityEngine.Object) null))
        {
          AnimEvent animEvent2 = UnityEngine.Object.Instantiate<AnimEvent>(animEvent1);
          ((UnityEngine.Object) animEvent2).name = ((UnityEngine.Object) animEvent1).name;
          animEventList.Add(animEvent2);
        }
      }
      instance.events = animEventList.ToArray();
    }
    return instance;
  }

  private class AnimationStateSource
  {
    public string Name;
    public AnimDef Clip;
    public float Speed = 1f;
    public float Time;
    public float TimeOld;
    public float Weight;
    public WrapMode WrapMode;
    public AnimationState CopiedStateRef;
    public float DesiredWeight;
    public float BlendRate = 1f;
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

    public string AnimName => this.name;

    public AnimationState AnimState => this.state;
  }
}
