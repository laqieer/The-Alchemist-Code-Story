// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.ObscuredTypes.ObscuredSByte
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
  public struct ObscuredSByte : 
    IFormattable,
    IEquatable<ObscuredSByte>,
    IComparable<ObscuredSByte>,
    IComparable<sbyte>,
    IComparable
  {
    private static sbyte cryptoKey = 112;
    private sbyte currentCryptoKey;
    private sbyte hiddenValue;
    private bool inited;
    private sbyte fakeValue;
    private bool fakeValueActive;

    private ObscuredSByte(sbyte value)
    {
      this.currentCryptoKey = ObscuredSByte.cryptoKey;
      this.hiddenValue = ObscuredSByte.EncryptDecrypt(value);
      bool existsAndIsRunning = ObscuredCheatingDetector.ExistsAndIsRunning;
      this.fakeValue = !existsAndIsRunning ? (sbyte) 0 : value;
      this.fakeValueActive = existsAndIsRunning;
      this.inited = true;
    }

    public static void SetNewCryptoKey(sbyte newKey) => ObscuredSByte.cryptoKey = newKey;

    public static sbyte EncryptDecrypt(sbyte value)
    {
      return ObscuredSByte.EncryptDecrypt(value, (sbyte) 0);
    }

    public static sbyte EncryptDecrypt(sbyte value, sbyte key)
    {
      return key == (sbyte) 0 ? (sbyte) ((int) value ^ (int) ObscuredSByte.cryptoKey) : (sbyte) ((int) value ^ (int) key);
    }

    public static ObscuredSByte FromEncrypted(sbyte encrypted)
    {
      ObscuredSByte obscuredSbyte = new ObscuredSByte();
      obscuredSbyte.SetEncrypted(encrypted);
      return obscuredSbyte;
    }

    public void ApplyNewCryptoKey()
    {
      if ((int) this.currentCryptoKey == (int) ObscuredSByte.cryptoKey)
        return;
      this.hiddenValue = ObscuredSByte.EncryptDecrypt(this.InternalDecrypt(), ObscuredSByte.cryptoKey);
      this.currentCryptoKey = ObscuredSByte.cryptoKey;
    }

    public void RandomizeCryptoKey()
    {
      sbyte num = this.InternalDecrypt();
      this.currentCryptoKey = (sbyte) ThreadSafeRandom.Next((int) sbyte.MaxValue);
      this.hiddenValue = ObscuredSByte.EncryptDecrypt(num, this.currentCryptoKey);
    }

    public sbyte GetEncrypted()
    {
      this.ApplyNewCryptoKey();
      return this.hiddenValue;
    }

    public void SetEncrypted(sbyte encrypted)
    {
      this.inited = true;
      this.hiddenValue = encrypted;
      if (this.currentCryptoKey == (sbyte) 0)
        this.currentCryptoKey = ObscuredSByte.cryptoKey;
      if (ObscuredCheatingDetector.ExistsAndIsRunning)
      {
        this.fakeValueActive = false;
        this.fakeValue = this.InternalDecrypt();
        this.fakeValueActive = true;
      }
      else
        this.fakeValueActive = false;
    }

    public sbyte GetDecrypted() => this.InternalDecrypt();

    private sbyte InternalDecrypt()
    {
      if (!this.inited)
      {
        this.currentCryptoKey = ObscuredSByte.cryptoKey;
        this.hiddenValue = ObscuredSByte.EncryptDecrypt((sbyte) 0);
        this.fakeValue = (sbyte) 0;
        this.fakeValueActive = false;
        this.inited = true;
        return 0;
      }
      sbyte num = ObscuredSByte.EncryptDecrypt(this.hiddenValue, this.currentCryptoKey);
      if (ObscuredCheatingDetector.ExistsAndIsRunning && this.fakeValueActive && (int) num != (int) this.fakeValue)
        ObscuredCheatingDetector.Instance.OnCheatingDetected();
      return num;
    }

    public static implicit operator ObscuredSByte(sbyte value) => new ObscuredSByte(value);

    public static implicit operator sbyte(ObscuredSByte value) => value.InternalDecrypt();

    public static ObscuredSByte operator ++(ObscuredSByte input)
    {
      sbyte num = (sbyte) ((int) input.InternalDecrypt() + 1);
      input.hiddenValue = ObscuredSByte.EncryptDecrypt(num, input.currentCryptoKey);
      if (ObscuredCheatingDetector.ExistsAndIsRunning)
      {
        input.fakeValue = num;
        input.fakeValueActive = true;
      }
      else
        input.fakeValueActive = false;
      return input;
    }

    public static ObscuredSByte operator --(ObscuredSByte input)
    {
      sbyte num = (sbyte) ((int) input.InternalDecrypt() - 1);
      input.hiddenValue = ObscuredSByte.EncryptDecrypt(num, input.currentCryptoKey);
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
      return obj is ObscuredSByte obscuredSbyte && this.Equals(obscuredSbyte);
    }

    public bool Equals(ObscuredSByte obj)
    {
      return (int) this.currentCryptoKey == (int) obj.currentCryptoKey ? (int) this.hiddenValue == (int) obj.hiddenValue : (int) ObscuredSByte.EncryptDecrypt(this.hiddenValue, this.currentCryptoKey) == (int) ObscuredSByte.EncryptDecrypt(obj.hiddenValue, obj.currentCryptoKey);
    }

    public int CompareTo(ObscuredSByte other)
    {
      return this.InternalDecrypt().CompareTo(other.InternalDecrypt());
    }

    public int CompareTo(sbyte other) => this.InternalDecrypt().CompareTo(other);

    public int CompareTo(object obj) => this.InternalDecrypt().CompareTo(obj);
  }
}
