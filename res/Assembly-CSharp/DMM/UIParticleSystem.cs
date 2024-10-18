// Decompiled with JetBrains decompiler
// Type: UIParticleSystem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#nullable disable
public class UIParticleSystem : Graphic
{
  public UIParticleSystem.ParticleUpdateMode updateMode;
  public UIParticleSystem.EmitterTypes emitterType = UIParticleSystem.EmitterTypes.Sphere;
  public UIParticleSystem.ConeEmitter coneEmitter = new UIParticleSystem.ConeEmitter(-20f, 20f);
  public UIParticleSystem.SphereEmitter sphereEmitter = new UIParticleSystem.SphereEmitter(0.0f, 10f);
  public UIParticleSystem.BoxEmitter boxEmitter = new UIParticleSystem.BoxEmitter(64f, 64f);
  private List<UIParticleSystem.Particle> mParticles = new List<UIParticleSystem.Particle>();
  private List<UIParticleSystem.Particle> mDeadParticles = new List<UIParticleSystem.Particle>();
  private float mPrevTime;
  private float mTime;
  public UIParticleSystem.ParticleRenderMode RenderMode;
  public UIParticleSystem.StretchBillboard m_StretchBillboard = new UIParticleSystem.StretchBillboard(1f, 0.0f);
  [UIParticleSystem.Particle]
  public float duration = 1f;
  [UIParticleSystem.Particle]
  public float emissionRate = 10f;
  [UIParticleSystem.Particle]
  public float gravityMultipler;
  [UIParticleSystem.Particle]
  public bool loop = true;
  [UIParticleSystem.Particle]
  public int maxParticles = 100;
  [UIParticleSystem.Particle]
  public float playbackSpeed = 1f;
  [UIParticleSystem.Particle]
  public UIParticleSystem.ColorRange startColor = new UIParticleSystem.ColorRange(Color.white, Color.white);
  [UIParticleSystem.Particle]
  public float startDelay;
  [UIParticleSystem.Particle]
  public UIParticleSystem.FloatRange startLifetime = new UIParticleSystem.FloatRange(5f, 5f);
  [UIParticleSystem.Particle]
  public UIParticleSystem.FloatRange startRotation = new UIParticleSystem.FloatRange(0.0f, 0.0f);
  [UIParticleSystem.Particle]
  public UIParticleSystem.FloatRange startSize = new UIParticleSystem.FloatRange(1f, 1f);
  [UIParticleSystem.Particle]
  public UIParticleSystem.FloatRange startSpeed = new UIParticleSystem.FloatRange(5f, 5f);
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
  [UIParticleSystem.Particle]
  public UIParticleSystem.TextureSheetAnimation textureSheetAnimation = new UIParticleSystem.TextureSheetAnimation(1, 1);
  public bool textureSheetAnimationEnable;
  [UIParticleSystem.Particle]
  public UIParticleSystem.ParticleBurst burst = new UIParticleSystem.ParticleBurst(0);
  public bool burstEnable;
  [UIParticleSystem.Particle]
  public UIParticleSystem.VelocityOverLifetime velocityOverLifetime = new UIParticleSystem.VelocityOverLifetime(0);
  public bool velocityOverLifetimeEnable;
  [NonSerialized]
  public bool IsPlaying;
  [NonSerialized]
  public bool emit = true;
  private float mSpawnCount;

  public int particleCount => this.mParticles.Count;

  public float PlaybackTime
  {
    get => this.mTime;
    set => this.mTime = value;
  }

  public bool IsAlive()
  {
    return this.particleCount > 0 || (double) this.mTime < (double) this.duration || this.loop;
  }

  protected virtual void Start()
  {
    ((UIBehaviour) this).Start();
    this.ResetParticleSystem();
  }

  public virtual bool Raycast(Vector2 sp, Camera eventCamera) => false;

