// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.ObscuredTypes.ObscuredVector3
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using CodeStage.AntiCheat.Detectors;
using CodeStage.AntiCheat.Utils;
using System;
using UnityEngine;

#nullable disable
namespace CodeStage.AntiCheat.ObscuredTypes
{
  [Serializable]
  public struct ObscuredVector3
  {
    private static int cryptoKey = 120207;
    private static readonly Vector3 zero = Vector3.zero;
    [SerializeField]
    private int currentCryptoKey;
    [SerializeField]
    private ObscuredVector3.RawEncryptedVector3 hiddenValue;
    [SerializeField]
    private bool inited;
    [SerializeField]
    private Vector3 fakeValue;
    [SerializeField]
    private bool fakeValueActive;

    private ObscuredVector3(Vector3 value)
    {
      this.currentCryptoKey = ObscuredVector3.cryptoKey;
      this.hiddenValue = ObscuredVector3.Encrypt(value);
      bool existsAndIsRunning = ObscuredCheatingDetector.ExistsAndIsRunning;
      this.fakeValue = !existsAndIsRunning ? ObscuredVector3.zero : value;
      this.fakeValueActive = existsAndIsRunning;
      this.inited = true;
    }

    public ObscuredVector3(float x, float y, float z)
    {
      this.currentCryptoKey = ObscuredVector3.cryptoKey;
      this.hiddenValue = ObscuredVector3.Encrypt(x, y, z, this.currentCryptoKey);
      if (ObscuredCheatingDetector.ExistsAndIsRunning)
      {
        this.fakeValue = new Vector3(x, y, z);
        this.fakeValueActive = true;
      }
      else
      {
        this.fakeValue = ObscuredVector3.zero;
        this.fakeValueActive = false;
      }
      this.inited = true;
    }

    public float x
    {
      get
      {
        float x = this.InternalDecryptField(this.hiddenValue.x);
        if (ObscuredCheatingDetector.ExistsAndIsRunning && this.fakeValueActive && (double) Math.Abs(x - this.fakeValue.x) > (double) ObscuredCheatingDetector.Instance.vector3Epsilon)
          ObscuredCheatingDetector.Instance.OnCheatingDetected();
        return x;
      }
      set
      {
        this.hiddenValue.x = this.InternalEncryptField(value);
        if (ObscuredCheatingDetector.ExistsAndIsRunning)
        {
          this.fakeValue.x = value;
          this.fakeValue.y = this.InternalDecryptField(this.hiddenValue.y);
          this.fakeValue.z = this.InternalDecryptField(this.hiddenValue.z);
          this.fakeValueActive = true;
        }
        else
          this.fakeValueActive = false;
      }
    }

    public float y
    {
      get
      {
        float y = this.InternalDecryptField(this.hiddenValue.y);
        if (ObscuredCheatingDetector.ExistsAndIsRunning && this.fakeValueActive && (double) Math.Abs(y - this.fakeValue.y) > (double) ObscuredCheatingDetector.Instance.vector3Epsilon)
          ObscuredCheatingDetector.Instance.OnCheatingDetected();
        return y;
      }
      set
      {
        this.hiddenValue.y = this.InternalEncryptField(value);
        if (ObscuredCheatingDetector.ExistsAndIsRunning)
        {
          this.fakeValue.x = this.InternalDecryptField(this.hiddenValue.x);
          this.fakeValue.y = value;
          this.fakeValue.z = this.InternalDecryptField(this.hiddenValue.z);
          this.fakeValueActive = true;
        }
        else
          this.fakeValueActive = false;
      }
    }

    public float z
    {
      get
      {
        float z = this.InternalDecryptField(this.hiddenValue.z);
        if (ObscuredCheatingDetector.ExistsAndIsRunning && this.fakeValueActive && (double) Math.Abs(z - this.fakeValue.z) > (double) ObscuredCheatingDetector.Instance.vector3Epsilon)
          ObscuredCheatingDetector.Instance.OnCheatingDetected();
        return z;
      }
      set
      {
        this.hiddenValue.z = this.InternalEncryptField(value);
        if (ObscuredCheatingDetector.ExistsAndIsRunning)
        {
          this.fakeValue.x = this.InternalDecryptField(this.hiddenValue.x);
          this.fakeValue.y = this.InternalDecryptField(this.hiddenValue.y);
          this.fakeValue.z = value;
          this.fakeValueActive = true;
        }
        else
          this.fakeValueActive = false;
      }
    }

    public float this[int index]
    {
      get
      {
        switch (index)
        {
          case 0:
            return this.x;
          case 1:
            return this.y;
          case 2:
            return this.z;
          default:
            throw new IndexOutOfRangeException("Invalid ObscuredVector3 index!");
        }
      }
      set
      {
        switch (index)
        {
          case 0:
            this.x = value;
            break;
          case 1:
            this.y = value;
            break;
          case 2:
            this.z = value;
            break;
          default:
            throw new IndexOutOfRangeException("Invalid ObscuredVector3 index!");
        }
      }
    }

