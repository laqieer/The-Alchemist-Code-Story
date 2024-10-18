// Decompiled with JetBrains decompiler
// Type: SRPG.MultiFuid
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class MultiFuid
  {
    public string fuid;
    public string status;

    public bool Deserialize(Json_MultiFuids json)
    {
      this.fuid = json.fuid;
      this.status = json.status;
      return true;
    }
  }
}
