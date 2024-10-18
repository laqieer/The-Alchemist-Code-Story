// Decompiled with JetBrains decompiler
// Type: SRPG.StoryPartSelectWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(10, "初期化", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(20, "1部を選択", FlowNode.PinTypes.Input, 20)]
  [FlowNode.Pin(30, "2部を選択", FlowNode.PinTypes.Input, 30)]
  [FlowNode.Pin(40, "幕間を選択", FlowNode.PinTypes.Input, 40)]
  [FlowNode.Pin(100, "1部を表示", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(110, "2部を表示", FlowNode.PinTypes.Output, 110)]
  [FlowNode.Pin(120, "幕間を表示", FlowNode.PinTypes.Output, 120)]
  [FlowNode.Pin(129, "アンロック演出開始", FlowNode.PinTypes.Output, 129)]
  [FlowNode.Pin(130, "アンロック演出終了", FlowNode.PinTypes.Output, 130)]
  public class StoryPartSelectWindow : MonoBehaviour, IFlowInterface
  {
    private const int PIN_INPUT_INIT = 10;
    private const int PIN_INPUT_SELECT_PART1 = 20;
    private const int PIN_INPUT_SELECT_PART2 = 30;
    private const int PIN_INPUT_SELECT_MAKUAI = 40;
    private const int PIN_OUTPUT_SELECT_PART1 = 100;
    private const int PIN_OUTPUT_SELECT_PART2 = 110;
    private const int PIN_OUTPUT_SELECT_MAKUAI = 120;
    private const int PIN_OUTPUT_UNLOCK_ANIMATION_START = 129;
    private const int PIN_OUTPUT_UNLOCK_ANIMATION_END = 130;
    [SerializeField]
    private StoryPartIcon mPartIcon_1;
    [SerializeField]
    private StoryPartIcon mPartIcon_2;
    [SerializeField]
    private StoryPartIcon mPartIcon_Makuai;
    [SerializeField]
    private GameObject mCloseButton;
    private Dictionary<eStoryPart, bool> mUnlockStates;
    private eStoryPart mNeedPlayUnlockAnim;

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 10:
          this.Init();
          break;
        case 20:
          this.OnClickPartButton(eStoryPart.Part1);
          break;
        case 30:
          this.OnClickPartButton(eStoryPart.Part2);
          break;
        case 40:
          this.OnClickPartButton(eStoryPart.Makuai);
          break;
      }
    }

    private void Init()
    {
      if (Object.op_Inequality((Object) WorldMapManager.Instance, (Object) null) && Object.op_Inequality((Object) this.mCloseButton, (Object) null))
        this.mCloseButton.SetActive(WorldMapManager.Instance.CurrentViewType != WorldMapManager.eViewType.Section);
      this.mNeedPlayUnlockAnim = MonoSingleton<GameManager>.Instance.GetNeedReleaseAnimationStoryPart();
      this.mUnlockStates = MonoSingleton<GameManager>.Instance.GetUnlockStoryPartStates();
      bool mUnlockState1 = this.mUnlockStates[eStoryPart.Part1];
      bool mUnlockState2 = this.mUnlockStates[eStoryPart.Part2];
      bool mUnlockState3 = this.mUnlockStates[eStoryPart.Makuai];
      this.mPartIcon_1.Setup(mUnlockState1, eStoryPart.Part1, this.mNeedPlayUnlockAnim == eStoryPart.Part1);
      this.mPartIcon_2.Setup(mUnlockState2, eStoryPart.Part2, this.mNeedPlayUnlockAnim == eStoryPart.Part2);
      this.mPartIcon_Makuai.Setup(mUnlockState3, eStoryPart.Makuai, this.mNeedPlayUnlockAnim == eStoryPart.Makuai);
      StoryPartIcon storyPartIcon = (StoryPartIcon) null;
      switch (this.mNeedPlayUnlockAnim)
      {
        case eStoryPart.Part1:
          storyPartIcon = this.mPartIcon_1;
          break;
        case eStoryPart.Part2:
          storyPartIcon = this.mPartIcon_2;
          break;
        case eStoryPart.Makuai:
          storyPartIcon = this.mPartIcon_Makuai;
          break;
      }
      if (!Object.op_Inequality((Object) storyPartIcon, (Object) null))
        return;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 129);
      storyPartIcon.PlayUnlockAnim(new StoryPartIcon.EndUnlockAnimStoryPart(this.OnEndUnlockAnimation));
    }

    private void OnClickPartButton(eStoryPart part)
    {
      if (!this.mUnlockStates[part])
      {
        UIUtility.SystemMessage((string) null, MonoSingleton<GameManager>.Instance.GetUnlockConditionsStoryPartMessage(part), (UIUtility.DialogResultEvent) null);
      }
      else
      {
        if (Object.op_Inequality((Object) WorldMapManager.Instance, (Object) null) && Object.op_Inequality((Object) WorldMapManager.Instance.SectionList, (Object) null))
        {
          QuestSectionList component = WorldMapManager.Instance.SectionList.GetComponent<QuestSectionList>();
          if (Object.op_Inequality((Object) component, (Object) null))
            component.ResetScroll();
        }
        int pinID = 100;
        switch (part)
        {
          case eStoryPart.Part1:
            pinID = 100;
            break;
          case eStoryPart.Part2:
            pinID = 110;
            break;
          case eStoryPart.Makuai:
            pinID = 120;
            break;
        }
        FlowNode_GameObject.ActivateOutputLinks((Component) this, pinID);
      }
    }

    private void OnEndUnlockAnimation()
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 130);
      PlayerPrefsUtility.SetString(PlayerPrefsUtility.RELEASE_STORY_PART_KEY + (object) (int) this.mNeedPlayUnlockAnim, "1", true);
    }
  }
}
