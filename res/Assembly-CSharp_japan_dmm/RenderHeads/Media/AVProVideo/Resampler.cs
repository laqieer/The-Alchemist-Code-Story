// Decompiled with JetBrains decompiler
// Type: RenderHeads.Media.AVProVideo.Resampler
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

#nullable disable
namespace RenderHeads.Media.AVProVideo
{
  public class Resampler
  {
    private List<Resampler.TimestampedRenderTexture[]> _buffer = new List<Resampler.TimestampedRenderTexture[]>();
    private MediaPlayer _mediaPlayer;
    private RenderTexture[] _outputTexture;
    private int _start;
    private int _end;
    private int _bufferSize;
    private long _baseTimestamp;
    private float _elapsedTimeSinceBase;
    private Material _blendMat;
    private Resampler.ResampleMode _resampleMode;
    private string _name = string.Empty;
    private long _lastTimeStamp = -1;
    private int _droppedFrames;
    private long _lastDisplayedTimestamp;
    private int _frameDisplayedTimer;
    private long _currentDisplayedTimestamp;
    private const string ShaderPropT = "_t";
    private const string ShaderPropAftertex = "_AfterTex";
    private int _propAfterTex;
    private int _propT;
    private float _videoFrameRate;

    public Resampler(
      MediaPlayer player,
      string name,
      int bufferSize = 2,
      Resampler.ResampleMode resampleMode = Resampler.ResampleMode.LINEAR)
    {
      this._bufferSize = Mathf.Max(2, bufferSize);
      // ISSUE: method pointer
      player.Events.AddListener(new UnityAction<MediaPlayer, MediaPlayerEvent.EventType, ErrorCode>((object) this, __methodptr(OnVideoEvent)));
      this._mediaPlayer = player;
      Shader shader = Shader.Find("AVProVideo/BlendFrames");
      if (Object.op_Inequality((Object) shader, (Object) null))
      {
        this._blendMat = new Material(shader);
        this._propT = Shader.PropertyToID("_t");
        this._propAfterTex = Shader.PropertyToID("_AfterTex");
      }
      else
        Debug.LogError((object) "[AVProVideo] Failed to find BlendFrames shader");
      this._resampleMode = resampleMode;
      this._name = name;
      Debug.Log((object) ("[AVProVideo] Resampler " + this._name + " started"));
    }

    public int DroppedFrames => this._droppedFrames;

    public int FrameDisplayedTimer => this._frameDisplayedTimer;

    public long BaseTimestamp
    {
      get => this._baseTimestamp;
      set => this._baseTimestamp = value;
    }

    public float ElapsedTimeSinceBase
    {
      get => this._elapsedTimeSinceBase;
      set => this._elapsedTimeSinceBase = value;
    }

    public float LastT { get; private set; }

    public long TextureTimeStamp { get; private set; }

    public void OnVideoEvent(MediaPlayer mp, MediaPlayerEvent.EventType et, ErrorCode errorCode)
    {
      switch (et)
      {
        case MediaPlayerEvent.EventType.MetaDataReady:
          this._videoFrameRate = mp.Info.GetVideoFrameRate();
          this._elapsedTimeSinceBase = 0.0f;
          if ((double) this._videoFrameRate <= 0.0)
            break;
          this._elapsedTimeSinceBase = (float) this._bufferSize / this._videoFrameRate;
          break;
        case MediaPlayerEvent.EventType.Closing:
          this.Reset();
          break;
      }
    }

    public Texture[] OutputTexture => (Texture[]) this._outputTexture;

    public void Reset()
    {
      this._lastTimeStamp = -1L;
      this._baseTimestamp = 0L;
      this.InvalidateBuffer();
    }

    public void Release()
    {
      this.ReleaseRenderTextures();
      if (!Object.op_Inequality((Object) this._blendMat, (Object) null))
        return;
      Object.Destroy((Object) this._blendMat);
    }

    private void ReleaseRenderTextures()
    {
      for (int index1 = 0; index1 < this._buffer.Count; ++index1)
      {
        for (int index2 = 0; index2 < this._buffer[index1].Length; ++index2)
        {
          if (Object.op_Inequality((Object) this._buffer[index1][index2].texture, (Object) null))
          {
            RenderTexture.ReleaseTemporary(this._buffer[index1][index2].texture);
            this._buffer[index1][index2].texture = (RenderTexture) null;
          }
        }
        if (this._outputTexture != null && Object.op_Inequality((Object) this._outputTexture[index1], (Object) null))
          RenderTexture.ReleaseTemporary(this._outputTexture[index1]);
      }
      this._outputTexture = (RenderTexture[]) null;
    }

