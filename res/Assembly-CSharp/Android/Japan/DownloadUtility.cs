// Decompiled with JetBrains decompiler
// Type: DownloadUtility
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using SRPG;
using System;
using System.Collections.Generic;
using System.Text;

public static class DownloadUtility
{
  public static void DownloadQuestBase(QuestParam q)
  {
    if (q.map.Count > 0)
    {
      string mapSceneName = q.map[0].mapSceneName;
      string mapSetName = q.map[0].mapSetName;
      if (!string.IsNullOrEmpty(mapSceneName))
      {
        AssetManager.PrepareAssets(mapSceneName);
        AssetManager.PrepareAssets(AssetPath.LocalMap(mapSceneName));
      }
      if (!string.IsNullOrEmpty(mapSetName))
        AssetManager.PrepareAssets(AssetPath.LocalMap(mapSetName));
    }
    if (!string.IsNullOrEmpty(q.storyTextID))
      AssetManager.PrepareAssets(LocalizedText.GetResourcePath(q.storyTextID));
    if (!string.IsNullOrEmpty(q.navigation))
      AssetManager.PrepareAssets(AssetPath.Navigation(q));
    if (!string.IsNullOrEmpty(q.event_start))
      AssetManager.PrepareAssets(AssetPath.QuestEvent(q.event_start));
    if (q.map != null)
    {
      for (int index = 0; index < q.map.Count; ++index)
      {
        if (!string.IsNullOrEmpty(q.map[index].eventSceneName))
          AssetManager.PrepareAssets(AssetPath.QuestEvent(q.map[index].eventSceneName));
        if (!string.IsNullOrEmpty(q.map[index].bgmName))
        {
          AssetManager.PrepareAssets("StreamingAssets/" + q.map[index].bgmName + ".acb");
          AssetManager.PrepareAssets("StreamingAssets/" + q.map[index].bgmName + ".awb");
        }
      }
    }
    if (!string.IsNullOrEmpty(q.event_clear))
      AssetManager.PrepareAssets(AssetPath.QuestEvent(q.event_clear));
    AssetManager.PrepareAssets("StreamingAssets/BGM_0006.acb");
    AssetManager.PrepareAssets("StreamingAssets/BGM_0006.awb");
  }

  public static void DownloadQuestMaps(QuestParam quest, bool dlStatusEffects = false)
  {
    if (dlStatusEffects && !BadStatusEffects.IsLoaded())
      DebugUtility.LogError("dlStatusEffects を true に指定する場合、BadStatusEffectsを先に読み込んでおく必要があります");
    for (int index1 = 0; index1 < quest.map.Count; ++index1)
    {
      if (!string.IsNullOrEmpty(quest.map[index1].mapSetName))
      {
        string src = AssetManager.LoadTextData(AssetPath.LocalMap(quest.map[index1].mapSetName));
        if (src != null)
        {
          JSON_MapUnit jsonObject = JSONParser.parseJSONObject<JSON_MapUnit>(src);
          List<JSON_MapEnemyUnit> jsonMapEnemyUnitList = new List<JSON_MapEnemyUnit>();
          if (jsonObject.enemy != null)
            jsonMapEnemyUnitList.AddRange((IEnumerable<JSON_MapEnemyUnit>) jsonObject.enemy);
          if (jsonObject.HasRandomEnemies)
            jsonMapEnemyUnitList.AddRange((IEnumerable<JSON_MapEnemyUnit>) jsonObject.GetMapEnemyUnits_AvailableRandom());
          if (jsonObject.inf_deck != null)
            jsonMapEnemyUnitList.AddRange((IEnumerable<JSON_MapEnemyUnit>) jsonObject.GetMapEnemyUnits_AvailableInfinitySpawn());
          List<Unit> units = new List<Unit>();
          for (int index2 = 0; index2 < jsonMapEnemyUnitList.Count; ++index2)
          {
            if (!jsonMapEnemyUnitList[index2].IsRandSymbol)
            {
              NPCSetting npcSetting = new NPCSetting(jsonMapEnemyUnitList[index2]);
              Unit unit = new Unit();
              unit.Setup((UnitData) null, (UnitSetting) npcSetting, (Unit.DropItem) null, (Unit.DropItem) null);
              units.Add(unit);
            }
          }
          for (int index2 = 0; index2 < units.Count; ++index2)
          {
            DownloadUtility.DownloadUnit(units[index2], dlStatusEffects, false);
            Unit unit = BattleCore.SearchTransformUnit(units, units[index2]);
            if (unit != null)
              DownloadUtility.DownloadTransformAnimation(unit, units[index2]);
          }
          if (jsonObject.tricks != null)
          {
            foreach (JSON_MapTrick trick in jsonObject.tricks)
            {
              if (!string.IsNullOrEmpty(trick.id))
              {
                TrickParam trickParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetTrickParam(trick.id);
                if (trickParam != null)
                {
                  AssetManager.PrepareAssets(AssetPath.TrickIconUI(trickParam.MarkerName));
                  if (!string.IsNullOrEmpty(trickParam.EffectName))
                    AssetManager.PrepareAssets(AssetPath.TrickEffect(trickParam.EffectName));
                }
              }
            }
          }
        }
      }
    }
    if (string.IsNullOrEmpty(quest.WeatherSetId))
      return;
    WeatherSetParam weatherSetParam = MonoSingleton<GameManager>.GetInstanceDirect().GetWeatherSetParam(quest.WeatherSetId);
    if (weatherSetParam == null)
      return;
    foreach (string startWeatherIdList in weatherSetParam.StartWeatherIdLists)
      DownloadUtility.WeatherPrepareAsset(startWeatherIdList);
    foreach (string changeWeatherIdList in weatherSetParam.ChangeWeatherIdLists)
      DownloadUtility.WeatherPrepareAsset(changeWeatherIdList);
  }

  public static void DownloadQuestResult(QuestParam q)
  {
    if (q.bonusObjective == null)
      return;
    foreach (QuestBonusObjective questBonusObjective in q.bonusObjective)
    {
      if (questBonusObjective != null && questBonusObjective.itemType == RewardType.ConceptCard)
      {
        ConceptCardParam conceptCardParam = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(questBonusObjective.item);
        if (conceptCardParam != null)
          DownloadUtility.DownloadConceptCard(conceptCardParam);
        else
          DebugUtility.LogError(string.Format("クエストミッションに指定した真理念装のIDが不正です。{0}->{1}", (object) q.iname, (object) questBonusObjective.item));
      }
    }
  }

