// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.ObscuredTypes.ObscuredUShort
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using CodeStage.AntiCheat.Detectors;
using CodeStage.AntiCheat.Utils;
using System;

#nullable disable
namespace CodeStage.AntiCheat.ObscuredTypes
{
  [Serializable]
  public struct ObscuredUShort : 
    IFormattable,
    IEquatable<ObscuredUShort>,
    IComparable<ObscuredUShort>,
    IComparable<ushort>,
    IComparable
  {
    private static ushort cryptoKey = 224;
    private ushort currentCryptoKey;
    private ushort hiddenValue;
    private bool inited;
    private ushort fakeValue;
    private bool fakeValueActive;

    private ObscuredUShort(ushort value)
    {
      this.currentCryptoKey = ObscuredUShort.cryptoKey;
      this.hiddenValue = ObscuredUShort.EncryptDecrypt(value);
      bool existsAndIsRunning = ObscuredCheatingDetector.ExistsAndIsRunning;
      this.fakeValue = !existsAndIsRunning ? (ushort) 0 : value;
      this.fakeValueActive = existsAndIsRunning;
      this.inited = true;
    }

    public static void SetNewCryptoKey(ushort newKey) => ObscuredUShort.cryptoKey = newKey;

    public static ushort EncryptDecrypt(ushort value)
    {
      return ObscuredUShort.EncryptDecrypt(value, (ushort) 0);
    }

    public static ushort EncryptDecrypt(ushort value, ushort key)
    {
      return key == (ushort) 0 ? (ushort) ((uint) value ^ (uint) ObscuredUShort.cryptoKey) : (ushort) ((uint) value ^ (uint) key);
    }

    public static ObscuredUShort FromEncrypted(ushort encrypted)
    {
      ObscuredUShort obscuredUshort = new ObscuredUShort();
      obscuredUshort.SetEncrypted(encrypted);
      return obscuredUshort;
    }

    public void ApplyNewCryptoKey()
    {
      if ((int) this.currentCryptoKey == (int) ObscuredUShort.cryptoKey)
        return;
      this.hiddenValue = ObscuredUShort.EncryptDecrypt(this.InternalDecrypt(), ObscuredUShort.cryptoKey);
      this.currentCryptoKey = ObscuredUShort.cryptoKey;
    }

    public void RandomizeCryptoKey()
    {
      ushort num = this.InternalDecrypt();
      this.currentCryptoKey = (ushort) ThreadSafeRandom.Next((int) short.MaxValue);
      this.hiddenValue = ObscuredUShort.EncryptDecrypt(num, this.currentCryptoKey);
    }

    public ushort GetEncrypted()
    {
      this.ApplyNewCryptoKey();
      return this.hiddenValue;
    }

    public void SetEncrypted(ushort encrypted)
    {
      this.inited = true;
      this.hiddenValue = encrypted;
      if (this.currentCryptoKey == (ushort) 0)
        this.currentCryptoKey = ObscuredUShort.cryptoKey;
      if (ObscuredCheatingDetector.ExistsAndIsRunning)
      {
        this.fakeValueActive = false;
        this.fakeValue = this.InternalDecrypt();
        this.fakeValueActive = true;
      }
      else
        this.fakeValueActive = false;
    }

    public ushort GetDecrypted() => this.InternalDecrypt();

    private ushort InternalDecrypt()
    {
      if (!this.inited)
      {
        this.currentCryptoKey = ObscuredUShort.cryptoKey;
        this.hiddenValue = ObscuredUShort.EncryptDecrypt((ushort) 0);
        this.fakeValue = (ushort) 0;
        this.fakeValueActive = false;
        this.inited = true;
        return 0;
      }
      ushort num = ObscuredUShort.EncryptDecrypt(this.hiddenValue, this.currentCryptoKey);
      if (ObscuredCheatingDetector.ExistsAndIsRunning && this.fakeValueActive && (int) num != (int) this.fakeValue)
        ObscuredCheatingDetector.Instance.OnCheatingDetected();
      return num;
    }

    public static implicit operator ObscuredUShort(ushort value) => new ObscuredUShort(value);

    public static implicit operator ushort(ObscuredUShort value) => value.InternalDecrypt();

    public static ObscuredUShort operator ++(ObscuredUShort input)
    {
      ushort num = (ushort) ((uint) input.InternalDecrypt() + 1U);
      input.hiddenValue = ObscuredUShort.EncryptDecrypt(num, input.currentCryptoKey);
      if (ObscuredCheatingDetector.ExistsAndIsRunning)
      {
        input.fakeValue = num;
        input.fakeValueActive = true;
      }
      else
        input.fakeValueActive = false;
      return input;
    }

    public static ObscuredUShort operator --(ObscuredUShort input)
    {
      ushort num = (ushort) ((uint) input.InternalDecrypt() - 1U);
      input.hiddenValue = ObscuredUShort.EncryptDecrypt(num, input.currentCryptoKey);
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
      return obj is ObscuredUShort obscuredUshort && this.Equals(obscuredUshort);
    }

    public bool Equals(ObscuredUShort obj)
    {
      return (int) this.currentCryptoKey == (int) obj.currentCryptoKey ? (int) this.hiddenValue == (int) obj.hiddenValue : (int) ObscuredUShort.EncryptDecrypt(this.hiddenValue, this.currentCryptoKey) == (int) ObscuredUShort.EncryptDecrypt(obj.hiddenValue, obj.currentCryptoKey);
    }

    public int CompareTo(ObscuredUShort other)
    {
      return this.InternalDecrypt().CompareTo(other.InternalDecrypt());
    }

    public int CompareTo(ushort other) => this.InternalDecrypt().CompareTo(other);

    public int CompareTo(object obj) => this.InternalDecrypt().CompareTo(obj);
  }
}
