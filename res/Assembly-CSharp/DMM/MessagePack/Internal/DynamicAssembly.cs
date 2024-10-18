// Decompiled with JetBrains decompiler
// Type: MessagePack.Internal.DynamicAssembly
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Reflection;
using System.Reflection.Emit;

#nullable disable
namespace MessagePack.Internal
{
  internal class DynamicAssembly
  {
    private readonly AssemblyBuilder assemblyBuilder;
    private readonly ModuleBuilder moduleBuilder;
    private readonly object gate = new object();

    public DynamicAssembly(string moduleName)
    {
      this.assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(new AssemblyName(moduleName), AssemblyBuilderAccess.Run);
      this.moduleBuilder = this.assemblyBuilder.DefineDynamicModule(moduleName);
    }

    public TypeBuilder DefineType(string name, TypeAttributes attr)
    {
      lock (this.gate)
        return this.moduleBuilder.DefineType(name, attr);
    }

    public TypeBuilder DefineType(string name, TypeAttributes attr, Type parent)
    {
      lock (this.gate)
        return this.moduleBuilder.DefineType(name, attr, parent);
    }

    public TypeBuilder DefineType(
      string name,
      TypeAttributes attr,
      Type parent,
      Type[] interfaces)
    {
      lock (this.gate)
        return this.moduleBuilder.DefineType(name, attr, parent, interfaces);
    }
  }
}
