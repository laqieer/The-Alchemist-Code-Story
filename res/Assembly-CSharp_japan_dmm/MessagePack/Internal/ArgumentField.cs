// Decompiled with JetBrains decompiler
// Type: MessagePack.Internal.ArgumentField
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Reflection;
using System.Reflection.Emit;

#nullable disable
namespace MessagePack.Internal
{
  internal struct ArgumentField
  {
    private readonly int i;
    private readonly bool @ref;
    private readonly ILGenerator il;

    public ArgumentField(ILGenerator il, int i, bool @ref = false)
    {
      this.il = il;
      this.i = i;
      this.@ref = @ref;
    }

    public ArgumentField(ILGenerator il, int i, Type type)
    {
      this.il = il;
      this.i = i;
      TypeInfo typeInfo = System.Reflection.ReflectionExtensions.GetTypeInfo(type);
      this.@ref = !typeInfo.IsClass && !typeInfo.IsInterface && !typeInfo.IsAbstract;
    }

    public void EmitLoad()
    {
      if (this.@ref)
        this.il.EmitLdarga(this.i);
      else
        this.il.EmitLdarg(this.i);
    }

    public void EmitLdarg() => this.il.EmitLdarg(this.i);

    public void EmitLdarga() => this.il.EmitLdarga(this.i);

    public void EmitStore() => this.il.EmitStarg(this.i);
  }
}