  public static void WeatherPrepareAsset(string weather_id)
  {
    if (string.IsNullOrEmpty(weather_id))
      return;
    WeatherParam weatherParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetWeatherParam(weather_id);
    if (weatherParam == null)
      return;
    if (!string.IsNullOrEmpty(weatherParam.Icon))
      AssetManager.PrepareAssets(AssetPath.WeatherIcon(weatherParam.Icon));
    if (string.IsNullOrEmpty(weatherParam.Effect))
      return;
    AssetManager.PrepareAssets(AssetPath.WeatherEffect(weatherParam.Effect));
  }

  public static void DownloadQuestEnemy(JSON_MapUnit mapset)
  {
    if (mapset.enemy == null)
      return;
    for (int index = 0; index < mapset.enemy.Length; ++index)
    {
      if (!mapset.enemy[index].IsRandSymbol)
        DownloadUtility.DownloadUnit(new NPCSetting(mapset.enemy[index]));
    }
  }

  public static void DownloadTowerQuestEnemyIcon(JSON_MapUnit mapset)
  {
    if (mapset == null)
      return;
    if (mapset.enemy != null)
    {
      for (int index = 0; index < mapset.enemy.Length; ++index)
      {
        if (!mapset.enemy[index].IsRandSymbol)
          DownloadUtility.LoadUnitIconMedium(mapset.enemy[index].iname);
      }
    }
    if (mapset.deck == null)
      return;
    for (int index = 0; index < mapset.deck.Length; ++index)
      DownloadUtility.LoadUnitIconMedium(mapset.deck[index].iname);
  }

  public static void LoadUnitIconMedium(string iname)
  {
    UnitParam unitParam = MonoSingleton<GameManager>.Instance.GetUnitParam(iname);
    if (unitParam == null)
      return;
    if (unitParam.jobsets != null)
    {
      for (int index = 0; index < unitParam.jobsets.Length; ++index)
      {
        JobSetParam jobSetParam = MonoSingleton<GameManager>.Instance.GetJobSetParam(unitParam.jobsets[index]);
        AssetManager.PrepareAssets(AssetPath.UnitIconMedium(unitParam, jobSetParam.job));
      }
    }
    else
      AssetManager.PrepareAssets(AssetPath.UnitIconMedium(unitParam, string.Empty));
  }

  public static void DownloadTowerQuests(List<TowerFloorParam> floorParams)
  {
    for (int index1 = 0; index1 < floorParams.Count; ++index1)
    {
      QuestParam questParam = floorParams[index1].GetQuestParam();
      for (int index2 = 0; index2 < questParam.map.Count; ++index2)
      {
        if (!string.IsNullOrEmpty(questParam.map[index2].mapSetName))
        {
          string src = AssetManager.LoadTextData(AssetPath.LocalMap(questParam.map[index2].mapSetName));
          if (src != null)
            DownloadUtility.DownloadTowerQuestEnemyIcon(JSONParser.parseJSONObject<JSON_MapUnit>(src));
        }
      }
    }
  }

  public static void DownloadMultiTowerQuest(MultiTowerFloorParam floorParam)
  {
    if (floorParam.map == null)
      return;
    for (int index1 = 0; index1 < floorParam.map.Count; ++index1)
    {
      if (!string.IsNullOrEmpty(floorParam.map[index1].mapSetName))
      {
        string src = AssetManager.LoadTextData(AssetPath.LocalMap(floorParam.map[index1].mapSetName));
        if (src != null)
        {
          JSON_MapUnit jsonObject = JSONParser.parseJSONObject<JSON_MapUnit>(src);
          if (jsonObject != null)
          {
            for (int index2 = 0; index2 < jsonObject.enemy.Length; ++index2)
              DownloadUtility.LoadUnitIconMedium(jsonObject.enemy[index2].iname);
          }
        }
      }
    }
  }

  public static void DownloadJobEquipment(JobParam job)
  {
    if (job == null)
      return;
    string resourcePath = AssetPath.JobEquipment(job);
    if (string.IsNullOrEmpty(resourcePath))
      return;
    AssetManager.PrepareAssets(resourcePath);
  }

  public static void DownloadArtifact(ArtifactParam artifalct)
  {
    if (artifalct == null)
      return;
    string resourcePath = AssetPath.Artifacts(artifalct);
    if (string.IsNullOrEmpty(resourcePath))
      return;
    AssetManager.PrepareAssets(resourcePath);
  }

  public static void DownloadConceptCard(ConceptCardParam concept_card)
  {
    if (concept_card == null)
      return;
    string resourcePath = AssetPath.ConceptCard(concept_card);
    if (string.IsNullOrEmpty(resourcePath))
      return;
    AssetManager.PrepareAssets(resourcePath);
    if (!string.IsNullOrEmpty(concept_card.first_get_unit))
      DownloadUtility.DownloadUnit(MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(concept_card.first_get_unit), (JobData[]) null);
    if (concept_card.effects == null)
      return;
    string empty = string.Empty;
    for (int i = 0; i < concept_card.effects.Length; ++i)
    {
      ArtifactParam artifactParam = MonoSingleton<GameManager>.Instance.MasterParam.Artifacts.Find((Predicate<ArtifactParam>) (art => art.iname == concept_card.effects[i].skin));
      if (artifactParam != null)
      {
        UnitGroupParam unitGroupParam = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardConditions(concept_card.effects[i].cnds_iname).GetUnitGroupParam();
        if (unitGroupParam != null)
        {
          for (int index1 = 0; index1 < unitGroupParam.units.Length; ++index1)
          {
            UnitParam unitParam = MonoSingleton<GameManager>.Instance.GetUnitParam(unitGroupParam.units[index1]);
            if (unitParam != null)
            {
              AssetManager.PrepareAssets(AssetPath.UnitSkinImage(unitParam, artifactParam, (string) null));
              AssetManager.PrepareAssets(AssetPath.UnitSkinImage2(unitParam, artifactParam, (string) null));
              AssetManager.PrepareAssets(AssetPath.UnitSkinIconSmall(unitParam, artifactParam, (string) null));
              AssetManager.PrepareAssets(AssetPath.UnitSkinIconMedium(unitParam, artifactParam, (string) null));
              AssetManager.PrepareAssets(AssetPath.UnitSkinEyeImage(unitParam, artifactParam, (string) null));
              if (!string.IsNullOrEmpty(artifactParam.voice))
              {
                string charName = AssetPath.UnitVoiceFileName(unitParam, artifactParam, string.Empty);
                if (!string.IsNullOrEmpty(charName))
                {
                  string[] strArray = MySound.VoiceCueSheetFileName(charName);
                  if (strArray != null)
                  {
                    for (int index2 = 0; index2 < strArray.Length; ++index2)
                      AssetManager.PrepareAssets("StreamingAssets/" + strArray[index2]);
                  }
                }
              }
            }
          }
        }
      }
    }
  }

