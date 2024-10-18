// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MultiTowerShowDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(0, "Start", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("Multi/MultiTowerShowDetail", 32741)]
  public class FlowNode_MultiTowerShowDetail : FlowNode
  {
    [SerializeField]
    private GameObject DetailObject;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      this.OnClickDetail();
      this.ActivateOutputLinks(1);
    }

    public void OnClickDetail()
    {
      QuestParam dataOfClass = DataSource.FindDataOfClass<QuestParam>(this.gameObject, (QuestParam) null);
      MultiTowerFloorParam data = DataSource.FindDataOfClass<MultiTowerFloorParam>(this.gameObject, (MultiTowerFloorParam) null) ?? MonoSingleton<GameManager>.Instance.GetMTFloorParam(GlobalVars.SelectedMultiTowerID, GlobalVars.SelectedMultiTowerFloor);
      if (!((UnityEngine.Object) this.DetailObject != (UnityEngine.Object) null) || dataOfClass == null)
        return;
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.DetailObject);
      DataSource.Bind<QuestParam>(gameObject, dataOfClass);
      QuestCampaignData[] questCampaigns = MonoSingleton<GameManager>.Instance.FindQuestCampaigns(dataOfClass);
      DataSource.Bind<QuestCampaignData[]>(gameObject, questCampaigns.Length != 0 ? questCampaigns : (QuestCampaignData[]) null);
      DataSource.Bind<QuestParam>(gameObject, dataOfClass);
      DataSource.Bind<MultiTowerFloorParam>(gameObject, data);
      MultiTowerQuestInfo component = gameObject.GetComponent<MultiTowerQuestInfo>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
        return;
      component.Refresh();
    }
  }
}
