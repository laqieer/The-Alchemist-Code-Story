// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqConceptCardSkin
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/ReqConceptCardSkin", 32741)]
  [FlowNode.Pin(1, "スキン付き真理念装を取得", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "スキン付き真理念装を取得した", FlowNode.PinTypes.Output, 101)]
  public class FlowNode_ReqConceptCardSkin : FlowNode_Network
  {
    private const int PIN_INPUT_CONCEPTCARD_SKIN_GET = 1;
    private const int PIN_OUTPUT_CONCEPTCARD_SKIN_GOT = 101;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      if (GlobalVars.IsDirtySkinConceptCardData.Get())
      {
        ((Behaviour) this).enabled = true;
        this.ExecRequest((WebAPI) new ReqConceptCardSkin(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
      }
      else
        this.ActivateOutputLinks(101);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        int errCode = (int) Network.ErrCode;
        this.OnRetry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<FlowNode_ReqConceptCardSkin.Json_ConceptCardSkinList> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_ReqConceptCardSkin.Json_ConceptCardSkinList>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        try
        {
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.cards);
          GlobalVars.IsDirtySkinConceptCardData.Set(false);
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
          return;
        }
        this.StartCoroutine(this.DownloadAssetsAndOutputPin());
      }
    }

    [DebuggerHidden]
    private IEnumerator DownloadAssetsAndOutputPin()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_ReqConceptCardSkin.\u003CDownloadAssetsAndOutputPin\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    public class Json_ConceptCardSkinList
    {
      public string[] cards;
    }
  }
}
