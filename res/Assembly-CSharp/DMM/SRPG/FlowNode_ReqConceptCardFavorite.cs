﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqConceptCardFavorite
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("ConceptCard/Req/ReqConceptCardFavorite", 32741)]
  [FlowNode.Pin(100, "お気に入りON", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(110, "お気に入りOFF", FlowNode.PinTypes.Input, 110)]
  [FlowNode.Pin(1000, "お気に入り設定ON完了", FlowNode.PinTypes.Output, 1000)]
  [FlowNode.Pin(1010, "お気に入り設定OFF完了", FlowNode.PinTypes.Output, 1010)]
  public class FlowNode_ReqConceptCardFavorite : FlowNode_Network
  {
    private const int INPUT_FAVORITE_ON = 100;
    private const int INPUT_FAVORITE_OFF = 110;
    private const int OUTPUT_FAVORITE_ON = 1000;
    private const int OUTPUT_FAVORITE_OFF = 1010;
    private int mOutPutPinId;

    public override void OnActivate(int pinID)
    {
      ConceptCardManager instance = ConceptCardManager.Instance;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance, (UnityEngine.Object) null))
        return;
      switch (pinID)
      {
        case 100:
          this.ExecRequest((WebAPI) new ReqFavoriteConceptCard((long) instance.SelectedConceptCardData.UniqueID, true, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          this.mOutPutPinId = 1000;
          break;
        case 110:
          this.ExecRequest((WebAPI) new ReqFavoriteConceptCard((long) instance.SelectedConceptCardData.UniqueID, false, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          this.mOutPutPinId = 1010;
          break;
      }
      ((Behaviour) this).enabled = true;
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
        WebAPI.JSON_BodyResponse<FlowNode_ReqConceptCardFavorite.Json_ConceptCardFavorite> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_ReqConceptCardFavorite.Json_ConceptCardFavorite>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        try
        {
          MonoSingleton<GameManager>.Instance.Player.Deserialize(jsonObject.body.concept_card);
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
          return;
        }
        this.ActivateOutputLinks(this.mOutPutPinId);
        ((Behaviour) this).enabled = false;
      }
    }

    public class Json_ConceptCardFavorite
    {
      public JSON_ConceptCard concept_card;
    }
  }
}
