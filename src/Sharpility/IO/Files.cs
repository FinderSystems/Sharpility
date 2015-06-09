using System;
using System.IO;
using System.Reflection;
using System.Text;
using Sharpility.Base;

namespace Sharpility.IO
{
    /// <summary>
    /// Files utilities.
    /// </summary>
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

        /// <summary>
        /// Deletes exiting directory recursively.
        /// </summary>
        /// <param name="directory">Directory to delete</param>
        /// <returns>True when existing directory was deleted</returns>
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
        /// <returns>True if file was deleted, false if not</returns>
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

        /// <summary>
        /// Tries to delete directory recursively.
        /// </summary>
        /// <param name="directory">Directory to delete</param>
        /// <returns>True if directory was deleted</returns>
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

        public static string Path(params string[] paths)
        {
            var result = new StringBuilder();
            foreach (var path in paths)
            {
                result.Append(TrimEndingDirectorySeparator(path) + System.IO.Path.DirectorySeparatorChar);
            }
            return TrimEndingDirectorySeparator(result.ToString());
        }

        /// <summary>
        /// Returns executing assembly directory.
        /// </summary>
        public static string AssemblyDirectory
        {
            get 
            {
                var assembly = Assembly.GetExecutingAssembly();
                var assemblyDirectory = System.IO.Path.GetDirectoryName(assembly.Location);
                return assemblyDirectory;
            }
        }

        /// <summary>
        /// Returns user home directory.
        /// </summary>
        public static string HomeDirectory
        {
            get
            {
                var homeDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                return homeDir;
            }
        }

        /// <summary>
        /// Removes ending directory separator from path.
        /// </summary>
        /// <param name="path">path</param>
        /// <returns>path with ending directory separator removed</returns>
        public static string TrimEndingDirectorySeparator(string path)
        {
            Precognitions.IsNotNull(path, "Path not specified");
            if (path.EndsWith(System.IO.Path.DirectorySeparatorChar.ToString()) && path.Length > 1)
            {
                return path.Substring(0, path.Length - 1);
            }
            return path == System.IO.Path.DirectorySeparatorChar.ToString() ? "" : path;
        }
    }
}