    public static void SetNewCryptoKey(int newKey) => ObscuredVector3.cryptoKey = newKey;

    public static ObscuredVector3.RawEncryptedVector3 Encrypt(Vector3 value)
    {
      return ObscuredVector3.Encrypt(value, 0);
    }

    public static ObscuredVector3.RawEncryptedVector3 Encrypt(Vector3 value, int key)
    {
      return ObscuredVector3.Encrypt(value.x, value.y, value.z, key);
    }

    public static ObscuredVector3.RawEncryptedVector3 Encrypt(float x, float y, float z, int key)
    {
      if (key == 0)
        key = ObscuredVector3.cryptoKey;
      ObscuredVector3.RawEncryptedVector3 encryptedVector3;
      encryptedVector3.x = ObscuredFloat.Encrypt(x, key);
      encryptedVector3.y = ObscuredFloat.Encrypt(y, key);
      encryptedVector3.z = ObscuredFloat.Encrypt(z, key);
      return encryptedVector3;
    }

    public static Vector3 Decrypt(ObscuredVector3.RawEncryptedVector3 value)
    {
      return ObscuredVector3.Decrypt(value, 0);
    }

    public static Vector3 Decrypt(ObscuredVector3.RawEncryptedVector3 value, int key)
    {
      if (key == 0)
        key = ObscuredVector3.cryptoKey;
      Vector3 vector3;
      vector3.x = ObscuredFloat.Decrypt(value.x, key);
      vector3.y = ObscuredFloat.Decrypt(value.y, key);
      vector3.z = ObscuredFloat.Decrypt(value.z, key);
      return vector3;
    }

    public static ObscuredVector3 FromEncrypted(ObscuredVector3.RawEncryptedVector3 encrypted)
    {
      ObscuredVector3 obscuredVector3 = new ObscuredVector3();
      obscuredVector3.SetEncrypted(encrypted);
      return obscuredVector3;
    }

    public void ApplyNewCryptoKey()
    {
      if (this.currentCryptoKey == ObscuredVector3.cryptoKey)
        return;
      this.hiddenValue = ObscuredVector3.Encrypt(this.InternalDecrypt(), ObscuredVector3.cryptoKey);
      this.currentCryptoKey = ObscuredVector3.cryptoKey;
    }

    public void RandomizeCryptoKey()
    {
      Vector3 vector3 = this.InternalDecrypt();
      this.currentCryptoKey = ThreadSafeRandom.Next();
      this.hiddenValue = ObscuredVector3.Encrypt(vector3, this.currentCryptoKey);
    }

    public ObscuredVector3.RawEncryptedVector3 GetEncrypted()
    {
      this.ApplyNewCryptoKey();
      return this.hiddenValue;
    }

    public void SetEncrypted(ObscuredVector3.RawEncryptedVector3 encrypted)
    {
      this.inited = true;
      this.hiddenValue = encrypted;
      if (this.currentCryptoKey == 0)
        this.currentCryptoKey = ObscuredVector3.cryptoKey;
      if (ObscuredCheatingDetector.ExistsAndIsRunning)
      {
        this.fakeValueActive = false;
        this.fakeValue = this.InternalDecrypt();
        this.fakeValueActive = true;
      }
      else
        this.fakeValueActive = false;
    }

    public Vector3 GetDecrypted() => this.InternalDecrypt();

    private Vector3 InternalDecrypt()
    {
      if (!this.inited)
      {
        this.currentCryptoKey = ObscuredVector3.cryptoKey;
        this.hiddenValue = ObscuredVector3.Encrypt(ObscuredVector3.zero, ObscuredVector3.cryptoKey);
        this.fakeValue = ObscuredVector3.zero;
        this.fakeValueActive = false;
        this.inited = true;
        return ObscuredVector3.zero;
      }
      Vector3 vector1;
      vector1.x = ObscuredFloat.Decrypt(this.hiddenValue.x, this.currentCryptoKey);
      vector1.y = ObscuredFloat.Decrypt(this.hiddenValue.y, this.currentCryptoKey);
      vector1.z = ObscuredFloat.Decrypt(this.hiddenValue.z, this.currentCryptoKey);
      if (ObscuredCheatingDetector.ExistsAndIsRunning && this.fakeValueActive && !this.CompareVectorsWithTolerance(vector1, this.fakeValue))
        ObscuredCheatingDetector.Instance.OnCheatingDetected();
      return vector1;
    }

