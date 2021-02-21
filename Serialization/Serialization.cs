using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Serialization
{
    public class Serialization
    {
		public static byte[] ObjectToByteArray(Object obj)
		{
			if (obj == null)
				return null;
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			//Убирате привязку к сборке Serialization
			binaryFormatter.AssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple;
			MemoryStream memoryStream = new MemoryStream();
			binaryFormatter.Serialize(memoryStream, obj);

			return memoryStream.ToArray();
		}
		public static Object ByteArrayToObject(byte[] arrayByte)
		{
			MemoryStream memoryStream = new MemoryStream();
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			binaryFormatter.Binder = new Binder();
			//Убирате привязку к сборке Serialization
			binaryFormatter.AssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple;
			memoryStream.Write(arrayByte, 0, arrayByte.Length);
			memoryStream.Seek(0, SeekOrigin.Begin);
			Object obj = (Object)binaryFormatter.Deserialize(memoryStream);

			return obj;
		}
	}
	public class Binder : SerializationBinder
	{
		public override Type BindToType(string assemblyName, string typeName)
		{
			Assembly currentasm = Assembly.GetExecutingAssembly();

			return Type.GetType($"{currentasm.GetName().Name}.{typeName.Split('.')[1]}");
		}
	}
	[Serializable]
	public class ObjectAction
	{
		public int X { get; set; }
		public int Y { get; set; }
		public int Speed { get; set; }
		public bool Up { get; set; } = true;
		public bool Down { get; set; } = false;
		public bool Left { get; set; } = false;
		public bool Righ { get; set; } = false;
		public bool Shot { get; set; } = false;
	}
}
