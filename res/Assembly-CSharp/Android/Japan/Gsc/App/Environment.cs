// Decompiled with JetBrains decompiler
// Type: Gsc.App.Environment
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using SRPG;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Gsc.App
{
  [StructLayout(LayoutKind.Sequential, Size = 1)]
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

    public string Digest { get; set; }

    public string Pub { get; set; }

    public string PubU { get; set; }

    public Network.EErrCode Stat { get; set; }

    public string StatMsg { get; set; }

    public string StatCode { get; set; }

    public long ServerTime { get; set; }

    public int UseAppguard { get; set; }

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
        Environment.\u003C\u003Ef__switch\u0024map2 = new Dictionary<string, int>(17)
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
            "digest",
            12
          },
          {
            "pub",
            13
          },
          {
            "pub_u",
            14
          },
          {
            "use_appguard",
            15
          },
          {
            "env_flg",
            16
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
          this.Stat = (Network.EErrCode) int.Parse(value);
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
          this.Digest = value;
          break;
        case 13:
          this.Pub = value;
          break;
        case 14:
          this.PubU = value;
          break;
        case 15:
          this.UseAppguard = int.Parse(value);
          break;
        case 16:
          this.EnvironmentFlag = (Environment.EnvironmentFlagBit) int.Parse(value);
          break;
      }
    }

    private static string EndsSlashDelete(string value)
    {
      if (value.EndsWith("/"))
        value = value.Substring(0, value.Length - 1);
      return value;
    }

    public static Configuration.Builder<Environment> SetEnvironment(Configuration.Builder<Environment> builder)
    {
      string url = Environment.EndsSlashDelete(Network.GetDefaultHostConfigured()) + "/chkver2";
      return builder.SetEnvironment(url);
    }

    [Flags]
    public enum EnvironmentFlagBit
    {
      ENV_FLG_NONE = 0,
      ENV_FLG_DLC_DOWNLOAD_OLD = 1,
    }
  }
}
