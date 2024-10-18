// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.ObscuredTypes.ObscuredDecimal
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using CodeStage.AntiCheat.Common;
using CodeStage.AntiCheat.Detectors;
using CodeStage.AntiCheat.Utils;
using System;
using System.Runtime.InteropServices;
using UnityEngine;

#nullable disable
namespace CodeStage.AntiCheat.ObscuredTypes
{
  [Serializable]
  public struct ObscuredDecimal : 
    IFormattable,
    IEquatable<ObscuredDecimal>,
    IComparable<ObscuredDecimal>,
    IComparable<Decimal>,
    IComparable
  {
    private static long cryptoKey = 209208;
    [SerializeField]
    private long currentCryptoKey;
    [SerializeField]
    private ACTkByte16 hiddenValue;
    [SerializeField]
    private bool inited;
    private Decimal fakeValue;
    [SerializeField]
    private bool fakeValueActive;

    private ObscuredDecimal(Decimal value)
    {
      this.currentCryptoKey = ObscuredDecimal.cryptoKey;
      this.hiddenValue = ObscuredDecimal.InternalEncrypt(value);
      bool existsAndIsRunning = ObscuredCheatingDetector.ExistsAndIsRunning;
      this.fakeValue = !existsAndIsRunning ? 0M : value;
      this.fakeValueActive = existsAndIsRunning;
      this.inited = true;
    }

    public static void SetNewCryptoKey(long newKey) => ObscuredDecimal.cryptoKey = newKey;

    public static Decimal Encrypt(Decimal value)
    {
      return ObscuredDecimal.Encrypt(value, ObscuredDecimal.cryptoKey);
    }

    public static Decimal Encrypt(Decimal value, long key)
    {
      ObscuredDecimal.DecimalLongBytesUnion decimalLongBytesUnion = new ObscuredDecimal.DecimalLongBytesUnion();
      decimalLongBytesUnion.d = value;
      decimalLongBytesUnion.l1 ^= key;
      decimalLongBytesUnion.l2 ^= key;
      return decimalLongBytesUnion.d;
    }

    private static ACTkByte16 InternalEncrypt(Decimal value)
    {
      return ObscuredDecimal.InternalEncrypt(value, 0L);
    }

    private static ACTkByte16 InternalEncrypt(Decimal value, long key)
    {
      long num = key;
      if (num == 0L)
        num = ObscuredDecimal.cryptoKey;
      ObscuredDecimal.DecimalLongBytesUnion decimalLongBytesUnion = new ObscuredDecimal.DecimalLongBytesUnion();
      decimalLongBytesUnion.d = value;
      decimalLongBytesUnion.l1 ^= num;
      decimalLongBytesUnion.l2 ^= num;
      return decimalLongBytesUnion.b16;
    }

    public static Decimal Decrypt(Decimal value)
    {
      return ObscuredDecimal.Decrypt(value, ObscuredDecimal.cryptoKey);
    }

    public static Decimal Decrypt(Decimal value, long key)
    {
      ObscuredDecimal.DecimalLongBytesUnion decimalLongBytesUnion = new ObscuredDecimal.DecimalLongBytesUnion();
      decimalLongBytesUnion.d = value;
      decimalLongBytesUnion.l1 ^= key;
      decimalLongBytesUnion.l2 ^= key;
      return decimalLongBytesUnion.d;
    }

    public static ObscuredDecimal FromEncrypted(Decimal encrypted)
    {
      ObscuredDecimal obscuredDecimal = new ObscuredDecimal();
      obscuredDecimal.SetEncrypted(encrypted);
      return obscuredDecimal;
    }

    public void ApplyNewCryptoKey()
    {
      if (this.currentCryptoKey == ObscuredDecimal.cryptoKey)
        return;
      this.hiddenValue = ObscuredDecimal.InternalEncrypt(this.InternalDecrypt(), ObscuredDecimal.cryptoKey);
      this.currentCryptoKey = ObscuredDecimal.cryptoKey;
    }

    public void RandomizeCryptoKey()
    {
      Decimal num = this.InternalDecrypt();
      this.currentCryptoKey = (long) ThreadSafeRandom.Next();
      this.hiddenValue = ObscuredDecimal.InternalEncrypt(num, this.currentCryptoKey);
    }

    public Decimal GetEncrypted()
    {
      this.ApplyNewCryptoKey();
      return new ObscuredDecimal.DecimalLongBytesUnion()
      {
        b16 = this.hiddenValue
      }.d;
    }

    public void SetEncrypted(Decimal encrypted)
    {
      this.inited = true;
      this.hiddenValue = new ObscuredDecimal.DecimalLongBytesUnion()
      {
        d = encrypted
      }.b16;
      if (this.currentCryptoKey == 0L)
        this.currentCryptoKey = ObscuredDecimal.cryptoKey;
      if (ObscuredCheatingDetector.ExistsAndIsRunning)
      {
        this.fakeValueActive = false;
        this.fakeValue = this.InternalDecrypt();
        this.fakeValueActive = true;
      }
      else
        this.fakeValueActive = false;
    }

    public Decimal GetDecrypted() => this.InternalDecrypt();

    private Decimal InternalDecrypt()
    {
      if (!this.inited)
      {
        this.currentCryptoKey = ObscuredDecimal.cryptoKey;
        this.hiddenValue = ObscuredDecimal.InternalEncrypt(0M);
        this.fakeValue = 0M;
        this.fakeValueActive = false;
        this.inited = true;
        return 0M;
      }
      ObscuredDecimal.DecimalLongBytesUnion decimalLongBytesUnion = new ObscuredDecimal.DecimalLongBytesUnion();
      decimalLongBytesUnion.b16 = this.hiddenValue;
      decimalLongBytesUnion.l1 ^= this.currentCryptoKey;
      decimalLongBytesUnion.l2 ^= this.currentCryptoKey;
      Decimal d = decimalLongBytesUnion.d;
      if (ObscuredCheatingDetector.ExistsAndIsRunning && this.fakeValueActive && d != this.fakeValue)
        ObscuredCheatingDetector.Instance.OnCheatingDetected();
      return d;
    }

    public static implicit operator ObscuredDecimal(Decimal value) => new ObscuredDecimal(value);

    public static implicit operator Decimal(ObscuredDecimal value) => value.InternalDecrypt();

    public static explicit operator ObscuredDecimal(ObscuredFloat f)
    {
      return (ObscuredDecimal) (Decimal) (float) f;
    }

    public static ObscuredDecimal operator ++(ObscuredDecimal input)
    {
      Decimal num = input.InternalDecrypt() + 1M;
      input.hiddenValue = ObscuredDecimal.InternalEncrypt(num, input.currentCryptoKey);
      if (ObscuredCheatingDetector.ExistsAndIsRunning)
      {
        input.fakeValue = num;
        input.fakeValueActive = true;
      }
      else
        input.fakeValueActive = false;
      return input;
    }

    public static ObscuredDecimal operator --(ObscuredDecimal input)
    {
      Decimal num = input.InternalDecrypt() - 1M;
      input.hiddenValue = ObscuredDecimal.InternalEncrypt(num, input.currentCryptoKey);
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
      return obj is ObscuredDecimal obscuredDecimal && this.Equals(obscuredDecimal);
    }

    public bool Equals(ObscuredDecimal obj) => obj.InternalDecrypt().Equals(this.InternalDecrypt());

    public int CompareTo(ObscuredDecimal other)
    {
      return this.InternalDecrypt().CompareTo(other.InternalDecrypt());
    }

    public int CompareTo(Decimal other) => this.InternalDecrypt().CompareTo(other);

    public int CompareTo(object obj) => this.InternalDecrypt().CompareTo(obj);

    [StructLayout(LayoutKind.Explicit)]
    private struct DecimalLongBytesUnion
    {
      [FieldOffset(0)]
      public Decimal d;
      [FieldOffset(0)]
      public long l1;
      [FieldOffset(8)]
      public long l2;
      [FieldOffset(0)]
      public ACTkByte16 b16;
    }
  }
}