  public static void DownloadUnit(UnitParam unit, JobData[] jobs = null)
  {
    if (unit == null)
      return;
    CharacterDB.Character character = CharacterDB.FindCharacter(unit.model);
    if (character == null)
      return;
    GameManager instance = MonoSingleton<GameManager>.Instance;
    if (unit.jobsets != null)
    {
      for (int index = 0; index < unit.jobsets.Length; ++index)
      {
        JobSetParam jobSetParam = instance.GetJobSetParam(unit.jobsets[index]);
        while (jobSetParam != null)
        {
          JobParam jobParam = instance.GetJobParam(jobSetParam.job);
          DownloadUtility.DownloadJobEquipment(jobParam);
          ArtifactParam artifactParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetArtifactParam(jobParam.artifact);
          if (artifactParam != null)
            DownloadUtility.DownloadArtifact(artifactParam);
          int artifactSlotIndex = JobData.GetArtifactSlotIndex(ArtifactTypes.Arms);
          if (jobs != null)
          {
            foreach (JobData job in jobs)
            {
              JobData jd = job;
              if (jd != null)
              {
                List<ArtifactData> artifacts = MonoSingleton<GameManager>.GetInstanceDirect().Player.Artifacts;
                long uniqId = jd.Artifacts[artifactSlotIndex];
                ArtifactData artifactData = artifacts.Find((Predicate<ArtifactData>) (item => (long) item.UniqueID == uniqId));
                if (artifactData != null && artifactData.ArtifactParam.type == ArtifactTypes.Arms)
                  DownloadUtility.DownloadArtifact(artifactData.ArtifactParam);
                SkillSequence sequence = SkillSequence.FindSequence(jd.GetAttackSkill().SkillParam.motion, false);
                if (sequence != null)
                {
                  CharacterDB.Job jobData = character.Jobs.Find((Predicate<CharacterDB.Job>) (x => x.JobID == jd.JobResourceID));
                  if (jobData != null)
                    DownloadUtility.PrepareUnitAnimation(jobData, sequence.SkillAnimation.Name, false, false);
                }
              }
            }
          }
          else
          {
            DownloadUtility.DownloadArtifact(instance.MasterParam.GetArtifactParam(jobParam.artifact));
            SkillData skillData = new SkillData();
            if (skillData != null)
            {
              if (!string.IsNullOrEmpty(jobParam.atkskill[0]))
                skillData.Setup(jobParam.atkskill[0], 1, 1, (MasterParam) null);
              else
                skillData.Setup(jobParam.atkskill[(int) unit.element], 1, 1, (MasterParam) null);
              SkillSequence sequence = SkillSequence.FindSequence(skillData.SkillParam.motion, false);
              if (sequence != null)
              {
                CharacterDB.Job characterData = DownloadUtility.GetCharacterData(unit, jobParam, (ArtifactParam) null);
                if (characterData != null)
                  DownloadUtility.PrepareUnitAnimation(characterData, sequence.SkillAnimation.Name, false, false);
              }
              else
                continue;
            }
          }
          AssetManager.PrepareAssets(AssetPath.UnitImage(unit, jobSetParam.job));
          AssetManager.PrepareAssets(AssetPath.UnitImage2(unit, jobSetParam.job));
          AssetManager.PrepareAssets(AssetPath.UnitIconSmall(unit, jobSetParam.job));
          AssetManager.PrepareAssets(AssetPath.UnitIconMedium(unit, jobSetParam.job));
          AssetManager.PrepareAssets(AssetPath.UnitEyeImage(unit, jobSetParam.job));
          CharacterDB.Job characterData1 = DownloadUtility.GetCharacterData(unit, jobParam, (ArtifactParam) null);
          if (characterData1 != null && characterData1.JobID != jobParam.model)
          {
            CharacterDB.Job jobData = new CharacterDB.Job();
            jobData.JobID = jobParam.model;
            jobData.AssetPrefix = characterData1.AssetPrefix;
            DownloadUtility.PrepareUnitAnimation(jobData, "unit_info_idle0", true, false);
            DownloadUtility.PrepareUnitAnimation(jobData, "unit_info_act0", true, false);
            DebugUtility.LogError("Job is Different:" + characterData1.JobID + " / " + jobParam.model);
          }
          jobSetParam = string.IsNullOrEmpty(jobSetParam.jobchange) ? (JobSetParam) null : instance.GetJobSetParam(jobSetParam.jobchange);
        }
      }
      JobSetParam[] changeJobSetParam = instance.GetClassChangeJobSetParam(unit.iname);
      if (changeJobSetParam != null && changeJobSetParam.Length > 0)
      {
        for (int index = 0; index < changeJobSetParam.Length; ++index)
        {
          JobSetParam jobSetParam = changeJobSetParam[index];
          if (jobSetParam != null)
          {
            JobParam jobParam = instance.GetJobParam(jobSetParam.job);
            ArtifactParam artifactParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetArtifactParam(jobParam.artifact);
            if (artifactParam != null)
              DownloadUtility.DownloadArtifact(artifactParam);
            AssetManager.PrepareAssets(AssetPath.UnitImage(unit, jobSetParam.job));
            AssetManager.PrepareAssets(AssetPath.UnitImage2(unit, jobSetParam.job));
            AssetManager.PrepareAssets(AssetPath.UnitIconSmall(unit, jobSetParam.job));
            AssetManager.PrepareAssets(AssetPath.UnitIconMedium(unit, jobSetParam.job));
            AssetManager.PrepareAssets(AssetPath.UnitEyeImage(unit, jobSetParam.job));
            CharacterDB.Job characterData = DownloadUtility.GetCharacterData(unit, jobParam, (ArtifactParam) null);
            if (characterData != null && characterData.JobID != jobParam.model)
            {
              CharacterDB.Job jobData = new CharacterDB.Job();
              jobData.JobID = jobParam.model;
              jobData.AssetPrefix = characterData.AssetPrefix;
              DownloadUtility.PrepareUnitAnimation(jobData, "unit_info_idle0", true, false);
              DownloadUtility.PrepareUnitAnimation(jobData, "unit_info_act0", true, false);
              DebugUtility.LogError("Job is Different:" + characterData.JobID + " / " + jobParam.model);
            }
            SkillData skillData = new SkillData();
            if (!string.IsNullOrEmpty(jobParam.atkskill[0]))
              skillData.Setup(jobParam.atkskill[0], 1, 1, (MasterParam) null);
            else
              skillData.Setup(jobParam.atkskill[(int) unit.element], 1, 1, (MasterParam) null);
            SkillSequence sequence = SkillSequence.FindSequence(skillData.SkillParam.motion, false);
            if (sequence != null)
              DownloadUtility.PrepareUnitAnimation(characterData, sequence.SkillAnimation.Name, false, false);
          }
        }
      }
    }
    for (int index = 0; index < character.Jobs.Count; ++index)
    {
      CharacterDB.Job job = character.Jobs[index];
      DownloadUtility.PrepareUnitModels(job);
      DownloadUtility.PrepareUnitAnimation(job, "unit_info_idle0", true, false);
      DownloadUtility.PrepareUnitAnimation(job, "unit_info_act0", true, false);
    }
    AssetManager.PrepareAssets("CHM/Home_" + unit.model + "_walk0");
    if (unit.skins != null)
    {
      List<ArtifactParam> artifacts = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.Artifacts;
      for (int i = 0; i < unit.skins.Length; ++i)
      {
        ArtifactParam skin = artifacts.Find((Predicate<ArtifactParam>) (p => p.iname == unit.skins[i]));
        if (skin != null)
        {
          AssetManager.PrepareAssets(AssetPath.UnitSkinImage(unit, skin, (string) null));
          AssetManager.PrepareAssets(AssetPath.UnitSkinImage2(unit, skin, (string) null));
          AssetManager.PrepareAssets(AssetPath.UnitSkinIconSmall(unit, skin, (string) null));
          AssetManager.PrepareAssets(AssetPath.UnitSkinIconMedium(unit, skin, (string) null));
          AssetManager.PrepareAssets(AssetPath.UnitSkinEyeImage(unit, skin, (string) null));
        }
      }
    }
    DownloadUtility.PrepareUnitVoice(unit);
  }

