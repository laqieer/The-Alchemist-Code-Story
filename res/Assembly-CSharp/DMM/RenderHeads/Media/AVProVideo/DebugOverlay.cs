// Decompiled with JetBrains decompiler
// Type: RenderHeads.Media.AVProVideo.DebugOverlay
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace RenderHeads.Media.AVProVideo
{
  [AddComponentMenu("AVPro Video/Debug Overlay", -99)]
  public class DebugOverlay : MonoBehaviour
  {
    [SerializeField]
    private MediaPlayer _mediaPlayer;
    [SerializeField]
    private int _guiDepth = -1000;
    [SerializeField]
    private float _displaySize = 1f;
    private int _debugOverlayCount;
    [SerializeField]
    private bool _displayControls = true;
    private const int s_GuiStartWidth = 10;
    private const int s_GuiWidth = 180;

    public bool DisplayControls
    {
      get => this._displayControls;
      set => this._displayControls = value;
    }

    public MediaPlayer CurrentMediaPlayer
    {
      get => this._mediaPlayer;
      set
      {
        if (!Object.op_Inequality((Object) this._mediaPlayer, (Object) value))
          return;
        this._mediaPlayer = value;
      }
    }

    private void SetGuiPositionFromVideoIndex(int index)
    {
    }

    private void Update()
    {
      this._debugOverlayCount = 0;
      this._guiDepth = -1000;
      this._displaySize = 1f;
      this._debugOverlayCount = 0;
    }
  }
}
