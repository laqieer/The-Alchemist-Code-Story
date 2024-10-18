// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.ObscuredTypes.ObscuredBool
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using CodeStage.AntiCheat.Detectors;
using CodeStage.AntiCheat.Utils;
using System;
using UnityEngine;
using UnityEngine.Serialization;

#nullable disable
namespace CodeStage.AntiCheat.ObscuredTypes
{
  [Serializable]
  public struct ObscuredBool : 
    IEquatable<ObscuredBool>,
    IComparable<ObscuredBool>,
    IComparable<bool>,
    IComparable
  {
    private static byte cryptoKey = 215;
    [SerializeField]
    private byte currentCryptoKey;
    [SerializeField]
    private int hiddenValue;
    [SerializeField]
    private bool inited;
    [SerializeField]
    private bool fakeValue;
    [SerializeField]
    [FormerlySerializedAs("fakeValueChanged")]
    private bool fakeValueActive;

    private ObscuredBool(bool value)
    {
      this.currentCryptoKey = ObscuredBool.cryptoKey;
      this.hiddenValue = ObscuredBool.Encrypt(value);
      bool existsAndIsRunning = ObscuredCheatingDetector.ExistsAndIsRunning;
      this.fakeValue = existsAndIsRunning && value;
      this.fakeValueActive = existsAndIsRunning;
      this.inited = true;
    }

    public static void SetNewCryptoKey(byte newKey) => ObscuredBool.cryptoKey = newKey;

    public static int Encrypt(bool value) => ObscuredBool.Encrypt(value, (byte) 0);

    public static int Encrypt(bool value, byte key)
    {
      if (key == (byte) 0)
        key = ObscuredBool.cryptoKey;
      return (!value ? 181 : 213) ^ (int) key;
    }

    public static bool Decrypt(int value) => ObscuredBool.Decrypt(value, (byte) 0);

    public static bool Decrypt(int value, byte key)
    {
      if (key == (byte) 0)
        key = ObscuredBool.cryptoKey;
      value ^= (int) key;
      return value != 181;
    }

    public static ObscuredBool FromEncrypted(int encrypted)
    {
      ObscuredBool obscuredBool = new ObscuredBool();
      obscuredBool.SetEncrypted(encrypted);
      return obscuredBool;
    }

    public void ApplyNewCryptoKey()
    {
      if ((int) this.currentCryptoKey == (int) ObscuredBool.cryptoKey)
        return;
      this.hiddenValue = ObscuredBool.Encrypt(this.InternalDecrypt(), ObscuredBool.cryptoKey);
      this.currentCryptoKey = ObscuredBool.cryptoKey;
    }

    public void RandomizeCryptoKey()
    {
      bool flag = this.InternalDecrypt();
      this.currentCryptoKey = (byte) ThreadSafeRandom.Next(150);
      this.hiddenValue = ObscuredBool.Encrypt(flag, this.currentCryptoKey);
    }

    public int GetEncrypted()
    {
      this.ApplyNewCryptoKey();
      return this.hiddenValue;
    }

    public void SetEncrypted(int encrypted)
    {
      this.inited = true;
      this.hiddenValue = encrypted;
      if (this.currentCryptoKey == (byte) 0)
        this.currentCryptoKey = ObscuredBool.cryptoKey;
      if (ObscuredCheatingDetector.ExistsAndIsRunning)
      {
        this.fakeValueActive = false;
        this.fakeValue = this.InternalDecrypt();
        this.fakeValueActive = true;
      }
      else
        this.fakeValueActive = false;
    }

    public bool GetDecrypted() => this.InternalDecrypt();

    private bool InternalDecrypt()
    {
      if (!this.inited)
      {
        this.currentCryptoKey = ObscuredBool.cryptoKey;
        this.hiddenValue = ObscuredBool.Encrypt(false);
        this.fakeValue = false;
        this.fakeValueActive = false;
        this.inited = true;
        return false;
      }
      bool flag = (this.hiddenValue ^ (int) this.currentCryptoKey) != 181;
      if (ObscuredCheatingDetector.ExistsAndIsRunning && this.fakeValueActive && flag != this.fakeValue)
        ObscuredCheatingDetector.Instance.OnCheatingDetected();
      return flag;
    }

    public static implicit operator ObscuredBool(bool value) => new ObscuredBool(value);

    public static implicit operator bool(ObscuredBool value) => value.InternalDecrypt();

    public override int GetHashCode() => this.InternalDecrypt().GetHashCode();

    public override string ToString() => this.InternalDecrypt().ToString();

    public override bool Equals(object obj)
    {
      return obj is ObscuredBool obscuredBool && this.Equals(obscuredBool);
    }

    public bool Equals(ObscuredBool obj)
    {
      return (int) this.currentCryptoKey == (int) obj.currentCryptoKey ? this.hiddenValue == obj.hiddenValue : ObscuredBool.Decrypt(this.hiddenValue, this.currentCryptoKey) == ObscuredBool.Decrypt(obj.hiddenValue, obj.currentCryptoKey);
    }

    public int CompareTo(ObscuredBool other)
    {
      return this.InternalDecrypt().CompareTo(other.InternalDecrypt());
    }

    public int CompareTo(bool other) => this.InternalDecrypt().CompareTo(other);

    public int CompareTo(object obj) => this.InternalDecrypt().CompareTo(obj);
  }
}
