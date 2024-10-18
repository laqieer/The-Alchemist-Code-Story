// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.ObscuredTypes.ObscuredVector2
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
  public struct ObscuredVector2
  {
    private static int cryptoKey = 120206;
    private static readonly Vector2 zero = Vector2.zero;
    [SerializeField]
    private int currentCryptoKey;
    [SerializeField]
    private ObscuredVector2.RawEncryptedVector2 hiddenValue;
    [SerializeField]
    private bool inited;
    [SerializeField]
    private Vector2 fakeValue;
    [SerializeField]
    private bool fakeValueActive;

    private ObscuredVector2(Vector2 value)
    {
      this.currentCryptoKey = ObscuredVector2.cryptoKey;
      this.hiddenValue = ObscuredVector2.Encrypt(value);
      bool existsAndIsRunning = ObscuredCheatingDetector.ExistsAndIsRunning;
      this.fakeValue = !existsAndIsRunning ? ObscuredVector2.zero : value;
      this.fakeValueActive = existsAndIsRunning;
      this.inited = true;
    }

    public ObscuredVector2(float x, float y)
    {
      this.currentCryptoKey = ObscuredVector2.cryptoKey;
      this.hiddenValue = ObscuredVector2.Encrypt(x, y, this.currentCryptoKey);
      if (ObscuredCheatingDetector.ExistsAndIsRunning)
      {
        this.fakeValue = new Vector2(x, y);
        this.fakeValueActive = true;
      }
      else
      {
        this.fakeValue = ObscuredVector2.zero;
        this.fakeValueActive = false;
      }
      this.inited = true;
    }

    public float x
    {
      get
      {
        float x = this.InternalDecryptField(this.hiddenValue.x);
        if (ObscuredCheatingDetector.ExistsAndIsRunning && this.fakeValueActive && (double) Math.Abs(x - this.fakeValue.x) > (double) ObscuredCheatingDetector.Instance.vector2Epsilon)
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
        if (ObscuredCheatingDetector.ExistsAndIsRunning && this.fakeValueActive && (double) Math.Abs(y - this.fakeValue.y) > (double) ObscuredCheatingDetector.Instance.vector2Epsilon)
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
        if (index == 0)
          return this.x;
        if (index == 1)
          return this.y;
        throw new IndexOutOfRangeException("Invalid ObscuredVector2 index!");
      }
      set
      {
        if (index != 0)
        {
          if (index != 1)
            throw new IndexOutOfRangeException("Invalid ObscuredVector2 index!");
          this.y = value;
        }
        else
          this.x = value;
      }
    }

    public static void SetNewCryptoKey(int newKey) => ObscuredVector2.cryptoKey = newKey;

    public static ObscuredVector2.RawEncryptedVector2 Encrypt(Vector2 value)
    {
      return ObscuredVector2.Encrypt(value, 0);
    }

    public static ObscuredVector2.RawEncryptedVector2 Encrypt(Vector2 value, int key)
    {
      return ObscuredVector2.Encrypt(value.x, value.y, key);
    }

    public static ObscuredVector2.RawEncryptedVector2 Encrypt(float x, float y, int key)
    {
      if (key == 0)
        key = ObscuredVector2.cryptoKey;
      ObscuredVector2.RawEncryptedVector2 encryptedVector2;
      encryptedVector2.x = ObscuredFloat.Encrypt(x, key);
      encryptedVector2.y = ObscuredFloat.Encrypt(y, key);
      return encryptedVector2;
    }

    public static Vector2 Decrypt(ObscuredVector2.RawEncryptedVector2 value)
    {
      return ObscuredVector2.Decrypt(value, 0);
    }

    public static Vector2 Decrypt(ObscuredVector2.RawEncryptedVector2 value, int key)
    {
      if (key == 0)
        key = ObscuredVector2.cryptoKey;
      Vector2 vector2;
      vector2.x = ObscuredFloat.Decrypt(value.x, key);
      vector2.y = ObscuredFloat.Decrypt(value.y, key);
      return vector2;
    }

    public static ObscuredVector2 FromEncrypted(ObscuredVector2.RawEncryptedVector2 encrypted)
    {
      ObscuredVector2 obscuredVector2 = new ObscuredVector2();
      obscuredVector2.SetEncrypted(encrypted);
      return obscuredVector2;
    }

    public void ApplyNewCryptoKey()
    {
      if (this.currentCryptoKey == ObscuredVector2.cryptoKey)
        return;
      this.hiddenValue = ObscuredVector2.Encrypt(this.InternalDecrypt(), ObscuredVector2.cryptoKey);
      this.currentCryptoKey = ObscuredVector2.cryptoKey;
    }

    public void RandomizeCryptoKey()
    {
      Vector2 vector2 = this.InternalDecrypt();
      this.currentCryptoKey = ThreadSafeRandom.Next();
      this.hiddenValue = ObscuredVector2.Encrypt(vector2, this.currentCryptoKey);
    }

    public ObscuredVector2.RawEncryptedVector2 GetEncrypted()
    {
      this.ApplyNewCryptoKey();
      return this.hiddenValue;
    }

    public void SetEncrypted(ObscuredVector2.RawEncryptedVector2 encrypted)
    {
      this.inited = true;
      this.hiddenValue = encrypted;
      if (this.currentCryptoKey == 0)
        this.currentCryptoKey = ObscuredVector2.cryptoKey;
      if (ObscuredCheatingDetector.ExistsAndIsRunning)
      {
        this.fakeValueActive = false;
        this.fakeValue = this.InternalDecrypt();
        this.fakeValueActive = true;
      }
      else
        this.fakeValueActive = false;
    }

    public Vector2 GetDecrypted() => this.InternalDecrypt();

    private Vector2 InternalDecrypt()
    {
      if (!this.inited)
      {
        this.currentCryptoKey = ObscuredVector2.cryptoKey;
        this.hiddenValue = ObscuredVector2.Encrypt(ObscuredVector2.zero);
        this.fakeValue = ObscuredVector2.zero;
        this.fakeValueActive = false;
        this.inited = true;
        return ObscuredVector2.zero;
      }
      Vector2 vector1;
      vector1.x = ObscuredFloat.Decrypt(this.hiddenValue.x, this.currentCryptoKey);
      vector1.y = ObscuredFloat.Decrypt(this.hiddenValue.y, this.currentCryptoKey);
      if (ObscuredCheatingDetector.ExistsAndIsRunning && this.fakeValueActive && !this.CompareVectorsWithTolerance(vector1, this.fakeValue))
        ObscuredCheatingDetector.Instance.OnCheatingDetected();
      return vector1;
    }

    private bool CompareVectorsWithTolerance(Vector2 vector1, Vector2 vector2)
    {
      float vector2Epsilon = ObscuredCheatingDetector.Instance.vector2Epsilon;
      return (double) Math.Abs(vector1.x - vector2.x) < (double) vector2Epsilon && (double) Math.Abs(vector1.y - vector2.y) < (double) vector2Epsilon;
    }

    private float InternalDecryptField(int encrypted)
    {
      int key = ObscuredVector2.cryptoKey;
      if (this.currentCryptoKey != ObscuredVector2.cryptoKey)
        key = this.currentCryptoKey;
      return ObscuredFloat.Decrypt(encrypted, key);
    }

    private int InternalEncryptField(float encrypted)
    {
      return ObscuredFloat.Encrypt(encrypted, ObscuredVector2.cryptoKey);
    }

    public static implicit operator ObscuredVector2(Vector2 value) => new ObscuredVector2(value);

    public static implicit operator Vector2(ObscuredVector2 value) => value.InternalDecrypt();

    public static implicit operator Vector3(ObscuredVector2 value)
    {
      Vector2 vector2 = value.InternalDecrypt();
      return new Vector3(vector2.x, vector2.y, 0.0f);
    }

    public override int GetHashCode() => this.InternalDecrypt().GetHashCode();

    public override string ToString() => this.InternalDecrypt().ToString();

    public string ToString(string format)
    {
      Vector2 vector2 = this.InternalDecrypt();
      return ((Vector2) ref vector2).ToString(format);
    }

    [Serializable]
    public struct RawEncryptedVector2
    {
      public int x;
      public int y;
    }
  }
}
