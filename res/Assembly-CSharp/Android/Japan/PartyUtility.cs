// Decompiled with JetBrains decompiler
// Type: PartyUtility
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using SRPG;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public static class PartyUtility
{
  public static List<PartyEditData> LoadTeamPresets(PlayerPartyTypes partyType, out int lastSelectionIndex, bool containsNotFree = false)
  {
    return PartyUtility.LoadTeamPresets(partyType.ToEditPartyType(), out lastSelectionIndex, containsNotFree);
  }

  public static List<PartyEditData> LoadTeamPresets(PartyWindow2.EditPartyTypes partyType, out int lastSelectionIndex, bool containsNotFree = false)
  {
    lastSelectionIndex = -1;
    List<PartyEditData> partyEditDataList = (List<PartyEditData>) null;
    string currentTeamId = PartyUtility.GetCurrentTeamID(partyType, containsNotFree);
    if (PlayerPrefsUtility.HasKey(currentTeamId))
    {
      partyEditDataList = new List<PartyEditData>();
      PartyData party = MonoSingleton<GameManager>.Instance.Player.Partys[(int) partyType.ToPlayerPartyType()];
      UnitData[] src = new UnitData[party.MAX_UNIT];
      string str = PlayerPrefsUtility.GetString(currentTeamId, string.Empty);
      DebugUtility.Log(str);
      try
      {
        PartyUtility.JSON_TeamSettings jsonObject = JSONParser.parseJSONObject<PartyUtility.JSON_TeamSettings>(str);
        for (int index1 = 0; index1 < jsonObject.t.Length; ++index1)
        {
          PartyUtility.JSON_Team jsonTeam = jsonObject.t[index1];
          for (int index2 = 0; index2 < party.MAX_UNIT; ++index2)
          {
            if (index2 >= jsonTeam.u.Length)
            {
              src[index2] = (UnitData) null;
            }
            else
            {
              UnitData unitData = jsonTeam.u[index2] == 0L ? (UnitData) null : MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(jsonTeam.u[index2]);
              src[index2] = unitData;
            }
          }
          string name = !string.IsNullOrEmpty(jsonObject.t[index1].name) ? jsonObject.t[index1].name : PartyUtility.CreateDefaultPartyNameFromIndex(index1);
          PartyEditData partyEditData = new PartyEditData(src, name, party);
          partyEditDataList.Add(partyEditData);
        }
        lastSelectionIndex = jsonObject.n;
      }
      catch (Exception ex)
      {
        DebugUtility.LogException(ex);
      }
    }
    return partyEditDataList;
  }

  public static void SaveTeamPresets(PartyWindow2.EditPartyTypes partyType, int teamIndex, List<PartyEditData> teams, bool containsNotFree = false)
  {
    string currentTeamId = PartyUtility.GetCurrentTeamID(partyType, containsNotFree);
    if (string.IsNullOrEmpty(currentTeamId) || teamIndex < 0 || teamIndex >= teams.Count)
      return;
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("{\"n\":");
    stringBuilder.Append(teamIndex);
    stringBuilder.Append(",\"t\":[");
    for (int index1 = 0; index1 < teams.Count; ++index1)
    {
      if (index1 > 0)
        stringBuilder.Append(",");
      stringBuilder.Append("{\"name\":\"");
      stringBuilder.Append(teams[index1].Name);
      stringBuilder.Append("\",");
      stringBuilder.Append("\"u\":[");
      for (int index2 = 0; index2 < teams[teamIndex].PartyData.MAX_UNIT; ++index2)
      {
        long num = 0;
        if (index2 > 0)
          stringBuilder.Append(',');
        if (teams[index1].Units[index2] != null)
          num = teams[index1].Units[index2].UniqueID;
        stringBuilder.Append(num.ToString());
      }
      stringBuilder.Append("]}");
    }
    stringBuilder.Append("]}");
    DebugUtility.Log(stringBuilder.ToString());
    PlayerPrefsUtility.SetString(currentTeamId, stringBuilder.ToString(), true);
  }

  public static string CreateDefaultPartyNameFromIndex(int index)
  {
    return string.Format(LocalizedText.Get("sys.TEAMNAME"), (object) (index + 1));
  }

  public static string CreateOrdealPartyNameFromIndex(int index)
  {
    string str;
    switch (index)
    {
      case 0:
        str = "\x2460";
        break;
      case 1:
        str = "\x2461";
        break;
      case 2:
        str = "\x2462";
        break;
      case 3:
        str = "\x2463";
        break;
      case 4:
        str = "\x2464";
        break;
      case 5:
        str = "\x2465";
        break;
      case 6:
        str = "\x2466";
        break;
      case 7:
        str = "\x2467";
        break;
      case 8:
        str = "\x2468";
        break;
      default:
        str = "\x2460";
        break;
    }
    return string.Format(LocalizedText.Get("sys.TEAMNAME"), (object) str);
  }

  public static string GetCurrentTeamID(PartyWindow2.EditPartyTypes partyType, bool containForced = false)
  {
    string str = string.Empty;
    if (partyType == PartyWindow2.EditPartyTypes.Event && containForced)
      str = PlayerPrefsUtility.PARTY_TEAM_SUFFIX;
    return PlayerPrefsUtility.PARTY_TEAM_PREFIX + partyType.ToString().ToUpper() + str;
  }

  public static bool SetUnitIfEmptyParty(QuestParam quest, List<PartyEditData> teams, string[] units_list, List<PartySlotData> slotData)
  {
    bool flag1 = false;
    List<UnitData> units = MonoSingleton<GameManager>.Instance.Player.Units;
    foreach (PartyEditData team in teams)
    {
      bool flag2 = false;
      for (int index = 0; index < team.Units.Length && index < slotData.Count; ++index)
      {
        if (slotData[index].Type != PartySlotType.Free || team.Units[index] != null)
          flag2 = true;
      }
      if (!flag2)
      {
        bool flag3 = PartyUtility.AutoSetLeaderUnit(quest, team, units_list, units, slotData);
        flag1 = flag1 || flag3;
      }
    }
    return flag1;
  }

  public static bool AutoSetLeaderUnit(QuestParam quest, PartyEditData party, string[] kyouseiUnitIds, List<UnitData> units, List<PartySlotData> slotData)
  {
    if (units == null || units.Count <= 0)
      return false;
    using (List<UnitData>.Enumerator enumerator = units.GetEnumerator())
    {
label_10:
      while (enumerator.MoveNext())
      {
        UnitData u = enumerator.Current;
        if (u != null && (kyouseiUnitIds == null || -1 == Array.FindIndex<string>(kyouseiUnitIds, (Predicate<string>) (p => p == u.UnitID))) && (-1 == Array.FindIndex<UnitData>(party.Units, (Predicate<UnitData>) (p =>
        {
          if (p == null)
            return false;
          return p.UnitID == u.UnitID;
        })) && (!PartyUtility.IsHeloQuest(quest) || !u.UnitParam.IsHero())))
        {
          int index = 0;
          while (true)
          {
            if (index < party.Units.Length && index < slotData.Count)
            {
              if (slotData[index].Type != PartySlotType.Free)
                ++index;
              else
                break;
            }
            else
              goto label_10;
          }
          party.Units[index] = u;
          return true;
        }
      }
    }
    return false;
  }

  public static bool IsHeloQuest(QuestParam quest)
  {
    if (quest == null)
      return false;
    if (quest.type != QuestTypes.Story && quest.type != QuestTypes.Free && quest.type != QuestTypes.Extra)
      return quest.type == QuestTypes.Character;
    return true;
  }

  public static bool IsTeamLegalForOrdealQuest(QuestParam quest, IEnumerable<PartyEditData> teams)
  {
    HashSet<long> longSet = new HashSet<long>();
    foreach (PartyEditData team in teams)
    {
      foreach (UnitData unit in team.Units)
      {
        if (unit != null)
        {
          if (longSet.Contains(unit.UniqueID))
            return false;
          longSet.Add(unit.UniqueID);
        }
      }
    }
    return true;
  }

  public static bool IsTeamLegal(PlayerPartyTypes partyType, QuestParam quest, PartyEditData team)
  {
    return PartyUtility.IsTeamLegal(partyType.ToEditPartyType(), quest, team);
  }

  public static bool IsTeamLegal(PartyWindow2.EditPartyTypes partyType, QuestParam quest, PartyEditData team)
  {
    QuestPartyParam questPartyParam = quest == null ? (QuestPartyParam) null : quest.questParty;
    if (questPartyParam != null)
    {
      if (questPartyParam.type_1 == PartySlotType.Free && team.Units[0] == null)
        return false;
    }
    else if (team.Units[0] == null)
      return false;
    bool flag = false;
    if (questPartyParam != null)
    {
      PartySlotTypeUnitPair[] mainSubSlots = questPartyParam.GetMainSubSlots();
      for (int index = 0; index < team.Units.Length && index < mainSubSlots.Length; ++index)
      {
        if (mainSubSlots[index].Type != PartySlotType.Free || team.Units[index] != null)
          flag = true;
      }
    }
    else
    {
      for (int index = 0; index < team.Units.Length; ++index)
      {
        if (team.Units[index] != null)
          flag = true;
      }
    }
    if (!flag)
      return false;
    for (int index1 = 0; index1 < team.Units.Length; ++index1)
    {
      if (team.Units[index1] != null)
      {
        for (int index2 = index1 + 1; index2 < team.Units.Length; ++index2)
        {
          if (team.Units[index1] == team.Units[index2])
            return false;
        }
      }
    }
    if (!PartyUtility.IsHeroesAvailable(partyType, quest))
    {
      for (int index = 0; index < team.Units.Length; ++index)
      {
        if (team.Units[index] != null && team.Units[index].UnitParam.IsHero())
          return false;
      }
    }
    return true;
  }

  public static bool IsHeroesAvailable(PartyWindow2.EditPartyTypes partyType, QuestParam quest)
  {
    return partyType != PartyWindow2.EditPartyTypes.Normal;
  }

  public static bool IsSoloStoryParty(QuestParam quest)
  {
    if (!PartyUtility.IsHeloQuest(quest))
      return false;
    QuestCondParam entryCondition = quest.EntryCondition;
    return quest.units.IsNotNull() && entryCondition != null && (entryCondition.unit != null && quest.units.Length == 1) && (entryCondition.unit.Length == 1 && quest.units.Get(0) == entryCondition.unit[0]);
  }

  public static bool IsSoloStoryOrEventParty(QuestParam quest)
  {
    QuestCondParam entryCondition = quest.EntryCondition;
    if (!quest.units.IsNotNull() || entryCondition == null || (entryCondition.unit == null || !quest.UseFixEditor))
      return false;
    if (quest.type != QuestTypes.Event)
      return quest.type == QuestTypes.Beginner;
    return true;
  }

  public static bool KyouseiUnitPartyEdit(QuestParam quest, PartyEditData edit)
  {
    bool flag = false;
    QuestCondParam entryCondition = quest.EntryCondition;
    string[] list = quest.units.GetList();
    for (int i = 0; i < edit.Units.Length; ++i)
    {
      if (edit.Units[i] != null)
      {
        if (Array.FindIndex<string>(list, (Predicate<string>) (p => p == edit.Units[i].UnitID)) != -1)
        {
          edit.Units[i] = (UnitData) null;
          flag = true;
        }
        else if (Array.FindIndex<string>(entryCondition.unit, (Predicate<string>) (p => p == edit.Units[i].UnitID)) == -1)
        {
          edit.Units[i] = (UnitData) null;
          flag = true;
        }
      }
    }
    return flag;
  }

  public static bool PartyUnitsRemoveHelo(PartyEditData party, string[] kyouseiUnitIds)
  {
    bool flag = false;
    for (int index = 0; index < party.Units.Length; ++index)
    {
      if (party.Units[index] != null)
      {
        UnitData ptu = party.Units[index];
        if (-1 != Array.FindIndex<string>(kyouseiUnitIds, (Predicate<string>) (p => p == ptu.UnitID)))
        {
          party.Units[index] = (UnitData) null;
          flag = true;
        }
      }
    }
    return flag;
  }

  public static bool ResetToDefaultTeamIfNeeded(PlayerPartyTypes partyType, QuestParam quest, List<PartyEditData> teams)
  {
    bool flag = true;
    UnitData[] src = (UnitData[]) null;
    PlayerData player = MonoSingleton<GameManager>.Instance.Player;
    for (int index1 = 0; index1 < teams.Count; ++index1)
    {
      if (!PartyUtility.IsTeamLegal(partyType, quest, teams[index1]))
      {
        flag = false;
        if (src == null)
        {
          PartyData partyOfType = player.FindPartyOfType(partyType);
          src = new UnitData[partyOfType.MAX_UNIT];
          for (int index2 = 0; index2 < src.Length; ++index2)
          {
            long unitUniqueId = partyOfType.GetUnitUniqueID(index2);
            if (unitUniqueId != 0L)
            {
              UnitData unitDataByUniqueId = player.FindUnitDataByUniqueID(unitUniqueId);
              if (unitDataByUniqueId != null)
                src[index2] = unitDataByUniqueId;
            }
          }
        }
        teams[index1].SetUnits(src);
      }
    }
    return flag;
  }

  public static bool ResetToDefaultTeamIfNeededForOrdealQuest(QuestParam quest, List<PartyEditData> teams)
  {
    if (!PartyUtility.IsTeamLegalForOrdealQuest(quest, (IEnumerable<PartyEditData>) teams))
    {
      using (List<PartyEditData>.Enumerator enumerator = teams.GetEnumerator())
      {
        if (enumerator.MoveNext())
        {
          enumerator.Current.Reset();
          return false;
        }
      }
    }
    return true;
  }

  public static PartyWindow2.EditPartyTypes GetEditPartyTypes(QuestParam quest)
  {
    QuestTypes type = quest.type;
    switch (type)
    {
      case QuestTypes.Event:
      case QuestTypes.Gps:
      case QuestTypes.Beginner:
      case QuestTypes.GenesisStory:
      case QuestTypes.GenesisBoss:
        return PartyWindow2.EditPartyTypes.Event;
      case QuestTypes.Character:
        return PartyWindow2.EditPartyTypes.Character;
      case QuestTypes.Tower:
        return PartyWindow2.EditPartyTypes.Tower;
      case QuestTypes.VersusFree:
      case QuestTypes.VersusRank:
        return PartyWindow2.EditPartyTypes.Versus;
      case QuestTypes.MultiGps:
        return PartyWindow2.EditPartyTypes.MP;
      case QuestTypes.RankMatch:
        return PartyWindow2.EditPartyTypes.RankMatch;
      case QuestTypes.Raid:
        return PartyWindow2.EditPartyTypes.Raid;
      default:
        if (type != QuestTypes.Multi)
          return PartyWindow2.EditPartyTypes.Auto;
        goto case QuestTypes.MultiGps;
    }
  }

  public static int RecommendTypeToComparatorOrder(GlobalVars.RecommendType targetType)
  {
    switch (targetType)
    {
      case GlobalVars.RecommendType.Total:
        return 0;
      case GlobalVars.RecommendType.Attack:
        return 2;
      case GlobalVars.RecommendType.Defence:
        return 3;
      case GlobalVars.RecommendType.Magic:
        return 4;
      case GlobalVars.RecommendType.Mind:
        return 5;
      case GlobalVars.RecommendType.Speed:
        return 6;
      case GlobalVars.RecommendType.AttackTypeSlash:
        return 7;
      case GlobalVars.RecommendType.AttackTypeStab:
        return 8;
      case GlobalVars.RecommendType.AttackTypeBlow:
        return 9;
      case GlobalVars.RecommendType.AttackTypeShot:
        return 10;
      case GlobalVars.RecommendType.AttackTypeMagic:
        return 11;
      case GlobalVars.RecommendType.AttackTypeNone:
        return 12;
      case GlobalVars.RecommendType.Hp:
        return 1;
      default:
        return 0;
    }
  }

  public static int CalcTotalAttack(PartyEditData party, List<UnitData> ownUnits, SupportData support = null, List<UnitData> guest = null)
  {
    return PartyUtility.CalcTotalAttack(party, 0, ownUnits, support, guest);
  }

  public static int CalcTotalAttack(PartyEditData party, int lockedSlots, List<UnitData> ownUnits, SupportData support, List<UnitData> guest = null)
  {
    int num = 0;
    for (int index = 0; index < party.Units.Length; ++index)
    {
      UnitData unit = PartyUtility.FindUnit(party.Units[index], ownUnits);
      if (unit != null && (lockedSlots & 1 << index) == 0)
        num = num + (int) unit.Status.param.atk + (int) unit.Status.param.mag;
    }
    if (support != null && support.Unit != null)
      num = num + (int) support.Unit.Status.param.atk + (int) support.Unit.Status.param.mag;
    if (guest != null)
    {
      foreach (UnitData unitData in guest)
      {
        num += (int) unitData.Status.param.atk;
        num += (int) unitData.Status.param.mag;
      }
    }
    return num;
  }

  public static int CalcTotalStatus(PartyEditData party, int lockedSlots, List<UnitData> ownUnits, SupportData support, UnitData guest = null)
  {
    int num = 0;
    for (int index = 0; index < party.Units.Length; ++index)
    {
      UnitData unit = PartyUtility.FindUnit(party.Units[index], ownUnits);
      if (unit != null && (lockedSlots & 1 << index) == 0)
        num = num + (int) unit.Status.param.atk + (int) unit.Status.param.mag + (int) unit.Status.param.def + (int) unit.Status.param.mnd;
    }
    if (support != null && support.Unit != null)
      num = num + (int) support.Unit.Status.param.atk + (int) support.Unit.Status.param.mag + (int) support.Unit.Status.param.def + (int) support.Unit.Status.param.mnd;
    if (guest != null)
      num = num + (int) guest.Status.param.atk + (int) guest.Status.param.mag + (int) guest.Status.param.def + (int) guest.Status.param.mnd;
    return num;
  }

  public static bool ValidateOrdealTeams(QuestParam quest, List<PartyEditData> teams, List<SupportData> supports, bool validateOnly)
  {
    if (quest != null)
    {
      int lv = MonoSingleton<GameManager>.Instance.Player.Lv;
      if (MonoSingleton<GameManager>.Instance.Player.Stamina < quest.RequiredApWithPlayerLv(lv, true) && !validateOnly)
      {
        MonoSingleton<GameManager>.Instance.StartBuyStaminaSequence(true);
        return false;
      }
    }
    foreach (PartyEditData team in teams)
    {
      bool flag = false;
      foreach (UnitData unit in team.Units)
      {
        if (unit != null)
        {
          flag = true;
          break;
        }
      }
      if (!flag)
      {
        if (!validateOnly)
          UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.PARTYEDITOR_CANTSTART"), (UIUtility.DialogResultEvent) (dialog => {}), (GameObject) null, false, -1);
        return false;
      }
    }
    if (quest != null)
    {
      foreach (PartyEditData team in teams)
      {
        int num1 = 0;
        int num2 = 0;
        int maxUnit = team.PartyData.MAX_UNIT;
        for (int index = 0; index < maxUnit; ++index)
        {
          if (team.Units.Length > index)
          {
            if (team.Units[index] != null)
              ++num1;
            if (team.Units[index] != null && !quest.IsUnitAllowed(team.Units[index]))
              ++num2;
          }
        }
        if (num1 <= 0)
        {
          if (!validateOnly)
            UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.PARTYEDITOR_CANTSTART"), (UIUtility.DialogResultEvent) (dialog => {}), (GameObject) null, false, -1);
          return false;
        }
        string empty = string.Empty;
        if (!quest.IsEntryQuestCondition((IEnumerable<UnitData>) team.Units, ref empty))
        {
          if (!validateOnly)
            UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get(empty), (UIUtility.DialogResultEvent) (dialog => {}), (GameObject) null, false, -1);
          return false;
        }
      }
      if (supports != null)
      {
        foreach (SupportData support in supports)
        {
          if (support != null && support.Unit != null)
          {
            string empty = string.Empty;
            if (!quest.IsEntryQuestCondition(support.Unit, ref empty))
            {
              if (!validateOnly)
                UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get(empty), (UIUtility.DialogResultEvent) (dialog => {}), (GameObject) null, false, -1);
              return false;
            }
          }
        }
      }
    }
    return true;
  }

  public static bool CheckWarningForOrdealTeams(List<PartyEditData> teams, Action okAction)
  {
    bool flag = false;
    foreach (PartyEditData team in teams)
    {
      int num = 0;
      foreach (UnitData unit in team.Units)
      {
        if (unit != null)
          ++num;
      }
      if (num < team.PartyData.MAX_MAINMEMBER)
        flag = true;
    }
    if (!flag)
      return false;
    UIUtility.ConfirmBox(LocalizedText.Get("sys.PARTYEDITOR_PARTYNOTFULL"), (UIUtility.DialogResultEvent) (dialog => okAction()), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1, (string) null, (string) null);
    return true;
  }

  public static UnitData FindUnit(long uniqueID, List<UnitData> ownUnits)
  {
    for (int index = ownUnits.Count - 1; index >= 0; --index)
    {
      if (ownUnits[index].UniqueID == uniqueID)
        return ownUnits[index];
    }
    return (UnitData) null;
  }

  public static UnitData FindUnit(UnitData source, List<UnitData> ownUnits)
  {
    if (source != null)
      return PartyUtility.FindUnit(source.UniqueID, ownUnits);
    return (UnitData) null;
  }

  public static void CreatePartySlotData(QuestParam param, out List<PartySlotData> mainSlotData, out List<PartySlotData> subSlotData, out PartySlotData supportSlotData)
  {
    mainSlotData = new List<PartySlotData>();
    subSlotData = new List<PartySlotData>();
    supportSlotData = (PartySlotData) null;
    if (param == null)
    {
      mainSlotData.Add(new PartySlotData(PartySlotType.Free, (string) null, PartySlotIndex.Main1, false));
      mainSlotData.Add(new PartySlotData(PartySlotType.Free, (string) null, PartySlotIndex.Main2, false));
      mainSlotData.Add(new PartySlotData(PartySlotType.Free, (string) null, PartySlotIndex.Main3, false));
      mainSlotData.Add(new PartySlotData(PartySlotType.Free, (string) null, PartySlotIndex.Main4, false));
      supportSlotData = new PartySlotData(PartySlotType.Free, (string) null, PartySlotIndex.Support, false);
      subSlotData.Add(new PartySlotData(PartySlotType.Free, (string) null, PartySlotIndex.Sub1, false));
      subSlotData.Add(new PartySlotData(PartySlotType.Free, (string) null, PartySlotIndex.Sub2, false));
    }
    else if (param.questParty != null)
    {
      QuestPartyParam questParty = param.questParty;
      mainSlotData.Add(new PartySlotData(questParty.type_1, questParty.unit_1, PartySlotIndex.Main1, false));
      mainSlotData.Add(new PartySlotData(questParty.type_2, questParty.unit_2, PartySlotIndex.Main2, false));
      mainSlotData.Add(new PartySlotData(questParty.type_3, questParty.unit_3, PartySlotIndex.Main3, false));
      mainSlotData.Add(new PartySlotData(questParty.type_4, questParty.unit_4, PartySlotIndex.Main4, false));
      supportSlotData = new PartySlotData(questParty.support_type, (string) null, PartySlotIndex.Support, false);
      subSlotData.Add(new PartySlotData(questParty.subtype_1, questParty.subunit_1, PartySlotIndex.Sub1, false));
      subSlotData.Add(new PartySlotData(questParty.subtype_2, questParty.subunit_2, PartySlotIndex.Sub2, false));
    }
    else
    {
      string unitName1 = param.units == null || param.units.Length <= 0 ? (string) null : param.units.GetList()[0];
      if (param.GetPartyCondType() != PartyCondType.Forced)
      {
        if (param.type == QuestTypes.Tower)
        {
          mainSlotData.Add(new PartySlotData(PartySlotType.Free, (string) null, PartySlotIndex.Main1, false));
          mainSlotData.Add(new PartySlotData(PartySlotType.Free, (string) null, PartySlotIndex.Main2, false));
          mainSlotData.Add(new PartySlotData(PartySlotType.Free, (string) null, PartySlotIndex.Main3, false));
          mainSlotData.Add(new PartySlotData(PartySlotType.Free, (string) null, PartySlotIndex.Main4, false));
          mainSlotData.Add(new PartySlotData(PartySlotType.Free, (string) null, PartySlotIndex.Main5, false));
          supportSlotData = new PartySlotData(PartySlotType.Free, (string) null, PartySlotIndex.Support, false);
          subSlotData.Add(new PartySlotData(PartySlotType.Free, (string) null, PartySlotIndex.Sub1, false));
          subSlotData.Add(new PartySlotData(PartySlotType.Free, (string) null, PartySlotIndex.Sub2, false));
        }
        else if (param.type == QuestTypes.Ordeal)
        {
          mainSlotData.Add(new PartySlotData(PartySlotType.Free, (string) null, PartySlotIndex.Main1, false));
          mainSlotData.Add(new PartySlotData(PartySlotType.Free, (string) null, PartySlotIndex.Main2, false));
          mainSlotData.Add(new PartySlotData(PartySlotType.Free, (string) null, PartySlotIndex.Main3, false));
          mainSlotData.Add(new PartySlotData(PartySlotType.Free, (string) null, PartySlotIndex.Main4, false));
          supportSlotData = new PartySlotData(PartySlotType.Free, (string) null, PartySlotIndex.Support, false);
        }
        else if (param.type == QuestTypes.VersusFree)
        {
          mainSlotData.Add(new PartySlotData(PartySlotType.Free, (string) null, PartySlotIndex.Main1, false));
          mainSlotData.Add(new PartySlotData(PartySlotType.Free, (string) null, PartySlotIndex.Main2, false));
          mainSlotData.Add(new PartySlotData(PartySlotType.Free, (string) null, PartySlotIndex.Main3, false));
          mainSlotData.Add(new PartySlotData(PartySlotType.Free, (string) null, PartySlotIndex.Main4, false));
          mainSlotData.Add(new PartySlotData(PartySlotType.Free, (string) null, PartySlotIndex.Main5, false));
        }
        else if (param.type == QuestTypes.Arena)
        {
          mainSlotData.Add(new PartySlotData(PartySlotType.Free, (string) null, PartySlotIndex.Main1, false));
          mainSlotData.Add(new PartySlotData(PartySlotType.Free, (string) null, PartySlotIndex.Main2, false));
          mainSlotData.Add(new PartySlotData(PartySlotType.Free, (string) null, PartySlotIndex.Main3, false));
          mainSlotData.Add(new PartySlotData(PartySlotType.Locked, (string) null, PartySlotIndex.Main4, false));
          supportSlotData = new PartySlotData(PartySlotType.Locked, (string) null, PartySlotIndex.Support, false);
          subSlotData.Add(new PartySlotData(PartySlotType.Locked, (string) null, PartySlotIndex.Sub1, false));
          subSlotData.Add(new PartySlotData(PartySlotType.Locked, (string) null, PartySlotIndex.Sub2, false));
        }
        else
        {
          mainSlotData.Add(new PartySlotData(PartySlotType.Free, (string) null, PartySlotIndex.Main1, false));
          mainSlotData.Add(new PartySlotData(PartySlotType.Free, (string) null, PartySlotIndex.Main2, false));
          mainSlotData.Add(new PartySlotData(PartySlotType.Free, (string) null, PartySlotIndex.Main3, false));
          if (string.IsNullOrEmpty(unitName1))
            mainSlotData.Add(new PartySlotData(PartySlotType.Free, (string) null, PartySlotIndex.Main4, false));
          else
            mainSlotData.Add(new PartySlotData(PartySlotType.ForcedHero, unitName1, PartySlotIndex.Main4, false));
          supportSlotData = new PartySlotData(PartySlotType.Free, (string) null, PartySlotIndex.Support, false);
          subSlotData.Add(new PartySlotData(PartySlotType.Free, (string) null, PartySlotIndex.Sub1, false));
          subSlotData.Add(new PartySlotData(PartySlotType.Free, (string) null, PartySlotIndex.Sub2, false));
        }
      }
      else
      {
        QuestCondParam cond = param.GetQuestCondParam();
        if (cond.unit.Length > 1)
        {
          int index = 0;
          Func<string> func = (Func<string>) (() =>
          {
            if (index >= cond.unit.Length)
              return (string) null;
            string[] unit = cond.unit;
            int num;
            index = (num = index) + 1;
            int index = num;
            return unit[index];
          });
          string unitName2 = func();
          mainSlotData.Add(new PartySlotData(unitName2 != null ? PartySlotType.Forced : PartySlotType.Locked, unitName2, PartySlotIndex.Main1, false));
          string unitName3 = func();
          mainSlotData.Add(new PartySlotData(unitName3 != null ? PartySlotType.Forced : PartySlotType.Locked, unitName3, PartySlotIndex.Main2, false));
          string unitName4 = func();
          mainSlotData.Add(new PartySlotData(unitName4 != null ? PartySlotType.Forced : PartySlotType.Locked, unitName4, PartySlotIndex.Main3, false));
          string str = func();
          mainSlotData.Add(new PartySlotData(PartySlotType.ForcedHero, unitName1, PartySlotIndex.Main4, false));
          supportSlotData = new PartySlotData(PartySlotType.Free, (string) null, PartySlotIndex.Support, false);
          string unitName5 = func();
          subSlotData.Add(new PartySlotData(unitName5 != null ? PartySlotType.Forced : PartySlotType.Locked, unitName5, PartySlotIndex.Sub1, false));
          string unitName6 = func();
          subSlotData.Add(new PartySlotData(unitName6 != null ? PartySlotType.Forced : PartySlotType.Locked, unitName6, PartySlotIndex.Sub2, false));
        }
        else
        {
          if (cond.unit[0] == unitName1)
            mainSlotData.Add(new PartySlotData(PartySlotType.ForcedHero, unitName1, PartySlotIndex.Main1, false));
          else
            mainSlotData.Add(new PartySlotData(PartySlotType.Forced, cond.unit[0], PartySlotIndex.Main1, false));
          mainSlotData.Add(new PartySlotData(PartySlotType.Locked, (string) null, PartySlotIndex.Main2, false));
          mainSlotData.Add(new PartySlotData(PartySlotType.Locked, (string) null, PartySlotIndex.Main3, false));
          mainSlotData.Add(new PartySlotData(PartySlotType.Locked, (string) null, PartySlotIndex.Main4, false));
          supportSlotData = new PartySlotData(PartySlotType.Free, (string) null, PartySlotIndex.Support, false);
          subSlotData.Add(new PartySlotData(PartySlotType.Locked, (string) null, PartySlotIndex.Sub1, false));
          subSlotData.Add(new PartySlotData(PartySlotType.Locked, (string) null, PartySlotIndex.Sub2, false));
        }
      }
    }
  }

  public static void MergePartySlotWithPartyUnits(QuestParam questParam, List<PartySlotData> partySlotData, UnitData[] partyUnits, List<UnitData> guestUnits, bool isOwnUnits)
  {
    UnitData[] unitDataArray1 = new UnitData[partySlotData.Count];
    UnitData[] unitDataArray2 = new UnitData[partyUnits.Length];
    partyUnits.CopyTo((Array) unitDataArray2, 0);
    int num1 = 0;
    List<string> stringList = new List<string>();
    for (int index = 0; index < partySlotData.Count; ++index)
    {
      PartySlotData partySlotData1 = partySlotData[index];
      if (partySlotData1.Type == PartySlotType.Forced || partySlotData1.Type == PartySlotType.ForcedHero)
        stringList.Add(partySlotData1.UnitName);
    }
    for (int index = 0; index < unitDataArray1.Length && index < partySlotData.Count; ++index)
    {
      PartySlotData slot = partySlotData[index];
      if (slot.Type == PartySlotType.ForcedHero)
      {
        UnitData unitData = (UnitData) null;
        if (isOwnUnits)
          unitData = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUnitID(slot.UnitName);
        else if (partyUnits != null)
          unitData = Array.Find<UnitData>(partyUnits, (Predicate<UnitData>) (unit =>
          {
            if (unit != null && unit.UnitParam != null)
              return unit.UnitParam.iname == slot.UnitName;
            return false;
          }));
        if (unitData != null)
          guestUnits.Add(unitData);
        unitDataArray1[index] = (UnitData) null;
      }
      else if (slot.Type == PartySlotType.Forced)
      {
        if (isOwnUnits)
          unitDataArray1[index] = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUnitID(slot.UnitName);
        else if (partyUnits != null)
          unitDataArray1[index] = Array.Find<UnitData>(partyUnits, (Predicate<UnitData>) (unit =>
          {
            if (unit != null && unit.UnitParam != null)
              return unit.UnitParam.iname == slot.UnitName;
            return false;
          }));
      }
      else if (slot.Type == PartySlotType.Npc || slot.Type == PartySlotType.NpcHero || slot.Type == PartySlotType.Locked)
      {
        unitDataArray1[index] = (UnitData) null;
      }
      else
      {
        PartyData partyData = new PartyData(QuestParam.QuestToPartyIndex(questParam.type));
        int num2 = index >= partyData.MAX_MAINMEMBER ? partyData.MAX_UNIT : partyData.MAX_MAINMEMBER;
        while (num1 < num2 && num1 < unitDataArray2.Length)
        {
          UnitData unitData = unitDataArray2[num1++];
          if (unitData != null && !stringList.Contains(unitData.UnitID))
          {
            unitDataArray1[index] = unitData;
            break;
          }
        }
      }
    }
    for (int index = 0; index < partyUnits.Length && index < unitDataArray1.Length; ++index)
      partyUnits[index] = unitDataArray1[index];
  }

  private class JSON_Team
  {
    public string name;
    public long[] u;
  }

  private class JSON_TeamSettings
  {
    public int n;
    public PartyUtility.JSON_Team[] t;
  }
}
