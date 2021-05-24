using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbiCALC.serialization
{
    partial class database
    {
        //singleton
        static database singleton { get { return (_singleton ??= new database()); } }
        static database _singleton;


        //consts
        //APP
        const string ApplicationName = "AbiCalc2";
        //Extensions
        const string DirectoryExtensionProfiles = "profiles";
        const string DirectoryExtensionConfig = "config";
        const string DirectoryExtensionFile = "bin";
        const string DirectoryExtensionConfigFile = "settings";
    }
}
