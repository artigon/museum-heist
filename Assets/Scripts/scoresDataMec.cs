using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;

public class scoresDataMec : MonoBehaviour
{
    public string saveName = "savedScors";
    public string directoryName = "saves";
    public ArrayList scors;
    // Start is called before the first frame update
    void Start()
    {
        scors = new ArrayList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void saveToFile()
    {
        if (!Directory.Exists(directoryName))
            Directory.CreateDirectory(directoryName);

        BinaryFormatter formatter = new BinaryFormatter();

        FileStream saveFile = File.Create(directoryName + "/" + saveName + ".bin");

        formatter.Serialize(saveFile, scors);

        saveFile.Close();

    }

    public void loadFromFile()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream saveFile = File.Open(directoryName + "/" + saveName + ".bin", FileMode.Open);
        scors.Clear();
        scors = (ArrayList)formatter.Deserialize(saveFile);

        saveFile.Close();
    }


    public void addScore(string tmp)
    {
        print(tmp);
        scors.Add(tmp);
    }

    public void clearScorsList()
    {
        scors.Clear();
    }
}
