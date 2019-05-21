using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualBasic.FileIO;
using System.IO;

namespace CopyUSB
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("\t\t {0} {1} {2} {3} {4} {5} {6}", DriveInfo1.RootDirectory.ToString(), DriveInfo1.Name,DriveInfo1.VolumeLabel, DriveInfo1.DriveType, DriveInfo1.DriveFormat, DriveInfo1.TotalSize, DriveInfo1.AvailableFreeSpace);
            try
            {
                foreach (System.IO.DriveInfo DriveInfoFrom in System.IO.DriveInfo.GetDrives())
                {

                    if (DriveInfoFrom.DriveType == System.IO.DriveType.Removable)
                    {
                        System.IO.Directory.CreateDirectory(System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Files", DriveInfoFrom.VolumeLabel));
                        System.IO.DirectoryInfo DirectoryInfoTO = new System.IO.DirectoryInfo(System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Files", DriveInfoFrom.VolumeLabel));
                        //Console.WriteLine("TEST: " + DriveInfoTO.Name);
                        //CopyDrive();
                        CopyDrive2Directory(DriveInfoFrom, DirectoryInfoTO);
                    }
                }
            }
            catch (Exception e)
            {
                File.AppendAllText(Path.Combine(Directory.GetCurrentDirectory(), "Error.txt"), DateTime.Now + ": " + e.Message + "\r\n", Encoding.UTF8);
            }

        }

        private static void CopyDrive2Directory(System.IO.DriveInfo DriveInfoIn, System.IO.DirectoryInfo DirectoryInfoTo)
        {
            try
            {
                System.IO.FileInfo[] Files = DriveInfoIn.RootDirectory.GetFiles();
                System.IO.DirectoryInfo[] Directories = DriveInfoIn.RootDirectory.GetDirectories();
                foreach (System.IO.FileInfo FileInfo1 in Files)
                {
                    System.IO.File.Copy(FileInfo1.FullName, System.IO.Path.Combine(DirectoryInfoTo.FullName, FileInfo1.Name), false);
                }
                foreach (System.IO.DirectoryInfo DirectoryInfo1 in Directories)
                {
                    try
                    {
                        FileSystem.CopyDirectory(DirectoryInfo1.FullName, Path.Combine(DirectoryInfoTo.FullName,DirectoryInfo1.Name));
                    }
                    catch (Exception e)
                    {
                        File.AppendAllText(Path.Combine(Directory.GetCurrentDirectory(), "Error.txt"), DateTime.Now + ": " + e.Message + "\r\n", Encoding.UTF8);
                    }
                }
            }
            catch (Exception e)
            {
                File.AppendAllText(Path.Combine(Directory.GetCurrentDirectory(), "Error.txt"), DateTime.Now + ": " + e.Message + "\r\n", Encoding.UTF8);
            }
        }
    }
}
