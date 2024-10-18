// Decompiled with JetBrains decompiler
// Type: SRPG.GenesisChapterSelector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "セレクト開始", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "セレクトした", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "期間外をセレクト", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(103, "未解放をセレクト", FlowNode.PinTypes.Output, 103)]
  public class GenesisChapterSelector : MonoBehaviour, IFlowInterface
  {
    [SerializeField]
    private GameObject Window;
    [Space(5f)]
    [SerializeField]
    private SRPG_ScrollRect ScrollRectController;
    [SerializeField]
    private GameObject GoSelParent;
    [SerializeField]
    private GenesisChapterSelectorItem SelBaseItem;
    [Space(5f)]
    [SerializeField]
    private bool IsSortUpper;
    private const int PIN_IN_START = 1;
    private const int PIN_OUT_SELECTED = 101;
    private const int PIN_OUT_OUT_OF_PERIOD = 102;
    private const int PIN_OUT_NO_LIBERATION = 103;
    private List<GenesisChapterSelectorItem> mSelectList = new List<GenesisChapterSelectorItem>();

    private void Awake()
    {
      if (!Object.op_Implicit((Object) this.Window))
        return;
      this.Window.SetActive(false);
    }

    private void Init()
    {
      ((Component) this).gameObject.SetActive(true);
      GlobalVars.SelectedQuestID = string.Empty;
      if (Object.op_Implicit((Object) this.GoSelParent) && Object.op_Implicit((Object) this.SelBaseItem))
      {
        ((Component) this.SelBaseItem).gameObject.SetActive(false);
        BattleUnitDetail.DestroyChildGameObjects<GenesisChapterSelectorItem>(this.GoSelParent, new List<GameObject>((IEnumerable<GameObject>) new GameObject[1]
        {
          ((Component) this.SelBaseItem).gameObject
        }));
      }
      if (Object.op_Implicit((Object) this.ScrollRectController))
        this.ScrollRectController.ResetVerticalPosition();
      this.mSelectList.Clear();
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (!Object.op_Implicit((Object) instance) || instance.GenesisChapterParamList == null || instance.GenesisChapterParamList.Count == 0)
        return;
      List<GenesisChapterParam> genesisChapterParamList = new List<GenesisChapterParam>((IEnumerable<GenesisChapterParam>) instance.GenesisChapterParamList);
      if (!this.IsSortUpper)
        genesisChapterParamList.Reverse();
      List<QuestParam>[] questParamListArray = new List<QuestParam>[genesisChapterParamList.Count];
      for (int index = 0; index < genesisChapterParamList.Count; ++index)
      {
        GenesisChapterParam genesisChapterParam = genesisChapterParamList[index];
        questParamListArray[index] = genesisChapterParam.GetQuestList();
      }
      for (int index = 0; index < genesisChapterParamList.Count; ++index)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        GenesisChapterSelector.\u003CInit\u003Ec__AnonStorey0 initCAnonStorey0 = new GenesisChapterSelector.\u003CInit\u003Ec__AnonStorey0();
        // ISSUE: reference to a compiler-generated field
        initCAnonStorey0.\u0024this = this;
        GenesisChapterParam chapter_param = genesisChapterParamList[index];
        // ISSUE: reference to a compiler-generated field
        initCAnonStorey0.item = Object.Instantiate<GenesisChapterSelectorItem>(this.SelBaseItem);
        // ISSUE: reference to a compiler-generated field
        if (Object.op_Implicit((Object) initCAnonStorey0.item))
        {
          // ISSUE: reference to a compiler-generated field
          ((Component) initCAnonStorey0.item).transform.SetParent(this.GoSelParent.transform);
          // ISSUE: reference to a compiler-generated field
          ((Component) initCAnonStorey0.item).transform.localScale = ((Component) this.SelBaseItem).transform.localScale;
          bool is_out_of_period = questParamListArray[index].Count == 0;
          bool is_liberation = false;
          if (questParamListArray[index].Count != 0)
            is_liberation = questParamListArray[index][0].IsQuestCondition();
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          initCAnonStorey0.item.SetItem(chapter_param, new UnityAction((object) initCAnonStorey0, __methodptr(\u003C\u003Em__0)), is_out_of_period, is_liberation);
          // ISSUE: reference to a compiler-generated field
          ((Component) initCAnonStorey0.item).gameObject.SetActive(true);
        }
      }
      if (!Object.op_Implicit((Object) this.Window))
        return;
      this.Window.SetActive(true);
    }

    private void OnSelectItem(GenesisChapterSelectorItem item)
    {
      if (!Object.op_Implicit((Object) item))
        return;
      GenesisManager.CurrentChapterParam = item.ChapterParam;
      if (item.IsOutOfPeriod)
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 102);
      else if (!item.IsLiberation)
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 103);
      else
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
    }

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      this.Init();
    }
  }
}
