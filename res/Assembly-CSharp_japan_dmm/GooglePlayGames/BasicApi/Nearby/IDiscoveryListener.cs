// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.Nearby.IDiscoveryListener
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace GooglePlayGames.BasicApi.Nearby
{
  public interface IDiscoveryListener
  {
    void OnEndpointFound(EndpointDetails discoveredEndpoint);

    void OnEndpointLost(string lostEndpointId);
  }
}
