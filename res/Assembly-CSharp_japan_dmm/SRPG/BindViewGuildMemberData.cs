// Decompiled with JetBrains decompiler
// Type: SRPG.BindViewGuildMemberData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class BindViewGuildMemberData
  {
    private GuildManager.GuildAttendStatus mAttendStatus;

    public GuildManager.GuildAttendStatus AttendStatus => this.mAttendStatus;

    public void SetAttendStatus(GuildManager.GuildAttendStatus status)
    {
      this.mAttendStatus = status;
    }
  }
}
