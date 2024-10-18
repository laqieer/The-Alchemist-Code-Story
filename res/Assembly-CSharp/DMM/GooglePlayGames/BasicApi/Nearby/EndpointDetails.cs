// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.Nearby.EndpointDetails
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GooglePlayGames.OurUtils;

#nullable disable
namespace GooglePlayGames.BasicApi.Nearby
{
  public struct EndpointDetails
  {
    private readonly string mEndpointId;
    private readonly string mName;
    private readonly string mServiceId;

    public EndpointDetails(string endpointId, string name, string serviceId)
    {
      this.mEndpointId = Misc.CheckNotNull<string>(endpointId);
      this.mName = Misc.CheckNotNull<string>(name);
      this.mServiceId = Misc.CheckNotNull<string>(serviceId);
    }

    public string EndpointId => this.mEndpointId;

    public string Name => this.mName;

    public string ServiceId => this.mServiceId;
  }
}
