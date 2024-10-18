// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.ObscuredTypes.ObscuredLong
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
  public struct ObscuredLong : 
    IFormattable,
    IEquatable<ObscuredLong>,
    IComparable<ObscuredLong>,
    IComparable<long>,
    IComparable
  {
    private static long cryptoKey = 444442;
    [SerializeField]
    private long currentCryptoKey;
    [SerializeField]
    private long hiddenValue;
    [SerializeField]
    private bool inited;
    [SerializeField]
    private long fakeValue;
    [SerializeField]
    private bool fakeValueActive;

    private ObscuredLong(long value)
    {
      this.currentCryptoKey = ObscuredLong.cryptoKey;
      this.hiddenValue = ObscuredLong.Encrypt(value);
      bool existsAndIsRunning = ObscuredCheatingDetector.ExistsAndIsRunning;
      this.fakeValue = !existsAndIsRunning ? 0L : value;
      this.fakeValueActive = existsAndIsRunning;
      this.inited = true;
    }

    public static void SetNewCryptoKey(long newKey) => ObscuredLong.cryptoKey = newKey;

    public static long Encrypt(long value) => ObscuredLong.Encrypt(value, 0L);

    public static long Decrypt(long value) => ObscuredLong.Decrypt(value, 0L);

    public static long Encrypt(long value, long key)
    {
      return key == 0L ? value ^ ObscuredLong.cryptoKey : value ^ key;
    }

    public static long Decrypt(long value, long key)
    {
      return key == 0L ? value ^ ObscuredLong.cryptoKey : value ^ key;
    }

    public static ObscuredLong FromEncrypted(long encrypted)
    {
      ObscuredLong obscuredLong = new ObscuredLong();
      obscuredLong.SetEncrypted(encrypted);
      return obscuredLong;
    }

    public void ApplyNewCryptoKey()
    {
      if (this.currentCryptoKey == ObscuredLong.cryptoKey)
        return;
      this.hiddenValue = ObscuredLong.Encrypt(this.InternalDecrypt(), ObscuredLong.cryptoKey);
      this.currentCryptoKey = ObscuredLong.cryptoKey;
    }

    public void RandomizeCryptoKey()
    {
      long num = this.InternalDecrypt();
      this.currentCryptoKey = (long) ThreadSafeRandom.Next();
      this.hiddenValue = ObscuredLong.Encrypt(num, this.currentCryptoKey);
    }

    public long GetEncrypted()
    {
      this.ApplyNewCryptoKey();
      return this.hiddenValue;
    }

    public void SetEncrypted(long encrypted)
    {
      this.inited = true;
      this.hiddenValue = encrypted;
      if (this.currentCryptoKey == 0L)
        this.currentCryptoKey = ObscuredLong.cryptoKey;
      if (ObscuredCheatingDetector.ExistsAndIsRunning)
      {
        this.fakeValueActive = false;
        this.fakeValue = this.InternalDecrypt();
        this.fakeValueActive = true;
      }
      else
        this.fakeValueActive = false;
    }

    public long GetDecrypted() => this.InternalDecrypt();

    private long InternalDecrypt()
    {
      if (!this.inited)
      {
        this.currentCryptoKey = ObscuredLong.cryptoKey;
        this.hiddenValue = ObscuredLong.Encrypt(0L);
        this.fakeValue = 0L;
        this.fakeValueActive = false;
        this.inited = true;
        return 0;
      }
      long num = ObscuredLong.Decrypt(this.hiddenValue, this.currentCryptoKey);
      if (ObscuredCheatingDetector.ExistsAndIsRunning && this.fakeValueActive && num != this.fakeValue)
        ObscuredCheatingDetector.Instance.OnCheatingDetected();
      return num;
    }

    public static implicit operator ObscuredLong(long value) => new ObscuredLong(value);

    public static implicit operator long(ObscuredLong value) => value.InternalDecrypt();

    public static ObscuredLong operator ++(ObscuredLong input)
    {
      long num = input.InternalDecrypt() + 1L;
      input.hiddenValue = ObscuredLong.Encrypt(num, input.currentCryptoKey);
      if (ObscuredCheatingDetector.ExistsAndIsRunning)
      {
        input.fakeValue = num;
        input.fakeValueActive = true;
      }
      else
        input.fakeValueActive = false;
      return input;
    }

    public static ObscuredLong operator --(ObscuredLong input)
    {
      long num = input.InternalDecrypt() - 1L;
      input.hiddenValue = ObscuredLong.Encrypt(num, input.currentCryptoKey);
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
      return obj is ObscuredLong obscuredLong && this.Equals(obscuredLong);
    }

    public bool Equals(ObscuredLong obj)
    {
      return this.currentCryptoKey == obj.currentCryptoKey ? this.hiddenValue == obj.hiddenValue : ObscuredLong.Decrypt(this.hiddenValue, this.currentCryptoKey) == ObscuredLong.Decrypt(obj.hiddenValue, obj.currentCryptoKey);
    }

    public int CompareTo(ObscuredLong other)
    {
      return this.InternalDecrypt().CompareTo(other.InternalDecrypt());
    }

    public int CompareTo(long other) => this.InternalDecrypt().CompareTo(other);

    public int CompareTo(object obj) => this.InternalDecrypt().CompareTo(obj);
  }
}
