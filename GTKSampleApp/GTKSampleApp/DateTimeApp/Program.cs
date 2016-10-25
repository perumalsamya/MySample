using System;
using System.IO;
using System.Reflection;
using Lucene.Net;
using Lucene.Net.Store;

namespace DateTimeApp
{
	public class MainClass
	{
		public static void Main (string[] args)
		{
			//var path1 = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments), "Index");
			////Lucene.Net.Store.Directory directory = FSDirectory.Open (Path.GetDirectoryName (Assembly.GetExecutingAssembly ().Location) + Path.DirectorySeparatorChar + "Index" + Path.DirectorySeparatorChar);
			////Lucene.Net.Store.Directory directory=FSDirectory.Open(path1);
			//Lucene.Net.Store.Directory directory=FSDirectory.Open(new DirectoryInfo(Environment.CurrentDirectory+ Path.DirectorySeparatorChar + "Index" + Path.DirectorySeparatorChar));
			//const string path = @"/home/aditi/Desktop/Hello.txt";
			//var dir = Path.GetDirectoryName (Assembly.GetExecutingAssembly ().Location) + Path.DirectorySeparatorChar + "Index" + Path.DirectorySeparatorChar;
			//TextWriter tsw = !File.Exists(path) ? new StreamWriter(path, true) : File.AppendText(path);
			////Writing text to the file.
			//tsw.WriteLine("Log is inserted at : {0}", DateTime.Now);
			//tsw.Close ();
		}			
	}
}
	