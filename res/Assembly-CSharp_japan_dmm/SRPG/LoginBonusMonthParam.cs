// Decompiled with JetBrains decompiler
// Type: SRPG.LoginBonusMonthParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class LoginBonusMonthParam
  {
    private LoginBonusMonthState m_State;
    private int m_Day;

    public LoginBonusMonthParam(LoginBonusMonthState state, int day)
    {
      this.m_State = state;
      this.m_Day = day;
    }

    public LoginBonusMonthState State => this.m_State;

    public int Day => this.m_Day;
  }
}
