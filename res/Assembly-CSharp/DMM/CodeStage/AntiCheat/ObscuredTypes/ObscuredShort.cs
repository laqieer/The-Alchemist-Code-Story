// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.ObscuredTypes.ObscuredShort
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
  public struct ObscuredShort : 
    IFormattable,
    IEquatable<ObscuredShort>,
    IComparable<ObscuredShort>,
    IComparable<short>,
    IComparable
  {
    private static short cryptoKey = 214;
    [SerializeField]
    private short currentCryptoKey;
    [SerializeField]
    private short hiddenValue;
    [SerializeField]
    private bool inited;
    [SerializeField]
    private short fakeValue;
    [SerializeField]
    private bool fakeValueActive;

    private ObscuredShort(short value)
    {
      this.currentCryptoKey = ObscuredShort.cryptoKey;
      this.hiddenValue = ObscuredShort.EncryptDecrypt(value);
      bool existsAndIsRunning = ObscuredCheatingDetector.ExistsAndIsRunning;
      this.fakeValue = !existsAndIsRunning ? (short) 0 : value;
      this.fakeValueActive = existsAndIsRunning;
      this.inited = true;
    }

    public static void SetNewCryptoKey(short newKey) => ObscuredShort.cryptoKey = newKey;

    public static short EncryptDecrypt(short value)
    {
      return ObscuredShort.EncryptDecrypt(value, (short) 0);
    }

    public static short EncryptDecrypt(short value, short key)
    {
      return key == (short) 0 ? (short) ((int) value ^ (int) ObscuredShort.cryptoKey) : (short) ((int) value ^ (int) key);
    }

    public static ObscuredShort FromEncrypted(short encrypted)
    {
      ObscuredShort obscuredShort = new ObscuredShort();
      obscuredShort.SetEncrypted(encrypted);
      return obscuredShort;
    }

    public void ApplyNewCryptoKey()
    {
      if ((int) this.currentCryptoKey == (int) ObscuredShort.cryptoKey)
        return;
      this.hiddenValue = ObscuredShort.EncryptDecrypt(this.InternalDecrypt(), ObscuredShort.cryptoKey);
      this.currentCryptoKey = ObscuredShort.cryptoKey;
    }

    public void RandomizeCryptoKey()
    {
      short num = this.InternalDecrypt();
      this.currentCryptoKey = (short) ThreadSafeRandom.Next((int) short.MaxValue);
      this.hiddenValue = ObscuredShort.EncryptDecrypt(num, this.currentCryptoKey);
    }

    public short GetEncrypted()
    {
      this.ApplyNewCryptoKey();
      return this.hiddenValue;
    }

    public void SetEncrypted(short encrypted)
    {
      this.inited = true;
      this.hiddenValue = encrypted;
      if (this.currentCryptoKey == (short) 0)
        this.currentCryptoKey = ObscuredShort.cryptoKey;
      if (ObscuredCheatingDetector.ExistsAndIsRunning)
      {
        this.fakeValueActive = false;
        this.fakeValue = this.InternalDecrypt();
        this.fakeValueActive = true;
      }
      else
        this.fakeValueActive = false;
    }

    public short GetDecrypted() => this.InternalDecrypt();

    private short InternalDecrypt()
    {
      if (!this.inited)
      {
        this.currentCryptoKey = ObscuredShort.cryptoKey;
        this.hiddenValue = ObscuredShort.EncryptDecrypt((short) 0);
        this.fakeValue = (short) 0;
        this.fakeValueActive = false;
        this.inited = true;
        return 0;
      }
      short num = ObscuredShort.EncryptDecrypt(this.hiddenValue, this.currentCryptoKey);
      if (ObscuredCheatingDetector.ExistsAndIsRunning && this.fakeValueActive && (int) num != (int) this.fakeValue)
        ObscuredCheatingDetector.Instance.OnCheatingDetected();
      return num;
    }

    public static implicit operator ObscuredShort(short value) => new ObscuredShort(value);

    public static implicit operator short(ObscuredShort value) => value.InternalDecrypt();

    public static ObscuredShort operator ++(ObscuredShort input)
    {
      short num = (short) ((int) input.InternalDecrypt() + 1);
      input.hiddenValue = ObscuredShort.EncryptDecrypt(num);
      if (ObscuredCheatingDetector.ExistsAndIsRunning)
      {
        input.fakeValue = num;
        input.fakeValueActive = true;
      }
      else
        input.fakeValueActive = false;
      return input;
    }

    public static ObscuredShort operator --(ObscuredShort input)
    {
      short num = (short) ((int) input.InternalDecrypt() - 1);
      input.hiddenValue = ObscuredShort.EncryptDecrypt(num);
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
      return obj is ObscuredShort obscuredShort && this.Equals(obscuredShort);
    }

    public bool Equals(ObscuredShort obj)
    {
      return (int) this.currentCryptoKey == (int) obj.currentCryptoKey ? (int) this.hiddenValue == (int) obj.hiddenValue : (int) ObscuredShort.EncryptDecrypt(this.hiddenValue, this.currentCryptoKey) == (int) ObscuredShort.EncryptDecrypt(obj.hiddenValue, obj.currentCryptoKey);
    }

    public int CompareTo(ObscuredShort other)
    {
      return this.InternalDecrypt().CompareTo(other.InternalDecrypt());
    }

    public int CompareTo(short other) => this.InternalDecrypt().CompareTo(other);

    public int CompareTo(object obj) => this.InternalDecrypt().CompareTo(obj);
  }
}