    private bool CompareVectorsWithTolerance(Vector3 vector1, Vector3 vector2)
    {
      float vector3Epsilon = ObscuredCheatingDetector.Instance.vector3Epsilon;
      return (double) Math.Abs(vector1.x - vector2.x) < (double) vector3Epsilon && (double) Math.Abs(vector1.y - vector2.y) < (double) vector3Epsilon && (double) Math.Abs(vector1.z - vector2.z) < (double) vector3Epsilon;
    }

    private float InternalDecryptField(int encrypted)
    {
      int key = ObscuredVector3.cryptoKey;
      if (this.currentCryptoKey != ObscuredVector3.cryptoKey)
        key = this.currentCryptoKey;
      return ObscuredFloat.Decrypt(encrypted, key);
    }

    private int InternalEncryptField(float encrypted)
    {
      return ObscuredFloat.Encrypt(encrypted, ObscuredVector3.cryptoKey);
    }

    public static implicit operator ObscuredVector3(Vector3 value) => new ObscuredVector3(value);

    public static implicit operator Vector3(ObscuredVector3 value) => value.InternalDecrypt();

    public static ObscuredVector3 operator +(ObscuredVector3 a, ObscuredVector3 b)
    {
      return (ObscuredVector3) Vector3.op_Addition(a.InternalDecrypt(), b.InternalDecrypt());
    }

    public static ObscuredVector3 operator +(Vector3 a, ObscuredVector3 b)
    {
      return (ObscuredVector3) Vector3.op_Addition(a, b.InternalDecrypt());
    }

    public static ObscuredVector3 operator +(ObscuredVector3 a, Vector3 b)
    {
      return (ObscuredVector3) Vector3.op_Addition(a.InternalDecrypt(), b);
    }

    public static ObscuredVector3 operator -(ObscuredVector3 a, ObscuredVector3 b)
    {
      return (ObscuredVector3) Vector3.op_Subtraction(a.InternalDecrypt(), b.InternalDecrypt());
    }

    public static ObscuredVector3 operator -(Vector3 a, ObscuredVector3 b)
    {
      return (ObscuredVector3) Vector3.op_Subtraction(a, b.InternalDecrypt());
    }

    public static ObscuredVector3 operator -(ObscuredVector3 a, Vector3 b)
    {
      return (ObscuredVector3) Vector3.op_Subtraction(a.InternalDecrypt(), b);
    }

    public static ObscuredVector3 operator -(ObscuredVector3 a)
    {
      return (ObscuredVector3) Vector3.op_UnaryNegation(a.InternalDecrypt());
    }

    public static ObscuredVector3 operator *(ObscuredVector3 a, float d)
    {
      return (ObscuredVector3) Vector3.op_Multiply(a.InternalDecrypt(), d);
    }

    public static ObscuredVector3 operator *(float d, ObscuredVector3 a)
    {
      return (ObscuredVector3) Vector3.op_Multiply(d, a.InternalDecrypt());
    }

    public static ObscuredVector3 operator /(ObscuredVector3 a, float d)
    {
      return (ObscuredVector3) Vector3.op_Division(a.InternalDecrypt(), d);
    }

    public static bool operator ==(ObscuredVector3 lhs, ObscuredVector3 rhs)
    {
      return Vector3.op_Equality(lhs.InternalDecrypt(), rhs.InternalDecrypt());
    }

    public static bool operator ==(Vector3 lhs, ObscuredVector3 rhs)
    {
      return Vector3.op_Equality(lhs, rhs.InternalDecrypt());
    }

    public static bool operator ==(ObscuredVector3 lhs, Vector3 rhs)
    {
      return Vector3.op_Equality(lhs.InternalDecrypt(), rhs);
    }

    public static bool operator !=(ObscuredVector3 lhs, ObscuredVector3 rhs)
    {
      return Vector3.op_Inequality(lhs.InternalDecrypt(), rhs.InternalDecrypt());
    }

    public static bool operator !=(Vector3 lhs, ObscuredVector3 rhs)
    {
      return Vector3.op_Inequality(lhs, rhs.InternalDecrypt());
    }

    public static bool operator !=(ObscuredVector3 lhs, Vector3 rhs)
    {
      return Vector3.op_Inequality(lhs.InternalDecrypt(), rhs);
    }

    public override bool Equals(object other) => this.InternalDecrypt().Equals(other);

    public override int GetHashCode() => this.InternalDecrypt().GetHashCode();

    public override string ToString() => this.InternalDecrypt().ToString();

    public string ToString(string format)
    {
      Vector3 vector3 = this.InternalDecrypt();
      return ((Vector3) ref vector3).ToString(format);
    }

    [Serializable]
    public struct RawEncryptedVector3
    {
      public int x;
      public int y;
      public int z;
    }
  }
}
