// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.ObscuredTypes.ObscuredInt
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
  public struct ObscuredInt : 
    IFormattable,
    IEquatable<ObscuredInt>,
    IComparable<ObscuredInt>,
    IComparable<int>,
    IComparable
  {
    private static int cryptoKey = 444444;
    [SerializeField]
    private int currentCryptoKey;
    [SerializeField]
    private int hiddenValue;
    [SerializeField]
    private bool inited;
    [SerializeField]
    private int fakeValue;
    [SerializeField]
    private bool fakeValueActive;

    private ObscuredInt(int value)
    {
      this.currentCryptoKey = ObscuredInt.cryptoKey;
      this.hiddenValue = ObscuredInt.Encrypt(value);
      bool existsAndIsRunning = ObscuredCheatingDetector.ExistsAndIsRunning;
      this.fakeValue = !existsAndIsRunning ? 0 : value;
      this.fakeValueActive = existsAndIsRunning;
      this.inited = true;
    }

    public static void SetNewCryptoKey(int newKey) => ObscuredInt.cryptoKey = newKey;

    public static int Encrypt(int value) => ObscuredInt.Encrypt(value, 0);

    public static int Encrypt(int value, int key)
    {
      return key == 0 ? value ^ ObscuredInt.cryptoKey : value ^ key;
    }

    public static int Decrypt(int value) => ObscuredInt.Decrypt(value, 0);

    public static int Decrypt(int value, int key)
    {
      return key == 0 ? value ^ ObscuredInt.cryptoKey : value ^ key;
    }

    public static ObscuredInt FromEncrypted(int encrypted)
    {
      ObscuredInt obscuredInt = new ObscuredInt();
      obscuredInt.SetEncrypted(encrypted);
      return obscuredInt;
    }

    public void ApplyNewCryptoKey()
    {
      if (this.currentCryptoKey == ObscuredInt.cryptoKey)
        return;
      this.hiddenValue = ObscuredInt.Encrypt(this.InternalDecrypt(), ObscuredInt.cryptoKey);
      this.currentCryptoKey = ObscuredInt.cryptoKey;
    }

    public void RandomizeCryptoKey()
    {
      this.hiddenValue = this.InternalDecrypt();
      this.currentCryptoKey = ThreadSafeRandom.Next();
      this.hiddenValue = ObscuredInt.Encrypt(this.hiddenValue, this.currentCryptoKey);
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
        this.currentCryptoKey = ObscuredInt.cryptoKey;
      if (ObscuredCheatingDetector.ExistsAndIsRunning)
      {
        this.fakeValueActive = false;
        this.fakeValue = this.InternalDecrypt();
        this.fakeValueActive = true;
      }
      else
        this.fakeValueActive = false;
    }

    public int GetDecrypted() => this.InternalDecrypt();

    private int InternalDecrypt()
    {
      if (!this.inited)
      {
        this.currentCryptoKey = ObscuredInt.cryptoKey;
        this.hiddenValue = ObscuredInt.Encrypt(0);
        this.fakeValue = 0;
        this.fakeValueActive = false;
        this.inited = true;
        return 0;
      }
      int num = ObscuredInt.Decrypt(this.hiddenValue, this.currentCryptoKey);
      if (ObscuredCheatingDetector.ExistsAndIsRunning && this.fakeValueActive && num != this.fakeValue)
        ObscuredCheatingDetector.Instance.OnCheatingDetected();
      return num;
    }

    public static implicit operator ObscuredInt(int value) => new ObscuredInt(value);

    public static implicit operator int(ObscuredInt value) => value.InternalDecrypt();

    public static implicit operator ObscuredFloat(ObscuredInt value)
    {
      return (ObscuredFloat) (float) value.InternalDecrypt();
    }

    public static implicit operator ObscuredDouble(ObscuredInt value)
    {
      return (ObscuredDouble) (double) value.InternalDecrypt();
    }

    public static explicit operator ObscuredUInt(ObscuredInt value)
    {
      return (ObscuredUInt) (uint) value.InternalDecrypt();
    }

    public static ObscuredInt operator ++(ObscuredInt input)
    {
      int num = input.InternalDecrypt() + 1;
      input.hiddenValue = ObscuredInt.Encrypt(num, input.currentCryptoKey);
      if (ObscuredCheatingDetector.ExistsAndIsRunning)
      {
        input.fakeValue = num;
        input.fakeValueActive = true;
      }
      else
        input.fakeValueActive = false;
      return input;
    }

    public static ObscuredInt operator --(ObscuredInt input)
    {
      int num = input.InternalDecrypt() - 1;
      input.hiddenValue = ObscuredInt.Encrypt(num, input.currentCryptoKey);
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
      return obj is ObscuredInt obscuredInt && this.Equals(obscuredInt);
    }

    public bool Equals(ObscuredInt obj)
    {
      return this.currentCryptoKey == obj.currentCryptoKey ? this.hiddenValue == obj.hiddenValue : ObscuredInt.Decrypt(this.hiddenValue, this.currentCryptoKey) == ObscuredInt.Decrypt(obj.hiddenValue, obj.currentCryptoKey);
    }

    public int CompareTo(ObscuredInt other)
    {
      return this.InternalDecrypt().CompareTo(other.InternalDecrypt());
    }

    public int CompareTo(int other) => this.InternalDecrypt().CompareTo(other);

    public int CompareTo(object obj) => this.InternalDecrypt().CompareTo(obj);
  }
}
