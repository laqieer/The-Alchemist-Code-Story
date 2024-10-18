﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FixParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class FixParam
  {
    public OInt CriticalRate_Cri_Multiply;
    public OInt CriticalRate_Cri_Division;
    public OInt CriticalRate_Luk_Multiply;
    public OInt CriticalRate_Luk_Division;
    public OInt MinCriticalDamageRate;
    public OInt MaxCriticalDamageRate;
    public OInt HighGridAtkRate;
    public OInt HighGridDefRate;
    public OInt HighGridCriRate;
    public OInt DownGridAtkRate;
    public OInt DownGridDefRate;
    public OInt DownGridCriRate;
    public OInt ParalysedRate;
    public OInt PoisonDamageRate;
    public OInt BlindnessHitRate;
    public OInt BlindnessAvoidRate;
    public OInt BerserkAtkRate;
    public OInt BerserkDefRate;
    public OInt TokkouDamageRate;
    public OInt AbilityRankUpCountCoin;
    public OInt AbilityRankUpCountMax;
    public OInt AbilityRankUpCountRecoveryVal;
    public OLong AbilityRankUpCountRecoverySec;
    public OInt StaminaRecoveryCoin;
    public OInt StaminaRecoveryVal;
    public OLong StaminaRecoverySec;
    public OInt StaminaStockCap;
    public OInt StaminaAdd;
    public OInt StaminaAdd2;
    public OInt[] StaminaAddCost;
    public OInt CaveStaminaMax;
    public OInt CaveStaminaRecoveryVal;
    public OLong CaveStaminaRecoverySec;
    public OInt CaveStaminaStockCap;
    public OInt CaveStaminaAdd;
    public OInt[] CaveStaminaAddCost;
    public OInt ChallengeArenaMax;
    public OLong ChallengeArenaCoolDownSec;
    public OInt ArenaMedalMultipler;
    public OInt ArenaCoinRewardMultipler;
    public OInt ArenaResetCooldownCost;
    public OInt[] ArenaResetTicketCost;
    public OInt ChallengeTourMax;
    public OInt ChallengeMultiMax;
    public OInt AwakeRate;
    public OInt GemsGainNormalAttack;
    public OInt GemsGainSideAttack;
    public OInt GemsGainBackAttack;
    public OInt GemsGainWeakAttack;
    public OInt GemsGainCriticalAttack;
    public OInt GemsGainKillBonus;
    public OInt GemsGainDiffFloorCount;
    public OInt GemsGainDiffFloorMax;
    public OInt ElementResistUpRate;
    public OInt ElementResistDownRate;
    public OInt GemsGainValue;
    public OInt GemsBuffValue;
    public OInt GemsBuffTurn;
    public OInt[] ShopUpdateTime;
    public OInt ContinueCoinCost;
    public OInt ContinueCoinCostMulti;
    public OInt ContinueCoinCostMultiTower;
    public OInt AvoidBaseRate;
    public OInt AvoidParamScale;
    public OInt MaxAvoidRate;
    public OString[] Products;
    public OString VipCardProduct;
    public OInt VipCardDate;
    public OString PremiumProduct;
    public OInt FreeGachaGoldMax;
    public OLong FreeGachaGoldCoolDownSec;
    public OLong FreeGachaCoinCoolDownSec;
    public OInt BuyGoldCost;
    public OInt BuyGoldAmount;
    public OInt SupportCost;
    public Dictionary<EUnitCondition, OInt> DefaultCondTurns = new Dictionary<EUnitCondition, OInt>((int) Unit.MAX_UNIT_CONDITION);
    public OInt RandomEffectMax;
    public OInt ChargeTimeMax;
    public OInt ChargeTimeDecWait;
    public OInt ChargeTimeDecMove;
    public OInt ChargeTimeDecAction;
    public OInt AddHitRateSide;
    public OInt AddHitRateBack;
    public OInt HpAutoHealRate;
    public OInt MpAutoHealRate;
    public OInt GoodSleepHpHealRate;
    public OInt GoodSleepMpHealRate;
    public OInt HpDyingRate;
    public OInt ZeneiSupportSkillRate;
    public OInt BeginnerDays;
    public OInt ArtifactBoxCap;
    public OString CommonPieceFire;
    public OString CommonPieceWater;
    public OString CommonPieceThunder;
    public OString CommonPieceWind;
    public OString CommonPieceShine;
    public OString CommonPieceDark;
    public OString CommonPieceAll;
    public int PartyNumNormal;
    public int PartyNumEvent;
    public int PartyNumMulti;
    public int PartyNumArenaAttack;
    public int PartyNumArenaDefense;
    public int PartyNumChQuest;
    public int PartyNumTower;
    public int PartyNumVersus;
    public int PartyNumMultiTower;
    public int PartyNumOrdeal;
    public int PartyNumRaid;
    public int PartyNumGuildRaid;
    public int PartyNumExtra;
    public int PartyNumGvG;
    public int PartyNumWorldRaid;
    public OBool IsDisableSuspend;
    public OInt SuspendSaveInterval;
    public bool IsJobMaster;
    public OInt DefaultDeathCount;
    public OInt DefaultClockUpValue;
    public OInt DefaultClockDownValue;
    public OInt[] EquipArtifactSlotUnlock;
    public OInt KnockBackHeight;
    public OInt ThrowHeight;
    public OString[] ArtifactRarePiece;
    public OString ArtifactCommonPiece;
    public OString[] EquipCommonPiece;
    public OInt[] EquipCommonPieceNum;
    public OString[] SoulCommonPiece;
    public OInt[] EquipCommonPieceCost;
    public OString[] EquipCmn;
    public OInt AudienceMax;
    public OInt AbilityRankUpPointMax;
    public OInt AbilityRankUpPointAddMax;
    public OInt AbilityRankupPointCoinRate;
    public OInt FirstFriendMax;
    public OInt FirstFriendCoin;
    public OInt CombinationRate;
    public OInt WeakUpRate;
    public OInt ResistDownRate;
    public OInt OrdealCT;
    public OInt EsaAssist;
    public OInt EsaResist;
    public int CardSellMul;
    public int CardExpMul;
    public OInt CardMax;
    public OInt CardTrustMax;
    public OInt CardTrustPileUp;
    public string CardSellCoinItem;
    public OInt CardAwakeUnlockLevelCap;
    public OInt TobiraLvCap;
    public OInt TobiraUnitLvCapBonus;
    public OString[] TobiraUnlockElem;
    public OString[] TobiraUnlockBirth;
    public OInt IniValRec;
    public OInt GuerrillaVal;
    public OInt DraftSelectSeconds;
    public OInt DraftOrganizeSeconds;
    public OInt DraftPlaceSeconds;
    public int GuildCreateCost;
    public int GuildRenameCost;
    public int GuildEmblemCost;
    public int GuildInvestLimit;
    public int GuildDefaultMemberMax;
    public int GuildDefaultSubMasterMax;
    public int GuildEntryCoolTime;
    public int GuildInvestCoolTime;
    public OInt ConvertRatePieceElement;
    public OInt ConvertRatePieceCommon;
    public long RaidEffectiveTime = long.MaxValue;
    public OInt MTSkipCost;
    public OString GachaChangePieceCoinIname;
    public int AutoRepeatCountMax;
    public int AutoRepeatCoolTime;
    public TobiraParam.Category ConceptcardSlot2UnlockTobira;
    public int[] ConceptcardSlot2DecRate;

    public OInt MultiRoomCommentMax { get; private set; }

    public OInt MultiInviteCommentMax { get; private set; }

    public OInt InspirationSkillLvUpRate { get; private set; }

    public OInt InspirationSkillSlotMax { get; private set; }

    public int[] QuestResetCost { get; private set; }

    public int RuneEnhNextNum { get; private set; }

    public int RuneMaxEvoNum { get; private set; }

    public int RuneStorageInit { get; private set; }

    public int RuneStorageExpansion { get; private set; }

    public int RuneStorageMax { get; private set; }

    public int RuneStorageCoinCost { get; private set; }

    public int StoryExChallengeMax { get; private set; }

    public int StoryExResetMax { get; private set; }

    public string StoryExRsetCost { get; private set; }

    public int WorldRaidDmgDropMax { get; private set; }

    public bool Deserialize(JSON_FixParam json)
    {
      if (json == null)
        return false;
      this.ShopUpdateTime = (OInt[]) null;
      this.CriticalRate_Cri_Multiply = (OInt) json.mulcri;
      this.CriticalRate_Cri_Division = (OInt) json.divcri;
      this.CriticalRate_Luk_Multiply = (OInt) json.mulluk;
      this.CriticalRate_Luk_Division = (OInt) json.divluk;
      this.MinCriticalDamageRate = (OInt) json.mincri;
      this.MaxCriticalDamageRate = (OInt) json.maxcri;
      this.HighGridAtkRate = (OInt) json.hatk;
      this.HighGridDefRate = (OInt) json.hdef;
      this.HighGridCriRate = (OInt) json.hcri;
      this.DownGridAtkRate = (OInt) json.datk;
      this.DownGridDefRate = (OInt) json.ddef;
      this.DownGridCriRate = (OInt) json.dcri;
      this.ParalysedRate = (OInt) json.paralyse;
      this.PoisonDamageRate = (OInt) json.poi_rate;
      this.BlindnessHitRate = (OInt) json.bli_hit;
      this.BlindnessAvoidRate = (OInt) json.bli_avo;
      this.BerserkAtkRate = (OInt) json.ber_atk;
      this.BerserkDefRate = (OInt) json.ber_def;
      this.TokkouDamageRate = (OInt) json.tk_rate;
      this.AbilityRankUpCountCoin = (OInt) json.abilupcoin;
      this.AbilityRankUpCountMax = (OInt) json.abilupmax;
      this.AbilityRankUpCountRecoveryVal = (OInt) json.abiluprec;
      this.AbilityRankUpCountRecoverySec = (OLong) (long) json.abilupsec;
      this.StaminaRecoveryCoin = (OInt) json.stmncoin;
      this.StaminaRecoveryVal = (OInt) json.stmnrec;
      this.StaminaRecoverySec = (OLong) (long) json.stmnsec;
      this.StaminaStockCap = (OInt) json.stmncap;
      this.StaminaAdd = (OInt) json.stmnadd;
      this.StaminaAdd2 = (OInt) json.stmnadd2;
      this.StaminaAddCost = (OInt[]) null;
      if (json.stmncost != null)
      {
        this.StaminaAddCost = new OInt[json.stmncost.Length];
        for (int index = 0; index < json.stmncost.Length; ++index)
          this.StaminaAddCost[index] = (OInt) json.stmncost[index];
      }
      this.CaveStaminaMax = (OInt) json.cavemax;
      this.CaveStaminaRecoveryVal = (OInt) json.caverec;
      this.CaveStaminaRecoverySec = (OLong) (long) json.cavesec;
      this.CaveStaminaStockCap = (OInt) json.cavecap;
      this.CaveStaminaAdd = (OInt) json.caveadd;
      this.CaveStaminaAddCost = (OInt[]) null;
      if (json.cavecost != null)
      {
        this.CaveStaminaAddCost = new OInt[json.cavecost.Length];
        for (int index = 0; index < json.cavecost.Length; ++index)
          this.CaveStaminaAddCost[index] = (OInt) json.cavecost[index];
      }
      this.ChallengeArenaMax = (OInt) json.arenamax;
      this.ChallengeArenaCoolDownSec = (OLong) (long) json.arenasec;
      this.ArenaMedalMultipler = (OInt) json.arenamedal;
      this.ArenaCoinRewardMultipler = (OInt) json.arenacoin;
      this.ArenaResetCooldownCost = (OInt) json.arenaccost;
      this.ArenaResetTicketCost = (OInt[]) null;
      if (json.arenatcost != null)
      {
        this.ArenaResetTicketCost = new OInt[json.arenatcost.Length];
        for (int index = 0; index < json.arenatcost.Length; ++index)
          this.ArenaResetTicketCost[index] = (OInt) json.arenatcost[index];
      }
      this.ChallengeTourMax = (OInt) json.tourmax;
      this.ChallengeMultiMax = (OInt) json.multimax;
      this.AwakeRate = (OInt) json.awakerate;
      this.GemsGainNormalAttack = (OInt) json.na_gems;
      this.GemsGainSideAttack = (OInt) json.sa_gems;
      this.GemsGainBackAttack = (OInt) json.ba_gems;
      this.GemsGainWeakAttack = (OInt) json.wa_gems;
      this.GemsGainCriticalAttack = (OInt) json.ca_gems;
      this.GemsGainKillBonus = (OInt) json.ki_gems;
      this.GemsGainDiffFloorCount = (OInt) json.di_gems_floor;
      this.GemsGainDiffFloorMax = (OInt) json.di_gems_max;
      this.ElementResistUpRate = (OInt) json.elem_up;
      this.ElementResistDownRate = (OInt) json.elem_down;
      this.GemsGainValue = (OInt) json.gems_gain;
      this.GemsBuffValue = (OInt) json.gems_buff;
      this.GemsBuffTurn = (OInt) json.gems_buff_turn;
      this.ContinueCoinCost = (OInt) json.continue_cost;
      this.ContinueCoinCostMulti = (OInt) json.continue_cost_multi;
      this.ContinueCoinCostMultiTower = (OInt) json.continue_cost_multitower;
      this.AvoidBaseRate = (OInt) json.avoid_rate;
      this.AvoidParamScale = (OInt) json.avoid_scale;
      this.MaxAvoidRate = (OInt) json.avoid_rate_max;
      if (json.shop_update_time != null && json.shop_update_time.Length > 0)
      {
        this.ShopUpdateTime = new OInt[json.shop_update_time.Length];
        for (int index = 0; index < this.ShopUpdateTime.Length; ++index)
          this.ShopUpdateTime[index] = (OInt) json.shop_update_time[index];
      }
      if (json.products != null && json.products.Length > 0)
      {
        this.Products = new OString[json.products.Length];
        for (int index = 0; index < this.Products.Length; ++index)
          this.Products[index] = (OString) json.products[index];
      }
      this.VipCardProduct = (OString) json.vip_product;
      this.PremiumProduct = (OString) json.premium_product;
      this.VipCardDate = (OInt) json.vip_date;
      this.FreeGachaGoldMax = (OInt) json.ggmax;
      this.FreeGachaGoldCoolDownSec = (OLong) (long) json.ggsec;
      this.FreeGachaCoinCoolDownSec = (OLong) (long) json.cgsec;
      this.BuyGoldCost = (OInt) json.buygoldcost;
      this.BuyGoldAmount = (OInt) json.buygold;
      this.SupportCost = (OInt) json.sp_cost;
      if (!this.DefaultCondTurns.ContainsKey(EUnitCondition.Poison))
        this.DefaultCondTurns.Add(EUnitCondition.Poison, (OInt) 0);
      if (!this.DefaultCondTurns.ContainsKey(EUnitCondition.Paralysed))
        this.DefaultCondTurns.Add(EUnitCondition.Paralysed, (OInt) 0);
      if (!this.DefaultCondTurns.ContainsKey(EUnitCondition.Stun))
        this.DefaultCondTurns.Add(EUnitCondition.Stun, (OInt) 0);
      if (!this.DefaultCondTurns.ContainsKey(EUnitCondition.Sleep))
        this.DefaultCondTurns.Add(EUnitCondition.Sleep, (OInt) 0);
      if (!this.DefaultCondTurns.ContainsKey(EUnitCondition.Charm))
        this.DefaultCondTurns.Add(EUnitCondition.Charm, (OInt) 0);
      if (!this.DefaultCondTurns.ContainsKey(EUnitCondition.Stone))
        this.DefaultCondTurns.Add(EUnitCondition.Stone, (OInt) 0);
      if (!this.DefaultCondTurns.ContainsKey(EUnitCondition.Blindness))
        this.DefaultCondTurns.Add(EUnitCondition.Blindness, (OInt) 0);
      if (!this.DefaultCondTurns.ContainsKey(EUnitCondition.DisableSkill))
        this.DefaultCondTurns.Add(EUnitCondition.DisableSkill, (OInt) 0);
      if (!this.DefaultCondTurns.ContainsKey(EUnitCondition.DisableMove))
        this.DefaultCondTurns.Add(EUnitCondition.DisableMove, (OInt) 0);
      if (!this.DefaultCondTurns.ContainsKey(EUnitCondition.DisableAttack))
        this.DefaultCondTurns.Add(EUnitCondition.DisableAttack, (OInt) 0);
      if (!this.DefaultCondTurns.ContainsKey(EUnitCondition.Zombie))
        this.DefaultCondTurns.Add(EUnitCondition.Zombie, (OInt) 0);
      if (!this.DefaultCondTurns.ContainsKey(EUnitCondition.DeathSentence))
        this.DefaultCondTurns.Add(EUnitCondition.DeathSentence, (OInt) 0);
      if (!this.DefaultCondTurns.ContainsKey(EUnitCondition.Berserk))
        this.DefaultCondTurns.Add(EUnitCondition.Berserk, (OInt) 0);
      if (!this.DefaultCondTurns.ContainsKey(EUnitCondition.DisableKnockback))
        this.DefaultCondTurns.Add(EUnitCondition.DisableKnockback, (OInt) 0);
      if (!this.DefaultCondTurns.ContainsKey(EUnitCondition.DisableBuff))
        this.DefaultCondTurns.Add(EUnitCondition.DisableBuff, (OInt) 0);
      if (!this.DefaultCondTurns.ContainsKey(EUnitCondition.DisableDebuff))
        this.DefaultCondTurns.Add(EUnitCondition.DisableDebuff, (OInt) 0);
      if (!this.DefaultCondTurns.ContainsKey(EUnitCondition.Stop))
        this.DefaultCondTurns.Add(EUnitCondition.Stop, (OInt) 0);
      if (!this.DefaultCondTurns.ContainsKey(EUnitCondition.Fast))
        this.DefaultCondTurns.Add(EUnitCondition.Fast, (OInt) 0);
      if (!this.DefaultCondTurns.ContainsKey(EUnitCondition.Slow))
        this.DefaultCondTurns.Add(EUnitCondition.Slow, (OInt) 0);
      if (!this.DefaultCondTurns.ContainsKey(EUnitCondition.AutoHeal))
        this.DefaultCondTurns.Add(EUnitCondition.AutoHeal, (OInt) 0);
      if (!this.DefaultCondTurns.ContainsKey(EUnitCondition.Donsoku))
        this.DefaultCondTurns.Add(EUnitCondition.Donsoku, (OInt) 0);
      if (!this.DefaultCondTurns.ContainsKey(EUnitCondition.Rage))
        this.DefaultCondTurns.Add(EUnitCondition.Rage, (OInt) 0);
      if (!this.DefaultCondTurns.ContainsKey(EUnitCondition.GoodSleep))
        this.DefaultCondTurns.Add(EUnitCondition.GoodSleep, (OInt) 0);
      if (!this.DefaultCondTurns.ContainsKey(EUnitCondition.AutoJewel))
        this.DefaultCondTurns.Add(EUnitCondition.AutoJewel, (OInt) 0);
      if (!this.DefaultCondTurns.ContainsKey(EUnitCondition.DisableHeal))
        this.DefaultCondTurns.Add(EUnitCondition.DisableHeal, (OInt) 0);
      if (!this.DefaultCondTurns.ContainsKey(EUnitCondition.DisableSingleAttack))
        this.DefaultCondTurns.Add(EUnitCondition.DisableSingleAttack, (OInt) 0);
      if (!this.DefaultCondTurns.ContainsKey(EUnitCondition.DisableAreaAttack))
        this.DefaultCondTurns.Add(EUnitCondition.DisableAreaAttack, (OInt) 0);
      if (!this.DefaultCondTurns.ContainsKey(EUnitCondition.DisableDecCT))
        this.DefaultCondTurns.Add(EUnitCondition.DisableDecCT, (OInt) 0);
      if (!this.DefaultCondTurns.ContainsKey(EUnitCondition.DisableIncCT))
        this.DefaultCondTurns.Add(EUnitCondition.DisableIncCT, (OInt) 0);
      if (!this.DefaultCondTurns.ContainsKey(EUnitCondition.DisableEsaFire))
        this.DefaultCondTurns.Add(EUnitCondition.DisableEsaFire, (OInt) 0);
      if (!this.DefaultCondTurns.ContainsKey(EUnitCondition.DisableEsaWater))
        this.DefaultCondTurns.Add(EUnitCondition.DisableEsaWater, (OInt) 0);
      if (!this.DefaultCondTurns.ContainsKey(EUnitCondition.DisableEsaWind))
        this.DefaultCondTurns.Add(EUnitCondition.DisableEsaWind, (OInt) 0);
      if (!this.DefaultCondTurns.ContainsKey(EUnitCondition.DisableEsaThunder))
        this.DefaultCondTurns.Add(EUnitCondition.DisableEsaThunder, (OInt) 0);
      if (!this.DefaultCondTurns.ContainsKey(EUnitCondition.DisableEsaShine))
        this.DefaultCondTurns.Add(EUnitCondition.DisableEsaShine, (OInt) 0);
      if (!this.DefaultCondTurns.ContainsKey(EUnitCondition.DisableEsaDark))
        this.DefaultCondTurns.Add(EUnitCondition.DisableEsaDark, (OInt) 0);
      if (!this.DefaultCondTurns.ContainsKey(EUnitCondition.DisableMaxDamageHp))
        this.DefaultCondTurns.Add(EUnitCondition.DisableMaxDamageHp, (OInt) 0);
      if (!this.DefaultCondTurns.ContainsKey(EUnitCondition.DisableMaxDamageMp))
        this.DefaultCondTurns.Add(EUnitCondition.DisableMaxDamageMp, (OInt) 0);
      if (!this.DefaultCondTurns.ContainsKey(EUnitCondition.DisableSideAttack))
        this.DefaultCondTurns.Add(EUnitCondition.DisableSideAttack, (OInt) 0);
      if (!this.DefaultCondTurns.ContainsKey(EUnitCondition.DisableBackAttack))
        this.DefaultCondTurns.Add(EUnitCondition.DisableBackAttack, (OInt) 0);
      if (!this.DefaultCondTurns.ContainsKey(EUnitCondition.DisableObstReaction))
        this.DefaultCondTurns.Add(EUnitCondition.DisableObstReaction, (OInt) 0);
      if (!this.DefaultCondTurns.ContainsKey(EUnitCondition.DisableForcedTargeting))
        this.DefaultCondTurns.Add(EUnitCondition.DisableForcedTargeting, (OInt) 0);
      this.DefaultCondTurns[EUnitCondition.Poison] = (OInt) json.ct_poi;
      this.DefaultCondTurns[EUnitCondition.Paralysed] = (OInt) json.ct_par;
      this.DefaultCondTurns[EUnitCondition.Stun] = (OInt) json.ct_stu;
      this.DefaultCondTurns[EUnitCondition.Sleep] = (OInt) json.ct_sle;
      this.DefaultCondTurns[EUnitCondition.Charm] = (OInt) json.st_cha;
      this.DefaultCondTurns[EUnitCondition.Stone] = (OInt) json.ct_sto;
      this.DefaultCondTurns[EUnitCondition.Blindness] = (OInt) json.ct_bli;
      this.DefaultCondTurns[EUnitCondition.DisableSkill] = (OInt) json.ct_dsk;
      this.DefaultCondTurns[EUnitCondition.DisableMove] = (OInt) json.ct_dmo;
      this.DefaultCondTurns[EUnitCondition.DisableAttack] = (OInt) json.ct_dat;
      this.DefaultCondTurns[EUnitCondition.Zombie] = (OInt) json.ct_zom;
      this.DefaultCondTurns[EUnitCondition.DeathSentence] = (OInt) json.ct_dea;
      this.DefaultCondTurns[EUnitCondition.Berserk] = (OInt) json.ct_ber;
      this.DefaultCondTurns[EUnitCondition.DisableKnockback] = (OInt) json.ct_dkn;
      this.DefaultCondTurns[EUnitCondition.DisableBuff] = (OInt) json.ct_dbu;
      this.DefaultCondTurns[EUnitCondition.DisableDebuff] = (OInt) json.ct_ddb;
      this.DefaultCondTurns[EUnitCondition.Stop] = (OInt) json.ct_stop;
      this.DefaultCondTurns[EUnitCondition.Fast] = (OInt) json.ct_fast;
      this.DefaultCondTurns[EUnitCondition.Slow] = (OInt) json.ct_slow;
      this.DefaultCondTurns[EUnitCondition.AutoHeal] = (OInt) json.ct_ahe;
      this.DefaultCondTurns[EUnitCondition.Donsoku] = (OInt) json.ct_don;
      this.DefaultCondTurns[EUnitCondition.Rage] = (OInt) json.ct_rag;
      this.DefaultCondTurns[EUnitCondition.GoodSleep] = (OInt) json.ct_gsl;
      this.DefaultCondTurns[EUnitCondition.AutoJewel] = (OInt) json.ct_aje;
      this.DefaultCondTurns[EUnitCondition.DisableHeal] = (OInt) json.ct_dhe;
      this.DefaultCondTurns[EUnitCondition.DisableSingleAttack] = (OInt) json.ct_dsa;
      this.DefaultCondTurns[EUnitCondition.DisableAreaAttack] = (OInt) json.ct_daa;
      this.DefaultCondTurns[EUnitCondition.DisableDecCT] = (OInt) json.ct_ddc;
      this.DefaultCondTurns[EUnitCondition.DisableIncCT] = (OInt) json.ct_dic;
      this.DefaultCondTurns[EUnitCondition.DisableEsaFire] = (OInt) json.ct_esa;
      this.DefaultCondTurns[EUnitCondition.DisableEsaWater] = (OInt) json.ct_esa;
      this.DefaultCondTurns[EUnitCondition.DisableEsaWind] = (OInt) json.ct_esa;
      this.DefaultCondTurns[EUnitCondition.DisableEsaThunder] = (OInt) json.ct_esa;
      this.DefaultCondTurns[EUnitCondition.DisableEsaShine] = (OInt) json.ct_esa;
      this.DefaultCondTurns[EUnitCondition.DisableEsaDark] = (OInt) json.ct_esa;
      this.DefaultCondTurns[EUnitCondition.DisableMaxDamageHp] = (OInt) json.ct_mdh;
      this.DefaultCondTurns[EUnitCondition.DisableMaxDamageMp] = (OInt) json.ct_mdm;
      this.DefaultCondTurns[EUnitCondition.DisableSideAttack] = (OInt) json.ct_das;
      this.DefaultCondTurns[EUnitCondition.DisableBackAttack] = (OInt) json.ct_dab;
      this.DefaultCondTurns[EUnitCondition.DisableObstReaction] = (OInt) json.ct_dor;
      this.DefaultCondTurns[EUnitCondition.DisableForcedTargeting] = (OInt) json.ct_dft;
      this.RandomEffectMax = (OInt) json.yuragi;
      this.ChargeTimeMax = (OInt) json.ct_max;
      this.ChargeTimeDecWait = (OInt) json.ct_wait;
      this.ChargeTimeDecMove = (OInt) json.ct_mov;
      this.ChargeTimeDecAction = (OInt) json.ct_act;
      this.AddHitRateSide = (OInt) json.hit_side;
      this.AddHitRateBack = (OInt) json.hit_back;
      this.HpAutoHealRate = (OInt) json.ahhp_rate;
      this.MpAutoHealRate = (OInt) json.ahmp_rate;
      this.GoodSleepHpHealRate = (OInt) json.gshp_rate;
      this.GoodSleepMpHealRate = (OInt) json.gsmp_rate;
      this.HpDyingRate = (OInt) json.dy_rate;
      this.ZeneiSupportSkillRate = (OInt) json.zsup_rate;
      this.BeginnerDays = (OInt) json.beginner_days;
      this.ArtifactBoxCap = (OInt) json.afcap;
      this.CommonPieceFire = (OString) json.cmn_pi_fire;
      this.CommonPieceWater = (OString) json.cmn_pi_water;
      this.CommonPieceThunder = (OString) json.cmn_pi_thunder;
      this.CommonPieceWind = (OString) json.cmn_pi_wind;
      this.CommonPieceShine = (OString) json.cmn_pi_shine;
      this.CommonPieceDark = (OString) json.cmn_pi_dark;
      this.CommonPieceAll = (OString) json.cmn_pi_all;
      this.PartyNumNormal = json.ptnum_nml;
      this.PartyNumEvent = json.ptnum_evnt;
      this.PartyNumMulti = json.ptnum_mlt;
      this.PartyNumArenaAttack = json.ptnum_aatk;
      this.PartyNumArenaDefense = json.ptnum_adef;
      this.PartyNumChQuest = json.ptnum_chq;
      this.PartyNumTower = json.ptnum_tow;
      this.PartyNumVersus = json.ptnum_vs;
      this.PartyNumMultiTower = json.ptnum_mt;
      this.PartyNumOrdeal = json.ptnum_ordeal;
      this.PartyNumRaid = json.ptnum_raid;
      this.PartyNumGuildRaid = json.ptnum_guild_raid;
      this.PartyNumExtra = json.ptnum_extra;
      this.PartyNumGvG = json.ptnum_gvg;
      this.PartyNumWorldRaid = json.ptnum_wr;
      this.IsDisableSuspend = (OBool) (json.notsus != 0);
      this.SuspendSaveInterval = (OInt) json.sus_int;
      this.IsJobMaster = json.jobms != 0;
      this.DefaultDeathCount = (OInt) json.death_count;
      this.DefaultClockUpValue = (OInt) json.fast_val;
      this.DefaultClockDownValue = (OInt) json.slow_val;
      this.GuildCreateCost = json.guild_create_cost;
      this.GuildRenameCost = json.guild_rename_cost;
      this.GuildEmblemCost = json.guild_emblem_cost;
      this.GuildInvestLimit = json.guild_invest_limit;
      this.GuildDefaultMemberMax = json.guild_member_max;
      this.GuildDefaultSubMasterMax = json.guild_submaster_max;
      this.GuildEntryCoolTime = json.guild_entry_cooltime;
      this.GuildInvestCoolTime = json.guild_invest_cooltime;
      if (json.equip_artifact_slot_unlock != null && json.equip_artifact_slot_unlock.Length > 0)
      {
        this.EquipArtifactSlotUnlock = new OInt[json.equip_artifact_slot_unlock.Length];
        for (int index = 0; index < json.equip_artifact_slot_unlock.Length; ++index)
          this.EquipArtifactSlotUnlock[index] = (OInt) json.equip_artifact_slot_unlock[index];
      }
      this.KnockBackHeight = (OInt) json.kb_gh;
      this.ThrowHeight = (OInt) json.th_gh;
      if (json.art_rare_pi != null)
      {
        this.ArtifactRarePiece = new OString[json.art_rare_pi.Length];
        for (int index = 0; index < this.ArtifactRarePiece.Length; ++index)
          this.ArtifactRarePiece[index] = (OString) json.art_rare_pi[index];
      }
      this.ArtifactCommonPiece = (OString) json.art_cmn_pi;
      this.SoulCommonPiece = this.ConvertOStringArray(json.soul_rare);
      this.EquipCommonPiece = this.ConvertOStringArray(json.equ_rare_pi);
      this.EquipCommonPieceNum = this.ConvertOIntArray(json.equ_rare_pi_use);
      this.EquipCommonPieceCost = this.ConvertOIntArray(json.equ_rare_cost);
      this.EquipCmn = this.ConvertOStringArray(json.equip_cmn);
      this.AudienceMax = (OInt) json.aud_max;
      this.AbilityRankUpPointMax = (OInt) json.ab_rankup_max;
      this.AbilityRankUpPointAddMax = (OInt) json.ab_rankup_addmax;
      this.AbilityRankupPointCoinRate = (OInt) json.ab_coin_convert;
      this.FirstFriendMax = (OInt) json.firstfriend_max;
      this.FirstFriendCoin = (OInt) json.firstfriend_coin;
      this.CombinationRate = (OInt) json.cmb_rate;
      this.WeakUpRate = (OInt) json.weak_up;
      this.ResistDownRate = (OInt) json.resist_dw;
      this.OrdealCT = (OInt) json.ordeal_ct;
      this.EsaAssist = (OInt) json.esa_assist;
      this.EsaResist = (OInt) json.esa_resist;
      this.CardSellMul = json.card_sell_mul;
      this.CardExpMul = json.card_exp_mul;
      this.CardMax = (OInt) json.card_max;
      this.CardTrustMax = (OInt) json.card_trust_max;
      this.CardTrustPileUp = (OInt) json.card_trust_en_bonus;
      this.CardAwakeUnlockLevelCap = (OInt) json.card_awake_unlock_lvcap;
      this.CardSellCoinItem = json.card_sell_coin_iname;
      this.TobiraLvCap = (OInt) json.tobira_lv_cap;
      this.TobiraUnitLvCapBonus = (OInt) json.tobira_unit_lv_cap;
      this.TobiraUnlockElem = new OString[json.tobira_unlock_elem.Length];
      for (int index = 0; index < this.TobiraUnlockElem.Length; ++index)
        this.TobiraUnlockElem[index] = (OString) json.tobira_unlock_elem[index];
      this.TobiraUnlockBirth = new OString[json.tobira_unlock_birth.Length];
      for (int index = 0; index < this.TobiraUnlockBirth.Length; ++index)
        this.TobiraUnlockBirth[index] = (OString) json.tobira_unlock_birth[index];
      this.IniValRec = (OInt) json.ini_rec;
      this.GuerrillaVal = (OInt) json.guerrilla_val;
      this.DraftSelectSeconds = (OInt) json.draft_select_sec;
      this.DraftOrganizeSeconds = (OInt) json.draft_organize_sec;
      this.DraftPlaceSeconds = (OInt) json.draft_place_sec;
      this.ConvertRatePieceElement = (OInt) json.convert_rate_piece_element;
      this.ConvertRatePieceCommon = (OInt) json.convert_rate_piece_common;
      if (!string.IsNullOrEmpty(json.raid_effective_time))
      {
        DateTime result = new DateTime();
        DateTime.TryParse(json.raid_effective_time, out result);
        this.RaidEffectiveTime = TimeManager.FromDateTime(result);
      }
      this.MTSkipCost = (OInt) json.mt_skip_cost;
      this.MultiRoomCommentMax = (OInt) json.multi_room_comment_max;
      this.MultiInviteCommentMax = (OInt) json.multi_invite_comment_max;
      this.InspirationSkillLvUpRate = (OInt) json.insp_skill_lvup_rate;
      this.InspirationSkillSlotMax = (OInt) json.insp_skill_slot_max;
      this.GachaChangePieceCoinIname = (OString) json.ch_piece_coin_iname;
      this.QuestResetCost = json.quest_reset_cost;
      this.AutoRepeatCountMax = json.auto_repeat_max;
      this.AutoRepeatCoolTime = json.auto_repeat_cooltime;
      this.RuneEnhNextNum = json.rune_enh_next_num;
      this.RuneMaxEvoNum = json.rune_evo_num;
      this.RuneStorageInit = json.rune_storage_init;
      this.RuneStorageExpansion = json.rune_storage_expansion;
      this.RuneStorageMax = json.rune_storage_max;
      this.RuneStorageCoinCost = json.rune_storage_coin_cost;
      this.StoryExChallengeMax = json.story_ex_total_limit;
      this.StoryExResetMax = json.story_ex_total_limit_reset_num;
      this.StoryExRsetCost = json.story_ex_total_limit_reset_cost;
      this.ConceptcardSlot2UnlockTobira = (TobiraParam.Category) json.conceptcard_slot2_unlock_tobira;
      if (json.conceptcard_slot2_dec_rate == null)
      {
        DebugUtility.LogError("json.conceptcard_slot2_dec_rate が null な為、サブ枠に装備された真理念装の効果は減衰しません。");
      }
      else
      {
        this.ConceptcardSlot2DecRate = new int[json.conceptcard_slot2_dec_rate.Length];
        for (int index = 0; index < this.ConceptcardSlot2DecRate.Length; ++index)
          this.ConceptcardSlot2DecRate[index] = json.conceptcard_slot2_dec_rate[index];
      }
      this.WorldRaidDmgDropMax = json.wr_dmg_drop_max;
      return true;
    }

    public OString[] ConvertOStringArray(string[] strs)
    {
      OString[] ostringArray = (OString[]) null;
      if (strs != null)
      {
        ostringArray = new OString[strs.Length];
        for (int index = 0; index < ostringArray.Length; ++index)
          ostringArray[index] = (OString) strs[index];
      }
      return ostringArray;
    }

    public OInt[] ConvertOIntArray(int[] strs)
    {
      OInt[] ointArray = (OInt[]) null;
      if (strs != null)
      {
        ointArray = new OInt[strs.Length];
        for (int index = 0; index < ointArray.Length; ++index)
          ointArray[index] = (OInt) strs[index];
      }
      return ointArray;
    }
  }
}
