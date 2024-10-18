// Decompiled with JetBrains decompiler
// Type: SoundSettings
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
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
      if (Object.op_Equality((Object) SoundSettings.mInstance, (Object) null))
      {
        SoundSettings.mInstance = Resources.Load<SoundSettings>(nameof (SoundSettings));
        Object.DontDestroyOnLoad((Object) SoundSettings.mInstance);
      }
      return SoundSettings.mInstance;
    }
  }
}
