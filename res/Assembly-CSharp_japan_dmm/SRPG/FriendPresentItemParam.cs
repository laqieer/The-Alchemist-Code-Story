// Decompiled with JetBrains decompiler
// Type: SRPG.FriendPresentItemParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;

#nullable disable
namespace SRPG
{
  public class FriendPresentItemParam
  {
    public const string DEAULT_ID = "FP_DEFAULT";
    public static FriendPresentItemParam DefaultParam;
    private string m_Id;
    private string m_Name;
    private string m_Expr;
    private ItemParam m_Item;
    private int m_Num;
    private int m_Zeny;
    private long m_BeginAt;
    private long m_EndAt;

    public string iname => this.m_Id;

    public string name => this.m_Name;

    public string expr => this.m_Expr;

    public ItemParam item => this.m_Item;

    public int num => this.m_Num;

    public int zeny => this.m_Zeny;

    public long begin_at => this.m_BeginAt;

    public long end_at => this.m_EndAt;

    public long GetRestTime(long serverTime)
    {
      long restTime = this.m_EndAt - serverTime;
      if (restTime < 0L)
        restTime = 0L;
      return restTime;
    }

    public bool IsItem() => this.m_Item != null;

    public bool IsZeny() => this.m_Item == null;

    public bool HasTimeLimit() => this.m_BeginAt > 0L || this.m_EndAt > 0L;

    public bool IsValid(long nowSec)
    {
      if (!this.HasTimeLimit())
        return true;
      return this.m_BeginAt <= nowSec && nowSec < this.m_EndAt;
    }

    public bool IsDefault() => this.m_Id == "FP_DEFAULT";

    public void Deserialize(JSON_FriendPresentItemParam json)
    {
      this.m_Id = json != null ? json.iname : throw new InvalidJSONException();
      this.m_Name = json.name;
      this.m_Expr = json.expr;
      if (!string.IsNullOrEmpty(json.item))
        this.m_Item = MonoSingleton<GameManager>.Instance.GetItemParam(json.item);
      this.m_Num = json.num;
      this.m_Zeny = json.zeny;
      try
      {
        if (!string.IsNullOrEmpty(json.begin_at))
          this.m_BeginAt = TimeManager.GetUnixSec(DateTime.Parse(json.begin_at));
        if (!string.IsNullOrEmpty(json.end_at))
          this.m_EndAt = TimeManager.GetUnixSec(DateTime.Parse(json.end_at));
        if (!(this.m_Id == "FP_DEFAULT"))
          return;
        FriendPresentItemParam.DefaultParam = this;
      }
      catch (Exception ex)
      {
        DebugUtility.LogError(ex.ToString());
      }
    }
  }
}
