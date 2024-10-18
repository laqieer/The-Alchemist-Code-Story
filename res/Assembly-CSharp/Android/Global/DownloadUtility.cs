// Decompiled with JetBrains decompiler
// Type: DownloadUtility
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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

  public static void DownloadQuestMaps(QuestParam quest)
  {
    for (int index = 0; index < quest.map.Count; ++index)
    {
      if (!string.IsNullOrEmpty(quest.map[index].mapSetName))
      {
        string src = AssetManager.LoadTextData(AssetPath.LocalMap(quest.map[index].mapSetName));
        if (src != null)
        {
          JSON_MapUnit jsonObject = JSONParser.parseJSONObject<JSON_MapUnit>(src);
          DownloadUtility.DownloadQuestEnemy(jsonObject);
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
    using (List<string>.Enumerator enumerator = weatherSetParam.StartWeatherIdLists.GetEnumerator())
    {
      while (enumerator.MoveNext())
        DownloadUtility.WeatherPrepareAsset(enumerator.Current);
    }
    using (List<string>.Enumerator enumerator = weatherSetParam.ChangeWeatherIdLists.GetEnumerator())
    {
      while (enumerator.MoveNext())
        DownloadUtility.WeatherPrepareAsset(enumerator.Current);
    }
  }

  private static void WeatherPrepareAsset(string weather_id)
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
    if (mapset.enemy == null || mapset.is_rand != (byte) 0)
      return;
    for (int index = 0; index < mapset.enemy.Length; ++index)
      DownloadUtility.DownloadUnit(new NPCSetting(mapset.enemy[index]));
  }

  public static void DownloadTowerQuestEnemy(JSON_MapUnit mapset)
  {
    if (mapset.enemy == null || mapset.is_rand > (byte) 0)
      return;
    for (int index = 0; index < mapset.enemy.Length; ++index)
      DownloadUtility.LoadUnitIconMedium(mapset.enemy[index].iname);
  }

  public static void RandDownloadQuestEnemy(JSON_MapUnit mapset)
  {
    if (mapset.is_rand == (byte) 0 || mapset.deck == null)
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
        JobSetParam jobSetParam = MonoSingleton<GameManager>.Instance.GetJobSetParam((string) unitParam.jobsets[index]);
        AssetManager.PrepareAssets(AssetPath.UnitIconMedium(unitParam, jobSetParam.job));
      }
    }
    else
      AssetManager.PrepareAssets(AssetPath.UnitIconMedium(unitParam, string.Empty));
  }

  public static void DownloadQuests(List<TowerFloorParam> floorParams)
  {
    TowerFloorParam currentFloor = MonoSingleton<GameManager>.Instance.TowerResuponse.GetCurrentFloor();
    for (int index1 = 0; index1 < floorParams.Count; ++index1)
    {
      QuestParam questParam = floorParams[index1].GetQuestParam();
      for (int index2 = 0; index2 < questParam.map.Count; ++index2)
      {
        if (!string.IsNullOrEmpty(questParam.map[index2].mapSetName))
        {
          string src = AssetManager.LoadTextData(AssetPath.LocalMap(questParam.map[index2].mapSetName));
          if (src != null)
          {
            JSON_MapUnit jsonObject = JSONParser.parseJSONObject<JSON_MapUnit>(src);
            if (jsonObject.is_rand > (byte) 0)
            {
              if (currentFloor.iname == questParam.iname)
                DownloadUtility.RandDownloadQuestEnemy(jsonObject);
            }
            else
              DownloadUtility.DownloadTowerQuestEnemy(jsonObject);
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
        JobSetParam jobSetParam = instance.GetJobSetParam((string) unit.jobsets[index]);
        while (jobSetParam != null)
        {
          JobParam jobParam = instance.GetJobParam(jobSetParam.job);
          SkillParam skillParam = string.IsNullOrEmpty(jobParam.atkskill[0]) ? instance.MasterParam.GetSkillParam(jobParam.atkskill[(int) unit.element]) : instance.MasterParam.GetSkillParam(jobParam.atkskill[0]);
          if (skillParam != null)
          {
            SkillSequence sequence = SkillSequence.FindSequence(skillParam.motion);
            if (sequence != null && !string.IsNullOrEmpty(sequence.SkillAnimation.Name) && index < character.Jobs.Count)
              DownloadUtility.PrepareUnitAnimation(character.Jobs[index], sequence.SkillAnimation.Name, false, false, (JobParam) null);
          }
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
                SkillSequence sequence = SkillSequence.FindSequence(jd.GetAttackSkill().SkillParam.motion);
                if (sequence != null)
                {
                  CharacterDB.Job jobData = character.Jobs.Find((Predicate<CharacterDB.Job>) (x => x.JobID == jd.JobResourceID));
                  if (jobData != null)
                    DownloadUtility.PrepareUnitAnimation(jobData, sequence.SkillAnimation.Name, false, false, (JobParam) null);
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
              SkillSequence sequence = SkillSequence.FindSequence(skillData.SkillParam.motion);
              if (sequence != null)
              {
                CharacterDB.Job characterData = DownloadUtility.GetCharacterData(unit, jobParam, (ArtifactParam) null);
                if (characterData != null)
                  DownloadUtility.PrepareUnitAnimation(characterData, sequence.SkillAnimation.Name, false, false, (JobParam) null);
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
            DownloadUtility.PrepareUnitAnimation(jobData, "unit_info_idle0", true, false, (JobParam) null);
            DownloadUtility.PrepareUnitAnimation(jobData, "unit_info_act0", true, false, (JobParam) null);
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
            CharacterDB.Job characterData = DownloadUtility.GetCharacterData(unit, jobParam, (ArtifactParam) null);
            if (characterData != null && characterData.JobID != jobParam.model)
            {
              CharacterDB.Job jobData = new CharacterDB.Job();
              jobData.JobID = jobParam.model;
              jobData.AssetPrefix = characterData.AssetPrefix;
              DownloadUtility.PrepareUnitAnimation(jobData, "unit_info_idle0", true, false, (JobParam) null);
              DownloadUtility.PrepareUnitAnimation(jobData, "unit_info_act0", true, false, (JobParam) null);
              DebugUtility.LogError("Job is Different:" + characterData.JobID + " / " + jobParam.model);
            }
            SkillParam skillParam = string.IsNullOrEmpty(jobParam.atkskill[0]) ? instance.MasterParam.GetSkillParam(jobParam.atkskill[(int) unit.element]) : instance.MasterParam.GetSkillParam(jobParam.atkskill[0]);
            if (skillParam != null)
            {
              SkillSequence sequence = SkillSequence.FindSequence(skillParam.motion);
              if (sequence != null && !string.IsNullOrEmpty(sequence.SkillAnimation.Name))
                DownloadUtility.PrepareUnitAnimation(character.Jobs[index], sequence.SkillAnimation.Name, false, false, (JobParam) null);
            }
          }
        }
      }
    }
    for (int index = 0; index < character.Jobs.Count; ++index)
    {
      CharacterDB.Job job = character.Jobs[index];
      DownloadUtility.PrepareUnitModels(job);
      DownloadUtility.PrepareUnitAnimation(job, "unit_info_idle0", true, false, (JobParam) null);
      DownloadUtility.PrepareUnitAnimation(job, "unit_info_act0", true, false, (JobParam) null);
    }
    AssetManager.PrepareAssets("CHM/Home_" + unit.model + "_walk0");
    if (unit.skins != null)
    {
      List<ArtifactParam> artifacts = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.Artifacts;
      for (int i = 0; i < unit.skins.Length; ++i)
      {
        ArtifactParam skin = artifacts.Find((Predicate<ArtifactParam>) (p => p.iname == (string) unit.skins[i]));
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
    if (unitParam.skins != null)
    {
      List<ArtifactParam> artifacts = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.Artifacts;
      for (int i = 0; i < unitParam.skins.Length; ++i)
      {
        ArtifactParam artifact = artifacts.Find((Predicate<ArtifactParam>) (p => p.iname == (string) unitParam.skins[i]));
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
    if (unitParam.job_voices == null)
      return;
    for (int index1 = 0; index1 < unitParam.job_voices.Length; ++index1)
    {
      string charName = AssetPath.UnitVoiceFileName(unitParam, (ArtifactParam) null, (string) unitParam.job_voices[index1]);
      if (!string.IsNullOrEmpty(charName))
      {
        string[] strArray2 = MySound.VoiceCueSheetFileName(charName);
        if (strArray2 != null)
        {
          for (int index2 = 0; index2 < strArray2.Length; ++index2)
            AssetManager.PrepareAssets("StreamingAssets/" + strArray2[index2]);
        }
      }
    }
  }

  public static void DownloadUnit(NPCSetting npc)
  {
    Unit unit = new Unit();
    unit.Setup((UnitData) null, (UnitSetting) npc, (Unit.DropItem) null, (Unit.DropItem) null);
    DownloadUtility.DownloadUnit(unit, false);
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

  private static void PrepareUnitAnimation(CharacterDB.Job jobData, string animationName, bool addJobName, bool is_collabo_skill = false, JobParam param = null)
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
      if (param != null)
      {
        stringBuilder.Append(param.model);
        stringBuilder.Append('_');
      }
      else
      {
        stringBuilder.Append(jobData.JobID);
        stringBuilder.Append('_');
      }
    }
    stringBuilder.Append(animationName);
    AssetManager.PrepareAssets(stringBuilder.ToString());
  }

  private static CharacterDB.Job GetCharacterData(UnitParam unit, JobParam job, ArtifactParam skin)
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
    using (List<SkillData>.Enumerator enumerator = skill_unit.BattleSkills.GetEnumerator())
    {
      while (enumerator.MoveNext())
      {
        SkillData current = enumerator.Current;
        if (current.IsTransformSkill() && !string.IsNullOrEmpty(current.SkillParam.motion))
        {
          SkillSequence sequence = SkillSequence.FindSequence(current.SkillParam.motion);
          if (sequence != null && !string.IsNullOrEmpty(sequence.SkillAnimation.Name))
            DownloadUtility.PrepareUnitAnimation(characterData, sequence.SkillAnimation.Name + "_chg", false, false, (JobParam) null);
        }
      }
    }
  }

  public static void DownloadUnit(Unit unit, bool dlStatusEffects = false)
  {
    UnitParam unitParam = unit.UnitParam;
    JobParam job = unit.Job == null ? (JobParam) null : unit.Job.Param;
    ArtifactParam selectedSkin = unit.UnitData.GetSelectedSkin(-1);
    CharacterDB.Job characterData = DownloadUtility.GetCharacterData(unitParam, job, selectedSkin);
    if (characterData == null)
      return;
    DownloadUtility.PrepareUnitAssets(characterData, job);
    if (job != null && characterData.JobID != job.model)
    {
      DownloadUtility.PrepareUnitAssets(new CharacterDB.Job()
      {
        AssetPrefix = characterData.AssetPrefix,
        JobID = job.model,
        Movable = characterData.Movable
      }, (JobParam) null);
      DebugUtility.LogError("Job is Different:" + characterData.JobID + " / " + job.model);
    }
    if (unit.IsBreakObj)
    {
      CharacterDB.Character character = CharacterDB.FindCharacter(unitParam.model);
      if (character != null)
      {
        for (int index = 1; index < character.Jobs.Count; ++index)
          DownloadUtility.PrepareUnitAssets(character.Jobs[index], (JobParam) null);
      }
    }
    if (unit.Job != null)
      DownloadUtility.PrepareJobAssets(unit.Job.Param);
    string jobName = unit.UnitData.CurrentJob == null ? string.Empty : unit.UnitData.CurrentJob.JobID;
    AssetManager.PrepareAssets(AssetPath.UnitSkinIconSmall(unitParam, selectedSkin, jobName));
    AssetManager.PrepareAssets(AssetPath.UnitSkinIconMedium(unitParam, selectedSkin, jobName));
    AssetManager.PrepareAssets(AssetPath.UnitSkinEyeImage(unitParam, selectedSkin, jobName));
    SkillData attackSkill = unit.GetAttackSkill();
    if (attackSkill != null)
      DownloadUtility.PrepareSkillAssets(characterData, attackSkill.SkillParam);
    for (int index = unit.BattleSkills.Count - 1; index >= 0; --index)
      DownloadUtility.PrepareSkillAssets(characterData, unit.BattleSkills[index].SkillParam);
    for (int index = unit.BattleAbilitys.Count - 1; index >= 0; --index)
    {
      AbilityData battleAbility = unit.BattleAbilitys[index];
      if (battleAbility != null && battleAbility.Param != null)
        AssetManager.PrepareAssets(AssetPath.AbilityIcon(battleAbility.Param));
    }
    if (unit != null)
      DownloadUtility.PrepareUnitVoice(unit.UnitParam);
    if (dlStatusEffects)
    {
      for (int index = 0; index < BadStatusEffects.Effects.Count; ++index)
      {
        if (!string.IsNullOrEmpty(BadStatusEffects.Effects[index].AnimationName))
          DownloadUtility.PrepareUnitAnimation(characterData, BadStatusEffects.Effects[index].AnimationName, false, false, (JobParam) null);
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

  public static void PrepareUnitAssets(CharacterDB.Job jobData, JobParam param = null)
  {
    DownloadUtility.PrepareUnitModels(jobData);
    DownloadUtility.PrepareUnitAnimation(jobData, TacticsUnitController.ANIM_IDLE_FIELD, true, false, param);
    DownloadUtility.PrepareUnitAnimation(jobData, TacticsUnitController.ANIM_IDLE_DEMO, true, false, (JobParam) null);
    if (jobData.Movable)
    {
      DownloadUtility.PrepareUnitAnimation(jobData, TacticsUnitController.ANIM_RUN_FIELD, true, false, param);
      DownloadUtility.PrepareUnitAnimation(jobData, TacticsUnitController.ANIM_STEP, false, false, (JobParam) null);
      DownloadUtility.PrepareUnitAnimation(jobData, TacticsUnitController.ANIM_FALL_LOOP, false, false, (JobParam) null);
      DownloadUtility.PrepareUnitAnimation(jobData, TacticsUnitController.ANIM_FALL_END, false, false, (JobParam) null);
      DownloadUtility.PrepareUnitAnimation(jobData, TacticsUnitController.ANIM_CLIMBUP, false, false, (JobParam) null);
      DownloadUtility.PrepareUnitAnimation(jobData, TacticsUnitController.ANIM_GENKIDAMA, false, false, (JobParam) null);
    }
    DownloadUtility.PrepareUnitAnimation(jobData, TacticsUnitController.ANIM_PICKUP, false, false, (JobParam) null);
    DownloadUtility.PrepareUnitAnimation(jobData, "cmn_downstand0", false, false, (JobParam) null);
    DownloadUtility.PrepareUnitAnimation(jobData, "cmn_dodge0", false, false, (JobParam) null);
    DownloadUtility.PrepareUnitAnimation(jobData, "cmn_damage0", false, false, (JobParam) null);
    DownloadUtility.PrepareUnitAnimation(jobData, "cmn_damageair0", false, false, (JobParam) null);
    DownloadUtility.PrepareUnitAnimation(jobData, "cmn_down0", false, false, (JobParam) null);
    DownloadUtility.PrepareUnitAnimation(jobData, "itemuse0", false, false, (JobParam) null);
    DownloadUtility.PrepareUnitAnimation(jobData, "cmn_toss_lift0", false, false, (JobParam) null);
    DownloadUtility.PrepareUnitAnimation(jobData, "cmn_toss_throw0", false, false, (JobParam) null);
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
      if ((int) unitParam.search > 1)
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
    SkillSequence sequence = SkillSequence.FindSequence(skill.motion);
    if (sequence == null)
      return;
    bool is_collabo_skill = !string.IsNullOrEmpty(skill.CollaboMainId);
    if (!string.IsNullOrEmpty(sequence.SkillAnimation.Name))
    {
      DownloadUtility.PrepareUnitAnimation(jobData, sequence.SkillAnimation.Name, false, is_collabo_skill, (JobParam) null);
      if (is_collabo_skill)
        DownloadUtility.PrepareUnitAnimation(jobData, sequence.SkillAnimation.Name + "_sub", false, is_collabo_skill, (JobParam) null);
    }
    if (!string.IsNullOrEmpty(sequence.ChantAnimation.Name))
    {
      DownloadUtility.PrepareUnitAnimation(jobData, sequence.ChantAnimation.Name, false, is_collabo_skill, (JobParam) null);
      if (is_collabo_skill)
        DownloadUtility.PrepareUnitAnimation(jobData, sequence.ChantAnimation.Name + "_sub", false, is_collabo_skill, (JobParam) null);
    }
    if (!string.IsNullOrEmpty(sequence.EndAnimation.Name))
    {
      DownloadUtility.PrepareUnitAnimation(jobData, sequence.EndAnimation.Name, false, is_collabo_skill, (JobParam) null);
      if (is_collabo_skill)
        DownloadUtility.PrepareUnitAnimation(jobData, sequence.EndAnimation.Name + "_sub", false, is_collabo_skill, (JobParam) null);
    }
    if (!string.IsNullOrEmpty(sequence.StartAnimation))
    {
      DownloadUtility.PrepareUnitAnimation(jobData, sequence.StartAnimation, false, is_collabo_skill, (JobParam) null);
      if (is_collabo_skill)
        DownloadUtility.PrepareUnitAnimation(jobData, sequence.StartAnimation + "_sub", false, is_collabo_skill, (JobParam) null);
    }
    if (!string.IsNullOrEmpty(skill.effect))
    {
      AssetManager.PrepareAssets(AssetPath.SkillEffect(skill));
      if (!string.IsNullOrEmpty(skill.CollaboMainId))
        AssetManager.PrepareAssets(AssetPath.SkillEffect(skill) + "_sub");
    }
    if (string.IsNullOrEmpty(skill.SceneName))
      return;
    AssetManager.PrepareAssets(AssetPath.SkillScene(skill.SceneName));
  }

  public static void PrepareInventoryAssets()
  {
    GameManager instance = MonoSingleton<GameManager>.Instance;
    PlayerData player = instance.Player;
    for (int index = 0; index < player.Inventory.Length; ++index)
    {
      ItemData itemData = player.Inventory[index];
      if (itemData != null && itemData.Param != null && !string.IsNullOrEmpty((string) itemData.Param.skill))
      {
        SkillParam skillParam = instance.GetSkillParam((string) itemData.Param.skill);
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
}