    private void ConstructRenderTextures()
    {
      this.ReleaseRenderTextures();
      this._buffer.Clear();
      this._outputTexture = new RenderTexture[this._mediaPlayer.TextureProducer.GetTextureCount()];
      for (int index1 = 0; index1 < this._mediaPlayer.TextureProducer.GetTextureCount(); ++index1)
      {
        Texture texture = this._mediaPlayer.TextureProducer.GetTexture(index1);
        this._buffer.Add(new Resampler.TimestampedRenderTexture[this._bufferSize]);
        for (int index2 = 0; index2 < this._bufferSize; ++index2)
          this._buffer[index1][index2] = new Resampler.TimestampedRenderTexture();
        for (int index3 = 0; index3 < this._buffer[index1].Length; ++index3)
        {
          this._buffer[index1][index3].texture = RenderTexture.GetTemporary(texture.width, texture.height, 0);
          this._buffer[index1][index3].timestamp = 0L;
          this._buffer[index1][index3].used = false;
        }
        this._outputTexture[index1] = RenderTexture.GetTemporary(texture.width, texture.height, 0);
        ((Texture) this._outputTexture[index1]).filterMode = texture.filterMode;
        ((Texture) this._outputTexture[index1]).wrapMode = texture.wrapMode;
        ((Texture) this._outputTexture[index1]).anisoLevel = texture.anisoLevel;
      }
    }

    private bool CheckRenderTexturesValid()
    {
      for (int index1 = 0; index1 < this._mediaPlayer.TextureProducer.GetTextureCount(); ++index1)
      {
        Texture texture = this._mediaPlayer.TextureProducer.GetTexture(index1);
        for (int index2 = 0; index2 < this._buffer.Count; ++index2)
        {
          if (Object.op_Equality((Object) this._buffer[index1][index2].texture, (Object) null) || ((Texture) this._buffer[index1][index2].texture).width != texture.width || ((Texture) this._buffer[index1][index2].texture).height != texture.height)
            return false;
        }
        if (this._outputTexture == null || Object.op_Equality((Object) this._outputTexture[index1], (Object) null) || ((Texture) this._outputTexture[index1]).width != texture.width || ((Texture) this._outputTexture[index1]).height != texture.height)
          return false;
      }
      return true;
    }

    private int FindBeforeFrameIndex(int frameIdx)
    {
      if (frameIdx >= this._buffer.Count)
        return -1;
      int beforeFrameIndex = -1;
      float num1 = float.MaxValue;
      int num2 = -1;
      float num3 = float.MaxValue;
      for (int index = 0; index < this._buffer[frameIdx].Length; ++index)
      {
        if (this._buffer[frameIdx][index].used)
        {
          float num4 = (float) (this._buffer[frameIdx][index].timestamp - this._baseTimestamp) / 1E+07f;
          if ((double) num4 < (double) num3)
          {
            num2 = index;
            num3 = num4;
          }
          float num5 = this._elapsedTimeSinceBase - num4;
          if ((double) num5 >= 0.0 && (double) num5 < (double) num1)
          {
            num1 = num5;
            beforeFrameIndex = index;
          }
        }
      }
      if (beforeFrameIndex >= 0)
        return beforeFrameIndex;
      return num2 < 0 ? -1 : num2;
    }

    private int FindClosestFrame(int frameIdx)
    {
      if (frameIdx >= this._buffer.Count)
        return -1;
      int closestFrame = -1;
      float num1 = float.MaxValue;
      for (int index = 0; index < this._buffer[frameIdx].Length; ++index)
      {
        if (this._buffer[frameIdx][index].used)
        {
          float num2 = Mathf.Abs(this._elapsedTimeSinceBase - (float) (this._buffer[frameIdx][index].timestamp - this._baseTimestamp) / 1E+07f);
          if ((double) num2 < (double) num1)
          {
            closestFrame = index;
            num1 = num2;
          }
        }
      }
      return closestFrame;
    }

