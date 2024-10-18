// Decompiled with JetBrains decompiler
// Type: RenderHeads.Media.AVProVideo.AudioOutputManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace RenderHeads.Media.AVProVideo
{
  public class AudioOutputManager
  {
    private static AudioOutputManager _instance;
    private Dictionary<MediaPlayer, HashSet<AudioOutput>> _accessTrackers;
    private Dictionary<MediaPlayer, float[]> _pcmData;

    private AudioOutputManager()
    {
      this._accessTrackers = new Dictionary<MediaPlayer, HashSet<AudioOutput>>();
      this._pcmData = new Dictionary<MediaPlayer, float[]>();
    }

    public static AudioOutputManager Instance
    {
      get
      {
        if (AudioOutputManager._instance == null)
          AudioOutputManager._instance = new AudioOutputManager();
        return AudioOutputManager._instance;
      }
    }

    public void RequestAudio(
      AudioOutput _outputComponent,
      MediaPlayer mediaPlayer,
      float[] data,
      int channelMask,
      int totalChannels,
      AudioOutput.AudioOutputMode audioOutputMode)
    {
      if (Object.op_Equality((Object) mediaPlayer, (Object) null) || mediaPlayer.Control == null || !mediaPlayer.Control.IsPlaying())
        return;
      int numAudioChannels = mediaPlayer.Control.GetNumAudioChannels();
      if (numAudioChannels <= 0)
        return;
      if (!this._accessTrackers.ContainsKey(mediaPlayer))
        this._accessTrackers[mediaPlayer] = new HashSet<AudioOutput>();
      if (this._accessTrackers[mediaPlayer].Contains(_outputComponent) || this._accessTrackers[mediaPlayer].Count == 0 || this._pcmData[mediaPlayer] == null)
      {
        this._accessTrackers[mediaPlayer].Clear();
        int length = data.Length / totalChannels * numAudioChannels;
        this._pcmData[mediaPlayer] = new float[length];
        this.GrabAudio(mediaPlayer, this._pcmData[mediaPlayer], numAudioChannels);
        this._accessTrackers[mediaPlayer].Add(_outputComponent);
      }
      int num1 = Math.Min(data.Length / totalChannels, this._pcmData[mediaPlayer].Length / numAudioChannels);
      int num2 = 0;
      int num3 = 0;
      switch (audioOutputMode)
      {
        case AudioOutput.AudioOutputMode.Single:
          int num4 = 0;
          for (int index = 0; index < 8; ++index)
          {
            if ((channelMask & 1 << index) > 0)
            {
              num4 = index;
              break;
            }
          }
          if (num4 >= numAudioChannels)
            break;
          for (int index1 = 0; index1 < num1; ++index1)
          {
            for (int index2 = 0; index2 < totalChannels; ++index2)
              data[num3 + index2] = this._pcmData[mediaPlayer][num2 + num4];
            num2 += numAudioChannels;
            num3 += totalChannels;
          }
          break;
        case AudioOutput.AudioOutputMode.Multiple:
          int num5 = Math.Min(numAudioChannels, totalChannels);
          for (int index3 = 0; index3 < num1; ++index3)
          {
            for (int index4 = 0; index4 < num5; ++index4)
            {
              if ((1 << index4 & channelMask) > 0)
                data[num3 + index4] = this._pcmData[mediaPlayer][num2 + index4];
            }
            num2 += numAudioChannels;
            num3 += totalChannels;
          }
          break;
      }
    }

    private void GrabAudio(MediaPlayer player, float[] data, int channels)
    {
      player.Control.GrabAudio(data, data.Length, channels);
    }
  }
}
