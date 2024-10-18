// Decompiled with JetBrains decompiler
// Type: FlowNodePersistent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

public abstract class FlowNodePersistent : FlowNode
{
  protected override void Awake()
  {
    base.Awake();
    this.enabled = true;
  }
}
