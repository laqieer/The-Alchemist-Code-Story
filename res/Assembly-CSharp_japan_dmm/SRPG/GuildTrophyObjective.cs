// Decompiled with JetBrains decompiler
// Type: SRPG.GuildTrophyObjective
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Text;

#nullable disable
namespace SRPG
{
  public class GuildTrophyObjective : TrophyObjective
  {
    public GuildTrophyConditionTypes type;

    public new int RequiredCount => this.ival;

    public override string GetDescription()
    {
      if (this.Param == null)
        return string.Empty;
      if (!string.IsNullOrEmpty(this.Param.Expr))
        return string.Format(LocalizedText.Get(this.Param.Expr), (object) this.ival);
      switch (this.type)
      {
        case GuildTrophyConditionTypes.winquest:
          if (this.SvalCount <= 0)
            return string.Format(LocalizedText.Get("sys.GUILDTROPHY_PLAYQUEST_DEFAULT"), (object) this.ival);
          if (this.SvalCount == 1)
          {
            QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(this.sval_base);
            return string.Format(LocalizedText.Get("sys.GUILDTROPHY_PLAYQUEST"), quest == null ? (object) ("?" + (object) this.sval) : (object) quest.name, (object) this.ival);
          }
          StringBuilder stringBuilder1 = GameUtility.GetStringBuilder();
          for (int index = 0; index < this.SvalCount; ++index)
          {
            QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(this.sval[index]);
            if (index > 0)
              stringBuilder1.Append(LocalizedText.Get("sys.GUILDTROPHY_OR"));
            stringBuilder1.Append(quest == null ? "?" + (object) this.sval : quest.name);
          }
          return string.Format(LocalizedText.Get("sys.GUILDTROPHY_PLAYQUEST_OR"), (object) stringBuilder1.ToString(), (object) this.ival);
        case GuildTrophyConditionTypes.winquest_by_area:
          if (this.SvalCount <= 0)
            return string.Format(LocalizedText.Get("sys.GUILDTROPHY_PLAYQUEST_BY_AREA_DEFAULT"), (object) this.ival);
          if (this.SvalCount == 1)
          {
            ChapterParam area = MonoSingleton<GameManager>.Instance.FindArea(this.sval_base);
            return string.Format(LocalizedText.Get("sys.GUILDTROPHY_PLAYQUEST_BY_AREA"), area == null ? (object) ("?" + (object) this.sval) : (object) area.name, (object) this.ival);
          }
          StringBuilder stringBuilder2 = GameUtility.GetStringBuilder();
          for (int index = 0; index < this.SvalCount; ++index)
          {
            ChapterParam area = MonoSingleton<GameManager>.Instance.FindArea(this.sval[index]);
            if (index > 0)
              stringBuilder2.Append(LocalizedText.Get("sys.GUILDTROPHY_OR"));
            stringBuilder2.Append(area == null ? "?" + (object) this.sval : area.name);
          }
          return string.Format(LocalizedText.Get("sys.GUILDTROPHY_PLAYQUEST_BY_AREA_OR"), (object) stringBuilder2.ToString(), (object) this.ival);
        case GuildTrophyConditionTypes.winquest_by_mode:
          if (this.SvalCount <= 0)
            return string.Format(LocalizedText.Get("sys.GUILDTROPHY_PLAYQUEST_BY_MODE_DEFAULT"), (object) this.ival);
          if (this.SvalCount == 1)
          {
            string questDifficultName = this.GetQuestDifficultName(QuestParam.GetQuestDifficulties(this.sval_base));
            return string.Format(LocalizedText.Get("sys.GUILDTROPHY_PLAYQUEST_BY_MODE"), (object) questDifficultName, (object) this.ival);
          }
          StringBuilder stringBuilder3 = GameUtility.GetStringBuilder();
          for (int index = 0; index < this.SvalCount; ++index)
          {
            if (index > 0)
              stringBuilder3.Append(LocalizedText.Get("sys.GUILDTROPHY_TEXT_CONNECT"));
            QuestDifficulties questDifficulties = QuestParam.GetQuestDifficulties(this.sval[index]);
            stringBuilder3.Append(this.GetQuestDifficultName(questDifficulties));
          }
          return string.Format(LocalizedText.Get("sys.GUILDTROPHY_PLAYQUEST_BY_MODE_OR"), (object) stringBuilder3.ToString(), (object) this.ival);
        case GuildTrophyConditionTypes.winevent:
          return string.Format(LocalizedText.Get("sys.GUILDTROPHY_PLAYEVENT", (object) this.ival));
        case GuildTrophyConditionTypes.playcolo:
          return string.Format(LocalizedText.Get("sys.GUILDTROPHY_PLAYCOLO", (object) this.ival));
        case GuildTrophyConditionTypes.playmulti:
          return string.Format(LocalizedText.Get("sys.GUILDTROPHY_PLAYMULTI", (object) this.ival));
        case GuildTrophyConditionTypes.guild_attend:
          return string.Format(LocalizedText.Get("sys.GUILDTROPHY_GUILD_ATTEND", (object) this.ival));
        case GuildTrophyConditionTypes.gvg_btl_exec:
          return string.Format(LocalizedText.Get("sys.GUILDTROPHY_GVG_BATTLE_EXEC", (object) this.ival));
        case GuildTrophyConditionTypes.guildraid_attack:
          return string.Format(LocalizedText.Get("sys.GUILDTROPHY_GUILDRAID_ATTACK", (object) this.ival));
        default:
          return string.Empty;
      }
    }

    private string GetQuestDifficultName(QuestDifficulties qdiff)
    {
      string empty = string.Empty;
      switch (qdiff)
      {
        case QuestDifficulties.Normal:
          empty = LocalizedText.Get("sys.QUEST_MODE_NORMAL");
          break;
        case QuestDifficulties.Elite:
          empty = LocalizedText.Get("sys.QUEST_MODE_HARD");
          break;
        case QuestDifficulties.Extra:
          empty = LocalizedText.Get("sys.QUEST_MODE_EXTRA");
          break;
      }
      return empty;
    }
  }
}
