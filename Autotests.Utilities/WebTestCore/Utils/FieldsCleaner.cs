using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace Autotests.Utilities.WebTestCore.Utils
{
    internal class FieldsCleaner
    {
        private readonly ILGenerator il;
        private readonly LocalBuilder local;
        private readonly DynamicMethod method;
        private readonly Type type;

        public FieldsCleaner(Type type)
        {
            this.type = type;
            method = new DynamicMethod(Guid.NewGuid().ToString(), typeof (void), new[] {typeof (object)}, true);
            il = method.GetILGenerator();
            local = il.DeclareLocal(type);
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Castclass, type);
            il.Emit(OpCodes.Stloc, local);
            EmitCleanFileds();
            il.Emit(OpCodes.Ret);
        }

        public Action<object> GetDelegate()
        {
            return (Action<object>) method.CreateDelegate(typeof (Action<object>));
        }

        private void EmitRefTypeFieldClean(FieldInfo fieldInfo)
        {
            il.Emit(OpCodes.Ldloc, local);
            il.Emit(OpCodes.Ldnull);
            il.Emit(OpCodes.Stfld, fieldInfo);
        }

        private void EmitValueTypeFieldClean(FieldInfo fieldInfo)
        {
            il.Emit(OpCodes.Ldloc, local);
            il.Emit(OpCodes.Ldflda, fieldInfo);
            il.Emit(OpCodes.Initobj, fieldInfo.FieldType);
        }

        private void EmitCleanFileds()
        {
            Type currentType = type;
            while (currentType != null)
            {
                ClearObjectFileds(currentType);
                currentType = currentType.BaseType;
            }
        }

        private void ClearObjectFileds(Type objType)
        {
            FieldInfo[] fields = objType.GetFields(BindingFlags.Public | BindingFlags.NonPublic
                                                   | BindingFlags.SetField | BindingFlags.Instance | BindingFlags.Static)
                .Where(x => !x.GetCustomAttributes(typeof (DoNotCleanAttribute), true).Any())
                .ToArray();
            foreach (FieldInfo info in fields)
            {
                if (!info.IsInitOnly && !info.IsLiteral)
                {
                    if (info.FieldType.IsValueType)
                        EmitValueTypeFieldClean(info);
                    else
                        EmitRefTypeFieldClean(info);
                }
            }
        }
    }
}