// Decompiled with JetBrains decompiler
// Type: SRPG.TrophyObjective
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;
using System.Text;

#nullable disable
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
      get => this.sval != null && 0 < this.sval.Count ? this.sval[0] : string.Empty;
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
label_8:
            return 1;
          case TrophyConditionTypes.envy_unlock_unit:
          case TrophyConditionTypes.sloth_unlock_unit:
          case TrophyConditionTypes.lust_unlock_unit:
          case TrophyConditionTypes.greed_unlock_unit:
          case TrophyConditionTypes.wrath_unlock_unit:
          case TrophyConditionTypes.gluttonny_unlock_unit:
          case TrophyConditionTypes.pride_unlock_unit:
            return string.IsNullOrEmpty(this.sval_base) ? this.ival : 1;
          default:
            switch (type - 131)
            {
              case TrophyConditionTypes.none:
              case TrophyConditionTypes.winquest:
              case TrophyConditionTypes.killenemy:
              case TrophyConditionTypes.getitem:
              case TrophyConditionTypes.playerlv:
              case TrophyConditionTypes.winelite:
              case TrophyConditionTypes.winevent:
              case TrophyConditionTypes.gacha:
                goto label_8;
              default:
                switch (type - 17)
                {
                  case TrophyConditionTypes.none:
                  case TrophyConditionTypes.getitem:
                  case TrophyConditionTypes.playerlv:
                  case TrophyConditionTypes.winelite:
                  case TrophyConditionTypes.multiplay:
                  case TrophyConditionTypes.buygold:
                    goto label_8;
                  case TrophyConditionTypes.winquest:
                    return 0;
                  default:
                    switch (type - 40)
                    {
                      case TrophyConditionTypes.none:
                      case TrophyConditionTypes.winquest:
                      case TrophyConditionTypes.killenemy:
                      case TrophyConditionTypes.getitem:
                      case TrophyConditionTypes.winelite:
                        goto label_8;
                      default:
                        switch (type - 58)
                        {
                          case TrophyConditionTypes.none:
                          case TrophyConditionTypes.winquest:
                          case TrophyConditionTypes.getitem:
                          case TrophyConditionTypes.playerlv:
                            goto label_8;
                          default:
                            switch (type - 78)
                            {
                              case TrophyConditionTypes.none:
                              case TrophyConditionTypes.killenemy:
                              case TrophyConditionTypes.getitem:
                                goto label_8;
                              default:
                                if (type != TrophyConditionTypes.playerlv && type != TrophyConditionTypes.makeabilitylevel)
                                  return this.ival;
                                goto label_8;
                            }
                        }
                    }
                }
            }
        }
      }
    }

    public bool ContainsSval(string value)
    {
      return this.sval != null && this.sval.Count > 0 && this.sval.Contains(value);
    }

    public int SvalCount => this.sval == null ? 0 : this.sval.Count;

    public virtual string GetDescription()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      string str1 = string.Empty;
      string str2 = string.Empty;
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      char[] chArray = new char[1]{ ',' };
      if (!string.IsNullOrEmpty(this.Param.Expr))
        return string.Format(LocalizedText.Get(this.Param.Expr), (object) this.ival);
      switch (this.type)
      {
        case TrophyConditionTypes.winquest:
          if (this.SvalCount <= 0)
            return string.Format(LocalizedText.Get("sys.TROPHY_WINQUEST_NORMAL"), (object) this.ival);
          if (this.sval.Count == 1)
          {
            QuestParam quest = instance.FindQuest(this.sval_base);
            return string.Format(LocalizedText.Get("sys.TROPHY_WINQUEST"), quest == null ? (object) ("?" + (object) this.sval) : (object) quest.name, (object) this.ival);
          }
          string empty3 = string.Empty;
          for (int index = 0; index < this.sval.Count; ++index)
          {
            QuestParam quest = instance.FindQuest(this.sval[index]);
            if (index != 0)
              empty3 += LocalizedText.Get("sys.TROPHY_OR");
            empty3 += quest == null ? "?" + this.sval[index] : quest.name;
          }
          return string.Format(LocalizedText.Get("sys.TROPHY_WINQUEST_OR"), (object) empty3, (object) this.ival);
        case TrophyConditionTypes.killenemy:
          if (this.SvalCount > 1)
          {
            string unitNames = this.GetUnitNames(this.sval);
            return string.Format(LocalizedText.Get("sys.TROPHY_KILLENEMY_OR"), (object) unitNames, (object) this.ival);
          }
          UnitParam unitParam1 = instance.GetUnitParam(this.sval_base);
          return string.Format(LocalizedText.Get("sys.TROPHY_KILLENEMY"), unitParam1 == null ? (object) ("?" + this.sval_base) : (object) unitParam1.name, (object) this.ival);
        case TrophyConditionTypes.getitem:
          if (this.SvalCount > 1)
          {
            string itemNames = this.GetItemNames(this.sval);
            return string.Format(LocalizedText.Get("sys.TROPHY_GETITEM_OR"), (object) itemNames, (object) this.ival);
          }
          ItemParam itemParam = instance.GetItemParam(this.sval_base);
          return string.Format(LocalizedText.Get("sys.TROPHY_GETITEM"), itemParam == null ? (object) ("?" + this.sval_base) : (object) itemParam.name, (object) this.ival);
        case TrophyConditionTypes.playerlv:
          return string.Format(LocalizedText.Get("sys.TROPHY_PLAYERLV"), (object) this.ival);
        case TrophyConditionTypes.winelite:
          return string.Format(LocalizedText.Get("sys.TROPHY_WINQUEST_ELITE"), (object) this.ival);
        case TrophyConditionTypes.winevent:
          return string.Format(LocalizedText.Get("sys.TROPHY_WINQUEST_EVENT"), (object) this.ival);
        case TrophyConditionTypes.gacha:
          this.CheckUnexpectedMultipleSval();
          return string.Format(LocalizedText.Get("sys.TROPHY_GACHA"), (object) this.ival);
        case TrophyConditionTypes.multiplay:
          return string.Format(LocalizedText.Get("sys.TROPHY_MULTIPLAY"), (object) this.ival);
        case TrophyConditionTypes.ability:
          return string.Format(LocalizedText.Get("sys.TROPHY_ABILITY"), (object) this.ival);
        case TrophyConditionTypes.soubi:
          return string.Format(LocalizedText.Get("sys.TROPHY_SOUBI"), (object) this.ival);
        case TrophyConditionTypes.buygold:
          return string.Format(LocalizedText.Get("sys.TROPHY_BUYGOLD"), (object) this.ival);
        case TrophyConditionTypes.vip:
          return string.Format(LocalizedText.Get("sys.TROPHY_VIP"), (object) this.ival);
        case TrophyConditionTypes.stamina:
          this.CheckUnexpectedMultipleSval();
          int num1 = int.Parse(this.sval_base.Substring(0, 2));
          int num2 = int.Parse(this.sval_base.Substring(3, 2));
          return string.Format(LocalizedText.Get("sys.TROPHY_STAMINA"), (object) num1, (object) num2);
        case TrophyConditionTypes.arena:
          return string.Format(LocalizedText.Get("sys.TROPHY_ARENA"), (object) this.ival);
        case TrophyConditionTypes.winarena:
          return string.Format(LocalizedText.Get("sys.TROPHY_WINARENA"), (object) this.ival);
        case TrophyConditionTypes.review:
          return LocalizedText.Get("sys.TROPHY_REVIEW");
        case TrophyConditionTypes.followtwitter:
          return LocalizedText.Get("sys.TROPHY_FOLLOWTWITTER");
        case TrophyConditionTypes.fggid:
          return LocalizedText.Get("sys.TROPHY_FGGID");
        case TrophyConditionTypes.unitlevel:
          if (this.SvalCount <= 0)
          {
            DebugUtility.LogError("[" + this.Param.iname + "] にはsvalが必要です。");
            return string.Empty;
          }
          if (this.SvalCount == 1)
          {
            UnitParam unitParam2 = instance.GetUnitParam(this.sval_base);
            return string.Format(LocalizedText.Get("sys.TROPHY_UNITLV"), unitParam2 == null ? (object) ("?" + this.sval_base) : (object) unitParam2.name, (object) this.ival);
          }
          string unitNames1 = this.GetUnitNames(this.sval);
          return string.Format(LocalizedText.Get("sys.TROPHY_UNITLV_OR"), (object) unitNames1, (object) this.ival);
        case TrophyConditionTypes.evolutionnum:
          if (this.SvalCount <= 0)
          {
            DebugUtility.LogError("[" + this.Param.iname + "] にはsvalが必要です。");
            return string.Empty;
          }
          if (this.SvalCount == 1)
          {
            UnitParam unitParam3 = instance.GetUnitParam(this.sval_base);
            return string.Format(LocalizedText.Get("sys.TROPHY_EVOLUTIONCNT"), unitParam3 == null ? (object) ("?" + this.sval_base) : (object) unitParam3.name, (object) (this.ival + 1));
          }
          string unitNames2 = this.GetUnitNames(this.sval);
          return string.Format(LocalizedText.Get("sys.TROPHY_EVOLUTIONCNT_OR"), (object) unitNames2, (object) (this.ival + 1));
        case TrophyConditionTypes.joblevel:
          this.CheckUnexpectedMultipleSval();
          string[] strArray1 = this.sval_base.Split(chArray);
          UnitParam unitParam4 = instance.GetUnitParam(strArray1[0]);
          JobParam jobParam1 = instance.GetJobParam(strArray1[1]);
          return string.Format(LocalizedText.Get("sys.TROPHY_JOBLV"), unitParam4 == null ? (object) ("?" + strArray1[0]) : (object) unitParam4.name, jobParam1 == null ? (object) ("?" + strArray1[1]) : (object) jobParam1.name, (object) this.ival);
        case TrophyConditionTypes.logincount:
          return string.Format(LocalizedText.Get("sys.TROPHY_LOGINCNT"), (object) this.ival);
        case TrophyConditionTypes.upunitlevel:
          this.CheckUnexpectedMultipleSval();
          return string.Format(LocalizedText.Get("sys.TROPHY_UNITLVUP"), (object) this.GetUnitName(this.sval_base), (object) this.ival);
        case TrophyConditionTypes.makeunitlevel:
          return this.SvalCount > 1 ? string.Format(LocalizedText.Get("sys.TROPHY_UNITLVMAKE_OR"), (object) this.GetUnitNames(this.sval), (object) this.ival) : string.Format(LocalizedText.Get("sys.TROPHY_UNITLVMAKE"), (object) this.GetUnitName(this.sval_base), (object) this.ival);
        case TrophyConditionTypes.unitequip:
          return this.SvalCount > 1 ? string.Format(LocalizedText.Get("sys.TROPHY_EQUIP_OR"), (object) this.GetUnitNames(this.sval), (object) this.ival) : string.Format(LocalizedText.Get("sys.TROPHY_EQUIP"), (object) this.GetUnitName(this.sval_base), (object) this.ival);
        case TrophyConditionTypes.upjoblevel:
          this.CheckUnexpectedMultipleSval();
          if (!string.IsNullOrEmpty(this.sval_base))
          {
            string[] strArray2 = this.sval_base.Split(chArray);
            UnitParam unitParam5 = instance.GetUnitParam(strArray2[0]);
            JobParam jobParam2 = instance.GetJobParam(strArray2[1]);
            str2 = unitParam5 == null ? "?" + strArray2[0] : unitParam5.name;
            str1 = jobParam2 == null ? "?" + strArray2[1] : jobParam2.name;
          }
          return string.Format(LocalizedText.Get("sys.TROPHY_JOBLVUP"), (object) str2, (object) str1, (object) this.ival);
        case TrophyConditionTypes.makejoblevel:
          this.CheckUnexpectedMultipleSval();
          if (!string.IsNullOrEmpty(this.sval_base))
          {
            string[] strArray3 = this.sval_base.Split(chArray);
            UnitParam unitParam6 = instance.GetUnitParam(strArray3[0]);
            JobParam jobParam3 = instance.GetJobParam(strArray3[1]);
            str2 = unitParam6 == null ? "?" + strArray3[0] : unitParam6.name;
            str1 = jobParam3 == null ? "?" + strArray3[1] : jobParam3.name;
          }
          return string.Format(LocalizedText.Get("sys.TROPHY_JOBLVMAKE"), (object) str2, (object) str1, (object) this.ival);
        case TrophyConditionTypes.limitbreak:
          return this.SvalCount > 1 ? string.Format(LocalizedText.Get("sys.TROPHY_LIMITBREAK_OR"), (object) this.GetUnitNames(this.sval), (object) this.ival) : string.Format(LocalizedText.Get("sys.TROPHY_LIMITBREAK"), (object) this.GetUnitName(this.sval_base), (object) this.ival);
        case TrophyConditionTypes.evoltiontimes:
          return this.SvalCount > 1 ? string.Format(LocalizedText.Get("sys.TROPHY_EVOLUTIONTIMES_OR"), (object) this.GetUnitNames(this.sval), (object) this.ival) : string.Format(LocalizedText.Get("sys.TROPHY_EVOLUTIONTIMES"), (object) this.GetUnitName(this.sval_base), (object) this.ival);
        case TrophyConditionTypes.changejob:
          return this.SvalCount > 1 ? string.Format(LocalizedText.Get("sys.TROPHY_CHANGEJOB_OR"), (object) this.GetUnitNames(this.sval), (object) this.ival) : string.Format(LocalizedText.Get("sys.TROPHY_CHANGEJOB"), (object) this.GetUnitName(this.sval_base), (object) this.ival);
        case TrophyConditionTypes.changeability:
          return this.SvalCount > 1 ? string.Format(LocalizedText.Get("sys.TROPHY_CHANGEABILITY_OR"), (object) this.GetUnitNames(this.sval), (object) this.ival) : string.Format(LocalizedText.Get("sys.TROPHY_CHANGEABILITY"), (object) this.GetUnitName(this.sval_base), (object) this.ival);
        case TrophyConditionTypes.makeabilitylevel:
          this.CheckUnexpectedMultipleSval();
          if (string.IsNullOrEmpty(this.sval_base))
            return string.Format(LocalizedText.Get("sys.TROPHY_ABILITYLV"), (object) string.Empty, (object) string.Empty, (object) this.ival);
          string[] strArray4 = this.sval_base.Split(chArray);
          string str3 = string.Empty;
          if (!string.IsNullOrEmpty(strArray4[1]))
          {
            AbilityParam abilityParam = instance.GetAbilityParam(strArray4[1]);
            str3 = abilityParam == null ? "?" + strArray4[1] : abilityParam.name;
          }
          return string.Format(LocalizedText.Get("sys.TROPHY_ABILITYLV"), (object) this.GetUnitName(strArray4[0]), (object) str3, (object) this.ival);
        case TrophyConditionTypes.winquestsoldier:
          return string.Format(LocalizedText.Get("sys.TROPHY_WINSOLIDER"), (object) this.ival);
        case TrophyConditionTypes.winmulti:
          if (this.SvalCount <= 0)
            return string.Format(LocalizedText.Get("sys.TROPHY_WINMULTI_NONE"), (object) this.ival);
          if (this.SvalCount == 1)
            return string.Format(LocalizedText.Get("sys.TROPHY_WINMULTI"), (object) this.GetQuestName(this.sval_base), (object) this.ival);
          string questNames = this.GetQuestNames(this.sval);
          return string.Format(LocalizedText.Get("sys.TROPHY_WINMULTI_OR"), (object) questNames, (object) this.ival);
        case TrophyConditionTypes.buyatshop:
          string str4 = string.Empty;
          string str5 = string.Empty;
          this.CheckUnexpectedMultipleSval();
          if (!string.IsNullOrEmpty(this.sval_base))
          {
            string[] strArray5 = this.sval_base.Split(chArray);
            if (!string.IsNullOrEmpty(strArray5[0]))
            {
              int shopType = instance.MasterParam.GetShopType(strArray5[0]);
              str4 = instance.Player.GetShopName((EShopType) shopType);
            }
            str5 = this.GetItemName(strArray5[1]);
          }
          return string.Format(LocalizedText.Get("sys.TROPHY_BUYATSHOP"), (object) str4, (object) str5, (object) this.ival);
        case TrophyConditionTypes.artifacttransmute:
          if (this.SvalCount <= 0)
            return string.Format(LocalizedText.Get("sys.TROPHY_AFORDRILL_NONE"), (object) this.ival);
          return this.SvalCount == 1 ? string.Format(LocalizedText.Get("sys.TROPHY_AFORDRILL"), (object) this.GetArtifactName(this.sval_base), (object) this.ival) : string.Format(LocalizedText.Get("sys.TROPHY_AFORDRILL_OR"), (object) this.GetArtifactNames(this.sval), (object) this.ival);
        case TrophyConditionTypes.artifactstrength:
          if (this.SvalCount <= 0)
            return string.Format(LocalizedText.Get("sys.TROPHY_AFSTRENGTHEN_NONE"), (object) this.ival);
          return this.SvalCount == 1 ? string.Format(LocalizedText.Get("sys.TROPHY_AFSTRENGTHEN"), (object) this.GetArtifactName(this.sval_base), (object) this.ival) : string.Format(LocalizedText.Get("sys.TROPHY_AFSTRENGTHEN_OR"), (object) this.GetArtifactNames(this.sval), (object) this.ival);
        case TrophyConditionTypes.artifactevolution:
          if (this.SvalCount <= 0)
            return string.Format(LocalizedText.Get("sys.TROPHY_AFVOLUTION_NONE"), (object) this.ival);
          return this.SvalCount == 1 ? string.Format(LocalizedText.Get("sys.TROPHY_AFVOLUTION"), (object) this.GetArtifactName(this.sval_base), (object) this.ival) : string.Format(LocalizedText.Get("sys.TROPHY_AFVOLUTION_OR"), (object) this.GetArtifactNames(this.sval), (object) this.ival);
        case TrophyConditionTypes.winmultimore:
          if (this.SvalCount <= 0)
            return string.Format(LocalizedText.Get("sys.TROPHY_WINMULTIMORE_NONE"), (object) this.ival);
          return this.SvalCount == 1 ? string.Format(LocalizedText.Get("sys.TROPHY_WINMULTIMORE"), (object) this.GetQuestName(this.sval_base), (object) this.ival) : string.Format(LocalizedText.Get("sys.TROPHY_WINMULTIMORE_OR"), (object) this.GetQuestNames(this.sval), (object) this.ival);
        case TrophyConditionTypes.winmultiless:
          if (this.SvalCount <= 0)
            return string.Format(LocalizedText.Get("sys.TROPHY_WINMULTILESS_NONE"), (object) this.ival);
          return this.SvalCount == 1 ? string.Format(LocalizedText.Get("sys.TROPHY_WINMULTILESS"), (object) this.GetQuestName(this.sval_base), (object) this.ival) : string.Format(LocalizedText.Get("sys.TROPHY_WINMULTILESS_OR"), (object) this.GetQuestNames(this.sval), (object) this.ival);
        case TrophyConditionTypes.collectunits:
          return string.Format(LocalizedText.Get("sys.TROPHY_COLLECTUNITS"), (object) this.ival);
        case TrophyConditionTypes.totaljoblv11:
          return string.Format(LocalizedText.Get("sys.TROPHY_TOTALJOBLV11"), (object) this.ival);
        case TrophyConditionTypes.totalunitlvs:
          return string.Format(LocalizedText.Get("sys.TROPHY_TOTALUNITLVS"), (object) this.ival);
        case TrophyConditionTypes.childrencomp:
          return string.Format(LocalizedText.Get("sys.TROPHY_CHILDRENCOMP"), (object) this.ival);
        case TrophyConditionTypes.wintower:
          if (this.SvalCount <= 0)
            return string.Format(LocalizedText.Get("sys.TROPHY_WINTOWER_NORMAL"), (object) this.ival);
          if (this.SvalCount == 1)
          {
            QuestParam quest = instance.FindQuest(this.sval_base);
            if (quest != null)
            {
              string title = quest.title;
              string name = quest.name;
              return string.Format(LocalizedText.Get("sys.TROPHY_WINTOWER"), (object) title, (object) name, (object) this.ival);
            }
            DebugUtility.LogError("「" + (object) this.sval + "」quest_id is not found.");
            return string.Empty;
          }
          string towerQuestNames1 = this.GetTowerQuestNames(this.sval);
          return string.Format(LocalizedText.Get("sys.TROPHY_WINTOWER_OR"), (object) towerQuestNames1, (object) this.ival);
        case TrophyConditionTypes.losequest:
          if (this.SvalCount <= 0)
            return string.Format(LocalizedText.Get("sys.TROPHY_LOSEQUEST_NORMAL"), (object) this.ival);
          if (this.sval.Count == 1)
          {
            QuestParam quest = instance.FindQuest(this.sval_base);
            return string.Format(LocalizedText.Get("sys.TROPHY_LOSEQUEST"), quest == null ? (object) ("?" + this.sval_base) : (object) quest.name, (object) this.ival);
          }
          string empty4 = string.Empty;
          for (int index = 0; index < this.sval.Count; ++index)
          {
            QuestParam quest = instance.FindQuest(this.sval[index]);
            if (index != 0)
              empty4 += LocalizedText.Get("sys.TROPHY_OR");
            empty4 += quest == null ? "?" + this.sval[index] : quest.name;
          }
          return string.Format(LocalizedText.Get("sys.TROPHY_LOSEQUEST_OR"), (object) empty4, (object) this.ival);
        case TrophyConditionTypes.loseelite:
          return string.Format(LocalizedText.Get("sys.TROPHY_LOSEQUEST_ELITE"), (object) this.ival);
        case TrophyConditionTypes.loseevent:
          return string.Format(LocalizedText.Get("sys.TROPHY_LOSEQUEST_EVENT"), (object) this.ival);
        case TrophyConditionTypes.losetower:
          if (this.SvalCount <= 0)
            return string.Format(LocalizedText.Get("sys.TROPHY_LOSETOWER_NORMAL"), (object) this.ival);
          if (this.SvalCount == 1)
          {
            QuestParam quest = instance.FindQuest(this.sval_base);
            if (quest != null)
            {
              string title = quest.title;
              string name = quest.name;
              return string.Format(LocalizedText.Get("sys.TROPHY_LOSETOWER"), (object) title, (object) name, (object) this.ival);
            }
            DebugUtility.Log("「" + this.sval_base + "」quest_id is not found.");
            return string.Empty;
          }
          string towerQuestNames2 = this.GetTowerQuestNames(this.sval);
          return string.Format(LocalizedText.Get("sys.TROPHY_LOSETOWER_OR"), (object) towerQuestNames2, (object) this.ival);
        case TrophyConditionTypes.losearena:
          return string.Format(LocalizedText.Get("sys.TROPHY_LOSEARENA"), (object) this.ival);
        case TrophyConditionTypes.dailyall:
          return string.Format(LocalizedText.Get("sys.TROPHY_DAILYALL"), (object) this.ival);
        case TrophyConditionTypes.vs:
          this.CheckUnexpectedMultipleSval();
          return string.Format(LocalizedText.Get("sys.TROPHY_VS"), (object) this.ival);
        case TrophyConditionTypes.vswin:
          this.CheckUnexpectedMultipleSval();
          return string.Format(LocalizedText.Get("sys.TROPHY_VSWIN"), (object) this.ival);
        case TrophyConditionTypes.vslose:
          this.CheckUnexpectedMultipleSval();
          return string.Format(LocalizedText.Get("sys.TROPHY_VSLOSE"), (object) this.ival);
        case TrophyConditionTypes.makeartifactlevel:
          if (this.SvalCount <= 0)
            return string.Format(LocalizedText.Get("sys.TROPHY_MAKE_ARTIFACT_LEVEL_NONE"), (object) this.ival);
          return this.SvalCount == 1 ? string.Format(LocalizedText.Get("sys.TROPHY_MAKE_ARTIFACT_LEVEL"), (object) this.GetArtifactName(this.sval_base), (object) this.ival) : string.Format(LocalizedText.Get("sys.TROPHY_MAKE_ARTIFACT_LEVEL_OR"), (object) this.GetArtifactNames(this.sval), (object) this.ival);
        case TrophyConditionTypes.exclear_fire:
          return string.Format(LocalizedText.Get("sys.TROPHY_EXTRA_CLEAR"), (object) LocalizedText.Get("sys.PARAM_ASSIST_FIRE"), (object) this.ival);
        case TrophyConditionTypes.exclear_water:
          return string.Format(LocalizedText.Get("sys.TROPHY_EXTRA_CLEAR"), (object) LocalizedText.Get("sys.PARAM_ASSIST_WATER"), (object) this.ival);
        case TrophyConditionTypes.exclear_wind:
          return string.Format(LocalizedText.Get("sys.TROPHY_EXTRA_CLEAR"), (object) LocalizedText.Get("sys.PARAM_ASSIST_WIND"), (object) this.ival);
        case TrophyConditionTypes.exclear_thunder:
          return string.Format(LocalizedText.Get("sys.TROPHY_EXTRA_CLEAR"), (object) LocalizedText.Get("sys.PARAM_ASSIST_THUNDER"), (object) this.ival);
        case TrophyConditionTypes.exclear_light:
          return string.Format(LocalizedText.Get("sys.TROPHY_EXTRA_CLEAR"), (object) LocalizedText.Get("sys.PARAM_ASSIST_SHINE"), (object) this.ival);
        case TrophyConditionTypes.exclear_dark:
          return string.Format(LocalizedText.Get("sys.TROPHY_EXTRA_CLEAR"), (object) LocalizedText.Get("sys.PARAM_ASSIST_DARK"), (object) this.ival);
        case TrophyConditionTypes.exclear_fire_nocon:
          return string.Format(LocalizedText.Get("sys.TROPHY_EXTRA_CLEAR_NOCON"), (object) LocalizedText.Get("sys.PARAM_ASSIST_FIRE"), (object) this.ival);
        case TrophyConditionTypes.exclear_water_nocon:
          return string.Format(LocalizedText.Get("sys.TROPHY_EXTRA_CLEAR_NOCON"), (object) LocalizedText.Get("sys.PARAM_ASSIST_WATER"), (object) this.ival);
        case TrophyConditionTypes.exclear_wind_nocon:
          return string.Format(LocalizedText.Get("sys.TROPHY_EXTRA_CLEAR_NOCON"), (object) LocalizedText.Get("sys.PARAM_ASSIST_WIND"), (object) this.ival);
        case TrophyConditionTypes.exclear_thunder_nocon:
          return string.Format(LocalizedText.Get("sys.TROPHY_EXTRA_CLEAR_NOCON"), (object) LocalizedText.Get("sys.PARAM_ASSIST_THUNDER"), (object) this.ival);
        case TrophyConditionTypes.exclear_light_nocon:
          return string.Format(LocalizedText.Get("sys.TROPHY_EXTRA_CLEAR_NOCON"), (object) LocalizedText.Get("sys.PARAM_ASSIST_SHINE"), (object) this.ival);
        case TrophyConditionTypes.exclear_dark_nocon:
          return string.Format(LocalizedText.Get("sys.TROPHY_EXTRA_CLEAR_NOCON"), (object) LocalizedText.Get("sys.PARAM_ASSIST_DARK"), (object) this.ival);
        case TrophyConditionTypes.winstory_extra:
          return string.Format(LocalizedText.Get("sys.TROPHY_WINSTORY_EXTRA"), (object) this.ival);
        case TrophyConditionTypes.multitower_help:
          return string.Format(LocalizedText.Get("sys.TROPHY_MULTITOWER_HELP"), (object) this.ival);
        case TrophyConditionTypes.multitower:
          return string.Format(LocalizedText.Get("sys.TROPHY_MULTITOWER"), (object) this.ival);
        case TrophyConditionTypes.damage_over:
          return string.Format(LocalizedText.Get("sys.TROPHY_DAMAGE_OVER"), (object) this.ival);
        case TrophyConditionTypes.complete_all_quest_mission:
          this.CheckUnexpectedMultipleSval();
          return string.Format(LocalizedText.Get("sys.TROPHY_COMPLETE_ALL_QUESTMISSION"), (object) this.GetQuestName(this.sval_base));
        case TrophyConditionTypes.has_gold_over:
          return string.Format(LocalizedText.Get("sys.TROPHY_HAS_GOLD_OVER"), (object) this.ival);
        case TrophyConditionTypes.read_tips:
          if (this.SvalCount == 1)
          {
            string tipsName = this.GetTipsName(this.sval_base);
            return string.Format(LocalizedText.Get("sys.TROPHY_READ_TIPS"), (object) tipsName);
          }
          if (this.SvalCount > 1)
          {
            string tipsNames = this.GetTipsNames(this.sval);
            return string.Format(LocalizedText.Get("sys.TROPHY_READ_TIPS_OR"), (object) tipsNames);
          }
          DebugUtility.LogError(this.Param.iname + "にTIPSの指定がありません。");
          return string.Empty;
        case TrophyConditionTypes.read_tips_count:
          return string.Format(LocalizedText.Get("sys.TROPHY_READ_TIPS_COUNT"), (object) this.ival);
        case TrophyConditionTypes.up_conceptcard_level:
          this.CheckUnexpectedMultipleSval();
          return string.Format(LocalizedText.Get("sys.TROPHY_LEVELUP_CONCEPTCARD"), (object) this.ival);
        case TrophyConditionTypes.up_conceptcard_level_target:
          if (this.SvalCount <= 0)
          {
            DebugUtility.LogError(this.Param.iname + "には念装の指定がありません。");
            return string.Empty;
          }
          if (this.SvalCount == 1)
          {
            ConceptCardParam conceptCardParam = MonoSingleton<GameManager>.Instance.GetConceptCardParam(this.sval_base);
            string str6 = string.Empty;
            if (conceptCardParam != null)
              str6 = conceptCardParam.name;
            else
              DebugUtility.LogError("真理念装「" + this.sval_base + "」は存在しません.");
            return string.Format(LocalizedText.Get("sys.TROPHY_LEVELUP_TARGET_CONCEPTCARD"), (object) str6, (object) this.ival);
          }
          string conceptCardNames1 = this.GetConceptCardNames(this.sval);
          return string.Format(LocalizedText.Get("sys.TROPHY_LEVELUP_TARGET_CONCEPTCARD_OR"), (object) conceptCardNames1, (object) this.ival);
        case TrophyConditionTypes.limitbreak_conceptcard:
          return string.Format(LocalizedText.Get("sys.TROPHY_LIMITBREAKE_CONCEPTCARD"), (object) this.ival);
        case TrophyConditionTypes.limitbreak_conceptcard_target:
          if (this.SvalCount <= 0)
          {
            DebugUtility.LogError(this.Param.iname + "には念装の指定がありません。");
            return string.Empty;
          }
          if (this.SvalCount == 1)
          {
            ConceptCardParam conceptCardParam = MonoSingleton<GameManager>.Instance.GetConceptCardParam(this.sval_base);
            string str7 = string.Empty;
            if (conceptCardParam != null)
              str7 = conceptCardParam.name;
            else
              DebugUtility.LogError("真理念装「" + this.sval_base + "」は存在しません.");
            return string.Format(LocalizedText.Get("sys.TROPHY_LIMITBREAKE_TARGET_CONCEPTCARD"), (object) str7, (object) this.ival);
          }
          string conceptCardNames2 = this.GetConceptCardNames(this.sval);
          return string.Format(LocalizedText.Get("sys.TROPHY_LIMITBREAKE_TARGET_CONCEPTCARD_OR"), (object) conceptCardNames2, (object) this.ival);
        case TrophyConditionTypes.up_conceptcard_trust:
          return string.Format(LocalizedText.Get("sys.TROPHY_TRUSTUP_CONCEPTCARD"), (object) string.Format("{0:0.0}", (object) (float) ((double) this.ival / 100.0)));
        case TrophyConditionTypes.up_conceptcard_trust_target:
          if (this.SvalCount <= 0)
          {
            DebugUtility.LogError(this.Param.iname + "には念装の指定がありません。");
            return string.Empty;
          }
          if (this.SvalCount == 1)
          {
            ConceptCardParam conceptCardParam = MonoSingleton<GameManager>.Instance.GetConceptCardParam(this.sval_base);
            string str8 = string.Empty;
            if (conceptCardParam != null)
              str8 = conceptCardParam.name;
            else
              DebugUtility.LogError("真理念装「" + this.sval_base + "」は存在しません.");
            return string.Format(LocalizedText.Get("sys.TROPHY_TRUSTUP_TARGET_CONCEPTCARD"), (object) str8, (object) string.Format("{0:0.0}", (object) (float) ((double) this.ival / 100.0)));
          }
          string conceptCardNames3 = this.GetConceptCardNames(this.sval);
          return string.Format(LocalizedText.Get("sys.TROPHY_TRUSTUP_TARGET_CONCEPTCARD_OR"), (object) conceptCardNames3, (object) string.Format("{0:0.0}", (object) (float) ((double) this.ival / 100.0)));
        case TrophyConditionTypes.max_conceptcard_trust:
          if (this.SvalCount <= 0)
            return string.Format(LocalizedText.Get("sys.TROPHY_MAX_TRUST_CONCEPTCARD"));
          if (this.SvalCount == 1)
          {
            ConceptCardParam conceptCardParam = MonoSingleton<GameManager>.Instance.GetConceptCardParam(this.sval_base);
            string str9 = string.Empty;
            if (conceptCardParam != null)
              str9 = conceptCardParam.name;
            else
              DebugUtility.LogError("真理念装「" + this.sval_base + "」は存在しません.");
            return string.Format(LocalizedText.Get("sys.TROPHY_MAX_TRUST_TARGET_CONCEPTCARD"), (object) str9);
          }
          string conceptCardNames4 = this.GetConceptCardNames(this.sval);
          return string.Format(LocalizedText.Get("sys.TROPHY_MAX_TRUST_TARGET_CONCEPTCARD_OR"), (object) conceptCardNames4);
        case TrophyConditionTypes.unlock_tobira_total:
          return string.Format(LocalizedText.Get("sys.TROPHY_UNLOCK_TOBIRA_TOTAL"), (object) this.ival);
        case TrophyConditionTypes.unlock_tobira_unit:
          if (this.SvalCount <= 0)
          {
            DebugUtility.LogError("トロフィー[" + this.Param.Name + "]にはユニットが指定されていません。");
            return string.Empty;
          }
          return this.SvalCount == 1 ? string.Format(LocalizedText.Get("sys.TROPHY_UNLOCK_TOBIRA_UNIT_TARGET"), (object) this.GetUnitName(this.sval_base)) : string.Format(LocalizedText.Get("sys.TROPHY_UNLOCK_TOBIRA_UNIT_TARGET_OR"), (object) this.GetUnitNames(this.sval));
        case TrophyConditionTypes.envy_unlock_unit:
          if (this.SvalCount <= 0)
            return string.Format(LocalizedText.Get("sys.TROPHY_UNLOCK_TOBIRA_ENVY"));
          return this.SvalCount == 1 ? string.Format(LocalizedText.Get("sys.TROPHY_UNLOCK_TOBIRA_ENVY_TARGET"), (object) this.GetUnitName(this.sval_base)) : string.Format(LocalizedText.Get("sys.TROPHY_UNLOCK_TOBIRA_ENVY_TARGET_OR"), (object) this.GetUnitNames(this.sval));
        case TrophyConditionTypes.sloth_unlock_unit:
          if (this.SvalCount <= 0)
            return string.Format(LocalizedText.Get("sys.TROPHY_UNLOCK_TOBIRA_SLOTH"));
          return this.SvalCount == 1 ? string.Format(LocalizedText.Get("sys.TROPHY_UNLOCK_TOBIRA_SLOTH_TARGET"), (object) this.GetUnitName(this.sval_base)) : string.Format(LocalizedText.Get("sys.TROPHY_UNLOCK_TOBIRA_SLOTH_TARGET_OR"), (object) this.GetUnitNames(this.sval));
        case TrophyConditionTypes.lust_unlock_unit:
          if (this.SvalCount <= 0)
            return string.Format(LocalizedText.Get("sys.TROPHY_UNLOCK_TOBIRA_LUST"));
          return this.SvalCount == 1 ? string.Format(LocalizedText.Get("sys.TROPHY_UNLOCK_TOBIRA_LUST_TARGET"), (object) this.GetUnitName(this.sval_base)) : string.Format(LocalizedText.Get("sys.TROPHY_UNLOCK_TOBIRA_LUST_TARGET_OR"), (object) this.GetUnitNames(this.sval));
        case TrophyConditionTypes.greed_unlock_unit:
          if (this.SvalCount <= 0)
            return string.Format(LocalizedText.Get("sys.TROPHY_UNLOCK_TOBIRA_GREED"));
          return this.SvalCount == 1 ? string.Format(LocalizedText.Get("sys.TROPHY_UNLOCK_TOBIRA_GREED_TARGET"), (object) this.GetUnitName(this.sval_base)) : string.Format(LocalizedText.Get("sys.TROPHY_UNLOCK_TOBIRA_GREED_TARGET_OR"), (object) this.GetUnitNames(this.sval));
        case TrophyConditionTypes.wrath_unlock_unit:
          if (this.SvalCount <= 0)
            return string.Format(LocalizedText.Get("sys.TROPHY_UNLOCK_TOBIRA_WRATH"));
          return this.SvalCount == 1 ? string.Format(LocalizedText.Get("sys.TROPHY_UNLOCK_TOBIRA_WRATH_TARGET"), (object) this.GetUnitName(this.sval_base)) : string.Format(LocalizedText.Get("sys.TROPHY_UNLOCK_TOBIRA_WRATH_TARGET_OR"), (object) this.GetUnitNames(this.sval));
        case TrophyConditionTypes.gluttonny_unlock_unit:
          if (this.SvalCount <= 0)
            return string.Format(LocalizedText.Get("sys.TROPHY_UNLOCK_TOBIRA_GLUTTONNY"));
          return this.SvalCount == 1 ? string.Format(LocalizedText.Get("sys.TROPHY_UNLOCK_TOBIRA_GLUTTONNY_TARGET"), (object) this.GetUnitName(this.sval_base)) : string.Format(LocalizedText.Get("sys.TROPHY_UNLOCK_TOBIRA_GLUTTONNY_TARGET_OR"), (object) this.GetUnitNames(this.sval));
        case TrophyConditionTypes.pride_unlock_unit:
          if (this.SvalCount <= 0)
            return string.Format(LocalizedText.Get("sys.TROPHY_UNLOCK_TOBIRA_PRIDE"));
          return this.SvalCount == 1 ? string.Format(LocalizedText.Get("sys.TROPHY_UNLOCK_TOBIRA_PRIDE_TARGET"), (object) this.GetUnitName(this.sval_base)) : string.Format(LocalizedText.Get("sys.TROPHY_UNLOCK_TOBIRA_PRIDE_TARGET_OR"), (object) this.GetUnitNames(this.sval));
        case TrophyConditionTypes.send_present:
          return string.Format(LocalizedText.Get("sys.TROPHY_SEND_PRESENT"), (object) this.ival);
        case TrophyConditionTypes.complete_all_quest_mission_total:
          this.CheckUnexpectedMultipleSval();
          if (!string.IsNullOrEmpty(this.sval_base))
          {
            QuestParam quest = instance.FindQuest(this.sval_base);
            empty2 += quest.name;
          }
          return string.Format(LocalizedText.Get("sys.TROPHY_COMPLETE_MISSION_ALL", (object) empty2));
        case TrophyConditionTypes.complete_all_mission_count:
          return string.Format(LocalizedText.Get("sys.TROPHY_COMPLETE_ALL_MISSION_COUNT", (object) this.ival));
        case TrophyConditionTypes.complete_story_mission_count:
          if (this.sval != null && this.sval.Count > 0)
          {
            DebugUtility.LogError("トロフィー [" + this.Param.iname + "] に sval を指定することはできません。");
            return string.Format(LocalizedText.Get("sys.TROPHY_COMPLETE_COUNT_ORDER", (object) this.GetAreaList(this.sval), (object) this.ival));
          }
          return string.Format(LocalizedText.Get("sys.TROPHY_COMPLETE_STORY_MISSION_COUNT", (object) this.ival));
        case TrophyConditionTypes.complete_event_mission_count:
          return this.sval != null && this.sval.Count > 0 ? string.Format(LocalizedText.Get("sys.TROPHY_COMPLETE_COUNT_ORDER", (object) this.GetAreaList(this.sval), (object) this.ival)) : string.Format(LocalizedText.Get("sys.TROPHY_COMPLETE_EVENT_COUNT", (object) this.ival));
        case TrophyConditionTypes.complete_ordeal_mission_count:
          return this.sval != null && this.sval.Count > 0 ? string.Format(LocalizedText.Get("sys.TROPHY_COMPLETE_COUNT_ORDER", (object) this.GetAreaList(this.sval), (object) this.ival)) : string.Format(LocalizedText.Get("sys.TROPHY_COMPLETE_ORDEAL_COUNT", (object) this.ival));
        case TrophyConditionTypes.clear_ordeal:
          this.CheckUnexpectedMultipleSval();
          if (!string.IsNullOrEmpty(this.sval_base))
          {
            QuestParam quest = instance.FindQuest(this.sval_base);
            if (quest != null)
            {
              empty2 += quest.name;
            }
            else
            {
              empty2 = string.Empty;
              DebugUtility.LogError("トロフィー「" + this.Param.iname + "」に指定されたクエストが存在しません");
            }
          }
          return string.Format(LocalizedText.Get("sys.TROPHY_CLEAR_ORDEAL", (object) empty2, (object) this.ival));
        case TrophyConditionTypes.view_news:
          return string.Format(LocalizedText.Get("sys.TROPHY_VIEW_ANNOUNCEMENT"));
        case TrophyConditionTypes.makeunitandjoblevel:
          int result1 = 0;
          int result2 = 0;
          this.CheckUnexpectedMultipleSval();
          if (!string.IsNullOrEmpty(this.sval_base))
          {
            string[] strArray6 = this.sval_base.Split(chArray);
            if (strArray6.Length > 1)
            {
              str2 = this.GetUnitName(strArray6[0]);
              int.TryParse(strArray6[1], out result1);
            }
            if (strArray6.Length > 3)
            {
              JobParam jobParam4 = instance.GetJobParam(strArray6[2]);
              str1 = jobParam4 == null ? "?" + strArray6[2] : jobParam4.name;
              int.TryParse(strArray6[3], out result2);
            }
          }
          return string.Format(LocalizedText.Get("sys.TROPHY_UNITANDJOBLVMAKE"), (object) str2, (object) result1, (object) str1, (object) result2);
        default:
          return string.Empty;
      }
    }

    protected bool CheckUnexpectedMultipleSval()
    {
      bool flag = this.sval != null && this.sval.Count > 1;
      if (flag)
        DebugUtility.LogError("Trophy [" + (this.Param != null ? this.Param.iname : "?") + "] に複数のsvalを指定することはできません。");
      return flag;
    }

    protected string GetAreaList(List<string> svals)
    {
      bool flag = true;
      string empty = string.Empty;
      foreach (string sval in svals)
      {
        ChapterParam area = MonoSingleton<GameManager>.Instance.FindArea(sval);
        if (area != null)
        {
          if (!flag)
            empty += ",";
          else
            flag = false;
          empty += area.name;
        }
      }
      return empty;
    }

    private string GetUnitName(string unitid)
    {
      string unitName = string.Empty;
      if (!string.IsNullOrEmpty(unitid))
      {
        UnitParam unitParam = MonoSingleton<GameManager>.Instance.GetUnitParam(unitid);
        unitName = unitParam == null ? "?" + unitid : unitParam.name;
      }
      return unitName;
    }

    private string GetUnitNames(List<string> units)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < units.Count; ++index)
      {
        string unitName = this.GetUnitName(units[index]);
        if (index > 0)
          stringBuilder.Append(LocalizedText.Get("sys.TROPHY_OR"));
        stringBuilder.Append(unitName);
      }
      return stringBuilder.ToString();
    }

    private string GetItemName(string itemid)
    {
      string itemName = string.Empty;
      if (!string.IsNullOrEmpty(itemid))
      {
        ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(itemid);
        itemName = itemParam == null ? "?" + itemid : itemParam.name;
      }
      return itemName;
    }

    private string GetItemNames(List<string> items)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < items.Count; ++index)
      {
        string itemName = this.GetItemName(items[index]);
        if (index > 0)
          stringBuilder.Append(LocalizedText.Get("sys.TROPHY_OR"));
        stringBuilder.Append(itemName);
      }
      return stringBuilder.ToString();
    }

    private string GetArtifactName(string itemid)
    {
      string artifactName = string.Empty;
      if (!string.IsNullOrEmpty(itemid))
      {
        ArtifactParam artifactParam = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(itemid);
        artifactName = artifactParam == null ? "?" + itemid : artifactParam.name;
      }
      return artifactName;
    }

    private string GetArtifactNames(List<string> artifacts)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < artifacts.Count; ++index)
      {
        string artifactName = this.GetArtifactName(artifacts[index]);
        if (index > 0)
          stringBuilder.Append(LocalizedText.Get("sys.TROPHY_OR"));
        stringBuilder.Append(artifactName);
      }
      return stringBuilder.ToString();
    }

    protected string GetQuestName(string questid)
    {
      string questName = string.Empty;
      if (!string.IsNullOrEmpty(questid))
      {
        QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(questid);
        questName = quest == null ? "?" + questid : quest.name;
      }
      return questName;
    }

    protected string GetQuestNames(List<string> quests)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < quests.Count; ++index)
      {
        string questName = this.GetQuestName(quests[index]);
        if (index > 0)
          stringBuilder.Append(LocalizedText.Get("sys.TROPHY_OR"));
        stringBuilder.Append(questName);
      }
      return stringBuilder.ToString();
    }

    protected string GetTowerQuestNames(List<string> quests)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < quests.Count; ++index)
      {
        QuestParam quest = instance.FindQuest(quests[index]);
        if (quest == null)
        {
          DebugUtility.LogError("「" + quests[index] + "」quest_id is not found.");
          stringBuilder.Append("?");
        }
        else
        {
          if (index > 0)
            stringBuilder.Append(LocalizedText.Get("sys.TROPHY_OR"));
          string title = quest.title;
          string name = quest.name;
          stringBuilder.Append(string.Format(LocalizedText.Get("sys.TROPHY_WINTOWER_TITLE_AND_NAME"), (object) title, (object) name));
        }
      }
      return stringBuilder.ToString();
    }

    private string GetConceptCardNames(List<string> cards)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < cards.Count; ++index)
      {
        string str = string.Empty;
        ConceptCardParam conceptCardParam = instance.GetConceptCardParam(cards[index]);
        if (conceptCardParam == null)
          DebugUtility.LogError("真理念装「" + cards[index] + "」は存在しません.");
        else
          str = conceptCardParam.name;
        if (index > 0)
          stringBuilder.Append(LocalizedText.Get("sys.TROPHY_OR"));
        stringBuilder.Append(str);
      }
      return stringBuilder.ToString();
    }

    private string GetTipsName(string iname)
    {
      TipsParam tips = MonoSingleton<GameManager>.Instance.FindTips(iname);
      if (tips != null)
        return tips.title;
      DebugUtility.LogError("[" + iname + "] は不正なTIPS名です。");
      return string.Empty;
    }

    private string GetTipsNames(List<string> inames)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < inames.Count; ++index)
      {
        string tipsName = this.GetTipsName(inames[index]);
        if (index > 0)
          stringBuilder.Append(LocalizedText.Get("sys.TROPHY_OR"));
        stringBuilder.Append(tipsName);
      }
      return stringBuilder.ToString();
    }
  }
}
