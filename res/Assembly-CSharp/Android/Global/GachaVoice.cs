// Decompiled with JetBrains decompiler
// Type: GachaVoice
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

public class GachaVoice : MonoBehaviour
{
  public string DirectCharName = "uroboros";
  public string Play1CueName = "gacha_0011";
  public string Play2CueName = "gacha_0012";
  public string[] Play3Cuename = new string[3]
  {
    "gacha_0013",
    "gacha_0014",
    "gacha_0015"
  };
  public int Excites;
  private int excites;
  private string mCharName;
  private string mCueName;
  private MySound.Voice mVoice;

  private void Awake()
  {
    if (string.IsNullOrEmpty(this.DirectCharName))
      this.DirectCharName = "uroboros";
    this.mVoice = new MySound.Voice(this.DirectCharName);
    this.excites = 0;
    this.mCharName = this.DirectCharName;
  }

  public void Play1()
  {
    this.SetupCueName(this.Play1CueName);
    this.Play();
  }

  public void Play2()
  {
    this.SetupCueName(this.Play2CueName);
    this.Play();
  }

  public void Play3()
  {
    this.excites = this.Excites <= 0 || this.Excites >= 4 ? 0 : this.Excites - 1;
    this.SetupCueName(this.Play3Cuename[this.excites]);
    this.Play();
  }

  private void Play()
  {
    if (this.mVoice == null)
      return;
    this.mVoice.Play(this.mCueName, 0.0f, false);
  }

  public void Stop()
  {
    if (this.mVoice == null)
      return;
    this.mVoice.StopAll(0.0f);
  }

  public void Discard()
  {
    if (this.mVoice != null)
      this.mVoice.Cleanup();
    this.mVoice = (MySound.Voice) null;
    this.mCharName = (string) null;
  }

  public bool SetupCueName(string cuename)
  {
    if (string.IsNullOrEmpty(cuename) || string.IsNullOrEmpty(this.mCharName))
      return false;
    this.mCueName = MySound.Voice.ReplaceCharNameOfCueName(cuename, this.mCharName);
    return true;
  }
}
