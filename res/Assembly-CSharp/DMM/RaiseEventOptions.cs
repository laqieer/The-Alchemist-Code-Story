// Decompiled with JetBrains decompiler
// Type: RaiseEventOptions
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
public class RaiseEventOptions
{
  public static readonly RaiseEventOptions Default = new RaiseEventOptions();
  public EventCaching CachingOption;
  public byte InterestGroup;
  public int[] TargetActors;
  public ReceiverGroup Receivers;
  public byte SequenceChannel;
  public bool ForwardToWebhook;
  public bool Encrypt;

  public void Reset()
  {
    this.CachingOption = RaiseEventOptions.Default.CachingOption;
    this.InterestGroup = RaiseEventOptions.Default.InterestGroup;
    this.TargetActors = RaiseEventOptions.Default.TargetActors;
    this.Receivers = RaiseEventOptions.Default.Receivers;
    this.SequenceChannel = RaiseEventOptions.Default.SequenceChannel;
    this.ForwardToWebhook = RaiseEventOptions.Default.ForwardToWebhook;
    this.Encrypt = RaiseEventOptions.Default.Encrypt;
  }
}
