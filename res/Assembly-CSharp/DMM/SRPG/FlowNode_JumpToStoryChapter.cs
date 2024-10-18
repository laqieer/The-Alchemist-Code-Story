// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_JumpToStoryChapter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Story/JumpToChapter", 32741)]
  [FlowNode.Pin(0, "In", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(101, "Out", FlowNode.PinTypes.Output, 101)]
  public class FlowNode_JumpToStoryChapter : FlowNode
  {
    private const int PIN_IN_INPUT = 0;
    private const int PIN_OUT_OUTPUT = 101;
    [SerializeField]
    private FlowNode_JumpToStoryChapter.Type JumpType;
    [SerializeField]
    private eStoryPart ChapterId;

    public override void OnActivate(int pinID)
    {
      base.OnActivate(pinID);
      if (pinID != 0)
        return;
      this.Jump();
    }

    private void Jump()
    {
      WorldMapController instance = WorldMapController.Instance;
      if (Object.op_Equality((Object) instance, (Object) null))
        return;
      GlobalVars.SelectedStoryPart.Set((int) this.ChapterId);
      QuestParam lastChapterQuest = FlowNode_SelectLatestChapter.GetLastChapterQuest((int) this.ChapterId);
      if (lastChapterQuest != null)
      {
        PlayerPrefsUtility.SetString(PlayerPrefsUtility.LAST_SELECTED_STORY_QUEST_ID, lastChapterQuest.iname, true);
        GlobalVars.SelectedChapter.Set(lastChapterQuest.ChapterID);
        GlobalVars.SelectedSection.Set(lastChapterQuest.Chapter.section);
      }
      if (this.JumpType == FlowNode_JumpToStoryChapter.Type.NEWEST)
      {
        GlobalVars.SelectedQuestID = lastChapterQuest.iname;
        instance.ResetAreaAll();
        instance.AutoSelectArea = true;
        instance.Refresh();
      }
      instance.AutoSelectArea = false;
      this.ActivateOutputLinks(101);
    }

    private enum Type
    {
      START,
      NEWEST,
    }
  }
}
