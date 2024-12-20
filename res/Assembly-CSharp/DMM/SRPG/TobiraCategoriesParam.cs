﻿// Decompiled with JetBrains decompiler
// Type: SRPG.TobiraCategoriesParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class TobiraCategoriesParam
  {
    private TobiraParam.Category mCategory;
    private string mName;

    public TobiraParam.Category TobiraCategory => this.mCategory;

    public string Name => this.mName;

    public void Deserialize(JSON_TobiraCategoriesParam json)
    {
      if (json == null)
        return;
      this.mCategory = (TobiraParam.Category) json.category;
      this.mName = json.name;
    }
  }
}