  public virtual Texture mainTexture
  {
    get
    {
      return Object.op_Implicit((Object) this.material) ? this.material.mainTexture : base.mainTexture;
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
    foreach (UIParticleSystem componentsInChild in ((Component) this).GetComponentsInChildren<UIParticleSystem>())
      componentsInChild.IsPlaying = true;
  }

  public void PauseEmitters()
  {
    foreach (UIParticleSystem componentsInChild in ((Component) this).GetComponentsInChildren<UIParticleSystem>())
      componentsInChild.IsPlaying = false;
  }

  public void ResetEmitters()
  {
    UIParticleSystem[] componentsInChildren = ((Component) this).GetComponentsInChildren<UIParticleSystem>();
    for (int index = 0; index < componentsInChildren.Length; ++index)
    {
      componentsInChildren[index].ResetParticleSystem();
      componentsInChildren[index].IsPlaying = false;
      componentsInChildren[index].UpdateGeometry();
    }
  }

  public void StopEmitters()
  {
    UIParticleSystem[] componentsInChildren = ((Component) this).GetComponentsInChildren<UIParticleSystem>();
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
        float num1 = (float) (1.0 - (double) mParticle.lifetime / (double) mParticle.startLifetime);
        UIParticleSystem.Particle particle1 = mParticle;
        particle1.position = Vector3.op_Addition(particle1.position, Vector3.op_Multiply(mParticle.velocity, dt));
        mParticle.rotation += mParticle.angularVelocity * dt;
        if ((double) this.gravityMultipler > 0.0)
        {
          UIParticleSystem.Particle particle2 = mParticle;
          particle2.velocity = Vector3.op_Addition(particle2.velocity, Vector3.op_Multiply(Physics.gravity, dt));
        }
        mParticle.visualVelocity = Vector2.op_Implicit(mParticle.velocity);
        if (this.velocityOverLifetimeEnable)
        {
          float num2 = this.velocityOverLifetime.X.Evaluate(num1) * this.velocityOverLifetime.ScaleX;
          float num3 = this.velocityOverLifetime.Y.Evaluate(num1) * this.velocityOverLifetime.ScaleY;
          mParticle.position.x += dt * num2;
          mParticle.position.y += dt * num3;
          mParticle.visualVelocity.x += num2;
          mParticle.visualVelocity.y += num3;
        }
        if (this.rotationOverLifetimeEnable)
          mParticle.rotation = mParticle.startRotation + this.rotationOverLifetime.Evaluate(num1);
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
        UIParticleSystem.Particle particle3;
        if (this.mDeadParticles.Count > 0)
        {
          particle3 = this.mDeadParticles[this.mDeadParticles.Count - 1];
          this.mDeadParticles.RemoveAt(this.mDeadParticles.Count - 1);
        }
        else
          particle3 = new UIParticleSystem.Particle();
        particle3.randomSeed = Random.Range(0, (int) ushort.MaxValue);
        particle3.startLifetime = particle3.lifetime = this.startLifetime.Evaluate();
        particle3.color = Color32.op_Implicit(this.startColor.Evaluate());
        particle3.rotation = particle3.startRotation = this.startRotation.Evaluate();
        particle3.size = this.startSize.Evaluate();
        particle3.angularVelocity = !this.angularVelocityEnable ? 0.0f : this.angularVelocity.Evaluate();
        switch (this.emitterType)
        {
          case UIParticleSystem.EmitterTypes.Cone:
            particle3.velocity = Quaternion.op_Multiply(Quaternion.AngleAxis(this.coneEmitter.Angle.Evaluate() + this.emitterRotation, Vector3.forward), Vector2.op_Implicit(Vector2.up));
            particle3.position = Vector3.op_Multiply(((Vector3) ref particle3.velocity).normalized, this.coneEmitter.Radius.Evaluate());
            if (this.coneEmitter.RandomDirection)
            {
              particle3.velocity = Quaternion.op_Multiply(Quaternion.AngleAxis(Random.value * 360f, Vector3.forward), Vector2.op_Implicit(Vector2.up));
              break;
            }
            break;
          case UIParticleSystem.EmitterTypes.Sphere:
            if (this.sphereEmitter.RandomDirection)
            {
              particle3.velocity = Quaternion.op_Multiply(Quaternion.AngleAxis(Random.value * 360f, Vector3.forward), Vector2.op_Implicit(Vector2.up));
              particle3.position = Vector3.op_Multiply(Quaternion.op_Multiply(Quaternion.AngleAxis(Random.value * 360f, Vector3.forward), Vector2.op_Implicit(Vector2.up)), this.sphereEmitter.Radius.Evaluate());
            }
            else
            {
              particle3.velocity = Quaternion.op_Multiply(Quaternion.AngleAxis(Random.value * 360f, Vector3.forward), Vector2.op_Implicit(Vector2.up));
              particle3.position = Vector3.op_Multiply(((Vector3) ref particle3.velocity).normalized, this.sphereEmitter.Radius.Evaluate());
            }
            if (this.sphereEmitter.Inverse)
            {
              particle3.velocity = Vector3.op_UnaryNegation(particle3.velocity);
              break;
            }
            break;
          case UIParticleSystem.EmitterTypes.Box:
            particle3.velocity = !this.boxEmitter.RandomDirection ? Quaternion.op_Multiply(Quaternion.AngleAxis(this.emitterRotation, Vector3.forward), Vector2.op_Implicit(Vector2.up)) : Quaternion.op_Multiply(Quaternion.AngleAxis(Random.value * 360f, Vector3.forward), Vector2.op_Implicit(Vector2.up));
            particle3.position = new Vector3((Random.value - 0.5f) * this.boxEmitter.Width, (Random.value - 0.5f) * this.boxEmitter.Height, 0.0f);
            break;
        }
        UIParticleSystem.Particle particle4 = particle3;
        particle4.velocity = Vector3.op_Multiply(particle4.velocity, this.startSpeed.Evaluate());
        this.mParticles.Add(particle3);
      }
      this.mSpawnCount -= (float) num;
    }
    this.SetVerticesDirty();
  }

  protected virtual void OnPopulateMesh(VertexHelper vh)
  {
    UIVertex uiVertex = new UIVertex();
    Vector3 vector3_1;
    // ISSUE: explicit constructor call
    ((Vector3) ref vector3_1).\u002Ector(-1f, 1f, 0.0f);
    Vector3 vector3_2;
    // ISSUE: explicit constructor call
    ((Vector3) ref vector3_2).\u002Ector(1f, 1f, 0.0f);
    Vector3 vector3_3;
    // ISSUE: explicit constructor call
    ((Vector3) ref vector3_3).\u002Ector(-1f, -1f, 0.0f);
    Vector3 vector3_4;
    // ISSUE: explicit constructor call
    ((Vector3) ref vector3_4).\u002Ector(1f, -1f, 0.0f);
    Vector2 vector2_1;
    // ISSUE: explicit constructor call
    ((Vector2) ref vector2_1).\u002Ector(0.0f, 1f);
    Vector2 vector2_2;
    // ISSUE: explicit constructor call
    ((Vector2) ref vector2_2).\u002Ector(1f, 1f);
    Vector2 vector2_3;
    // ISSUE: explicit constructor call
    ((Vector2) ref vector2_3).\u002Ector(0.0f, 0.0f);
    Vector2 vector2_4;
    // ISSUE: explicit constructor call
    ((Vector2) ref vector2_4).\u002Ector(1f, 0.0f);
    Vector2 vector2_5;
    // ISSUE: explicit constructor call
    ((Vector2) ref vector2_5).\u002Ector(0.0f, 0.0f);
    float num1 = 1f;
    float num2 = 1f;
    float num3 = 1f;
    int num4 = 1;
    int num5 = 0;
    vh.Clear();
    if (this.textureSheetAnimationEnable)
    {
      num1 = 1f / (float) this.textureSheetAnimation.TilesX;
      num2 = 1f / (float) this.textureSheetAnimation.TilesY;
      vector2_2.x = vector2_4.x = num1;
      vector2_3.y = vector2_4.y = 1f - num2;
      num3 = this.textureSheetAnimation.Animation != UIParticleSystem.TextureSheetAnimation.AnimationRow.WholeSheet ? (float) this.textureSheetAnimation.TilesX : (float) (this.textureSheetAnimation.TilesX * this.textureSheetAnimation.TilesY);
      num4 = Mathf.FloorToInt(num3);
    }
    if (this.RenderMode == UIParticleSystem.ParticleRenderMode.Billboard)
    {
      for (int index = this.mParticles.Count - 1; index >= 0; --index)
      {
        UIParticleSystem.Particle mParticle = this.mParticles[index];
        Quaternion quaternion = Quaternion.AngleAxis(mParticle.rotation, Vector3.forward);
        float size = mParticle.size;
        float num6 = (float) (1.0 - (double) mParticle.lifetime / (double) mParticle.startLifetime);
        Color32 color32 = mParticle.color;
        if (this.textureSheetAnimationEnable)
        {
          int num7 = Mathf.FloorToInt(this.textureSheetAnimation.FrameOverTime.Evaluate(num6) * num3) * this.textureSheetAnimation.Cycles % num4;
          vector2_5.x = (float) (num7 % this.textureSheetAnimation.TilesX) * num1;
          vector2_5.y = this.textureSheetAnimation.Animation != UIParticleSystem.TextureSheetAnimation.AnimationRow.WholeSheet ? (float) (1.0 - (double) (mParticle.randomSeed % this.textureSheetAnimation.TilesY) * (double) num2) : (float) (1.0 - (double) (num7 / this.textureSheetAnimation.TilesY) * (double) num2);
        }
        if (this.sizeOverLifetimeEnable)
          size *= this.sizeOverLifetime.Evaluate(num6);
        if (this.colorOverLifetimeEnable)
          color32 = Color32.op_Implicit(Color.op_Multiply(Color32.op_Implicit(color32), this.colorOverLifetime.Evaluate(num6)));
        uiVertex.position = Vector3.op_Addition(mParticle.position, Vector3.op_Multiply(Quaternion.op_Multiply(quaternion, vector3_1), size));
        uiVertex.color = color32;
        uiVertex.uv0 = Vector2.op_Addition(vector2_5, vector2_1);
        vh.AddVert(uiVertex);
        uiVertex.position = Vector3.op_Addition(mParticle.position, Vector3.op_Multiply(Quaternion.op_Multiply(quaternion, vector3_2), size));
        uiVertex.color = color32;
        uiVertex.uv0 = Vector2.op_Addition(vector2_5, vector2_2);
        vh.AddVert(uiVertex);
        uiVertex.position = Vector3.op_Addition(mParticle.position, Vector3.op_Multiply(Quaternion.op_Multiply(quaternion, vector3_4), size));
        uiVertex.color = color32;
        uiVertex.uv0 = Vector2.op_Addition(vector2_5, vector2_4);
        vh.AddVert(uiVertex);
        uiVertex.position = Vector3.op_Addition(mParticle.position, Vector3.op_Multiply(Quaternion.op_Multiply(quaternion, vector3_3), size));
        uiVertex.color = color32;
        uiVertex.uv0 = Vector2.op_Addition(vector2_5, vector2_3);
        vh.AddVert(uiVertex);
        vh.AddTriangle(num5, num5 + 1, num5 + 2);
        vh.AddTriangle(num5 + 2, num5 + 3, num5);
        num5 += 4;
      }
    }
    else
    {
      for (int index = this.mParticles.Count - 1; index >= 0; --index)
      {
        UIParticleSystem.Particle mParticle = this.mParticles[index];
        float size = mParticle.size;
        float num8 = (float) (1.0 - (double) mParticle.lifetime / (double) mParticle.startLifetime);
        Color32 color32 = mParticle.color;
        if (this.sizeOverLifetimeEnable)
          size *= this.sizeOverLifetime.Evaluate(num8);
        Vector2 vector2_6;
        Vector2 right;
        if ((double) ((Vector2) ref mParticle.visualVelocity).sqrMagnitude > 0.0)
        {
          float num9 = this.m_StretchBillboard.LengthScale * (float) (1.0 + (double) ((Vector3) ref mParticle.velocity).magnitude * (double) this.m_StretchBillboard.SpeedScale);
          vector2_6 = ((Vector2) ref mParticle.visualVelocity).normalized;
          // ISSUE: explicit constructor call
          ((Vector2) ref right).\u002Ector(vector2_6.y * size, -vector2_6.x * size);
          vector2_6 = Vector2.op_Multiply(vector2_6, size * 2f * num9);
        }
        else
        {
          vector2_6 = Vector2.up;
          right = Vector2.right;
        }
        if (this.textureSheetAnimationEnable)
        {
          int num10 = Mathf.FloorToInt(this.textureSheetAnimation.FrameOverTime.Evaluate(num8) * num3) * this.textureSheetAnimation.Cycles % num4;
          vector2_5.x = (float) (num10 % this.textureSheetAnimation.TilesX) * num1;
          vector2_5.y = this.textureSheetAnimation.Animation != UIParticleSystem.TextureSheetAnimation.AnimationRow.WholeSheet ? (float) (1.0 - (double) (mParticle.randomSeed % this.textureSheetAnimation.TilesY) * (double) num2) : (float) (1.0 - (double) (num10 / this.textureSheetAnimation.TilesY) * (double) num2);
        }
        if (this.colorOverLifetimeEnable)
          color32 = Color32.op_Implicit(Color.op_Multiply(Color32.op_Implicit(color32), this.colorOverLifetime.Evaluate(num8)));
        uiVertex.position.x = mParticle.position.x - right.x;
        uiVertex.position.y = mParticle.position.y - right.y;
        uiVertex.color = color32;
        uiVertex.uv0 = Vector2.op_Addition(vector2_5, vector2_1);
        vh.AddVert(uiVertex);
        uiVertex.position.x = mParticle.position.x + right.x;
        uiVertex.position.y = mParticle.position.y + right.y;
        uiVertex.color = color32;
        uiVertex.uv0 = Vector2.op_Addition(vector2_5, vector2_2);
        vh.AddVert(uiVertex);
        uiVertex.position.x = mParticle.position.x + right.x - vector2_6.x;
        uiVertex.position.y = mParticle.position.y + right.y - vector2_6.y;
        uiVertex.color = color32;
        uiVertex.uv0 = Vector2.op_Addition(vector2_5, vector2_4);
        vh.AddVert(uiVertex);
        uiVertex.position.x = mParticle.position.x - right.x - vector2_6.x;
        uiVertex.position.y = mParticle.position.y - right.y - vector2_6.y;
        uiVertex.color = color32;
        uiVertex.uv0 = Vector2.op_Addition(vector2_5, vector2_3);
        vh.AddVert(uiVertex);
        vh.AddTriangle(num5, num5 + 1, num5 + 2);
        vh.AddTriangle(num5 + 2, num5 + 3, num5);
        num5 += 4;
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

    public float Evaluate() => Mathf.Lerp(this.Min, this.Max, Random.value);
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

    public Color Evaluate() => Color.Lerp(this.Min, this.Max, Random.value);
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

    public ParticleBurst(int n) => this.Points = new UIParticleSystem.ParticleBurstPoint[n];
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
