// Decompiled with JetBrains decompiler
// Type: DependencyUtility
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using SRPG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

#nullable disable
public class DependencyUtility
{
  private const string MASTER_PARAM_PATH = "Data/MasterParam";
  private const string QUEST_PARAM_PATH = "Data/QuestParam";
  private HashSet<string> m_Dependency = new HashSet<string>();
  private AssetList m_AssetList;
  private BadStatusEffects.BadStatusEffectParam m_BadStatusEffectParam;
  private JSON_InitUnit[] m_InitUnits;
  private static DependencyUtility instance;

  public static DependencyUtility Instance
  {
    get
    {
      if (DependencyUtility.instance == null)
        DependencyUtility.instance = new DependencyUtility();
      return DependencyUtility.instance;
    }
  }

  private BadStatusEffects.BadStatusEffectParam badStatusEffectParam
  {
    get
    {
      if (this.m_BadStatusEffectParam == null)
        this.m_BadStatusEffectParam = BadStatusEffects.LoadBadStatusEffectParam();
      return this.m_BadStatusEffectParam;
    }
  }

  private bool IsReadyToUseMasterParam
  {
    get => this.m_InitUnits != null && MonoSingleton<GameManager>.Instance.MasterParam.Loaded;
  }

  public void Initialize(AssetList assetList) => this.m_AssetList = assetList;

  private static void InitializeMaster()
  {
    JSON_MasterParam json1 = (JSON_MasterParam) null;
    Json_QuestList json2 = (Json_QuestList) null;
    if (!DependencyUtility.Instance.IsReadyToUseMasterParam)
    {
      string str1 = DependencyUtility.LoadText("Data/MasterParam");
      if (!string.IsNullOrEmpty(str1))
        json1 = JsonUtility.FromJson<JSON_MasterParam>(str1);
      string str2 = DependencyUtility.LoadText("Data/QuestParam");
      if (!string.IsNullOrEmpty(str2))
        json2 = JsonUtility.FromJson<Json_QuestList>(str2);
    }
    if (!MonoSingleton<GameManager>.Instance.MasterParam.Loaded)
    {
      MonoSingleton<GameManager>.Instance.Deserialize2(json1);
      MonoSingleton<GameManager>.Instance.Deserialize(json2);
    }
    if (DependencyUtility.Instance.m_InitUnits != null)
      return;
    DependencyUtility.Instance.m_InitUnits = json1.InitUnit;
  }

  public void ClearDependencies() => this.m_Dependency.Clear();

  public List<string> GetDependencies_Tutorial()
  {
    if (!this.IsReadyToUseMasterParam)
      DependencyUtility.InitializeMaster();
    string[] tutorialSteps = GameSettings.Instance.Tutorial_Steps;
    UnitData[] unitDataArray = new UnitData[DependencyUtility.Instance.m_InitUnits.Length];
    for (int index = 0; index < unitDataArray.Length; ++index)
    {
      unitDataArray[index] = new UnitData();
      unitDataArray[index].Setup(DependencyUtility.Instance.m_InitUnits[index].iname, DependencyUtility.Instance.m_InitUnits[index].exp, DependencyUtility.Instance.m_InitUnits[index].rare, 0);
      this.GetDependencies_UnitData(unitDataArray[index]);
    }
    for (int index = 0; index < tutorialSteps.Length; ++index)
    {
      if (!string.IsNullOrEmpty(tutorialSteps[index]))
      {
        QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(tutorialSteps[index]);
        if (quest != null)
          this.GetDependencies_Quest(quest);
        else
          this.AddExternalDependencies(tutorialSteps[index]);
      }
    }
    return this.m_Dependency.ToList<string>();
  }

  public List<string> GetDependencies_Quest(QuestParam questParam)
  {
    this.GetDependencies_QuestBase(questParam);
    this.GetDependencies_QuestMap(questParam);
    return this.m_Dependency.ToList<string>();
  }