  private static void PrepareUnitVoice(UnitParam unitParam)
  {
    if (unitParam == null || string.IsNullOrEmpty(unitParam.voice))
      return;
    string[] strArray1 = MySound.VoiceCueSheetFileName(unitParam.voice);
    if (strArray1 == null)
      return;
    for (int index = 0; index < strArray1.Length; ++index)
      AssetManager.PrepareAssets("StreamingAssets/" + strArray1[index]);
    string empty = string.Empty;
    if (unitParam.skins == null)
      return;
    List<ArtifactParam> artifacts = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.Artifacts;
    for (int i = 0; i < unitParam.skins.Length; ++i)
    {
      ArtifactParam artifact = artifacts.Find((Predicate<ArtifactParam>) (p => p.iname == unitParam.skins[i]));
      if (artifact != null && !string.IsNullOrEmpty(artifact.voice))
      {
        string charName = AssetPath.UnitVoiceFileName(unitParam, artifact, string.Empty);
        if (!string.IsNullOrEmpty(charName))
        {
          string[] strArray2 = MySound.VoiceCueSheetFileName(charName);
          if (strArray2 != null)
          {
            for (int index = 0; index < strArray2.Length; ++index)
              AssetManager.PrepareAssets("StreamingAssets/" + strArray2[index]);
          }
        }
      }
    }
  }

  public static void DownloadUnit(NPCSetting npc)
  {
    Unit unit = new Unit();
    unit.Setup((UnitData) null, (UnitSetting) npc, (Unit.DropItem) null, (Unit.DropItem) null);
    DownloadUtility.DownloadUnit(unit, false, false);
  }

  public static void DownloadUnit(UnitData unitData, bool dlStatusEffects = false)
  {
    UnitSetting setting = new UnitSetting();
    setting.side = (OInt) 0;
    Unit unit = new Unit();
    unit.Setup(unitData, setting, (Unit.DropItem) null, (Unit.DropItem) null);
    DownloadUtility.DownloadUnit(unit, dlStatusEffects, false);
  }

  public static void PrepareUnitModels(CharacterDB.Job jobData)
  {
    if (!string.IsNullOrEmpty(jobData.BodyName))
      AssetManager.PrepareAssets("CH/BODY/" + jobData.BodyName);
    if (!string.IsNullOrEmpty(jobData.BodyAttachmentName))
      AssetManager.PrepareAssets("CH/BODYOPT/" + jobData.BodyAttachmentName);
    if (!string.IsNullOrEmpty(jobData.BodyTextureName))
      AssetManager.PrepareAssets("CH/BODYTEX/" + jobData.BodyTextureName);
    if (!string.IsNullOrEmpty(jobData.HeadName))
      AssetManager.PrepareAssets("CH/HEAD/" + jobData.HeadName);
    if (!string.IsNullOrEmpty(jobData.HeadAttachmentName))
      AssetManager.PrepareAssets("CH/HEADOPT/" + jobData.HeadAttachmentName);
    if (string.IsNullOrEmpty(jobData.HairName))
      return;
    AssetManager.PrepareAssets("CH/HAIR/" + jobData.HairName);
  }

  private static void PrepareUnitAnimation(CharacterDB.Job jobData, string animationName, bool addJobName, bool is_collabo_skill = false)
  {
    StringBuilder stringBuilder = GameUtility.GetStringBuilder();
    stringBuilder.Append("CHM/");
    string str = jobData.AssetPrefix;
    if (is_collabo_skill)
      str = "D";
    stringBuilder.Append(str);
    stringBuilder.Append('_');
    if (addJobName)
    {
      stringBuilder.Append(jobData.JobID);
      stringBuilder.Append('_');
    }
    stringBuilder.Append(animationName);
    AssetManager.PrepareAssets(stringBuilder.ToString());
  }

