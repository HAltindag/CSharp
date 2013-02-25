using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

namespace Scratch
{

	using Scratch.Repository;
	using System.Collections;

	class Program
	{

		static void Main(string[] args)
		{
			//string strFoldername = @"C:\dev\Interview\WordLists";
			string strFoldername = @"..\..\..\..\WordLists";
			

			#region display

			var parser = new WordListParser(strFoldername);

			Console.WriteLine("\nPlease wait...");
			//loop through and display all the text in each text file
			foreach (var word in parser.ParseList())
			{
				//get first char of word
				string strFirstchar = word.ToString().Substring(0, 1);

				Console.WriteLine("\n"+strFirstchar.ToUpper() + " Words" + "\nPress any key to continue...");
				Console.ReadKey();
				Console.WriteLine(word);
				//Console.Clear();
			}//foreach

			Console.WriteLine("\nEnd of programm...Press any key");
			Console.ReadKey();
			#endregion

		}
	

	}
}
