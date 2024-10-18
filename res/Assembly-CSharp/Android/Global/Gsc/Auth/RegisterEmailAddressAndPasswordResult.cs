// Decompiled with JetBrains decompiler
// Type: Gsc.Auth.RegisterEmailAddressAndPasswordResult
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace Gsc.Auth
{
  public struct RegisterEmailAddressAndPasswordResult
  {
    public RegisterEmailAddressAndPasswordResult(RegisterEmailAddressAndPasswordResultCode resultCode)
    {
      this.ResultCode = resultCode;
    }

    public RegisterEmailAddressAndPasswordResultCode ResultCode { get; private set; }

    public override bool Equals(object obj)
    {
      return base.Equals(obj);
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    public static bool operator true(RegisterEmailAddressAndPasswordResult self)
    {
      return self.ResultCode == RegisterEmailAddressAndPasswordResultCode.Success;
    }

    public static bool operator false(RegisterEmailAddressAndPasswordResult self)
    {
      return self.ResultCode != RegisterEmailAddressAndPasswordResultCode.Success;
    }

    public static bool operator ==(RegisterEmailAddressAndPasswordResult self, RegisterEmailAddressAndPasswordResultCode resultCode)
    {
      return self.ResultCode == resultCode;
    }

    public static bool operator !=(RegisterEmailAddressAndPasswordResult self, RegisterEmailAddressAndPasswordResultCode resultCode)
    {
      return self.ResultCode != resultCode;
    }
  }
}
