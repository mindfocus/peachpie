﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeGen;
using Microsoft.CodeAnalysis.Emit;
using Pchp.CodeAnalysis.Emit;
using Pchp.CodeAnalysis.FlowAnalysis;
using Pchp.CodeAnalysis.Symbols;
using Pchp.CodeAnalysis.Semantics.Graph;
using System.Reflection.Metadata;
using System.Diagnostics;

namespace Pchp.CodeAnalysis.CodeGen
{
    partial class CodeGenerator
    {
        /// <summary>
        /// Gets value indicating the given type represents a double and nothing else.
        /// </summary>
        internal bool IsDoubleOnly(TypeRefMask tmask)
        {
            return tmask.IsSingleType && _routine.TypeRefContext.IsDouble(tmask);
        }

        /// <summary>
        /// Gets value indicating the given type represents a long and nothing else.
        /// </summary>
        internal bool IsLongOnly(TypeRefMask tmask)
        {
            return tmask.IsSingleType && _routine.TypeRefContext.IsLong(tmask);
        }

        /// <summary>The given type represents only an integer, long or double.</summary>
        internal bool IsNumberOnly(TypeRefMask tmask)
        {
            return !tmask.IsVoid && !tmask.IsAnyType && _routine.TypeRefContext.GetTypes(tmask)
                .All(x => x.TypeCode == Core.PhpTypeCode.Long || x.TypeCode == Core.PhpTypeCode.Int32 || x.TypeCode == Core.PhpTypeCode.Double);
        }

        /// <summary>
        /// Gets value indicating the given type represents a long and nothing else.
        /// </summary>
        internal bool IsBooleanOnly(TypeRefMask tmask)
        {
            return tmask.IsSingleType && _routine.TypeRefContext.IsBoolean(tmask);
        }

        /// <summary>
        /// Gets value indicating the given type represents UTF16 readonly string and nothing else.
        /// </summary>
        internal bool IsReadonlyStringOnly(TypeRefMask tmask)
        {
            return tmask.IsSingleType && _routine.TypeRefContext.IsReadonlyString(tmask);
        }

        /// <summary>
        /// Gets value indicating the given type represents only class types.
        /// </summary>
        internal bool IsClassOnly(TypeRefMask tmask)
        {
            return !tmask.IsVoid && !tmask.IsAnyType && _routine.TypeRefContext.GetTypes(tmask).All(x => x.IsObject);
        }

        /// <summary>
        /// Gets value indicating the given type represents only PHP Array.
        /// </summary>
        internal bool IsArrayOnly(TypeRefMask tmask)
        {
            return !tmask.IsVoid && !tmask.IsAnyType && _routine.TypeRefContext.GetTypes(tmask).All(x => x.IsArray);
        }

        /// <summary>
        /// Determines whether the type needs to be copied when passing by value.
        /// </summary>
        internal bool IsCopiable(TypeSymbol t)
        {
            return
                t == CoreTypes.PhpValue ||
                t == CoreTypes.PhpString ||
                t == CoreTypes.PhpArray;
        }
    }
}
