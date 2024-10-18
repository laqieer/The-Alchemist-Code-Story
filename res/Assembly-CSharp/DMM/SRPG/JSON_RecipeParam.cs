// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_RecipeParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  [Serializable]
  public class JSON_RecipeParam
  {
    public string iname;
    public int cost;
    public string mat1;
    public string mat2;
    public string mat3;
    public string mat4;
    public string mat5;
    public int num1;
    public int num2;
    public int num3;
    public int num4;
    public int num5;
  }
}
