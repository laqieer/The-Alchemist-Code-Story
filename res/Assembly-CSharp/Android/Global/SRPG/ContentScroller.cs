﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ContentScroller
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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