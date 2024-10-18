// Decompiled with JetBrains decompiler
// Type: SRPG.GuildEmblemParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  [Serializable]
  public class GuildEmblemParam
  {
    private string mIname;
    private string mName;
    private int mConditionsType;
    private int mConditionsValue;
    private string mImage;
    private DateTime mStartAt;
    private DateTime mEndAt;

    public string Iname => this.mIname;

    public string Name => this.mName;

    public int ConditionsType => this.mConditionsType;

    public int ConditionsValue => this.mConditionsValue;

    public string Image => this.mImage;

    public DateTime StartAt => this.mStartAt;

    public DateTime EndAt => this.mEndAt;

    public void Deserialize(JSON_GuildEmblemParam json)
    {
      this.mIname = json.iname;
      this.mName = json.name;
      this.mConditionsType = json.cnds_type;
      this.mConditionsValue = json.cnds_val;
      this.mImage = json.image;
      this.mStartAt = DateTime.MinValue;
      if (!string.IsNullOrEmpty(json.start_at))
        DateTime.TryParse(json.start_at, out this.mStartAt);
      this.mEndAt = DateTime.MinValue;
      if (string.IsNullOrEmpty(json.end_at))
        return;
      DateTime.TryParse(json.end_at, out this.mEndAt);
    }
  }
}
