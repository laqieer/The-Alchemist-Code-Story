// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.ObscuredTypes.ObscuredUInt
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using CodeStage.AntiCheat.Detectors;
using System;
using UnityEngine;

namespace CodeStage.AntiCheat.ObscuredTypes
{
  [Serializable]
  public struct ObscuredUInt : IEquatable<ObscuredUInt>, IFormattable
  {
    private static uint cryptoKey = 240513;
    private uint currentCryptoKey;
    private uint hiddenValue;
    private uint fakeValue;
    private bool inited;

    private ObscuredUInt(uint value)
    {
      this.currentCryptoKey = ObscuredUInt.cryptoKey;
      this.hiddenValue = value;
      this.fakeValue = 0U;
      this.inited = true;
    }

    public static void SetNewCryptoKey(uint newKey)
    {
      ObscuredUInt.cryptoKey = newKey;
    }

    public static uint Encrypt(uint value)
    {
      return ObscuredUInt.Encrypt(value, 0U);
    }

    public static uint Decrypt(uint value)
    {
      return ObscuredUInt.Decrypt(value, 0U);
    }

    public static uint Encrypt(uint value, uint key)
    {
      if (key == 0U)
        return value ^ ObscuredUInt.cryptoKey;
      return value ^ key;
    }

    public static uint Decrypt(uint value, uint key)
    {
      if (key == 0U)
        return value ^ ObscuredUInt.cryptoKey;
      return value ^ key;
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
      this.currentCryptoKey = (uint) Random.seed;
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
      if (!ObscuredCheatingDetector.IsRunning)
        return;
      this.fakeValue = this.InternalDecrypt();
    }

    private uint InternalDecrypt()
    {
      if (!this.inited)
      {
        this.currentCryptoKey = ObscuredUInt.cryptoKey;
        this.hiddenValue = ObscuredUInt.Encrypt(0U);
        this.fakeValue = 0U;
        this.inited = true;
      }
      uint num = ObscuredUInt.Decrypt(this.hiddenValue, this.currentCryptoKey);
      if (ObscuredCheatingDetector.IsRunning && this.fakeValue != 0U && (int) num != (int) this.fakeValue)
        ObscuredCheatingDetector.Instance.OnCheatingDetected();
      return num;
    }

    public static implicit operator ObscuredUInt(uint value)
    {
      ObscuredUInt obscuredUint = new ObscuredUInt(ObscuredUInt.Encrypt(value));
      if (ObscuredCheatingDetector.IsRunning)
        obscuredUint.fakeValue = value;
      return obscuredUint;
    }

    public static implicit operator uint(ObscuredUInt value)
    {
      return value.InternalDecrypt();
    }

    public static explicit operator ObscuredInt(ObscuredUInt value)
    {
      return (ObscuredInt) ((int) value.InternalDecrypt());
    }

    public static ObscuredUInt operator ++(ObscuredUInt input)
    {
      uint num = input.InternalDecrypt() + 1U;
      input.hiddenValue = ObscuredUInt.Encrypt(num, input.currentCryptoKey);
      if (ObscuredCheatingDetector.IsRunning)
        input.fakeValue = num;
      return input;
    }

    public static ObscuredUInt operator --(ObscuredUInt input)
    {
      uint num = input.InternalDecrypt() - 1U;
      input.hiddenValue = ObscuredUInt.Encrypt(num, input.currentCryptoKey);
      if (ObscuredCheatingDetector.IsRunning)
        input.fakeValue = num;
      return input;
    }

    public override bool Equals(object obj)
    {
      if (!(obj is ObscuredUInt))
        return false;
      return this.Equals((ObscuredUInt) obj);
    }

    public bool Equals(ObscuredUInt obj)
    {
      if ((int) this.currentCryptoKey == (int) obj.currentCryptoKey)
        return (int) this.hiddenValue == (int) obj.hiddenValue;
      return (int) ObscuredUInt.Decrypt(this.hiddenValue, this.currentCryptoKey) == (int) ObscuredUInt.Decrypt(obj.hiddenValue, obj.currentCryptoKey);
    }

    public override string ToString()
    {
      return this.InternalDecrypt().ToString();
    }

    public string ToString(string format)
    {
      return this.InternalDecrypt().ToString(format);
    }

    public override int GetHashCode()
    {
      return this.InternalDecrypt().GetHashCode();
    }

    public string ToString(IFormatProvider provider)
    {
      return this.InternalDecrypt().ToString(provider);
    }

    public string ToString(string format, IFormatProvider provider)
    {
      return this.InternalDecrypt().ToString(format, provider);
    }
  }
}
