// Decompiled with JetBrains decompiler
// Type: SRPG.GuildData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;

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
    }

    public long UniqueID
    {
      get
      {
        return this.mUniqueID;
      }
    }

    public string CreatedUid
    {
      get
      {
        return this.mCreatedUid;
      }
    }

    public string Name
    {
      get
      {
        return this.mName;
      }
      set
      {
        this.mName = value;
      }
    }

    public string Emblem
    {
      get
      {
        return this.mEmblem;
      }
      set
      {
        this.mEmblem = value;
      }
    }

    public string Board
    {
      get
      {
        return this.mBoard;
      }
      set
      {
        this.mBoard = value;
      }
    }

    public GuildFacilityData[] Facilities
    {
      get
      {
        return this.mFacilities;
      }
    }

    public long CreatedAt
    {
      get
      {
        return this.mCreatedAt;
      }
    }

    public GuildEntryConditions EntryConditions
    {
      get
      {
        return this.mEntryConditions;
      }
      set
      {
        this.mEntryConditions = value;
      }
    }

    public GuildMemberData[] Members
    {
      get
      {
        return this.mMembers;
      }
    }

    public string[] HaveAwards
    {
      get
      {
        return this.mHaveAwards;
      }
    }

    public int MemberMax
    {
      get
      {
        return this.mMemberCountMax;
      }
    }

    public int SubMasterCountMax
    {
      get
      {
        return this.mSubMasterCountMax;
      }
    }

    public int MemberCount
    {
      get
      {
        if (this.mMemberCount > 0)
          return this.mMemberCount;
        return this.mMembers.Length;
      }
    }

    public GuildMemberData GuildMaster
    {
      get
      {
        if (this.Members == null)
          return (GuildMemberData) null;
        return Array.Find<GuildMemberData>(this.Members, (Predicate<GuildMemberData>) (member => member.RoleId == GuildMemberData.eRole.MASTAER));
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
        guildData.mHaveAwards = original.HaveAwards.Clone() as string[];
      }
      return guildData;
    }

    public static Dictionary<int, string> CreateConditionsLvTable(int min_lv, int lv_distance)
    {
      Dictionary<int, string> dictionary = new Dictionary<int, string>();
      int playerLevelCap = MonoSingleton<GameManager>.Instance.MasterParam.GetPlayerLevelCap();
      int key = min_lv;
      bool flag = false;
      dictionary.Add(0, LocalizedText.Get("sys.GUILD_ENTRY_CONDITIONS_LV0"));
      while (!flag)
      {
        if (key >= playerLevelCap)
        {
          dictionary.Add(playerLevelCap, playerLevelCap.ToString() + "以上");
          flag = true;
        }
        else
          dictionary.Add(key, key.ToString() + "以上");
        key += lv_distance;
      }
      return dictionary;
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
    }
  }
}
