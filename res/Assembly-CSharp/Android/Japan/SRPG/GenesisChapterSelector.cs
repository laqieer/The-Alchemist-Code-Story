// Decompiled with JetBrains decompiler
// Type: SRPG.GenesisChapterSelector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SRPG
{
  [FlowNode.Pin(1, "セレクト開始", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "セレクトした", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "期間外をセレクト", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(103, "未解放をセレクト", FlowNode.PinTypes.Output, 103)]
  public class GenesisChapterSelector : MonoBehaviour, IFlowInterface
  {
    private List<GenesisChapterSelectorItem> mSelectList = new List<GenesisChapterSelectorItem>();
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

    private void Awake()
    {
      if (!(bool) ((UnityEngine.Object) this.Window))
        return;
      this.Window.SetActive(false);
    }

    private void Init()
    {
      this.gameObject.SetActive(true);
      if ((bool) ((UnityEngine.Object) this.GoSelParent) && (bool) ((UnityEngine.Object) this.SelBaseItem))
      {
        this.SelBaseItem.gameObject.SetActive(false);
        BattleUnitDetail.DestroyChildGameObjects<GenesisChapterSelectorItem>(this.GoSelParent, new List<GameObject>((IEnumerable<GameObject>) new GameObject[1]
        {
          this.SelBaseItem.gameObject
        }));
      }
      if ((bool) ((UnityEngine.Object) this.ScrollRectController))
        this.ScrollRectController.ResetVerticalPosition();
      this.mSelectList.Clear();
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (!(bool) ((UnityEngine.Object) instance) || instance.GenesisChapterParamList == null || instance.GenesisChapterParamList.Count == 0)
        return;
      List<GenesisChapterParam> genesisChapterParamList = new List<GenesisChapterParam>((IEnumerable<GenesisChapterParam>) instance.GenesisChapterParamList);
      if (!this.IsSortUpper)
        genesisChapterParamList.Reverse();
      List<QuestParam>[] questParamListArray = new List<QuestParam>[genesisChapterParamList.Count];
      for (int index = 0; index < genesisChapterParamList.Count; ++index)
      {
        GenesisChapterParam genesisChapterParam = genesisChapterParamList[index];
        questParamListArray[index] = genesisChapterParam.GetQuestList(QuestDifficulties.MAX, false);
      }
      for (int index = 0; index < genesisChapterParamList.Count; ++index)
      {
        GenesisChapterParam chapter_param = genesisChapterParamList[index];
        GenesisChapterSelectorItem item = UnityEngine.Object.Instantiate<GenesisChapterSelectorItem>(this.SelBaseItem);
        if ((bool) ((UnityEngine.Object) item))
        {
          item.transform.SetParent(this.GoSelParent.transform);
          item.transform.localScale = this.SelBaseItem.transform.localScale;
          bool is_out_of_period = questParamListArray[index].Count == 0;
          bool is_liberation = false;
          if (questParamListArray[index].Count != 0)
            is_liberation = questParamListArray[index][0].IsQuestCondition();
          item.SetItem(chapter_param, (UnityAction) (() => this.OnSelectItem(item)), is_out_of_period, is_liberation);
          item.gameObject.SetActive(true);
        }
      }
      if (!(bool) ((UnityEngine.Object) this.Window))
        return;
      this.Window.SetActive(true);
    }

    private void OnSelectItem(GenesisChapterSelectorItem item)
    {
      if (!(bool) ((UnityEngine.Object) item))
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
