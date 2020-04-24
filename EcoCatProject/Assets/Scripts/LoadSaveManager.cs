/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Mono.Data.SqliteClient;
using System.IO;
using System.Data;
using UnityEngine.UI;


public class playerInfo {
    public string name;
    public string password;

    public playerInfo(string name, string password) {
        this.name = name;
        this.password = password;
    }
}

public class LoadSaveManager : MonoBehaviour
{
    string name;
    string password;
    public Text NameInput;
    public Text PasswordInput;

    public List<playerInfo> playerinfoList = new List<playerInfo>();
    void Start()
    {
       // StartCoroutine(Main());
    }

    void Update()
    {
        if (NameInput.text != null)
        {
            if (PasswordInput.text != null)
            {
                name = NameInput.text;
                password = PasswordInput.text;
            }
        }
    }

    /*
    IEnumerator Main() {

        yield return StartCoroutine(PlayerInfoDBParsing("ecoDB.sqlite")); // 플레이어 정보 파싱

        //StartCoroutine(InGameStart());
    }
    */
    /*
    public void ReadBtn()
    {
        StartCoroutine(PlayerInfoDBParsing("ecoDB.sqlite"));
    }

    // DB 에서 읽어오기
    IEnumerator PlayerInfoDBParsing(string p) {

        string Filepath = Application.persistentDataPath + "/" + p;

        if (!File.Exists(Filepath)) {
            Debug.LogWarning("File \"" + Filepath + "\" does not exist. Attempting to create from \"" +
                Application.dataPath + "!/assets/" + p);

            WWW loadDB = new WWW("jar:file://" + Application.dataPath + "!/assets/" + p);
            while (!loadDB.isDone) { }
            File.WriteAllBytes(Filepath, loadDB.bytes);
        }

        string connectionString = "URI=file:" + Filepath;

        playerinfoList.Clear();

        using (IDbConnection dbConnection = new SqliteConnection(connectionString)) {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand()) {
                string sqlQuery = "SELECT * FROM playDB";

                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader()) {
                    while (reader.Read()) {
                       // Debug.Log(reader.GetString(0));
                        playerinfoList.Add(new playerInfo(reader.GetString(0), reader.GetString(1)));
                    }
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }

        for (int i = 0; i < playerinfoList.Count; i++) {
            Debug.Log(playerinfoList[i].name+", "+playerinfoList[i].password);
        }

        yield return null;
    }

    public void SaveBtn() {
        StartCoroutine(SaveDB("ecoDB.sqlite"));
    }

    // DB 에 수정
    IEnumerator SaveDB(string p)
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

        playerinfoList.Clear();

        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                // 수정하는 코드
                string name = "기매옹";
                string password = "abcd1234";
                string sqlQuery = "UPDATE playDB SET NAME ='" + name 
                    + "', PASSWORD ='"+password+"' WHERE NAME = '옹머'";
                Debug.Log(sqlQuery);

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

    public void InsertBtn()
    {
        StartCoroutine(InsertDB("ecoDB.sqlite"));
    }

    // DB 에 삽입
    IEnumerator InsertDB(string p)
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
                

                string sqlQuery = "INSERT INTO playDB (name, password) VALUES('"+
                    name.ToString() +
                    "', '" + password.ToString() +
                    "')";
                Debug.Log(sqlQuery);

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

    public void Delete() {
        StartCoroutine(DeleteDB("ecoDB.sqlite"));
    }

    // DB 에서 삭제
    IEnumerator DeleteDB(string p)
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

        playerinfoList.Clear();

        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {


                string sqlQuery = "DELETE FROM playDB WHERE NAME = '힝야'";
                Debug.Log(sqlQuery);

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

    IEnumerator InGameStart() {

        yield return null;
    }
}
*/