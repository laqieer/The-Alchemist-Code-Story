// Decompiled with JetBrains decompiler
// Type: SRPG.PlayerParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
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
