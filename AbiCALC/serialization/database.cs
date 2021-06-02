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
                ((App)(App.Current)).createNewAccount();
            }
        }

        //close
        public static void close() { if (_singleton != null) singleton.closeInstance(); }
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
        public static void saveCurrent()
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
            saveCurrent();
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
        private List<data> _profiles;
        private List<(data,FileInfo)> _profiles2;
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
        private static IEnumerable<(data, FileInfo)> getDatas2()
        {
            foreach (FileInfo f in profilesDir.dir.EnumerateFiles())
            {
                if (serial.Deserialize<data>(f, out var v))
                {
                    yield return (v,f);
                }
            }
            yield break;
        }

        private void updateProfiles() 
        {
            _profiles2 = new List<(data,FileInfo)>(getDatas2());
            List<data> ret = new List<data>();
            foreach ((data x, FileInfo) i in _profiles2) ret.Add(i.x);
            profiles = ret;
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
        public static FileInfo getInfo(data d) 
        {
            foreach ((data x, FileInfo y) item in singleton._profiles2)
            {
                if (d==item.x) return item.y;
            }
            throw new Exception("404 - File not found.");
        }
    }
}
