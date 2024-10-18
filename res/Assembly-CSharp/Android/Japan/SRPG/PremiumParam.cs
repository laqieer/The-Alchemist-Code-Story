// Decompiled with JetBrains decompiler
// Type: SRPG.PremiumParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class PremiumParam
  {
    public string m_Iname;
    public string m_Image;
    public long m_BeginAt;
    public long m_EndAt;
    public int m_Span;

    public bool Deserialize(JSON_PremiumParam json)
    {
      if (json == null)
        return false;
      this.m_Iname = json.iname;
      this.m_Image = json.image;
      this.m_BeginAt = json.begin_at == null ? 0L : TimeManager.GetUnixSec(DateTime.Parse(json.begin_at));
      this.m_EndAt = json.begin_at == null ? 0L : TimeManager.GetUnixSec(DateTime.Parse(json.end_at));
      this.m_Span = json.span;
      return true;
    }
  }
}
