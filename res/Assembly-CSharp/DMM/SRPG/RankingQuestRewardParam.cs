// Decompiled with JetBrains decompiler
// Type: SRPG.RankingQuestRewardParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;

#nullable disable
namespace SRPG
{
  public class RankingQuestRewardParam
  {
    public int id;
    public RankingQuestRewardType type;
    public string iname;
    public int num;

    public bool Deserialize(JSON_RankingQuestRewardParam json)
    {
      this.id = json.id;
      try
      {
        this.type = (RankingQuestRewardType) Enum.Parse(typeof (RankingQuestRewardType), json.type);
      }
      catch
      {
        DebugUtility.LogError("定義されていない列挙値が指定されようとしました");
      }
      this.iname = json.iname;
      this.num = json.num;
      return true;
    }

    public static RankingQuestRewardParam FindByID(int id)
    {
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) instanceDirect, (UnityEngine.Object) null))
        return (RankingQuestRewardParam) null;
      return instanceDirect.RankingQuestRewardParams == null ? (RankingQuestRewardParam) null : instanceDirect.RankingQuestRewardParams.Find((Predicate<RankingQuestRewardParam>) (param => param.id == id));
    }
  }
}
