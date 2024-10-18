// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_ProductParamResponse
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  public class JSON_ProductParamResponse
  {
    public JSON_ProductParam[] products;
    public JSON_ProductBuyCoinParam[] buycoins;
  }
}
