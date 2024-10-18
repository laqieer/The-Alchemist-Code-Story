// Decompiled with JetBrains decompiler
// Type: DownloaderClient
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Networking;

#nullable disable
public class DownloaderClient : DLCDownloadClient
{
  private const float sleepTime = 0.05f;
  private const int timeoutTime = 10;
  private const int bufferSize = 1048576;
  private MonoBehaviour _coroutineExecuter;
  private IEnumerator _downloadEnumerator;
  private IEnumerator _writeStreamEnumerator;
  private UnityWebRequest _request;
  private float _timeOutCount;

  public override int DownloadedSize => this.DataBytes == null ? 0 : this.DataBytes.Length;

  public override void Download(string url, int size, MonoBehaviour coroutineExecuter)
  {
    this.IsDone = false;
    this.IsGotRest = false;
    this.ContentLength = size;
    this.DataBytes = new byte[this.ContentLength];
    this.LoadingSize = 0;
    this.CanWriteStream = false;
    this.HasError = string.Empty;
    this._coroutineExecuter = coroutineExecuter;
    if (this._downloadEnumerator != null)
      this._coroutineExecuter.StopCoroutine(this._downloadEnumerator);
    this._downloadEnumerator = this._Download(url);
    this._coroutineExecuter.StartCoroutine(this._downloadEnumerator);
  }

  [DebuggerHidden]
  private IEnumerator _Download(string url)
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new DownloaderClient.\u003C_Download\u003Ec__Iterator0()
    {
      url = url,
      \u0024this = this
    };
  }

  public override void Abort()
  {
    Debug.LogWarning((object) "Aborting");
    if (this._downloadEnumerator != null)
      this._coroutineExecuter.StopCoroutine(this._downloadEnumerator);
    this._downloadEnumerator = (IEnumerator) null;
    if (this._writeStreamEnumerator != null)
      this._coroutineExecuter.StopCoroutine(this._writeStreamEnumerator);
    this._writeStreamEnumerator = (IEnumerator) null;
    this.Dispose();
  }

  public override void Dispose()
  {
    this.DataBytes = (byte[]) null;
    if (this._writeStreamEnumerator != null)
    {
      this._coroutineExecuter.StopCoroutine(this._writeStreamEnumerator);
      this._writeStreamEnumerator = (IEnumerator) null;
    }
    if (this._downloadEnumerator != null)
    {
      this._coroutineExecuter.StopCoroutine(this._downloadEnumerator);
      this._downloadEnumerator = (IEnumerator) null;
    }
    if (this._request != null)
      this._request.Dispose();
    this.CanWriteStream = false;
  }

  [DebuggerHidden]
  private IEnumerator WriteMemoryStreamResponse()
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new DownloaderClient.\u003CWriteMemoryStreamResponse\u003Ec__Iterator1()
    {
      \u0024this = this
    };
  }

  private class DLCDownloadHandler : DownloadHandlerScript
  {
    private DownloaderClient loader;
    private List<byte> buf;

    public DLCDownloadHandler(DownloaderClient _loader, byte[] buffer)
      : base(buffer)
    {
      this.loader = _loader;
      this.loader.IsGotRest = true;
      this.buf = new List<byte>();
    }

    protected virtual bool ReceiveData(byte[] data, int dataLength)
    {
      this.loader._timeOutCount = 0.0f;
      if (!this.loader.IsGotRest && this.buf != null)
      {
        this.buf.AddRange((IEnumerable<byte>) data);
        return true;
      }
      if (this.buf != null && this.buf.Count > 0)
      {
        this.BufCopy(this.buf.ToArray(), this.buf.Count);
        this.buf.Clear();
        this.buf = (List<byte>) null;
      }
      this.BufCopy(data, dataLength);
      return true;
    }

    private void BufCopy(byte[] bytes, int length)
    {
      if (this.loader == null)
        DebugUtility.LogError("loader is null");
      else if (this.loader.DataBytes == null)
        DebugUtility.LogError("loader.DataBytes is null");
      else if (bytes == null)
      {
        DebugUtility.LogError("bytes is null");
      }
      else
      {
        if (length + this.loader.LoadingSize > this.loader.DataBytes.Length)
        {
          byte[] destinationArray = new byte[length + this.loader.LoadingSize];
          Array.Copy((Array) this.loader.DataBytes, 0, (Array) destinationArray, 0, this.loader.LoadingSize);
          this.loader.DataBytes = destinationArray;
        }
        Array.Copy((Array) bytes, 0, (Array) this.loader.DataBytes, this.loader.LoadingSize, length);
        this.loader.LoadingSize += length;
      }
    }

    protected virtual void CompleteContent()
    {
      if (!this.loader.IsGotRest)
      {
        this.loader.ContentLength = this.buf.Count;
        this.loader.LoadingSize = this.buf.Count;
        this.loader.IsGotRest = true;
        this.loader.DataBytes = this.buf.ToArray();
      }
      if (this.buf != null)
      {
        this.buf.Clear();
        this.buf = (List<byte>) null;
      }
      this.loader._downloadEnd = Stopwatch.GetTimestamp();
      this.loader.IsDone = true;
    }
  }
}
