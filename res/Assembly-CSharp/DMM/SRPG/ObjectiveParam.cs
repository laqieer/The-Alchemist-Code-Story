// Decompiled with JetBrains decompiler
// Type: SRPG.ObjectiveParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class ObjectiveParam
  {
    public string iname;
    public JSON_InnerObjective[] objective;

    public void Deserialize(JSON_ObjectiveParam json)
    {
      this.iname = json != null ? json.iname : throw new InvalidJSONException();
      this.objective = json.objective != null ? json.objective : throw new InvalidJSONException();
    }
  }
}
