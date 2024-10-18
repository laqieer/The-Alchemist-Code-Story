// Decompiled with JetBrains decompiler
// Type: MessagePack.Resolvers.DynamicEnumResolver
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Formatters;
using MessagePack.Internal;
using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;

#nullable disable
namespace MessagePack.Resolvers
{
  public sealed class DynamicEnumResolver : IFormatterResolver
  {
    public static readonly DynamicEnumResolver Instance = new DynamicEnumResolver();
    private const string ModuleName = "MessagePack.Resolvers.DynamicEnumResolver";
    private static readonly DynamicAssembly assembly;
    private static int nameSequence = 0;

    private DynamicEnumResolver()
    {
    }

    static DynamicEnumResolver()
    {
      DynamicEnumResolver.assembly = new DynamicAssembly("MessagePack.Resolvers.DynamicEnumResolver");
    }

    public IMessagePackFormatter<T> GetFormatter<T>()
    {
      return DynamicEnumResolver.FormatterCache<T>.formatter;
    }

    private static TypeInfo BuildType(Type enumType)
    {
      Type underlyingType = Enum.GetUnderlyingType(enumType);
      Type type1 = typeof (IMessagePackFormatter<>).MakeGenericType(enumType);
      TypeBuilder type2 = DynamicEnumResolver.assembly.DefineType("MessagePack.Formatters." + enumType.FullName.Replace(".", "_") + "Formatter" + (object) Interlocked.Increment(ref DynamicEnumResolver.nameSequence), TypeAttributes.Public | TypeAttributes.Sealed, (Type) null, new Type[1]
      {
        type1
      });
      ILGenerator ilGenerator1 = type2.DefineMethod("Serialize", MethodAttributes.Public | MethodAttributes.Final | MethodAttributes.Virtual, typeof (int), new Type[4]
      {
        typeof (byte[]).MakeByRefType(),
        typeof (int),
        enumType,
        typeof (IFormatterResolver)
      }).GetILGenerator();
      ilGenerator1.Emit(OpCodes.Ldarg_1);
      ilGenerator1.Emit(OpCodes.Ldarg_2);
      ilGenerator1.Emit(OpCodes.Ldarg_3);
      ilGenerator1.Emit(OpCodes.Call, System.Reflection.ReflectionExtensions.GetRuntimeMethod(typeof (MessagePackBinary), "Write" + underlyingType.Name, new Type[3]
      {
        typeof (byte[]).MakeByRefType(),
        typeof (int),
        underlyingType
      }));
      ilGenerator1.Emit(OpCodes.Ret);
      ILGenerator ilGenerator2 = type2.DefineMethod("Deserialize", MethodAttributes.Public | MethodAttributes.Final | MethodAttributes.Virtual, enumType, new Type[4]
      {
        typeof (byte[]),
        typeof (int),
        typeof (IFormatterResolver),
        typeof (int).MakeByRefType()
      }).GetILGenerator();
      ilGenerator2.Emit(OpCodes.Ldarg_1);
      ilGenerator2.Emit(OpCodes.Ldarg_2);
      ilGenerator2.Emit(OpCodes.Ldarg_S, (byte) 4);
      ilGenerator2.Emit(OpCodes.Call, System.Reflection.ReflectionExtensions.GetRuntimeMethod(typeof (MessagePackBinary), "Read" + underlyingType.Name, new Type[3]
      {
        typeof (byte[]),
        typeof (int),
        typeof (int).MakeByRefType()
      }));
      ilGenerator2.Emit(OpCodes.Ret);
      return System.Reflection.ReflectionExtensions.CreateTypeInfo(type2);
    }

    private static class FormatterCache<T>
    {
      public static readonly IMessagePackFormatter<T> formatter;

      static FormatterCache()
      {
        TypeInfo typeInfo1 = System.Reflection.ReflectionExtensions.GetTypeInfo(typeof (T));
        if (typeInfo1.IsNullable())
        {
          TypeInfo typeInfo2 = System.Reflection.ReflectionExtensions.GetTypeInfo(typeInfo1.GenericTypeArguments[0]);
          if (!typeInfo2.IsEnum)
            return;
          object formatterDynamic = DynamicEnumResolver.Instance.GetFormatterDynamic(typeInfo2.AsType());
          if (formatterDynamic == null)
            return;
          DynamicEnumResolver.FormatterCache<T>.formatter = (IMessagePackFormatter<T>) Activator.CreateInstance(typeof (StaticNullableFormatter<>).MakeGenericType(typeInfo2.AsType()), formatterDynamic);
        }
        else
        {
          if (!typeInfo1.IsEnum)
            return;
          DynamicEnumResolver.FormatterCache<T>.formatter = (IMessagePackFormatter<T>) Activator.CreateInstance(DynamicEnumResolver.BuildType(typeof (T)).AsType());
        }
      }
    }
  }
}
