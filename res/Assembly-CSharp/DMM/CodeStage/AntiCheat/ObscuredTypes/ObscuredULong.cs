// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.ObscuredTypes.ObscuredULong
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
  public struct ObscuredULong : 
    IFormattable,
    IEquatable<ObscuredULong>,
    IComparable<ObscuredULong>,
    IComparable<ulong>,
    IComparable
  {
    private static ulong cryptoKey = 444443;
    [SerializeField]
    private ulong currentCryptoKey;
    [SerializeField]
    private ulong hiddenValue;
    [SerializeField]
    private bool inited;
    [SerializeField]
    private ulong fakeValue;
    [SerializeField]
    private bool fakeValueActive;

    private ObscuredULong(ulong value)
    {
      this.currentCryptoKey = ObscuredULong.cryptoKey;
      this.hiddenValue = ObscuredULong.Encrypt(value);
      bool existsAndIsRunning = ObscuredCheatingDetector.ExistsAndIsRunning;
      this.fakeValue = !existsAndIsRunning ? 0UL : value;
      this.fakeValueActive = existsAndIsRunning;
      this.inited = true;
    }

    public static void SetNewCryptoKey(ulong newKey) => ObscuredULong.cryptoKey = newKey;

    public static ulong Encrypt(ulong value) => ObscuredULong.Encrypt(value, 0UL);

    public static ulong Decrypt(ulong value) => ObscuredULong.Decrypt(value, 0UL);

    public static ulong Encrypt(ulong value, ulong key)
    {
      return key == 0UL ? value ^ ObscuredULong.cryptoKey : value ^ key;
    }

    public static ulong Decrypt(ulong value, ulong key)
    {
      return key == 0UL ? value ^ ObscuredULong.cryptoKey : value ^ key;
    }

    public static ObscuredULong FromEncrypted(ulong encrypted)
    {
      ObscuredULong obscuredUlong = new ObscuredULong();
      obscuredUlong.SetEncrypted(encrypted);
      return obscuredUlong;
    }

    public void ApplyNewCryptoKey()
    {
      if ((long) this.currentCryptoKey == (long) ObscuredULong.cryptoKey)
        return;
      this.hiddenValue = ObscuredULong.Encrypt(this.InternalDecrypt(), ObscuredULong.cryptoKey);
      this.currentCryptoKey = ObscuredULong.cryptoKey;
    }

    public void RandomizeCryptoKey()
    {
      ulong num = this.InternalDecrypt();
      this.currentCryptoKey = (ulong) ThreadSafeRandom.Next();
      this.hiddenValue = ObscuredULong.Encrypt(num, this.currentCryptoKey);
    }

    public ulong GetEncrypted()
    {
      this.ApplyNewCryptoKey();
      return this.hiddenValue;
    }

    public void SetEncrypted(ulong encrypted)
    {
      this.inited = true;
      this.hiddenValue = encrypted;
      if (this.currentCryptoKey == 0UL)
        this.currentCryptoKey = ObscuredULong.cryptoKey;
      if (ObscuredCheatingDetector.ExistsAndIsRunning)
      {
        this.fakeValueActive = false;
        this.fakeValue = this.InternalDecrypt();
        this.fakeValueActive = true;
      }
      else
        this.fakeValueActive = false;
    }

    public ulong GetDecrypted() => this.InternalDecrypt();

    private ulong InternalDecrypt()
    {
      if (!this.inited)
      {
        this.currentCryptoKey = ObscuredULong.cryptoKey;
        this.hiddenValue = ObscuredULong.Encrypt(0UL);
        this.fakeValue = 0UL;
        this.fakeValueActive = false;
        this.inited = true;
        return 0;
      }
      ulong num = ObscuredULong.Decrypt(this.hiddenValue, this.currentCryptoKey);
      if (ObscuredCheatingDetector.ExistsAndIsRunning && this.fakeValueActive && (long) num != (long) this.fakeValue)
        ObscuredCheatingDetector.Instance.OnCheatingDetected();
      return num;
    }

    public static implicit operator ObscuredULong(ulong value) => new ObscuredULong(value);

    public static implicit operator ulong(ObscuredULong value) => value.InternalDecrypt();

    public static ObscuredULong operator ++(ObscuredULong input)
    {
      ulong num = input.InternalDecrypt() + 1UL;
      input.hiddenValue = ObscuredULong.Encrypt(num, input.currentCryptoKey);
      if (ObscuredCheatingDetector.ExistsAndIsRunning)
      {
        input.fakeValue = num;
        input.fakeValueActive = true;
      }
      else
        input.fakeValueActive = false;
      return input;
    }

    public static ObscuredULong operator --(ObscuredULong input)
    {
      ulong num = input.InternalDecrypt() - 1UL;
      input.hiddenValue = ObscuredULong.Encrypt(num, input.currentCryptoKey);
      if (ObscuredCheatingDetector.ExistsAndIsRunning)
      {
        input.fakeValue = num;
        input.fakeValueActive = true;
      }
      else
        input.fakeValueActive = false;
      return input;
    }

    public override int GetHashCode() => this.InternalDecrypt().GetHashCode();

    public override string ToString() => this.InternalDecrypt().ToString();

    public string ToString(string format) => this.InternalDecrypt().ToString(format);

    public string ToString(IFormatProvider provider) => this.InternalDecrypt().ToString(provider);

    public string ToString(string format, IFormatProvider provider)
    {
      return this.InternalDecrypt().ToString(format, provider);
    }

    public override bool Equals(object obj)
    {
      return obj is ObscuredULong obscuredUlong && this.Equals(obscuredUlong);
    }

    public bool Equals(ObscuredULong obj)
    {
      return (long) this.currentCryptoKey == (long) obj.currentCryptoKey ? (long) this.hiddenValue == (long) obj.hiddenValue : (long) ObscuredULong.Decrypt(this.hiddenValue, this.currentCryptoKey) == (long) ObscuredULong.Decrypt(obj.hiddenValue, obj.currentCryptoKey);
    }

    public int CompareTo(ObscuredULong other)
    {
      return this.InternalDecrypt().CompareTo(other.InternalDecrypt());
    }

    public int CompareTo(ulong other) => this.InternalDecrypt().CompareTo(other);

    public int CompareTo(object obj) => this.InternalDecrypt().CompareTo(obj);
  }
}
