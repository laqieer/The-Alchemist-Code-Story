// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.ObscuredTypes.ObscuredPrefs
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using CodeStage.AntiCheat.Utils;
using System;
using System.Text;
using UnityEngine;

#nullable disable
namespace CodeStage.AntiCheat.ObscuredTypes
{
  public static class ObscuredPrefs
  {
    private const byte Version = 2;
    private const string RawNotFound = "{not_found}";
    private const string DataSeparator = "|";
    private static bool alterationReported;
    private static bool foreignSavesReported;
    private static string cryptoKey = "e806f6";
    private static string deviceId;
    private static uint deviceIdHash;
    [Obsolete("Please use OnAlterationDetected event instead.")]
    public static Action onAlterationDetected;
    [Obsolete("Please use OnPossibleForeignSavesDetected event instead.")]
    public static Action onPossibleForeignSavesDetected;
    public static bool preservePlayerPrefs;
    public static ObscuredPrefs.DeviceLockLevel lockToDevice;
    public static bool readForeignSaves;
    public static bool emergencyMode;
    private const char DEPRECATED_RAW_SEPARATOR = ':';
    private static string deprecatedDeviceId;

    public static string CryptoKey
    {
      set => ObscuredPrefs.cryptoKey = value;
      get => ObscuredPrefs.cryptoKey;
    }

    public static string DeviceId
    {
      get
      {
        if (string.IsNullOrEmpty(ObscuredPrefs.deviceId))
          ObscuredPrefs.deviceId = ObscuredPrefs.GetDeviceId();
        return ObscuredPrefs.deviceId;
      }
      set => ObscuredPrefs.deviceId = value;
    }

    [Obsolete("This property is obsolete, please use DeviceId instead.")]
    internal static string DeviceID
    {
      get => ObscuredPrefs.DeviceId;
      set => ObscuredPrefs.DeviceId = value;
    }

    private static uint DeviceIdHash
    {
      get
      {
        if (ObscuredPrefs.deviceIdHash == 0U)
          ObscuredPrefs.deviceIdHash = ObscuredPrefs.CalculateChecksum(ObscuredPrefs.DeviceId);
        return ObscuredPrefs.deviceIdHash;
      }
    }

    public static event Action OnAlterationDetected;

    public static event Action OnPossibleForeignSavesDetected;

    public static void ForceLockToDeviceInit()
    {
      if (string.IsNullOrEmpty(ObscuredPrefs.deviceId))
      {
        ObscuredPrefs.deviceId = ObscuredPrefs.GetDeviceId();
        ObscuredPrefs.deviceIdHash = ObscuredPrefs.CalculateChecksum(ObscuredPrefs.deviceId);
      }
      else
        Debug.LogWarning((object) "[ACTk] ObscuredPrefs.ForceLockToDeviceInit() is called, but device ID is already obtained!");
    }

    [Obsolete("This method is obsolete, use property CryptoKey instead")]
    internal static void SetNewCryptoKey(string newKey) => ObscuredPrefs.CryptoKey = newKey;

    public static void SetInt(string key, int value)
    {
      PlayerPrefs.SetString(ObscuredPrefs.EncryptKey(key), ObscuredPrefs.EncryptIntValue(key, value));
    }

    public static int GetInt(string key) => ObscuredPrefs.GetInt(key, 0);

    public static int GetInt(string key, int defaultValue)
    {
      string encryptedKey = ObscuredPrefs.EncryptKey(key);
      if (!PlayerPrefs.HasKey(encryptedKey) && PlayerPrefs.HasKey(key))
      {
        int num = PlayerPrefs.GetInt(key, defaultValue);
        if (!ObscuredPrefs.preservePlayerPrefs)
        {
          ObscuredPrefs.SetInt(key, num);
          PlayerPrefs.DeleteKey(key);
        }
        return num;
      }
      string encryptedPrefsString = ObscuredPrefs.GetEncryptedPrefsString(key, encryptedKey);
      return encryptedPrefsString == "{not_found}" ? defaultValue : ObscuredPrefs.DecryptIntValue(key, encryptedPrefsString, defaultValue);
    }

    public static string EncryptIntValue(string key, int value)
    {
      byte[] bytes = BitConverter.GetBytes(value);
      return ObscuredPrefs.EncryptData(key, bytes, ObscuredPrefs.DataType.Int);
    }

    public static int DecryptIntValue(string key, string encryptedInput, int defaultValue)
    {
      if (encryptedInput.IndexOf(':') > -1)
      {
        string s = ObscuredPrefs.DeprecatedDecryptValue(encryptedInput);
        if (s == string.Empty)
          return defaultValue;
        int result;
        int.TryParse(s, out result);
        ObscuredPrefs.SetInt(key, result);
        return result;
      }
      byte[] numArray = ObscuredPrefs.DecryptData(key, encryptedInput);
      return numArray == null ? defaultValue : BitConverter.ToInt32(numArray, 0);
    }

    public static void SetUInt(string key, uint value)
    {
      PlayerPrefs.SetString(ObscuredPrefs.EncryptKey(key), ObscuredPrefs.EncryptUIntValue(key, value));
    }

    public static uint GetUInt(string key) => ObscuredPrefs.GetUInt(key, 0U);

    public static uint GetUInt(string key, uint defaultValue)
    {
      string encryptedPrefsString = ObscuredPrefs.GetEncryptedPrefsString(key, ObscuredPrefs.EncryptKey(key));
      return encryptedPrefsString == "{not_found}" ? defaultValue : ObscuredPrefs.DecryptUIntValue(key, encryptedPrefsString, defaultValue);
    }

