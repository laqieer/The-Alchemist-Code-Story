// Decompiled with JetBrains decompiler
// Type: MsgPack.Compiler.Variable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Reflection.Emit;

namespace MsgPack.Compiler
{
  public class Variable
  {
    private Variable(VariableType type, int index)
    {
      this.VarType = type;
      this.Index = index;
    }

    public static Variable CreateLocal(LocalBuilder local)
    {
      return new Variable(VariableType.Local, local.LocalIndex);
    }

    public static Variable CreateArg(int idx)
    {
      return new Variable(VariableType.Arg, idx);
    }

    public VariableType VarType { get; set; }

    public int Index { get; set; }
  }
}
