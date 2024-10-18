// Decompiled with JetBrains decompiler
// Type: SRPG.RaidPeriodTimeScheduleParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  public class RaidPeriodTimeScheduleParam
  {
    private string mBegin;
    private string mOpen;

    public string Begin => this.mBegin;

    public string Open => this.mOpen;

    public bool Deserialize(JSON_RaidPeriodTimeScheduleParam json)
    {
      this.mBegin = json.begin_time;
      this.mOpen = json.open_time;
      return true;
    }
  }
}
