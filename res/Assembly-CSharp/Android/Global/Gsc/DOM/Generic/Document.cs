// Decompiled with JetBrains decompiler
// Type: Gsc.DOM.Generic.Document
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;

namespace Gsc.DOM.Generic
{
  public class Document : IDisposable, IDocument
  {
    private readonly Value root;

    public Document(Document document, ref Value root)
    {
      this.root = root;
    }

    IValue IDocument.Root
    {
      get
      {
        return (IValue) this.root;
      }
    }

    public Value Root
    {
      get
      {
        return this.root;
      }
    }

    ~Document()
    {
      this.Dispose();
    }

    public void Dispose()
    {
    }
  }
}