    private void PointUpdate()
    {
      for (int index = 0; index < this._buffer.Count; ++index)
      {
        int closestFrame = this.FindClosestFrame(index);
        if (closestFrame >= 0)
        {
          this._outputTexture[index].DiscardContents();
          Graphics.Blit((Texture) this._buffer[index][closestFrame].texture, this._outputTexture[index]);
          this.TextureTimeStamp = this._currentDisplayedTimestamp = this._buffer[index][closestFrame].timestamp;
        }
      }
    }

    private void SampleFrame(int frameIdx, int bufferIdx)
    {
      this._outputTexture[bufferIdx].DiscardContents();
      Graphics.Blit((Texture) this._buffer[bufferIdx][frameIdx].texture, this._outputTexture[bufferIdx]);
      this.TextureTimeStamp = this._currentDisplayedTimestamp = this._buffer[bufferIdx][frameIdx].timestamp;
    }

    private void SampleFrames(int bufferIdx, int frameIdx1, int frameIdx2, float t)
    {
      this._blendMat.SetFloat(this._propT, t);
      this._blendMat.SetTexture(this._propAfterTex, (Texture) this._buffer[bufferIdx][frameIdx2].texture);
      this._outputTexture[bufferIdx].DiscardContents();
      Graphics.Blit((Texture) this._buffer[bufferIdx][frameIdx1].texture, this._outputTexture[bufferIdx], this._blendMat);
      this.TextureTimeStamp = (long) Mathf.Lerp((float) this._buffer[bufferIdx][frameIdx1].timestamp, (float) this._buffer[bufferIdx][frameIdx2].timestamp, t);
      this._currentDisplayedTimestamp = this._buffer[bufferIdx][frameIdx1].timestamp;
    }

    private void LinearUpdate()
    {
      for (int index = 0; index < this._buffer.Count; ++index)
      {
        int beforeFrameIndex = this.FindBeforeFrameIndex(index);
        if (beforeFrameIndex >= 0)
        {
          float num1 = (float) (this._buffer[index][beforeFrameIndex].timestamp - this._baseTimestamp) / 1E+07f;
          if ((double) num1 > (double) this._elapsedTimeSinceBase)
          {
            this.SampleFrame(beforeFrameIndex, index);
            this.LastT = -1f;
          }
          else
          {
            int frameIdx2 = (beforeFrameIndex + 1) % this._buffer[index].Length;
            float num2 = (float) (this._buffer[index][frameIdx2].timestamp - this._baseTimestamp) / 1E+07f;
            if ((double) num2 < (double) num1)
            {
              this.SampleFrame(beforeFrameIndex, index);
              this.LastT = 2f;
            }
            else
            {
              float num3 = num2 - num1;
              float t = (this._elapsedTimeSinceBase - num1) / num3;
              this.SampleFrames(index, beforeFrameIndex, frameIdx2, t);
              this.LastT = t;
            }
          }
        }
      }
    }

    private void InvalidateBuffer()
    {
      this._elapsedTimeSinceBase = (float) (this._bufferSize / 2) / this._videoFrameRate;
      for (int index1 = 0; index1 < this._buffer.Count; ++index1)
      {
        for (int index2 = 0; index2 < this._buffer[index1].Length; ++index2)
          this._buffer[index1][index2].used = false;
      }
      this._start = this._end = 0;
    }

    private float GuessFrameRate()
    {
      int num1 = 0;
      long num2 = 0;
      for (int index1 = 0; index1 < this._buffer[0].Length; ++index1)
      {
        if (this._buffer[0][index1].used)
        {
          long num3 = long.MaxValue;
          for (int index2 = index1 + 1; index2 < this._buffer[0].Length; ++index2)
          {
            if (this._buffer[0][index2].used)
            {
              long num4 = Math.Abs(this._buffer[0][index1].timestamp - this._buffer[0][index2].timestamp);
              if (num4 < num3)
                num3 = num4;
            }
          }
          if (num3 != long.MaxValue)
          {
            num2 += num3;
            ++num1;
          }
        }
      }
      if (num1 > 1)
        num2 /= (long) num1;
      return 1E+07f / (float) num2;
    }

