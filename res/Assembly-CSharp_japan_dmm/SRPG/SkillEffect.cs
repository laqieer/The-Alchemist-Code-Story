// Decompiled with JetBrains decompiler
// Type: SRPG.SkillEffect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class SkillEffect : ScriptableObject
  {
    [HideInInspector]
    public SkillEffect.SFX StartSound;
    [HideInInspector]
    public GameObject ChantEffect;
    [HideInInspector]
    public SkillEffect.SFX ChantSound;
    [HeaderBar("オーラ")]
    public GameObject AuraEffect;
    [HideInInspector]
    public SkillEffect.SFX AuraSound;
    [HideInInspector]
    public SkillEffect.AuraStopTimings StopAura;
    [HeaderBar("弾")]
    public GameObject ProjectileEffect;
    [HideInInspector]
    public SkillEffect.SFX ProjectileSound;
    public HitReactionTypes RangedHitReactionType = HitReactionTypes.None;
    public GameObject[] ExplosionEffects;
    [HeaderBar("ヒット")]
    public SkillEffect.Effect HitEffects = new SkillEffect.Effect();
    [HideInInspector]
    public SkillEffect.SFX[] ExplosionSounds;
    public bool AlwaysExplode;
    public GameObject TargetHitEffect;
    public SkillEffect.Effect AreaEffects;
    public float CharacterHitDelay;
    public Color HitColor = Color.white;
    public float HitColorBlendTime;
    [HideInInspector]
    public AnimationClip ProjectileStart;
    [HideInInspector]
    public float ProjectileStartTime = 0.5f;
    [HideInInspector]
    public AnimationClip ProjectileEnd;
    [HideInInspector]
    public float ProjectileEndTime = 0.5f;
    [HeaderBar("弾道 (マップ)")]
    public SkillEffect.TrajectoryTypes MapTrajectoryType;
    public float MapProjectileSpeed = 1f;
    public float MapProjectileHitDelay = 0.5f;
    public float MapTrajectoryTimeScale = 1f;
    public SkillEffect.MapHitEffectTypes MapHitEffectType;
    public float MapHitEffectIntervals = 0.5f;
    [Space(10f)]
    [Tooltip("瞬間移動用として、ProjectileFrameを\n固定時間＆HitEffectなしで動作させるモード")]
    public bool IsTeleportMode;
    [Space(10f)]
    [Tooltip("レーザー系の際、ProjectileFrameの\nターゲット位置を指定")]
    public SkillEffect.eTargetTypeForLaser TargetTypeForLaser;
    [Tooltip("TargetTypeForLaser=StepFrontの際、\n前方のグリッド数を指定")]
    public int StepFrontTypeForLaser = 1;
    [HideInInspector]
    public AnimationCurve PointDistribution = new AnimationCurve(new Keyframe[2]
    {
      new Keyframe(0.0f, 0.5f),
      new Keyframe(1f, 0.5f)
    });
    [HideInInspector]
    public AnimationCurve PointRandomness = new AnimationCurve(new Keyframe[2]
    {
      new Keyframe(0.0f, 0.2f),
      new Keyframe(1f, 0.2f)
    });

    public void SpawnExplosionEffect(
      int index,
      Vector3 position,
      Quaternion rotation,
      float rotationY = 0.0f)
    {
      if (this.HitEffects != null && this.HitEffects.Effects != null && this.HitEffects.Effects.Length > 0)
      {
        DelayedEffectSpawner delayedEffectSpawner = new GameObject().RequireComponent<DelayedEffectSpawner>();
        if (this.HitEffects.SyncDirection)
          rotation = Quaternion.Euler(((Quaternion) ref rotation).eulerAngles.x, rotationY, ((Quaternion) ref rotation).eulerAngles.z);
        delayedEffectSpawner.Init(this.HitEffects.Effects, position, rotation);
      }
      else
      {
        GameObject arrayElementSafe = GameUtility.GetArrayElementSafe<GameObject>(this.ExplosionEffects, index);
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) arrayElementSafe, (UnityEngine.Object) null))
          return;
        GameUtility.SpawnParticle(arrayElementSafe, position, rotation, (GameObject) null);
      }
    }

    [Serializable]
    public class SFX
    {
      public string cueID;

      public bool IsCritical { get; set; }

      public void Play()
      {
        MonoSingleton<MySound>.Instance.PlaySEOneShot(this.cueID);
        if (!this.IsCritical)
          return;
        MonoSingleton<MySound>.Instance.PlaySEOneShot("SE_0506");
      }
    }

    [Serializable]
    public class Effect
    {
      [SerializeField]
      public bool SyncDirection;
      [SerializeField]
      public SkillEffect.EffectElement[] Effects;
    }

    [Serializable]
    public class EffectElement
    {
      [SerializeField]
      public GameObject Effect;
      [SerializeField]
      public string CueID = string.Empty;
      [SerializeField]
      public CustomSound.EType SoundType;
      [SerializeField]
      public float Delay;
    }

    public enum AuraStopTimings
    {
      AfterChant,
      BeforeHit,
      AfterHit,
    }

    public enum TrajectoryTypes
    {
      Straight,
      Arrow,
    }

    public enum MapHitEffectTypes
    {
      TargetRadial,
      EachTargets,
      EachGrids,
      Directional,
      EachHits,
      InstigatorRadial,
    }

    public enum eTargetTypeForLaser
    {
      Default,
      StepFront,
      FrontCenter,
    }
  }
}
