// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.ObscuredTypes.ObscuredVector2Int
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
  public struct ObscuredVector2Int
  {
    private static int cryptoKey = 160122;
    private static readonly Vector2Int zero = Vector2Int.zero;
    [SerializeField]
    private int currentCryptoKey;
    [SerializeField]
    private ObscuredVector2Int.RawEncryptedVector2Int hiddenValue;
    [SerializeField]
    private bool inited;
    [SerializeField]
    private Vector2Int fakeValue;
    [SerializeField]
    private bool fakeValueActive;

    private ObscuredVector2Int(Vector2Int value)
    {
      this.currentCryptoKey = ObscuredVector2Int.cryptoKey;
      this.hiddenValue = ObscuredVector2Int.Encrypt(value);
      bool existsAndIsRunning = ObscuredCheatingDetector.ExistsAndIsRunning;
      this.fakeValue = !existsAndIsRunning ? ObscuredVector2Int.zero : value;
      this.fakeValueActive = existsAndIsRunning;
      this.inited = true;
    }

    public ObscuredVector2Int(int x, int y)
    {
      this.currentCryptoKey = ObscuredVector2Int.cryptoKey;
      this.hiddenValue = ObscuredVector2Int.Encrypt(x, y, this.currentCryptoKey);
      if (ObscuredCheatingDetector.ExistsAndIsRunning)
      {
        this.fakeValue = new Vector2Int(x, y);
        this.fakeValueActive = true;
      }
      else
      {
        this.fakeValue = ObscuredVector2Int.zero;
        this.fakeValueActive = false;
      }
      this.inited = true;
    }

    public int x
    {
      get
      {
        int x = this.InternalDecryptField(this.hiddenValue.x);
        if (ObscuredCheatingDetector.ExistsAndIsRunning && this.fakeValueActive && Math.Abs(x - ((Vector2Int) ref this.fakeValue).x) > 0)
          ObscuredCheatingDetector.Instance.OnCheatingDetected();
        return x;
      }
      set
      {
        this.hiddenValue.x = this.InternalEncryptField(value);
        if (ObscuredCheatingDetector.ExistsAndIsRunning)
        {
          ((Vector2Int) ref this.fakeValue).x = value;
          ((Vector2Int) ref this.fakeValue).y = this.InternalDecryptField(this.hiddenValue.y);
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
        if (ObscuredCheatingDetector.ExistsAndIsRunning && this.fakeValueActive && Math.Abs(y - ((Vector2Int) ref this.fakeValue).y) > 0)
          ObscuredCheatingDetector.Instance.OnCheatingDetected();
        return y;
      }
      set
      {
        this.hiddenValue.y = this.InternalEncryptField(value);
        if (ObscuredCheatingDetector.ExistsAndIsRunning)
        {
          ((Vector2Int) ref this.fakeValue).x = this.InternalDecryptField(this.hiddenValue.x);
          ((Vector2Int) ref this.fakeValue).y = value;
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
        if (index == 0)
          return this.x;
        if (index == 1)
          return this.y;
        throw new IndexOutOfRangeException("Invalid ObscuredVector2Int index!");
      }
      set
      {
        if (index != 0)
        {
          if (index != 1)
            throw new IndexOutOfRangeException("Invalid ObscuredVector2Int index!");
          this.y = value;
        }
        else
          this.x = value;
      }
    }

    public static void SetNewCryptoKey(int newKey) => ObscuredVector2Int.cryptoKey = newKey;

    public static ObscuredVector2Int.RawEncryptedVector2Int Encrypt(Vector2Int value)
    {
      return ObscuredVector2Int.Encrypt(value, 0);
    }

    public static ObscuredVector2Int.RawEncryptedVector2Int Encrypt(Vector2Int value, int key)
    {
      return ObscuredVector2Int.Encrypt(((Vector2Int) ref value).x, ((Vector2Int) ref value).y, key);
    }

    public static ObscuredVector2Int.RawEncryptedVector2Int Encrypt(int x, int y, int key)
    {
      if (key == 0)
        key = ObscuredVector2Int.cryptoKey;
      ObscuredVector2Int.RawEncryptedVector2Int encryptedVector2Int;
      encryptedVector2Int.x = ObscuredInt.Encrypt(x, key);
      encryptedVector2Int.y = ObscuredInt.Encrypt(y, key);
      return encryptedVector2Int;
    }

    public static Vector2Int Decrypt(ObscuredVector2Int.RawEncryptedVector2Int value)
    {
      return ObscuredVector2Int.Decrypt(value, 0);
    }

    public static Vector2Int Decrypt(ObscuredVector2Int.RawEncryptedVector2Int value, int key)
    {
      if (key == 0)
        key = ObscuredVector2Int.cryptoKey;
      Vector2Int vector2Int = new Vector2Int();
      ((Vector2Int) ref vector2Int).x = ObscuredInt.Decrypt(value.x, key);
      ((Vector2Int) ref vector2Int).y = ObscuredInt.Decrypt(value.y, key);
      return vector2Int;
    }

    public static ObscuredVector2Int FromEncrypted(
      ObscuredVector2Int.RawEncryptedVector2Int encrypted)
    {
      ObscuredVector2Int obscuredVector2Int = new ObscuredVector2Int();
      obscuredVector2Int.SetEncrypted(encrypted);
      return obscuredVector2Int;
    }

    public void ApplyNewCryptoKey()
    {
      if (this.currentCryptoKey == ObscuredVector2Int.cryptoKey)
        return;
      this.hiddenValue = ObscuredVector2Int.Encrypt(this.InternalDecrypt(), ObscuredVector2Int.cryptoKey);
      this.currentCryptoKey = ObscuredVector2Int.cryptoKey;
    }

    public void RandomizeCryptoKey()
    {
      Vector2Int vector2Int = this.InternalDecrypt();
      this.currentCryptoKey = ThreadSafeRandom.Next();
      this.hiddenValue = ObscuredVector2Int.Encrypt(vector2Int, this.currentCryptoKey);
    }

    public ObscuredVector2Int.RawEncryptedVector2Int GetEncrypted()
    {
      this.ApplyNewCryptoKey();
      return this.hiddenValue;
    }

    public void SetEncrypted(
      ObscuredVector2Int.RawEncryptedVector2Int encrypted)
    {
      this.inited = true;
      this.hiddenValue = encrypted;
      if (this.currentCryptoKey == 0)
        this.currentCryptoKey = ObscuredVector2Int.cryptoKey;
      if (ObscuredCheatingDetector.ExistsAndIsRunning)
      {
        this.fakeValueActive = false;
        this.fakeValue = this.InternalDecrypt();
        this.fakeValueActive = true;
      }
      else
        this.fakeValueActive = false;
    }

    public Vector2Int GetDecrypted() => this.InternalDecrypt();

    private Vector2Int InternalDecrypt()
    {
      if (!this.inited)
      {
        this.currentCryptoKey = ObscuredVector2Int.cryptoKey;
        this.hiddenValue = ObscuredVector2Int.Encrypt(ObscuredVector2Int.zero);
        this.fakeValue = ObscuredVector2Int.zero;
        this.fakeValueActive = false;
        this.inited = true;
        return ObscuredVector2Int.zero;
      }
      Vector2Int vector2Int1 = new Vector2Int();
      ((Vector2Int) ref vector2Int1).x = ObscuredInt.Decrypt(this.hiddenValue.x, this.currentCryptoKey);
      ((Vector2Int) ref vector2Int1).y = ObscuredInt.Decrypt(this.hiddenValue.y, this.currentCryptoKey);
      Vector2Int vector2Int2 = vector2Int1;
      if (ObscuredCheatingDetector.ExistsAndIsRunning && this.fakeValueActive && Vector2Int.op_Inequality(vector2Int2, this.fakeValue))
        ObscuredCheatingDetector.Instance.OnCheatingDetected();
      return vector2Int2;
    }

    private int InternalDecryptField(int encrypted)
    {
      int key = ObscuredVector2Int.cryptoKey;
      if (this.currentCryptoKey != ObscuredVector2Int.cryptoKey)
        key = this.currentCryptoKey;
      return ObscuredInt.Decrypt(encrypted, key);
    }

    private int InternalEncryptField(int encrypted)
    {
      return ObscuredInt.Encrypt(encrypted, ObscuredVector2Int.cryptoKey);
    }

    public static implicit operator ObscuredVector2Int(Vector2Int value)
    {
      return new ObscuredVector2Int(value);
    }

    public static implicit operator Vector2Int(ObscuredVector2Int value) => value.InternalDecrypt();

    public static implicit operator Vector2(ObscuredVector2Int value)
    {
      return Vector2Int.op_Implicit(value.InternalDecrypt());
    }

    public override int GetHashCode() => this.InternalDecrypt().GetHashCode();

    public override string ToString() => this.InternalDecrypt().ToString();

    [Serializable]
    public struct RawEncryptedVector2Int
    {
      public int x;
      public int y;
    }
  }
}