  public static CharacterDB.Job GetCharacterData(UnitParam unit, JobParam job, ArtifactParam skin)
  {
    CharacterDB.Character character = CharacterDB.FindCharacter(unit.model);
    if (character == null)
    {
      DebugUtility.LogWarning("Unknown character '" + unit.model + "'");
      return (CharacterDB.Job) null;
    }
    string jobResourceID = skin == null ? string.Empty : skin.asset;
    if (string.IsNullOrEmpty(jobResourceID))
      jobResourceID = job == null ? "none" : job.model;
    int index = character.Jobs.FindIndex((Predicate<CharacterDB.Job>) (p => p.JobID == jobResourceID));
    if (index < 0)
    {
      DebugUtility.LogWarning("Invalid Character " + unit.model + "@" + jobResourceID);
      index = 0;
    }
    return character.Jobs[index];
  }

  public static void DownloadTransformAnimation(Unit unit, Unit skill_unit)
  {
    if (unit == null || skill_unit == null)
      return;
    CharacterDB.Job characterData = DownloadUtility.GetCharacterData(unit.UnitParam, unit.Job == null ? (JobParam) null : unit.Job.Param, unit.UnitData.GetSelectedSkin(-1));
    if (characterData == null)
      return;
    foreach (SkillData battleSkill in skill_unit.BattleSkills)
    {
      if (battleSkill.IsTransformSkill() && !string.IsNullOrEmpty(battleSkill.SkillParam.motion))
      {
        SkillSequence sequence = SkillSequence.FindSequence(battleSkill.SkillParam.motion, false);
        if (sequence != null)
        {
          if (!string.IsNullOrEmpty(sequence.SkillAnimation.Name))
            DownloadUtility.PrepareUnitAnimation(characterData, sequence.SkillAnimation.Name + "_chg", false, false);
          if (!string.IsNullOrEmpty(sequence.StartAnimation))
            DownloadUtility.PrepareUnitAnimation(characterData, sequence.StartAnimation + "_chg", false, false);
        }
      }
    }
  }

  public static void DownloadUnit(Unit unit, bool dlStatusEffects = false, bool dlUnitImage = false)
  {
    UnitParam unitParam = unit.UnitParam;
    JobParam job1 = unit.Job == null ? (JobParam) null : unit.Job.Param;
    ArtifactParam selectedSkin1 = unit.UnitData.GetSelectedSkin(-1);
    CharacterDB.Job characterData1 = DownloadUtility.GetCharacterData(unitParam, job1, selectedSkin1);
    if (characterData1 == null)
      return;
    DownloadUtility.PrepareUnitAssets(characterData1);
    if (job1 != null && characterData1.JobID != job1.model)
    {
      DownloadUtility.PrepareUnitAssets(new CharacterDB.Job()
      {
        AssetPrefix = characterData1.AssetPrefix,
        JobID = job1.model,
        Movable = characterData1.Movable
      });
      DebugUtility.LogError("Job is Different:" + characterData1.JobID + " / " + job1.model);
    }
    if (unit.IsBreakObj)
    {
      CharacterDB.Character character = CharacterDB.FindCharacter(unitParam.model);
      if (character != null)
      {
        for (int index = 1; index < character.Jobs.Count; ++index)
          DownloadUtility.PrepareUnitAssets(character.Jobs[index]);
      }
    }
    if (unit.Job != null)
      DownloadUtility.PrepareJobAssets(unit.Job.Param);
    string jobName = unit.UnitData.CurrentJob == null ? string.Empty : unit.UnitData.CurrentJob.JobID;
    AssetManager.PrepareAssets(AssetPath.UnitSkinIconSmall(unitParam, selectedSkin1, jobName));
    AssetManager.PrepareAssets(AssetPath.UnitSkinIconMedium(unitParam, selectedSkin1, jobName));
    AssetManager.PrepareAssets(AssetPath.UnitSkinEyeImage(unitParam, selectedSkin1, jobName));
    if (dlUnitImage)
      AssetManager.PrepareAssets(AssetPath.UnitSkinImage(unitParam, selectedSkin1, jobName));
    SkillData attackSkill = unit.GetAttackSkill();
    if (attackSkill != null)
      DownloadUtility.PrepareSkillAssets(characterData1, attackSkill.SkillParam);
    List<DynamicTransformUnitParam> transformUnitParamList = new List<DynamicTransformUnitParam>();
    for (int index1 = unit.BattleSkills.Count - 1; index1 >= 0; --index1)
    {
      SkillParam skillParam = unit.BattleSkills[index1].SkillParam;
      if (skillParam != null)
      {
        DownloadUtility.PrepareSkillAssets(characterData1, skillParam);
        if (!string.IsNullOrEmpty(skillParam.AcToAbilId))
        {
          List<SkillParam> sp_lists = new List<SkillParam>();
          DownloadUtility.CreateAbilityChangeSkillList(ref sp_lists, skillParam);
          for (int index2 = 0; index2 < sp_lists.Count; ++index2)
            DownloadUtility.PrepareSkillAssets(characterData1, sp_lists[index2]);
        }
        if (!string.IsNullOrEmpty(skillParam.DynamicTransformUnitId))
        {
          DynamicTransformUnitParam transformUnitParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetDynamicTransformUnitParam(skillParam.DynamicTransformUnitId);
          if (transformUnitParam != null)
          {
            AssetManager.PrepareAssets(transformUnitParam.CancelEffect);
            if (unitParam.iname != transformUnitParam.TrUnitId && !transformUnitParamList.Contains(transformUnitParam))
              transformUnitParamList.Add(transformUnitParam);
          }
        }
      }
    }
    for (int index = 0; index < transformUnitParamList.Count; ++index)
    {
      DynamicTransformUnitParam dtu_param = transformUnitParamList[index];
      Unit unit1 = new Unit();
      unit1.SetupDynamicTransform(unit, dtu_param, true);
      DownloadUtility.DownloadUnit(unit1, dlStatusEffects, false);
      JobParam job2 = unit1.Job == null ? (JobParam) null : unit1.Job.Param;
      ArtifactParam selectedSkin2 = unit1.UnitData.GetSelectedSkin(-1);
      CharacterDB.Job characterData2 = DownloadUtility.GetCharacterData(unit1.UnitParam, job2, selectedSkin2);
      if (characterData2 != null)
      {
        foreach (SkillData battleSkill in unit.BattleSkills)
        {
          if (battleSkill.IsDynamicTransformSkill() && !string.IsNullOrEmpty(battleSkill.SkillParam.motion))
          {
            SkillSequence sequence = SkillSequence.FindSequence(battleSkill.SkillParam.motion, false);
            if (sequence != null)
            {
              if (!string.IsNullOrEmpty(sequence.SkillAnimation.Name))
                DownloadUtility.PrepareUnitAnimation(characterData2, sequence.SkillAnimation.Name + "_chg", false, false);
              if (!string.IsNullOrEmpty(sequence.StartAnimation))
                DownloadUtility.PrepareUnitAnimation(characterData2, sequence.StartAnimation + "_chg", false, false);
            }
          }
        }
      }
    }
    for (int index = unit.BattleAbilitys.Count - 1; index >= 0; --index)
    {
      AbilityData battleAbility = unit.BattleAbilitys[index];
      if (battleAbility != null && battleAbility.Param != null)
        AssetManager.PrepareAssets(AssetPath.AbilityIcon(battleAbility.Param));
    }
    if (unit != null)
      DownloadUtility.PrepareUnitVoice(unit.UnitParam);
    if (dlStatusEffects && BadStatusEffects.Effects != null)
    {
      for (int index = 0; index < BadStatusEffects.Effects.Count; ++index)
      {
        if (!string.IsNullOrEmpty(BadStatusEffects.Effects[index].AnimationName))
          DownloadUtility.PrepareUnitAnimation(characterData1, BadStatusEffects.Effects[index].AnimationName, false, false);
      }
    }
    JobData[] jobs = unit.UnitData.Jobs;
    int artifactSlotIndex = JobData.GetArtifactSlotIndex(ArtifactTypes.Arms);
    if (jobs != null)
    {
      List<ArtifactParam> artifacts = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.Artifacts;
      foreach (JobData jobData in jobs)
      {
        JobData jd = jobData;
        if (jd != null)
        {
          string iname = (string) null;
          if (jd.ArtifactDatas[artifactSlotIndex] != null && jd.ArtifactDatas[artifactSlotIndex].ArtifactParam.type == ArtifactTypes.Arms)
            iname = jd.ArtifactDatas[artifactSlotIndex].ArtifactParam.iname;
          if (!string.IsNullOrEmpty(iname))
          {
            ArtifactParam artifalct = artifacts.Find((Predicate<ArtifactParam>) (item => item.iname == iname));
            if (artifalct != null && artifalct.type == ArtifactTypes.Arms)
              DownloadUtility.DownloadArtifact(artifalct);
          }
          ArtifactParam artifalct1 = artifacts.Find((Predicate<ArtifactParam>) (item => item.iname == jd.Param.artifact));
          if (artifalct1 != null && artifalct1.type == ArtifactTypes.Arms)
            DownloadUtility.DownloadArtifact(artifalct1);
        }
      }
    }
    else
    {
      if (unit.Job == null)
        return;
      DownloadUtility.DownloadArtifact(MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetArtifactParam(unit.Job.Param.artifact));
    }
  }

