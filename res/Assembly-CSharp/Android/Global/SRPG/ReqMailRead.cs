﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ReqMailRead
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqMailRead : WebAPI
  {
    public ReqMailRead(long[] mailids, bool period, int page, Network.ResponseCallback response)
    {
      this.name = "mail/read";
      this.body = "\"mailids\":[";
      for (int index = 0; index < mailids.Length; ++index)
      {
        this.body += mailids[index].ToString();
        if (index != mailids.Length - 1)
          this.body += ",";
      }
      this.body += "],";
      this.body += "\"page\":";
      this.body += (string) (object) page;
      this.body += ",";
      this.body += "\"period\":";
      this.body += (string) (object) (!period ? 0 : 1);
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }

    public ReqMailRead(long[] mailids, int[] periods, int page, Network.ResponseCallback response)
    {
      this.name = "mail/read";
      this.body = "\"mailids\":[";
      for (int index = 0; index < mailids.Length; ++index)
      {
        this.body += mailids[index].ToString();
        if (index != mailids.Length - 1)
          this.body += ",";
      }
      this.body += "],";
      this.body += "\"page\":";
      this.body += (string) (object) page;
      this.body += ",";
      this.body += "\"period\":";
      this.body += (string) (object) periods[0];
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }

    public ReqMailRead(long mailid, bool period, int page, string iname, Network.ResponseCallback response)
    {
      this.name = "mail/read";
      this.body = "\"mailids\":[";
      this.body += mailid.ToString();
      this.body += "],";
      this.body += "\"page\":";
      this.body += (string) (object) page;
      this.body += ",";
      this.body += "\"period\":";
      this.body += (string) (object) (!period ? 0 : 1);
      this.body += ",";
      this.body += "\"selected\":\"";
      this.body += iname;
      this.body += "\"";
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }
  }
}
