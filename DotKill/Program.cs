using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using dnlib.DotNet;
using dnlib.DotNet.Writer;
using DotKill.KillProtect;

internal class Program
{
	private static void Main(string[] args)
	{
		string loadPath = args[0];
		ModuleDefMD module = ModuleDefMD.Load(loadPath);
		string junkantide4dot = AntiDe4Dot.Execute(module);
		string dedot = junkantide4dot.Split('+')[0];
		string junk = junkantide4dot.Split('+')[1];
		Console.WriteLine($"Anti De4Dot ........: {dedot}");
		Console.WriteLine($"Junk ...............: {junk}");
		Console.WriteLine($"Maths ..............: {MathProtection.Execute(module)}");
		Console.WriteLine($"Anti Decompiler ....: {AntiDecompiler.Execute(module)}");
		Console.WriteLine($"CFlow ..............: {CFlow.Execute(module)}");
		string text2 = Path.GetDirectoryName(loadPath);
		string fileName = Path.GetFileNameWithoutExtension(loadPath) + "_dotkill" + Path.GetExtension(loadPath);
		string savePath = text2 != null ? Path.Combine(text2, fileName) : fileName;
		module.Write(savePath, new ModuleWriterOptions(module)
		{
			PEHeadersOptions =
			{
				NumberOfRvaAndSizes = 13u
			},
			Logger = DummyLogger.NoThrowInstance
		});
		Console.Write("Assembly saved .....: ");
		Console.WriteLine(savePath);
	}
}
