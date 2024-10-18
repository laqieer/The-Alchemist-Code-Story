// Decompiled with JetBrains decompiler
// Type: SRPG.CharacterQuestData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class CharacterQuestData
  {
    private CharacterQuestData.EStatus status;
    public CharacterQuestData.EType questType;
    public QuestParam questParam;
    public UnitData unitData1;
    public UnitData unitData2;
    public UnitParam unitParam1;
    public UnitParam unitParam2;

    public bool HasUnit => this.unitData1 != null;

    public bool HasPairUnit => this.unitData1 != null && this.unitData2 != null;

    public CharacterQuestData.EStatus Status => this.status;

    public bool IsLock => this.status == CharacterQuestData.EStatus.Lock;

    public bool IsChallenged => this.status == CharacterQuestData.EStatus.Challenged;

    public bool IsComplete => this.status == CharacterQuestData.EStatus.Complete;

    public bool IsNew => this.status == CharacterQuestData.EStatus.New;

    public CollaboSkillParam.Pair GetPairUnit()
    {
      return this.unitData1 == null || this.unitData2 == null ? (CollaboSkillParam.Pair) null : new CollaboSkillParam.Pair(this.unitData1.UnitParam, this.unitData2.UnitParam);
    }

    public void UpdateStatus()
    {
      if (this.questType == CharacterQuestData.EType.Chara)
      {
        if (this.unitData1 != null)
        {
          if (this.unitData1.IsOpenCharacterQuest())
          {
            List<KeyValuePair<QuestParam, bool>> characterQuests = CharacterQuestList.GetCharacterQuests(this.unitData1);
            int count = characterQuests.FindAll((Predicate<KeyValuePair<QuestParam, bool>>) (pair => pair.Key.state == QuestStates.Cleared)).Count;
            if (characterQuests.FindAll((Predicate<KeyValuePair<QuestParam, bool>>) (pair => pair.Key.state == QuestStates.New && pair.Value)).Count > 0)
              this.status = CharacterQuestData.EStatus.New;
            else if (count == characterQuests.Count)
              this.status = CharacterQuestData.EStatus.Complete;
            else
              this.status = CharacterQuestData.EStatus.Challenged;
          }
          else
            this.status = CharacterQuestData.EStatus.Lock;
        }
        else
          this.status = CharacterQuestData.EStatus.Lock;
      }
      else if (this.unitData1 != null && this.unitData2 != null)
      {
        List<KeyValuePair<QuestParam, bool>> collaboSkillQuests = CharacterQuestList.GetCollaboSkillQuests(this.unitData1, this.unitData2);
        int count = collaboSkillQuests.FindAll((Predicate<KeyValuePair<QuestParam, bool>>) (pair => pair.Key.state == QuestStates.Cleared)).Count;
        if (collaboSkillQuests.FindAll((Predicate<KeyValuePair<QuestParam, bool>>) (pair => pair.Key.state == QuestStates.New && pair.Value)).Count > 0)
          this.status = CharacterQuestData.EStatus.New;
        else if (count == collaboSkillQuests.Count)
          this.status = CharacterQuestData.EStatus.Complete;
        else
          this.status = CharacterQuestData.EStatus.Challenged;
      }
      else
        this.status = CharacterQuestData.EStatus.Lock;
    }

    public enum EType
    {
      Chara,
      Collabo,
    }

    public enum EStatus
    {
      New,
      Challenged,
      Lock,
      Complete,
    }
  }
}
