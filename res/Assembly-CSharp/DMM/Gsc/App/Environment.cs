// Decompiled with JetBrains decompiler
// Type: Gsc.App.Environment
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using SRPG;
using System;
using System.Collections.Generic;

#nullable disable
namespace Gsc.App
{
  public struct Environment : Configuration.IEnvironment
  {
    private const string NATIVEBASE_URL = "https://production-alchemist.nativebase.gu3.jp";
    private const string AUTH_API_PREFIX = "/gauth";
    private const string PURCHASE_API_PREFIX = "/charge";

    public string ServerUrl { get; set; }

    public string NativeBaseUrl { get; set; }

    public string LogCollectionUrl { get; set; }

    public string ClientErrorApi { get; set; }

    public string AuthApiPrefix { get; set; }

    public string PurchaseApiPrefix { get; set; }

    public string DLHost { get; set; }

    public string SiteHost { get; set; }

    public string NewsHost { get; set; }

    public string Assets { get; set; }

    public string AssetsEx { get; set; }

    public string MasterDigest { get; set; }

    public string QuestDigest { get; set; }

    public string EnvFlg2 { get; set; }

    public string Pub { get; set; }

    public string PubU { get; set; }

    public SRPG.Network.EErrCode Stat { get; set; }

    public string StatMsg { get; set; }

    public string StatCode { get; set; }

    public long ServerTime { get; set; }

    public int UseAppguard { get; set; }

    public string BattleVersion { get; set; }

    public Environment.EnvironmentFlagBit EnvironmentFlag { get; private set; }

    public bool IsEnvironmentFlag(Environment.EnvironmentFlagBit flag)
    {
      return (this.EnvironmentFlag & flag) == flag;
    }

    public void SetValue(string key, string value)
    {
      this.ClientErrorApi = (string) null;
      this.AuthApiPrefix = "/gauth";
      this.PurchaseApiPrefix = "/charge";
      if (key == null)
        return;
      // ISSUE: reference to a compiler-generated field
      if (Environment.\u003C\u003Ef__switch\u0024map2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Environment.\u003C\u003Ef__switch\u0024map2 = new Dictionary<string, int>(20)
        {
          {
            "stat",
            0
          },
          {
            "stat_msg",
            1
          },
          {
            "stat_code",
            2
          },
          {
            "time",
            3
          },
          {
            "host_ap",
            4
          },
          {
            "nativebase_url",
            5
          },
          {
            "logcollection_url",
            6
          },
          {
            "host_dl",
            7
          },
          {
            "host_site",
            8
          },
          {
            "host_news",
            9
          },
          {
            "assets",
            10
          },
          {
            "assets_ex",
            11
          },
          {
            "master_digest",
            12
          },
          {
            "quest_digest",
            13
          },
          {
            "pub",
            14
          },
          {
            "pub_u",
            15
          },
          {
            "use_appguard",
            16
          },
          {
            "env_flg",
            17
          },
          {
            "env_flg2",
            18
          },
          {
            "btl_ver",
            19
          }
        };
      }
      int num;
      // ISSUE: reference to a compiler-generated field
      if (!Environment.\u003C\u003Ef__switch\u0024map2.TryGetValue(key, out num))
        return;
      switch (num)
      {
        case 0:
          this.Stat = (SRPG.Network.EErrCode) int.Parse(value);
          break;
        case 1:
          this.StatMsg = value;
          break;
        case 2:
          this.StatCode = value;
          break;
        case 3:
          this.ServerTime = long.Parse(value);
          break;
        case 4:
          this.ServerUrl = Environment.EndsSlashDelete(value);
          break;
        case 5:
          this.NativeBaseUrl = Environment.EndsSlashDelete(value);
          if (!string.IsNullOrEmpty(value))
            break;
          this.NativeBaseUrl = Environment.EndsSlashDelete("https://production-alchemist.nativebase.gu3.jp");
          DebugUtility.LogError("nativebase_url is Empty. Please set the value with the management tool.");
          break;
        case 6:
          this.LogCollectionUrl = Environment.EndsSlashDelete(value);
          break;
        case 7:
          this.DLHost = value;
          break;
        case 8:
          this.SiteHost = value;
          break;
        case 9:
          this.NewsHost = value;
          break;
        case 10:
          this.Assets = value;
          break;
        case 11:
          this.AssetsEx = value;
          break;
        case 12:
          this.MasterDigest = value;
          break;
        case 13:
          this.QuestDigest = value;
          break;
        case 14:
          this.Pub = value;
          break;
        case 15:
          this.PubU = value;
          break;
        case 16:
          this.UseAppguard = int.Parse(value);
          break;
        case 17:
          this.EnvironmentFlag = (Environment.EnvironmentFlagBit) int.Parse(value);
          Environment.ProcessMsgPackEncryptionEnvFlags(this.EnvironmentFlag);
          break;
        case 18:
          this.EnvFlg2 = Environment.ProcessMsgPackEncryptionEnvFlags2(value);
          break;
        case 19:
          this.BattleVersion = value;
          break;
      }
    }

    public static void ProcessMsgPackEncryptionEnvFlags(Environment.EnvironmentFlagBit envFlag)
    {
      if ((envFlag & Environment.EnvironmentFlagBit.ENV_FLG_FORCE_NO_SERIALIZATION) == Environment.EnvironmentFlagBit.ENV_FLG_FORCE_NO_SERIALIZATION)
      {
        DebugUtility.LogWarning("Forcing serialization off");
        GameUtility.Config_UseSerializedParams.Value = false;
        GlobalVars.SelectedSerializeCompressMethod = EncodingTypes.ESerializeCompressMethod.JSON;
        GlobalVars.SelectedSerializeCompressMethodWasNodeSet = true;
      }
      if ((envFlag & Environment.EnvironmentFlagBit.ENV_FLG_FORCE_NO_ENCRYPTION) != Environment.EnvironmentFlagBit.ENV_FLG_FORCE_NO_ENCRYPTION)
        return;
      DebugUtility.LogWarning("Forcing encryption off");
      GameUtility.Config_UseEncryption.Value = false;
    }

    public static string ProcessMsgPackEncryptionEnvFlags2(string value)
    {
      return (~Convert.ToUInt32(value, 16)).ToString("x");
    }

    private static string EndsSlashDelete(string value)
    {
      if (string.IsNullOrEmpty(value))
        return string.Empty;
      if (value.EndsWith("/"))
        value = value.Substring(0, value.Length - 1);
      return value;
    }

    public static Configuration.Builder<Environment> SetEnvironment(
      Configuration.Builder<Environment> builder)
    {
      string url = Environment.EndsSlashDelete(SRPG.Network.GetDefaultHostConfigured()) + "/chkver2";
      return builder.SetEnvironment(url);
    }

    [Flags]
    public enum EnvironmentFlagBit
    {
      ENV_FLG_NONE = 0,
      ENV_FLG_DLC_DOWNLOAD_OLD = 1,
      ENV_FLG_FORCE_SERIALIZATION = 2,
      ENV_FLG_FORCE_ENCRYPTION = 4,
      ENV_FLG_FORCE_NO_SERIALIZATION = 8,
      ENV_FLG_FORCE_NO_ENCRYPTION = 16, // 0x00000010
      ENV_FLG_RANKMATCH = 32, // 0x00000020
      ENV_FLG_DLC_CHECKBOX_OFF = 64, // 0x00000040
      ENV_FLG_PHOTONVERSION_OFF = 128, // 0x00000080
    }
  }
}
