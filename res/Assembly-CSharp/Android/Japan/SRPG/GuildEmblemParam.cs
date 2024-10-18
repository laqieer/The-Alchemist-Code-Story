// Decompiled with JetBrains decompiler
// Type: SRPG.GuildEmblemParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

namespace SRPG
{
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

    public string Iname
    {
      get
      {
        return this.mIname;
      }
    }

    public string Name
    {
      get
      {
        return this.mName;
      }
    }

    public int ConditionsType
    {
      get
      {
        return this.mConditionsType;
      }
    }

    public int ConditionsValue
    {
      get
      {
        return this.mConditionsValue;
      }
    }

    public string Image
    {
      get
      {
        return this.mImage;
      }
    }

    public DateTime StartAt
    {
      get
      {
        return this.mStartAt;
      }
    }

    public DateTime EndAt
    {
      get
      {
        return this.mEndAt;
      }
    }

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
