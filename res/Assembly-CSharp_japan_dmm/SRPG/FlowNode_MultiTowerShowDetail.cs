// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MultiTowerShowDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Multi/MultiTowerShowDetail", 32741)]
  [FlowNode.Pin(0, "Start", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(2, "Close", FlowNode.PinTypes.Input, 2)]
  public class FlowNode_MultiTowerShowDetail : FlowNode
  {
    private const int PIN_INPUT_START = 0;
    private const int PIN_OUTPUT_SUCCESS = 1;
    private const int PIN_INPUT_CLOSE = 2;
    [SerializeField]
    private GameObject DetailObject;
    private GameObject mCreatedDetailObject;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
      {
        if (pinID != 2)
          return;
        this.Close();
      }
      else
      {
        this.OnClickDetail();
        this.ActivateOutputLinks(1);
      }
    }

    public void OnClickDetail()
    {
      if (Object.op_Inequality((Object) this.mCreatedDetailObject, (Object) null))
        return;
      QuestParam dataOfClass = DataSource.FindDataOfClass<QuestParam>(((Component) this).gameObject, (QuestParam) null);
      MultiTowerFloorParam data = DataSource.FindDataOfClass<MultiTowerFloorParam>(((Component) this).gameObject, (MultiTowerFloorParam) null) ?? MonoSingleton<GameManager>.Instance.GetMTFloorParam(GlobalVars.SelectedQuestID);
      if (!Object.op_Inequality((Object) this.DetailObject, (Object) null) || dataOfClass == null)
        return;
      this.mCreatedDetailObject = Object.Instantiate<GameObject>(this.DetailObject);
      QuestCampaignData[] questCampaigns = MonoSingleton<GameManager>.Instance.FindQuestCampaigns(dataOfClass);
      DataSource.Bind<QuestCampaignData[]>(this.mCreatedDetailObject, questCampaigns.Length != 0 ? questCampaigns : (QuestCampaignData[]) null);
      DataSource.Bind<QuestParam>(this.mCreatedDetailObject, dataOfClass);
      DataSource.Bind<MultiTowerFloorParam>(this.mCreatedDetailObject, data);
      MultiTowerQuestInfo component = this.mCreatedDetailObject.GetComponent<MultiTowerQuestInfo>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      component.Refresh();
    }

    private void Close()
    {
      if (!Object.op_Inequality((Object) this.mCreatedDetailObject, (Object) null))
        return;
      Object.Destroy((Object) this.mCreatedDetailObject);
    }
  }
}
