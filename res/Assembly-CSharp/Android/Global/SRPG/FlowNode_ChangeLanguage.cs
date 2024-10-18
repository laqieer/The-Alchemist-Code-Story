// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ChangeLanguage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(4, "Change To German", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(10, "Finished", FlowNode.PinTypes.Output, 7)]
  [FlowNode.Pin(5, "Change To Spanish", FlowNode.PinTypes.Input, 4)]
  [FlowNode.Pin(102, "Send API Set Language", FlowNode.PinTypes.Input, 6)]
  [FlowNode.Pin(1, "Change To English", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(2, "Change To Japanese", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "Change To System Language", FlowNode.PinTypes.Input, 5)]
  [FlowNode.Pin(3, "Change To French", FlowNode.PinTypes.Input, 2)]
  [FlowNode.NodeType("Localization/ChangeLanguage", 32741)]
  public class FlowNode_ChangeLanguage : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      string str = "english";
      switch (pinID)
      {
        case 1:
          str = "english";
          break;
        case 2:
          str = "japanese";
          break;
        case 3:
          str = "french";
          break;
        case 4:
          str = "german";
          break;
        case 5:
          str = "spanish";
          break;
        case 101:
          SystemLanguage systemLanguage = Application.systemLanguage;
          switch (systemLanguage)
          {
            case SystemLanguage.English:
              str = "english";
              break;
            case SystemLanguage.French:
              str = "french";
              break;
            case SystemLanguage.German:
              str = "german";
              break;
            default:
              if (systemLanguage == SystemLanguage.Spanish)
              {
                str = "spanish";
                break;
              }
              break;
          }
      }
      if (pinID == 102)
      {
        this.ExecRequest((WebAPI) new ReqSetLanguage(GameUtility.Config_Language, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
      }
      else
      {
        GameUtility.Config_Language = str;
        LocalizedText.LanguageCode = str;
        LocalizedText.UnloadAll();
        this.ActivateOutputLinks(10);
      }
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        this.OnRetry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          this.OnRetry();
        }
        else
        {
          Network.RemoveAPI();
          this.ActivateOutputLinks(10);
        }
      }
    }
  }
}
