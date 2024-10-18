// Decompiled with JetBrains decompiler
// Type: SRPG.ChargeCheckData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
