// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.ObscuredTypes.ObscuredByte
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
  public struct ObscuredByte : 
    IFormattable,
    IEquatable<ObscuredByte>,
    IComparable<ObscuredByte>,
    IComparable<byte>,
    IComparable
  {
    private static byte cryptoKey = 244;
    private byte currentCryptoKey;
    private byte hiddenValue;
    private bool inited;
    private byte fakeValue;
    private bool fakeValueActive;

    private ObscuredByte(byte value)
    {
      this.currentCryptoKey = ObscuredByte.cryptoKey;
      this.hiddenValue = ObscuredByte.EncryptDecrypt(value);
      bool existsAndIsRunning = ObscuredCheatingDetector.ExistsAndIsRunning;
      this.fakeValue = !existsAndIsRunning ? (byte) 0 : value;
      this.fakeValueActive = existsAndIsRunning;
      this.inited = true;
    }

    public static void SetNewCryptoKey(byte newKey) => ObscuredByte.cryptoKey = newKey;

    public static byte EncryptDecrypt(byte value) => ObscuredByte.EncryptDecrypt(value, (byte) 0);

    public static void EncryptDecrypt(byte[] value) => ObscuredByte.EncryptDecrypt(value, (byte) 0);

    public static byte EncryptDecrypt(byte value, byte key)
    {
      return key == (byte) 0 ? (byte) ((uint) value ^ (uint) ObscuredByte.cryptoKey) : (byte) ((uint) value ^ (uint) key);
    }

    public static void EncryptDecrypt(byte[] value, byte key)
    {
      int length = value.Length;
      for (int index = 0; index < length; ++index)
        value[index] = key != (byte) 0 ? (byte) ((uint) value[index] ^ (uint) key) : (byte) ((uint) value[index] ^ (uint) ObscuredByte.cryptoKey);
    }

    public static ObscuredByte FromEncrypted(byte encrypted)
    {
      ObscuredByte obscuredByte = new ObscuredByte();
      obscuredByte.SetEncrypted(encrypted);
      return obscuredByte;
    }

    public void ApplyNewCryptoKey()
    {
      if ((int) this.currentCryptoKey == (int) ObscuredByte.cryptoKey)
        return;
      this.hiddenValue = ObscuredByte.EncryptDecrypt(this.InternalDecrypt(), ObscuredByte.cryptoKey);
      this.currentCryptoKey = ObscuredByte.cryptoKey;
    }

    public void RandomizeCryptoKey()
    {
      byte num = this.InternalDecrypt();
      this.currentCryptoKey = (byte) ThreadSafeRandom.Next((int) byte.MaxValue);
      this.hiddenValue = ObscuredByte.EncryptDecrypt(num, this.currentCryptoKey);
    }

    public byte GetEncrypted()
    {
      this.ApplyNewCryptoKey();
      return this.hiddenValue;
    }

    public void SetEncrypted(byte encrypted)
    {
      this.inited = true;
      this.hiddenValue = encrypted;
      if (this.currentCryptoKey == (byte) 0)
        this.currentCryptoKey = ObscuredByte.cryptoKey;
      if (ObscuredCheatingDetector.ExistsAndIsRunning)
      {
        this.fakeValueActive = false;
        this.fakeValue = this.InternalDecrypt();
        this.fakeValueActive = true;
      }
      else
        this.fakeValueActive = false;
    }

    public byte GetDecrypted() => this.InternalDecrypt();

    private byte InternalDecrypt()
    {
      if (!this.inited)
      {
        this.currentCryptoKey = ObscuredByte.cryptoKey;
        this.hiddenValue = ObscuredByte.EncryptDecrypt((byte) 0);
        this.fakeValue = (byte) 0;
        this.fakeValueActive = false;
        this.inited = true;
        return 0;
      }
      byte num = ObscuredByte.EncryptDecrypt(this.hiddenValue, this.currentCryptoKey);
      if (ObscuredCheatingDetector.ExistsAndIsRunning && this.fakeValueActive && (int) num != (int) this.fakeValue)
        ObscuredCheatingDetector.Instance.OnCheatingDetected();
      return num;
    }

    public static implicit operator ObscuredByte(byte value) => new ObscuredByte(value);

    public static implicit operator byte(ObscuredByte value) => value.InternalDecrypt();

    public static ObscuredByte operator ++(ObscuredByte input)
    {
      byte num = (byte) ((uint) input.InternalDecrypt() + 1U);
      input.hiddenValue = ObscuredByte.EncryptDecrypt(num, input.currentCryptoKey);
      if (ObscuredCheatingDetector.ExistsAndIsRunning)
      {
        input.fakeValue = num;
        input.fakeValueActive = true;
      }
      else
        input.fakeValueActive = false;
      return input;
    }

    public static ObscuredByte operator --(ObscuredByte input)
    {
      byte num = (byte) ((uint) input.InternalDecrypt() - 1U);
      input.hiddenValue = ObscuredByte.EncryptDecrypt(num, input.currentCryptoKey);
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
      return obj is ObscuredByte obscuredByte && this.Equals(obscuredByte);
    }

    public bool Equals(ObscuredByte obj)
    {
      return (int) this.currentCryptoKey == (int) obj.currentCryptoKey ? (int) this.hiddenValue == (int) obj.hiddenValue : (int) ObscuredByte.EncryptDecrypt(this.hiddenValue, this.currentCryptoKey) == (int) ObscuredByte.EncryptDecrypt(obj.hiddenValue, obj.currentCryptoKey);
    }

    public int CompareTo(ObscuredByte other)
    {
      return this.InternalDecrypt().CompareTo(other.InternalDecrypt());
    }

    public int CompareTo(byte other) => this.InternalDecrypt().CompareTo(other);

    public int CompareTo(object obj) => this.InternalDecrypt().CompareTo(obj);
  }
}
