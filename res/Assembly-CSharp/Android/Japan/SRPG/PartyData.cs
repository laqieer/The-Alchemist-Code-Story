﻿// Decompiled with JetBrains decompiler
// Type: SRPG.PartyData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;

namespace SRPG
{
  public class PartyData
  {
    private int mMAX_UNIT;
    private int mMAX_MAINMEMBER;
    private int mMAX_SUBMEMBER;
    private int mMAINMEMBER_START;
    private int mMAINMEMBER_END;
    private int mSUBMEMBER_START;
    private int mSUBMEMBER_END;
    private int mVSWAITMEMBER_START;
    private int mVSWAITMEMBER_END;
    private string mName;
    private long[] mUniqueIDs;
    private int mLeaderIndex;

    public PartyData(PlayerPartyTypes type)
    {
      switch (type)
      {
        case PlayerPartyTypes.Tower:
          this.mMAX_UNIT = 7;
          this.mMAX_MAINMEMBER = 5;
          this.mMAX_SUBMEMBER = this.MAX_UNIT - this.MAX_MAINMEMBER;
          this.mMAINMEMBER_START = 0;
          this.mMAINMEMBER_END = this.MAX_MAINMEMBER - 1;
          this.mSUBMEMBER_START = this.MAX_MAINMEMBER;
          this.mSUBMEMBER_END = this.SUBMEMBER_START + this.MAX_SUBMEMBER - 1;
          break;
        case PlayerPartyTypes.Versus:
          this.mMAX_UNIT = 5;
          this.mMAX_MAINMEMBER = 5;
          this.mMAX_SUBMEMBER = this.MAX_UNIT - this.MAX_MAINMEMBER;
          this.mMAINMEMBER_START = 0;
          this.mMAINMEMBER_END = this.MAX_MAINMEMBER - 1;
          this.mSUBMEMBER_START = this.MAX_MAINMEMBER;
          this.mSUBMEMBER_END = this.SUBMEMBER_START + this.MAX_SUBMEMBER - 1;
          this.mVSWAITMEMBER_START = 3;
          this.mVSWAITMEMBER_END = this.mMAX_UNIT;
          break;
        case PlayerPartyTypes.MultiTower:
          this.mMAX_UNIT = 3;
          this.mMAX_MAINMEMBER = 2;
          this.mMAX_SUBMEMBER = this.MAX_UNIT - this.MAX_MAINMEMBER;
          this.mMAINMEMBER_START = 0;
          this.mMAINMEMBER_END = this.MAX_MAINMEMBER - 1;
          this.mSUBMEMBER_START = this.MAX_MAINMEMBER;
          this.mSUBMEMBER_END = this.SUBMEMBER_START + this.MAX_SUBMEMBER - 1;
          break;
        case PlayerPartyTypes.Ordeal:
          this.mMAX_UNIT = 4;
          this.mMAX_MAINMEMBER = 4;
          this.mMAX_SUBMEMBER = this.MAX_UNIT - this.MAX_MAINMEMBER;
          this.mMAINMEMBER_START = 0;
          this.mMAINMEMBER_END = this.MAX_MAINMEMBER - 1;
          this.mSUBMEMBER_START = this.MAX_MAINMEMBER;
          this.mSUBMEMBER_END = this.SUBMEMBER_START + this.MAX_SUBMEMBER - 1;
          break;
        case PlayerPartyTypes.RankMatch:
          this.mMAX_UNIT = 5;
          this.mMAX_MAINMEMBER = 5;
          this.mMAX_SUBMEMBER = this.MAX_UNIT - this.MAX_MAINMEMBER;
          this.mMAINMEMBER_START = 0;
          this.mMAINMEMBER_END = this.MAX_MAINMEMBER - 1;
          this.mSUBMEMBER_START = this.MAX_MAINMEMBER;
          this.mSUBMEMBER_END = this.SUBMEMBER_START + this.MAX_SUBMEMBER - 1;
          this.mVSWAITMEMBER_START = 3;
          this.mVSWAITMEMBER_END = this.mMAX_UNIT;
          break;
        case PlayerPartyTypes.Raid:
          this.mMAX_UNIT = 7;
          this.mMAX_MAINMEMBER = 5;
          this.mMAX_SUBMEMBER = this.MAX_UNIT - this.MAX_MAINMEMBER;
          this.mMAINMEMBER_START = 0;
          this.mMAINMEMBER_END = this.MAX_MAINMEMBER - 1;
          this.mSUBMEMBER_START = this.MAX_MAINMEMBER;
          this.mSUBMEMBER_END = this.SUBMEMBER_START + this.MAX_SUBMEMBER - 1;
          break;
        default:
          this.mMAX_UNIT = 6;
          this.mMAX_MAINMEMBER = 4;
          this.mMAX_SUBMEMBER = this.MAX_UNIT - this.MAX_MAINMEMBER;
          this.mMAINMEMBER_START = 0;
          this.mMAINMEMBER_END = this.MAX_MAINMEMBER - 1;
          this.mSUBMEMBER_START = this.MAX_MAINMEMBER;
          this.mSUBMEMBER_END = this.SUBMEMBER_START + this.MAX_SUBMEMBER - 1;
          break;
      }
      this.mUniqueIDs = new long[this.MAX_UNIT];
    }

