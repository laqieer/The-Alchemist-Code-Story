// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.ObscuredTypes.ObscuredVector3Int
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
  public struct ObscuredVector3Int
  {
    private static int cryptoKey = 120207;
    private static readonly Vector3Int zero = Vector3Int.zero;
    [SerializeField]
    private int currentCryptoKey;
    [SerializeField]
    private ObscuredVector3Int.RawEncryptedVector3Int hiddenValue;
    [SerializeField]
    private bool inited;
    [SerializeField]
    private Vector3Int fakeValue;
    [SerializeField]
    private bool fakeValueActive;

    private ObscuredVector3Int(Vector3Int value)
    {
      this.currentCryptoKey = ObscuredVector3Int.cryptoKey;
      this.hiddenValue = ObscuredVector3Int.Encrypt(value);
      bool existsAndIsRunning = ObscuredCheatingDetector.ExistsAndIsRunning;
      this.fakeValue = !existsAndIsRunning ? ObscuredVector3Int.zero : value;
      this.fakeValueActive = existsAndIsRunning;
      this.inited = true;
    }

    public ObscuredVector3Int(int x, int y, int z)
    {
      this.currentCryptoKey = ObscuredVector3Int.cryptoKey;
      this.hiddenValue = ObscuredVector3Int.Encrypt(x, y, z, this.currentCryptoKey);
      if (ObscuredCheatingDetector.ExistsAndIsRunning)
      {
        Vector3Int vector3Int = new Vector3Int();
        ((Vector3Int) ref vector3Int).x = x;
        ((Vector3Int) ref vector3Int).y = y;
        ((Vector3Int) ref vector3Int).z = z;
        this.fakeValue = vector3Int;
        this.fakeValueActive = true;
      }
      else
      {
        this.fakeValue = ObscuredVector3Int.zero;
        this.fakeValueActive = false;
      }
      this.inited = true;
    }

    public int x
    {
      get
      {
        int x = this.InternalDecryptField(this.hiddenValue.x);
        if (ObscuredCheatingDetector.ExistsAndIsRunning && this.fakeValueActive && Math.Abs(x - ((Vector3Int) ref this.fakeValue).x) > 0)
          ObscuredCheatingDetector.Instance.OnCheatingDetected();
        return x;
      }
      set
      {
        this.hiddenValue.x = this.InternalEncryptField(value);
        if (ObscuredCheatingDetector.ExistsAndIsRunning)
        {
          ((Vector3Int) ref this.fakeValue).x = value;
          ((Vector3Int) ref this.fakeValue).y = this.InternalDecryptField(this.hiddenValue.y);
          ((Vector3Int) ref this.fakeValue).z = this.InternalDecryptField(this.hiddenValue.z);
          this.fakeValueActive = true;
        }
        else
          this.fakeValueActive = false;
      }
    }

    public int y
    {
      get
      {
        int y = this.InternalDecryptField(this.hiddenValue.y);
        if (ObscuredCheatingDetector.ExistsAndIsRunning && this.fakeValueActive && Math.Abs(y - ((Vector3Int) ref this.fakeValue).y) > 0)
          ObscuredCheatingDetector.Instance.OnCheatingDetected();
        return y;
      }
      set
      {
        this.hiddenValue.y = this.InternalEncryptField(value);
        if (ObscuredCheatingDetector.ExistsAndIsRunning)
        {
          ((Vector3Int) ref this.fakeValue).x = this.InternalDecryptField(this.hiddenValue.x);
          ((Vector3Int) ref this.fakeValue).y = value;
          ((Vector3Int) ref this.fakeValue).z = this.InternalDecryptField(this.hiddenValue.z);
          this.fakeValueActive = true;
        }
        else
          this.fakeValueActive = false;
      }
    }

    public int z
    {
      get
      {
        int z = this.InternalDecryptField(this.hiddenValue.z);
        if (ObscuredCheatingDetector.ExistsAndIsRunning && this.fakeValueActive && Math.Abs(z - ((Vector3Int) ref this.fakeValue).z) > 0)
          ObscuredCheatingDetector.Instance.OnCheatingDetected();
        return z;
      }
      set
      {
        this.hiddenValue.z = this.InternalEncryptField(value);
        if (ObscuredCheatingDetector.ExistsAndIsRunning)
        {
          ((Vector3Int) ref this.fakeValue).x = this.InternalDecryptField(this.hiddenValue.x);
          ((Vector3Int) ref this.fakeValue).y = this.InternalDecryptField(this.hiddenValue.y);
          ((Vector3Int) ref this.fakeValue).z = value;
          this.fakeValueActive = true;
        }
        else
          this.fakeValueActive = false;
      }
    }

    public int this[int index]
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
            throw new IndexOutOfRangeException("Invalid ObscuredVector3Int index!");
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
            throw new IndexOutOfRangeException("Invalid ObscuredVector3Int index!");
        }
      }
    }

    public static void SetNewCryptoKey(int newKey) => ObscuredVector3Int.cryptoKey = newKey;

    public static ObscuredVector3Int.RawEncryptedVector3Int Encrypt(Vector3Int value)
    {
      return ObscuredVector3Int.Encrypt(value, 0);
    }

    public static ObscuredVector3Int.RawEncryptedVector3Int Encrypt(Vector3Int value, int key)
    {
      return ObscuredVector3Int.Encrypt(((Vector3Int) ref value).x, ((Vector3Int) ref value).y, ((Vector3Int) ref value).z, key);
    }

    public static ObscuredVector3Int.RawEncryptedVector3Int Encrypt(int x, int y, int z, int key)
    {
      if (key == 0)
        key = ObscuredVector3Int.cryptoKey;
      ObscuredVector3Int.RawEncryptedVector3Int encryptedVector3Int;
      encryptedVector3Int.x = ObscuredInt.Encrypt(x, key);
      encryptedVector3Int.y = ObscuredInt.Encrypt(y, key);
      encryptedVector3Int.z = ObscuredInt.Encrypt(z, key);
      return encryptedVector3Int;
    }

    public static Vector3Int Decrypt(ObscuredVector3Int.RawEncryptedVector3Int value)
    {
      return ObscuredVector3Int.Decrypt(value, 0);
    }

    public static Vector3Int Decrypt(ObscuredVector3Int.RawEncryptedVector3Int value, int key)
    {
      if (key == 0)
        key = ObscuredVector3Int.cryptoKey;
      Vector3Int vector3Int = new Vector3Int();
      ((Vector3Int) ref vector3Int).x = ObscuredInt.Decrypt(value.x, key);
      ((Vector3Int) ref vector3Int).y = ObscuredInt.Decrypt(value.y, key);
      ((Vector3Int) ref vector3Int).z = ObscuredInt.Decrypt(value.z, key);
      return vector3Int;
    }

    public static ObscuredVector3Int FromEncrypted(
      ObscuredVector3Int.RawEncryptedVector3Int encrypted)
    {
      ObscuredVector3Int obscuredVector3Int = new ObscuredVector3Int();
      obscuredVector3Int.SetEncrypted(encrypted);
      return obscuredVector3Int;
    }

    public void ApplyNewCryptoKey()
    {
      if (this.currentCryptoKey == ObscuredVector3Int.cryptoKey)
        return;
      this.hiddenValue = ObscuredVector3Int.Encrypt(this.InternalDecrypt(), ObscuredVector3Int.cryptoKey);
      this.currentCryptoKey = ObscuredVector3Int.cryptoKey;
    }

    public void RandomizeCryptoKey()
    {
      Vector3Int vector3Int = this.InternalDecrypt();
      this.currentCryptoKey = ThreadSafeRandom.Next();
      this.hiddenValue = ObscuredVector3Int.Encrypt(vector3Int, this.currentCryptoKey);
    }

    public ObscuredVector3Int.RawEncryptedVector3Int GetEncrypted()
    {
      this.ApplyNewCryptoKey();
      return this.hiddenValue;
    }

    public void SetEncrypted(
      ObscuredVector3Int.RawEncryptedVector3Int encrypted)
    {
      this.inited = true;
      this.hiddenValue = encrypted;
      if (this.currentCryptoKey == 0)
        this.currentCryptoKey = ObscuredVector3Int.cryptoKey;
      if (ObscuredCheatingDetector.ExistsAndIsRunning)
      {
        this.fakeValueActive = false;
        this.fakeValue = this.InternalDecrypt();
        this.fakeValueActive = true;
      }
      else
        this.fakeValueActive = false;
    }

    public Vector3Int GetDecrypted() => this.InternalDecrypt();

    private Vector3Int InternalDecrypt()
    {
      if (!this.inited)
      {
        this.currentCryptoKey = ObscuredVector3Int.cryptoKey;
        this.hiddenValue = ObscuredVector3Int.Encrypt(ObscuredVector3Int.zero, ObscuredVector3Int.cryptoKey);
        this.fakeValue = ObscuredVector3Int.zero;
        this.fakeValueActive = false;
        this.inited = true;
        return ObscuredVector3Int.zero;
      }
      Vector3Int vector3Int1 = new Vector3Int();
      ((Vector3Int) ref vector3Int1).x = ObscuredInt.Decrypt(this.hiddenValue.x, this.currentCryptoKey);
      ((Vector3Int) ref vector3Int1).y = ObscuredInt.Decrypt(this.hiddenValue.y, this.currentCryptoKey);
      ((Vector3Int) ref vector3Int1).z = ObscuredInt.Decrypt(this.hiddenValue.z, this.currentCryptoKey);
      Vector3Int vector3Int2 = vector3Int1;
      if (ObscuredCheatingDetector.ExistsAndIsRunning && this.fakeValueActive && Vector3Int.op_Inequality(vector3Int2, this.fakeValue))
        ObscuredCheatingDetector.Instance.OnCheatingDetected();
      return vector3Int2;
    }

    private int InternalDecryptField(int encrypted)
    {
      int key = ObscuredVector3Int.cryptoKey;
      if (this.currentCryptoKey != ObscuredVector3Int.cryptoKey)
        key = this.currentCryptoKey;
      return ObscuredInt.Decrypt(encrypted, key);
    }

    private int InternalEncryptField(int encrypted)
    {
      return ObscuredInt.Encrypt(encrypted, ObscuredVector3Int.cryptoKey);
    }

    public static implicit operator ObscuredVector3Int(Vector3Int value)
    {
      return new ObscuredVector3Int(value);
    }

    public static implicit operator Vector3Int(ObscuredVector3Int value) => value.InternalDecrypt();

    public static implicit operator Vector3(ObscuredVector3Int value)
    {
      return Vector3Int.op_Implicit(value.InternalDecrypt());
    }

    public static ObscuredVector3Int operator +(ObscuredVector3Int a, ObscuredVector3Int b)
    {
      return (ObscuredVector3Int) Vector3Int.op_Addition(a.InternalDecrypt(), b.InternalDecrypt());
    }

    public static ObscuredVector3Int operator +(Vector3Int a, ObscuredVector3Int b)
    {
      return (ObscuredVector3Int) Vector3Int.op_Addition(a, b.InternalDecrypt());
    }

    public static ObscuredVector3Int operator +(ObscuredVector3Int a, Vector3Int b)
    {
      return (ObscuredVector3Int) Vector3Int.op_Addition(a.InternalDecrypt(), b);
    }

    public static ObscuredVector3Int operator -(ObscuredVector3Int a, ObscuredVector3Int b)
    {
      return (ObscuredVector3Int) Vector3Int.op_Subtraction(a.InternalDecrypt(), b.InternalDecrypt());
    }

    public static ObscuredVector3Int operator -(Vector3Int a, ObscuredVector3Int b)
    {
      return (ObscuredVector3Int) Vector3Int.op_Subtraction(a, b.InternalDecrypt());
    }

    public static ObscuredVector3Int operator -(ObscuredVector3Int a, Vector3Int b)
    {
      return (ObscuredVector3Int) Vector3Int.op_Subtraction(a.InternalDecrypt(), b);
    }

    public static ObscuredVector3Int operator *(ObscuredVector3Int a, int d)
    {
      return (ObscuredVector3Int) Vector3Int.op_Multiply(a.InternalDecrypt(), d);
    }

    public static bool operator ==(ObscuredVector3Int lhs, ObscuredVector3Int rhs)
    {
      return Vector3Int.op_Equality(lhs.InternalDecrypt(), rhs.InternalDecrypt());
    }

    public static bool operator ==(Vector3Int lhs, ObscuredVector3Int rhs)
    {
      return Vector3Int.op_Equality(lhs, rhs.InternalDecrypt());
    }

    public static bool operator ==(ObscuredVector3Int lhs, Vector3Int rhs)
    {
      return Vector3Int.op_Equality(lhs.InternalDecrypt(), rhs);
    }

    public static bool operator !=(ObscuredVector3Int lhs, ObscuredVector3Int rhs)
    {
      return Vector3Int.op_Inequality(lhs.InternalDecrypt(), rhs.InternalDecrypt());
    }

    public static bool operator !=(Vector3Int lhs, ObscuredVector3Int rhs)
    {
      return Vector3Int.op_Inequality(lhs, rhs.InternalDecrypt());
    }

    public static bool operator !=(ObscuredVector3Int lhs, Vector3Int rhs)
    {
      return Vector3Int.op_Inequality(lhs.InternalDecrypt(), rhs);
    }

    public override bool Equals(object other) => this.InternalDecrypt().Equals(other);

    public override int GetHashCode() => this.InternalDecrypt().GetHashCode();

    public override string ToString() => this.InternalDecrypt().ToString();

    public string ToString(string format)
    {
      Vector3Int vector3Int = this.InternalDecrypt();
      return ((Vector3Int) ref vector3Int).ToString(format);
    }

    [Serializable]
    public struct RawEncryptedVector3Int
    {
      public int x;
      public int y;
      public int z;
    }
  }
}
