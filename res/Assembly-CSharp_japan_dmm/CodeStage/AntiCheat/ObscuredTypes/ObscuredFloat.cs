// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.ObscuredTypes.ObscuredFloat
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
  public struct ObscuredFloat : 
    IFormattable,
    IEquatable<ObscuredFloat>,
    IComparable<ObscuredFloat>,
    IComparable<float>,
    IComparable
  {
    private static int cryptoKey = 230887;
    [SerializeField]
    private int currentCryptoKey;
    [SerializeField]
    private int hiddenValue;
    [SerializeField]
    [FormerlySerializedAs("hiddenValue")]
    private ACTkByte4 hiddenValueOldByte4;
    [SerializeField]
    private bool inited;
    [SerializeField]
    private float fakeValue;
    [SerializeField]
    private bool fakeValueActive;

    private ObscuredFloat(float value)
    {
      this.currentCryptoKey = ObscuredFloat.cryptoKey;
      this.hiddenValue = ObscuredFloat.InternalEncrypt(value);
      this.hiddenValueOldByte4 = new ACTkByte4();
      bool existsAndIsRunning = ObscuredCheatingDetector.ExistsAndIsRunning;
      this.fakeValue = !existsAndIsRunning ? 0.0f : value;
      this.fakeValueActive = existsAndIsRunning;
      this.inited = true;
    }

    public static void SetNewCryptoKey(int newKey) => ObscuredFloat.cryptoKey = newKey;

    public static int Encrypt(float value) => ObscuredFloat.Encrypt(value, ObscuredFloat.cryptoKey);

    public static int Encrypt(float value, int key)
    {
      ObscuredFloat.FloatIntBytesUnion floatIntBytesUnion = new ObscuredFloat.FloatIntBytesUnion()
      {
        f = value
      };
      floatIntBytesUnion.i ^= key;
      floatIntBytesUnion.b4.Shuffle();
      return floatIntBytesUnion.i;
    }

    private static int InternalEncrypt(float value, int key = 0)
    {
      int key1 = key;
      if (key1 == 0)
        key1 = ObscuredFloat.cryptoKey;
      return ObscuredFloat.Encrypt(value, key1);
    }

    public static float Decrypt(int value) => ObscuredFloat.Decrypt(value, ObscuredFloat.cryptoKey);

    public static float Decrypt(int value, int key)
    {
      ObscuredFloat.FloatIntBytesUnion floatIntBytesUnion = new ObscuredFloat.FloatIntBytesUnion()
      {
        i = value
      };
      floatIntBytesUnion.b4.UnShuffle();
      floatIntBytesUnion.i ^= key;
      return floatIntBytesUnion.f;
    }

    public static int MigrateEncrypted(int encrypted, byte fromVersion = 0, byte toVersion = 2)
    {
      ObscuredFloat.FloatIntBytesUnion floatIntBytesUnion = new ObscuredFloat.FloatIntBytesUnion()
      {
        i = encrypted
      };
      if (fromVersion < (byte) 2 && toVersion == (byte) 2)
        floatIntBytesUnion.b4.Shuffle();
      return floatIntBytesUnion.i;
    }

    public static ObscuredFloat FromEncrypted(int encrypted)
    {
      ObscuredFloat obscuredFloat = new ObscuredFloat();
      obscuredFloat.SetEncrypted(encrypted);
      return obscuredFloat;
    }

    public void ApplyNewCryptoKey()
    {
      if (this.currentCryptoKey == ObscuredFloat.cryptoKey)
        return;
      this.hiddenValue = ObscuredFloat.InternalEncrypt(this.InternalDecrypt(), ObscuredFloat.cryptoKey);
      this.currentCryptoKey = ObscuredFloat.cryptoKey;
    }

    public void RandomizeCryptoKey()
    {
      float num = this.InternalDecrypt();
      this.currentCryptoKey = ThreadSafeRandom.Next(100000, 999999);
      this.hiddenValue = ObscuredFloat.InternalEncrypt(num, this.currentCryptoKey);
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
      if (this.currentCryptoKey == 0)
        this.currentCryptoKey = ObscuredFloat.cryptoKey;
      if (ObscuredCheatingDetector.ExistsAndIsRunning)
      {
        this.fakeValueActive = false;
        this.fakeValue = this.InternalDecrypt();
        this.fakeValueActive = true;
      }
      else
        this.fakeValueActive = false;
    }

    public float GetDecrypted() => this.InternalDecrypt();

    private float InternalDecrypt()
    {
      if (!this.inited)
      {
        this.currentCryptoKey = ObscuredFloat.cryptoKey;
        this.hiddenValue = ObscuredFloat.InternalEncrypt(0.0f);
        this.fakeValue = 0.0f;
        this.fakeValueActive = false;
        this.inited = true;
        return 0.0f;
      }
      float num = ObscuredFloat.Decrypt(this.hiddenValue, this.currentCryptoKey);
      if (ObscuredCheatingDetector.ExistsAndIsRunning && this.fakeValueActive && (double) Math.Abs(num - this.fakeValue) > (double) ObscuredCheatingDetector.Instance.floatEpsilon)
        ObscuredCheatingDetector.Instance.OnCheatingDetected();
      return num;
    }

    public static implicit operator ObscuredFloat(float value) => new ObscuredFloat(value);

    public static implicit operator float(ObscuredFloat value) => value.InternalDecrypt();

    public static ObscuredFloat operator ++(ObscuredFloat input)
    {
      float num = input.InternalDecrypt() + 1f;
      input.hiddenValue = ObscuredFloat.InternalEncrypt(num, input.currentCryptoKey);
      if (ObscuredCheatingDetector.ExistsAndIsRunning)
      {
        input.fakeValue = num;
        input.fakeValueActive = true;
      }
      else
        input.fakeValueActive = false;
      return input;
    }

    public static ObscuredFloat operator --(ObscuredFloat input)
    {
      float num = input.InternalDecrypt() - 1f;
      input.hiddenValue = ObscuredFloat.InternalEncrypt(num, input.currentCryptoKey);
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
      return obj is ObscuredFloat obscuredFloat && this.Equals(obscuredFloat);
    }

    public bool Equals(ObscuredFloat obj) => obj.InternalDecrypt().Equals(this.InternalDecrypt());

    public int CompareTo(ObscuredFloat other)
    {
      return this.InternalDecrypt().CompareTo(other.InternalDecrypt());
    }

    public int CompareTo(float other) => this.InternalDecrypt().CompareTo(other);

    public int CompareTo(object obj) => this.InternalDecrypt().CompareTo(obj);

    [StructLayout(LayoutKind.Explicit)]
    internal struct FloatIntBytesUnion
    {
      [FieldOffset(0)]
      public float f;
      [FieldOffset(0)]
      public int i;
      [FieldOffset(0)]
      public ACTkByte4 b4;
    }
  }
}
