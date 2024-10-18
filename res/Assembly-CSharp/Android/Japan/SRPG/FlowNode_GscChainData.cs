﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_GscChainData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using Gsc.Auth;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.NodeType("GscSystem/GscChainData", 32741)]
  [FlowNode.Pin(0, "Register", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "AddUser", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(10, "Success", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(11, "Failed", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(12, "RegistEmail", FlowNode.PinTypes.Output, 12)]
  [FlowNode.Pin(13, "RegistPassword", FlowNode.PinTypes.Output, 13)]
  [FlowNode.Pin(14, "RegistDuplicate", FlowNode.PinTypes.Output, 14)]
  [FlowNode.Pin(20, "ChainMissing", FlowNode.PinTypes.Output, 20)]
  [FlowNode.Pin(21, "ChainEmail", FlowNode.PinTypes.Output, 21)]
  [FlowNode.Pin(22, "ChainLocked", FlowNode.PinTypes.Output, 22)]
  [FlowNode.Pin(100, "Requested", FlowNode.PinTypes.Output, 100)]
  public class FlowNode_GscChainData : FlowNode
  {
    private const int PIN_REGISTER = 0;
    private const int PIN_ADD_USER = 1;
    private const int PIN_SUCCESS = 10;
    private const int PIN_FAILED = 11;
    private const int PIN_REG_EMAIL = 12;
    private const int PIN_REG_PASSWORD = 13;
    private const int PIN_REG_DUPLICATE = 14;
    private const int PIN_CHN_MISSING = 20;
    private const int PIN_CHN_EMAIL = 21;
    private const int PIN_CHN_LOCKED = 22;
    private const int PIN_CHN_IDPASS = 23;
    private const int PIN_REQUESTED = 100;
    [SerializeField]
    private Text okyakusama_code_txt;
    [SerializeField]
    private Text pass_txt;

    public override void OnActivate(int pinID)
    {
      string email = string.Empty;
      string password = string.Empty;
      if ((UnityEngine.Object) this.okyakusama_code_txt != (UnityEngine.Object) null)
        email = this.okyakusama_code_txt.text;
      if ((UnityEngine.Object) this.pass_txt != (UnityEngine.Object) null)
        password = this.pass_txt.text;
      if (pinID == 0)
      {
        Session.DefaultSession.RegisterEmailAddressAndPassword(email, password, true, new Action<RegisterEmailAddressAndPasswordResult>(this.RegistResponse));
        this.ActivateOutputLinks(100);
      }
      else
      {
        if (pinID != 1)
          return;
        Session.DefaultSession.AddDeviceWithEmailAddressAndPassword(email, password, new Action<AddDeviceWithEmailAddressAndPasswordResult>(this.AddUserResponse));
        this.ActivateOutputLinks(100);
      }
    }

    private void RegistResponse(RegisterEmailAddressAndPasswordResult res)
    {
      switch (res.ResultCode)
      {
        case RegisterEmailAddressAndPasswordResultCode.Success:
          this.ActivateOutputLinks(10);
          break;
        case RegisterEmailAddressAndPasswordResultCode.InvalidEmailAddress:
          this.ActivateOutputLinks(12);
          break;
        case RegisterEmailAddressAndPasswordResultCode.InvalidPassword:
          this.ActivateOutputLinks(13);
          break;
        case RegisterEmailAddressAndPasswordResultCode.DuplicatedEmailAddress:
          this.ActivateOutputLinks(14);
          break;
        default:
          this.ActivateOutputLinks(11);
          break;
      }
    }

    private void AddUserResponse(AddDeviceWithEmailAddressAndPasswordResult res)
    {
      switch (res.ResultCode)
      {
        case AddDeviceWithEmailAddressAndPasswordResultCode.Success:
          GameUtility.ClearPreferences();
          this.ActivateOutputLinks(10);
          break;
        case AddDeviceWithEmailAddressAndPasswordResultCode.MissingDeviceId:
          this.ActivateOutputLinks(20);
          break;
        case AddDeviceWithEmailAddressAndPasswordResultCode.MissingEmailOrPassword:
          this.ActivateOutputLinks(21);
          break;
        case AddDeviceWithEmailAddressAndPasswordResultCode.Locked:
          DateTime dateTime = TimeManager.ServerTime.AddSeconds((double) res.LockedExpiresIn);
          string msg = LocalizedText.Get("sys.CHAINDATA_LOCKED", new object[1]{ (object) string.Format("{0}/{1:D2}/{2:D2} {3:D2}:{4:D2}", (object) dateTime.Year, (object) dateTime.Month, (object) dateTime.Day, (object) dateTime.Hour, (object) dateTime.Minute) });
          UIUtility.NegativeSystemMessage(string.Empty, msg, (UIUtility.DialogResultEvent) (obj => this.ActivateOutputLinks(22)), (GameObject) null, true, -1);
          break;
        default:
          this.ActivateOutputLinks(11);
          break;
      }
    }
  }
}
