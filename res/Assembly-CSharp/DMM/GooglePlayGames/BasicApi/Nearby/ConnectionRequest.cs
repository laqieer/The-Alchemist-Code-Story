// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.Nearby.ConnectionRequest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GooglePlayGames.OurUtils;

#nullable disable
namespace GooglePlayGames.BasicApi.Nearby
{
  public struct ConnectionRequest
  {
    private readonly EndpointDetails mRemoteEndpoint;
    private readonly byte[] mPayload;

    public ConnectionRequest(
      string remoteEndpointId,
      string remoteEndpointName,
      string serviceId,
      byte[] payload)
    {
      Logger.d("Constructing ConnectionRequest");
      this.mRemoteEndpoint = new EndpointDetails(remoteEndpointId, remoteEndpointName, serviceId);
      this.mPayload = Misc.CheckNotNull<byte[]>(payload);
    }

    public EndpointDetails RemoteEndpoint => this.mRemoteEndpoint;

    public byte[] Payload => this.mPayload;
  }
}
