using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace Scratch.Repository
{
	public class Anagrams
	{
		const int _maxLength = 10;
		const string _fileName = "anagrams.txt";
		const string _inputFile = "enable1.txt";

		Dictionary<string, List<string>> _dict;

		/// <summary>
		/// Display all the anagrams for the parameter word.
		/// </summary>
		/// <param name="wordIn">The word you want to find anagrams for.</param>
		public void FindAnagramsFor(string wordIn)
		{
			string alpha = Alpha(wordIn);

			List<string> anagramList;
			if (_dict.TryGetValue(alpha, out anagramList))
			{
				Console.WriteLine(wordIn + ":");
				foreach (string anagram in anagramList)
				{
					Console.WriteLine(" " + anagram);
				}
			}
		}

		/// <summary>
		/// Read in the dictionary file to build the anagram data structure.
		/// </summary>
		public void ReadDictionary()
		{
			_dict = new Dictionary<string, List<string>>();

			using (StreamReader reader = new StreamReader(_fileName))
			{
				string line;
				while ((line = reader.ReadLine()) != null)
				{
					string[] parts = line.Split(';');

					List<string> localList;
					if (_dict.TryGetValue(parts[0], out localList))
					{
						localList.Add(parts[1]);
					}
					else
					{
						localList = new List<string>();
						localList.Add(parts[1]);
						_dict.Add(parts[0], localList);
					}
				}
			}
		}

		/// <summary>
		/// Write a file containing all the key/value anagram pairs.
		/// </summary>
		public void WriteDictionary()
		{
			if (File.Exists(_fileName))
			{
				return;
			}

			List<string>[] wordLists = new List<string>[_maxLength];
			for (int i = 0; i < _maxLength; i++)
			{
				wordLists[i] = new List<string>();
			}

			Dictionary<string, List<string>> alphaDictionary = new Dictionary<string, List<string>>();

			using (StreamReader reader = new StreamReader(_inputFile))
			{
				string line;
				while ((line = reader.ReadLine()) != null)
				{
					line = line.Trim();
					if (line.Length >= _maxLength)
					{
						continue;
					}

					string alphaString = Alpha(line);
					if (alphaDictionary.ContainsKey(alphaString) == true)
					{
						bool found = false;
						foreach (string lineThere in alphaDictionary[alphaString])
						{
							if (lineThere == line)
							{
								found = true;
								break;
							}
						}
						if (found == false)
						{
							alphaDictionary[alphaString].Add(line);
						}
					}
					else
					{
						alphaDictionary.Add(alphaString, new List<string>());
						alphaDictionary[alphaString].Add(line);
						wordLists[alphaString.Length].Add(alphaString);
					}
				}
			}

			using (StreamWriter writer = new StreamWriter(_fileName))
			{
				List<string> alphaList = new List<string>(alphaDictionary.Keys);
				alphaList.Sort();

				for (int i = 0; i < alphaList.Count; i++)
				{
					string alpha = alphaList[i];
					List<string> actualWords = alphaDictionary[alpha];

					foreach (string realWord in actualWords)
					{
						writer.WriteLine(alpha + ";" + realWord);
					}
				}
			}
		}

		/// <summary>
		/// Alphabetize the word received as a parameter. This will create
		/// a key from the word that is always the same for words with
		/// equal letter frequencies.
		/// </summary>
		/// <param name="wordIn">The word you need to alphabetize.</param>
		/// <returns>The alphabetized string.</returns>
		static string Alpha(string wordIn)
		{
			char[] arr = wordIn.ToCharArray();
			Array.Sort(arr);
			return new string(arr);
		}
	}
}