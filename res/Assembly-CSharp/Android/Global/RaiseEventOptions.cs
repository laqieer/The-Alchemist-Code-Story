// Decompiled with JetBrains decompiler
// Type: RaiseEventOptions
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
