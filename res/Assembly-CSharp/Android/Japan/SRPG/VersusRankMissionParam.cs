﻿// Decompiled with JetBrains decompiler
// Type: SRPG.VersusRankMissionParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class VersusRankMissionParam
  {
    private string mIName;
    private string mName;
    private string mExpire;
    private RankMatchMissionType mType;
    private string mSVal;
    private int mIVal;
    private string mRewardId;

    public string IName
    {
      get
      {
        return this.mIName;
      }
    }

    public string Name
    {
      get
      {
        return this.mName;
      }
    }

    public string Expire
    {
      get
      {
        return this.mExpire;
      }
    }

    public RankMatchMissionType Type
    {
      get
      {
        return this.mType;
      }
    }

    public string SVal
    {
      get
      {
        return this.mSVal;
      }
    }

    public int IVal
    {
      get
      {
        return this.mIVal;
      }
    }

    public string RewardId
    {
      get
      {
        return this.mRewardId;
      }
    }

    public bool Deserialize(JSON_VersusRankMissionParam json)
    {
      if (json == null)
        return false;
      this.mIName = json.iname;
      this.mName = json.name;
      this.mExpire = json.expr;
      this.mType = (RankMatchMissionType) json.type;
      this.mSVal = json.sval;
      this.mIVal = json.ival;
      this.mRewardId = json.reward_id;
      return true;
    }
  }
}
