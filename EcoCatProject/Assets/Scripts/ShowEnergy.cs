using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Mono.Data.SqliteClient;
using System.IO;
using System.Data;
using System;
using UnityEngine.SceneManagement;

public class ShowEnergy : MonoBehaviour
{
    int energy;
    public Text energyNum;
    public Text Name;
    // Start is called before the first frame update
    void Start()
    {
        SelectEnergy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // sqlite db에서 energy 받아오기 - 함수
    public void SelectEnergy()
    {
        StartCoroutine(SelectDBEnergy("ecoDB.sqlite"));
    }

    // sqlite db에서 energy 받아오기
    IEnumerator SelectDBEnergy(string p)
    {

        string Filepath = Application.persistentDataPath + "/" + p;

        if (!File.Exists(Filepath))
        {
            Debug.LogWarning("File \"" + Filepath + "\" does not exist. Attempting to create from \"" +
                Application.dataPath + "!/assets/" + p);

            WWW loadDB = new WWW("jar:file://" + Application.dataPath + "!/assets/" + p);
            while (!loadDB.isDone) { }
            File.WriteAllBytes(Filepath, loadDB.bytes);
        }

        string connectionString = "URI=file:" + Filepath;

        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = "SELECT energy, name FROM ecoCAT WHERE login = 1 OR login = 2";
                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        energy = Int32.Parse(reader.GetString(0));
                        name = reader.GetString(1);
                    }
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }

        energyNum.text = energy.ToString();
        Name.text = name;

        yield return null;
    }
}
