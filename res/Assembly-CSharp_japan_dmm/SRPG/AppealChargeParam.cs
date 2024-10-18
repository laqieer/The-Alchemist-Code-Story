// Decompiled with JetBrains decompiler
// Type: SRPG.AppealChargeParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  public class AppealChargeParam
  {
    public string m_AppealId = string.Empty;
    public string m_BeforeImg = string.Empty;
    public string m_AfterImg = string.Empty;
    public long m_StartAt;
    public long m_EndAt;

    public AppealChargeParam()
    {
      this.m_AppealId = string.Empty;
      this.m_BeforeImg = string.Empty;
      this.m_AfterImg = string.Empty;
      this.m_StartAt = 0L;
      this.m_EndAt = 0L;
    }

    public string appeal_id => this.m_AppealId;

    public string before_img => this.m_BeforeImg;

    public string after_img => this.m_AfterImg;

    public long start_at => this.m_StartAt;

    public long end_at => this.m_EndAt;

    public void Deserialize(JSON_AppealChargeParam _json)
    {
      if (_json == null)
        throw new InvalidJSONException();
      this.m_AppealId = _json.fields.appeal_id;
      this.m_BeforeImg = _json.fields.before_img_id;
      this.m_AfterImg = _json.fields.after_img_id;
      try
      {
        if (!string.IsNullOrEmpty(_json.fields.start_at))
          this.m_StartAt = TimeManager.GetUnixSec(DateTime.Parse(_json.fields.start_at));
        if (string.IsNullOrEmpty(_json.fields.end_at))
          return;
        this.m_EndAt = TimeManager.GetUnixSec(DateTime.Parse(_json.fields.end_at));
      }
      catch (Exception ex)
      {
        DebugUtility.LogError(ex.ToString());
      }
    }
  }
}
