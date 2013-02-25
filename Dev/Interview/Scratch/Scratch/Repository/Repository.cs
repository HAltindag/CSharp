using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Scratch.Repository
{
	public class Repository
	{

		internal static bool FnCheckFolderExist(string strFoldername)
		{
			bool bRet = true;

			#region try-catch; check if folder exists first
			try
			{
				if (!Directory.Exists(strFoldername))
				{
					Directory.CreateDirectory(strFoldername);
				}
			}
			catch (Exception)
			{
				bRet = false;
				throw;
			} 

			#endregion

			return bRet;

		}


		internal static bool FnIsDirectoryEmpty(string path)
		{
			return !Directory.EnumerateFileSystemEntries(path).Any();
		}


	}
}
