// Decompiled with JetBrains decompiler
// Type: SRPG.GuildData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class GuildData
  {
    private long mUniqueID;
    private string mCreatedUid;
    private string mName;
    private string mEmblem;
    private string mBoard;
    private int mMemberCount;
    private int mMemberCountMax;
    private int mSubMasterCountMax;
    private GuildEntryConditions mEntryConditions;
    private GuildMemberData[] mMembers;
    private string[] mHaveAwards;
    private GuildFacilityData[] mFacilities;
    private long mCreatedAt;
    private int mGvGJoinStatus;
    private int mGvGRate;

    public GuildData()
    {
      this.mUniqueID = 0L;
      this.mCreatedUid = string.Empty;
      this.mName = string.Empty;
      this.mEmblem = string.Empty;
      this.mBoard = string.Empty;
      this.mMemberCount = 0;
      this.mMemberCountMax = 0;
      this.mSubMasterCountMax = 0;
      this.mEntryConditions = (GuildEntryConditions) null;
      this.mMembers = (GuildMemberData[]) null;
      this.mHaveAwards = new string[0];
      this.mFacilities = (GuildFacilityData[]) null;
      this.mCreatedAt = 0L;
      this.mGvGJoinStatus = 1;
      this.mGvGRate = 0;
    }

    public long UniqueID => this.mUniqueID;

    public string CreatedUid => this.mCreatedUid;

    public string Name
    {
      get => this.mName;
      set => this.mName = value;
    }

    public string Emblem
    {
      get => this.mEmblem;
      set => this.mEmblem = value;
    }

    public string Board
    {
      get => this.mBoard;
      set => this.mBoard = value;
    }

    public GuildFacilityData[] Facilities => this.mFacilities;

    public long CreatedAt => this.mCreatedAt;

    public int GvGJoinStatus
    {
      get => this.mGvGJoinStatus;
      set => this.mGvGJoinStatus = value;
    }

    public int GvGRate => this.mGvGRate;

    public GuildEntryConditions EntryConditions
    {
      get => this.mEntryConditions;
      set => this.mEntryConditions = value;
    }

    public GuildMemberData[] Members => this.mMembers;

    public string[] HaveAwards => this.mHaveAwards;

    public int MemberMax => this.mMemberCountMax;

    public int SubMasterCountMax => this.mSubMasterCountMax;

    public int MemberCount => this.mMemberCount > 0 ? this.mMemberCount : this.mMembers.Length;

    public GuildMemberData GuildMaster
    {
      get
      {
        return this.Members == null ? (GuildMemberData) null : Array.Find<GuildMemberData>(this.Members, (Predicate<GuildMemberData>) (member => member.RoleId == GuildMemberData.eRole.MASTAER));
      }
    }

    public bool Deserialize(JSON_Guild json)
    {
      this.mUniqueID = json.id;
      this.mCreatedUid = json.created_uid;
      this.mName = json.name;
      this.mEmblem = json.award_id;
      this.mBoard = !string.IsNullOrEmpty(json.board) ? json.board : string.Empty;
      this.mMemberCount = json.count;
      this.mMemberCountMax = json.max_count;
      this.mSubMasterCountMax = json.submaster_count;
      this.mCreatedAt = json.created_at;
      this.mGvGJoinStatus = json.gvg_join_status;
      this.mGvGRate = json.gvg_rate;
      this.mEntryConditions = new GuildEntryConditions();
      this.mEntryConditions.Deserialize(json.guild_subscription_condition);
      this.mMembers = (GuildMemberData[]) null;
      if (json.guild_member != null)
      {
        this.mMembers = new GuildMemberData[json.guild_member.Length];
        for (int index = 0; index < json.guild_member.Length; ++index)
        {
          this.mMembers[index] = new GuildMemberData();
          this.mMembers[index].Deserialize(json.guild_member[index]);
        }
      }
      this.mHaveAwards = new string[0];
      if (json.have_awards != null)
      {
        this.mHaveAwards = new string[json.have_awards.Length];
        json.have_awards.CopyTo((Array) this.mHaveAwards, 0);
      }
      this.mFacilities = (GuildFacilityData[]) null;
      if (json.facilities != null)
      {
        this.mFacilities = new GuildFacilityData[json.facilities.Length];
        for (int index = 0; index < json.facilities.Length; ++index)
        {
          this.mFacilities[index] = new GuildFacilityData();
          this.mFacilities[index].Deserialize(json.facilities[index]);
        }
      }
      return true;
    }

    public static GuildData CreateDefault()
    {
      GuildData guildData = new GuildData();
      guildData.mUniqueID = 0L;
      guildData.mName = string.Empty;
      guildData.mBoard = string.Empty;
      guildData.mEmblem = string.Empty;
      GuildEmblemParam[] conditionsGuildEmblemes = MonoSingleton<GameManager>.Instance.MasterParam.GetNoConditionsGuildEmblemes();
      if (conditionsGuildEmblemes != null && conditionsGuildEmblemes.Length > 0)
        guildData.mEmblem = conditionsGuildEmblemes[0].Image;
      guildData.mEntryConditions = new GuildEntryConditions();
      guildData.mEntryConditions.LowerLevel = 0;
      return guildData;
    }

    public static GuildData Clone(GuildData original)
    {
      GuildData guildData = GuildData.CreateDefault();
      if (original != null)
      {
        guildData.SetParam(original.UniqueID, original.Name, original.EntryConditions);
        guildData.Emblem = original.Emblem;
        guildData.Board = original.Board;
        guildData.EntryConditions.Comment = original.EntryConditions.Comment;
        guildData.EntryConditions.IsAutoApproval = original.EntryConditions.IsAutoApproval;
        guildData.EntryConditions.Policy = original.EntryConditions.Policy;
        guildData.mHaveAwards = original.HaveAwards.Clone() as string[];
        guildData.GvGJoinStatus = original.GvGJoinStatus;
      }
      return guildData;
    }

    public static Dictionary<int, string> CreateConditionsLvTable(int min_lv, int lv_distance)
    {
      Dictionary<int, string> conditionsLvTable = new Dictionary<int, string>();
      int playerLevelCap = MonoSingleton<GameManager>.Instance.MasterParam.GetPlayerLevelCap();
      int key = min_lv;
      bool flag = false;
      conditionsLvTable.Add(0, LocalizedText.Get("sys.GUILD_ENTRY_CONDITIONS_LV0"));
      while (!flag)
      {
        if (key >= playerLevelCap)
        {
          conditionsLvTable.Add(playerLevelCap, playerLevelCap.ToString() + "以上");
          flag = true;
        }
        else
          conditionsLvTable.Add(key, key.ToString() + "以上");
        key += lv_distance;
      }
      return conditionsLvTable;
    }

    public void CopyParam(GuildData guild)
    {
      this.mName = guild.Name;
      this.mEmblem = guild.Emblem;
      this.mBoard = guild.Board;
      this.mEntryConditions = guild.EntryConditions;
    }

    public bool IsMatchConditions(PlayerData target_player)
    {
      return this.mEntryConditions.LowerLevel <= target_player.CalcLevel();
    }

    public void SetParam(long gid, string guild_name, GuildEntryConditions conditions)
    {
      this.mUniqueID = gid;
      this.mName = guild_name;
      this.mEntryConditions = new GuildEntryConditions();
      this.mEntryConditions.LowerLevel = conditions.LowerLevel;
      this.mEntryConditions.IsAutoApproval = conditions.IsAutoApproval;
      this.mEntryConditions.Policy = conditions.Policy;
    }

    public GuildFacilityData GetFacilityData(GuildFacilityParam.eFacilityType facility_type)
    {
      GuildFacilityData facilityData = (GuildFacilityData) null;
      if (this.mFacilities == null)
        return facilityData;
      for (int index = 0; index < this.mFacilities.Length; ++index)
      {
        GuildFacilityData mFacility = this.mFacilities[index];
        if (mFacility != null && mFacility.Param != null && mFacility.Param.Type == facility_type)
        {
          facilityData = mFacility;
          break;
        }
      }
      return facilityData;
    }
  }
}
