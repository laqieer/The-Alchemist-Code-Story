// Decompiled with JetBrains decompiler
// Type: SRPG.TrophyObjective
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

namespace SRPG
{
  public class TrophyObjective
  {
    public TrophyParam Param;
    public int index;
    public TrophyConditionTypes type;
    public List<string> sval;
    public int ival;

    public string sval_base
    {
      get
      {
        if (this.sval != null && 0 < this.sval.Count)
          return this.sval[0];
        return string.Empty;
      }
    }

    public int RequiredCount
    {
      get
      {
        TrophyConditionTypes type = this.type;
        switch (type)
        {
          case TrophyConditionTypes.unlock_tobira_unit:
          case TrophyConditionTypes.complete_all_quest_mission_total:
label_7:
            return 1;
          case TrophyConditionTypes.envy_unlock_unit:
          case TrophyConditionTypes.sloth_unlock_unit:
          case TrophyConditionTypes.lust_unlock_unit:
          case TrophyConditionTypes.greed_unlock_unit:
          case TrophyConditionTypes.wrath_unlock_unit:
          case TrophyConditionTypes.gluttonny_unlock_unit:
          case TrophyConditionTypes.pride_unlock_unit:
            if (string.IsNullOrEmpty(this.sval_base))
              return this.ival;
            return 1;
          default:
            switch (type - 17)
            {
              case TrophyConditionTypes.none:
              case TrophyConditionTypes.getitem:
              case TrophyConditionTypes.playerlv:
              case TrophyConditionTypes.winelite:
              case TrophyConditionTypes.multiplay:
              case TrophyConditionTypes.buygold:
                goto label_7;
              case TrophyConditionTypes.winquest:
                return 0;
              default:
                switch (type - 40)
                {
                  case TrophyConditionTypes.none:
                  case TrophyConditionTypes.winquest:
                  case TrophyConditionTypes.killenemy:
                  case TrophyConditionTypes.getitem:
                  case TrophyConditionTypes.playerlv:
                  case TrophyConditionTypes.winelite:
                    goto label_7;
                  default:
                    switch (type - 58)
                    {
                      case TrophyConditionTypes.none:
                      case TrophyConditionTypes.winquest:
                      case TrophyConditionTypes.getitem:
                      case TrophyConditionTypes.playerlv:
                        goto label_7;
                      default:
                        switch (type - 78)
                        {
                          case TrophyConditionTypes.none:
                          case TrophyConditionTypes.killenemy:
                          case TrophyConditionTypes.getitem:
                            goto label_7;
                          default:
                            if (type != TrophyConditionTypes.playerlv && type != TrophyConditionTypes.makeabilitylevel)
                              return this.ival;
                            goto label_7;
                        }
                    }
                }
            }
        }
      }
    }
  }
}
