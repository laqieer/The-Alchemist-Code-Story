// Decompiled with JetBrains decompiler
// Type: SRPG.SkillEffect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;

namespace SRPG
{
  public class SkillEffect : ScriptableObject
  {
    public HitReactionTypes RangedHitReactionType = HitReactionTypes.None;
    public Color HitColor = Color.white;
    [HideInInspector]
    public float ProjectileStartTime = 0.5f;
    [HideInInspector]
    public float ProjectileEndTime = 0.5f;
    public float MapProjectileSpeed = 1f;
    public float MapProjectileHitDelay = 0.5f;
    public float MapTrajectoryTimeScale = 1f;
    public float MapHitEffectIntervals = 0.5f;
    [Tooltip("TargetTypeForLaser=StepFrontの際、\n前方のグリッド数を指定")]
    public int StepFrontTypeForLaser = 1;
    [HideInInspector]
    public AnimationCurve PointDistribution = new AnimationCurve(new Keyframe[2]{ new Keyframe(0.0f, 0.5f), new Keyframe(1f, 0.5f) });
    [HideInInspector]
    public AnimationCurve PointRandomness = new AnimationCurve(new Keyframe[2]{ new Keyframe(0.0f, 0.2f), new Keyframe(1f, 0.2f) });
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
    [HeaderBar("ヒット")]
    public GameObject[] ExplosionEffects;
    [HideInInspector]
    public SkillEffect.SFX[] ExplosionSounds;
    public bool AlwaysExplode;
    public GameObject TargetHitEffect;
    public float HitColorBlendTime;
    [HideInInspector]
    public AnimationClip ProjectileStart;
    [HideInInspector]
    public AnimationClip ProjectileEnd;
    [HeaderBar("弾道 (マップ)")]
    public SkillEffect.TrajectoryTypes MapTrajectoryType;
    public SkillEffect.MapHitEffectTypes MapHitEffectType;
    [Space(10f)]
    [Tooltip("瞬間移動用として、ProjectileFrameを\n固定時間＆HitEffectなしで動作させるモード")]
    public bool IsTeleportMode;
    [Space(10f)]
    [Tooltip("レーザー系の際、ProjectileFrameの\nターゲット位置を指定")]
    public SkillEffect.eTargetTypeForLaser TargetTypeForLaser;

    public void SpawnExplosionEffect(int index, Vector3 position, Quaternion rotation)
    {
      GameObject arrayElementSafe = GameUtility.GetArrayElementSafe<GameObject>(this.ExplosionEffects, index);
      if (!((UnityEngine.Object) arrayElementSafe != (UnityEngine.Object) null))
        return;
      GameUtility.SpawnParticle(arrayElementSafe, position, rotation, (GameObject) null);
    }

    [Serializable]
    public class SFX
    {
      public string cueID;

      public bool IsCritical { get; set; }

      public void Play()
      {
        MonoSingleton<MySound>.Instance.PlaySEOneShot(this.cueID, 0.0f);
        if (!this.IsCritical)
          return;
        MonoSingleton<MySound>.Instance.PlaySEOneShot("SE_0506", 0.0f);
      }
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
