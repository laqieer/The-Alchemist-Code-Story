// Decompiled with JetBrains decompiler
// Type: RenderHeads.Media.AVProVideo.MediaPlaylist
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace RenderHeads.Media.AVProVideo
{
  [Serializable]
  public class MediaPlaylist
  {
    [SerializeField]
    private List<MediaPlaylist.MediaItem> _items = new List<MediaPlaylist.MediaItem>(8);

    public List<MediaPlaylist.MediaItem> Items => this._items;

    public bool HasItemAt(int index) => index >= 0 && index < this._items.Count;

    [Serializable]
    public class MediaItem
    {
      [SerializeField]
      public MediaPlayer.FileLocation fileLocation = MediaPlayer.FileLocation.RelativeToStreamingAssetsFolder;
      [SerializeField]
      public string filePath;
      [SerializeField]
      public bool loop;
      [SerializeField]
      public PlaylistMediaPlayer.StartMode startMode;
      [SerializeField]
      public PlaylistMediaPlayer.ProgressMode progressMode;
      [SerializeField]
      public float progressTimeSeconds = 0.5f;
      [SerializeField]
      public bool autoPlay = true;
      [SerializeField]
      public StereoPacking stereoPacking;
      [SerializeField]
      public AlphaPacking alphaPacking;
      [SerializeField]
      public bool isOverrideTransition;
      [SerializeField]
      public PlaylistMediaPlayer.Transition overrideTransition;
      [SerializeField]
      public float overrideTransitionDuration = 1f;
      [SerializeField]
      public PlaylistMediaPlayer.Easing overrideTransitionEasing;
    }
  }
}
