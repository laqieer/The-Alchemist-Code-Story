// Decompiled with JetBrains decompiler
// Type: SRPG.MultiFuid
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
