// Decompiled with JetBrains decompiler
// Type: Gsc.Auth.AddDeviceWithEmailAddressAndPasswordResult
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace Gsc.Auth
{
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

    public override bool Equals(object obj)
    {
      return base.Equals(obj);
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

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
  }
}