    public static string EncryptUIntValue(string key, uint value)
    {
      byte[] bytes = BitConverter.GetBytes(value);
      return ObscuredPrefs.EncryptData(key, bytes, ObscuredPrefs.DataType.UInt);
    }

    public static uint DecryptUIntValue(string key, string encryptedInput, uint defaultValue)
    {
      if (encryptedInput.IndexOf(':') > -1)
      {
        string s = ObscuredPrefs.DeprecatedDecryptValue(encryptedInput);
        if (s == string.Empty)
          return defaultValue;
        uint result;
        uint.TryParse(s, out result);
        ObscuredPrefs.SetUInt(key, result);
        return result;
      }
      byte[] numArray = ObscuredPrefs.DecryptData(key, encryptedInput);
      return numArray == null ? defaultValue : BitConverter.ToUInt32(numArray, 0);
    }

    public static void SetString(string key, string value)
    {
      if (key == null)
        throw new ArgumentNullException(nameof (key));
      if (value == null)
        value = string.Empty;
      PlayerPrefs.SetString(ObscuredPrefs.EncryptKey(key), ObscuredPrefs.EncryptStringValue(key, value));
    }

    public static string GetString(string key) => ObscuredPrefs.GetString(key, string.Empty);

    public static string GetString(string key, string defaultValue)
    {
      string encryptedKey = ObscuredPrefs.EncryptKey(key);
      if (!PlayerPrefs.HasKey(encryptedKey) && PlayerPrefs.HasKey(key))
      {
        string str = PlayerPrefs.GetString(key, defaultValue);
        if (!ObscuredPrefs.preservePlayerPrefs)
        {
          ObscuredPrefs.SetString(key, str);
          PlayerPrefs.DeleteKey(key);
        }
        return str;
      }
      string encryptedPrefsString = ObscuredPrefs.GetEncryptedPrefsString(key, encryptedKey);
      return encryptedPrefsString == "{not_found}" ? defaultValue : ObscuredPrefs.DecryptStringValue(key, encryptedPrefsString, defaultValue);
    }

    public static string EncryptStringValue(string key, string value)
    {
      byte[] bytes = Encoding.UTF8.GetBytes(value);
      return ObscuredPrefs.EncryptData(key, bytes, ObscuredPrefs.DataType.String);
    }

    public static string DecryptStringValue(string key, string encryptedInput, string defaultValue)
    {
      if (encryptedInput.IndexOf(':') > -1)
      {
        string str = ObscuredPrefs.DeprecatedDecryptValue(encryptedInput);
        if (str == string.Empty)
          return defaultValue;
        ObscuredPrefs.SetString(key, str);
        return str;
      }
      byte[] bytes = ObscuredPrefs.DecryptData(key, encryptedInput);
      return bytes == null ? defaultValue : Encoding.UTF8.GetString(bytes, 0, bytes.Length);
    }

    public static void SetFloat(string key, float value)
    {
      PlayerPrefs.SetString(ObscuredPrefs.EncryptKey(key), ObscuredPrefs.EncryptFloatValue(key, value));
    }

    public static float GetFloat(string key) => ObscuredPrefs.GetFloat(key, 0.0f);

    public static float GetFloat(string key, float defaultValue)
    {
      string encryptedKey = ObscuredPrefs.EncryptKey(key);
      if (!PlayerPrefs.HasKey(encryptedKey) && PlayerPrefs.HasKey(key))
      {
        float num = PlayerPrefs.GetFloat(key, defaultValue);
        if (!ObscuredPrefs.preservePlayerPrefs)
        {
          ObscuredPrefs.SetFloat(key, num);
          PlayerPrefs.DeleteKey(key);
        }
        return num;
      }
      string encryptedPrefsString = ObscuredPrefs.GetEncryptedPrefsString(key, encryptedKey);
      return encryptedPrefsString == "{not_found}" ? defaultValue : ObscuredPrefs.DecryptFloatValue(key, encryptedPrefsString, defaultValue);
    }

    public static string EncryptFloatValue(string key, float value)
    {
      byte[] bytes = BitConverter.GetBytes(value);
      return ObscuredPrefs.EncryptData(key, bytes, ObscuredPrefs.DataType.Float);
    }

    public static float DecryptFloatValue(string key, string encryptedInput, float defaultValue)
    {
      if (encryptedInput.IndexOf(':') > -1)
      {
        string s = ObscuredPrefs.DeprecatedDecryptValue(encryptedInput);
        if (s == string.Empty)
          return defaultValue;
        float result;
        float.TryParse(s, out result);
        ObscuredPrefs.SetFloat(key, result);
        return result;
      }
      byte[] numArray = ObscuredPrefs.DecryptData(key, encryptedInput);
      return numArray == null ? defaultValue : BitConverter.ToSingle(numArray, 0);
    }

    public static void SetDouble(string key, double value)
    {
      PlayerPrefs.SetString(ObscuredPrefs.EncryptKey(key), ObscuredPrefs.EncryptDoubleValue(key, value));
    }

    public static double GetDouble(string key) => ObscuredPrefs.GetDouble(key, 0.0);

    public static double GetDouble(string key, double defaultValue)
    {
      string encryptedPrefsString = ObscuredPrefs.GetEncryptedPrefsString(key, ObscuredPrefs.EncryptKey(key));
      return encryptedPrefsString == "{not_found}" ? defaultValue : ObscuredPrefs.DecryptDoubleValue(key, encryptedPrefsString, defaultValue);
    }

    private static string EncryptDoubleValue(string key, double value)
    {
      byte[] bytes = BitConverter.GetBytes(value);
      return ObscuredPrefs.EncryptData(key, bytes, ObscuredPrefs.DataType.Double);
    }

