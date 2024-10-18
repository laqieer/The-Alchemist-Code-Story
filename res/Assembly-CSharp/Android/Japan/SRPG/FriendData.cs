// Decompiled with JetBrains decompiler
// Type: SRPG.FriendData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  public class FriendData
  {
    public UnitData Unit;
    public FriendStates State;
    public string UID;
    public string FUID;
    public string PlayerName;
    public int PlayerLevel;
    public long LastLogin;
    public string CreatedAt;
    public bool IsFavorite;
    public string UnitID;
    public int UnitLevel;
    public int UnitRarity;
    public string SelectAward;
    public string Wish;
    public string WishStatus;
    public bool MultiPush;
    public string MultiComment;
    public ViewGuildData ViewGuild;

    public EElement UnitElement
    {
      get
      {
        UnitParam unitParam = MonoSingleton<GameManager>.Instance.GetUnitParam(this.UnitID);
        if (unitParam != null)
          return unitParam.element;
        return EElement.None;
      }
    }

    public void Deserialize(Json_Friend json)
    {
      if (json == null)
        throw new InvalidJSONException();
      this.UID = json.uid;
      this.FUID = json.fuid;
      this.PlayerName = json.name;
      this.PlayerLevel = json.lv;
      this.LastLogin = json.lastlogin;
      this.CreatedAt = json.created_at;
      this.IsFavorite = json.is_favorite != 0;
      this.SelectAward = json.award;
      this.Wish = json.wish;
      this.WishStatus = json.status;
      this.MultiPush = json.is_multi_push == 1;
      this.MultiComment = json.multi_comment;
      if (json.guild != null)
      {
        this.ViewGuild = new ViewGuildData();
        this.ViewGuild.Deserialize(json.guild);
      }
      if (json.unit != null)
      {
        this.UnitID = json.unit.iname;
        this.UnitLevel = json.unit.lv;
        this.UnitRarity = json.unit.rare;
        UnitData unitData = new UnitData();
        unitData.Deserialize(json.unit);
        this.Unit = unitData;
      }
      switch (json.type)
      {
        case "friend":
          this.State = FriendStates.Friend;
          break;
        case "follow":
          this.State = FriendStates.Follow;
          break;
        case "follower":
          this.State = FriendStates.Follwer;
          break;
        default:
          this.State = FriendStates.None;
          break;
      }
    }

    public bool IsFriend()
    {
      return this.State == FriendStates.Friend;
    }

    public int GetCost()
    {
      if (this.Unit == null)
        return 0;
      int num = this.Unit.Lv * (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.SupportCost;
      if (this.State != FriendStates.Friend)
        num *= 2;
      return num;
    }
  }
}