  public void GetDependencies_QuestBase(QuestParam questParam)
  {
    if (questParam.map.Count > 0)
    {
      string mapSceneName = questParam.map[0].mapSceneName;
      string mapSetName = questParam.map[0].mapSetName;
      if (!string.IsNullOrEmpty(mapSceneName))
      {
        this.AddExternalDependencies(mapSceneName);
        this.AddExternalDependencies(AssetPath.LocalMap(mapSceneName));
      }
      if (!string.IsNullOrEmpty(mapSetName))
        this.AddExternalDependencies(AssetPath.LocalMap(mapSetName));
    }
    if (!string.IsNullOrEmpty(questParam.storyTextID))
      this.AddExternalDependencies(LocalizedText.GetResourcePath(questParam.storyTextID));
    if (!string.IsNullOrEmpty(questParam.navigation))
      this.AddExternalDependencies(AssetPath.Navigation(questParam));
    if (!string.IsNullOrEmpty(questParam.event_start))
      this.AddExternalDependencies(AssetPath.QuestEvent(questParam.event_start));
    if (questParam.map != null)
    {
      for (int index = 0; index < questParam.map.Count; ++index)
      {
        if (!string.IsNullOrEmpty(questParam.map[index].eventSceneName))
          this.AddExternalDependencies(AssetPath.QuestEvent(questParam.map[index].eventSceneName));
        if (!string.IsNullOrEmpty(questParam.map[index].bgmName))
        {
          this.AddExternalDependencies("StreamingAssets/" + questParam.map[index].bgmName + ".acb");
          this.AddExternalDependencies("StreamingAssets/" + questParam.map[index].bgmName + ".awb");
        }
      }
    }
    if (!string.IsNullOrEmpty(questParam.event_clear))
      this.AddExternalDependencies(AssetPath.QuestEvent(questParam.event_clear));
    this.AddExternalDependencies("StreamingAssets/BGM_0006.acb");
    this.AddExternalDependencies("StreamingAssets/BGM_0006.awb");
  }

