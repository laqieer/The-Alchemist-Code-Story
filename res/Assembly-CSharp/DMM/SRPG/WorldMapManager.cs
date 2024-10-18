// Decompiled with JetBrains decompiler
// Type: SRPG.WorldMapManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(10, "初期化", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(20, "「章」 表示", FlowNode.PinTypes.Input, 20)]
  [FlowNode.Pin(30, "「幕」 表示", FlowNode.PinTypes.Input, 30)]
  [FlowNode.Pin(40, "「クエスト」 表示", FlowNode.PinTypes.Input, 40)]
  [FlowNode.Pin(100, "「部」選択UIを表示するべき", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(110, "「幕」を表示するべき", FlowNode.PinTypes.Output, 110)]
  [FlowNode.Pin(120, "「クエスト」を表示するべき", FlowNode.PinTypes.Output, 120)]
  public class WorldMapManager : MonoBehaviour, IFlowInterface
  {
    private const int PIN_INPUT_INIT = 10;
    private const int PIN_INPUT_DISPLAY_SECTION = 20;
    private const int PIN_INPUT_DISPLAY_CHAPTER = 30;
    private const int PIN_INPUT_DISPLAY_QUEST = 40;
    private const int PIN_OUTPUT_OPEN_SELECT_PART_UI = 100;
    private const int PIN_OUTPUT_DISPLAY_CHAPTER = 110;
    private const int PIN_OUTPUT_DISPLAY_QUEST = 120;
    private WorldMapManager.eViewType mCurrentViewType;
    [SerializeField]
    private GameObject mSectionList;
    [SerializeField]
    private GameObject mChapterList;
    [SerializeField]
    private GameObject mQuestSelector;
    private static WorldMapManager mInstance;

    public WorldMapManager.eViewType CurrentViewType => this.mCurrentViewType;

    public GameObject SectionList => this.mSectionList;

    public static WorldMapManager Instance => WorldMapManager.mInstance;

    private void Awake() => WorldMapManager.mInstance = this;

    private void OnDestroy()
    {
      GlobalEvent.Invoke("SHOW_HEADER_UI", (object) null);
      WorldMapManager.mInstance = (WorldMapManager) null;
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 10:
          this.Init();
          break;
        case 20:
          this.Disp_SectionList();
          break;
        case 30:
          this.Disp_ChapterList();
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 110);
          break;
        case 40:
          this.Disp_QuestSelector();
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 120);
          break;
      }
    }

    private void Init()
    {
      if (!this.IsSelected())
      {
        this.mCurrentViewType = WorldMapManager.eViewType.Section;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
      }
      else if (MonoSingleton<GameManager>.Instance.CheckReleaseStoryPart())
      {
        this.mCurrentViewType = WorldMapManager.eViewType.Section;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
      }
      else
      {
        WorldMapManager.eViewType eViewType = !(FlowNode_Variable.Get("SHOW_CHAPTER") == "0") ? WorldMapManager.eViewType.Chapter : WorldMapManager.eViewType.Quest;
        this.mCurrentViewType = eViewType;
        switch (eViewType)
        {
          case WorldMapManager.eViewType.Chapter:
            this.Disp_ChapterList();
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 110);
            break;
          case WorldMapManager.eViewType.Quest:
            this.Disp_QuestSelector(true);
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 120);
            break;
        }
        FlowNode_Variable.Set("SHOW_CHAPTER", "0");
      }
    }

    private void Disp_SectionList()
    {
      if (Object.op_Inequality((Object) WorldMapController.Instance, (Object) null))
        WorldMapController.Instance.GotoArea((string) null);
      QuestSectionList component = this.mSectionList.GetComponent<QuestSectionList>();
      if (Object.op_Inequality((Object) component, (Object) null))
        component.ResetScroll();
      this.mSectionList.SetActive(true);
      this.mChapterList.SetActive(false);
      this.mQuestSelector.SetActive(false);
      this.mCurrentViewType = WorldMapManager.eViewType.Section;
    }

    private void Disp_ChapterList()
    {
      QuestChapterList component = this.mChapterList.GetComponent<QuestChapterList>();
      if (Object.op_Inequality((Object) component, (Object) null))
        component.ResetScroll();
      this.mSectionList.SetActive(false);
      this.mChapterList.SetActive(true);
      this.mQuestSelector.SetActive(false);
      this.mCurrentViewType = WorldMapManager.eViewType.Chapter;
    }

    private void Disp_QuestSelector(bool is_skip_focus_anim = false)
    {
      this.mSectionList.SetActive(false);
      this.mChapterList.SetActive(false);
      this.mQuestSelector.SetActive(true);
      this.mCurrentViewType = WorldMapManager.eViewType.Quest;
      if (!is_skip_focus_anim)
        return;
      WorldMapController.Instance.ResetAreaAll();
      WorldMapController.Instance.AutoSelectArea = true;
      WorldMapController.Instance.Refresh();
    }

    private bool IsSelected()
    {
      return GlobalVars.SelectedStoryPart.Get() != 0 || !string.IsNullOrEmpty(GlobalVars.SelectedSection.Get()) || !string.IsNullOrEmpty(GlobalVars.SelectedChapter.Get());
    }

    public enum eViewType
    {
      None,
      Section,
      Chapter,
      Quest,
    }
  }
}
