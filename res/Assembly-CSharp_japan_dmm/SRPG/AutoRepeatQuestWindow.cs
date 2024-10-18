// Decompiled with JetBrains decompiler
// Type: SRPG.AutoRepeatQuestWindow
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
  [FlowNode.Pin(20, "自動周回を終了", FlowNode.PinTypes.Input, 20)]
  [FlowNode.Pin(100, "中断確認", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(200, "完了済み", FlowNode.PinTypes.Output, 200)]
  public class AutoRepeatQuestWindow : MonoBehaviour, IFlowInterface
  {
    private const int PIN_INPUT_INIT = 10;
    private const int PIN_INPUT_FINISH = 20;
    private const int PIN_OUTPUT_CONFIRM_SUSPEND = 100;
    private const int PIN_OUTPUT_COMPLATE = 200;
    [SerializeField]
    private ContentController mContentController;

    public void Activated(int pinID)
    {
      if (pinID != 10)
      {
        if (pinID != 20)
          return;
        this.Finish();
      }
      else
        this.Init();
    }

    private void Awake() => this.mContentController.SetWork((object) this);

    private void Init()
    {
      List<Unit.DropItem> dropItem = MonoSingleton<GameManager>.Instance.Player.AutoRepeatQuestProgress.GetDropItem();
      List<QuestResult.DropItemData> dropItemDataList = new List<QuestResult.DropItemData>();
      for (int index = 0; index < dropItem.Count; ++index)
      {
        QuestResult.DropItemData dropItemData = new QuestResult.DropItemData();
        if (dropItem[index].isItem)
          dropItemData.SetupDropItemData(dropItem[index].BattleRewardType, 0L, dropItem[index].itemParam.iname, (int) dropItem[index].num);
        else if (dropItem[index].isConceptCard)
          dropItemData.SetupDropItemData(dropItem[index].BattleRewardType, 0L, dropItem[index].conceptCardParam.iname, (int) dropItem[index].num);
        dropItemData.mIsSecret = (bool) dropItem[index].is_secret;
        dropItemDataList.Add(dropItemData);
      }
      List<DropItemSource.DropItemParam> dropItemParamList = new List<DropItemSource.DropItemParam>();
      for (int index = 0; index < dropItemDataList.Count; ++index)
        dropItemParamList.Add(new DropItemSource.DropItemParam(dropItemDataList[index]));
      DropItemSource source = new DropItemSource();
      source.SetTable((ContentSource.Param[]) dropItemParamList.ToArray());
      this.mContentController.Initialize((ContentSource) source, Vector2.zero);
    }

    private void Finish() => FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
  }
}
