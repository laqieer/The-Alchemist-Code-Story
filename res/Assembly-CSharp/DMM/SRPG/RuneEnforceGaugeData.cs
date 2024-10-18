// Decompiled with JetBrains decompiler
// Type: SRPG.RuneEnforceGaugeData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class RuneEnforceGaugeData
  {
    public byte rare;
    public short val;

    public bool Deserialize(Json_RuneEnforceGaugeData json)
    {
      if (json == null)
        return false;
      this.rare = (byte) json.rare;
      this.val = (short) json.val;
      return true;
    }
  }
}
