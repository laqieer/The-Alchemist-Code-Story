// Decompiled with JetBrains decompiler
// Type: SRPG.MailPageData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
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