  public void GetDependencies_QuestMap(QuestParam questParam)
  {
    for (int index1 = 0; index1 < questParam.map.Count; ++index1)
    {
      if (!string.IsNullOrEmpty(questParam.map[index1].mapSetName))
      {
        string src = DependencyUtility.LoadText(AssetPath.LocalMap(questParam.map[index1].mapSetName));
        if (!string.IsNullOrEmpty(src))
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
              NPCSetting setting = new NPCSetting(jsonMapEnemyUnitList[index2]);
              Unit unit = new Unit();
              unit.Setup(setting: (UnitSetting) setting);
              units.Add(unit);
            }
          }
          for (int index3 = 0; index3 < units.Count; ++index3)
          {
            this.GetDependencies_Unit(units[index3]);
            Unit unit = BattleCore.SearchTransformUnit(units, units[index3]);
            if (unit != null)
              this.GetDependencies_UnitTransformAnimation(unit, units[index3]);
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
                  this.AddExternalDependencies(AssetPath.TrickIconUI(trickParam.MarkerName));
                  if (!string.IsNullOrEmpty(trickParam.EffectName))
                    this.AddExternalDependencies(AssetPath.TrickEffect(trickParam.EffectName));
                }
              }
            }
          }
        }
      }
    }
    if (string.IsNullOrEmpty(questParam.WeatherSetId))
      return;
    WeatherSetParam weatherSetParam = MonoSingleton<GameManager>.GetInstanceDirect().GetWeatherSetParam(questParam.WeatherSetId);
    if (weatherSetParam == null)
      return;
    foreach (string startWeatherIdList in weatherSetParam.StartWeatherIdLists)
      this.GetDependencies_WeatherAsset(startWeatherIdList);
    foreach (string changeWeatherIdList in weatherSetParam.ChangeWeatherIdLists)
      this.GetDependencies_WeatherAsset(changeWeatherIdList);
  }

  public void GetDependencies_WeatherAsset(string weatherID)
  {
    if (string.IsNullOrEmpty(weatherID))
      return;
    WeatherParam weatherParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetWeatherParam(weatherID);
    if (weatherParam == null)
      return;
    if (!string.IsNullOrEmpty(weatherParam.Icon))
      this.AddExternalDependencies(AssetPath.WeatherIcon(weatherParam.Icon));
    if (string.IsNullOrEmpty(weatherParam.Effect))
      return;
    this.AddExternalDependencies(AssetPath.WeatherEffect(weatherParam.Effect));
  }

  public void GetDependencies_UnitData(UnitData unitData)
  {
    UnitSetting setting = new UnitSetting();
    setting.side = (OInt) 0;
    Unit unit = new Unit();
    unit.Setup(unitData, setting);
    this.GetDependencies_Unit(unit);
  }

  public void GetDependencies_NPCSetting(NPCSetting npc)
  {
    Unit unit = new Unit();
    unit.Setup(setting: (UnitSetting) npc);
    this.GetDependencies_Unit(unit);
  }

  public void GetDependencies_Unit(Unit unit, bool dlStatusEffects = false, bool dlUnitImage = false)
  {
    UnitParam unitParam = unit.UnitParam;
    JobParam job1 = unit.Job == null ? (JobParam) null : unit.Job.Param;
    ArtifactParam selectedSkin1 = unit.UnitData.GetSelectedSkin();
    CharacterDB.Job characterData1 = DependencyUtility.GetCharacterData(unitParam, job1, selectedSkin1);
    if (characterData1 == null)
      return;
    this.GetDependencies_UnitModel(characterData1);
    if (job1 != null && characterData1.JobID != job1.model)
    {
      this.GetDependencies_UnitModel(new CharacterDB.Job()
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
          this.GetDependencies_UnitModel(character.Jobs[index]);
      }
    }
    if (unit.Job != null)
      this.GetDependencies_JobParam(unit.Job.Param);
    string jobName = unit.UnitData.CurrentJob == null ? string.Empty : unit.UnitData.CurrentJob.JobID;
    this.AddExternalDependencies(AssetPath.UnitSkinIconSmall(unitParam, selectedSkin1, jobName));
    this.AddExternalDependencies(AssetPath.UnitSkinIconMedium(unitParam, selectedSkin1, jobName));
    this.AddExternalDependencies(AssetPath.UnitSkinEyeImage(unitParam, selectedSkin1, jobName));
    if (dlUnitImage)
      this.AddExternalDependencies(AssetPath.UnitSkinImage(unitParam, selectedSkin1, jobName));
    SkillData attackSkill = unit.GetAttackSkill();
    if (attackSkill != null)
      this.GetDependencies_SkillParam(characterData1, attackSkill.SkillParam);
    List<DynamicTransformUnitParam> transformUnitParamList = new List<DynamicTransformUnitParam>();
    for (int index1 = unit.BattleSkills.Count - 1; index1 >= 0; --index1)
    {
      SkillParam skillParam = unit.BattleSkills[index1].SkillParam;
      if (skillParam != null)
      {
        this.GetDependencies_SkillParam(characterData1, skillParam);
        if (!string.IsNullOrEmpty(skillParam.AcToAbilId))
        {
          List<SkillParam> sp_lists = new List<SkillParam>();
          DependencyUtility.CreateAbilityChangeSkillList(ref sp_lists, skillParam);
          for (int index2 = 0; index2 < sp_lists.Count; ++index2)
            this.GetDependencies_SkillParam(characterData1, sp_lists[index2]);
        }
        if (!string.IsNullOrEmpty(skillParam.DynamicTransformUnitId))
        {
          DynamicTransformUnitParam transformUnitParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetDynamicTransformUnitParam(skillParam.DynamicTransformUnitId);
          if (transformUnitParam != null)
          {
            this.AddExternalDependencies(transformUnitParam.CancelEffect);
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
      this.GetDependencies_Unit(unit1, dlStatusEffects);
      JobParam job2 = unit1.Job == null ? (JobParam) null : unit1.Job.Param;
      ArtifactParam selectedSkin2 = unit1.UnitData.GetSelectedSkin();
      CharacterDB.Job characterData2 = DependencyUtility.GetCharacterData(unit1.UnitParam, job2, selectedSkin2);
      if (characterData2 != null)
      {
        foreach (SkillData battleSkill in unit.BattleSkills)
        {
          if (battleSkill.IsDynamicTransformSkill() && !string.IsNullOrEmpty(battleSkill.SkillParam.motion))
          {
            SkillSequence sequence = SkillSequence.FindSequence(battleSkill.SkillParam.motion, !Application.isPlaying);
            if (sequence != null)
            {
              if (!string.IsNullOrEmpty(sequence.SkillAnimation.Name))
                this.GetDependencies_UnitAnimation(characterData2, sequence.SkillAnimation.Name + "_chg", false);
              if (!string.IsNullOrEmpty(sequence.StartAnimation))
                this.GetDependencies_UnitAnimation(characterData2, sequence.StartAnimation + "_chg", false);
            }
          }
        }
      }
    }
    for (int index = unit.BattleAbilitys.Count - 1; index >= 0; --index)
    {
      AbilityData battleAbility = unit.BattleAbilitys[index];
      if (battleAbility != null && battleAbility.Param != null)
        this.AddExternalDependencies(AssetPath.AbilityIcon(battleAbility.Param));
    }
    if (unit != null)
      this.GetDependencies_UnitVoice(unit.UnitParam);
    if (dlStatusEffects && this.badStatusEffectParam != null && this.badStatusEffectParam.effects != null)
    {
      for (int index = 0; index < this.badStatusEffectParam.effects.Count; ++index)
      {
        if (!string.IsNullOrEmpty(this.badStatusEffectParam.effects[index].AnimationName))
          this.GetDependencies_UnitAnimation(characterData1, this.badStatusEffectParam.effects[index].AnimationName, false);
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
            ArtifactParam artifactParam = artifacts.Find((Predicate<ArtifactParam>) (item => item.iname == iname));
            if (artifactParam != null && artifactParam.type == ArtifactTypes.Arms)
              this.GetDependencies_ArtifactParam(artifactParam);
          }
          ArtifactParam artifactParam1 = artifacts.Find((Predicate<ArtifactParam>) (item => item.iname == jd.Param.artifact));
          if (artifactParam1 != null && artifactParam1.type == ArtifactTypes.Arms)
            this.GetDependencies_ArtifactParam(artifactParam1);
        }
      }
    }
    else
    {
      if (unit.Job == null)
        return;
      this.GetDependencies_ArtifactParam(MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetArtifactParam(unit.Job.Param.artifact));
    }
  }

  public void GetDependencies_UnitVoice(UnitParam unitParam)
  {
    if (unitParam == null || string.IsNullOrEmpty(unitParam.voice))
      return;
    string[] strArray1 = MySound.VoiceCueSheetFileName(unitParam.voice);
    if (strArray1 == null)
      return;
    for (int index = 0; index < strArray1.Length; ++index)
      this.AddExternalDependencies("StreamingAssets/" + strArray1[index]);
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
              this.AddExternalDependencies("StreamingAssets/" + strArray2[index]);
          }
        }
      }
    }
  }

  public void GetDependencies_ArtifactParam(ArtifactParam artifactParam)
  {
    if (artifactParam == null)
      return;
    string path = AssetPath.Artifacts(artifactParam);
    if (string.IsNullOrEmpty(path))
      return;
    this.AddExternalDependencies(path);
  }

  public void GetDependencies_JobParam(JobParam jobParam)
  {
    if (jobParam == null)
      return;
    this.AddExternalDependencies(AssetPath.JobIconMedium(jobParam));
    this.AddExternalDependencies(AssetPath.JobIconSmall(jobParam));
    if (string.IsNullOrEmpty(jobParam.wepmdl))
      return;
    this.AddExternalDependencies(AssetPath.JobEquipment(jobParam));
  }

  private void GetDependencies_SkillParam(CharacterDB.Job jobData, SkillParam skill)
  {
    if (skill == null)
      return;
    if (!string.IsNullOrEmpty(skill.CutInConceptCardId))
    {
      ConceptCardParam conceptCardParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetConceptCardParam(skill.CutInConceptCardId);
      if (conceptCardParam != null)
        this.AddExternalDependencies(AssetPath.ConceptCard(conceptCardParam));
    }
    if (!string.IsNullOrEmpty(skill.TrickId))
    {
      TrickParam trickParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetTrickParam(skill.TrickId);
      if (trickParam != null)
      {
        this.AddExternalDependencies(AssetPath.TrickIconUI(trickParam.MarkerName));
        if (!string.IsNullOrEmpty(trickParam.EffectName))
          this.AddExternalDependencies(AssetPath.TrickEffect(trickParam.EffectName));
      }
    }
    if (!string.IsNullOrEmpty(skill.WeatherId))
      this.GetDependencies_WeatherAsset(skill.WeatherId);
    if (!string.IsNullOrEmpty(skill.BreakObjId))
    {
      NPCSetting breakObjNpc = this.CreateBreakObjNPC(MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetBreakObjParam(skill.BreakObjId));
      if (breakObjNpc != null)
        this.GetDependencies_NPCSetting(breakObjNpc);
    }
    if (string.IsNullOrEmpty(skill.motion) && string.IsNullOrEmpty(skill.effect))
      return;
    SkillSequence sequence1 = SkillSequence.FindSequence(skill.motion, !Application.isPlaying);
    if (sequence1 == null)
      return;
    bool is_collabo_skill = !string.IsNullOrEmpty(skill.CollaboMainId);
    if (!string.IsNullOrEmpty(sequence1.SkillAnimation.Name))
    {
      this.GetDependencies_UnitAnimation(jobData, sequence1.SkillAnimation.Name, false, is_collabo_skill);
      if (is_collabo_skill)
        this.GetDependencies_UnitAnimation(jobData, sequence1.SkillAnimation.Name + "_sub", false, is_collabo_skill);
    }
    if (!string.IsNullOrEmpty(sequence1.ChantAnimation.Name))
    {
      this.GetDependencies_UnitAnimation(jobData, sequence1.ChantAnimation.Name, false, is_collabo_skill);
      if (is_collabo_skill)
        this.GetDependencies_UnitAnimation(jobData, sequence1.ChantAnimation.Name + "_sub", false, is_collabo_skill);
    }
    if (!string.IsNullOrEmpty(sequence1.EndAnimation.Name))
    {
      this.GetDependencies_UnitAnimation(jobData, sequence1.EndAnimation.Name, false, is_collabo_skill);
      if (is_collabo_skill)
        this.GetDependencies_UnitAnimation(jobData, sequence1.EndAnimation.Name + "_sub", false, is_collabo_skill);
    }
    if (!string.IsNullOrEmpty(sequence1.StartAnimation))
    {
      this.GetDependencies_UnitAnimation(jobData, sequence1.StartAnimation, false, is_collabo_skill);
      if (is_collabo_skill)
        this.GetDependencies_UnitAnimation(jobData, sequence1.StartAnimation + "_sub", false, is_collabo_skill);
    }
    if (!string.IsNullOrEmpty(skill.effect))
    {
      this.AddExternalDependencies(AssetPath.SkillEffect(skill));
      if (!string.IsNullOrEmpty(skill.CollaboMainId))
        this.AddExternalDependencies(AssetPath.SkillEffect(skill) + "_sub");
    }
    if (!string.IsNullOrEmpty(skill.SkillMotionId))
    {
      SkillMotionParam skillMotionParam = MonoSingleton<GameManager>.Instance.MasterParam.GetSkillMotionParam(skill.SkillMotionId);
      if (skillMotionParam != null)
      {
        List<string> motionList = skillMotionParam.GetMotionList();
        for (int index = 0; index < motionList.Count; ++index)
        {
          SkillSequence sequence2 = SkillSequence.FindSequence(motionList[index], !Application.isPlaying);
          if (sequence2 != null)
          {
            if (!string.IsNullOrEmpty(sequence2.SkillAnimation.Name))
            {
              this.GetDependencies_UnitAnimation(jobData, sequence2.SkillAnimation.Name, false, is_collabo_skill);
              if (is_collabo_skill)
                this.GetDependencies_UnitAnimation(jobData, sequence2.SkillAnimation.Name + "_sub", false, is_collabo_skill);
            }
            if (!string.IsNullOrEmpty(sequence2.ChantAnimation.Name))
            {
              this.GetDependencies_UnitAnimation(jobData, sequence2.ChantAnimation.Name, false, is_collabo_skill);
              if (is_collabo_skill)
                this.GetDependencies_UnitAnimation(jobData, sequence2.ChantAnimation.Name + "_sub", false, is_collabo_skill);
            }
            if (!string.IsNullOrEmpty(sequence2.EndAnimation.Name))
            {
              this.GetDependencies_UnitAnimation(jobData, sequence2.EndAnimation.Name, false, is_collabo_skill);
              if (is_collabo_skill)
                this.GetDependencies_UnitAnimation(jobData, sequence2.EndAnimation.Name + "_sub", false, is_collabo_skill);
            }
            if (!string.IsNullOrEmpty(sequence2.StartAnimation))
            {
              this.GetDependencies_UnitAnimation(jobData, sequence2.StartAnimation, false, is_collabo_skill);
              if (is_collabo_skill)
                this.GetDependencies_UnitAnimation(jobData, sequence2.StartAnimation + "_sub", false, is_collabo_skill);
            }
          }
        }
        List<string> effectList = skillMotionParam.GetEffectList();
        for (int index = 0; index < effectList.Count; ++index)
        {
          this.AddExternalDependencies(AssetPath.SkillEffect(skill, effectList[index]));
          if (!string.IsNullOrEmpty(skill.CollaboMainId))
            this.AddExternalDependencies(AssetPath.SkillEffect(skill, effectList[index]) + "_sub");
        }
      }
    }
    if (!string.IsNullOrEmpty(skill.SceneName))
      this.AddExternalDependencies(AssetPath.SkillScene(skill.SceneName));
    if (string.IsNullOrEmpty(skill.SceneNameBigUnit))
      return;
    this.AddExternalDependencies(AssetPath.SkillScene(skill.SceneNameBigUnit));
  }

  public void GetDependencies_UnitModel(CharacterDB.Job jobData)
  {
    this.GetDependencies_UnitJobModel(jobData);
    this.GetDependencies_UnitAnimation(jobData, TacticsUnitController.ANIM_IDLE_FIELD, true);
    this.GetDependencies_UnitAnimation(jobData, TacticsUnitController.ANIM_IDLE_DEMO, true);
    if (jobData.Movable)
    {
      this.GetDependencies_UnitAnimation(jobData, TacticsUnitController.ANIM_RUN_FIELD, true);
      this.GetDependencies_UnitAnimation(jobData, TacticsUnitController.ANIM_STEP, jobData.IsJobStep);
      this.GetDependencies_UnitAnimation(jobData, TacticsUnitController.ANIM_FALL_LOOP, jobData.IsJobFallloop);
      this.GetDependencies_UnitAnimation(jobData, TacticsUnitController.ANIM_CLIMBUP, jobData.IsJobJumploop);
      this.GetDependencies_UnitAnimation(jobData, TacticsUnitController.ANIM_GENKIDAMA, jobData.IsJobFreeze);
    }
    this.GetDependencies_UnitAnimation(jobData, TacticsUnitController.ANIM_PICKUP, jobData.IsJobPickup);
    this.GetDependencies_UnitAnimation(jobData, TacticsUnitController.ANIM_DOWN_STAND, jobData.IsJobDownstand);
    this.GetDependencies_UnitAnimation(jobData, TacticsUnitController.ANIM_DODGE, jobData.IsJobDodge);
    this.GetDependencies_UnitAnimation(jobData, TacticsUnitController.ANIM_DAMAGE, jobData.IsJobDamage);
    this.GetDependencies_UnitAnimation(jobData, TacticsUnitController.ANIM_DOWN, false);
    this.GetDependencies_UnitAnimation(jobData, TacticsUnitController.ANIM_ITEM_USE, false);
    this.GetDependencies_UnitAnimation(jobData, "cmn_toss_lift0", false);
    this.GetDependencies_UnitAnimation(jobData, "cmn_toss_throw0", false);
    this.GetDependencies_UnitAnimation(jobData, "idleBattleScene0", true);
    this.GetDependencies_UnitAnimation(jobData, "damageBattleScene0", true);
  }

  public void GetDependencies_UnitJobModel(CharacterDB.Job jobData)
  {
    if (!string.IsNullOrEmpty(jobData.BodyName))
      this.AddExternalDependencies("CH/BODY/" + jobData.BodyName);
    if (!string.IsNullOrEmpty(jobData.BodyAttachmentName))
      this.AddExternalDependencies("CH/BODYOPT/" + jobData.BodyAttachmentName);
    if (!string.IsNullOrEmpty(jobData.BodyTextureName))
      this.AddExternalDependencies("CH/BODYTEX/" + jobData.BodyTextureName);
    if (!string.IsNullOrEmpty(jobData.HeadName))
      this.AddExternalDependencies("CH/HEAD/" + jobData.HeadName);
    if (!string.IsNullOrEmpty(jobData.HeadAttachmentName))
      this.AddExternalDependencies("CH/HEADOPT/" + jobData.HeadAttachmentName);
    if (string.IsNullOrEmpty(jobData.HairName))
      return;
    this.AddExternalDependencies("CH/HAIR/" + jobData.HairName);
  }

  private void GetDependencies_UnitAnimation(
    CharacterDB.Job jobData,
    string animationName,
    bool addJobName,
    bool is_collabo_skill = false)
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
    this.AddExternalDependencies(stringBuilder.ToString());
  }

  public void GetDependencies_UnitTransformAnimation(Unit unit, Unit skill_unit)
  {
    if (unit == null || skill_unit == null)
      return;
    CharacterDB.Job characterData = DependencyUtility.GetCharacterData(unit.UnitParam, unit.Job == null ? (JobParam) null : unit.Job.Param, unit.UnitData.GetSelectedSkin());
    if (characterData == null)
      return;
    foreach (SkillData battleSkill in skill_unit.BattleSkills)
    {
      if (battleSkill.IsTransformSkill() && !string.IsNullOrEmpty(battleSkill.SkillParam.motion))
      {
        SkillSequence sequence = SkillSequence.FindSequence(battleSkill.SkillParam.motion, !Application.isPlaying);
        if (sequence != null)
        {
          if (!string.IsNullOrEmpty(sequence.SkillAnimation.Name))
            this.GetDependencies_UnitAnimation(characterData, sequence.SkillAnimation.Name + "_chg", false);
          if (!string.IsNullOrEmpty(sequence.StartAnimation))
            this.GetDependencies_UnitAnimation(characterData, sequence.StartAnimation + "_chg", false);
        }
      }
    }
  }

  private bool AddExternalDependencies(string path)
  {
    if (this.m_Dependency.Contains(path))
      return false;
    this.m_Dependency.Add(path);
    List<string> dependenciesFromAssetList = this.GetDependenciesFromAssetList(path);
    for (int index = 0; index < dependenciesFromAssetList.Count; ++index)
    {
      if (!string.IsNullOrEmpty(dependenciesFromAssetList[index]) && !this.m_Dependency.Contains(dependenciesFromAssetList[index]))
        this.m_Dependency.Add(dependenciesFromAssetList[index]);
    }
    return true;
  }

  private List<string> GetDependenciesFromAssetList(string path)
  {
    HashSet<string> dependencies = new HashSet<string>();
    List<string> dependenciesFromAssetList = new List<string>();
    if (this.m_AssetList.FindItemByPath(path) == null)
      return dependenciesFromAssetList;
    this.GetDependenciesFromAssetList(ref dependencies, this.m_AssetList, path);
    return dependencies.ToList<string>();
  }

  private void GetDependenciesFromAssetList(
    ref HashSet<string> dependencies,
    AssetList assetList,
    string path)
  {
    if (string.IsNullOrEmpty(path) || dependencies.Contains(path))
      return;
    AssetList.Item itemByPath = assetList.FindItemByPath(path);
    if (itemByPath == null)
      return;
    dependencies.Add(path);
    for (int index = 0; index < itemByPath.Dependencies.Length; ++index)
    {
      AssetList.Item dependency = itemByPath.Dependencies[index];
      if (dependency != null)
        this.GetDependenciesFromAssetList(ref dependencies, assetList, dependency.Path);
    }
    for (int index = 0; index < itemByPath.AdditionalDependencies.Length; ++index)
    {
      AssetList.Item additionalDependency = itemByPath.AdditionalDependencies[index];
      if (additionalDependency != null)
        this.GetDependenciesFromAssetList(ref dependencies, assetList, additionalDependency.Path);
    }
    for (int index = 0; index < itemByPath.AdditionalStreamingAssets.Length; ++index)
    {
      AssetList.Item additionalStreamingAsset = itemByPath.AdditionalStreamingAssets[index];
      if (additionalStreamingAsset != null)
        this.GetDependenciesFromAssetList(ref dependencies, assetList, additionalStreamingAsset.Path);
    }
  }

  public static string LoadText(string path)
  {
    string str = string.Empty;
    if (Application.isPlaying)
    {
      str = AssetManager.LoadTextData(path);
    }
    else
    {
      TextAsset textAsset = Resources.Load<TextAsset>(path);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) textAsset, (UnityEngine.Object) null))
        str = textAsset.text;
    }
    return str;
  }

  public NPCSetting CreateBreakObjNPC(BreakObjParam bo_param, int gx = 0, int gy = 0)
  {
    if (bo_param == null)
      return (NPCSetting) null;
    UnitParam unitParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetUnitParam(bo_param.UnitId);
    if (unitParam == null)
      return (NPCSetting) null;
    NPCSetting breakObjNpc = new NPCSetting();
    breakObjNpc.pos.x = (OInt) gx;
    breakObjNpc.pos.y = (OInt) gy;
    breakObjNpc.iname = (OString) bo_param.UnitId;
    breakObjNpc.lv = (OInt) 1;
    breakObjNpc.break_obj = new MapBreakObj();
    breakObjNpc.break_obj.clash_type = (int) bo_param.ClashType;
    breakObjNpc.break_obj.ai_type = (int) bo_param.AiType;
    breakObjNpc.break_obj.side_type = (int) bo_param.SideType;
    breakObjNpc.break_obj.ray_type = (int) bo_param.RayType;
    breakObjNpc.break_obj.is_ui = !bo_param.IsUI ? 0 : 1;
    breakObjNpc.break_obj.max_hp = (int) unitParam.ini_status.param.hp;
    if (bo_param.RestHps != null)
    {
      breakObjNpc.break_obj.rest_hps = new int[bo_param.RestHps.Length];
      for (int index = 0; index < bo_param.RestHps.Length; ++index)
        breakObjNpc.break_obj.rest_hps[index] = bo_param.RestHps[index];
    }
    else
    {
      int length = 2;
      if (unitParam.search > (byte) 1)
        length = (int) unitParam.search - 1;
      breakObjNpc.break_obj.rest_hps = new int[length];
      int maxHp = breakObjNpc.break_obj.max_hp;
      int num;
      breakObjNpc.break_obj.rest_hps[0] = num = maxHp - 1;
      for (int index = 1; index < length; ++index)
        breakObjNpc.break_obj.rest_hps[index] = num * (length - index) / length;
    }
    return breakObjNpc;
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

  private static void CreateAbilityChangeSkillList(
    ref List<SkillParam> sp_lists,
    SkillParam skill_param)
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
          DependencyUtility.CreateAbilityChangeSkillList(ref sp_lists, skillParam);
        }
      }
    }
  }
}
