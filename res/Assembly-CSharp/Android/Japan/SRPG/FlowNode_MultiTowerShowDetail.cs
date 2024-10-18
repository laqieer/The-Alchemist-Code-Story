// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MultiTowerShowDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("Multi/MultiTowerShowDetail", 32741)]
  [FlowNode.Pin(0, "Start", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
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
      MultiTowerFloorParam data = DataSource.FindDataOfClass<MultiTowerFloorParam>(this.gameObject, (MultiTowerFloorParam) null) ?? MonoSingleton<GameManager>.Instance.GetMTFloorParam(GlobalVars.SelectedQuestID);
      if (!((UnityEngine.Object) this.DetailObject != (UnityEngine.Object) null) || dataOfClass == null)
        return;
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.DetailObject);
      DataSource.Bind<QuestParam>(gameObject, dataOfClass, false);
      QuestCampaignData[] questCampaigns = MonoSingleton<GameManager>.Instance.FindQuestCampaigns(dataOfClass);
      DataSource.Bind<QuestCampaignData[]>(gameObject, questCampaigns.Length != 0 ? questCampaigns : (QuestCampaignData[]) null, false);
      DataSource.Bind<QuestParam>(gameObject, dataOfClass, false);
      DataSource.Bind<MultiTowerFloorParam>(gameObject, data, false);
      MultiTowerQuestInfo component = gameObject.GetComponent<MultiTowerQuestInfo>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
        return;
      component.Refresh();
    }
  }
}
