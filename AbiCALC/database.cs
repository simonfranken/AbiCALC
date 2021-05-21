using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace AbiCALC
{
    class database
    {
        //APP
        const string ApplicationName = "AbiCalc2";
        //Extensions
        const string DirectoryExtensionProfiles = "profiles";
        const string DirectoryExtensionConfig = "config";
        const string DirectoryExtensionFile = "bin";
        const string DirectoryExtensionConfigFile = "settings";

        //Main Dir
        static DirectoryInfo dirAppData = new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ApplicationName));
        //Sub dirs
        static DirectoryInfo profilesDir;
        static DirectoryInfo configDir;

        static FileInfo configFile;

        databaseInfo settings;


        public database()
        {
            _singleton = this;
            setFileAndDirectoryInfos();
            settings = Deserialize<databaseInfo>(configFile, out var x) ? x : new databaseInfo();
            bool b = false;
            if(settings.isSet()) 
            {
                b = load(settings.lastSelected);
            }
            if(!b) 
            {
                addNew(((App)(App.Current)).promptNewAccount(random.Next()));
            }
        }

        private static void setFileAndDirectoryInfos()
        {
            profilesDir = new DirectoryInfo(Path.Combine(dirAppData.FullName, DirectoryExtensionProfiles));
            configDir = new DirectoryInfo(Path.Combine(dirAppData.FullName, DirectoryExtensionConfig));
            configFile = new FileInfo(Path.Combine(dirAppData.FullName, DirectoryExtensionConfig, $"{DirectoryExtensionConfigFile}.{DirectoryExtensionFile}"));
        }

        public static void close() => singleton.closePr();
        private void closePr()
        {
            if (d != null) save();
            Serialize<databaseInfo>(configFile, settings);
        }

        data d;
        FileInfo file;

        static database singleton { get { return (_singleton ??= new database()); } }
        static database _singleton;

        private static Random random 
        {
            get => _random ??= new Random((int)(System.DateTime.Now - new DateTime((long)0)).TotalMilliseconds);
        }
        private static Random _random = null;


        public static void addNew(data d) 
        {
            FileInfo f = new FileInfo(getFileNameFromId(d));
            bool b = Serialize<data>(f, d);
            load(d.id);

        }

        public static string getFileNameFromId(data d) => Path.Combine(profilesDir.FullName, $"{d.id}.{DirectoryExtensionFile}");

        public static data current
        {
            get => singleton.d;
        }

        public static bool save()
        {
            if(Serialize<data>(singleton.file, singleton.d)) 
            {
                return true;
            }
            return false;
        }

        public static IEnumerable<string> getProfiles()
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();
            foreach (data d in getDatas()) 
            {
                if (!dict.ContainsKey(d.name.unformatted)) dict[d.name.unformatted] = 0;
                dict[d.name.unformatted]++;
                yield return d.name.unformatted + (dict[d.name.unformatted] <= 1 ? "" : $" [{dict[d.name.unformatted]}]");
            }
        }

        private static Dict2<FileInfo, int> getFilesProfiles() 
        {
            Dict2<FileInfo, int> r = new Dict2<FileInfo, int>();

            foreach (data v in getDatas())
            {
                    r[new FileInfo(getFileNameFromId(v))] = v.id;
            }

            return r;
        }

        private static IEnumerable<data> getDatas() 
        {
            foreach (FileInfo f in profilesDir.EnumerateFiles())
            {
                if (Deserialize<data>(f, out var v))
                {
                    yield return v;
                }
            }
            yield break;
        }

        public static bool load(int id)
        {
            FileInfo fileInfo = getFilesProfiles()[id];
            if(fileInfo != null && Deserialize<data>(fileInfo, out var x)) 
            {
                singleton.d = x;
                singleton.file = fileInfo;
                singleton.settings.lastSelected = id;
                return true;
            }
            return false;
        }

        static bool Serialize<T>(FileInfo info, T t)
        {
            if (info == null) return false;
            createDir(info.Directory);
            using (FileStream fs = new FileStream(info.FullName, FileMode.Create))
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(fs, t);
                }
                catch (SerializationException e)
                {
                    throw;
                    return false;
                }
            return true;
        }

        static void createDir(DirectoryInfo d) 
        {
            d.Create();
        }

        static bool Deserialize<T>(FileInfo info, out T t) where T : class
        {
            T output;
            createDir(info.Directory);
            if (!info.Exists) 
            {
                t = null;
                return false; 
            }
            using (FileStream fs = new FileStream(info.FullName, FileMode.Open))
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    output = (T)formatter.Deserialize(fs);
                }
                catch (SerializationException e)
                {
                    t = null;
                    return false;
                }
            t = output;
            return true;
        }
    
        [Serializable]
        private class databaseInfo 
        {
            private int? _lastSelected = null;
            private bool b = false;
            public int lastSelected 
            {
                get => (int)_lastSelected;
                set 
                {
                    b = true;
                    _lastSelected = value;
                }
            }

            public bool isSet() => b;
        }
    }
}
