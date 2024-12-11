using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public abstract class DataProcess : BaseMonoBehaviour
{
    public async void Save<T>(string dataPath, T source)
    {
        var json = JsonUtility.ToJson(source);
       
        await json.EncryptAsync();
        
        BinaryFormatter bFormatter = new BinaryFormatter();

        if(Directory.Exists(dataPath) == false)
        {
            Directory.CreateDirectory(dataPath);
        }

        FileStream fStream = File.Open(dataPath, FileMode.Create);

        bFormatter.Serialize(fStream, json);
        fStream.Close();
        fStream.Dispose();
    }

    public async Task<T> Laod<T>(string dataPath)
        where T : ScriptableObject
    {
        var bFormatter = new BinaryFormatter();
        var fStream = new FileStream(dataPath, FileMode.Open);
        var readData = (string)bFormatter.Deserialize(fStream);

        readData = await readData.DecryptAsync();

        var createdInstance = ScriptableObject.CreateInstance<T>();
        JsonUtility.FromJsonOverwrite(readData, createdInstance);

        fStream.Close();
        fStream.Dispose();

        return createdInstance;
    }
}