    private static double DecryptDoubleValue(
      string key,
      string encryptedInput,
      double defaultValue)
    {
      if (encryptedInput.IndexOf(':') > -1)
      {
        string s = ObscuredPrefs.DeprecatedDecryptValue(encryptedInput);
        if (s == string.Empty)
          return defaultValue;
        double result;
        double.TryParse(s, out result);
        ObscuredPrefs.SetDouble(key, result);
        return result;
      }
      byte[] numArray = ObscuredPrefs.DecryptData(key, encryptedInput);
      return numArray == null ? defaultValue : BitConverter.ToDouble(numArray, 0);
    }

    public static void SetDecimal(string key, Decimal value)
    {
      PlayerPrefs.SetString(ObscuredPrefs.EncryptKey(key), ObscuredPrefs.EncryptDecimalValue(key, value));
    }

    public static Decimal GetDecimal(string key) => ObscuredPrefs.GetDecimal(key, 0M);

    public static Decimal GetDecimal(string key, Decimal defaultValue)
    {
      string encryptedPrefsString = ObscuredPrefs.GetEncryptedPrefsString(key, ObscuredPrefs.EncryptKey(key));
      return encryptedPrefsString == "{not_found}" ? defaultValue : ObscuredPrefs.DecryptDecimalValue(key, encryptedPrefsString, defaultValue);
    }

    private static string EncryptDecimalValue(string key, Decimal value)
    {
      byte[] bytes = BitconverterExt.GetBytes(value);
      return ObscuredPrefs.EncryptData(key, bytes, ObscuredPrefs.DataType.Decimal);
    }

    private static Decimal DecryptDecimalValue(
      string key,
      string encryptedInput,
      Decimal defaultValue)
    {
      if (encryptedInput.IndexOf(':') > -1)
      {
        string s = ObscuredPrefs.DeprecatedDecryptValue(encryptedInput);
        if (s == string.Empty)
          return defaultValue;
        Decimal result;
        Decimal.TryParse(s, out result);
        ObscuredPrefs.SetDecimal(key, result);
        return result;
      }
      byte[] bytes = ObscuredPrefs.DecryptData(key, encryptedInput);
      return bytes == null ? defaultValue : BitconverterExt.ToDecimal(bytes);
    }

    public static void SetLong(string key, long value)
    {
      PlayerPrefs.SetString(ObscuredPrefs.EncryptKey(key), ObscuredPrefs.EncryptLongValue(key, value));
    }

    public static long GetLong(string key) => ObscuredPrefs.GetLong(key, 0L);

    public static long GetLong(string key, long defaultValue)
    {
      string encryptedPrefsString = ObscuredPrefs.GetEncryptedPrefsString(key, ObscuredPrefs.EncryptKey(key));
      return encryptedPrefsString == "{not_found}" ? defaultValue : ObscuredPrefs.DecryptLongValue(key, encryptedPrefsString, defaultValue);
    }

    private static string EncryptLongValue(string key, long value)
    {
      byte[] bytes = BitConverter.GetBytes(value);
      return ObscuredPrefs.EncryptData(key, bytes, ObscuredPrefs.DataType.Long);
    }

    private static long DecryptLongValue(string key, string encryptedInput, long defaultValue)
    {
      if (encryptedInput.IndexOf(':') > -1)
      {
        string s = ObscuredPrefs.DeprecatedDecryptValue(encryptedInput);
        if (s == string.Empty)
          return defaultValue;
        long result;
        long.TryParse(s, out result);
        ObscuredPrefs.SetLong(key, result);
        return result;
      }
      byte[] numArray = ObscuredPrefs.DecryptData(key, encryptedInput);
      return numArray == null ? defaultValue : BitConverter.ToInt64(numArray, 0);
    }

    public static void SetULong(string key, ulong value)
    {
      PlayerPrefs.SetString(ObscuredPrefs.EncryptKey(key), ObscuredPrefs.EncryptULongValue(key, value));
    }

    public static ulong GetULong(string key) => ObscuredPrefs.GetULong(key, 0UL);

    public static ulong GetULong(string key, ulong defaultValue)
    {
      string encryptedPrefsString = ObscuredPrefs.GetEncryptedPrefsString(key, ObscuredPrefs.EncryptKey(key));
      return encryptedPrefsString == "{not_found}" ? defaultValue : ObscuredPrefs.DecryptULongValue(key, encryptedPrefsString, defaultValue);
    }

    private static string EncryptULongValue(string key, ulong value)
    {
      byte[] bytes = BitConverter.GetBytes(value);
      return ObscuredPrefs.EncryptData(key, bytes, ObscuredPrefs.DataType.ULong);
    }

    private static ulong DecryptULongValue(string key, string encryptedInput, ulong defaultValue)
    {
      if (encryptedInput.IndexOf(':') > -1)
      {
        string s = ObscuredPrefs.DeprecatedDecryptValue(encryptedInput);
        if (s == string.Empty)
          return defaultValue;
        ulong result;
        ulong.TryParse(s, out result);
        ObscuredPrefs.SetULong(key, result);
        return result;
      }
      byte[] numArray = ObscuredPrefs.DecryptData(key, encryptedInput);
      return numArray == null ? defaultValue : BitConverter.ToUInt64(numArray, 0);
    }

    public static void SetBool(string key, bool value)
    {
      PlayerPrefs.SetString(ObscuredPrefs.EncryptKey(key), ObscuredPrefs.EncryptBoolValue(key, value));
    }

