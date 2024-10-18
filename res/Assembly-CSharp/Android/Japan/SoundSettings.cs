// Decompiled with JetBrains decompiler
// Type: SoundSettings
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class SoundSettings : ScriptableObject
{
  public string Tap = "0002";
  public string OK = "0003";
  public string Cancel = "0004";
  public string Select = "0005";
  public string Buzzer = "0006";
  public string Swipe = "0007";
  public string ScrollList = "0008";
  public string WindowPop = "0009";
  public string WindowClose = "0010";
  public const float BGMCrossFadeTime = 1f;
  private static SoundSettings mInstance;

  public static SoundSettings Current
  {
    get
    {
      if ((Object) SoundSettings.mInstance == (Object) null)
      {
        SoundSettings.mInstance = Resources.Load<SoundSettings>(nameof (SoundSettings));
        Object.DontDestroyOnLoad((Object) SoundSettings.mInstance);
      }
      return SoundSettings.mInstance;
    }
  }
}
