// Decompiled with JetBrains decompiler
// Type: Gsc.Auth.AddDeviceWithEmailAddressAndPasswordResult
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Runtime.InteropServices;

namespace Gsc.Auth
{
  [StructLayout(LayoutKind.Sequential, Size = 1)]
  public struct AddDeviceWithEmailAddressAndPasswordResult
  {
    public AddDeviceWithEmailAddressAndPasswordResult(AddDeviceWithEmailAddressAndPasswordResultCode resultCode)
    {
      this = new AddDeviceWithEmailAddressAndPasswordResult(resultCode, 0, 0);
    }

    public AddDeviceWithEmailAddressAndPasswordResult(AddDeviceWithEmailAddressAndPasswordResultCode resultCode, int lockedExpiresIn, int trialCounter)
    {
      this.ResultCode = resultCode;
      this.LockedExpiresIn = lockedExpiresIn;
      this.TrialCounter = trialCounter;
    }

    public AddDeviceWithEmailAddressAndPasswordResultCode ResultCode { get; private set; }

    public int LockedExpiresIn { get; private set; }

    public int TrialCounter { get; private set; }

    public static bool operator true(AddDeviceWithEmailAddressAndPasswordResult self)
    {
      return self.ResultCode == AddDeviceWithEmailAddressAndPasswordResultCode.Success;
    }

    public static bool operator false(AddDeviceWithEmailAddressAndPasswordResult self)
    {
      return self.ResultCode != AddDeviceWithEmailAddressAndPasswordResultCode.Success;
    }

    public static bool operator ==(AddDeviceWithEmailAddressAndPasswordResult self, AddDeviceWithEmailAddressAndPasswordResultCode resultCode)
    {
      return self.ResultCode == resultCode;
    }

    public static bool operator !=(AddDeviceWithEmailAddressAndPasswordResult self, AddDeviceWithEmailAddressAndPasswordResultCode resultCode)
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
