// Decompiled with JetBrains decompiler
// Type: Gsc.Auth.AddDeviceWithEmailAddressAndPasswordResult
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace Gsc.Auth
{
  public struct AddDeviceWithEmailAddressAndPasswordResult
  {
    public AddDeviceWithEmailAddressAndPasswordResult(
      AddDeviceWithEmailAddressAndPasswordResultCode resultCode)
      : this(resultCode, 0, 0)
    {
    }

    public AddDeviceWithEmailAddressAndPasswordResult(
      AddDeviceWithEmailAddressAndPasswordResultCode resultCode,
      int lockedExpiresIn,
      int trialCounter)
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

    public static bool operator ==(
      AddDeviceWithEmailAddressAndPasswordResult self,
      AddDeviceWithEmailAddressAndPasswordResultCode resultCode)
    {
      return self.ResultCode == resultCode;
    }

    public static bool operator !=(
      AddDeviceWithEmailAddressAndPasswordResult self,
      AddDeviceWithEmailAddressAndPasswordResultCode resultCode)
    {
      return self.ResultCode != resultCode;
    }

    public override bool Equals(object obj) => base.Equals(obj);

    public override int GetHashCode() => base.GetHashCode();
  }
}
