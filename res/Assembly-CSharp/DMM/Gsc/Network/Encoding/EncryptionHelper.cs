// Decompiled with JetBrains decompiler
// Type: Gsc.Network.Encoding.EncryptionHelper
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

#nullable disable
namespace Gsc.Network.Encoding
{
  public class EncryptionHelper
  {
    private static OByte[] aoba;
    private static OByte[] doba;

    static EncryptionHelper() => EncryptionHelper.GetSharedKey(EncryptionHelper.KeyType.APP);

    internal static byte[] Decrypt(
      EncryptionHelper.KeyType keyType,
      byte[] input,
      string keySalt,
      EncryptionHelper.DecryptOptions options)
    {
      if (input == null || input.Length < 16)
        throw new CryptographicException("invalid input to decrypt");
      RijndaelManaged rijndaelManaged = new RijndaelManaged();
      rijndaelManaged.KeySize = 128;
      rijndaelManaged.BlockSize = 128;
      rijndaelManaged.Mode = CipherMode.CBC;
      rijndaelManaged.Padding = PaddingMode.PKCS7;
      rijndaelManaged.IV = options != EncryptionHelper.DecryptOptions.IsFile ? ((IEnumerable<byte>) input).Take<byte>(16).ToArray<byte>() : EncryptionHelper.Hex2Byte(keySalt);
      rijndaelManaged.Key = ((IEnumerable<byte>) new SHA256Managed().ComputeHash(((IEnumerable<byte>) EncryptionHelper.GetSharedKey(keyType)).Concat<byte>(options != EncryptionHelper.DecryptOptions.IsFile ? (IEnumerable<byte>) System.Text.Encoding.ASCII.GetBytes(keySalt) : (IEnumerable<byte>) new byte[0]).Concat<byte>(options == EncryptionHelper.DecryptOptions.ExtraKeySaltATDI || options == EncryptionHelper.DecryptOptions.ExtraKeySaltAT ? (IEnumerable<byte>) System.Text.Encoding.ASCII.GetBytes(Session.DefaultSession.AccessToken) : (IEnumerable<byte>) new byte[0]).Concat<byte>(options != EncryptionHelper.DecryptOptions.ExtraKeySaltATDI ? (IEnumerable<byte>) new byte[0] : (IEnumerable<byte>) System.Text.Encoding.ASCII.GetBytes(Gsc.App.BootLoader.GetAccountManager().GetDeviceId((string) null))).ToArray<byte>())).Take<byte>(rijndaelManaged.KeySize / 8).ToArray<byte>();
      return rijndaelManaged.CreateDecryptor().TransformFinalBlock(input, 16, input.Length - 16);
    }

    internal static byte[] Encrypt(EncryptionHelper.KeyType keyType, byte[] input, string keySalt)
    {
      if (input == null || input.Length == 0)
        return input;
      RijndaelManaged rijndaelManaged = new RijndaelManaged();
      rijndaelManaged.KeySize = 128;
      rijndaelManaged.BlockSize = 128;
      rijndaelManaged.Mode = CipherMode.CBC;
      rijndaelManaged.Padding = PaddingMode.PKCS7;
      EncryptionHelper.DecryptOptions decryptOptions = EncryptionHelper.UseWhatExtraKeySalt(keySalt);
      rijndaelManaged.Key = ((IEnumerable<byte>) new SHA256Managed().ComputeHash(((IEnumerable<byte>) EncryptionHelper.GetSharedKey(keyType)).Concat<byte>((IEnumerable<byte>) System.Text.Encoding.ASCII.GetBytes(keySalt)).Concat<byte>(decryptOptions == EncryptionHelper.DecryptOptions.ExtraKeySaltATDI || decryptOptions == EncryptionHelper.DecryptOptions.ExtraKeySaltAT ? (IEnumerable<byte>) System.Text.Encoding.ASCII.GetBytes(Session.DefaultSession.AccessToken) : (IEnumerable<byte>) new byte[0]).Concat<byte>(decryptOptions != EncryptionHelper.DecryptOptions.ExtraKeySaltATDI ? (IEnumerable<byte>) new byte[0] : (IEnumerable<byte>) System.Text.Encoding.ASCII.GetBytes(Gsc.App.BootLoader.GetAccountManager().GetDeviceId((string) null))).ToArray<byte>())).Take<byte>(rijndaelManaged.KeySize / 8).ToArray<byte>();
      ICryptoTransform encryptor = rijndaelManaged.CreateEncryptor();
      return ((IEnumerable<byte>) rijndaelManaged.IV).Concat<byte>((IEnumerable<byte>) encryptor.TransformFinalBlock(input, 0, input.Length)).ToArray<byte>();
    }

    public static bool IsUseAPPSharedKey(string apiName)
    {
      return apiName == "/chkver2" || apiName.StartsWith("/gauth/") || apiName.StartsWith("/auth/") || apiName.StartsWith("/master/") || apiName == "/product" || apiName.StartsWith("/achieve/") || apiName.StartsWith("/debug/") || apiName.StartsWith("/mst/");
    }

