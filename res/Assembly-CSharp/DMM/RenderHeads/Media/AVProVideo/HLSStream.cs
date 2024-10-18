// Decompiled with JetBrains decompiler
// Type: RenderHeads.Media.AVProVideo.HLSStream
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

#nullable disable
namespace RenderHeads.Media.AVProVideo
{
  public class HLSStream : Stream
  {
    private const string BANDWITH_NAME = "BANDWIDTH=";
    private const string RESOLUTION_NAME = "RESOLUTION=";
    private const string CHUNK_TAG = "#EXTINF";
    private const string STREAM_TAG = "#EXT-X-STREAM-INF";
    private List<Stream> _streams;
    private List<Stream.Chunk> _chunks;
    private string _streamURL;
    private int _width;
    private int _height;
    private int _bandwidth;

    public HLSStream(string filename, int width = 0, int height = 0, int bandwidth = 0)
    {
      this._streams = new List<Stream>();
      this._chunks = new List<Stream.Chunk>();
      this._width = width;
      this._height = height;
      this._bandwidth = bandwidth;
      this._streamURL = filename;
      try
      {
        string[] text = (string[]) null;
        if (filename.ToLower().StartsWith("http://") || filename.ToLower().StartsWith("https://"))
        {
          if (filename.ToLower().StartsWith("https://"))
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(this.MyRemoteCertificateValidationCallback);
          using (WebClient webClient = new WebClient())
            text = webClient.DownloadString(filename).Split('\n');
        }
        else
          text = System.IO.File.ReadAllLines(filename);
        int num = filename.LastIndexOf('/');
        if (num < 0)
          num = filename.LastIndexOf('\\');
        string path = this._streamURL.Substring(0, num + 1);
        this.ParseFile(text, path);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public override int Width => this._width;

    public override int Height => this._height;

    public override int Bandwidth => this._bandwidth;

    public override string URL => this._streamURL;

    public override List<Stream.Chunk> GetAllChunks()
    {
      List<Stream.Chunk> allChunks1 = new List<Stream.Chunk>();
      for (int index = 0; index < this._streams.Count; ++index)
      {
        List<Stream.Chunk> allChunks2 = this._streams[index].GetAllChunks();
        allChunks1.AddRange((IEnumerable<Stream.Chunk>) allChunks2);
      }
      allChunks1.AddRange((IEnumerable<Stream.Chunk>) this._chunks);
      return allChunks1;
    }

    public override List<Stream.Chunk> GetChunks() => this._chunks;

    public override List<Stream> GetAllStreams()
    {
      List<Stream> allStreams1 = new List<Stream>();
      for (int index = 0; index < this._streams.Count; ++index)
      {
        List<Stream> allStreams2 = this._streams[index].GetAllStreams();
        allStreams1.AddRange((IEnumerable<Stream>) allStreams2);
      }
      allStreams1.AddRange((IEnumerable<Stream>) this._streams);
      return allStreams1;
    }

    public override List<Stream> GetStreams() => this._streams;

    private bool ExtractStreamInfo(string line, ref int width, ref int height, ref int bandwidth)
    {
      if (!line.StartsWith("#EXT-X-STREAM-INF"))
        return false;
      int num1 = line.IndexOf("BANDWIDTH=");
      if (num1 >= 0)
      {
        int num2 = line.IndexOf(',', num1 + "BANDWIDTH=".Length);
        if (num2 < 0)
          num2 = line.Length - 1;
        if (num2 >= 0 && num2 - "BANDWIDTH=".Length > num1)
        {
          int length = num2 - num1 - "BANDWIDTH=".Length;
          if (!int.TryParse(line.Substring(num1 + "BANDWIDTH=".Length, length), out bandwidth))
            bandwidth = 0;
        }
      }
      else
        bandwidth = 0;
      int num3 = line.IndexOf("RESOLUTION=");
      if (num3 >= 0)
      {
        int num4 = line.IndexOf(',', num3 + "RESOLUTION=".Length);
        if (num4 < 0)
          num4 = line.Length - 1;
        if (num4 >= 0 && num4 - "RESOLUTION=".Length > num3)
        {
          int length1 = num4 - num3 - "RESOLUTION=".Length;
          string str = line.Substring(num3 + "RESOLUTION=".Length, length1);
          int length2 = str.IndexOf('x');
          if (length2 < 0 || !int.TryParse(str.Substring(0, length2), out width) || !int.TryParse(str.Substring(length2 + 1, str.Length - (length2 + 1)), out height))
            width = height = 0;
        }
      }
      else
        width = height = 0;
      return true;
    }

    private static bool IsChunk(string line) => line.StartsWith("#EXTINF");

    private void ParseFile(string[] text, string path)
    {
      bool flag1 = false;
      bool flag2 = false;
      int width = 0;
      int height = 0;
      int bandwidth = 0;
      for (int index = 0; index < text.Length; ++index)
      {
        if (this.ExtractStreamInfo(text[index], ref width, ref height, ref bandwidth))
        {
          flag2 = true;
          flag1 = false;
        }
        else if (HLSStream.IsChunk(text[index]))
        {
          flag1 = true;
          flag2 = false;
        }
        else if (flag1)
        {
          Stream.Chunk chunk;
          chunk.name = path + text[index];
          this._chunks.Add(chunk);
          flag1 = false;
          flag2 = false;
        }
        else if (flag2)
        {
          try
          {
            this._streams.Add((Stream) new HLSStream(text[index].IndexOf("://") >= 0 ? text[index] : path + text[index], width, height, bandwidth));
          }
          catch (Exception ex)
          {
            Debug.LogError((object) ("[AVProVideo]HLSParser cannot parse stream " + path + text[index] + ", " + ex.Message));
          }
          flag1 = false;
          flag2 = false;
        }
        else
        {
          flag1 = false;
          flag2 = false;
        }
      }
    }

    private bool MyRemoteCertificateValidationCallback(
      object sender,
      X509Certificate certificate,
      X509Chain chain,
      SslPolicyErrors sslPolicyErrors)
    {
      bool flag = true;
      if (sslPolicyErrors != SslPolicyErrors.None)
      {
        for (int index = 0; index < chain.ChainStatus.Length; ++index)
        {
          if (chain.ChainStatus[index].Status != X509ChainStatusFlags.RevocationStatusUnknown)
          {
            chain.ChainPolicy.RevocationFlag = X509RevocationFlag.EntireChain;
            chain.ChainPolicy.RevocationMode = X509RevocationMode.Online;
            chain.ChainPolicy.UrlRetrievalTimeout = new TimeSpan(0, 1, 0);
            chain.ChainPolicy.VerificationFlags = X509VerificationFlags.NoFlag;
            if (!chain.Build((X509Certificate2) certificate))
            {
              flag = false;
              break;
            }
          }
        }
      }
      return flag;
    }
  }
}