  private static void CreateAbilityChangeSkillList(ref List<SkillParam> sp_lists, SkillParam skill_param)
  {
    if (sp_lists == null || skill_param == null || string.IsNullOrEmpty(skill_param.AcToAbilId))
      return;
    GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
    AbilityParam abilityParam = instanceDirect.MasterParam.GetAbilityParam(skill_param.AcToAbilId);
    if (abilityParam == null || abilityParam.skills == null)
      return;
    for (int index = 0; index < abilityParam.skills.Length; ++index)
    {
      if (abilityParam.skills[index] != null)
      {
        SkillParam skillParam = instanceDirect.MasterParam.GetSkillParam(abilityParam.skills[index].iname);
        if (skillParam != null && !sp_lists.Contains(skillParam))
        {
          sp_lists.Add(skillParam);
          DownloadUtility.CreateAbilityChangeSkillList(ref sp_lists, skillParam);
        }
      }
    }
  }

  public static void PrepareUnitAssets(CharacterDB.Job jobData)
  {
    DownloadUtility.PrepareUnitModels(jobData);
    DownloadUtility.PrepareUnitAnimation(jobData, TacticsUnitController.ANIM_IDLE_FIELD, true, false);
    DownloadUtility.PrepareUnitAnimation(jobData, TacticsUnitController.ANIM_IDLE_DEMO, true, false);
    if (jobData.Movable)
    {
      DownloadUtility.PrepareUnitAnimation(jobData, TacticsUnitController.ANIM_RUN_FIELD, true, false);
      DownloadUtility.PrepareUnitAnimation(jobData, TacticsUnitController.ANIM_STEP, false, false);
      DownloadUtility.PrepareUnitAnimation(jobData, TacticsUnitController.ANIM_FALL_LOOP, false, false);
      DownloadUtility.PrepareUnitAnimation(jobData, TacticsUnitController.ANIM_FALL_END, false, false);
      DownloadUtility.PrepareUnitAnimation(jobData, TacticsUnitController.ANIM_CLIMBUP, false, false);
      DownloadUtility.PrepareUnitAnimation(jobData, TacticsUnitController.ANIM_GENKIDAMA, false, false);
    }
    DownloadUtility.PrepareUnitAnimation(jobData, TacticsUnitController.ANIM_PICKUP, false, false);
    DownloadUtility.PrepareUnitAnimation(jobData, "cmn_downstand0", false, false);
    DownloadUtility.PrepareUnitAnimation(jobData, "cmn_dodge0", false, false);
    DownloadUtility.PrepareUnitAnimation(jobData, "cmn_damage0", false, false);
    DownloadUtility.PrepareUnitAnimation(jobData, "cmn_damageair0", false, false);
    DownloadUtility.PrepareUnitAnimation(jobData, "cmn_down0", false, false);
    DownloadUtility.PrepareUnitAnimation(jobData, "itemuse0", false, false);
    DownloadUtility.PrepareUnitAnimation(jobData, "cmn_toss_lift0", false, false);
    DownloadUtility.PrepareUnitAnimation(jobData, "cmn_toss_throw0", false, false);
    DownloadUtility.PrepareUnitAnimation(jobData, "idleBattleScene0", true, false);
    DownloadUtility.PrepareUnitAnimation(jobData, "damageBattleScene0", true, false);
  }

