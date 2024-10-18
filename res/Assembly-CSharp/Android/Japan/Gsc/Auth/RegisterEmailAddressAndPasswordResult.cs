// Decompiled with JetBrains decompiler
// Type: Gsc.Auth.RegisterEmailAddressAndPasswordResult
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Runtime.InteropServices;

namespace Gsc.Auth
{
  [StructLayout(LayoutKind.Sequential, Size = 1)]
  public struct RegisterEmailAddressAndPasswordResult
  {
    public RegisterEmailAddressAndPasswordResult(RegisterEmailAddressAndPasswordResultCode resultCode)
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

    public static bool operator ==(RegisterEmailAddressAndPasswordResult self, RegisterEmailAddressAndPasswordResultCode resultCode)
    {
      return self.ResultCode == resultCode;
    }

    public static bool operator !=(RegisterEmailAddressAndPasswordResult self, RegisterEmailAddressAndPasswordResultCode resultCode)
    {
      return self.ResultCode != resultCode;
    }

    public override bool Equals(object obj)
    {
      return base.Equals(obj);
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }
  }
}