    public static bool GetBool(string key) => ObscuredPrefs.GetBool(key, false);

    public static bool GetBool(string key, bool defaultValue)
    {
      string encryptedPrefsString = ObscuredPrefs.GetEncryptedPrefsString(key, ObscuredPrefs.EncryptKey(key));
      return encryptedPrefsString == "{not_found}" ? defaultValue : ObscuredPrefs.DecryptBoolValue(key, encryptedPrefsString, defaultValue);
    }

    private static string EncryptBoolValue(string key, bool value)
    {
      byte[] bytes = BitConverter.GetBytes(value);
      return ObscuredPrefs.EncryptData(key, bytes, ObscuredPrefs.DataType.Bool);
    }

    private static bool DecryptBoolValue(string key, string encryptedInput, bool defaultValue)
    {
      if (encryptedInput.IndexOf(':') > -1)
      {
        string s = ObscuredPrefs.DeprecatedDecryptValue(encryptedInput);
        if (s == string.Empty)
          return defaultValue;
        int result;
        int.TryParse(s, out result);
        ObscuredPrefs.SetBool(key, result == 1);
        return result == 1;
      }
      byte[] numArray = ObscuredPrefs.DecryptData(key, encryptedInput);
      return numArray == null ? defaultValue : BitConverter.ToBoolean(numArray, 0);
    }

    public static void SetByteArray(string key, byte[] value)
    {
      PlayerPrefs.SetString(ObscuredPrefs.EncryptKey(key), ObscuredPrefs.EncryptByteArrayValue(key, value));
    }

    public static byte[] GetByteArray(string key) => ObscuredPrefs.GetByteArray(key, (byte) 0, 0);

    public static byte[] GetByteArray(string key, byte defaultValue, int defaultLength)
    {
      string encryptedPrefsString = ObscuredPrefs.GetEncryptedPrefsString(key, ObscuredPrefs.EncryptKey(key));
      return encryptedPrefsString == "{not_found}" ? ObscuredPrefs.ConstructByteArray(defaultValue, defaultLength) : ObscuredPrefs.DecryptByteArrayValue(key, encryptedPrefsString, defaultValue, defaultLength);
    }

    private static string EncryptByteArrayValue(string key, byte[] value)
    {
      return ObscuredPrefs.EncryptData(key, value, ObscuredPrefs.DataType.ByteArray);
    }

    private static byte[] DecryptByteArrayValue(
      string key,
      string encryptedInput,
      byte defaultValue,
      int defaultLength)
    {
      if (encryptedInput.IndexOf(':') <= -1)
        return ObscuredPrefs.DecryptData(key, encryptedInput) ?? ObscuredPrefs.ConstructByteArray(defaultValue, defaultLength);
      string s = ObscuredPrefs.DeprecatedDecryptValue(encryptedInput);
      if (s == string.Empty)
        return ObscuredPrefs.ConstructByteArray(defaultValue, defaultLength);
      byte[] bytes = Encoding.UTF8.GetBytes(s);
      ObscuredPrefs.SetByteArray(key, bytes);
      return bytes;
    }

    private static byte[] ConstructByteArray(byte value, int length)
    {
      byte[] numArray = new byte[length];
      for (int index = 0; index < length; ++index)
        numArray[index] = value;
      return numArray;
    }

    public static void SetVector2(string key, Vector2 value)
    {
      PlayerPrefs.SetString(ObscuredPrefs.EncryptKey(key), ObscuredPrefs.EncryptVector2Value(key, value));
    }

    public static Vector2 GetVector2(string key) => ObscuredPrefs.GetVector2(key, Vector2.zero);

    public static Vector2 GetVector2(string key, Vector2 defaultValue)
    {
      string encryptedPrefsString = ObscuredPrefs.GetEncryptedPrefsString(key, ObscuredPrefs.EncryptKey(key));
      return encryptedPrefsString == "{not_found}" ? defaultValue : ObscuredPrefs.DecryptVector2Value(key, encryptedPrefsString, defaultValue);
    }

    private static string EncryptVector2Value(string key, Vector2 value)
    {
      byte[] numArray = new byte[8];
      Buffer.BlockCopy((Array) BitConverter.GetBytes(value.x), 0, (Array) numArray, 0, 4);
      Buffer.BlockCopy((Array) BitConverter.GetBytes(value.y), 0, (Array) numArray, 4, 4);
      return ObscuredPrefs.EncryptData(key, numArray, ObscuredPrefs.DataType.Vector2);
    }

    private static Vector2 DecryptVector2Value(
      string key,
      string encryptedInput,
      Vector2 defaultValue)
    {
      if (encryptedInput.IndexOf(':') > -1)
      {
        string str = ObscuredPrefs.DeprecatedDecryptValue(encryptedInput);
        if (str == string.Empty)
          return defaultValue;
        string[] strArray = str.Split("|"[0]);
        float result1;
        float.TryParse(strArray[0], out result1);
        float result2;
        float.TryParse(strArray[1], out result2);
        Vector2 vector2;
        // ISSUE: explicit constructor call
        ((Vector2) ref vector2).\u002Ector(result1, result2);
        ObscuredPrefs.SetVector2(key, vector2);
        return vector2;
      }
      byte[] numArray = ObscuredPrefs.DecryptData(key, encryptedInput);
      if (numArray == null)
        return defaultValue;
      Vector2 vector2_1;
      vector2_1.x = BitConverter.ToSingle(numArray, 0);
      vector2_1.y = BitConverter.ToSingle(numArray, 4);
      return vector2_1;
    }

