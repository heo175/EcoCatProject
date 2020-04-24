using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.SqliteClient;
using System.IO;
using System.Data;
using System;
using UnityEngine.SceneManagement;

public class VuforiaDateScript : MonoBehaviour
{
    string id;
    string date;

    // Start is called before the first frame update
    void Start()
    {
        SelectId();
        SelectDate();

        if (date == null) {
            UpdateVu();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // sqlite db에서 login=1 인 id - 함수
    public void SelectId()
    {
        StartCoroutine(SelectDBId("ecoDB.sqlite"));
    }

    // sqlite db에서 login=1 인 id
    IEnumerator SelectDBId(string p)
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
                string sqlQuery = "SELECT id FROM ecoCAT WHERE login = 1 OR login = 2";
                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        id = reader.GetString(0);
                    }
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }

        Debug.Log(id);

        yield return null;
    }

    // StepDb에서 date 받아오기 - 함수
    public void SelectDate()
    {
        StartCoroutine(SelectDBDate("ecoDB.sqlite"));
    }

    // StepDb에서 date 받아오기
    IEnumerator SelectDBDate(string p)
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
                string sqlQuery = "SELECT date FROM stepDB WHERE id = '" + id + "' AND date = '"
                   + DateTime.Now.ToString("yyyy-MM-dd") + "'";
                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        date = reader.GetString(0);
                    }
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }

        Debug.Log("date 있? : " + date);

        yield return null;
    }

    public void UpdateVu()
    {
        StartCoroutine(UpdateDBVu("ecoDB.sqlite"));
    }

    // 걸음수 3000 이상 되면 에너지 +200
    IEnumerator UpdateDBVu(string p)
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
                string sqlQuery = "UPDATE ecoCAT SET vuforiaNormal = '0', vuforiaAR = '0' WHERE login = 1 OR login = 2";

                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    dbConnection.Close();
                    reader.Close();
                }
            }

        }
        Debug.Log("업데이트 완료");

        yield return null;
    }

}
