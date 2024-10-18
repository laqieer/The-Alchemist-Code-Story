// Decompiled with JetBrains decompiler
// Type: RenderHeads.Media.AVProVideo.StreamParser
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

#nullable disable
namespace RenderHeads.Media.AVProVideo
{
  public class StreamParser : MonoBehaviour
  {
    public string _url;
    public StreamParser.StreamType _streamType;
    public bool _autoLoad = true;
    private Stream _parser;
    private bool _loaded;
    private List<Stream> _substreams;
    private List<Stream.Chunk> _chunks;
    private StreamParserEvent _events;

    public StreamParserEvent Events
    {
      get
      {
        if (this._events == null)
          this._events = new StreamParserEvent();
        return this._events;
      }
    }

    private void LoadFile()
    {
      try
      {
        this._parser = this._streamType == StreamParser.StreamType.HLS ? (Stream) new HLSStream(this._url) : (Stream) new HLSStream(this._url);
        this._substreams = this._parser.GetAllStreams();
        this._chunks = this._parser.GetAllChunks();
        this._loaded = true;
        Debug.Log((object) ("[AVProVideo] Stream parser completed parsing stream file " + this._url));
        if (this._events == null)
          return;
        this._events.Invoke(this, StreamParserEvent.EventType.Success);
      }
      catch (Exception ex)
      {
        this._loaded = false;
        Debug.LogError((object) ("[AVProVideo] Parser unable to read stream " + ex.Message));
        if (this._events == null)
          return;
        this._events.Invoke(this, StreamParserEvent.EventType.Failed);
      }
    }

    public bool Loaded => this._loaded;

    public Stream Root => this._loaded ? this._parser : (Stream) null;

    public List<Stream> SubStreams => this._loaded ? this._substreams : (List<Stream>) null;

    public List<Stream.Chunk> Chunks => this._loaded ? this._chunks : (List<Stream.Chunk>) null;

    public void ParseStream() => new Thread(new ThreadStart(this.LoadFile)).Start();

    private void Start()
    {
      if (!this._autoLoad)
        return;
      this.ParseStream();
    }

    public enum StreamType
    {
      HLS,
    }
  }
}
