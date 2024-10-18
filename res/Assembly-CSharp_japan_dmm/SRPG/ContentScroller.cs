// Decompiled with JetBrains decompiler
// Type: SRPG.ContentScroller
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public class ContentScroller : SRPG_ScrollRect
  {
    private ContentController mContentController;

    public ContentController contentController
    {
      get
      {
        if (Object.op_Equality((Object) this.mContentController, (Object) null) && Object.op_Inequality((Object) this.content, (Object) null))
          this.mContentController = ((Component) this.content).GetComponent<ContentController>();
        return this.mContentController;
      }
    }

    protected override void LateUpdate()
    {
      base.LateUpdate();
      if (!Object.op_Equality((Object) this.contentController, (Object) null))
        ;
    }
  }
}
