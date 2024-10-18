// Decompiled with JetBrains decompiler
// Type: SRPG.UnlockParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  public class UnlockParam
  {
    public string iname;
    public UnlockTargets UnlockTarget;
    public int PlayerLevel;
    public int VipRank;
    public string[] ClearQuests;
    public string OverWriteQuestText;

    public bool Deserialize(JSON_UnlockParam json)
    {
      if (json == null)
        return false;
      this.iname = json.iname;
      try
      {
        this.UnlockTarget = (UnlockTargets) Enum.Parse(typeof (UnlockTargets), json.iname);
      }
      catch (Exception ex)
      {
        return false;
      }
      this.PlayerLevel = json.lv;
      this.VipRank = json.vip;
      this.ClearQuests = json.quests;
      this.OverWriteQuestText = json.ow_qst_txt;
      return true;
    }

    public bool IsExistConds_ClearQuest()
    {
      if (this.ClearQuests == null || this.ClearQuests.Length <= 0)
        return false;
      for (int index = 0; index < this.ClearQuests.Length; ++index)
      {
        if (!string.IsNullOrEmpty(this.ClearQuests[index]))
          return true;
      }
      return false;
    }
  }
}