    public static void SetVector3(string key, Vector3 value)
    {
      PlayerPrefs.SetString(ObscuredPrefs.EncryptKey(key), ObscuredPrefs.EncryptVector3Value(key, value));
    }

    public static Vector3 GetVector3(string key) => ObscuredPrefs.GetVector3(key, Vector3.zero);

    public static Vector3 GetVector3(string key, Vector3 defaultValue)
    {
      string encryptedPrefsString = ObscuredPrefs.GetEncryptedPrefsString(key, ObscuredPrefs.EncryptKey(key));
      return encryptedPrefsString == "{not_found}" ? defaultValue : ObscuredPrefs.DecryptVector3Value(key, encryptedPrefsString, defaultValue);
    }

    private static string EncryptVector3Value(string key, Vector3 value)
    {
      byte[] numArray = new byte[12];
      Buffer.BlockCopy((Array) BitConverter.GetBytes(value.x), 0, (Array) numArray, 0, 4);
      Buffer.BlockCopy((Array) BitConverter.GetBytes(value.y), 0, (Array) numArray, 4, 4);
      Buffer.BlockCopy((Array) BitConverter.GetBytes(value.z), 0, (Array) numArray, 8, 4);
      return ObscuredPrefs.EncryptData(key, numArray, ObscuredPrefs.DataType.Vector3);
    }

    private static Vector3 DecryptVector3Value(
      string key,
      string encryptedInput,
      Vector3 defaultValue)
    {
      if (encryptedInput.IndexOf(':') > -1)
      {
        string str = ObscuredPrefs.DeprecatedDecryptValue(encryptedInput);
        if (str == string.Empty)
          return defaultValue;
        string[] strArray = str.Split("|"[0]);
        float result1;
        float.TryParse(strArray[0], out result1);
        float result2;
        float.TryParse(strArray[1], out result2);
        float result3;
        float.TryParse(strArray[2], out result3);
        Vector3 vector3;
        // ISSUE: explicit constructor call
        ((Vector3) ref vector3).\u002Ector(result1, result2, result3);
        ObscuredPrefs.SetVector3(key, vector3);
        return vector3;
      }
      byte[] numArray = ObscuredPrefs.DecryptData(key, encryptedInput);
      if (numArray == null)
        return defaultValue;
      Vector3 vector3_1;
      vector3_1.x = BitConverter.ToSingle(numArray, 0);
      vector3_1.y = BitConverter.ToSingle(numArray, 4);
      vector3_1.z = BitConverter.ToSingle(numArray, 8);
      return vector3_1;
    }

    public static void SetQuaternion(string key, Quaternion value)
    {
      PlayerPrefs.SetString(ObscuredPrefs.EncryptKey(key), ObscuredPrefs.EncryptQuaternionValue(key, value));
    }

    public static Quaternion GetQuaternion(string key)
    {
      return ObscuredPrefs.GetQuaternion(key, Quaternion.identity);
    }

    public static Quaternion GetQuaternion(string key, Quaternion defaultValue)
    {
      string encryptedPrefsString = ObscuredPrefs.GetEncryptedPrefsString(key, ObscuredPrefs.EncryptKey(key));
      return encryptedPrefsString == "{not_found}" ? defaultValue : ObscuredPrefs.DecryptQuaternionValue(key, encryptedPrefsString, defaultValue);
    }

    private static string EncryptQuaternionValue(string key, Quaternion value)
    {
      byte[] numArray = new byte[16];
      Buffer.BlockCopy((Array) BitConverter.GetBytes(value.x), 0, (Array) numArray, 0, 4);
      Buffer.BlockCopy((Array) BitConverter.GetBytes(value.y), 0, (Array) numArray, 4, 4);
      Buffer.BlockCopy((Array) BitConverter.GetBytes(value.z), 0, (Array) numArray, 8, 4);
      Buffer.BlockCopy((Array) BitConverter.GetBytes(value.w), 0, (Array) numArray, 12, 4);
      return ObscuredPrefs.EncryptData(key, numArray, ObscuredPrefs.DataType.Quaternion);
    }

    private static Quaternion DecryptQuaternionValue(
      string key,
      string encryptedInput,
      Quaternion defaultValue)
    {
      if (encryptedInput.IndexOf(':') > -1)
      {
        string str = ObscuredPrefs.DeprecatedDecryptValue(encryptedInput);
        if (str == string.Empty)
          return defaultValue;
        string[] strArray = str.Split("|"[0]);
        float result1;
        float.TryParse(strArray[0], out result1);
        float result2;
        float.TryParse(strArray[1], out result2);
        float result3;
        float.TryParse(strArray[2], out result3);
        float result4;
        float.TryParse(strArray[3], out result4);
        Quaternion quaternion;
        // ISSUE: explicit constructor call
        ((Quaternion) ref quaternion).\u002Ector(result1, result2, result3, result4);
        ObscuredPrefs.SetQuaternion(key, quaternion);
        return quaternion;
      }
      byte[] numArray = ObscuredPrefs.DecryptData(key, encryptedInput);
      if (numArray == null)
        return defaultValue;
      Quaternion quaternion1;
      quaternion1.x = BitConverter.ToSingle(numArray, 0);
      quaternion1.y = BitConverter.ToSingle(numArray, 4);
      quaternion1.z = BitConverter.ToSingle(numArray, 8);
      quaternion1.w = BitConverter.ToSingle(numArray, 12);
      return quaternion1;
    }

    public static void SetColor(string key, Color32 value)
    {
      uint num = (uint) ((int) value.a << 24 | (int) value.r << 16 | (int) value.g << 8) | (uint) value.b;
      PlayerPrefs.SetString(ObscuredPrefs.EncryptKey(key), ObscuredPrefs.EncryptColorValue(key, num));
    }

