// Decompiled with JetBrains decompiler
// Type: SRPG.TobiraCategoriesParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class TobiraCategoriesParam
  {
    private TobiraParam.Category mCategory;
    private string mName;

    public TobiraParam.Category TobiraCategory
    {
      get
      {
        return this.mCategory;
      }
    }

    public string Name
    {
      get
      {
        return this.mName;
      }
    }

    public void Deserialize(JSON_TobiraCategoriesParam json)
    {
      if (json == null)
        return;
      this.mCategory = (TobiraParam.Category) json.category;
      this.mName = json.name;
    }
  }
}
