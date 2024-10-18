// Decompiled with JetBrains decompiler
// Type: SRPG.VersusDraftUnitParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class VersusDraftUnitParam
  {
    private long mId;
    private long mDraftUnitId;
    private int mWeight;
    private long mDummyIID;
    private string mUnitIName;
    private int mRare;
    private int mAwake;
    private int mLevel;
    private int mSelectJobIndex;
    private List<VersusDraftUnitJob> mVersusDraftUnitJobs;
    private Dictionary<string, VersusDraftUnitAbility> mAbilities;
    private List<VersusDraftUnitArtifact> mVersusDraftUnitArtifacts;
    private List<VersusDraftUnitConceptCard> mVersusDraftUnitConceptCards;
    private int mConceptCardLeaderSkill;
    private List<VersusDraftUnitDoor> mVersusDraftUnitDoors;
    private string mMasterAbilityIName;
    private string mSkinIName;
    private string[] mClearQuestIName;

    public bool IsSecret { get; set; }

    public bool IsHidden { get; set; }

    public long Id => this.mId;

    public long DraftUnitId => this.mDraftUnitId;

    public int Weight => this.mWeight;

    public bool Deserialize(long dummy_iid, JSON_VersusDraftUnitParam param)
    {
      if (dummy_iid <= 0L || param == null)
        return false;
      this.mDummyIID = dummy_iid;
      this.mId = param.id;
      this.mDraftUnitId = param.draft_unit_id;
      this.mWeight = param.weight;
      this.mUnitIName = param.unit_iname;
      this.mRare = param.rare;
      this.mAwake = param.awake;
      this.mLevel = param.lv;
      this.mSelectJobIndex = param.select_job_idx;
      this.mVersusDraftUnitJobs = new List<VersusDraftUnitJob>();
      if (!string.IsNullOrEmpty(param.job1_iname))
        this.mVersusDraftUnitJobs.Add(new VersusDraftUnitJob()
        {
          mIName = param.job1_iname,
          mRank = param.job1_lv,
          mEquip = param.job1_equip == 1
        });
      if (!string.IsNullOrEmpty(param.job2_iname))
        this.mVersusDraftUnitJobs.Add(new VersusDraftUnitJob()
        {
          mIName = param.job2_iname,
          mRank = param.job2_lv,
          mEquip = param.job2_equip == 1
        });
      if (!string.IsNullOrEmpty(param.job3_iname))
        this.mVersusDraftUnitJobs.Add(new VersusDraftUnitJob()
        {
          mIName = param.job3_iname,
          mRank = param.job3_lv,
          mEquip = param.job3_equip == 1
        });
      this.mAbilities = new Dictionary<string, VersusDraftUnitAbility>();
      int num1 = 0;
      if (!string.IsNullOrEmpty(param.abil1_iname))
      {
        this.mAbilities.Add(param.abil1_iname, new VersusDraftUnitAbility()
        {
          mIName = param.abil1_iname,
          mLevel = param.abil1_lv,
          mIID = this.mDummyIID * 10L + (long) num1
        });
        ++num1;
      }
      if (!string.IsNullOrEmpty(param.abil2_iname))
      {
        this.mAbilities.Add(param.abil2_iname, new VersusDraftUnitAbility()
        {
          mIName = param.abil2_iname,
          mLevel = param.abil2_lv,
          mIID = this.mDummyIID * 10L + (long) num1
        });
        ++num1;
      }
      if (!string.IsNullOrEmpty(param.abil3_iname))
      {
        this.mAbilities.Add(param.abil3_iname, new VersusDraftUnitAbility()
        {
          mIName = param.abil3_iname,
          mLevel = param.abil3_lv,
          mIID = this.mDummyIID * 10L + (long) num1
        });
        ++num1;
      }
      if (!string.IsNullOrEmpty(param.abil4_iname))
      {
        this.mAbilities.Add(param.abil4_iname, new VersusDraftUnitAbility()
        {
          mIName = param.abil4_iname,
          mLevel = param.abil4_lv,
          mIID = this.mDummyIID * 10L + (long) num1
        });
        ++num1;
      }
      if (!string.IsNullOrEmpty(param.abil5_iname))
      {
        this.mAbilities.Add(param.abil5_iname, new VersusDraftUnitAbility()
        {
          mIName = param.abil5_iname,
          mLevel = param.abil5_lv,
          mIID = this.mDummyIID * 10L + (long) num1
        });
        int num2 = num1 + 1;
      }
      this.mVersusDraftUnitArtifacts = new List<VersusDraftUnitArtifact>();
      if (!string.IsNullOrEmpty(param.arti1_iname))
        this.mVersusDraftUnitArtifacts.Add(new VersusDraftUnitArtifact()
        {
          mIName = param.arti1_iname,
          mRare = param.arti1_rare,
          mLevel = param.arti1_lv
        });
      if (!string.IsNullOrEmpty(param.arti2_iname))
        this.mVersusDraftUnitArtifacts.Add(new VersusDraftUnitArtifact()
        {
          mIName = param.arti2_iname,
          mRare = param.arti2_rare,
          mLevel = param.arti2_lv
        });
      if (!string.IsNullOrEmpty(param.arti3_iname))
        this.mVersusDraftUnitArtifacts.Add(new VersusDraftUnitArtifact()
        {
          mIName = param.arti3_iname,
          mRare = param.arti3_rare,
          mLevel = param.arti3_lv
        });
      this.mVersusDraftUnitConceptCards = new List<VersusDraftUnitConceptCard>();
      if (!string.IsNullOrEmpty(param.card_iname))
        this.mVersusDraftUnitConceptCards.Add(new VersusDraftUnitConceptCard()
        {
          mIName = param.card_iname,
          mLevel = param.card_lv
        });
      if (!string.IsNullOrEmpty(param.card2_iname))
        this.mVersusDraftUnitConceptCards.Add(new VersusDraftUnitConceptCard()
        {
          mIName = param.card2_iname,
          mLevel = param.card2_lv
        });
      this.mConceptCardLeaderSkill = param.card_leaderskill;
      this.mVersusDraftUnitDoors = new List<VersusDraftUnitDoor>();
      if (param.door1_lv > 0)
        this.mVersusDraftUnitDoors.Add(new VersusDraftUnitDoor()
        {
          mCategory = TobiraParam.Category.Envy,
          mLevel = param.door1_lv
        });
      if (param.door2_lv > 0)
        this.mVersusDraftUnitDoors.Add(new VersusDraftUnitDoor()
        {
          mCategory = TobiraParam.Category.Wrath,
          mLevel = param.door2_lv
        });
      if (param.door3_lv > 0)
        this.mVersusDraftUnitDoors.Add(new VersusDraftUnitDoor()
        {
          mCategory = TobiraParam.Category.Sloth,
          mLevel = param.door3_lv
        });
      if (param.door4_lv > 0)
        this.mVersusDraftUnitDoors.Add(new VersusDraftUnitDoor()
        {
          mCategory = TobiraParam.Category.Lust,
          mLevel = param.door4_lv
        });
      if (param.door5_lv > 0)
        this.mVersusDraftUnitDoors.Add(new VersusDraftUnitDoor()
        {
          mCategory = TobiraParam.Category.Gluttony,
          mLevel = param.door5_lv
        });
      if (param.door6_lv > 0)
        this.mVersusDraftUnitDoors.Add(new VersusDraftUnitDoor()
        {
          mCategory = TobiraParam.Category.Greed,
          mLevel = param.door6_lv
        });
      if (param.door7_lv > 0)
        this.mVersusDraftUnitDoors.Add(new VersusDraftUnitDoor()
        {
          mCategory = TobiraParam.Category.Pride,
          mLevel = param.door7_lv
        });
      if (this.mVersusDraftUnitDoors.Count > 0)
        this.mVersusDraftUnitDoors.Insert(0, new VersusDraftUnitDoor()
        {
          mCategory = TobiraParam.Category.START,
          mLevel = 1
        });
      this.mMasterAbilityIName = param.master_abil;
      this.mSkinIName = param.skin;
      this.mClearQuestIName = param.clear_quest_iname;
      return true;
    }

    public Json_Unit GetJson_Unit()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (Object.op_Equality((Object) instance, (Object) null))
        return (Json_Unit) null;
      UnitParam unitParam = instance.GetUnitParam(this.mUnitIName);
      if (unitParam == null)
        return (Json_Unit) null;
      Json_Unit jsonUnit = new Json_Unit();
      jsonUnit.iid = this.mDraftUnitId;
      jsonUnit.iname = this.mUnitIName;
      jsonUnit.rare = this.mRare;
      jsonUnit.plus = this.mAwake;
      jsonUnit.exp = instance.MasterParam.GetUnitLevelExp(this.mLevel);
      jsonUnit.lv = this.mLevel;
      jsonUnit.fav = 0;
      jsonUnit.elem = 0;
      jsonUnit.select = new Json_UnitSelectable();
      jsonUnit.jobs = new Json_Job[this.mVersusDraftUnitJobs.Count];
      for (int index1 = 0; index1 < this.mVersusDraftUnitJobs.Count; ++index1)
      {
        JobParam jobParam = instance.GetJobParam(this.mVersusDraftUnitJobs[index1].mIName);
        if (jobParam != null && jobParam.ranks.Length > this.mVersusDraftUnitJobs[index1].mRank)
        {
          JobRankParam rank = jobParam.ranks[this.mVersusDraftUnitJobs[index1].mRank];
          Json_Job jsonJob = new Json_Job();
          jsonJob.iid = this.mDummyIID * 10L + (long) index1;
          jsonJob.iname = this.mVersusDraftUnitJobs[index1].mIName;
          jsonJob.rank = this.mVersusDraftUnitJobs[index1].mRank;
          jsonJob.equips = new Json_Equip[JobRankParam.MAX_RANKUP_EQUIPS];
          if (this.mVersusDraftUnitJobs[index1].mEquip)
          {
            for (int index2 = 0; index2 < JobRankParam.MAX_RANKUP_EQUIPS; ++index2)
              jsonJob.equips[index2] = new Json_Equip()
              {
                iid = jsonJob.iid * 10L + (long) index2,
                iname = rank.equips[index2]
              };
          }
          jsonJob.select = new Json_JobSelectable();
          jsonJob.select.abils = new long[5];
          jsonJob.select.artifacts = new long[3];
          List<Json_Ability> jsonAbilityList = new List<Json_Ability>();
          List<string> stringList = new List<string>();
          stringList.Add(jobParam.fixed_ability);
          for (int index3 = 1; index3 <= jsonJob.rank; ++index3)
          {
            if (jobParam.ranks.Length >= index3 && jobParam.ranks[index3] != null && jobParam.ranks[index3].learnings != null)
            {
              for (int index4 = 0; index4 < jobParam.ranks[index3].learnings.Length; ++index4)
                stringList.Add((string) jobParam.ranks[index3].learnings[index4]);
            }
          }
          for (int index5 = 0; index5 < stringList.Count; ++index5)
          {
            Json_Ability jsonAbility = new Json_Ability();
            jsonAbility.iid = jsonJob.iid * 10L + (long) index5;
            jsonAbility.iname = stringList[index5];
            jsonAbility.exp = 0;
            jsonAbilityList.Add(jsonAbility);
            if (this.mAbilities.ContainsKey(jsonAbility.iname))
            {
              jsonAbility.exp = this.mAbilities[jsonAbility.iname].mLevel - 1;
              jsonAbility.iid = this.mAbilities[jsonAbility.iname].mIID;
            }
          }
          jsonJob.abils = jsonAbilityList.ToArray();
          if (index1 == this.mSelectJobIndex)
          {
            jsonUnit.select.job = jsonJob.iid;
            jsonJob.artis = new Json_Artifact[3];
            for (int index6 = 0; index6 < this.mVersusDraftUnitArtifacts.Count; ++index6)
            {
              Json_Artifact jsonArtifact = new Json_Artifact();
              jsonArtifact.iid = jsonJob.iid * 100L + (long) index6;
              jsonArtifact.iname = this.mVersusDraftUnitArtifacts[index6].mIName;
              jsonArtifact.rare = this.mVersusDraftUnitArtifacts[index6].mRare;
              jsonArtifact.exp = ArtifactData.StaticCalcExpFromLevel(this.mVersusDraftUnitArtifacts[index6].mLevel);
              jsonJob.artis[index6] = jsonArtifact;
              jsonJob.select.artifacts[index6] = jsonArtifact.iid;
            }
            int index7 = 0;
            foreach (VersusDraftUnitAbility draftUnitAbility in this.mAbilities.Values)
            {
              jsonJob.select.abils[index7] = draftUnitAbility.mIID;
              ++index7;
            }
            jsonJob.cur_skin = this.mSkinIName;
          }
          jsonUnit.jobs[index1] = jsonJob;
        }
      }
      if (this.mClearQuestIName != null && this.mClearQuestIName.Length >= 1)
      {
        QuestClearUnlockUnitDataParam[] unlockUnitDataParamArray = new QuestClearUnlockUnitDataParam[this.mClearQuestIName.Length];
        for (int index = 0; index < this.mClearQuestIName.Length; ++index)
        {
          QuestClearUnlockUnitDataParam unlockUnitData = instance.MasterParam.GetUnlockUnitData(this.mClearQuestIName[index]);
          if (unitParam != null)
            unlockUnitDataParamArray[index] = unlockUnitData;
        }
        for (int index8 = 0; index8 < unlockUnitDataParamArray.Length; ++index8)
        {
          if (unlockUnitDataParamArray[index8].type == QuestClearUnlockUnitDataParam.EUnlockTypes.Ability || unlockUnitDataParamArray[index8].type == QuestClearUnlockUnitDataParam.EUnlockTypes.MasterAbility)
          {
            for (int index9 = 0; index9 < jsonUnit.jobs.Length; ++index9)
            {
              for (int index10 = 0; index10 < jsonUnit.jobs[index9].abils.Length; ++index10)
              {
                if (jsonUnit.jobs[index9].abils[index10].iname == unlockUnitDataParamArray[index8].old_id && this.mAbilities.ContainsKey(unlockUnitDataParamArray[index8].old_id))
                  jsonUnit.jobs[index9].abils[index10].iname = unlockUnitDataParamArray[index8].new_id;
              }
            }
          }
        }
      }
      if (!string.IsNullOrEmpty(this.mMasterAbilityIName))
      {
        jsonUnit.abil = new Json_MasterAbility();
        jsonUnit.abil.iid = this.mDummyIID;
        jsonUnit.abil.iname = this.mMasterAbilityIName;
        jsonUnit.abil.exp = 0;
      }
      jsonUnit.concept_cards = new JSON_ConceptCard[2];
      for (int index = 0; index < this.mVersusDraftUnitConceptCards.Count; ++index)
      {
        ConceptCardParam conceptCardParam = instance.MasterParam.GetConceptCardParam(this.mVersusDraftUnitConceptCards[index].mIName);
        JSON_ConceptCard jsonConceptCard = (JSON_ConceptCard) null;
        if (conceptCardParam != null)
        {
          RarityParam rarityParam = instance.GetRarityParam(conceptCardParam.rare);
          jsonConceptCard = new JSON_ConceptCard();
          jsonConceptCard.iname = this.mVersusDraftUnitConceptCards[index].mIName;
          jsonConceptCard.iid = this.mDummyIID + (long) index;
          jsonConceptCard.plus = (int) rarityParam.ConceptCardAwakeCountMax;
          jsonConceptCard.exp = instance.MasterParam.GetConceptCardLevelExp(conceptCardParam.rare, this.mVersusDraftUnitConceptCards[index].mLevel);
          jsonConceptCard.trust = 0;
          jsonConceptCard.trust_bonus = 0;
          jsonConceptCard.fav = 0;
          if (ConceptCardData.IsMainSlot(index))
            jsonConceptCard.leaderskill = this.mConceptCardLeaderSkill;
        }
        jsonUnit.concept_cards[index] = jsonConceptCard;
      }
      jsonUnit.doors = new Json_Tobira[this.mVersusDraftUnitDoors.Count];
      List<Json_Ability> jsonAbilityList1 = new List<Json_Ability>();
      for (int index11 = 0; index11 < this.mVersusDraftUnitDoors.Count; ++index11)
      {
        Json_Tobira jsonTobira = new Json_Tobira();
        jsonTobira.category = (int) this.mVersusDraftUnitDoors[index11].mCategory;
        jsonTobira.lv = this.mVersusDraftUnitDoors[index11].mLevel;
        jsonUnit.doors[index11] = jsonTobira;
        TobiraParam tobiraParam = instance.MasterParam.GetTobiraParam(this.mUnitIName, this.mVersusDraftUnitDoors[index11].mCategory);
        if (tobiraParam != null)
        {
          for (int index12 = 0; index12 < tobiraParam.LeanAbilityParam.Length; ++index12)
          {
            TobiraLearnAbilityParam learnAbilityParam = tobiraParam.LeanAbilityParam[index12];
            if (learnAbilityParam.Level <= jsonTobira.lv)
            {
              switch (learnAbilityParam.AbilityAddType)
              {
                case TobiraLearnAbilityParam.AddType.JobOverwrite:
                  for (int index13 = 0; index13 < jsonUnit.jobs.Length; ++index13)
                  {
                    for (int index14 = 0; index14 < jsonUnit.jobs[index13].abils.Length; ++index14)
                    {
                      if (jsonUnit.jobs[index13].abils[index14].iname == learnAbilityParam.AbilityOverwrite)
                      {
                        jsonUnit.jobs[index13].abils[index14].iname = learnAbilityParam.AbilityIname;
                        if (this.mAbilities.ContainsKey(learnAbilityParam.AbilityIname))
                        {
                          jsonUnit.jobs[index13].abils[index14].iid = this.mAbilities[learnAbilityParam.AbilityIname].mIID;
                          jsonUnit.jobs[index13].abils[index14].exp = this.mAbilities[learnAbilityParam.AbilityIname].mLevel - 1;
                        }
                      }
                    }
                  }
                  continue;
                case TobiraLearnAbilityParam.AddType.MasterAdd:
                  jsonAbilityList1.Add(new Json_Ability()
                  {
                    iid = this.mDummyIID * 100L + (long) (index11 * 10) + (long) index12,
                    iname = learnAbilityParam.AbilityIname,
                    exp = 0
                  });
                  continue;
                default:
                  continue;
              }
            }
          }
        }
      }
      jsonUnit.door_abils = jsonAbilityList1.ToArray();
      jsonUnit.quest_clear_unlocks = this.mClearQuestIName;
      return jsonUnit;
    }
  }
}
