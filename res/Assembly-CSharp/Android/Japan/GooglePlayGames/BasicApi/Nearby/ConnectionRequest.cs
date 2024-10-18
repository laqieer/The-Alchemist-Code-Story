// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.Nearby.ConnectionRequest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GooglePlayGames.OurUtils;

namespace GooglePlayGames.BasicApi.Nearby
{
  public struct ConnectionRequest
  {
    private readonly EndpointDetails mRemoteEndpoint;
    private readonly byte[] mPayload;

    public ConnectionRequest(string remoteEndpointId, string remoteEndpointName, string serviceId, byte[] payload)
    {
      Logger.d("Constructing ConnectionRequest");
      this.mRemoteEndpoint = new EndpointDetails(remoteEndpointId, remoteEndpointName, serviceId);
      this.mPayload = Misc.CheckNotNull<byte[]>(payload);
    }

    public EndpointDetails RemoteEndpoint
    {
      get
      {
        return this.mRemoteEndpoint;
      }
    }

    public byte[] Payload
    {
      get
      {
        return this.mPayload;
      }
    }
  }
}
