﻿using System;
using System.IO;
using UnityEngine;

namespace TowerDefence
{
    public static class FileHandler
    {
        public static string Path(string filename)
        {
            return $"{Application.persistentDataPath}/{filename}";
        }

        public static void Reset(string filename)
        {
            var path = Path(filename);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public static bool HaveFile(string filename)
        {
           return File.Exists(Path(filename));
        }
    }

    [Serializable]
    public class Saver<T>
    {   
        public static void TryLoad(string filename, ref T data)
        {
            var path= FileHandler.Path(filename);

            if (File.Exists(path))
            {
                Debug.Log($"loading form {path}");
                var dataString = File.ReadAllText(path);
                var saver = JsonUtility.FromJson<Saver<T>>(dataString);
                data = saver.data;
            }
        }

        public static void Save(string filename, T data)
        {
            Debug.Log("save");
            var wrapper = new Saver<T>{ data = data };
            var dataString = JsonUtility.ToJson(wrapper);
            File.WriteAllText(FileHandler.Path(filename), dataString);
        }



        public T data;

    }
}