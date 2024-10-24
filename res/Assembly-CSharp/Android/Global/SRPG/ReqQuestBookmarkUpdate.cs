﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ReqQuestBookmarkUpdate
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SRPG
{
  public class ReqQuestBookmarkUpdate : WebAPI
  {
    public ReqQuestBookmarkUpdate(IEnumerable<string> add, IEnumerable<string> delete, Network.ResponseCallback response)
    {
      this.name = "quest/favorite/set";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"inames\":{");
      if (delete != null && delete.Count<string>() > 0)
      {
        stringBuilder.Append("\"del\":[");
        foreach (string str in delete)
        {
          stringBuilder.Append("\"");
          stringBuilder.Append(str);
          stringBuilder.Append("\",");
        }
        stringBuilder.Remove(stringBuilder.Length - 1, 1);
        stringBuilder.Append("]");
      }
      if (add != null && add.Count<string>() > 0)
      {
        if (delete != null && delete.Count<string>() > 0)
          stringBuilder.Append(",");
        stringBuilder.Append("\"add\":[");
        foreach (string str in add)
        {
          stringBuilder.Append("\"");
          stringBuilder.Append(str);
          stringBuilder.Append("\",");
        }
        stringBuilder.Remove(stringBuilder.Length - 1, 1);
        stringBuilder.Append("]");
      }
      stringBuilder.Append("}");
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
