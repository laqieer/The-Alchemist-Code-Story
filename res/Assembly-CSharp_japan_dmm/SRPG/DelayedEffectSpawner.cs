// Decompiled with JetBrains decompiler
// Type: SRPG.DelayedEffectSpawner
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class DelayedEffectSpawner : MonoBehaviour
  {
    private float mCurrTime;
    private List<DelayedEffectSpawner.Cue> mCues = new List<DelayedEffectSpawner.Cue>();
    private Transform mSelfTransform;
    private bool mAllSpawned;

    public void Init(SkillEffect.EffectElement[] effects, Vector3 position, Quaternion rotation)
    {
      this.mSelfTransform = ((Component) this).transform;
      this.mSelfTransform.position = position;
      this.mSelfTransform.rotation = rotation;
      if (effects == null || effects.Length <= 0)
        return;
      foreach (SkillEffect.EffectElement effect in effects)
      {
        DelayedEffectSpawner.Cue cue = new DelayedEffectSpawner.Cue();
        cue.delay = effect.Delay;
        if (Object.op_Inequality((Object) effect.Effect, (Object) null))
        {
          cue.particle = Object.Instantiate<GameObject>(effect.Effect, this.mSelfTransform);
          cue.particle.RequireComponent<OneShotParticle>();
          cue.particle.SetActive(false);
        }
        if (!string.IsNullOrEmpty(effect.CueID))
        {
          cue.customSound = ((Component) this).gameObject.AddComponent<CustomSound>();
          cue.customSound.cueID = effect.CueID;
          cue.customSound.type = effect.SoundType;
          cue.customSound.PlayOnAwake = false;
        }
        this.mCues.Add(cue);
      }
      this.mAllSpawned = false;
    }

    private void Update()
    {
      if (this.mCues == null || this.mCues.Count <= 0)
        return;
      if (this.mAllSpawned && this.mSelfTransform.childCount <= 0)
      {
        Object.Destroy((Object) ((Component) this).gameObject);
      }
      else
      {
        this.mCurrTime += Time.deltaTime;
        this.mAllSpawned = true;
        foreach (DelayedEffectSpawner.Cue mCue in this.mCues)
        {
          if ((double) mCue.delay <= (double) this.mCurrTime && !mCue.spawned)
          {
            mCue.spawned = true;
            if (Object.op_Inequality((Object) mCue.particle, (Object) null))
              mCue.particle.SetActive(true);
            if (Object.op_Inequality((Object) mCue.customSound, (Object) null))
              mCue.customSound.Play();
          }
          if (!mCue.spawned)
            this.mAllSpawned = false;
        }
      }
    }

    public ParticleSystem GetLastDurationParticle()
    {
      if (this.mCues == null || this.mCues.Count <= 0)
        return (ParticleSystem) null;
      ParticleSystem durationParticle = (ParticleSystem) null;
      float num1 = 0.0f;
      foreach (DelayedEffectSpawner.Cue mCue in this.mCues)
      {
        if (!Object.op_Equality((Object) mCue.particle, (Object) null))
        {
          ParticleSystem[] componentsInChildren = mCue.particle.GetComponentsInChildren<ParticleSystem>(true);
          if (componentsInChildren != null && componentsInChildren.Length > 0)
          {
            foreach (ParticleSystem particleSystem in componentsInChildren)
            {
              ParticleSystem.MainModule main = particleSystem.main;
              float num2 = ((ParticleSystem.MainModule) ref main).duration + mCue.delay;
              if ((double) num2 > (double) num1)
              {
                durationParticle = particleSystem;
                num1 = num2;
              }
            }
          }
        }
      }
      return durationParticle;
    }

    private class Cue
    {
      public GameObject particle;
      public CustomSound customSound;
      public bool spawned;
      public float delay;
    }
  }
}
