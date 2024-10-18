// Decompiled with JetBrains decompiler
// Type: SRPG.WeatherParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class WeatherParam
  {
    private string mIname;
    private string mName;
    private string mExpr;
    private string mIcon;
    private string mEffect;
    private List<string> mBuffIdLists = new List<string>();
    private List<string> mCondIdLists = new List<string>();

    public string Iname => this.mIname;

    public string Name => this.mName;

    public string Expr => this.mExpr;

    public string Icon => this.mIcon;

    public string Effect => this.mEffect;

    public List<string> BuffIdLists => this.mBuffIdLists;

    public List<string> CondIdLists => this.mCondIdLists;

    public void Deserialize(JSON_WeatherParam json)
    {
      if (json == null)
        return;
      this.mIname = json.iname;
      this.mName = json.name;
      this.mExpr = json.expr;
      this.mIcon = json.icon;
      this.mEffect = json.effect;
      this.mBuffIdLists.Clear();
      if (json.buff_ids != null)
      {
        foreach (string buffId in json.buff_ids)
          this.mBuffIdLists.Add(buffId);
      }
      this.mCondIdLists.Clear();
      if (json.cond_ids == null)
        return;
      foreach (string condId in json.cond_ids)
        this.mCondIdLists.Add(condId);
    }
  }
}
