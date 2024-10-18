// Decompiled with JetBrains decompiler
// Type: SRPG.PlayerParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class PlayerParam
  {
    public OInt pt;
    public OInt ucap;
    public OInt icap;
    public OInt ecap;
    public OInt fcap;

    public bool Deserialize(JSON_PlayerParam json)
    {
      if (json == null)
        return false;
      this.pt = (OInt) json.pt;
      this.ucap = (OInt) json.ucap;
      this.icap = (OInt) json.icap;
      this.ecap = (OInt) json.ecap;
      this.fcap = (OInt) json.fcap;
      return true;
    }
  }
}
