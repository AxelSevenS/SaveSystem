using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SeleneGame.Core {

    public class SavingSystem<T> where T : SaveData, new() {

        public static T loadedData;


        public static void SaveData(uint slot){

            T data = loadedData == null ? new T() : loadedData;
            data.Save(); 

            SaveDataToFile(data, slot);
            Debug.Log($"Data Saved in Slot {slot} at {Application.persistentDataPath}");
        }

        public static void LoadData(uint slot){
            
            loadedData = LoadDataFromFile(slot);

            if (loadedData != null){
                loadedData.Load();
                Debug.Log($"Data Loaded from Slot {slot}");
            }else{
                Debug.Log($"Data in Slot {slot} could not be loaded.");
            }
        }
        public static void LoadData(T loadedData){

            if (loadedData != null){
                loadedData.Load();
                Debug.Log($"Data Loaded");
            }else{
                Debug.Log($"Data could not be loaded.");
            }
        }



        public static void SaveDataToFile(T data, uint slot) {
            string path = $"{Application.persistentDataPath}/SaveDat{slot}.dat";

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Create);

            formatter.Serialize(stream, data);
            stream.Close();
        }
        
        public static T LoadDataFromFile(uint slot) {
            string path = $"{Application.persistentDataPath}/SaveDat{slot}.dat";

            if (File.Exists(path)){
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                try {
                    return (T)formatter.Deserialize(stream);
                } catch {
                    return null;
                } finally {
                    stream.Close();
                }
                
            } else {
                return null;
            }
        }
    }

}