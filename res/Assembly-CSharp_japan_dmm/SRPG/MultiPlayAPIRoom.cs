// Decompiled with JetBrains decompiler
// Type: SRPG.MultiPlayAPIRoom
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Security.Cryptography;
using System.Text;

#nullable disable
namespace SRPG
{
  public class MultiPlayAPIRoom
  {
    private static readonly string LOCKED = "1";
    public int roomid;
    public string comment;
    public MultiPlayAPIRoom.Quest quest;
    public string pwd_hash;
    public int limit;
    public int unitlv;
    public int clear;
    public int is_clear;
    public int floor;
    public int btl_speed;
    public int enable_auto;
    public string network_version;
    public int num;
    public MultiPlayAPIRoom.Owner owner;

    public static string CalcHash(string pwd)
    {
      if (string.IsNullOrEmpty(pwd))
        return string.Empty;
      byte[] hash = new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(pwd));
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < hash.Length; ++index)
        stringBuilder.AppendFormat("{0:x2}", (object) hash[index]);
      return stringBuilder.ToString();
    }

    public static bool IsLocked(string pwd) => pwd == MultiPlayAPIRoom.LOCKED;

    public class Quest
    {
      public string iname;
    }

    public class Owner
    {
      public string name;
      public string level;
      public Json_Unit[] units;
    }
  }
}
