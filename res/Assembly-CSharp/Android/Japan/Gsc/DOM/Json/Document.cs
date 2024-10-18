// Decompiled with JetBrains decompiler
// Type: Gsc.DOM.Json.Document
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

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

    public static Document Parse(byte[] bytes)
    {
      return new Document(rapidjson.Document.Parse(bytes));
    }

    public static Document Parse(string text)
    {
      return new Document(rapidjson.Document.Parse(text));
    }

    public static Document ParseFromFile(string filepath)
    {
      return new Document(rapidjson.Document.ParseFromFile(filepath));
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

    public void SetRoot(Value root)
    {
      this.root = root;
    }

    ~Document()
    {
      this.Dispose();
    }

    public void Dispose()
    {
      this.document.Dispose();
    }
  }
}