  public static void PrepareJobAssets(JobParam job)
  {
    if (job == null)
      return;
    AssetManager.PrepareAssets(AssetPath.JobIconMedium(job));
    AssetManager.PrepareAssets(AssetPath.JobIconSmall(job));
    if (string.IsNullOrEmpty(job.wepmdl))
      return;
    AssetManager.PrepareAssets(AssetPath.JobEquipment(job));
  }

  public static NPCSetting CreateBreakObjNPC(BreakObjParam bo_param, int gx = 0, int gy = 0)
  {
    if (bo_param == null)
      return (NPCSetting) null;
    UnitParam unitParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetUnitParam(bo_param.UnitId);
    if (unitParam == null)
      return (NPCSetting) null;
    NPCSetting npcSetting = new NPCSetting();
    npcSetting.pos.x = (OInt) gx;
    npcSetting.pos.y = (OInt) gy;
    npcSetting.iname = (OString) bo_param.UnitId;
    npcSetting.lv = (OInt) 1;
    npcSetting.break_obj = new MapBreakObj();
    npcSetting.break_obj.clash_type = (int) bo_param.ClashType;
    npcSetting.break_obj.ai_type = (int) bo_param.AiType;
    npcSetting.break_obj.side_type = (int) bo_param.SideType;
    npcSetting.break_obj.ray_type = (int) bo_param.RayType;
    npcSetting.break_obj.is_ui = !bo_param.IsUI ? 0 : 1;
    npcSetting.break_obj.max_hp = (int) unitParam.ini_status.param.hp;
    if (bo_param.RestHps != null)
    {
      npcSetting.break_obj.rest_hps = new int[bo_param.RestHps.Length];
      for (int index = 0; index < bo_param.RestHps.Length; ++index)
        npcSetting.break_obj.rest_hps[index] = bo_param.RestHps[index];
    }
    else
    {
      int length = 2;
      if (unitParam.search > (byte) 1)
        length = (int) unitParam.search - 1;
      npcSetting.break_obj.rest_hps = new int[length];
      int maxHp = npcSetting.break_obj.max_hp;
      int num;
      npcSetting.break_obj.rest_hps[0] = num = maxHp - 1;
      for (int index = 1; index < length; ++index)
        npcSetting.break_obj.rest_hps[index] = num * (length - index) / length;
    }
    return npcSetting;
  }

  private static void PrepareSkillAssets(CharacterDB.Job jobData, SkillParam skill)
  {
    if (skill == null)
      return;
    if (!string.IsNullOrEmpty(skill.CutInConceptCardId))
    {
      ConceptCardParam conceptCardParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetConceptCardParam(skill.CutInConceptCardId);
      if (conceptCardParam != null)
        AssetManager.PrepareAssets(AssetPath.ConceptCard(conceptCardParam));
    }
    if (!string.IsNullOrEmpty(skill.TrickId))
    {
      TrickParam trickParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetTrickParam(skill.TrickId);
      if (trickParam != null)
      {
        AssetManager.PrepareAssets(AssetPath.TrickIconUI(trickParam.MarkerName));
        if (!string.IsNullOrEmpty(trickParam.EffectName))
          AssetManager.PrepareAssets(AssetPath.TrickEffect(trickParam.EffectName));
      }
    }
    if (!string.IsNullOrEmpty(skill.WeatherId))
      DownloadUtility.WeatherPrepareAsset(skill.WeatherId);
    if (!string.IsNullOrEmpty(skill.BreakObjId))
    {
      NPCSetting breakObjNpc = DownloadUtility.CreateBreakObjNPC(MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetBreakObjParam(skill.BreakObjId), 0, 0);
      if (breakObjNpc != null)
        DownloadUtility.DownloadUnit(breakObjNpc);
    }
    if (string.IsNullOrEmpty(skill.motion) && string.IsNullOrEmpty(skill.effect))
      return;
    SkillSequence sequence1 = SkillSequence.FindSequence(skill.motion, false);
    if (sequence1 == null)
      return;
    bool is_collabo_skill = !string.IsNullOrEmpty(skill.CollaboMainId);
    if (!string.IsNullOrEmpty(sequence1.SkillAnimation.Name))
    {
      DownloadUtility.PrepareUnitAnimation(jobData, sequence1.SkillAnimation.Name, false, is_collabo_skill);
      if (is_collabo_skill)
        DownloadUtility.PrepareUnitAnimation(jobData, sequence1.SkillAnimation.Name + "_sub", false, is_collabo_skill);
    }
    if (!string.IsNullOrEmpty(sequence1.ChantAnimation.Name))
    {
      DownloadUtility.PrepareUnitAnimation(jobData, sequence1.ChantAnimation.Name, false, is_collabo_skill);
      if (is_collabo_skill)
        DownloadUtility.PrepareUnitAnimation(jobData, sequence1.ChantAnimation.Name + "_sub", false, is_collabo_skill);
    }
    if (!string.IsNullOrEmpty(sequence1.EndAnimation.Name))
    {
      DownloadUtility.PrepareUnitAnimation(jobData, sequence1.EndAnimation.Name, false, is_collabo_skill);
      if (is_collabo_skill)
        DownloadUtility.PrepareUnitAnimation(jobData, sequence1.EndAnimation.Name + "_sub", false, is_collabo_skill);
    }
    if (!string.IsNullOrEmpty(sequence1.StartAnimation))
    {
      DownloadUtility.PrepareUnitAnimation(jobData, sequence1.StartAnimation, false, is_collabo_skill);
      if (is_collabo_skill)
        DownloadUtility.PrepareUnitAnimation(jobData, sequence1.StartAnimation + "_sub", false, is_collabo_skill);
    }
    if (!string.IsNullOrEmpty(skill.effect))
    {
      AssetManager.PrepareAssets(AssetPath.SkillEffect(skill));
      if (!string.IsNullOrEmpty(skill.CollaboMainId))
        AssetManager.PrepareAssets(AssetPath.SkillEffect(skill) + "_sub");
    }
    if (!string.IsNullOrEmpty(skill.SkillMotionId))
    {
      SkillMotionParam skillMotionParam = MonoSingleton<GameManager>.Instance.MasterParam.GetSkillMotionParam(skill.SkillMotionId);
      if (skillMotionParam != null)
      {
        List<string> motionList = skillMotionParam.GetMotionList();
        for (int index = 0; index < motionList.Count; ++index)
        {
          SkillSequence sequence2 = SkillSequence.FindSequence(motionList[index], false);
          if (sequence2 != null)
          {
            if (!string.IsNullOrEmpty(sequence2.SkillAnimation.Name))
            {
              DownloadUtility.PrepareUnitAnimation(jobData, sequence2.SkillAnimation.Name, false, is_collabo_skill);
              if (is_collabo_skill)
                DownloadUtility.PrepareUnitAnimation(jobData, sequence2.SkillAnimation.Name + "_sub", false, is_collabo_skill);
            }
            if (!string.IsNullOrEmpty(sequence2.ChantAnimation.Name))
            {
              DownloadUtility.PrepareUnitAnimation(jobData, sequence2.ChantAnimation.Name, false, is_collabo_skill);
              if (is_collabo_skill)
                DownloadUtility.PrepareUnitAnimation(jobData, sequence2.ChantAnimation.Name + "_sub", false, is_collabo_skill);
            }
            if (!string.IsNullOrEmpty(sequence2.EndAnimation.Name))
            {
              DownloadUtility.PrepareUnitAnimation(jobData, sequence2.EndAnimation.Name, false, is_collabo_skill);
              if (is_collabo_skill)
                DownloadUtility.PrepareUnitAnimation(jobData, sequence2.EndAnimation.Name + "_sub", false, is_collabo_skill);
            }
            if (!string.IsNullOrEmpty(sequence2.StartAnimation))
            {
              DownloadUtility.PrepareUnitAnimation(jobData, sequence2.StartAnimation, false, is_collabo_skill);
              if (is_collabo_skill)
                DownloadUtility.PrepareUnitAnimation(jobData, sequence2.StartAnimation + "_sub", false, is_collabo_skill);
            }
          }
        }
      }
    }
    if (!string.IsNullOrEmpty(skill.SceneName))
      AssetManager.PrepareAssets(AssetPath.SkillScene(skill.SceneName));
    if (string.IsNullOrEmpty(skill.SceneNameBigUnit))
      return;
    AssetManager.PrepareAssets(AssetPath.SkillScene(skill.SceneNameBigUnit));
  }

