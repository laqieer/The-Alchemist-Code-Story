// Decompiled with JetBrains decompiler
// Type: MsgPack.Compiler.Variable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
