// Decompiled with JetBrains decompiler
// Type: SRPG.QuestBonusObjective
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class QuestBonusObjective
  {
    public string item;
    public int itemNum;
    public RewardType itemType;
    public EMissionType Type;
    public string TypeParam;

    public bool IsMissionTypeExecSkill()
    {
      return this.Type == EMissionType.UseTargetSkill || this.Type == EMissionType.KillstreakByUsingTargetSkill || this.Type == EMissionType.KillstreakByUsingTargetItem;
    }
  }
}
