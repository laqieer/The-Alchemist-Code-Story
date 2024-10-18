// Decompiled with JetBrains decompiler
// Type: SRPG.HomeBgSection
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;

#nullable disable
namespace SRPG
{
  public class HomeBgSection
  {
    public static void SetSection_SectionId(string section_id)
    {
      if (string.IsNullOrEmpty(section_id) || !HomeBgSection.IsStorySection(section_id))
        return;
      GlobalVars.HomeBgSection.Set(section_id);
    }

    public static void SetSection_ChapterId(string chapter_id)
    {
      ChapterParam area = MonoSingleton<GameManager>.Instance.FindArea(chapter_id);
      if (area == null || !HomeBgSection.IsStorySection(area.section))
        return;
      GlobalVars.HomeBgSection.Set(area.section);
    }

    public static void SetSection_QuestId(string quest_id)
    {
      QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(quest_id);
      if (quest == null || quest.Chapter == null || !HomeBgSection.IsStorySection(quest.Chapter.section))
        return;
      GlobalVars.HomeBgSection.Set(quest.Chapter.section);
    }

    private static bool IsStorySection(string section_iname)
    {
      SectionParam world = MonoSingleton<GameManager>.Instance.FindWorld(section_iname);
      return world != null && !string.IsNullOrEmpty(world.home);
    }
  }
}
