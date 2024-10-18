// Decompiled with JetBrains decompiler
// Type: SRPG.ChargeCheckData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class ChargeCheckData
  {
    public int Age;
    public string[] AcceptIds;
    public string[] RejectIds;

    public bool Deserialize(JSON_ChargeCheckResponse json)
    {
      if (json == null)
        return false;
      this.Age = json.age;
      this.AcceptIds = json.accept_ids;
      this.RejectIds = json.reject_ids;
      if (this.RejectIds == null)
        this.RejectIds = new string[0];
      return true;
    }
  }
}
