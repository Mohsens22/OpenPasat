using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace UnoTest.Wasm
{
	static class MonoInternals
	{
		[DllImport("__Native")]
		internal static extern void mono_trace_enable(int enable);
		[DllImport("__Native")]
		internal static extern int mono_trace_set_options(string options);
	}
}
