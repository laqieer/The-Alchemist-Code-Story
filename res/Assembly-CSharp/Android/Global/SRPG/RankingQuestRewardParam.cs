// Decompiled with JetBrains decompiler
// Type: SRPG.RankingQuestRewardParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System;

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
      if ((UnityEngine.Object) instanceDirect == (UnityEngine.Object) null)
        return (RankingQuestRewardParam) null;
      if (instanceDirect.RankingQuestRewardParams == null)
        return (RankingQuestRewardParam) null;
      return instanceDirect.RankingQuestRewardParams.Find((Predicate<RankingQuestRewardParam>) (param => param.id == id));
    }
  }
}
