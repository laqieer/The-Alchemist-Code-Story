// Decompiled with JetBrains decompiler
// Type: RaiseEventOptions
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
}
