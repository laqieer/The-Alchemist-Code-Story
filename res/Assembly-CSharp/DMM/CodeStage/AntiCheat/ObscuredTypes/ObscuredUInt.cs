// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.ObscuredTypes.ObscuredUInt
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
  public struct ObscuredUInt : 
    IFormattable,
    IEquatable<ObscuredUInt>,
    IComparable<ObscuredUInt>,
    IComparable<uint>,
    IComparable
  {
    private static uint cryptoKey = 240513;
    [SerializeField]
    private uint currentCryptoKey;
    [SerializeField]
    private uint hiddenValue;
    [SerializeField]
    private bool inited;
    [SerializeField]
    private uint fakeValue;
    [SerializeField]
    private bool fakeValueActive;

    private ObscuredUInt(uint value)
    {
      this.currentCryptoKey = ObscuredUInt.cryptoKey;
      this.hiddenValue = ObscuredUInt.Encrypt(value);
      bool existsAndIsRunning = ObscuredCheatingDetector.ExistsAndIsRunning;
      this.fakeValue = !existsAndIsRunning ? 0U : value;
      this.fakeValueActive = existsAndIsRunning;
      this.inited = true;
    }

    public static void SetNewCryptoKey(uint newKey) => ObscuredUInt.cryptoKey = newKey;

    public static uint Encrypt(uint value) => ObscuredUInt.Encrypt(value, 0U);

    public static uint Decrypt(uint value) => ObscuredUInt.Decrypt(value, 0U);

    public static uint Encrypt(uint value, uint key)
    {
      return key == 0U ? value ^ ObscuredUInt.cryptoKey : value ^ key;
    }

    public static uint Decrypt(uint value, uint key)
    {
      return key == 0U ? value ^ ObscuredUInt.cryptoKey : value ^ key;
    }

    public static ObscuredUInt FromEncrypted(uint encrypted)
    {
      ObscuredUInt obscuredUint = new ObscuredUInt();
      obscuredUint.SetEncrypted(encrypted);
      return obscuredUint;
    }

    public void ApplyNewCryptoKey()
    {
      if ((int) this.currentCryptoKey == (int) ObscuredUInt.cryptoKey)
        return;
      this.hiddenValue = ObscuredUInt.Encrypt(this.InternalDecrypt(), ObscuredUInt.cryptoKey);
      this.currentCryptoKey = ObscuredUInt.cryptoKey;
    }

    public void RandomizeCryptoKey()
    {
      uint num = this.InternalDecrypt();
      this.currentCryptoKey = (uint) ThreadSafeRandom.Next();
      this.hiddenValue = ObscuredUInt.Encrypt(num, this.currentCryptoKey);
    }

    public uint GetEncrypted()
    {
      this.ApplyNewCryptoKey();
      return this.hiddenValue;
    }

    public void SetEncrypted(uint encrypted)
    {
      this.inited = true;
      this.hiddenValue = encrypted;
      if (this.currentCryptoKey == 0U)
        this.currentCryptoKey = ObscuredUInt.cryptoKey;
      if (ObscuredCheatingDetector.ExistsAndIsRunning)
      {
        this.fakeValueActive = false;
        this.fakeValue = this.InternalDecrypt();
        this.fakeValueActive = true;
      }
      else
        this.fakeValueActive = false;
    }

    public uint GetDecrypted() => this.InternalDecrypt();

    private uint InternalDecrypt()
    {
      if (!this.inited)
      {
        this.currentCryptoKey = ObscuredUInt.cryptoKey;
        this.hiddenValue = ObscuredUInt.Encrypt(0U);
        this.fakeValue = 0U;
        this.fakeValueActive = false;
        this.inited = true;
        return 0;
      }
      uint num = ObscuredUInt.Decrypt(this.hiddenValue, this.currentCryptoKey);
      if (ObscuredCheatingDetector.ExistsAndIsRunning && this.fakeValueActive && (int) num != (int) this.fakeValue)
        ObscuredCheatingDetector.Instance.OnCheatingDetected();
      return num;
    }

    public static implicit operator ObscuredUInt(uint value) => new ObscuredUInt(value);

    public static implicit operator uint(ObscuredUInt value) => value.InternalDecrypt();

    public static explicit operator ObscuredInt(ObscuredUInt value)
    {
      return (ObscuredInt) (int) value.InternalDecrypt();
    }

    public static ObscuredUInt operator ++(ObscuredUInt input)
    {
      uint num = input.InternalDecrypt() + 1U;
      input.hiddenValue = ObscuredUInt.Encrypt(num, input.currentCryptoKey);
      if (ObscuredCheatingDetector.ExistsAndIsRunning)
      {
        input.fakeValue = num;
        input.fakeValueActive = true;
      }
      else
        input.fakeValueActive = false;
      return input;
    }

    public static ObscuredUInt operator --(ObscuredUInt input)
    {
      uint num = input.InternalDecrypt() - 1U;
      input.hiddenValue = ObscuredUInt.Encrypt(num, input.currentCryptoKey);
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
      return obj is ObscuredUInt obscuredUint && this.Equals(obscuredUint);
    }

    public bool Equals(ObscuredUInt obj)
    {
      return (int) this.currentCryptoKey == (int) obj.currentCryptoKey ? (int) this.hiddenValue == (int) obj.hiddenValue : (int) ObscuredUInt.Decrypt(this.hiddenValue, this.currentCryptoKey) == (int) ObscuredUInt.Decrypt(obj.hiddenValue, obj.currentCryptoKey);
    }

    public int CompareTo(ObscuredUInt other)
    {
      return this.InternalDecrypt().CompareTo(other.InternalDecrypt());
    }

    public int CompareTo(uint other) => this.InternalDecrypt().CompareTo(other);

    public int CompareTo(object obj) => this.InternalDecrypt().CompareTo(obj);
  }
}
