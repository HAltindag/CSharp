using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Scratch.Repository;
using System.Collections;
using System.IO;

namespace Q2
{
	class Program
	{


		static int iArrayNumber = 0;

		static void Main(string[] args)
		{

			Anagrams anagrams = new Anagrams();
		//	string strFoldername = @"C:\dev\Interview\WordLists";
			string strFoldername = @"..\..\..\..\WordLists";
		

			List<string>[] allItems = new List<string>[1000];

			
			#region Anagrams

/* Anagrams are two different words or combinations of characters which have the same alphabets 
	and their counts. Therefore a particular set of alphabets can create many permutations of Anagrams.
 In other words, if we have the same set of characters in two words(strings) then they are Anagrams. 
*/
			ArrayList retList = new ArrayList();

			#region check directory
			var files = (string[])null;
			try
			{
				 files = Directory.GetFiles(strFoldername);
			}
			catch (Exception)
			{
				Console.WriteLine("\nError retrieving data from folder\nCheck C:\\Dev\\Interview\\WordLists exists\nPress any key to exit...");
				Console.ReadKey();
			}

			#endregion

			Console.WriteLine("\nPlease wait...");

			for (var ixFile = 0; ixFile < files.Length; ixFile++)
			{

				var file = new StreamReader(files[ixFile]);
				string line = file.ReadToEnd();
				string[] inputArr = line.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
				List<string> listTemp = new List<string>();

				var outputList = new List<string>();

				//find anagrams
				outputList = FnFindAnagrams(inputArr);
				//new instance of list array
				allItems[iArrayNumber] = new List<string>();

				#region loop through all anagrams, add members greater then 8 into "listTemp"

				foreach (var item in outputList)
				{
					if (item.Count() > 8)
					{
						//	Console.WriteLine(item);
						listTemp.Add(item);
					}
				}

				#endregion

				#region add "listTemp" into "allItems"

				allItems[iArrayNumber++].AddRange(listTemp);


				#endregion	

			}


			#region MyRegion

			//loop through the List of arrays
			for (int i = 0; i < allItems.Count(); i++)
			{
				//Lists of strings from allItems
				List<string> innerList = allItems[i];

				#region loop through;

				if (innerList != null)
				{
					int icount2 = innerList.Count;

					#region item found; innerList has values

					if ((icount2 > 0))
					{
						#region Print out on console
						string strFirstchar = innerList[0].ToString().Substring(0, 1);
						if (string.IsNullOrEmpty(strFirstchar))
							strFirstchar = "?";
						Console.WriteLine("\n" + strFirstchar.ToUpper() + " Words" + "\nPress Enter to continue...");
						Console.ReadLine();
						for (int j = 0; j < innerList.Count; j++)
						{
							if (innerList[j] != "")
								Console.WriteLine(innerList[j] + " ");
						}//for 

						#endregion

					}//if

					#endregion

					Console.WriteLine();

				}//if
				#endregion

			}//for


			#endregion

			#endregion

			Console.WriteLine("\nEnd of programm...Press any key");
			Console.ReadKey();
		}

		private static List<string> FnFindAnagrams(string[] inputArr)
		{

			var outputList = new List<string>();

			for (int i = 0; i < inputArr.Length; i++)
			{
				#region for
				for (int j = i + 1; j < inputArr.Length; j++)
				{
					char[] temp1 = inputArr[i].ToLower().ToCharArray();
					char[] temp2 = inputArr[j].ToLower().ToCharArray();
					if (temp1.Length != temp2.Length)
						continue;
					#region else
					else
					{
						bool isAnnograms = true;
						#region for

						string str1 = new string(temp1);
						string str2 = new string(temp2);

						//for (int k1 = 0, k2 = temp1.Length - 1; k1 < temp1.Length; k1++, k2--)
						//{
						//  if (temp1[k1] == temp2[k2])
						//    continue;
						//  else
						//    isAnnograms = false;
						//}//for 
						#endregion
						#region if; are any anagrams
						if ((isAnnograms) && (FnAreAnagrams(str1, str2)))
						{
							if (!outputList.Any(s => s.Contains(str1)))
								outputList.Add(new string(temp1));
							if (!outputList.Any(s => s.Contains(str2)))
								outputList.Add(new string(temp2));
							isAnnograms = false;
						}//if 
						#endregion
					}//else 
					#endregion

				}//for 

				#endregion


			}//for

			return outputList;

		}

		public static bool FnAreAnagrams(string s1, string s2)
		{
			if (s1 == null) throw new ArgumentNullException("s1");
			if (s2 == null) throw new ArgumentNullException("s2");

			var chars = new Dictionary<char, int>();
			foreach (char c in s1)
			{
				if (!chars.ContainsKey(c))
					chars[c] = 0;
				chars[c]++;
			}
			foreach (char c in s2)
			{
				if (!chars.ContainsKey(c))
					return false;
				chars[c]--;
			}

			return chars.Values.All(i => i == 0);
		}

	}
}
