// Decompiled with JetBrains decompiler
// Type: Gsc.DOM.Generic.Document
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace Gsc.DOM.Generic
{
  public class Document : IDocument, IDisposable
  {
    private readonly Value root;

    public Document(Document document, ref Value root) => this.root = root;

    public Value Root => this.root;

    IValue IDocument.Root => (IValue) this.root;

    ~Document() => this.Dispose();

    public void Dispose()
    {
    }
  }
}
