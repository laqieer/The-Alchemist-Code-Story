﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqTrophyAchievedQuest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.NodeType("Trophy/ReqAchievedQuest", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(2, "NoTrophy", FlowNode.PinTypes.Output, 2)]
  public class FlowNode_ReqTrophyAchievedQuest : FlowNode_Network
  {
    public Text Quests;
    public Toggle toggle;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      TrophyParam dataOfClass = DataSource.FindDataOfClass<TrophyParam>(this.gameObject, (TrophyParam) null);
      if (dataOfClass == null || !dataOfClass.Objectives[0].type.IsExtraClear())
      {
        this.toggle.isOn = true;
        this.ActivateOutputLinks(2);
      }
      else
        this.ExecRequest((WebAPI) new ReqTrophyAchievedQuest(dataOfClass.iname, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
        return;
      WebAPI.JSON_BodyResponse<TrophyQuests> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<TrophyQuests>>(www.text);
      if ((UnityEngine.Object) this.Quests != (UnityEngine.Object) null)
      {
        string[] histories = jsonObject.body.histories;
        if (histories == null || histories.Length <= 0)
        {
          this.Quests.text = LocalizedText.Get("sys.TROPHY_NOT_ACHIEVEDQUEST");
          Network.RemoveAPI();
          this.ActivateOutputLinks(1);
          return;
        }
        GameManager instance = MonoSingleton<GameManager>.Instance;
        for (int index = 0; index < histories.Length; ++index)
        {
          QuestParam quest = instance.FindQuest(histories[index]);
          if (quest != null)
          {
            Text quests = this.Quests;
            quests.text = quests.text + quest.name + "\n";
          }
        }
      }
      Network.RemoveAPI();
      this.ActivateOutputLinks(1);
    }
  }
}
