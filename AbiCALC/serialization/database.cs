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
using System.Collections.ObjectModel;

namespace AbiCALC.serialization
{
    partial class database : INotifyPropertyChanged
    {
        //event handler
        public event PropertyChangedEventHandler PropertyChanged;
        public static event PropertyChangedEventHandler PropertyChangedStatic;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //files
        static FileInfo configFile;
        databaseInfo settings;

        FileInfo _loadedFile;
        FileInfo loadedFile
        {
            get => _loadedFile;
            set
            {
                settings.lastSelected = value;
                _loadedFile = value;
            }
        }
        data _loadedData;
        data loadedData
        {
            get => _loadedData;
            set
            {
                if (loadedData != value)
                {
                    _loadedData = value;
                    NotifyPropertyChanged("currentData");
                }
            }
        }

        //constructor
        public database()
        {
            _singleton = this;
            setFileAndDirectoryInfos();
            settings = serial.Deserialize<databaseInfo>(configFile, out databaseInfo x) ? x : new databaseInfo();
            updateProfiles();
            PropertyChanged += (object? sender, PropertyChangedEventArgs e) => { PropertyChangedStatic?.Invoke(sender, e); };
            bool b = false;
            if (settings.isSet())
            {
                b = load(settings.lastSelected);
            }
            if (!b)
            {
                addNew(((App)(App.Current)).promptNewAccount());
            }
        }

        //close
        public static void close() => singleton.closeInstance();
        private void closeInstance()
        {
            saveCurrent();
            serial.Serialize<databaseInfo>(configFile, settings);
        }

        //IDS
        private static int getNewId()
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
            if (singleton.loadedData != null) save(singleton.loadedFile, singleton.loadedData);
        }
        private static bool save(FileInfo file, data _d)
        {
            if (serial.Serialize<data>(file, _d))
            {
                singleton.loadedFile = file;
                singleton.updateProfiles();
                return true;
            }
            return false;
        }

        //add
        private static void addNew(data d, int i)
        {
            FileInfo f = new FileInfo(getFileNameFromId(i));
            save(f, d);
            load(f);
        }

        public static void addNew(data d)
        {
            addNew(d, getNewId());
        }

        //info
        private static string getFileNameFromId(int id) => Path.Combine(profilesDir.dir.FullName, $"{id}.{DirectoryExtensionFile}");
        public static data currentData
        {
            get => singleton.loadedData;
        }
        public List<data> _profiles;
        public List<data> profiles
        {
            get => _profiles;
            set
            {
                _profiles = value;
                NotifyPropertyChanged();
            }
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
        
        private void updateProfiles() 
        {
            profiles = new List<data>(getDatas());
        }

        //load
        private static bool load(FileInfo fileInfo)
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
