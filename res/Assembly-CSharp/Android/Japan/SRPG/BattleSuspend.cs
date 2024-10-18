// Decompiled with JetBrains decompiler
// Type: SRPG.BattleSuspend
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace SRPG
{
  public class BattleSuspend
  {
    private const string SUSPEND_FILENAME = "new_suspend.bin";

    private static string mSuspendFileName
    {
      get
      {
        return AppPath.persistentDataPath + "/new_suspend.bin";
      }
    }

    private static bool writeSaveData(string file_name, BattleSuspend.Data data = null)
    {
      if (string.IsNullOrEmpty(file_name))
        return false;
      if (data == null)
        data = new BattleSuspend.Data();
      try
      {
        string json = JsonUtility.ToJson((object) data);
        if (!string.IsNullOrEmpty(json))
        {
          byte[] bytes = MyEncrypt.Encrypt(0, json, false);
          if (bytes != null)
          {
            File.WriteAllBytes(file_name, bytes);
            return true;
          }
        }
      }
      catch
      {
      }
      return false;
    }

    private static BattleSuspend.Data loadSaveData(string file_name)
    {
      if (string.IsNullOrEmpty(file_name))
        return (BattleSuspend.Data) null;
      try
      {
        byte[] data = File.ReadAllBytes(file_name);
        if (data != null)
        {
          string json = MyEncrypt.Decrypt(0, data, false);
          if (!string.IsNullOrEmpty(json))
            return JsonUtility.FromJson<BattleSuspend.Data>(json);
        }
      }
      catch
      {
      }
      return (BattleSuspend.Data) null;
    }

    private static BattleSuspend.Data makeSaveData()
    {
      SceneBattle instance = SceneBattle.Instance;
      if (!(bool) ((UnityEngine.Object) instance))
        return (BattleSuspend.Data) null;
      BattleCore battle = instance.Battle;
      if (battle == null)
        return (BattleSuspend.Data) null;
      BattleSuspend.Data data = new BattleSuspend.Data();
      BattleSuspend.Data.Header hdr = data.hdr;
      hdr.apv = Application.version;
      hdr.arv = AssetManager.AssetRevision;
      hdr.qid = battle.QuestID;
      hdr.bid = battle.BtlID;
      hdr.cat = GameUtility.Config_AutoMode_Treasure.Value;
      hdr.cad = GameUtility.Config_AutoMode_DisableSkill.Value;
      hdr.key = battle.UniqueKey;
      data.uil.Clear();
      foreach (Unit allUnit in battle.AllUnits)
      {
        if (allUnit != null)
        {
          BattleSuspend.Data.UnitInfo unitInfo = new BattleSuspend.Data.UnitInfo();
          unitInfo.nam = allUnit.UnitName;
          unitInfo.nhp = (int) allUnit.CurrentStatus.param.hp;
          unitInfo.chp = allUnit.UnitChangedHp;
          unitInfo.gem = allUnit.Gems;
          unitInfo.ugx = allUnit.x;
          unitInfo.ugy = allUnit.y;
          unitInfo.dir = (int) allUnit.Direction;
          unitInfo.ufg = allUnit.UnitFlag;
          unitInfo.isb = allUnit.IsSub;
          unitInfo.crt = (int) allUnit.ChargeTime;
          unitInfo.tgi = BattleSuspend.GetIdxFromAllUnits(battle, allUnit.Target);
          unitInfo.rti = BattleSuspend.GetIdxFromAllUnits(battle, allUnit.RageTarget);
          unitInfo.cti = -1;
          if (allUnit.CastSkill != null)
          {
            unitInfo.csi = allUnit.CastSkill.SkillParam.iname;
            unitInfo.ctm = (int) allUnit.CastTime;
            unitInfo.cid = (int) allUnit.CastIndex;
            if (allUnit.CastSkillGridMap != null)
            {
              unitInfo.cgw = allUnit.CastSkillGridMap.w;
              unitInfo.cgh = allUnit.CastSkillGridMap.h;
              if (allUnit.CastSkillGridMap.data != null)
              {
                unitInfo.cgm = new int[allUnit.CastSkillGridMap.data.Length];
                for (int index = 0; index < allUnit.CastSkillGridMap.data.Length; ++index)
                  unitInfo.cgm[index] = !allUnit.CastSkillGridMap.data[index] ? 0 : 1;
              }
            }
            unitInfo.ctx = allUnit.GridTarget == null ? -1 : allUnit.GridTarget.x;
            unitInfo.cty = allUnit.GridTarget == null ? -1 : allUnit.GridTarget.y;
            unitInfo.cti = BattleSuspend.GetIdxFromAllUnits(battle, allUnit.UnitTarget);
          }
          unitInfo.dct = allUnit.DeathCount;
          unitInfo.ajw = allUnit.AutoJewel;
          unitInfo.wtt = allUnit.WaitClock;
          unitInfo.mvt = allUnit.WaitMoveTurn;
          unitInfo.acc = allUnit.ActionCount;
          unitInfo.tuc = allUnit.TurnCount;
          unitInfo.trc = allUnit.EventTrigger == null ? 0 : allUnit.EventTrigger.Count;
          unitInfo.klc = allUnit.KillCount;
          if (allUnit.EntryTriggers != null)
          {
            unitInfo.etr = new int[allUnit.EntryTriggers.Count];
            for (int index = 0; index < allUnit.EntryTriggers.Count; ++index)
              unitInfo.etr[index] = !allUnit.EntryTriggers[index].on ? 0 : 1;
          }
          unitInfo.aid = (int) allUnit.AIActionIndex;
          unitInfo.atu = (int) allUnit.AIActionTurnCount;
          unitInfo.apt = (int) allUnit.AIPatrolIndex;
          unitInfo.boi = allUnit.CreateBreakObjId;
          unitInfo.boc = allUnit.CreateBreakObjClock;
          unitInfo.tid = allUnit.TeamId;
          unitInfo.fst = (int) allUnit.FriendStates;
          unitInfo.ist = allUnit.InfinitySpawnTag;
          unitInfo.isd = allUnit.InfinitySpawnDeck;
          unitInfo.did = allUnit.DtuParam == null ? (string) null : allUnit.DtuParam.Iname;
          unitInfo.dfu = BattleSuspend.GetIdxFromAllUnits(battle, allUnit.DtuFromUnit);
          unitInfo.drt = allUnit.DtuRemainTurn;
          unitInfo.okd = (int) allUnit.OverKillDamage;
          unitInfo.iil.Clear();
          for (int index = 0; index < allUnit.InspInsList.Count; ++index)
          {
            Unit.UnitInsp inspIns = allUnit.InspInsList[index];
            unitInfo.iil.Add(new BattleSuspend.Data.UnitInfo.Insp()
            {
              aii = (int) (long) inspIns.mArtifact.UniqueID,
              sno = (int) inspIns.mSlotNo,
              val = !(bool) inspIns.mValid ? 0 : 1,
              cct = (int) inspIns.mCheckCtr
            });
          }
          unitInfo.iul.Clear();
          for (int index = 0; index < allUnit.InspUseList.Count; ++index)
          {
            Unit.UnitInsp inspUse = allUnit.InspUseList[index];
            unitInfo.iul.Add((int) (long) inspUse.mArtifact.UniqueID);
          }
          unitInfo.acl.Clear();
          for (int index1 = 0; index1 < allUnit.AbilityChangeLists.Count; ++index1)
          {
            Unit.AbilityChange abilityChangeList = allUnit.AbilityChangeLists[index1];
            if (abilityChangeList != null && abilityChangeList.mDataLists.Count != 0)
            {
              BattleSuspend.Data.UnitInfo.AbilChg abilChg = new BattleSuspend.Data.UnitInfo.AbilChg();
              for (int index2 = 0; index2 < abilityChangeList.mDataLists.Count; ++index2)
              {
                Unit.AbilityChange.Data mDataList = abilityChangeList.mDataLists[index2];
                abilChg.acd.Add(new BattleSuspend.Data.UnitInfo.AbilChg.Data()
                {
                  fid = mDataList.mFromAp.iname,
                  tid = mDataList.mToAp.iname,
                  tur = mDataList.mTurn,
                  irs = !mDataList.mIsReset ? 0 : 1,
                  exp = mDataList.mExp,
                  iif = !mDataList.mIsInfinite ? 0 : 1
                });
              }
              unitInfo.acl.Add(abilChg);
            }
          }
          unitInfo.aal.Clear();
          for (int index = 0; index < allUnit.AddedAbilitys.Count; ++index)
          {
            AbilityData addedAbility = allUnit.AddedAbilitys[index];
            if (addedAbility != null)
              unitInfo.aal.Add(new BattleSuspend.Data.UnitInfo.AddedAbil()
              {
                aid = addedAbility.AbilityID,
                exp = addedAbility.Exp,
                nct = addedAbility.IsNoneCategory
              });
          }
          unitInfo.sul.Clear();
          foreach (KeyValuePair<SkillData, OInt> keyValuePair in allUnit.GetSkillUseCount())
            unitInfo.sul.Add(new BattleSuspend.Data.UnitInfo.SkillUse()
            {
              sid = keyValuePair.Key.SkillParam.iname,
              ctr = (int) keyValuePair.Value
            });
          unitInfo.bfl.Clear();
          foreach (BuffAttachment buffAttachment in allUnit.BuffAttachments)
          {
            if ((!(bool) buffAttachment.IsPassive || buffAttachment.Param != null && (bool) buffAttachment.Param.mIsUpBuff || buffAttachment.skill != null && !buffAttachment.skill.IsSubActuate()) && buffAttachment.CheckTiming != EffectCheckTimings.Moment)
            {
              BattleSuspend.Data.UnitInfo.Buff buff = new BattleSuspend.Data.UnitInfo.Buff();
              buff.sid = buffAttachment.skill == null ? (string) null : buffAttachment.skill.SkillParam.iname;
              buff.stg = (int) buffAttachment.skilltarget;
              buff.tur = (int) buffAttachment.turn;
              buff.uni = BattleSuspend.GetIdxFromAllUnits(battle, buffAttachment.user);
              buff.cui = BattleSuspend.GetIdxFromAllUnits(battle, buffAttachment.CheckTarget);
              buff.tim = (int) buffAttachment.CheckTiming;
              buff.ipa = (bool) buffAttachment.IsPassive;
              buff.ucd = (int) buffAttachment.UseCondition;
              buff.btp = (int) buffAttachment.BuffType;
              buff.vtp = !buffAttachment.IsNegativeValueIsBuff ? 0 : 1;
              buff.ctp = (int) buffAttachment.CalcType;
              buff.lid = buffAttachment.LinkageID;
              buff.ubc = (int) buffAttachment.UpBuffCount;
              buff.atl.Clear();
              if (buffAttachment.AagTargetLists != null)
              {
                for (int index = 0; index < buffAttachment.AagTargetLists.Count; ++index)
                {
                  int idxFromAllUnits = BattleSuspend.GetIdxFromAllUnits(battle, buffAttachment.AagTargetLists[index]);
                  if (idxFromAllUnits >= 0)
                    buff.atl.Add(idxFromAllUnits);
                }
              }
              unitInfo.bfl.Add(buff);
            }
          }
          unitInfo.cdl.Clear();
          foreach (CondAttachment condAttachment in allUnit.CondAttachments)
          {
            if (!(bool) condAttachment.IsPassive || condAttachment.skill != null && !condAttachment.skill.IsSubActuate())
              unitInfo.cdl.Add(new BattleSuspend.Data.UnitInfo.Cond()
              {
                sid = condAttachment.skill == null ? (string) null : condAttachment.skill.SkillParam.iname,
                stg = (int) condAttachment.skilltarget,
                cid = condAttachment.CondId,
                tur = (int) condAttachment.turn,
                uni = BattleSuspend.GetIdxFromAllUnits(battle, condAttachment.user),
                cui = BattleSuspend.GetIdxFromAllUnits(battle, condAttachment.CheckTarget),
                tim = (int) condAttachment.CheckTiming,
                ipa = (bool) condAttachment.IsPassive,
                ucd = (int) condAttachment.UseCondition,
                cdt = (int) condAttachment.CondType,
                cnd = (int) condAttachment.Condition,
                icu = condAttachment.IsCurse,
                lid = condAttachment.LinkageID
              });
          }
          unitInfo.shl.Clear();
          foreach (Unit.UnitShield shield in allUnit.Shields)
            unitInfo.shl.Add(new BattleSuspend.Data.UnitInfo.Shield()
            {
              inm = shield.skill_param.iname,
              nhp = (int) shield.hp,
              mhp = (int) shield.hpMax,
              ntu = (int) shield.turn,
              mtu = (int) shield.turnMax,
              drt = (int) shield.damage_rate,
              dvl = (int) shield.damage_value
            });
          unitInfo.hpi.Clear();
          foreach (SkillData judgeHpList in allUnit.JudgeHpLists)
            unitInfo.hpi.Add(judgeHpList.SkillID);
          unitInfo.mhl.Clear();
          foreach (Unit.UnitMhmDamage mhmDamageList in allUnit.MhmDamageLists)
            unitInfo.mhl.Add(new BattleSuspend.Data.UnitInfo.MhmDmg()
            {
              typ = (int) mhmDamageList.mType,
              dmg = (int) mhmDamageList.mDamage
            });
          data.uil.Add(unitInfo);
        }
      }
      data.itl.Clear();
      foreach (KeyValuePair<OString, OInt> usedItem in battle.GetQuestRecord().used_items)
        data.itl.Add(new BattleSuspend.Data.UsedItem()
        {
          iti = (string) usedItem.Key,
          num = (int) usedItem.Value
        });
      data.trl.Clear();
      foreach (TrickData trickData in TrickData.GetEffectAll())
        data.trl.Add(new BattleSuspend.Data.TrickInfo()
        {
          tid = trickData.TrickParam.Iname,
          val = (bool) trickData.Valid,
          cun = BattleSuspend.GetIdxFromAllUnits(battle, trickData.CreateUnit),
          rnk = (int) trickData.Rank,
          rcp = (int) trickData.RankCap,
          grx = (int) trickData.GridX,
          gry = (int) trickData.GridY,
          rac = (int) trickData.RestActionCount,
          ccl = (int) trickData.CreateClock,
          tag = trickData.Tag
        });
      data.sel.Clear();
      foreach (string key in battle.SkillExecLogs.Keys)
        data.sel.Add(new BattleSuspend.Data.SkillExecLogInfo()
        {
          inm = key,
          ucnt = battle.SkillExecLogs[key].use_count,
          kcnt = battle.SkillExecLogs[key].kill_count
        });
      BattleSuspend.Data.Variables var = data.var;
      var.wtc = battle.WinTriggerCount;
      var.ltc = battle.LoseTriggerCount;
      var.act = battle.mActionCounts;
      var.kls = battle.Killstreak;
      var.mks = battle.MaxKillstreak;
      var.thl = battle.TotalHeal;
      var.tdt = battle.TotalDamagesTaken;
      var.tdm = battle.TotalDamages;
      var.tdc = battle.TotalDamagesTakenCount;
      var.mdm = battle.MaxDamage;
      var.nui = battle.NumUsedItems;
      var.nus = battle.NumUsedSkills;
      var.ctm = battle.ClockTime;
      var.ctt = battle.ClockTimeTotal;
      var.coc = battle.ContinueCount;
      var.fns = battle.FinisherIname;
      var.glc = instance.GoldCount;
      var.trc = instance.TreasureCount;
      var.rsd = battle.Seed;
      uint[] seed = battle.Rand.GetSeed();
      if (seed != null)
      {
        var.ris = new uint[seed.Length];
        seed.CopyTo((Array) var.ris, 0);
      }
      var.gsl.Clear();
      foreach (GimmickEvent gimmickEvent in battle.GimmickEventList)
        var.gsl.Add(new BattleSuspend.Data.Variables.GimmickEvent()
        {
          ctr = gimmickEvent.count,
          cmp = !gimmickEvent.IsCompleted ? 0 : 1
        });
      var.ssl.Clear();
      if ((UnityEngine.Object) instance.EventScript != (UnityEngine.Object) null && instance.EventScript.mSequences != null)
      {
        foreach (EventScript.ScriptSequence mSequence in instance.EventScript.mSequences)
          var.ssl.Add(new BattleSuspend.Data.Variables.ScriptEvent()
          {
            trg = mSequence.Triggered
          });
      }
      var.tkk = battle.TargetKillstreak.Keys.ToArray<string>();
      var.tkv = battle.TargetKillstreak.Values.ToArray<int>();
      var.mtk = battle.MaxTargetKillstreak.Keys.ToArray<string>();
      var.mtv = battle.MaxTargetKillstreak.Values.ToArray<int>();
      var.pbm = battle.PlayByManually;
      var.uam = battle.IsUseAutoPlayMode;
      var.wti.wid = (string) null;
      WeatherData currentWeatherData = WeatherData.CurrentWeatherData;
      if (currentWeatherData != null)
      {
        var.wti.wid = currentWeatherData.WeatherParam.Iname;
        var.wti.mun = BattleSuspend.GetIdxFromAllUnits(battle, currentWeatherData.ModifyUnit);
        var.wti.rnk = (int) currentWeatherData.Rank;
        var.wti.rcp = (int) currentWeatherData.RankCap;
        var.wti.ccl = (int) currentWeatherData.ChangeClock;
      }
      var.ctd = battle.CurrentTeamId;
      var.mtd = battle.MaxTeamId;
      var.pbd = instance.EventPlayBgmID;
      data.ivl = true;
      return data;
    }

    private static bool restoreSaveData(BattleSuspend.Data data)
    {
      if (data == null || !data.ivl)
        return false;
      SceneBattle instance = SceneBattle.Instance;
      if (!(bool) ((UnityEngine.Object) instance))
        return false;
      BattleCore battle = instance.Battle;
      if (battle == null)
        return false;
      BattleSuspend.Data.Header hdr = data.hdr;
      if (hdr.apv != Application.version)
      {
        DebugUtility.LogWarning("BattleSuspend/Restoration failure! Version is different.");
        return false;
      }
      if (hdr.arv != AssetManager.AssetRevision)
      {
        DebugUtility.LogWarning("BattleSuspend/Restoration failure! Revision is different.");
        return false;
      }
      if (hdr.qid != battle.QuestID)
      {
        DebugUtility.LogWarning("BattleSuspend/Restoration failure! QuestID is different.");
        return false;
      }
      if (hdr.bid != battle.BtlID)
      {
        DebugUtility.LogWarning("BattleSuspend/Restoration failure! BattleID is different.");
        return false;
      }
      if (hdr.key != battle.UniqueKey)
      {
        DebugUtility.LogWarning("BattleSuspend/Restoration failure! Key is different.");
        return false;
      }
      GameUtility.Config_AutoMode_Treasure.Value = hdr.cat;
      GameUtility.Config_AutoMode_DisableSkill.Value = hdr.cad;
      battle.RemoveUnitsByUnitFlag(EUnitFlag.InfinitySpawn);
      for (int count = battle.AllUnits.Count; count < data.uil.Count; ++count)
      {
        BattleSuspend.Data.UnitInfo unitInfo = data.uil[count];
        if ((unitInfo.ufg & 67108864) != 0)
        {
          int dfu = unitInfo.dfu;
          while (0 <= dfu && dfu < data.uil.Count && data.uil[dfu].dfu >= 0)
            dfu = data.uil[dfu].dfu;
          Unit unitFromAllUnits = BattleSuspend.GetUnitFromAllUnits(battle, dfu);
          battle.DtuCreateUnit(unitFromAllUnits, unitInfo.did);
        }
        else if (unitInfo.ist >= 0)
          battle.CreateInfinitySpawnUnit(unitInfo.nam, unitInfo.ugx, unitInfo.ugy, unitInfo.dir, unitInfo.ist, unitInfo.isd, false);
        else if (!string.IsNullOrEmpty(unitInfo.boi))
          battle.BreakObjCreate(unitInfo.boi, unitInfo.ugx, unitInfo.ugy, (Unit) null, (LogSkill) null, 0);
      }
      if (battle.IsOrdeal)
        battle.OrdealRestore(data.var.ctd);
      for (int index = 0; index < data.uil.Count && index < battle.AllUnits.Count; ++index)
      {
        BattleSuspend.Data.UnitInfo unit_info = data.uil[index];
        Unit allUnit = battle.AllUnits[index];
        if (!(allUnit.UnitName != unit_info.nam))
          allUnit.SetupSuspend(battle, unit_info);
      }
      BattleCore.Record questRecord = battle.GetQuestRecord();
      foreach (BattleSuspend.Data.UsedItem usedItem in data.itl)
      {
        questRecord.used_items[(OString) usedItem.iti] = (OInt) usedItem.num;
        battle.FindInventoryByItemID(usedItem.iti)?.Used(usedItem.num);
      }
      TrickData.ClearEffect();
      foreach (BattleSuspend.Data.TrickInfo trickInfo in data.trl)
      {
        Unit unitFromAllUnits = BattleSuspend.GetUnitFromAllUnits(battle, trickInfo.cun);
        TrickData.SuspendEffect(trickInfo.tid, trickInfo.grx, trickInfo.gry, trickInfo.tag, unitFromAllUnits, trickInfo.ccl, trickInfo.rnk, trickInfo.rcp, trickInfo.rac);
      }
      battle.SkillExecLogs.Clear();
      foreach (BattleSuspend.Data.SkillExecLogInfo _log_info in data.sel)
      {
        BattleCore.SkillExecLog skillExecLog = new BattleCore.SkillExecLog();
        skillExecLog.Restore(_log_info);
        battle.SkillExecLogs.Add(_log_info.inm, skillExecLog);
      }
      BattleSuspend.Data.Variables var = data.var;
      battle.WinTriggerCount = var.wtc;
      battle.LoseTriggerCount = var.ltc;
      battle.mActionCounts = var.act;
      battle.Killstreak = var.kls;
      battle.MaxKillstreak = var.mks;
      battle.TotalHeal = var.thl;
      battle.TotalDamagesTaken = var.tdt;
      battle.TotalDamages = var.tdm;
      battle.TotalDamagesTakenCount = var.tdc;
      battle.MaxDamage = var.mdm;
      battle.NumUsedItems = var.nui;
      battle.NumUsedSkills = var.nus;
      battle.ClockTime = var.ctm;
      battle.ClockTimeTotal = var.ctt;
      battle.ContinueCount = var.coc;
      battle.FinisherIname = var.fns;
      instance.GoldCount = var.glc;
      instance.RestoreTreasureCount(var.trc);
      battle.Seed = var.rsd;
      battle.PlayByManually = var.pbm;
      battle.IsUseAutoPlayMode = var.uam;
      if (var.ris != null)
      {
        for (int index = 0; index < var.ris.Length; ++index)
          battle.SetRandSeed(index, var.ris[index]);
      }
      if (var.gsl.Count == battle.GimmickEventList.Count)
      {
        for (int index = 0; index < var.gsl.Count; ++index)
        {
          BattleSuspend.Data.Variables.GimmickEvent gimmickEvent = var.gsl[index];
          battle.GimmickEventList[index].count = gimmickEvent.ctr;
          battle.GimmickEventList[index].IsCompleted = gimmickEvent.cmp != 0;
        }
      }
      if (var.ssl.Count != 0)
      {
        battle.EventTriggers = new bool[var.ssl.Count];
        for (int index = 0; index < var.ssl.Count; ++index)
        {
          BattleSuspend.Data.Variables.ScriptEvent scriptEvent = var.ssl[index];
          battle.EventTriggers[index] = scriptEvent.trg;
        }
      }
      Dictionary<string, int> dictionary1 = new Dictionary<string, int>();
      int num1 = Math.Min(var.tkk.Length, var.tkv.Length);
      for (int index = 0; index < num1; ++index)
        dictionary1.Add(var.tkk[index], var.tkv[index]);
      battle.TargetKillstreak = dictionary1;
      Dictionary<string, int> dictionary2 = new Dictionary<string, int>();
      int num2 = Math.Min(var.mtk.Length, var.mtv.Length);
      for (int index = 0; index < num2; ++index)
        dictionary2.Add(var.mtk[index], var.mtv[index]);
      battle.MaxTargetKillstreak = dictionary2;
      BattleSuspend.Data.Variables.WeatherInfo wti = var.wti;
      if (!string.IsNullOrEmpty(wti.wid))
      {
        Unit unitFromAllUnits = BattleSuspend.GetUnitFromAllUnits(battle, wti.mun);
        WeatherData.SuspendWeather(wti.wid, battle.Units, unitFromAllUnits, wti.rnk, wti.rcp, wti.ccl);
      }
      battle.CurrentTeamId = var.ctd;
      battle.MaxTeamId = var.mtd;
      instance.EventPlayBgmID = var.pbd;
      return true;
    }

    public static int GetIdxFromAllUnits(BattleCore bc, Unit unit)
    {
      if (bc == null)
        bc = !(bool) ((UnityEngine.Object) SceneBattle.Instance) ? (BattleCore) null : SceneBattle.Instance.Battle;
      if (bc == null || unit == null)
        return -1;
      for (int index = 0; index < bc.AllUnits.Count; ++index)
      {
        if (bc.AllUnits[index].Equals((object) unit))
          return index;
      }
      return -1;
    }

    public static Unit GetUnitFromAllUnits(BattleCore bc, int idx)
    {
      if (bc == null)
        bc = !(bool) ((UnityEngine.Object) SceneBattle.Instance) ? (BattleCore) null : SceneBattle.Instance.Battle;
      if (bc == null || idx < 0 || idx >= bc.AllUnits.Count)
        return (Unit) null;
      return bc.AllUnits[idx];
    }

    public static bool IsExistData()
    {
      return File.Exists(BattleSuspend.mSuspendFileName);
    }

    public static void RemoveData()
    {
      string mSuspendFileName = BattleSuspend.mSuspendFileName;
      if (!File.Exists(mSuspendFileName))
        return;
      BattleSuspend.writeSaveData(mSuspendFileName, (BattleSuspend.Data) null);
      File.Delete(mSuspendFileName);
    }

    public static bool SaveData()
    {
      BattleSuspend.Data data = BattleSuspend.makeSaveData();
      if (data == null)
        return false;
      return BattleSuspend.writeSaveData(BattleSuspend.mSuspendFileName, data);
    }

    public static bool LoadData()
    {
      BattleSuspend.Data data = BattleSuspend.loadSaveData(BattleSuspend.mSuspendFileName);
      if (data == null)
        return false;
      return BattleSuspend.restoreSaveData(data);
    }

    [Serializable]
    public class Data
    {
      public BattleSuspend.Data.Header hdr = new BattleSuspend.Data.Header();
      public List<BattleSuspend.Data.UnitInfo> uil = new List<BattleSuspend.Data.UnitInfo>();
      public List<BattleSuspend.Data.UsedItem> itl = new List<BattleSuspend.Data.UsedItem>();
      public List<BattleSuspend.Data.TrickInfo> trl = new List<BattleSuspend.Data.TrickInfo>();
      public List<BattleSuspend.Data.SkillExecLogInfo> sel = new List<BattleSuspend.Data.SkillExecLogInfo>();
      public BattleSuspend.Data.Variables var = new BattleSuspend.Data.Variables();
      public bool ivl;

      [Serializable]
      public class Header
      {
        public string apv;
        public int arv;
        public string qid;
        public long bid;
        public bool cat;
        public bool cad;
        public string key;
      }

      [Serializable]
      public class UnitInfo
      {
        public int ist = -1;
        public List<BattleSuspend.Data.UnitInfo.Insp> iil = new List<BattleSuspend.Data.UnitInfo.Insp>();
        public List<int> iul = new List<int>();
        public List<BattleSuspend.Data.UnitInfo.AbilChg> acl = new List<BattleSuspend.Data.UnitInfo.AbilChg>();
        public List<BattleSuspend.Data.UnitInfo.AddedAbil> aal = new List<BattleSuspend.Data.UnitInfo.AddedAbil>();
        public List<BattleSuspend.Data.UnitInfo.SkillUse> sul = new List<BattleSuspend.Data.UnitInfo.SkillUse>();
        public List<BattleSuspend.Data.UnitInfo.Buff> bfl = new List<BattleSuspend.Data.UnitInfo.Buff>();
        public List<BattleSuspend.Data.UnitInfo.Cond> cdl = new List<BattleSuspend.Data.UnitInfo.Cond>();
        public List<BattleSuspend.Data.UnitInfo.Shield> shl = new List<BattleSuspend.Data.UnitInfo.Shield>();
        public List<string> hpi = new List<string>();
        public List<BattleSuspend.Data.UnitInfo.MhmDmg> mhl = new List<BattleSuspend.Data.UnitInfo.MhmDmg>();
        public string nam;
        public int nhp;
        public int chp;
        public int gem;
        public int ugx;
        public int ugy;
        public int dir;
        public int ufg;
        public bool isb;
        public int crt;
        public int tgi;
        public int rti;
        public string csi;
        public int ctm;
        public int cid;
        public int cti;
        public int ctx;
        public int cty;
        public int cgw;
        public int cgh;
        public int[] cgm;
        public int dct;
        public int ajw;
        public int wtt;
        public int mvt;
        public int acc;
        public int tuc;
        public int trc;
        public int klc;
        public int[] etr;
        public int aid;
        public int atu;
        public int apt;
        public string boi;
        public int boc;
        public int tid;
        public int fst;
        public int isd;
        public string did;
        public int dfu;
        public int drt;
        public int okd;

        [Serializable]
        public class Insp
        {
          public int aii;
          public int sno;
          public int val;
          public int cct;
        }

        [Serializable]
        public class AbilChg
        {
          public List<BattleSuspend.Data.UnitInfo.AbilChg.Data> acd = new List<BattleSuspend.Data.UnitInfo.AbilChg.Data>();

          [Serializable]
          public class Data
          {
            public string fid;
            public string tid;
            public int tur;
            public int irs;
            public int exp;
            public int iif;
          }
        }

        [Serializable]
        public class AddedAbil
        {
          public string aid;
          public int exp;
          public bool nct;
        }

        [Serializable]
        public class SkillUse
        {
          public string sid;
          public int ctr;
        }

        [Serializable]
        public class Buff
        {
          public List<int> atl = new List<int>();
          public string sid;
          public int stg;
          public int tur;
          public int uni;
          public int cui;
          public int tim;
          public bool ipa;
          public int ucd;
          public int btp;
          public int vtp;
          public int ctp;
          public uint lid;
          public int ubc;
        }

        [Serializable]
        public class Cond
        {
          public string sid;
          public int stg;
          public string cid;
          public int tur;
          public int uni;
          public int cui;
          public int tim;
          public bool ipa;
          public int ucd;
          public int cdt;
          public int cnd;
          public bool icu;
          public uint lid;
        }

        [Serializable]
        public class Shield
        {
          public string inm;
          public int nhp;
          public int mhp;
          public int ntu;
          public int mtu;
          public int drt;
          public int dvl;
        }

        [Serializable]
        public class MhmDmg
        {
          public int typ;
          public int dmg;
        }
      }

      [Serializable]
      public class UsedItem
      {
        public string iti;
        public int num;
      }

      [Serializable]
      public class TrickInfo
      {
        public string tid;
        public bool val;
        public int cun;
        public int rnk;
        public int rcp;
        public int grx;
        public int gry;
        public int rac;
        public int ccl;
        public string tag;
      }

      [Serializable]
      public class SkillExecLogInfo
      {
        public string inm;
        public int ucnt;
        public int kcnt;
      }

      [Serializable]
      public class Variables
      {
        public List<BattleSuspend.Data.Variables.GimmickEvent> gsl = new List<BattleSuspend.Data.Variables.GimmickEvent>();
        public List<BattleSuspend.Data.Variables.ScriptEvent> ssl = new List<BattleSuspend.Data.Variables.ScriptEvent>();
        public BattleSuspend.Data.Variables.WeatherInfo wti = new BattleSuspend.Data.Variables.WeatherInfo();
        public int wtc;
        public int ltc;
        public int[] act;
        public int kls;
        public int mks;
        public string[] tkk;
        public int[] tkv;
        public string[] mtk;
        public int[] mtv;
        public bool pbm;
        public bool uam;
        public int thl;
        public int tdt;
        public int tdm;
        public int tdc;
        public int mdm;
        public int nui;
        public int nus;
        public int ctm;
        public int ctt;
        public int coc;
        public string fns;
        public int glc;
        public int trc;
        public uint rsd;
        public uint[] ris;
        public int ctd;
        public int mtd;
        public string pbd;

        [Serializable]
        public class GimmickEvent
        {
          public int ctr;
          public int cmp;
        }

        [Serializable]
        public class ScriptEvent
        {
          public bool trg;
        }

        [Serializable]
        public class WeatherInfo
        {
          public string wid;
          public int mun;
          public int rnk;
          public int rcp;
          public int ccl;
        }
      }
    }
  }
}
