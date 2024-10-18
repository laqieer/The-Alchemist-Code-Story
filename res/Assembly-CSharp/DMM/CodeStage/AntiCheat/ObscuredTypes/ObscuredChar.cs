// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.ObscuredTypes.ObscuredChar
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
  public struct ObscuredChar : 
    IEquatable<ObscuredChar>,
    IComparable<ObscuredChar>,
    IComparable<char>,
    IComparable
  {
    private static char cryptoKey = '—';
    private char currentCryptoKey;
    private char hiddenValue;
    private bool inited;
    private char fakeValue;
    private bool fakeValueActive;

    private ObscuredChar(char value)
    {
      this.currentCryptoKey = ObscuredChar.cryptoKey;
      this.hiddenValue = ObscuredChar.EncryptDecrypt(value);
      bool existsAndIsRunning = ObscuredCheatingDetector.ExistsAndIsRunning;
      this.fakeValue = !existsAndIsRunning ? char.MinValue : value;
      this.fakeValueActive = existsAndIsRunning;
      this.inited = true;
    }

    public static void SetNewCryptoKey(char newKey) => ObscuredChar.cryptoKey = newKey;

    public static char EncryptDecrypt(char value)
    {
      return ObscuredChar.EncryptDecrypt(value, char.MinValue);
    }

    public static char EncryptDecrypt(char value, char key)
    {
      return key == char.MinValue ? (char) ((uint) value ^ (uint) ObscuredChar.cryptoKey) : (char) ((uint) value ^ (uint) key);
    }

    public static ObscuredChar FromEncrypted(char encrypted)
    {
      ObscuredChar obscuredChar = new ObscuredChar();
      obscuredChar.SetEncrypted(encrypted);
      return obscuredChar;
    }

    public void ApplyNewCryptoKey()
    {
      if ((int) this.currentCryptoKey == (int) ObscuredChar.cryptoKey)
        return;
      this.hiddenValue = ObscuredChar.EncryptDecrypt(this.InternalDecrypt(), ObscuredChar.cryptoKey);
      this.currentCryptoKey = ObscuredChar.cryptoKey;
    }

    public void RandomizeCryptoKey()
    {
      char ch = this.InternalDecrypt();
      this.currentCryptoKey = (char) ThreadSafeRandom.Next((int) ushort.MaxValue);
      this.hiddenValue = ObscuredChar.EncryptDecrypt(ch, this.currentCryptoKey);
    }

    public char GetEncrypted()
    {
      this.ApplyNewCryptoKey();
      return this.hiddenValue;
    }

    public void SetEncrypted(char encrypted)
    {
      this.inited = true;
      this.hiddenValue = encrypted;
      if (this.currentCryptoKey == char.MinValue)
        this.currentCryptoKey = ObscuredChar.cryptoKey;
      if (ObscuredCheatingDetector.ExistsAndIsRunning)
      {
        this.fakeValueActive = false;
        this.fakeValue = this.InternalDecrypt();
        this.fakeValueActive = true;
      }
      else
        this.fakeValueActive = false;
    }

    public char GetDecrypted() => this.InternalDecrypt();

    private char InternalDecrypt()
    {
      if (!this.inited)
      {
        this.currentCryptoKey = ObscuredChar.cryptoKey;
        this.hiddenValue = ObscuredChar.EncryptDecrypt(char.MinValue);
        this.fakeValue = char.MinValue;
        this.fakeValueActive = false;
        this.inited = true;
        return char.MinValue;
      }
      char ch = ObscuredChar.EncryptDecrypt(this.hiddenValue, this.currentCryptoKey);
      if (ObscuredCheatingDetector.ExistsAndIsRunning && this.fakeValueActive && (int) ch != (int) this.fakeValue)
        ObscuredCheatingDetector.Instance.OnCheatingDetected();
      return ch;
    }

    public static implicit operator ObscuredChar(char value) => new ObscuredChar(value);

    public static implicit operator char(ObscuredChar value) => value.InternalDecrypt();

    public static ObscuredChar operator ++(ObscuredChar input)
    {
      char ch = (char) ((uint) input.InternalDecrypt() + 1U);
      input.hiddenValue = ObscuredChar.EncryptDecrypt(ch, input.currentCryptoKey);
      if (ObscuredCheatingDetector.ExistsAndIsRunning)
      {
        input.fakeValue = ch;
        input.fakeValueActive = true;
      }
      else
        input.fakeValueActive = false;
      return input;
    }

    public static ObscuredChar operator --(ObscuredChar input)
    {
      char ch = (char) ((uint) input.InternalDecrypt() - 1U);
      input.hiddenValue = ObscuredChar.EncryptDecrypt(ch, input.currentCryptoKey);
      if (ObscuredCheatingDetector.ExistsAndIsRunning)
      {
        input.fakeValue = ch;
        input.fakeValueActive = true;
      }
      else
        input.fakeValueActive = false;
      return input;
    }

    public override int GetHashCode() => this.InternalDecrypt().GetHashCode();

    public override string ToString() => this.InternalDecrypt().ToString();

    public string ToString(IFormatProvider provider) => this.InternalDecrypt().ToString(provider);

    public override bool Equals(object obj)
    {
      return obj is ObscuredChar obscuredChar && this.Equals(obscuredChar);
    }

    public bool Equals(ObscuredChar obj)
    {
      return (int) this.currentCryptoKey == (int) obj.currentCryptoKey ? (int) this.hiddenValue == (int) obj.hiddenValue : (int) ObscuredChar.EncryptDecrypt(this.hiddenValue, this.currentCryptoKey) == (int) ObscuredChar.EncryptDecrypt(obj.hiddenValue, obj.currentCryptoKey);
    }

    public int CompareTo(ObscuredChar other)
    {
      return this.InternalDecrypt().CompareTo(other.InternalDecrypt());
    }

    public int CompareTo(char other) => this.InternalDecrypt().CompareTo(other);

    public int CompareTo(object obj) => this.InternalDecrypt().CompareTo(obj);
  }
}
