// Decompiled with JetBrains decompiler
// Type: Gsc.DOM.Json.Document
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace Gsc.DOM.Json
{
  public class Document : IDocument, IDisposable
  {
    private readonly rapidjson.Document document;
    private Value root;

    private Document(rapidjson.Document document)
    {
      this.document = document;
      this.root = new Value(document.Root);
    }

    public static Document Parse(byte[] bytes) => new Document(rapidjson.Document.Parse(bytes));

    public static Document Parse(string text) => new Document(rapidjson.Document.Parse(text));

    public static Document ParseFromFile(string filepath)
    {
      return new Document(rapidjson.Document.ParseFromFile(filepath));
    }

    public Value Root => this.root;

    IValue IDocument.Root => (IValue) this.root;

    public void SetRoot(Value root) => this.root = root;

    ~Document() => this.Dispose();

    public void Dispose() => this.document.Dispose();
  }
}
