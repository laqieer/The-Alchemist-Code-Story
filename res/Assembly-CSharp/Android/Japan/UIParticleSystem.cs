// Decompiled with JetBrains decompiler
// Type: UIParticleSystem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIParticleSystem : Graphic
{
  public UIParticleSystem.EmitterTypes emitterType = UIParticleSystem.EmitterTypes.Sphere;
  public UIParticleSystem.ConeEmitter coneEmitter = new UIParticleSystem.ConeEmitter(-20f, 20f);
  public UIParticleSystem.SphereEmitter sphereEmitter = new UIParticleSystem.SphereEmitter(0.0f, 10f);
  public UIParticleSystem.BoxEmitter boxEmitter = new UIParticleSystem.BoxEmitter(64f, 64f);
  private List<UIParticleSystem.Particle> mParticles = new List<UIParticleSystem.Particle>();
  private List<UIParticleSystem.Particle> mDeadParticles = new List<UIParticleSystem.Particle>();
  public UIParticleSystem.StretchBillboard m_StretchBillboard = new UIParticleSystem.StretchBillboard(1f, 0.0f);
  [UIParticleSystem.Particle]
  public float duration = 1f;
  [UIParticleSystem.Particle]
  public float emissionRate = 10f;
  [UIParticleSystem.Particle]
  public bool loop = true;
  [UIParticleSystem.Particle]
  public int maxParticles = 100;
  [UIParticleSystem.Particle]
  public float playbackSpeed = 1f;
  [UIParticleSystem.Particle]
  public UIParticleSystem.ColorRange startColor = new UIParticleSystem.ColorRange(Color.white, Color.white);
  [UIParticleSystem.Particle]
  public UIParticleSystem.FloatRange startLifetime = new UIParticleSystem.FloatRange(5f, 5f);
  [UIParticleSystem.Particle]
  public UIParticleSystem.FloatRange startRotation = new UIParticleSystem.FloatRange(0.0f, 0.0f);
  [UIParticleSystem.Particle]
  public UIParticleSystem.FloatRange startSize = new UIParticleSystem.FloatRange(1f, 1f);
  [UIParticleSystem.Particle]
  public UIParticleSystem.FloatRange startSpeed = new UIParticleSystem.FloatRange(5f, 5f);
  [UIParticleSystem.Particle]
  public UIParticleSystem.TextureSheetAnimation textureSheetAnimation = new UIParticleSystem.TextureSheetAnimation(1, 1);
  [UIParticleSystem.Particle]
  public UIParticleSystem.ParticleBurst burst = new UIParticleSystem.ParticleBurst(0);
  [UIParticleSystem.Particle]
  public UIParticleSystem.VelocityOverLifetime velocityOverLifetime = new UIParticleSystem.VelocityOverLifetime(0);
  [NonSerialized]
  public bool emit = true;
  public UIParticleSystem.ParticleUpdateMode updateMode;
  private float mPrevTime;
  private float mTime;
  public UIParticleSystem.ParticleRenderMode RenderMode;
  [UIParticleSystem.Particle]
  public float gravityMultipler;
  [UIParticleSystem.Particle]
  public float startDelay;
  [UIParticleSystem.Particle]
  public float emitterRotation;
  [UIParticleSystem.Particle]
  public UIParticleSystem.FloatRange angularVelocity;
  public bool angularVelocityEnable;
  [UIParticleSystem.Particle]
  public AnimationCurve rotationOverLifetime;
  public bool rotationOverLifetimeEnable;
  [UIParticleSystem.Particle]
  public Gradient colorOverLifetime;
  public bool colorOverLifetimeEnable;
  [UIParticleSystem.Particle]
  public AnimationCurve sizeOverLifetime;
  public bool sizeOverLifetimeEnable;
  public bool textureSheetAnimationEnable;
  public bool burstEnable;
  public bool velocityOverLifetimeEnable;
  [NonSerialized]
  public bool IsPlaying;
  private float mSpawnCount;

  public int particleCount
  {
    get
    {
      return this.mParticles.Count;
    }
  }

  public float PlaybackTime
  {
    get
    {
      return this.mTime;
    }
    set
    {
      this.mTime = value;
    }
  }

  public bool IsAlive()
  {
    if (this.particleCount <= 0 && (double) this.mTime >= (double) this.duration)
      return this.loop;
    return true;
  }

  protected override void Start()
  {
    base.Start();
    this.ResetParticleSystem();
  }

  public override bool Raycast(Vector2 sp, Camera eventCamera)
  {
    return false;
  }

  public override Texture mainTexture
  {
    get
    {
      if ((bool) ((Object) this.material))
        return this.material.mainTexture;
      return base.mainTexture;
    }
  }

  public void ResetParticleSystem()
  {
    this.IsPlaying = true;
    this.emit = true;
    this.mTime = -this.startDelay;
    this.mParticles.Clear();
  }

  public void ResumeEmitters()
  {
    foreach (UIParticleSystem componentsInChild in this.GetComponentsInChildren<UIParticleSystem>())
      componentsInChild.IsPlaying = true;
  }

  public void PauseEmitters()
  {
    foreach (UIParticleSystem componentsInChild in this.GetComponentsInChildren<UIParticleSystem>())
      componentsInChild.IsPlaying = false;
  }

  public void ResetEmitters()
  {
    UIParticleSystem[] componentsInChildren = this.GetComponentsInChildren<UIParticleSystem>();
    for (int index = 0; index < componentsInChildren.Length; ++index)
    {
      componentsInChildren[index].ResetParticleSystem();
      componentsInChildren[index].IsPlaying = false;
      componentsInChildren[index].UpdateGeometry();
    }
  }

  public void StopEmitters()
  {
    UIParticleSystem[] componentsInChildren = this.GetComponentsInChildren<UIParticleSystem>();
    for (int index = 0; index < componentsInChildren.Length; ++index)
    {
      componentsInChildren[index].emit = false;
      componentsInChildren[index].loop = false;
    }
  }

  private void Update()
  {
    if (!this.IsPlaying || !Application.isPlaying)
      return;
    switch (this.updateMode)
    {
      case UIParticleSystem.ParticleUpdateMode.UnscaledTime:
        this.AdvanceTime(Time.unscaledDeltaTime);
        break;
      case UIParticleSystem.ParticleUpdateMode.GameTime:
        this.AdvanceTime(Time.deltaTime);
        break;
      case UIParticleSystem.ParticleUpdateMode.FixedTime:
        this.AdvanceTime(Time.fixedDeltaTime);
        break;
    }
  }

  public void AdvanceTime(float dt)
  {
    dt *= this.playbackSpeed;
    this.mPrevTime = this.mTime;
    this.mTime += dt;
    if ((double) this.mTime >= (double) this.duration)
    {
      if (this.loop)
      {
        this.mTime = 0.0f;
        this.mPrevTime = 0.0f;
      }
      else
        this.mTime = this.duration;
    }
    for (int index = this.mParticles.Count - 1; index >= 0; --index)
    {
      UIParticleSystem.Particle mParticle = this.mParticles[index];
      mParticle.lifetime -= dt;
      if ((double) mParticle.lifetime <= 0.0)
      {
        this.mParticles.RemoveAt(index);
        this.mDeadParticles.Add(mParticle);
      }
      else
      {
        float time = (float) (1.0 - (double) mParticle.lifetime / (double) mParticle.startLifetime);
        mParticle.position += mParticle.velocity * dt;
        mParticle.rotation += mParticle.angularVelocity * dt;
        if ((double) this.gravityMultipler > 0.0)
          mParticle.velocity += Physics.gravity * dt;
        mParticle.visualVelocity = (Vector2) mParticle.velocity;
        if (this.velocityOverLifetimeEnable)
        {
          float num1 = this.velocityOverLifetime.X.Evaluate(time) * this.velocityOverLifetime.ScaleX;
          float num2 = this.velocityOverLifetime.Y.Evaluate(time) * this.velocityOverLifetime.ScaleY;
          mParticle.position.x += dt * num1;
          mParticle.position.y += dt * num2;
          mParticle.visualVelocity.x += num1;
          mParticle.visualVelocity.y += num2;
        }
        if (this.rotationOverLifetimeEnable)
          mParticle.rotation = mParticle.startRotation + this.rotationOverLifetime.Evaluate(time);
      }
    }
    if (!this.emit)
      return;
    if (this.burstEnable)
    {
      for (int index = 0; index < this.burst.Points.Length; ++index)
      {
        if ((double) this.mPrevTime <= (double) this.burst.Points[index].Time && (double) this.burst.Points[index].Time < (double) this.mTime)
          this.mSpawnCount += (float) this.burst.Points[index].Count;
      }
    }
    if (0.0 <= (double) this.mTime && (double) this.mTime < (double) this.duration)
      this.mSpawnCount += this.emissionRate * dt;
    if ((double) this.mSpawnCount >= 1.0)
    {
      int num = Mathf.FloorToInt(this.mSpawnCount);
      for (int index = 0; index < num && this.mParticles.Count < this.maxParticles; ++index)
      {
        UIParticleSystem.Particle particle;
        if (this.mDeadParticles.Count > 0)
        {
          particle = this.mDeadParticles[this.mDeadParticles.Count - 1];
          this.mDeadParticles.RemoveAt(this.mDeadParticles.Count - 1);
        }
        else
          particle = new UIParticleSystem.Particle();
        particle.randomSeed = Random.Range(0, (int) ushort.MaxValue);
        particle.startLifetime = particle.lifetime = this.startLifetime.Evaluate();
        particle.color = (Color32) this.startColor.Evaluate();
        particle.rotation = particle.startRotation = this.startRotation.Evaluate();
        particle.size = this.startSize.Evaluate();
        particle.angularVelocity = !this.angularVelocityEnable ? 0.0f : this.angularVelocity.Evaluate();
        switch (this.emitterType)
        {
          case UIParticleSystem.EmitterTypes.Cone:
            particle.velocity = Quaternion.AngleAxis(this.coneEmitter.Angle.Evaluate() + this.emitterRotation, Vector3.forward) * (Vector3) Vector2.up;
            particle.position = particle.velocity.normalized * this.coneEmitter.Radius.Evaluate();
            if (this.coneEmitter.RandomDirection)
            {
              particle.velocity = Quaternion.AngleAxis(Random.value * 360f, Vector3.forward) * (Vector3) Vector2.up;
              break;
            }
            break;
          case UIParticleSystem.EmitterTypes.Sphere:
            if (this.sphereEmitter.RandomDirection)
            {
              particle.velocity = Quaternion.AngleAxis(Random.value * 360f, Vector3.forward) * (Vector3) Vector2.up;
              particle.position = Quaternion.AngleAxis(Random.value * 360f, Vector3.forward) * (Vector3) Vector2.up * this.sphereEmitter.Radius.Evaluate();
            }
            else
            {
              particle.velocity = Quaternion.AngleAxis(Random.value * 360f, Vector3.forward) * (Vector3) Vector2.up;
              particle.position = particle.velocity.normalized * this.sphereEmitter.Radius.Evaluate();
            }
            if (this.sphereEmitter.Inverse)
            {
              particle.velocity = -particle.velocity;
              break;
            }
            break;
          case UIParticleSystem.EmitterTypes.Box:
            particle.velocity = !this.boxEmitter.RandomDirection ? Quaternion.AngleAxis(this.emitterRotation, Vector3.forward) * (Vector3) Vector2.up : Quaternion.AngleAxis(Random.value * 360f, Vector3.forward) * (Vector3) Vector2.up;
            particle.position = new Vector3((Random.value - 0.5f) * this.boxEmitter.Width, (Random.value - 0.5f) * this.boxEmitter.Height, 0.0f);
            break;
        }
        particle.velocity *= this.startSpeed.Evaluate();
        this.mParticles.Add(particle);
      }
      this.mSpawnCount -= (float) num;
    }
    this.SetVerticesDirty();
  }

  protected override void OnPopulateMesh(VertexHelper vh)
  {
    UIVertex v = new UIVertex();
    Vector3 vector3_1 = new Vector3(-1f, 1f, 0.0f);
    Vector3 vector3_2 = new Vector3(1f, 1f, 0.0f);
    Vector3 vector3_3 = new Vector3(-1f, -1f, 0.0f);
    Vector3 vector3_4 = new Vector3(1f, -1f, 0.0f);
    Vector2 vector2_1 = new Vector2(0.0f, 1f);
    Vector2 vector2_2 = new Vector2(1f, 1f);
    Vector2 vector2_3 = new Vector2(0.0f, 0.0f);
    Vector2 vector2_4 = new Vector2(1f, 0.0f);
    Vector2 vector2_5 = new Vector2(0.0f, 0.0f);
    float num1 = 1f;
    float num2 = 1f;
    float f = 1f;
    int num3 = 1;
    int num4 = 0;
    vh.Clear();
    if (this.textureSheetAnimationEnable)
    {
      num1 = 1f / (float) this.textureSheetAnimation.TilesX;
      num2 = 1f / (float) this.textureSheetAnimation.TilesY;
      vector2_2.x = vector2_4.x = num1;
      vector2_3.y = vector2_4.y = 1f - num2;
      f = this.textureSheetAnimation.Animation != UIParticleSystem.TextureSheetAnimation.AnimationRow.WholeSheet ? (float) this.textureSheetAnimation.TilesX : (float) (this.textureSheetAnimation.TilesX * this.textureSheetAnimation.TilesY);
      num3 = Mathf.FloorToInt(f);
    }
    if (this.RenderMode == UIParticleSystem.ParticleRenderMode.Billboard)
    {
      for (int index = this.mParticles.Count - 1; index >= 0; --index)
      {
        UIParticleSystem.Particle mParticle = this.mParticles[index];
        Quaternion quaternion = Quaternion.AngleAxis(mParticle.rotation, Vector3.forward);
        float size = mParticle.size;
        float time = (float) (1.0 - (double) mParticle.lifetime / (double) mParticle.startLifetime);
        Color32 color32 = mParticle.color;
        if (this.textureSheetAnimationEnable)
        {
          int num5 = Mathf.FloorToInt(this.textureSheetAnimation.FrameOverTime.Evaluate(time) * f) * this.textureSheetAnimation.Cycles % num3;
          vector2_5.x = (float) (num5 % this.textureSheetAnimation.TilesX) * num1;
          vector2_5.y = this.textureSheetAnimation.Animation != UIParticleSystem.TextureSheetAnimation.AnimationRow.WholeSheet ? (float) (1.0 - (double) (mParticle.randomSeed % this.textureSheetAnimation.TilesY) * (double) num2) : (float) (1.0 - (double) (num5 / this.textureSheetAnimation.TilesY) * (double) num2);
        }
        if (this.sizeOverLifetimeEnable)
          size *= this.sizeOverLifetime.Evaluate(time);
        if (this.colorOverLifetimeEnable)
          color32 = (Color32) ((Color) color32 * this.colorOverLifetime.Evaluate(time));
        v.position = mParticle.position + quaternion * vector3_1 * size;
        v.color = color32;
        v.uv0 = vector2_5 + vector2_1;
        vh.AddVert(v);
        v.position = mParticle.position + quaternion * vector3_2 * size;
        v.color = color32;
        v.uv0 = vector2_5 + vector2_2;
        vh.AddVert(v);
        v.position = mParticle.position + quaternion * vector3_4 * size;
        v.color = color32;
        v.uv0 = vector2_5 + vector2_4;
        vh.AddVert(v);
        v.position = mParticle.position + quaternion * vector3_3 * size;
        v.color = color32;
        v.uv0 = vector2_5 + vector2_3;
        vh.AddVert(v);
        vh.AddTriangle(num4, num4 + 1, num4 + 2);
        vh.AddTriangle(num4 + 2, num4 + 3, num4);
        num4 += 4;
      }
    }
    else
    {
      for (int index = this.mParticles.Count - 1; index >= 0; --index)
      {
        UIParticleSystem.Particle mParticle = this.mParticles[index];
        float size = mParticle.size;
        float time = (float) (1.0 - (double) mParticle.lifetime / (double) mParticle.startLifetime);
        Color32 color32 = mParticle.color;
        if (this.sizeOverLifetimeEnable)
          size *= this.sizeOverLifetime.Evaluate(time);
        Vector2 vector2_6;
        Vector2 vector2_7;
        if ((double) mParticle.visualVelocity.sqrMagnitude > 0.0)
        {
          float num5 = this.m_StretchBillboard.LengthScale * (float) (1.0 + (double) mParticle.velocity.magnitude * (double) this.m_StretchBillboard.SpeedScale);
          vector2_6 = mParticle.visualVelocity.normalized;
          vector2_7 = new Vector2(vector2_6.y * size, -vector2_6.x * size);
          vector2_6 *= size * 2f * num5;
        }
        else
        {
          vector2_6 = Vector2.up;
          vector2_7 = Vector2.right;
        }
        if (this.textureSheetAnimationEnable)
        {
          int num5 = Mathf.FloorToInt(this.textureSheetAnimation.FrameOverTime.Evaluate(time) * f) * this.textureSheetAnimation.Cycles % num3;
          vector2_5.x = (float) (num5 % this.textureSheetAnimation.TilesX) * num1;
          vector2_5.y = this.textureSheetAnimation.Animation != UIParticleSystem.TextureSheetAnimation.AnimationRow.WholeSheet ? (float) (1.0 - (double) (mParticle.randomSeed % this.textureSheetAnimation.TilesY) * (double) num2) : (float) (1.0 - (double) (num5 / this.textureSheetAnimation.TilesY) * (double) num2);
        }
        if (this.colorOverLifetimeEnable)
          color32 = (Color32) ((Color) color32 * this.colorOverLifetime.Evaluate(time));
        v.position.x = mParticle.position.x - vector2_7.x;
        v.position.y = mParticle.position.y - vector2_7.y;
        v.color = color32;
        v.uv0 = vector2_5 + vector2_1;
        vh.AddVert(v);
        v.position.x = mParticle.position.x + vector2_7.x;
        v.position.y = mParticle.position.y + vector2_7.y;
        v.color = color32;
        v.uv0 = vector2_5 + vector2_2;
        vh.AddVert(v);
        v.position.x = mParticle.position.x + vector2_7.x - vector2_6.x;
        v.position.y = mParticle.position.y + vector2_7.y - vector2_6.y;
        v.color = color32;
        v.uv0 = vector2_5 + vector2_4;
        vh.AddVert(v);
        v.position.x = mParticle.position.x - vector2_7.x - vector2_6.x;
        v.position.y = mParticle.position.y - vector2_7.y - vector2_6.y;
        v.color = color32;
        v.uv0 = vector2_5 + vector2_3;
        vh.AddVert(v);
        vh.AddTriangle(num4, num4 + 1, num4 + 2);
        vh.AddTriangle(num4 + 2, num4 + 3, num4);
        num4 += 4;
      }
    }
  }

  public enum ParticleUpdateMode
  {
    UnscaledTime,
    GameTime,
    FixedTime,
  }

  public enum EmitterTypes
  {
    Cone,
    Sphere,
    Box,
  }

  public enum ParticleRenderMode
  {
    Billboard,
    StretchBillboard,
  }

  [Serializable]
  public struct ConeEmitter
  {
    public UIParticleSystem.FloatRange Angle;
    public UIParticleSystem.FloatRange Radius;
    public bool RandomDirection;

    public ConeEmitter(float angleMin, float angleMax)
    {
      this.Angle = new UIParticleSystem.FloatRange(angleMin, angleMax);
      this.Radius = new UIParticleSystem.FloatRange(0.0f, 0.0f);
      this.RandomDirection = false;
    }
  }

  [Serializable]
  public struct SphereEmitter
  {
    public UIParticleSystem.FloatRange Radius;
    public bool Inverse;
    public bool RandomDirection;

    public SphereEmitter(float radiusMin, float radiusMax)
    {
      this.Radius = new UIParticleSystem.FloatRange(radiusMin, radiusMax);
      this.Inverse = false;
      this.RandomDirection = false;
    }
  }

  [Serializable]
  public struct BoxEmitter
  {
    public float Width;
    public float Height;
    public bool RandomDirection;

    public BoxEmitter(float w, float h)
    {
      this.Width = w;
      this.Height = h;
      this.RandomDirection = false;
    }
  }

  public class Particle
  {
    public float angularVelocity;
    public Vector3 axisOfRotation;
    public Color32 color;
    public float lifetime;
    public Vector3 position;
    public int randomSeed;
    public float rotation;
    public float size;
    public float startLifetime;
    public float startRotation;
    public Vector3 velocity;
    public Vector2 visualVelocity;
  }

  [Serializable]
  public struct FloatRange
  {
    public float Min;
    public float Max;

    public FloatRange(float min, float max)
    {
      this.Min = min;
      this.Max = max;
    }

    public float Evaluate()
    {
      return Mathf.Lerp(this.Min, this.Max, Random.value);
    }
  }

  [Serializable]
  public struct ColorRange
  {
    public Color Min;
    public Color Max;

    public ColorRange(Color min, Color max)
    {
      this.Min = min;
      this.Max = max;
    }

    public Color Evaluate()
    {
      return Color.Lerp(this.Min, this.Max, Random.value);
    }
  }

  [Serializable]
  public struct TextureSheetAnimation
  {
    public int TilesX;
    public int TilesY;
    public AnimationCurve FrameOverTime;
    public UIParticleSystem.TextureSheetAnimation.AnimationRow Animation;
    public int Cycles;

    public TextureSheetAnimation(int tx, int ty)
    {
      this.TilesX = tx;
      this.TilesY = ty;
      this.FrameOverTime = new AnimationCurve(new Keyframe[2]
      {
        new Keyframe(0.0f, 0.0f),
        new Keyframe(1f, 1f)
      });
      this.Animation = UIParticleSystem.TextureSheetAnimation.AnimationRow.WholeSheet;
      this.Cycles = 1;
    }

    public enum AnimationRow
    {
      WholeSheet,
      RandomRow,
    }
  }

  [Serializable]
  public struct ParticleBurstPoint
  {
    public float Time;
    public int Count;
  }

  [Serializable]
  public struct ParticleBurst
  {
    public UIParticleSystem.ParticleBurstPoint[] Points;

    public ParticleBurst(int n)
    {
      this.Points = new UIParticleSystem.ParticleBurstPoint[n];
    }
  }

  [Serializable]
  public struct VelocityOverLifetime
  {
    public AnimationCurve X;
    public AnimationCurve Y;
    public float ScaleX;
    public float ScaleY;

    public VelocityOverLifetime(int n)
    {
      this.X = new AnimationCurve(new Keyframe[2]
      {
        new Keyframe(0.0f, 0.0f),
        new Keyframe(1f, 0.0f)
      });
      this.Y = new AnimationCurve(new Keyframe[2]
      {
        new Keyframe(0.0f, 0.0f),
        new Keyframe(1f, 0.0f)
      });
      this.ScaleX = 1f;
      this.ScaleY = 1f;
    }
  }

  [Serializable]
  public struct StretchBillboard
  {
    public float LengthScale;
    public float SpeedScale;

    public StretchBillboard(float lengthScale, float speedScale)
    {
      this.LengthScale = lengthScale;
      this.SpeedScale = speedScale;
    }
  }

  public class ParticleAttribute : PropertyAttribute
  {
  }
}
