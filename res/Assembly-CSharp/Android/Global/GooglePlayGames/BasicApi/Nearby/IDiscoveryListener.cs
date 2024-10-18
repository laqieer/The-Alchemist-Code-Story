// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.Nearby.IDiscoveryListener
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace GooglePlayGames.BasicApi.Nearby
{
  public interface IDiscoveryListener
  {
    void OnEndpointFound(EndpointDetails discoveredEndpoint);

    void OnEndpointLost(string lostEndpointId);
  }
}
