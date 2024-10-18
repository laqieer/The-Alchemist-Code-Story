﻿// Decompiled with JetBrains decompiler
// Type: SRPG.MailPageData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

namespace SRPG
{
  public class MailPageData
  {
    public List<MailData> mails;
    public bool hasNext;
    public bool hasPrev;
    public int page;
    public int pageMax;
    public int mailCount;

    public void Deserialize(Json_Mail[] mailArray)
    {
      if (this.mails == null)
        this.mails = new List<MailData>();
      if (mailArray == null)
        return;
      foreach (Json_Mail mail in mailArray)
      {
        MailData mailData = new MailData();
        mailData.Deserialize(mail);
        this.mails.Add(mailData);
      }
    }

    public void Deserialize(Json_MailOption mailOption)
    {
      this.hasNext = mailOption.hasNext > (byte) 0;
      this.hasPrev = mailOption.hasPrev > (byte) 0;
      this.page = mailOption.currentPage;
      this.pageMax = mailOption.totalPage;
      this.mailCount = mailOption.totalCount;
    }
  }
}
