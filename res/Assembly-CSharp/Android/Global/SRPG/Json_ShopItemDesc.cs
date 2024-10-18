// Decompiled with JetBrains decompiler
// Type: SRPG.Json_ShopItemDesc
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class Json_ShopItemDesc
  {
    public string iname;
    public int num;
    public int maxnum;
    public int boughtnum;

    public bool IsArtifact
    {
      get
      {
        return this.iname.StartsWith("AF_");
      }
    }
  }
}
