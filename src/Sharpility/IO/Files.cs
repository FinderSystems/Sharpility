using System;
using System.IO;
using System.Reflection;

namespace Sharpility.IO
{
    public static class Files
    {
        /// <summary>
        /// Deletes file if exists.
        /// </summary>
        /// <param name="file">file to delete</param>
        /// <returns>true if file was deleted, false if not</returns>
        public static bool DeleteIfExists(string file)
        {
            if (File.Exists(file))
            {
                File.Delete(file);
                return true;
            }
            return false;
        }

        public static bool DeleteDirectoryRecursiveIfExists(string directory)
        {
            if (Directory.Exists(directory))
            {
                const bool recursive = true;
                Directory.Delete(directory, recursive);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Tries to delete file.
        /// </summary>
        /// <param name="file">file to delete</param>
        /// <returns>true if file was deleted, false if not</returns>
        public static bool TryDeleteFile(string file)
        {
            try
            {
                return DeleteIfExists(file);
            }
            catch (IOException)
            {
                return false;
            }
        }

        public static bool TryDeleteDirectoryRecursive(string directory)
        {
            try
            {
                return DeleteDirectoryRecursiveIfExists(directory);
            }
            catch (IOException)
            {
                return false;
            }
        }

        public static string AssemblyDirectory
        {
            get 
            {
                var assembly = Assembly.GetExecutingAssembly();
                var assemblyDirectory = Path.GetDirectoryName(assembly.Location);
                return assemblyDirectory;
            }
        }

        public static string HomeDirectory
        {
            get
            {
                var homeDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                return homeDir;
            }
        }

        public static string RemoveEndingSeparator(string path)
        {
            if (path.EndsWith(Path.DirectorySeparatorChar.ToString()) && path.Length > 1)
            {
                return path.Substring(0, path.Length - 1);
            }
            return path;
        }
    }
}
