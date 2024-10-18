// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.ObscuredTypes.ObscuredQuaternion
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
  public struct ObscuredQuaternion
  {
    private static int cryptoKey = 120205;
    private static readonly Quaternion identity = Quaternion.identity;
    [SerializeField]
    private int currentCryptoKey;
    [SerializeField]
    private ObscuredQuaternion.RawEncryptedQuaternion hiddenValue;
    [SerializeField]
    private bool inited;
    [SerializeField]
    private Quaternion fakeValue;
    [SerializeField]
    private bool fakeValueActive;

    private ObscuredQuaternion(Quaternion value)
    {
      this.currentCryptoKey = ObscuredQuaternion.cryptoKey;
      this.hiddenValue = ObscuredQuaternion.Encrypt(value);
      bool existsAndIsRunning = ObscuredCheatingDetector.ExistsAndIsRunning;
      this.fakeValue = !existsAndIsRunning ? ObscuredQuaternion.identity : value;
      this.fakeValueActive = existsAndIsRunning;
      this.inited = true;
    }

    public ObscuredQuaternion(float x, float y, float z, float w)
    {
      this.currentCryptoKey = ObscuredQuaternion.cryptoKey;
      this.hiddenValue = ObscuredQuaternion.Encrypt(x, y, z, w, this.currentCryptoKey);
      if (ObscuredCheatingDetector.ExistsAndIsRunning)
      {
        this.fakeValue = new Quaternion(x, y, z, w);
        this.fakeValueActive = true;
      }
      else
      {
        this.fakeValue = ObscuredQuaternion.identity;
        this.fakeValueActive = false;
      }
      this.inited = true;
    }

    public static void SetNewCryptoKey(int newKey) => ObscuredQuaternion.cryptoKey = newKey;

    public static ObscuredQuaternion.RawEncryptedQuaternion Encrypt(Quaternion value)
    {
      return ObscuredQuaternion.Encrypt(value, 0);
    }

    public static ObscuredQuaternion.RawEncryptedQuaternion Encrypt(Quaternion value, int key)
    {
      return ObscuredQuaternion.Encrypt(value.x, value.y, value.z, value.w, key);
    }

    public static ObscuredQuaternion.RawEncryptedQuaternion Encrypt(
      float x,
      float y,
      float z,
      float w,
      int key)
    {
      if (key == 0)
        key = ObscuredQuaternion.cryptoKey;
      ObscuredQuaternion.RawEncryptedQuaternion encryptedQuaternion;
      encryptedQuaternion.x = ObscuredFloat.Encrypt(x, key);
      encryptedQuaternion.y = ObscuredFloat.Encrypt(y, key);
      encryptedQuaternion.z = ObscuredFloat.Encrypt(z, key);
      encryptedQuaternion.w = ObscuredFloat.Encrypt(w, key);
      return encryptedQuaternion;
    }

    public static Quaternion Decrypt(ObscuredQuaternion.RawEncryptedQuaternion value)
    {
      return ObscuredQuaternion.Decrypt(value, 0);
    }

    public static Quaternion Decrypt(ObscuredQuaternion.RawEncryptedQuaternion value, int key)
    {
      if (key == 0)
        key = ObscuredQuaternion.cryptoKey;
      Quaternion quaternion;
      quaternion.x = ObscuredFloat.Decrypt(value.x, key);
      quaternion.y = ObscuredFloat.Decrypt(value.y, key);
      quaternion.z = ObscuredFloat.Decrypt(value.z, key);
      quaternion.w = ObscuredFloat.Decrypt(value.w, key);
      return quaternion;
    }

    public static ObscuredQuaternion FromEncrypted(
      ObscuredQuaternion.RawEncryptedQuaternion encrypted)
    {
      ObscuredQuaternion obscuredQuaternion = new ObscuredQuaternion();
      obscuredQuaternion.SetEncrypted(encrypted);
      return obscuredQuaternion;
    }

    public void ApplyNewCryptoKey()
    {
      if (this.currentCryptoKey == ObscuredQuaternion.cryptoKey)
        return;
      this.hiddenValue = ObscuredQuaternion.Encrypt(this.InternalDecrypt(), ObscuredQuaternion.cryptoKey);
      this.currentCryptoKey = ObscuredQuaternion.cryptoKey;
    }

    public void RandomizeCryptoKey()
    {
      Quaternion quaternion = this.InternalDecrypt();
      this.currentCryptoKey = ThreadSafeRandom.Next();
      this.hiddenValue = ObscuredQuaternion.Encrypt(quaternion, this.currentCryptoKey);
    }

    public ObscuredQuaternion.RawEncryptedQuaternion GetEncrypted()
    {
      this.ApplyNewCryptoKey();
      return this.hiddenValue;
    }

    public void SetEncrypted(
      ObscuredQuaternion.RawEncryptedQuaternion encrypted)
    {
      this.inited = true;
      this.hiddenValue = encrypted;
      if (this.currentCryptoKey == 0)
        this.currentCryptoKey = ObscuredQuaternion.cryptoKey;
      if (ObscuredCheatingDetector.ExistsAndIsRunning)
      {
        this.fakeValueActive = false;
        this.fakeValue = this.InternalDecrypt();
        this.fakeValueActive = true;
      }
      else
        this.fakeValueActive = false;
    }

    public Quaternion GetDecrypted() => this.InternalDecrypt();

    private Quaternion InternalDecrypt()
    {
      if (!this.inited)
      {
        this.currentCryptoKey = ObscuredQuaternion.cryptoKey;
        this.hiddenValue = ObscuredQuaternion.Encrypt(ObscuredQuaternion.identity);
        this.fakeValue = ObscuredQuaternion.identity;
        this.fakeValueActive = false;
        this.inited = true;
        return ObscuredQuaternion.identity;
      }
      Quaternion q1;
      q1.x = ObscuredFloat.Decrypt(this.hiddenValue.x, this.currentCryptoKey);
      q1.y = ObscuredFloat.Decrypt(this.hiddenValue.y, this.currentCryptoKey);
      q1.z = ObscuredFloat.Decrypt(this.hiddenValue.z, this.currentCryptoKey);
      q1.w = ObscuredFloat.Decrypt(this.hiddenValue.w, this.currentCryptoKey);
      if (ObscuredCheatingDetector.ExistsAndIsRunning && this.fakeValueActive && !this.CompareQuaternionsWithTolerance(q1, this.fakeValue))
        ObscuredCheatingDetector.Instance.OnCheatingDetected();
      return q1;
    }

    private bool CompareQuaternionsWithTolerance(Quaternion q1, Quaternion q2)
    {
      float quaternionEpsilon = ObscuredCheatingDetector.Instance.quaternionEpsilon;
      return (double) Math.Abs(q1.x - q2.x) < (double) quaternionEpsilon && (double) Math.Abs(q1.y - q2.y) < (double) quaternionEpsilon && (double) Math.Abs(q1.z - q2.z) < (double) quaternionEpsilon && (double) Math.Abs(q1.w - q2.w) < (double) quaternionEpsilon;
    }

    public static implicit operator ObscuredQuaternion(Quaternion value)
    {
      return new ObscuredQuaternion(value);
    }

    public static implicit operator Quaternion(ObscuredQuaternion value) => value.InternalDecrypt();

    public override int GetHashCode() => this.InternalDecrypt().GetHashCode();

    public override string ToString() => this.InternalDecrypt().ToString();

    public string ToString(string format)
    {
      Quaternion quaternion = this.InternalDecrypt();
      return ((Quaternion) ref quaternion).ToString(format);
    }

    [Serializable]
    public struct RawEncryptedQuaternion
    {
      public int x;
      public int y;
      public int z;
      public int w;
    }
  }
}
