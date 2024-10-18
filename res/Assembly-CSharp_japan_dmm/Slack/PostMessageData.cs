// Decompiled with JetBrains decompiler
// Type: Slack.PostMessageData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace Slack
{
  [Serializable]
  public class PostMessageData
  {
    public string token = string.Empty;
    public string channel = string.Empty;
    public string text = string.Empty;
    public string parse = string.Empty;
    public string link_names = string.Empty;
    public string username = string.Empty;
    public string icon_url = string.Empty;
    public string icon_emoji = string.Empty;
  }
}