  public static void PrepareInventoryAssets(ItemData[] items = null)
  {
    GameManager instance = MonoSingleton<GameManager>.Instance;
    PlayerData player = instance.Player;
    if (items == null)
      items = player.Inventory;
    for (int index = 0; index < items.Length; ++index)
    {
      ItemData itemData = items[index];
      if (itemData != null && itemData.Param != null && !string.IsNullOrEmpty(itemData.Param.skill))
      {
        SkillParam skillParam = instance.GetSkillParam(itemData.Param.skill);
        if (skillParam != null && !string.IsNullOrEmpty(skillParam.effect))
          AssetManager.PrepareAssets(AssetPath.SkillEffect(skillParam));
        if (!string.IsNullOrEmpty(skillParam.TrickId))
        {
          TrickParam trickParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetTrickParam(skillParam.TrickId);
          if (trickParam != null)
          {
            AssetManager.PrepareAssets(AssetPath.TrickIconUI(trickParam.MarkerName));
            if (!string.IsNullOrEmpty(trickParam.EffectName))
              AssetManager.PrepareAssets(AssetPath.TrickEffect(trickParam.EffectName));
          }
        }
        if (!string.IsNullOrEmpty(skillParam.WeatherId))
          DownloadUtility.WeatherPrepareAsset(skillParam.WeatherId);
      }
    }
  }

  public static void PrepareHomeBGAssets(SectionParam section)
  {
    if (section == null)
      return;
    if (string.IsNullOrEmpty(section.home))
    {
      DebugUtility.LogError(string.Format("ストーリークエストの章には「ホーム画面」を設定してください。 シート【QuestParam/world】 ⇒ iname【{0}】 ⇒ カラム【home】", (object) section.iname));
    }
    else
    {
      AssetManager.PrepareAssets(section.home);
      string str = section.bgm;
      if (string.IsNullOrEmpty(str))
      {
        DebugUtility.LogWarning(string.Format("ストーリークエストの章に「bgm」が設定されていません。 シート【QuestParam/world】 ⇒ iname【{0}】 ⇒ カラム【bgm】", (object) section.iname));
        str = "BGM_0027";
      }
      AssetManager.PrepareAssets("StreamingAssets/" + str + ".acb");
      AssetManager.PrepareAssets("StreamingAssets/" + str + ".awb");
    }
  }

  public static void PrepareRaidBossAsset(RaidBossParam raidBoss)
  {
    if (raidBoss == null)
      return;
    UnitParam unitParam = MonoSingleton<GameManager>.Instance.GetUnitParam(raidBoss.UnitIName);
    if (unitParam == null)
    {
      DebugUtility.LogError("レイドボスに指定されたユニットが見つかりませんでした。 RaidBossParam.UnitIName => <color=#ffff00>" + raidBoss.UnitIName + "</color>");
    }
    else
    {
      AssetManager.PrepareAssets(AssetPath.UnitIconMedium(unitParam, (string) null));
      AssetManager.PrepareAssets(AssetPath.UnitImage(unitParam, (string) null));
    }
  }

  public static void PrepareQuestAllAssets(QuestParam questParam, bool dsStatusEffects)
  {
    DownloadUtility.DownloadQuestBase(questParam);
    DownloadUtility.DownloadQuestMaps(questParam, dsStatusEffects);
  }

  private static void PrepareUnmanagedEventAsset(string demoID)
  {
    AssetManager.Load<EventScript>("Events/" + demoID).PrepareUnmanagedAssets();
  }

  public static void PrepareAssetByAssetBundleFlags(AssetBundleFlags flags)
  {
    foreach (AssetList.Item asset in AssetManager.AssetList.Assets)
    {
      if ((asset.Flags & flags) != (AssetBundleFlags) 0 && asset.Size > 0)
        AssetManager.PrepareAssets(asset);
    }
  }
}
