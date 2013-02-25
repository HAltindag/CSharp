using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Scratch
{
    public class WordListParser
    {

				#region variables

			public string _fileDirectory; 

			#endregion

				#region constructor with one parameter
		
        public WordListParser(string fileDirectory)
        {
            _fileDirectory = fileDirectory;
        }

				#endregion

				#region public methods

				/// <summary>
				/// it loops through the files in the folder
				/// because the text files contains carriage returns remove all 
				/// read line for line and store in the ArrayList
				/// </summary>
				/// <returns></returns>
				public ArrayList ParseList()
				{
					ArrayList retList = new ArrayList();

					
					#region check directory
					var files = (string[])null;
					try
					{
						files = Directory.GetFiles(_fileDirectory);
					}
					catch (Exception)
					{

						Console.WriteLine("\nError retrieving data from folder\nCheck C:\\Dev\\Interview\\WordLists exists\nPress any key to exit...");
						Console.ReadKey();
					}

					#endregion
					
					for (var ixFile = 0; ixFile < files.Length; ixFile++)
					{						

						#region try-catch
						try
						{
							var file = new StreamReader(files[ixFile]);
							string line = file.ReadToEnd().Replace("\n", string.Empty);
							
							 if (line != null)
								{			
									retList.Add(line);
								}
						}
						catch (Exception)
						{
						} 

						#endregion
					}
					return retList;
				}

				#endregion

    }
}
