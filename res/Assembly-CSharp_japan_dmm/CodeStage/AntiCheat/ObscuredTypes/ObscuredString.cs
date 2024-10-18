// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.ObscuredTypes.ObscuredString
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
  public sealed class ObscuredString : IComparable<ObscuredString>, IComparable<string>, IComparable
  {
    private static string cryptoKey = "4441";
    [SerializeField]
    private string currentCryptoKey;
    [SerializeField]
    private byte[] hiddenValue;
    [SerializeField]
    private bool inited;
    [SerializeField]
    private string fakeValue;
    [SerializeField]
    private bool fakeValueActive;

    private ObscuredString()
    {
    }

    private ObscuredString(string value)
    {
      this.currentCryptoKey = ObscuredString.cryptoKey;
      this.hiddenValue = ObscuredString.InternalEncrypt(value);
      bool existsAndIsRunning = ObscuredCheatingDetector.ExistsAndIsRunning;
      this.fakeValue = !existsAndIsRunning ? (string) null : value;
      this.fakeValueActive = existsAndIsRunning;
      this.inited = true;
    }

    public static void SetNewCryptoKey(string newKey) => ObscuredString.cryptoKey = newKey;

    public static string EncryptDecrypt(string value)
    {
      return ObscuredString.EncryptDecrypt(value, string.Empty);
    }

    public static string EncryptDecrypt(string value, string key)
    {
      if (string.IsNullOrEmpty(value))
        return string.Empty;
      if (string.IsNullOrEmpty(key))
        key = ObscuredString.cryptoKey;
      int length1 = key.Length;
      int length2 = value.Length;
      char[] chArray = new char[length2];
      for (int index = 0; index < length2; ++index)
        chArray[index] = (char) ((uint) value[index] ^ (uint) key[index % length1]);
      return new string(chArray);
    }

    public static ObscuredString FromEncrypted(string encrypted)
    {
      ObscuredString obscuredString = new ObscuredString();
      obscuredString.SetEncrypted(encrypted);
      return obscuredString;
    }

    public void ApplyNewCryptoKey()
    {
      if (!(this.currentCryptoKey != ObscuredString.cryptoKey))
        return;
      this.hiddenValue = ObscuredString.InternalEncrypt(this.InternalDecrypt());
      this.currentCryptoKey = ObscuredString.cryptoKey;
    }

    public void RandomizeCryptoKey()
    {
      string str = this.InternalDecrypt();
      this.currentCryptoKey = ThreadSafeRandom.Next().ToString();
      this.hiddenValue = ObscuredString.InternalEncrypt(str, this.currentCryptoKey);
    }

    public string GetEncrypted()
    {
      this.ApplyNewCryptoKey();
      return ObscuredString.GetString(this.hiddenValue);
    }

    public void SetEncrypted(string encrypted)
    {
      this.inited = true;
      this.hiddenValue = ObscuredString.GetBytes(encrypted);
      if (string.IsNullOrEmpty(this.currentCryptoKey))
        this.currentCryptoKey = ObscuredString.cryptoKey;
      if (ObscuredCheatingDetector.ExistsAndIsRunning)
      {
        this.fakeValueActive = false;
        this.fakeValue = this.InternalDecrypt();
        this.fakeValueActive = true;
      }
      else
        this.fakeValueActive = false;
    }

    public string GetDecrypted() => this.InternalDecrypt();

    private static byte[] InternalEncrypt(string value)
    {
      return ObscuredString.InternalEncrypt(value, ObscuredString.cryptoKey);
    }

    private static byte[] InternalEncrypt(string value, string key)
    {
      return ObscuredString.GetBytes(ObscuredString.EncryptDecrypt(value, key));
    }

    private string InternalDecrypt()
    {
      if (!this.inited)
      {
        this.currentCryptoKey = ObscuredString.cryptoKey;
        this.hiddenValue = ObscuredString.InternalEncrypt(string.Empty);
        this.fakeValue = string.Empty;
        this.fakeValueActive = false;
        this.inited = true;
        return string.Empty;
      }
      string key = this.currentCryptoKey;
      if (string.IsNullOrEmpty(key))
        key = ObscuredString.cryptoKey;
      string str = ObscuredString.EncryptDecrypt(ObscuredString.GetString(this.hiddenValue), key);
      if (ObscuredCheatingDetector.ExistsAndIsRunning && this.fakeValueActive && str != this.fakeValue)
        ObscuredCheatingDetector.Instance.OnCheatingDetected();
      return str;
    }

    public int Length => this.hiddenValue.Length / 2;

    public static implicit operator ObscuredString(string value)
    {
      return value == null ? (ObscuredString) null : new ObscuredString(value);
    }

    public static implicit operator string(ObscuredString value)
    {
      return value == (ObscuredString) null ? (string) null : value.InternalDecrypt();
    }

    public static bool operator ==(ObscuredString a, ObscuredString b)
    {
      if (object.ReferenceEquals((object) a, (object) b))
        return true;
      if ((object) a == null || (object) b == null)
        return false;
      return a.currentCryptoKey == b.currentCryptoKey ? ObscuredString.ArraysEquals(a.hiddenValue, b.hiddenValue) : string.Equals(a.InternalDecrypt(), b.InternalDecrypt());
    }

    public static bool operator !=(ObscuredString a, ObscuredString b) => !(a == b);

    public override int GetHashCode() => this.InternalDecrypt().GetHashCode();

    public override string ToString() => this.InternalDecrypt();

    public override bool Equals(object obj)
    {
      return (object) (obj as ObscuredString) != null && this.Equals((ObscuredString) obj);
    }

    public bool Equals(ObscuredString value)
    {
      if (value == (ObscuredString) null)
        return false;
      return this.currentCryptoKey == value.currentCryptoKey ? ObscuredString.ArraysEquals(this.hiddenValue, value.hiddenValue) : string.Equals(this.InternalDecrypt(), value.InternalDecrypt());
    }

    public bool Equals(ObscuredString value, StringComparison comparisonType)
    {
      return !(value == (ObscuredString) null) && string.Equals(this.InternalDecrypt(), value.InternalDecrypt(), comparisonType);
    }

    public int CompareTo(ObscuredString other)
    {
      return this.InternalDecrypt().CompareTo(other.InternalDecrypt());
    }

    public int CompareTo(string other) => this.InternalDecrypt().CompareTo(other);

    public int CompareTo(object obj) => this.InternalDecrypt().CompareTo(obj);

    private static byte[] GetBytes(string str)
    {
      byte[] dst = new byte[str.Length * 2];
      Buffer.BlockCopy((Array) str.ToCharArray(), 0, (Array) dst, 0, dst.Length);
      return dst;
    }

    private static string GetString(byte[] bytes)
    {
      char[] dst = new char[bytes.Length / 2];
      Buffer.BlockCopy((Array) bytes, 0, (Array) dst, 0, bytes.Length);
      return new string(dst);
    }

    private static bool ArraysEquals(byte[] a1, byte[] a2)
    {
      if (a1 == a2)
        return true;
      if (a1 == null || a2 == null || a1.Length != a2.Length)
        return false;
      for (int index = 0; index < a1.Length; ++index)
      {
        if ((int) a1[index] != (int) a2[index])
          return false;
      }
      return true;
    }
  }
}