    public static Color32 GetColor(string key)
    {
      return ObscuredPrefs.GetColor(key, new Color32((byte) 0, (byte) 0, (byte) 0, (byte) 1));
    }

    public static Color32 GetColor(string key, Color32 defaultValue)
    {
      string encryptedPrefsString = ObscuredPrefs.GetEncryptedPrefsString(key, ObscuredPrefs.EncryptKey(key));
      if (encryptedPrefsString == "{not_found}")
        return defaultValue;
      uint num1 = ObscuredPrefs.DecryptUIntValue(key, encryptedPrefsString, 16777216U);
      byte num2 = (byte) (num1 >> 24);
      return new Color32((byte) (num1 >> 16), (byte) (num1 >> 8), (byte) (num1 >> 0), num2);
    }

    private static string EncryptColorValue(string key, uint value)
    {
      byte[] bytes = BitConverter.GetBytes(value);
      return ObscuredPrefs.EncryptData(key, bytes, ObscuredPrefs.DataType.Color);
    }

    public static void SetRect(string key, Rect value)
    {
      PlayerPrefs.SetString(ObscuredPrefs.EncryptKey(key), ObscuredPrefs.EncryptRectValue(key, value));
    }

    public static Rect GetRect(string key)
    {
      return ObscuredPrefs.GetRect(key, new Rect(0.0f, 0.0f, 0.0f, 0.0f));
    }

    public static Rect GetRect(string key, Rect defaultValue)
    {
      string encryptedPrefsString = ObscuredPrefs.GetEncryptedPrefsString(key, ObscuredPrefs.EncryptKey(key));
      return encryptedPrefsString == "{not_found}" ? defaultValue : ObscuredPrefs.DecryptRectValue(key, encryptedPrefsString, defaultValue);
    }

    private static string EncryptRectValue(string key, Rect value)
    {
      byte[] numArray = new byte[16];
      Buffer.BlockCopy((Array) BitConverter.GetBytes(((Rect) ref value).x), 0, (Array) numArray, 0, 4);
      Buffer.BlockCopy((Array) BitConverter.GetBytes(((Rect) ref value).y), 0, (Array) numArray, 4, 4);
      Buffer.BlockCopy((Array) BitConverter.GetBytes(((Rect) ref value).width), 0, (Array) numArray, 8, 4);
      Buffer.BlockCopy((Array) BitConverter.GetBytes(((Rect) ref value).height), 0, (Array) numArray, 12, 4);
      return ObscuredPrefs.EncryptData(key, numArray, ObscuredPrefs.DataType.Rect);
    }

    private static Rect DecryptRectValue(string key, string encryptedInput, Rect defaultValue)
    {
      if (encryptedInput.IndexOf(':') > -1)
      {
        string str = ObscuredPrefs.DeprecatedDecryptValue(encryptedInput);
        if (str == string.Empty)
          return defaultValue;
        string[] strArray = str.Split("|"[0]);
        float result1;
        float.TryParse(strArray[0], out result1);
        float result2;
        float.TryParse(strArray[1], out result2);
        float result3;
        float.TryParse(strArray[2], out result3);
        float result4;
        float.TryParse(strArray[3], out result4);
        Rect rect;
        // ISSUE: explicit constructor call
        ((Rect) ref rect).\u002Ector(result1, result2, result3, result4);
        ObscuredPrefs.SetRect(key, rect);
        return rect;
      }
      byte[] numArray = ObscuredPrefs.DecryptData(key, encryptedInput);
      if (numArray == null)
        return defaultValue;
      Rect rect1 = new Rect();
      ((Rect) ref rect1).x = BitConverter.ToSingle(numArray, 0);
      ((Rect) ref rect1).y = BitConverter.ToSingle(numArray, 4);
      ((Rect) ref rect1).width = BitConverter.ToSingle(numArray, 8);
      ((Rect) ref rect1).height = BitConverter.ToSingle(numArray, 12);
      return rect1;
    }

    public static void SetRawValue(string key, string encryptedValue)
    {
      PlayerPrefs.SetString(ObscuredPrefs.EncryptKey(key), encryptedValue);
    }

    public static string GetRawValue(string key)
    {
      return PlayerPrefs.GetString(ObscuredPrefs.EncryptKey(key));
    }

    public static ObscuredPrefs.DataType GetRawValueType(string value)
    {
      ObscuredPrefs.DataType rawValueType1 = ObscuredPrefs.DataType.Unknown;
      byte[] numArray;
      try
      {
        numArray = Convert.FromBase64String(value);
      }
      catch (Exception ex)
      {
        return rawValueType1;
      }
      if (numArray.Length < 7)
        return rawValueType1;
      int length = numArray.Length;
      ObscuredPrefs.DataType rawValueType2 = (ObscuredPrefs.DataType) numArray[length - 7];
      if (numArray[length - 6] > (byte) 10)
        rawValueType2 = ObscuredPrefs.DataType.Unknown;
      return rawValueType2;
    }

    public static string EncryptKey(string key)
    {
      key = ObscuredString.EncryptDecrypt(key, ObscuredPrefs.cryptoKey);
      key = Convert.ToBase64String(Encoding.UTF8.GetBytes(key));
      return key;
    }

    public static bool HasKey(string key)
    {
      return PlayerPrefs.HasKey(key) || PlayerPrefs.HasKey(ObscuredPrefs.EncryptKey(key));
    }

    public static void DeleteKey(string key)
    {
      PlayerPrefs.DeleteKey(ObscuredPrefs.EncryptKey(key));
      if (ObscuredPrefs.preservePlayerPrefs)
        return;
      PlayerPrefs.DeleteKey(key);
    }