    private static EncryptionHelper.DecryptOptions UseWhatExtraKeySalt(string apiName)
    {
      if (apiName == "/chkver2" || apiName.StartsWith("/gauth/") || apiName.StartsWith("/auth/") || apiName == "/product" || apiName == "/playnew" || apiName.StartsWith("/master/") || apiName.StartsWith("/debug/") || apiName.StartsWith("/mst/"))
        return EncryptionHelper.DecryptOptions.None;
      if (Session.DefaultSession != null && !string.IsNullOrEmpty(Session.DefaultSession.AccessToken) && apiName.StartsWith("/login"))
        return EncryptionHelper.DecryptOptions.ExtraKeySaltAT;
      return Session.DefaultSession != null && !string.IsNullOrEmpty(Gsc.App.BootLoader.GetAccountManager().GetDeviceId((string) null)) && !string.IsNullOrEmpty(Session.DefaultSession.AccessToken) ? EncryptionHelper.DecryptOptions.ExtraKeySaltATDI : EncryptionHelper.DecryptOptions.None;
    }

    private static byte[] GetSharedKey(EncryptionHelper.KeyType keyType, Texture2D useThisImage = null)
    {
      if (keyType == EncryptionHelper.KeyType.APP && EncryptionHelper.aoba != null)
        return ((IEnumerable<OByte>) EncryptionHelper.aoba).Select<OByte, byte>((Func<OByte, byte>) (b => (byte) b)).ToArray<byte>();
      if (keyType == EncryptionHelper.KeyType.DLC && EncryptionHelper.doba != null)
        return ((IEnumerable<OByte>) EncryptionHelper.doba).Select<OByte, byte>((Func<OByte, byte>) (b => (byte) b)).ToArray<byte>();
      Texture2D keyImageAsset = EncryptionHelper.GetKeyImageAsset(keyType);
      int num1 = 0;
      int num2 = 0;
      int n = 0;
      OByte[] obyteArray = new OByte[16];
      try
      {
        for (int index1 = 0; index1 < ((Texture) keyImageAsset).height; ++index1)
        {
          for (int index2 = 0; index2 < ((Texture) keyImageAsset).width; ++index2)
          {
            Color32 color32 = Color32.op_Implicit(keyImageAsset.GetPixel(index2, index1));
            for (int index3 = 0; index3 < 3; ++index3)
            {
              switch (num1 % 3)
              {
                case 0:
                  n = n * 2 + (int) color32.r % 2;
                  break;
                case 1:
                  n = n * 2 + (int) color32.g % 2;
                  break;
                case 2:
                  n = n * 2 + (int) color32.b % 2;
                  break;
              }
              ++num1;
              if (num1 % 8 == 0)
              {
                n = EncryptionHelper.ReverseBits(n);
                if (n == 0)
                {
                  if (keyType == EncryptionHelper.KeyType.APP)
                  {
                    EncryptionHelper.aoba = obyteArray;
                    return ((IEnumerable<OByte>) EncryptionHelper.aoba).Select<OByte, byte>((Func<OByte, byte>) (b => (byte) b)).ToArray<byte>();
                  }
                  EncryptionHelper.doba = obyteArray;
                  return ((IEnumerable<OByte>) EncryptionHelper.doba).Select<OByte, byte>((Func<OByte, byte>) (b => (byte) b)).ToArray<byte>();
                }
                byte num3 = (byte) n;
                obyteArray[num2++] = (OByte) num3;
              }
            }
          }
        }
      }
      catch (Exception ex)
      {
        EncryptionHelper.KeyNotEmbeddedError(keyType, ex);
      }
      if (keyType == EncryptionHelper.KeyType.APP)
      {
        EncryptionHelper.aoba = obyteArray;
        return ((IEnumerable<OByte>) EncryptionHelper.aoba).Select<OByte, byte>((Func<OByte, byte>) (b => (byte) b)).ToArray<byte>();
      }
      EncryptionHelper.doba = obyteArray;
      return ((IEnumerable<OByte>) EncryptionHelper.doba).Select<OByte, byte>((Func<OByte, byte>) (b => (byte) b)).ToArray<byte>();
    }

    private static void KeyNotEmbeddedError(EncryptionHelper.KeyType keyType, Exception e)
    {
      throw e;
    }

    public static byte[] DecryptEmbeddedTutorialMasterData(byte[] input)
    {
      return EncryptionHelper.Decrypt(EncryptionHelper.KeyType.APP, input, string.Empty, EncryptionHelper.DecryptOptions.None);
    }

    private static Texture2D GetKeyImageAsset(EncryptionHelper.KeyType keyType)
    {
      if (!keyType.Equals((object) EncryptionHelper.KeyType.APP))
        return AssetManager.Load<Texture2D>(AssetManager.AssetList.FastFindItemByID(SRPG.Network.EnvFlg2).Path);
      ScriptableTexture2D scriptableTexture2D = Resources.Load<ScriptableTexture2D>("ScriptableTexture2D");
      return UnityEngine.Object.op_Inequality((UnityEngine.Object) scriptableTexture2D, (UnityEngine.Object) null) ? scriptableTexture2D.texture : (Texture2D) null;
    }

    private static byte[] Hex2Byte(string hex)
    {
      return Enumerable.Range(0, hex.Length).Where<int>((Func<int, bool>) (x => x % 2 == 0)).Select<int, byte>((Func<int, byte>) (x => Convert.ToByte(hex.Substring(x, 2), 16))).ToArray<byte>();
    }

    private static int ReverseBits(int n)
    {
      int num = 0;
      for (int index = 0; index < 8; ++index)
      {
        num = num * 2 + n % 2;
        n /= 2;
      }
      return num;
    }

    public enum KeyType
    {
      APP,
      DLC,
    }

    internal enum DecryptOptions
    {
      None,
      IsFile,
      ExtraKeySaltAT,
      ExtraKeySaltATDI,
    }
  }
}
