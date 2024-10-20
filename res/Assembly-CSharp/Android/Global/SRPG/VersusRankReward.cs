﻿// Decompiled with JetBrains decompiler
// Type: SRPG.VersusRankReward
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class VersusRankReward
  {
    private RewardType mType;
    private string mIName;
    private int mNum;

    public RewardType Type
    {
      get
      {
        return this.mType;
      }
    }

    public string IName
    {
      get
      {
        return this.mIName;
      }
    }

    public int Num
    {
      get
      {
        return this.mNum;
      }
    }

    public bool Deserialize(JSON_VersusRankRewardRewardParam json)
    {
      this.mType = (RewardType) json.item_type;
      this.mIName = json.item_iname;
      this.mNum = json.item_num;
      return true;
    }
  }
}