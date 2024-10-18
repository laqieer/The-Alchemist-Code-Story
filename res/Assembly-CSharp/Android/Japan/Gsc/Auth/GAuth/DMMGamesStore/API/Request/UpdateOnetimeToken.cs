// Decompiled with JetBrains decompiler
// Type: Gsc.Auth.GAuth.DMMGamesStore.API.Request.UpdateOnetimeToken
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
      Dictionary<string, object> dictionary1 = new Dictionary<string, object>();
      Dictionary<string, object> dictionary2 = dictionary1;
      string index1 = "dmm_viewer_id";
      Serializer instance1 = Serializer.Instance;
      // ISSUE: reference to a compiler-generated field
      if (UpdateOnetimeToken.\u003C\u003Ef__mg\u0024cache0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        UpdateOnetimeToken.\u003C\u003Ef__mg\u0024cache0 = new Func<int, object>(Serializer.From<int>);
      }
      // ISSUE: reference to a compiler-generated field
      Func<int, object> fMgCache0 = UpdateOnetimeToken.\u003C\u003Ef__mg\u0024cache0;
      object obj1 = instance1.Add<int>(fMgCache0).Serialize<int>(this.ViewerID);
      dictionary2[index1] = obj1;
      Dictionary<string, object> dictionary3 = dictionary1;
      string index2 = "dmm_onetime_token";
      Serializer instance2 = Serializer.Instance;
      // ISSUE: reference to a compiler-generated field
      if (UpdateOnetimeToken.\u003C\u003Ef__mg\u0024cache1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        UpdateOnetimeToken.\u003C\u003Ef__mg\u0024cache1 = new Func<string, object>(Serializer.From<string>);
      }
      // ISSUE: reference to a compiler-generated field
      Func<string, object> fMgCache1 = UpdateOnetimeToken.\u003C\u003Ef__mg\u0024cache1;
      object obj2 = instance2.Add<string>(fMgCache1).Serialize<string>(this.OnetimeToken);
      dictionary3[index2] = obj2;
      return dictionary1;
    }
  }
}
