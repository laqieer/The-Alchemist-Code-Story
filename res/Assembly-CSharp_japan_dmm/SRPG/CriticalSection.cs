// Decompiled with JetBrains decompiler
// Type: SRPG.CriticalSection
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;
using System.Diagnostics;

#nullable disable
namespace SRPG
{
  public static class CriticalSection
  {
    private static int[] mCounts = new int[32];
    private const int NumMasks = 4;

    public static CriticalSections GetActive()
    {
      CriticalSections active = (CriticalSections) 0;
      for (int index = 3; index >= 0; --index)
      {
        if (CriticalSection.mCounts[index] > 0)
          active |= (CriticalSections) (1 << index);
      }
      return active;
    }

    public static void ForceReset() => CriticalSection.mCounts = new int[32];

    public static void Enter(CriticalSections mask = CriticalSections.Default)
    {
      CriticalSections updateMask = (CriticalSections) 0;
      for (int index = 3; index >= 0; --index)
      {
        if ((mask & (CriticalSections) (1 << index)) != (CriticalSections) 0)
        {
          ++CriticalSection.mCounts[index];
          if (CriticalSection.mCounts[index] == 1)
            updateMask |= (CriticalSections) (1 << index);
        }
      }
      if (updateMask == (CriticalSections) 0)
        return;
      UIValidator.UpdateValidators(updateMask, CriticalSection.GetActive());
    }

    public static void Leave(CriticalSections mask = CriticalSections.Default)
    {
      CriticalSections updateMask = (CriticalSections) 0;
      for (int index = 3; index >= 0; --index)
      {
        if ((mask & (CriticalSections) (1 << index)) != (CriticalSections) 0)
        {
          --CriticalSection.mCounts[index];
          if (CriticalSection.mCounts[index] == 0)
            updateMask |= (CriticalSections) (1 << index);
        }
      }
      if (updateMask == (CriticalSections) 0)
        return;
      UIValidator.UpdateValidators(updateMask, CriticalSection.GetActive());
    }

    public static bool IsActive => CriticalSection.GetActive() != (CriticalSections) 0;

    [DebuggerHidden]
    public static IEnumerator Wait()
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      CriticalSection.\u003CWait\u003Ec__Iterator0 waitCIterator0 = new CriticalSection.\u003CWait\u003Ec__Iterator0();
      return (IEnumerator) waitCIterator0;
    }
  }
}
