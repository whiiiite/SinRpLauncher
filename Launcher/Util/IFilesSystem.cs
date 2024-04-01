using System.Threading.Tasks;

namespace SinRpLauncher.Util
{
    /// <summary>
    /// Interface for files system(like launcher files system or game file system)
    /// Need to be implemented method FilesIsWhole() that return string from the Task
    /// if returned string is NOT string.Empty. It means that we have some issues with files
    /// </summary>
    internal interface IFilesSystem
    {
        /// <summary>
        /// Method for implement check that all files is whole
        /// </summary>
        /// <returns></returns>
        abstract public Task<string> FilesIsWhole();
    }
}
