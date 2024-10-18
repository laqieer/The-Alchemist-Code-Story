// Decompiled with JetBrains decompiler
// Type: Gsc.DOM.Generic.Document
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

namespace Gsc.DOM.Generic
{
  public class Document : IDocument, IDisposable
  {
    private readonly Value root;

    public Document(Document document, ref Value root)
    {
      this.root = root;
    }

    public Value Root
    {
      get
      {
        return this.root;
      }
    }

    IValue IDocument.Root
    {
      get
      {
        return (IValue) this.root;
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