    public int MAX_UNIT
    {
      get
      {
        return this.mMAX_UNIT;
      }
    }

    public int MAX_MAINMEMBER
    {
      get
      {
        return this.mMAX_MAINMEMBER;
      }
    }

    public int MAX_SUBMEMBER
    {
      get
      {
        return this.mMAX_SUBMEMBER;
      }
    }

    public int MAINMEMBER_START
    {
      get
      {
        return this.mMAINMEMBER_START;
      }
    }

    public int MAINMEMBER_END
    {
      get
      {
        return this.mMAINMEMBER_END;
      }
    }

    public int SUBMEMBER_START
    {
      get
      {
        return this.mSUBMEMBER_START;
      }
    }

    public int SUBMEMBER_END
    {
      get
      {
        return this.mSUBMEMBER_END;
      }
    }

    public int VSWAITMEMBER_START
    {
      get
      {
        return this.mVSWAITMEMBER_START;
      }
    }

    public int VSWAITMEMBER_END
    {
      get
      {
        return this.mVSWAITMEMBER_END;
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

    public int Num
    {
      get
      {
        int num = 0;
        for (int index = 0; index < this.mUniqueIDs.Length; ++index)
        {
          if (this.mUniqueIDs[index] != 0L)
            ++num;
        }
        return num;
      }
    }

    public int LeaderIndex
    {
      get
      {
        return this.mLeaderIndex;
      }
    }

    public PlayerPartyTypes PartyType { set; get; }

    public bool Selected { set; get; }

    public bool IsDefense { set; get; }

    public static PlayerPartyTypes GetPartyTypeFromString(string ptype)
    {
      if (ptype != null)
      {
        // ISSUE: reference to a compiler-generated field
        if (PartyData.\u003C\u003Ef__switch\u0024map0 == null)
        {
          // ISSUE: reference to a compiler-generated field
          PartyData.\u003C\u003Ef__switch\u0024map0 = new Dictionary<string, int>(12)
          {
            {
              "norm",
              0
            },
            {
              "ev",
              1
            },
            {
              "mul",
              2
            },
            {
              "col",
              3
            },
            {
              "coldef",
              4
            },
            {
              "chara",
              5
            },
            {
              "tower",
              6
            },
            {
              "vs",
              7
            },
            {
              "multi_tw",
              8
            },
            {
              "ordeal",
              9
            },
            {
              "rm",
              10
            },
            {
              "raidboss",
              11
            }
          };
        }
        int num;
        // ISSUE: reference to a compiler-generated field
        if (PartyData.\u003C\u003Ef__switch\u0024map0.TryGetValue(ptype, out num))
        {
          switch (num)
          {
            case 0:
              return PlayerPartyTypes.Normal;
            case 1:
              return PlayerPartyTypes.Event;
            case 2:
              return PlayerPartyTypes.Multiplay;
            case 3:
              return PlayerPartyTypes.Arena;
            case 4:
              return PlayerPartyTypes.ArenaDef;
            case 5:
              return PlayerPartyTypes.Character;
            case 6:
              return PlayerPartyTypes.Tower;
            case 7:
              return PlayerPartyTypes.Versus;
            case 8:
              return PlayerPartyTypes.MultiTower;
            case 9:
              return PlayerPartyTypes.Ordeal;
            case 10:
              return PlayerPartyTypes.RankMatch;
            case 11:
              return PlayerPartyTypes.Raid;
          }
        }
      }
      return PlayerPartyTypes.Normal;
    }

    public static string GetStringFromPartyType(PlayerPartyTypes type)
    {
      switch (type)
      {
        case PlayerPartyTypes.Normal:
          return "norm";
        case PlayerPartyTypes.Event:
          return "ev";
        case PlayerPartyTypes.Multiplay:
          return "mul";
        case PlayerPartyTypes.Arena:
          return "col";
        case PlayerPartyTypes.ArenaDef:
          return "coldef";
        case PlayerPartyTypes.Character:
          return "chara";
        case PlayerPartyTypes.Tower:
          return "tower";
        case PlayerPartyTypes.Versus:
          return "vs";
        case PlayerPartyTypes.MultiTower:
          return "multi_tw";
        case PlayerPartyTypes.Ordeal:
          return "ordeal";
        case PlayerPartyTypes.RankMatch:
          return "rm";
        case PlayerPartyTypes.Raid:
          return "raidboss";
        default:
          return "norm";
      }
    }

    public void Deserialize(Json_Party json)
    {
      this.Reset();
      if (json == null)
        throw new InvalidCastException();
      this.mLeaderIndex = 0;
      for (int index = 0; index < json.units.Length && this.mUniqueIDs.Length > index; ++index)
        this.mUniqueIDs[index] = json.units[index];
      this.Selected = json.flg_sel != 0;
      this.IsDefense = json.flg_seldef != 0;
    }

    public void Reset()
    {
      Array.Clear((Array) this.mUniqueIDs, 0, this.mUniqueIDs.Length);
    }

    public void SetParty(PartyData org)
    {
      if (org == null)
        return;
      this.Reset();
      for (int index = 0; index < this.MAX_UNIT; ++index)
        this.mUniqueIDs[index] = org.GetUnitUniqueID(index);
    }

    public void SetUnitUniqueID(int index, long uniqueid)
    {
      if (index < 0 || this.MAX_UNIT <= index)
        return;
      this.mUniqueIDs[index] = uniqueid;
    }

    public long GetUnitUniqueID(int index)
    {
      if (index < 0 || this.MAX_UNIT <= index)
        return 0;
      return this.mUniqueIDs[index];
    }

    public bool IsPartyUnit(long uniqueid)
    {
      return this.FindPartyUnit(uniqueid) != -1;
    }

    public int FindPartyUnit(long uniqueid)
    {
      for (int index = 0; index < this.MAX_UNIT; ++index)
      {
        if (this.mUniqueIDs[index] == uniqueid)
          return index;
      }
      return -1;
    }

    public bool IsSub(long uniqueid)
    {
      return this.FindPartyUnit(uniqueid) >= this.MAX_MAINMEMBER;
    }

    public bool IsSub(UnitData unit)
    {
      return this.IsSub(unit.UniqueID);
    }

    public UnitData[] ToUnits()
    {
      UnitData[] unitDataArray = new UnitData[this.MAX_UNIT];
      for (int index = 0; index < this.MAX_UNIT; ++index)
        unitDataArray[index] = this.mUniqueIDs[index] > 0L ? MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(this.mUniqueIDs[index]) : (UnitData) null;
      return unitDataArray;
    }
  }
}
