using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace UbStudyHelpGenerator.UbStandardObjects.Helpers
{
    public class FolderCreator
    {
        /// <summary>
        /// Sanitizes a string to be a valid folder name by replacing invalid characters.
        /// </summary>
        /// <param name="folderName">The original string to sanitize.</param>
        /// <param name="replacementChar">The character to replace invalid characters with (default is '_').</param>
        /// <returns>A sanitized string suitable for a folder name.</returns>
        public static string SanitizeFolderName(string folderName, char replacementChar = '_')
        {
            // Get invalid characters for path names (includes directory separators)
            char[] invalidPathChars = Path.GetInvalidPathChars();
            // Get invalid characters for file names (more restrictive, good for individual folder names)
            char[] invalidFileNameChars = Path.GetInvalidFileNameChars();

            // Combine both sets of invalid characters
            char[] allInvalidChars = invalidPathChars.Union(invalidFileNameChars).Distinct().ToArray();

            string sanitizedName = folderName;

            // Replace each invalid character with the replacement character
            foreach (char invalidChar in allInvalidChars)
            {
                sanitizedName = sanitizedName.Replace(invalidChar, replacementChar);
            }

            // Additional sanitization for common issues:
            // 1. Remove leading/trailing spaces
            sanitizedName = sanitizedName.Trim();
            // 2. Remove leading/trailing dots (can cause issues on Windows)
            sanitizedName = sanitizedName.Trim('.');
            // 3. Replace multiple consecutive replacement characters with a single one
            sanitizedName = Regex.Replace(sanitizedName, Regex.Escape(replacementChar.ToString()) + "{2,}", replacementChar.ToString());

            // Ensure the name is not empty after sanitization
            if (string.IsNullOrWhiteSpace(sanitizedName))
            {
                return "UntitledFolder"; // Provide a default if the name becomes empty
            }

            return sanitizedName;
        }

        public static void ExtractFileInfo(string fullPath, 
                                           string rootFolder, 
                                           out string fileNameWithoutExtension, 
                                           out string foldersPathRelativeToRoot)
        {
            // Extract the file name without extension
            fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fullPath);

            // Get the directory name part of the full path
            string directoryPath = Path.GetDirectoryName(fullPath);

            // Remove the root folder from the directory path
            if (directoryPath != null && directoryPath.StartsWith(rootFolder, System.StringComparison.OrdinalIgnoreCase))
            {
                // Remove the root folder and any leading directory separator
                foldersPathRelativeToRoot = directoryPath.Substring(rootFolder.Length);
                if (foldersPathRelativeToRoot.StartsWith(Path.DirectorySeparatorChar.ToString()) ||
                    foldersPathRelativeToRoot.StartsWith(Path.AltDirectorySeparatorChar.ToString()))
                {
                    foldersPathRelativeToRoot = foldersPathRelativeToRoot.Substring(1);
                }
            }
            else
            {
                // If the root folder is not found or path is not under the root,
                // return the full directory path or an empty string as appropriate.
                foldersPathRelativeToRoot = directoryPath ?? string.Empty;
            }
        }

        /// <summary>
        /// Copies all files and subfolders from a source directory to a destination directory.
        /// The destination directory will be created if it doesn't exist, and the directory structure
        /// within the source will be replicated in the destination.
        /// </summary>
        /// <param name="sourcePath">The path to the source directory.</param>
        /// <param name="destinationPath">The path to the destination directory.</param>
        public static void CopyAll(string sourcePath, string destinationPath)
        {
            // Check if source directory exists
            if (!Directory.Exists(destinationPath))
            {
                Directory.CreateDirectory(destinationPath);
            }
            else
            {
                // All existing file is deleted
                Directory.Delete(destinationPath, true);
                Directory.CreateDirectory(destinationPath);
            }

            // Get all files and subdirectories in the source path
            string[] files = Directory.GetFiles(sourcePath);
            string[] subdirectories = Directory.GetDirectories(sourcePath);

            // Copy all files directly in the source directory to the destination
            foreach (string file in files)
            {
                if (file.Contains(".gitignore")) continue;
                string fileName = Path.GetFileName(file);
                string destFile = Path.Combine(destinationPath, fileName);
                File.Copy(file, destFile, true); // The 'true' overwrites if file exists,
                                                 // which is fine since destination is supposed to be empty
            }

            // Recursively copy subdirectories
            foreach (string subdirectory in subdirectories)
            {
                string dirName = Path.GetFileName(subdirectory);
                if (dirName.StartsWith(".")) continue;
                string destSubdirectory = Path.Combine(destinationPath, dirName);

                // Recursively call CopyAll for the subdirectory
                CopyAll(subdirectory, destSubdirectory);
            }
        }


        /// <summary>
        /// Creates a new folder with a sanitized name, appended to a given base path.
        /// </summary>
        /// <param name="basePath">The base directory where the new folder will be created.</param>
        /// <param name="desiredFolderName">The desired name for the new folder (will be sanitized).</param>
        /// <returns>The full path of the created folder, or null if creation failed.</returns>
        public static string CreateValidFolder(string basePath, string desiredFolderName)
        {
            if (string.IsNullOrWhiteSpace(basePath))
            {
                Console.WriteLine("Error: Base path cannot be null or empty.");
                return null;
            }

            // 1. Sanitize the desired folder name
            string validFolderName = SanitizeFolderName(desiredFolderName);
            if (string.IsNullOrWhiteSpace(validFolderName))
            {
                Console.WriteLine($"Error: Desired folder name '{desiredFolderName}' resulted in an empty or invalid sanitized name.");
                return null;
            }

            // 2. Combine the base path and the valid folder name
            string fullPath = Path.Combine(basePath, validFolderName);

            try
            {
                // 3. Create the directory
                // Directory.CreateDirectory handles creating all directories in the path if they don't exist.
                // It also does not throw an exception if the directory already exists.
                DirectoryInfo directoryInfo = Directory.CreateDirectory(fullPath);
                Console.WriteLine($"Successfully created or ensured directory: {directoryInfo.FullName}");
                return directoryInfo.FullName;
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Error: Access denied when trying to create folder at '{fullPath}'. Please check permissions. Details: {ex.Message}");
                return null;
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error: An I/O error occurred while creating folder at '{fullPath}'. Details: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: An unexpected error occurred while creating folder at '{fullPath}'. Details: {ex.Message}");
                return null;
            }
        }
    }
}

