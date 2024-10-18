// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.ObscuredTypes.ObscuredDouble
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using CodeStage.AntiCheat.Common;
using CodeStage.AntiCheat.Detectors;
using CodeStage.AntiCheat.Utils;
using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Serialization;

#nullable disable
namespace CodeStage.AntiCheat.ObscuredTypes
{
  [Serializable]
  public struct ObscuredDouble : 
    IFormattable,
    IEquatable<ObscuredDouble>,
    IComparable<ObscuredDouble>,
    IComparable<double>,
    IComparable
  {
    private static long cryptoKey = 210987;
    [SerializeField]
    private long currentCryptoKey;
    [SerializeField]
    private long hiddenValue;
    [SerializeField]
    [FormerlySerializedAs("hiddenValue")]
    private ACTkByte8 hiddenValueOldByte8;
    [SerializeField]
    private bool inited;
    [SerializeField]
    private double fakeValue;
    [SerializeField]
    private bool fakeValueActive;

    private ObscuredDouble(double value)
    {
      this.currentCryptoKey = ObscuredDouble.cryptoKey;
      this.hiddenValue = ObscuredDouble.InternalEncrypt(value);
      this.hiddenValueOldByte8 = new ACTkByte8();
      bool existsAndIsRunning = ObscuredCheatingDetector.ExistsAndIsRunning;
      this.fakeValue = !existsAndIsRunning ? 0.0 : value;
      this.fakeValueActive = existsAndIsRunning;
      this.inited = true;
    }

    public static void SetNewCryptoKey(long newKey) => ObscuredDouble.cryptoKey = newKey;

    public static long Encrypt(double value)
    {
      return ObscuredDouble.Encrypt(value, ObscuredDouble.cryptoKey);
    }

    public static long Encrypt(double value, long key)
    {
      ObscuredDouble.DoubleLongBytesUnion doubleLongBytesUnion = new ObscuredDouble.DoubleLongBytesUnion()
      {
        d = value
      };
      doubleLongBytesUnion.l ^= key;
      doubleLongBytesUnion.b8.Shuffle();
      return doubleLongBytesUnion.l;
    }

    private static long InternalEncrypt(double value, long key = 0)
    {
      long key1 = key;
      if (key1 == 0L)
        key1 = ObscuredDouble.cryptoKey;
      return ObscuredDouble.Encrypt(value, key1);
    }

    public static double Decrypt(long value)
    {
      return ObscuredDouble.Decrypt(value, ObscuredDouble.cryptoKey);
    }

    public static double Decrypt(long value, long key)
    {
      ObscuredDouble.DoubleLongBytesUnion doubleLongBytesUnion = new ObscuredDouble.DoubleLongBytesUnion()
      {
        l = value
      };
      doubleLongBytesUnion.b8.UnShuffle();
      doubleLongBytesUnion.l ^= key;
      return doubleLongBytesUnion.d;
    }

    public static long MigrateEncrypted(long encrypted, byte fromVersion = 0, byte toVersion = 2)
    {
      ObscuredDouble.DoubleLongBytesUnion doubleLongBytesUnion = new ObscuredDouble.DoubleLongBytesUnion()
      {
        l = encrypted
      };
      if (fromVersion < (byte) 2 && toVersion == (byte) 2)
        doubleLongBytesUnion.b8.Shuffle();
      return doubleLongBytesUnion.l;
    }

    public static ObscuredDouble FromEncrypted(long encrypted)
    {
      ObscuredDouble obscuredDouble = new ObscuredDouble();
      obscuredDouble.SetEncrypted(encrypted);
      return obscuredDouble;
    }

    public void ApplyNewCryptoKey()
    {
      if (this.currentCryptoKey == ObscuredDouble.cryptoKey)
        return;
      this.hiddenValue = ObscuredDouble.InternalEncrypt(this.InternalDecrypt(), ObscuredDouble.cryptoKey);
      this.currentCryptoKey = ObscuredDouble.cryptoKey;
    }

    public void RandomizeCryptoKey()
    {
      double num = this.InternalDecrypt();
      this.currentCryptoKey = (long) ThreadSafeRandom.Next(100000, 999999);
      this.hiddenValue = ObscuredDouble.InternalEncrypt(num, this.currentCryptoKey);
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
        this.currentCryptoKey = ObscuredDouble.cryptoKey;
      if (ObscuredCheatingDetector.ExistsAndIsRunning)
      {
        this.fakeValueActive = false;
        this.fakeValue = this.InternalDecrypt();
        this.fakeValueActive = true;
      }
      else
        this.fakeValueActive = false;
    }

    public double GetDecrypted() => this.InternalDecrypt();

    private double InternalDecrypt()
    {
      if (!this.inited)
      {
        this.currentCryptoKey = ObscuredDouble.cryptoKey;
        this.hiddenValue = ObscuredDouble.InternalEncrypt(0.0);
        this.fakeValue = 0.0;
        this.fakeValueActive = false;
        this.inited = true;
        return 0.0;
      }
      double num = ObscuredDouble.Decrypt(this.hiddenValue, this.currentCryptoKey);
      if (ObscuredCheatingDetector.ExistsAndIsRunning && this.fakeValueActive && Math.Abs(num - this.fakeValue) > ObscuredCheatingDetector.Instance.doubleEpsilon)
        ObscuredCheatingDetector.Instance.OnCheatingDetected();
      return num;
    }

    public static implicit operator ObscuredDouble(double value) => new ObscuredDouble(value);

    public static implicit operator double(ObscuredDouble value) => value.InternalDecrypt();

    public static explicit operator ObscuredDouble(ObscuredFloat f)
    {
      return (ObscuredDouble) (double) (float) f;
    }

    public static ObscuredDouble operator ++(ObscuredDouble input)
    {
      double num = input.InternalDecrypt() + 1.0;
      input.hiddenValue = ObscuredDouble.InternalEncrypt(num, input.currentCryptoKey);
      if (ObscuredCheatingDetector.ExistsAndIsRunning)
      {
        input.fakeValue = num;
        input.fakeValueActive = true;
      }
      else
        input.fakeValueActive = false;
      return input;
    }

    public static ObscuredDouble operator --(ObscuredDouble input)
    {
      double num = input.InternalDecrypt() - 1.0;
      input.hiddenValue = ObscuredDouble.InternalEncrypt(num, input.currentCryptoKey);
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
      return obj is ObscuredDouble obscuredDouble && this.Equals(obscuredDouble);
    }

    public bool Equals(ObscuredDouble obj) => obj.InternalDecrypt().Equals(this.InternalDecrypt());

    public int CompareTo(ObscuredDouble other)
    {
      return this.InternalDecrypt().CompareTo(other.InternalDecrypt());
    }

    public int CompareTo(double other) => this.InternalDecrypt().CompareTo(other);

    public int CompareTo(object obj) => this.InternalDecrypt().CompareTo(obj);

    [StructLayout(LayoutKind.Explicit)]
    private struct DoubleLongBytesUnion
    {
      [FieldOffset(0)]
      public double d;
      [FieldOffset(0)]
      public long l;
      [FieldOffset(0)]
      public ACTkByte8 b8;
    }
  }
}
