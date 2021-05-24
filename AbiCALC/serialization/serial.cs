using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace AbiCALC.serialization
{
    public class serial
    {
        public static bool Deserialize<T>(FileInfo info, out T t) where T : class
        {
            T output;
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
                catch
                {
                    throw;
                }
            t = output;
            return true;
        }
        public static bool Serialize<T>(FileInfo info, T t)
        {
            if (info == null) return false;
            createDir(info.Directory);
            using (FileStream fs = new FileStream(info.FullName, FileMode.Create))
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(fs, t);
                }
                catch
                {
                    throw;
                }
            return true;
        }

        static void createDir(DirectoryInfo d)
        {
            d.Create();
        }
    }
}
