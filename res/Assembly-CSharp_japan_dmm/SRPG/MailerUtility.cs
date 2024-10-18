// Decompiled with JetBrains decompiler
// Type: SRPG.MailerUtility
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using DeviceKit;

#nullable disable
namespace SRPG
{
  public class MailerUtility
  {
    public static void Launch(string mailto, string subject, string body)
    {
      App.LaunchMailer(mailto, subject, body.Replace("\n", "%0A"));
    }
  }
}
