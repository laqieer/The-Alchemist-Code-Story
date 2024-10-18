// Decompiled with JetBrains decompiler
// Type: SRPG.ObjectiveParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class ObjectiveParam
  {
    public string iname;
    public JSON_InnerObjective[] objective;

    public void Deserialize(JSON_ObjectiveParam json)
    {
      if (json == null)
        throw new InvalidJSONException();
      this.iname = json.iname;
      if (json.objective == null)
        throw new InvalidJSONException();
      this.objective = json.objective;
    }
  }
}
