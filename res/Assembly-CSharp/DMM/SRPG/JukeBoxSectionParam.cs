// Decompiled with JetBrains decompiler
// Type: SRPG.JukeBoxSectionParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  public class JukeBoxSectionParam
  {
    private string iname;
    private string title;

    public string Title => this.title;

    public string Iname => this.iname;

    public bool Deserialize(JSON_JukeBoxSectionParam json)
    {
      this.iname = json.iname;
      this.title = json.title;
      return true;
    }

    public static void Deserialize(
      ref List<JukeBoxSectionParam> ref_params,
      JSON_JukeBoxSectionParam[] json)
    {
      if (ref_params == null)
        ref_params = new List<JukeBoxSectionParam>();
      ref_params.Clear();
      if (json == null)
        return;
      foreach (JSON_JukeBoxSectionParam json1 in json)
      {
        JukeBoxSectionParam jukeBoxSectionParam = new JukeBoxSectionParam();
        jukeBoxSectionParam.Deserialize(json1);
        ref_params.Add(jukeBoxSectionParam);
      }
    }
  }
}