    public static void DeleteAll() => PlayerPrefs.DeleteAll();

    public static void Save() => PlayerPrefs.Save();

    private static string GetEncryptedPrefsString(string key, string encryptedKey)
    {
      string encryptedPrefsString = PlayerPrefs.GetString(encryptedKey, "{not_found}");
      if (encryptedPrefsString == "{not_found}" && PlayerPrefs.HasKey(key))
        Debug.LogWarning((object) ("[ACTk] Are you trying to read regular PlayerPrefs data using ObscuredPrefs (key = " + key + ")?"));
      return encryptedPrefsString;
    }

    private static string EncryptData(string key, byte[] cleanBytes, ObscuredPrefs.DataType type)
    {
      int length1 = cleanBytes.Length;
      byte[] src1 = ObscuredPrefs.EncryptDecryptBytes(cleanBytes, length1, key + ObscuredPrefs.cryptoKey);
      uint hash = xxHash.CalculateHash(cleanBytes, length1, 0U);
      byte[] src2 = new byte[4]
      {
        (byte) (hash & (uint) byte.MaxValue),
        (byte) (hash >> 8 & (uint) byte.MaxValue),
        (byte) (hash >> 16 & (uint) byte.MaxValue),
        (byte) (hash >> 24 & (uint) byte.MaxValue)
      };
      byte[] src3 = (byte[]) null;
      int length2;
      if (ObscuredPrefs.lockToDevice != ObscuredPrefs.DeviceLockLevel.None)
      {
        length2 = length1 + 11;
        uint deviceIdHash = ObscuredPrefs.DeviceIdHash;
        src3 = new byte[4]
        {
          (byte) (deviceIdHash & (uint) byte.MaxValue),
          (byte) (deviceIdHash >> 8 & (uint) byte.MaxValue),
          (byte) (deviceIdHash >> 16 & (uint) byte.MaxValue),
          (byte) (deviceIdHash >> 24 & (uint) byte.MaxValue)
        };
      }
      else
        length2 = length1 + 7;
      byte[] numArray = new byte[length2];
      Buffer.BlockCopy((Array) src1, 0, (Array) numArray, 0, length1);
      if (src3 != null)
        Buffer.BlockCopy((Array) src3, 0, (Array) numArray, length1, 4);
      numArray[length2 - 7] = (byte) type;
      numArray[length2 - 6] = (byte) 2;
      numArray[length2 - 5] = (byte) ObscuredPrefs.lockToDevice;
      Buffer.BlockCopy((Array) src2, 0, (Array) numArray, length2 - 4, 4);
      return Convert.ToBase64String(numArray);
    }

    internal static byte[] DecryptData(string key, string encryptedInput)
    {
      byte[] src;
      try
      {
        src = Convert.FromBase64String(encryptedInput);
      }
      catch (Exception ex)
      {
        ObscuredPrefs.SavesTampered();
        return (byte[]) null;
      }
      if (src.Length <= 0)
      {
        ObscuredPrefs.SavesTampered();
        return (byte[]) null;
      }
      int length1 = src.Length;
      if (src[length1 - 6] != (byte) 2)
      {
        ObscuredPrefs.SavesTampered();
        return (byte[]) null;
      }
      ObscuredPrefs.DeviceLockLevel deviceLockLevel = (ObscuredPrefs.DeviceLockLevel) src[length1 - 5];
      byte[] dst1 = new byte[4];
      Buffer.BlockCopy((Array) src, length1 - 4, (Array) dst1, 0, 4);
      uint num1 = (uint) ((int) dst1[0] | (int) dst1[1] << 8 | (int) dst1[2] << 16 | (int) dst1[3] << 24);
      uint num2 = 0;
      int length2;
      if (deviceLockLevel != ObscuredPrefs.DeviceLockLevel.None)
      {
        length2 = length1 - 11;
        if (ObscuredPrefs.lockToDevice != ObscuredPrefs.DeviceLockLevel.None)
        {
          byte[] dst2 = new byte[4];
          Buffer.BlockCopy((Array) src, length2, (Array) dst2, 0, 4);
          num2 = (uint) ((int) dst2[0] | (int) dst2[1] << 8 | (int) dst2[2] << 16 | (int) dst2[3] << 24);
        }
      }
      else
        length2 = length1 - 7;
      byte[] numArray = new byte[length2];
      Buffer.BlockCopy((Array) src, 0, (Array) numArray, 0, length2);
      byte[] buf = ObscuredPrefs.EncryptDecryptBytes(numArray, length2, key + ObscuredPrefs.cryptoKey);
      if ((int) xxHash.CalculateHash(buf, length2, 0U) != (int) num1)
      {
        ObscuredPrefs.SavesTampered();
        return (byte[]) null;
      }
      if (ObscuredPrefs.lockToDevice == ObscuredPrefs.DeviceLockLevel.Strict && num2 == 0U && !ObscuredPrefs.emergencyMode && !ObscuredPrefs.readForeignSaves)
        return (byte[]) null;
      if (num2 != 0U && !ObscuredPrefs.emergencyMode)
      {
        uint deviceIdHash = ObscuredPrefs.DeviceIdHash;
        if ((int) num2 != (int) deviceIdHash)
        {
          ObscuredPrefs.PossibleForeignSavesDetected();
          if (!ObscuredPrefs.readForeignSaves)
            return (byte[]) null;
        }
      }
      return buf;
    }

