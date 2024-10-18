// Decompiled with JetBrains decompiler
// Type: SRPG.RecipeTree
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class RecipeTree
  {
    private List<RecipeTree> children = new List<RecipeTree>();
    private RecipeTree parent;
    private bool is_common;
    private ItemParam param;

    public RecipeTree(ItemParam param) => this.param = param;

    public List<RecipeTree> Children => this.children;

    public RecipeTree Parent => this.parent;

    public bool IsCommon => this.is_common;

    public string iname => this.param != null ? this.param.iname : (string) null;

    public void SetChild(RecipeTree child)
    {
      child.parent = this;
      this.children.Add(child);
    }

    public void SetIsCommon()
    {
      this.is_common = true;
      if (this.parent == null)
        return;
      this.parent.SetIsCommon();
    }

    public void RemoveLastAt()
    {
      if (this.children == null || this.children.Count <= 0)
        return;
      this.children.RemoveAt(this.children.Count - 1);
    }
  }
}
