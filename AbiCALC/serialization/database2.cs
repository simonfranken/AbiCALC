using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbiCALC.serialization
{
    partial class database
    {
        //singleton
        public static database singleton { get { return (_singleton ??= new database()); } }
        static database _singleton;


        //consts
        //APP
        const string ApplicationName = "AbiCalc2";
        //Extensions
        const string DirectoryExtensionProfiles = "profiles";
        const string DirectoryExtensionConfig = "config";
        const string DirectoryExtensionFile = "bin";
        const string DirectoryExtensionConfigFile = "settings";

        //dirs & files
        //Main Dir
        static DirectoryInfo2 dirAppData = (DirectoryInfo2)(new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ApplicationName)));
        //Sub dirs
        static DirectoryInfo2 profilesDir;
        static DirectoryInfo2 configDir;

        //init
        private static void setFileAndDirectoryInfos()
        {
            profilesDir = new DirectoryInfo2(Path.Combine(dirAppData.dir.FullName, DirectoryExtensionProfiles));
            configDir = new DirectoryInfo2(Path.Combine(dirAppData.dir.FullName, DirectoryExtensionConfig));
            configFile = new FileInfo(Path.Combine(dirAppData.dir.FullName, DirectoryExtensionConfig, $"{DirectoryExtensionConfigFile}.{DirectoryExtensionFile}"));
        }

        //random
        private static Random random
        {
            get => _random ??= new Random((int)(System.DateTime.Now - new DateTime((long)0)).TotalMilliseconds);
        }
        private static Random _random = null;
    }
}
