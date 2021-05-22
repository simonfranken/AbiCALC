using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AbiCALC.serialization
{
    class database : INotifyPropertyChanged
    {
        //event handler
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
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

        //dirs & files
        //Main Dir
        static DirectoryInfo2 dirAppData = (DirectoryInfo2)(new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ApplicationName)));
        //Sub dirs
        static DirectoryInfo2 profilesDir;
        static DirectoryInfo2 configDir;
        //files
        static FileInfo configFile;
        databaseInfo settings;

        FileInfo loadedFile
        {
            get => _file;
            set
            {
                settings.lastSelected = value;
                _file = value;
            }
        }
        FileInfo _file;
        data loadedData;

        //random
        private static Random random
        {
            get => _random ??= new Random((int)(System.DateTime.Now - new DateTime((long)0)).TotalMilliseconds);
        }
        private static Random _random = null;

        //constructor
        public database()
        {
            _singleton = this;
            setFileAndDirectoryInfos();
            settings = serial.Deserialize<databaseInfo>(configFile, out databaseInfo x) ? x : new databaseInfo();
            bool b = false;
            if (settings.isSet())
            {
                b = load(settings.lastSelected);
            }
            if (!b)
            {
                addNew(((App)(App.Current)).promptNewAccount(), getNewId());
            }
        }
        //init
        private static void setFileAndDirectoryInfos()
        {
            profilesDir = new DirectoryInfo2(Path.Combine(dirAppData.dir.FullName, DirectoryExtensionProfiles));
            configDir = new DirectoryInfo2(Path.Combine(dirAppData.dir.FullName, DirectoryExtensionConfig));
            configFile = new FileInfo(Path.Combine(dirAppData.dir.FullName, DirectoryExtensionConfig, $"{DirectoryExtensionConfigFile}.{DirectoryExtensionFile}"));
        }
        
        //close
        public static void close() => singleton.closePr();
        private void closePr()
        {
            if (loadedData != null) saveCurrent();
            serial.Serialize<databaseInfo>(configFile, settings);
        }
        
        //IDS
        private int getNewId()
        {
            int i;
            List<int> l = new List<int>(getUsedIds());
            for (i = 0; i < 1000; i++)
            {
                int x = random.Next();
                if (!l.Contains(x)) return x;
            }
            throw new Exception($"no unused ID found in {i + 1} tries.");
        }
        private static IEnumerable<int> getUsedIds()
        {

            foreach (FileInfo d in profilesDir.dir.GetFiles())

                if (int.TryParse(Path.GetFileNameWithoutExtension(d.FullName), out int x))
                {
                    yield return x;
                }
            yield break;
        }

        //save
        private static void saveCurrent()
        {
            save(singleton.loadedFile, singleton.loadedData);
        }
        public static bool save(FileInfo file, data _d)
        {
            if (serial.Serialize<data>(file, _d))
            {
                singleton.loadedFile = file;
                return true;
            }
            return false;
        }

        //add
        public static void addNew(data d, int i)
        {
            FileInfo f = new FileInfo(getFileNameFromId(i));
            save(f, d);
            load(f);
        }

        //info
        public static string getFileNameFromId(int id) => Path.Combine(profilesDir.dir.FullName, $"{id}.{DirectoryExtensionFile}");
        public static data currentData
        {
            get => singleton.loadedData;
        }
        public static IEnumerable<string> getProfiles()
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();
            foreach (data d in getDatas())
            {
                string s = d.name.itemValue;
                if (!dict.ContainsKey(s)) dict[s] = 0;
                dict[s]++;
                yield return s + (dict[s] <= 1 ? "" : $" [{dict[s]}]");
            }
            yield break;
        }
        private static IEnumerable<data> getDatas()
        {
            foreach (FileInfo f in profilesDir.dir.EnumerateFiles())
            {
                if (serial.Deserialize<data>(f, out var v))
                {
                    yield return v;
                }
            }
            yield break;
        }
        
        //load
        public static bool load(FileInfo fileInfo)
        {
            if (fileInfo != null && serial.Deserialize<data>(fileInfo, out var x))
            {
                singleton.loadedData = x;
                singleton.loadedFile = fileInfo;
                return true;
            }
            return false;
        }       
    }
}