    private static uint CalculateChecksum(string input)
    {
      byte[] bytes = Encoding.UTF8.GetBytes(input + ObscuredPrefs.cryptoKey);
      return xxHash.CalculateHash(bytes, bytes.Length, 0U);
    }

    private static void SavesTampered()
    {
      if (ObscuredPrefs.onAlterationDetected != null && !ObscuredPrefs.alterationReported)
      {
        ObscuredPrefs.alterationReported = true;
        ObscuredPrefs.onAlterationDetected();
      }
      if (ObscuredPrefs.OnAlterationDetected == null || ObscuredPrefs.alterationReported)
        return;
      ObscuredPrefs.alterationReported = true;
      ObscuredPrefs.OnAlterationDetected();
    }

    private static void PossibleForeignSavesDetected()
    {
      if (ObscuredPrefs.onPossibleForeignSavesDetected != null && !ObscuredPrefs.foreignSavesReported)
      {
        ObscuredPrefs.foreignSavesReported = true;
        ObscuredPrefs.onPossibleForeignSavesDetected();
      }
      if (ObscuredPrefs.OnPossibleForeignSavesDetected == null || ObscuredPrefs.foreignSavesReported)
        return;
      ObscuredPrefs.foreignSavesReported = true;
      ObscuredPrefs.OnPossibleForeignSavesDetected();
    }

    private static string GetDeviceId()
    {
      string deviceId = string.Empty;
      if (string.IsNullOrEmpty(deviceId))
        deviceId = SystemInfo.deviceUniqueIdentifier;
      return deviceId;
    }

    private static byte[] EncryptDecryptBytes(byte[] bytes, int dataLength, string key)
    {
      int length = key.Length;
      byte[] numArray = new byte[dataLength];
      for (int index = 0; index < dataLength; ++index)
        numArray[index] = (byte) ((uint) bytes[index] ^ (uint) key[index % length]);
      return numArray;
    }

    private static string DeprecatedDecryptValue(string value)
    {
      string[] strArray = value.Split(':');
      if (strArray.Length < 2)
      {
        ObscuredPrefs.SavesTampered();
        return string.Empty;
      }
      string str1 = strArray[0];
      string str2 = strArray[1];
      byte[] bytes;
      try
      {
        bytes = Convert.FromBase64String(str1);
      }
      catch
      {
        ObscuredPrefs.SavesTampered();
        return string.Empty;
      }
      string str3 = ObscuredString.EncryptDecrypt(Encoding.UTF8.GetString(bytes, 0, bytes.Length), ObscuredPrefs.cryptoKey);
      if (strArray.Length == 3)
      {
        if (str2 != ObscuredPrefs.DeprecatedCalculateChecksum(str1 + ObscuredPrefs.DeprecatedDeviceId))
          ObscuredPrefs.SavesTampered();
      }
      else if (strArray.Length == 2)
      {
        if (str2 != ObscuredPrefs.DeprecatedCalculateChecksum(str1))
          ObscuredPrefs.SavesTampered();
      }
      else
        ObscuredPrefs.SavesTampered();
      if (ObscuredPrefs.lockToDevice != ObscuredPrefs.DeviceLockLevel.None && !ObscuredPrefs.emergencyMode)
      {
        if (strArray.Length >= 3)
        {
          if (strArray[2] != ObscuredPrefs.DeprecatedDeviceId)
          {
            if (!ObscuredPrefs.readForeignSaves)
              str3 = string.Empty;
            ObscuredPrefs.PossibleForeignSavesDetected();
          }
        }
        else if (ObscuredPrefs.lockToDevice == ObscuredPrefs.DeviceLockLevel.Strict)
        {
          if (!ObscuredPrefs.readForeignSaves)
            str3 = string.Empty;
          ObscuredPrefs.PossibleForeignSavesDetected();
        }
        else if (str2 != ObscuredPrefs.DeprecatedCalculateChecksum(str1))
        {
          if (!ObscuredPrefs.readForeignSaves)
            str3 = string.Empty;
          ObscuredPrefs.PossibleForeignSavesDetected();
        }
      }
      return str3;
    }

    private static string DeprecatedCalculateChecksum(string input)
    {
      int num1 = 0;
      byte[] bytes = Encoding.UTF8.GetBytes(input + ObscuredPrefs.cryptoKey);
      int length = bytes.Length;
      int num2 = ObscuredPrefs.cryptoKey.Length ^ 64;
      for (int index = 0; index < length; ++index)
      {
        byte num3 = bytes[index];
        num1 += (int) num3 + (int) num3 * (index + num2) % 3;
      }
      return num1.ToString("X2");
    }

    private static string DeprecatedDeviceId
    {
      get
      {
        if (string.IsNullOrEmpty(ObscuredPrefs.deprecatedDeviceId))
          ObscuredPrefs.deprecatedDeviceId = ObscuredPrefs.DeprecatedCalculateChecksum(ObscuredPrefs.DeviceId);
        return ObscuredPrefs.deprecatedDeviceId;
      }
    }

    public enum DataType : byte
    {
      Unknown = 0,
      Int = 5,
      UInt = 10, // 0x0A
      String = 15, // 0x0F
      Float = 20, // 0x14
      Double = 25, // 0x19
      Decimal = 27, // 0x1B
      Long = 30, // 0x1E
      ULong = 32, // 0x20
      Bool = 35, // 0x23
      ByteArray = 40, // 0x28
      Vector2 = 45, // 0x2D
      Vector3 = 50, // 0x32
      Quaternion = 55, // 0x37
      Color = 60, // 0x3C
      Rect = 65, // 0x41
    }

    public enum DeviceLockLevel : byte
    {
      None,
      Soft,
      Strict,
    }
  }
}
