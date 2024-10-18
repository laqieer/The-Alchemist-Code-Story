// Decompiled with JetBrains decompiler
// Type: SRPG.ContentScroller
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class ContentScroller : SRPG_ScrollRect
  {
    private ContentController mContentController;

    public ContentController contentController
    {
      get
      {
        if ((UnityEngine.Object) this.mContentController == (UnityEngine.Object) null && (UnityEngine.Object) this.content != (UnityEngine.Object) null)
          this.mContentController = this.content.GetComponent<ContentController>();
        return this.mContentController;
      }
    }

    protected override void LateUpdate()
    {
      base.LateUpdate();
      if ((UnityEngine.Object) this.contentController == (UnityEngine.Object) null)
        ;
    }
  }
}
