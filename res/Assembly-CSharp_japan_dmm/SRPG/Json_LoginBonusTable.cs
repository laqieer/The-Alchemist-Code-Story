// Decompiled with JetBrains decompiler
// Type: SRPG.Json_LoginBonusTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  public class Json_LoginBonusTable
  {
    public int count;
    public string type;
    public string prefab;
    public string[] bonus_units;
    public int lastday;
    public int[] login_days;
    public int remain_recover;
    public int max_recover;
    public int current_month;
    public Json_LoginBonus[] bonuses;
    public Json_PremiumLoginBonus[] premium_bonuses;

    public bool IsCanRecover
    {
      get
      {
        return this.remain_recover > 0 && this.login_days != null && this.login_days.Length > 0 && TimeManager.ServerTime.Day - this.login_days.Length > 0;
      }
    }
  }
}