    public void Update()
    {
      if (this._mediaPlayer.TextureProducer == null || this._mediaPlayer.TextureProducer == null || Object.op_Equality((Object) this._mediaPlayer.TextureProducer.GetTexture(), (Object) null))
        return;
      if (!this.CheckRenderTexturesValid())
        this.ConstructRenderTextures();
      long textureTimeStamp1 = this._mediaPlayer.TextureProducer.GetTextureTimeStamp();
      if (textureTimeStamp1 != this._lastTimeStamp)
      {
        float num1 = Mathf.Abs((float) (textureTimeStamp1 - this._lastTimeStamp));
        float num2 = 1E+07f / this._videoFrameRate;
        if ((double) num1 > (double) num2 * 1.1000000238418579 && (double) num1 < (double) num2 * 3.0999999046325684)
          this._droppedFrames += (int) (((double) num1 - (double) num2) / (double) num2 + 0.5);
        this._lastTimeStamp = textureTimeStamp1;
      }
      long textureTimeStamp2 = this._mediaPlayer.TextureProducer.GetTextureTimeStamp();
      bool flag1 = !this._mediaPlayer.Control.IsSeeking();
      if (this._start != this._end || this._buffer[0][this._end].used)
      {
        int index = (this._end + this._buffer[0].Length - 1) % this._buffer[0].Length;
        if (textureTimeStamp2 == this._buffer[0][index].timestamp)
          flag1 = false;
      }
      bool flag2 = this._start != this._end || !this._buffer[0][this._end].used;
      if (flag1)
      {
        if (this._start == this._end && !this._buffer[0][this._end].used)
          this._baseTimestamp = textureTimeStamp2;
        if (this._end == this._start && this._buffer[0][this._end].used)
          this._start = (this._start + 1) % this._buffer[0].Length;
        for (int index = 0; index < this._mediaPlayer.TextureProducer.GetTextureCount(); ++index)
        {
          Texture texture = this._mediaPlayer.TextureProducer.GetTexture(index);
          this._buffer[index][this._end].texture.DiscardContents();
          Graphics.Blit(texture, this._buffer[index][this._end].texture);
          this._buffer[index][this._end].timestamp = textureTimeStamp2;
          this._buffer[index][this._end].used = true;
        }
        this._end = (this._end + 1) % this._buffer[0].Length;
      }
      bool flag3 = this._start != this._end || !this._buffer[0][this._end].used;
      if (flag3)
      {
        for (int index = 0; index < this._buffer.Count; ++index)
        {
          this._outputTexture[index].DiscardContents();
          Graphics.Blit((Texture) this._buffer[index][this._start].texture, this._outputTexture[index]);
          this._currentDisplayedTimestamp = this._buffer[index][this._start].timestamp;
        }
      }
      else if (flag2 && (double) this._videoFrameRate <= 0.0)
      {
        this._videoFrameRate = this.GuessFrameRate();
        this._elapsedTimeSinceBase = (float) (this._bufferSize / 2) / this._videoFrameRate;
      }
      if (this._mediaPlayer.Control.IsPaused())
        this.InvalidateBuffer();
      if (flag3 || !this._mediaPlayer.Control.IsPlaying() || this._mediaPlayer.Control.IsFinished())
        return;
      long num = this._buffer[0][(this._start + this._bufferSize / 2) % this._bufferSize].timestamp - this._baseTimestamp;
      if ((double) Mathf.Abs(this._elapsedTimeSinceBase * 1E+07f - (float) num) > (double) (this._buffer[0].Length / 2) / (double) this._videoFrameRate * 10000000.0)
        this._elapsedTimeSinceBase = (float) num / 1E+07f;
      if (this._resampleMode == Resampler.ResampleMode.POINT)
        this.PointUpdate();
      else if (this._resampleMode == Resampler.ResampleMode.LINEAR)
        this.LinearUpdate();
      this._elapsedTimeSinceBase += Time.unscaledDeltaTime;
    }

    public void UpdateTimestamp()
    {
      if (this._lastDisplayedTimestamp != this._currentDisplayedTimestamp)
      {
        this._lastDisplayedTimestamp = this._currentDisplayedTimestamp;
        this._frameDisplayedTimer = 0;
      }
      ++this._frameDisplayedTimer;
    }

    private class TimestampedRenderTexture
    {
      public RenderTexture texture;
      public long timestamp;
      public bool used;
    }

    public enum ResampleMode
    {
      POINT,
      LINEAR,
    }
  }
}
