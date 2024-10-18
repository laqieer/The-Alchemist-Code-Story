// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.PInvoke.GameServices
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.PInvoke
{
  internal class GameServices : BaseReferenceHolder
  {
    internal GameServices(IntPtr selfPointer)
      : base(selfPointer)
    {
    }

    internal bool IsAuthenticated()
    {
      return GooglePlayGames.Native.Cwrapper.GameServices.GameServices_IsAuthorized(this.SelfPtr());
    }

    internal void SignOut()
    {
      GooglePlayGames.Native.Cwrapper.GameServices.GameServices_SignOut(this.SelfPtr());
    }

    internal void StartAuthorizationUI()
    {
      GooglePlayGames.Native.Cwrapper.GameServices.GameServices_StartAuthorizationUI(this.SelfPtr());
    }

    public AchievementManager AchievementManager()
    {
      return new AchievementManager(this);
    }

    public LeaderboardManager LeaderboardManager()
    {
      return new LeaderboardManager(this);
    }

    public PlayerManager PlayerManager()
    {
      return new PlayerManager(this);
    }

    internal HandleRef AsHandle()
    {
      return this.SelfPtr();
    }

    protected override void CallDispose(HandleRef selfPointer)
    {
      GooglePlayGames.Native.Cwrapper.GameServices.GameServices_Dispose(selfPointer);
    }
  }
}
