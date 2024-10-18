// Decompiled with JetBrains decompiler
// Type: SRPG.VersusRankMissionParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class VersusRankMissionParam
  {
    protected string localizedNameID;
    private string mIName;
    private string mName;
    private string mExpire;
    private RankMatchMissionType mType;
    private string mSVal;
    private int mIVal;
    private string mRewardId;

    protected void localizeFields(string language)
    {
      this.init();
      this.mName = LocalizedText.SGGet(language, GameUtility.LocalizedMasterParamFileName, this.localizedNameID);
    }

    protected void init()
    {
      this.localizedNameID = this.GetType().GenerateLocalizedID(this.mIName, "NAME");
    }

    public void Deserialize(string language, JSON_VersusRankMissionParam json)
    {
      this.Deserialize(json);
      this.localizeFields(language);
    }

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
