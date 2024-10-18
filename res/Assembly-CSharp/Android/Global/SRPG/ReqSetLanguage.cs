// Decompiled with JetBrains decompiler
// Type: SRPG.ReqSetLanguage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqSetLanguage : WebAPI
  {
    public ReqSetLanguage(string language, Network.ResponseCallback response)
    {
      string isO639 = GameUtility.ConvertLanguageToISO639(language);
      this.name = "setlanguage";
      this.body = WebAPI.GetRequestString("\"lang\":\"" + isO639 + "\"");
      this.callback = response;
    }
  }
}
