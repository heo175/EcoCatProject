using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Mono.Data.SqliteClient;
using System.IO;
using System.Data;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class GameDBManager : MonoBehaviour
{
    int energy;

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
        StartCoroutine(SelectDBEnergy2("ecoDB.sqlite"));
    }

    // sqlite db에서 energy 받아오기
    IEnumerator SelectDBEnergy2(string p)
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
                string sqlQuery = "SELECT energy FROM ecoCAT WHERE login = 1";
                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        energy = Int32.Parse(reader.GetString(0));
                    }
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }

        
        yield return null;
    }

    // sqlite db에 energy 업데이트 - 함수
    public void UpdateEnergyEASY()
    {
        StartCoroutine(UpdateDBEnergyEASY("ecoDB.sqlite"));
    }

    public void UpdateEnergyNORMAL()
    {
        StartCoroutine(UpdateDBEnergyNORMAL("ecoDB.sqlite"));
    }

    public void UpdateEnergyHARD()
    {
        StartCoroutine(UpdateDBEnergyHARD("ecoDB.sqlite"));
    }

    // sqlite db에 energy 업데이트
    IEnumerator UpdateDBEnergyEASY(string p)
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
                string sqlQuery = "UPDATE ecoCAT SET energy = " + (energy+500)+" WHERE login = 1";

                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    dbConnection.Close();
                    reader.Close();
                }
            }

        }
        yield return null;
    }

    IEnumerator UpdateDBEnergyNORMAL(string p)
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
                string sqlQuery = "UPDATE ecoCAT SET energy = " + (energy + 1000) + " WHERE login = 1";

                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    dbConnection.Close();
                    reader.Close();
                }
            }

        }
        yield return null;
    }

    IEnumerator UpdateDBEnergyHARD(string p)
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
                string sqlQuery = "UPDATE ecoCAT SET energy = " + (energy + 1500) + " WHERE login = 1";

                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    dbConnection.Close();
                    reader.Close();
                }
            }

        }
        yield return null;
    }
}
