// Decompiled with JetBrains decompiler
// Type: Gsc.Auth.RegisterEmailAddressAndPasswordResult
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace Gsc.Auth
{
  public struct RegisterEmailAddressAndPasswordResult
  {
    public RegisterEmailAddressAndPasswordResult(
      RegisterEmailAddressAndPasswordResultCode resultCode)
    {
      this.ResultCode = resultCode;
    }

    public RegisterEmailAddressAndPasswordResultCode ResultCode { get; private set; }

    public static bool operator true(RegisterEmailAddressAndPasswordResult self)
    {
      return self.ResultCode == RegisterEmailAddressAndPasswordResultCode.Success;
    }

    public static bool operator false(RegisterEmailAddressAndPasswordResult self)
    {
      return self.ResultCode != RegisterEmailAddressAndPasswordResultCode.Success;
    }

    public static bool operator ==(
      RegisterEmailAddressAndPasswordResult self,
      RegisterEmailAddressAndPasswordResultCode resultCode)
    {
      return self.ResultCode == resultCode;
    }

    public static bool operator !=(
      RegisterEmailAddressAndPasswordResult self,
      RegisterEmailAddressAndPasswordResultCode resultCode)
    {
      return self.ResultCode != resultCode;
    }

    public override bool Equals(object obj) => base.Equals(obj);

    public override int GetHashCode() => base.GetHashCode();
  }
}
