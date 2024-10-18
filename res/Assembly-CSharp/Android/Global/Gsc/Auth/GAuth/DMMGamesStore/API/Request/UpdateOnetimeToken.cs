// Decompiled with JetBrains decompiler
// Type: Gsc.Auth.GAuth.DMMGamesStore.API.Request.UpdateOnetimeToken
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using Gsc.Network.Support.MiniJsonHelper;
using System;
using System.Collections.Generic;

namespace Gsc.Auth.GAuth.DMMGamesStore.API.Request
{
  public class UpdateOnetimeToken : Gsc.Network.Request<UpdateOnetimeToken, UpdateOnetimeTokenResponse>
  {
    private const string ___path = "{0}/dmm-auth-proxy/{1}/update_onetime_token";

    public UpdateOnetimeToken(int viewerId, string onetimeToken)
    {
      this.ViewerID = viewerId;
      this.OnetimeToken = onetimeToken;
    }

    public int ViewerID { get; set; }

    public string OnetimeToken { get; set; }

    public override string GetUrl()
    {
      return string.Format("{0}/dmm-auth-proxy/{1}/update_onetime_token", (object) SDK.Configuration.Env.NativeBaseUrl, (object) SDK.Configuration.AppName);
    }

    public override string GetPath()
    {
      return "{0}/dmm-auth-proxy/{1}/update_onetime_token";
    }

    public override string GetMethod()
    {
      return "POST";
    }

    protected override Dictionary<string, object> GetParameters()
    {
      return new Dictionary<string, object>()
      {
        ["dmm_viewer_id"] = Serializer.Instance.Add<int>(new Func<int, object>(Serializer.From<int>)).Serialize<int>(this.ViewerID),
        ["dmm_onetime_token"] = Serializer.Instance.Add<string>(new Func<string, object>(Serializer.From<string>)).Serialize<string>(this.OnetimeToken)
      };
    }
  }
}
