// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MultiPlayAPIDraft
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("Multi/MultiPlayAPI/Draft", 32741)]
  [FlowNode.Pin(31, "UnitList", FlowNode.PinTypes.Input, 31)]
  [FlowNode.Pin(32, "UnitSelected", FlowNode.PinTypes.Input, 32)]
  [FlowNode.Pin(33, "PartySelected", FlowNode.PinTypes.Input, 33)]
  [FlowNode.Pin(34, "SetDeck", FlowNode.PinTypes.Input, 34)]
  [FlowNode.Pin(100, "Success", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "Failure", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(5000, "NoMatchVersion", FlowNode.PinTypes.Output, 5000)]
  [FlowNode.Pin(6000, "MultiMaintenance", FlowNode.PinTypes.Output, 6000)]
  public class FlowNode_MultiPlayAPIDraft : FlowNode_Network
  {
    private const int PIN_IN_DRAFT_UNIT_LIST = 31;
    private const int PIN_IN_DRAFT_UNIT_SELECTED = 32;
    private const int PIN_IN_DRAFT_PARTY_SELECTED = 33;
    private const int PIN_IN_DRAFT_SET_DECK = 34;
    private const int PIN_OUT_SUCCESS = 100;
    private const int PIN_OUT_FAILURE = 101;
    private const int PIN_OUT_NO_MATCH_VERSION = 5000;
    private const int PIN_OUT_MULTI_MAINTENANCE = 6000;
    private FlowNode_MultiPlayAPIDraft.Proccess mProccess;

    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 31:
          this.mProccess = (FlowNode_MultiPlayAPIDraft.Proccess) new FlowNode_MultiPlayAPIDraft.Proccess_DraftUnitList();
          break;
        case 32:
          this.mProccess = (FlowNode_MultiPlayAPIDraft.Proccess) new FlowNode_MultiPlayAPIDraft.Proccess_DraftUnitSelected();
          break;
        case 33:
          this.mProccess = (FlowNode_MultiPlayAPIDraft.Proccess) new FlowNode_MultiPlayAPIDraft.Proccess_DraftParty();
          break;
        case 34:
          this.mProccess = (FlowNode_MultiPlayAPIDraft.Proccess) new FlowNode_MultiPlayAPIDraft.Proccess_DraftDeck();
          break;
      }
      if (this.mProccess == null)
        return;
      this.mProccess.SetParent(this);
      this.mProccess.Activate();
    }

    private void Success()
    {
      DebugUtility.Log("MultiPlayAPI success");
      if ((UnityEngine.Object) this == (UnityEngine.Object) null)
        return;
      this.enabled = false;
      this.ActivateOutputLinks(100);
    }

    private void Failure()
    {
      DebugUtility.Log("MultiPlayAPI failure");
      if ((UnityEngine.Object) this == (UnityEngine.Object) null)
        return;
      this.enabled = false;
      this.ActivateOutputLinks(101);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        this.OnFailed();
      }
      else
      {
        if (this.mProccess != null && !this.mProccess.Success(www))
          return;
        Network.RemoveAPI();
        this.Success();
      }
    }

    private abstract class Proccess
    {
      protected FlowNode_MultiPlayAPIDraft mParent;

      public void SetParent(FlowNode_MultiPlayAPIDraft apiDraft)
      {
        this.mParent = apiDraft;
      }

      public abstract void Activate();

      public virtual bool Success(WWWResult www)
      {
        return true;
      }
    }

    private class Proccess_DraftUnitList : FlowNode_MultiPlayAPIDraft.Proccess
    {
      public override void Activate()
      {
        this.mParent.enabled = true;
        this.mParent.ExecRequest((WebAPI) new ReqVersusDraft(GlobalVars.SelectedMultiPlayRoomName, new Network.ResponseCallback(((FlowNode_Network) this.mParent).ResponseCallback)));
      }

      public override bool Success(WWWResult www)
      {
        WebAPI.JSON_BodyResponse<ReqVersusDraft.Response> res = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqVersusDraft.Response>>(www.text);
        DebugUtility.Assert(res != null, "res == null");
        if (res.body == null)
        {
          this.mParent.OnFailed();
          return false;
        }
        GameManager instance = MonoSingleton<GameManager>.Instance;
        List<VersusDraftUnitParam> versusDraftUnits = instance.GetVersusDraftUnits(instance.VSDraftId);
        if (versusDraftUnits == null || versusDraftUnits.Count < 16)
        {
          Debug.LogError((object) "VersusDraftUnits below than 16. It needs above than 16.");
          Network.RemoveAPI();
          this.mParent.Failure();
          return false;
        }
        if (res.body.draft_units == null || res.body.draft_units.Length != 16)
        {
          Debug.LogError((object) "The number of VersusDraftUnit is not 16.");
          Network.RemoveAPI();
          this.mParent.Failure();
          return false;
        }
        VersusDraftDeckParam versusDraftDeck = instance.GetVersusDraftDeck(instance.VSDraftId);
        int num1 = versusDraftDeck == null ? 0 : versusDraftDeck.SecretMax;
        int num2 = 0;
        VersusDraftList.VersusDraftUnitList = new List<VersusDraftUnitParam>();
        for (int i = 0; i < res.body.draft_units.Length; ++i)
        {
          VersusDraftUnitParam versusDraftUnitParam = versusDraftUnits.Find((Predicate<VersusDraftUnitParam>) (vdup => vdup.DraftUnitId == res.body.draft_units[i].id));
          if (versusDraftUnitParam == null)
          {
            Debug.LogError((object) ("Selecting ID invalid: " + (object) res.body.draft_units[i].id));
            Network.RemoveAPI();
            this.mParent.Failure();
            return false;
          }
          if (res.body.draft_units[i].secret == 1)
          {
            versusDraftUnitParam.IsSecret = true;
            if (num1 <= 0 || num1 > num2)
            {
              ++num2;
              versusDraftUnitParam.IsHidden = true;
            }
            else
              versusDraftUnitParam.IsHidden = false;
          }
          else
          {
            versusDraftUnitParam.IsSecret = false;
            versusDraftUnitParam.IsHidden = false;
          }
          VersusDraftList.VersusDraftUnitList.Add(versusDraftUnitParam);
        }
        VersusDraftList.VersusDraftTurnOwn = res.body.turn_own == 1;
        return true;
      }
    }

    private class Proccess_DraftUnitSelected : FlowNode_MultiPlayAPIDraft.Proccess
    {
      public override void Activate()
      {
        this.mParent.enabled = true;
        this.mParent.ExecRequest((WebAPI) new ReqVersusDraftSelect(GlobalVars.SelectedMultiPlayRoomName, new Network.ResponseCallback(((FlowNode_Network) this.mParent).ResponseCallback)));
      }

      public override bool Success(WWWResult www)
      {
        WebAPI.JSON_BodyResponse<ReqVersusDraftSelect.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqVersusDraftSelect.Response>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          this.mParent.OnFailed();
          return false;
        }
        VersusDraftList.DraftID = jsonObject.body.draft_id;
        return true;
      }
    }

    private class Proccess_DraftParty : FlowNode_MultiPlayAPIDraft.Proccess
    {
      public override void Activate()
      {
        this.mParent.enabled = true;
        this.mParent.ExecRequest((WebAPI) new ReqVersusDraftParty(GlobalVars.SelectedMultiPlayRoomName, VersusDraftList.DraftID, new Network.ResponseCallback(((FlowNode_Network) this.mParent).ResponseCallback)));
      }
    }

    private class Proccess_DraftDeck : FlowNode_MultiPlayAPIDraft.Proccess
    {
      public override void Activate()
      {
        this.mParent.enabled = true;
        this.mParent.ExecRequest((WebAPI) new ReqVersusDraftDeck(GlobalVars.SelectedMultiPlayRoomName, MonoSingleton<GameManager>.Instance.VSDraftId, new Network.ResponseCallback(((FlowNode_Network) this.mParent).ResponseCallback)));
      }
    }
  }
}
