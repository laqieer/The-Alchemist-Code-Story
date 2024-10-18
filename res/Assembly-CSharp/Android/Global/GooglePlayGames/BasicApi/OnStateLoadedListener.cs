// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.OnStateLoadedListener
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace GooglePlayGames.BasicApi
{
  public interface OnStateLoadedListener
  {
    void OnStateLoaded(bool success, int slot, byte[] data);

    byte[] OnStateConflict(int slot, byte[] localData, byte[] serverData);

    void OnStateSaved(bool success, int slot);
  }
}
