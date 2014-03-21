using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace rDelete
{
    class Program
    {
        static void Main(string[] args)
        {
            var currentDirectory = new DirectoryInfo(Environment.CurrentDirectory);
            
            RemoveReadOnlyAttributesAndDelete(currentDirectory.FullName);
        }

        private static void RemoveReadOnlyAttributesAndDelete(string dir)
        {
            var d = new DirectoryInfo(dir);

            foreach (var file in d.GetFiles())
            {
                File.SetAttributes(file.FullName, File.GetAttributes(file.FullName) & ~FileAttributes.ReadOnly);
                file.Refresh();
                file.Delete();

                file.Delete();
            }

            foreach (var directoryInfo in d.GetDirectories())
            {
                directoryInfo.Attributes = FileAttributes.Normal;
                File.SetAttributes(directoryInfo.FullName, File.GetAttributes(directoryInfo.FullName) & ~FileAttributes.ReadOnly);
                directoryInfo.Refresh();

                RemoveReadOnlyAttributesAndDelete(directoryInfo.FullName);

                directoryInfo.Delete(true);
            }
        }
    }
}
