using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Mono.Data.SqliteClient;
using System.IO;
using System.Data;
using UnityEngine.UI;

public class playerInfo
{
    public string id;
    public string name;
    public string password;
    public string energy;
    public string animal;
    public string grade;
    public string walking;
    public string total;
    public string vuforiaNormal;
    public string vuforiaAR;
    public string login;

    public playerInfo(string id, string name, string password, string energy,
        string animal, string grade, string walking, string total,
        string vuforiaNormal, string vuforiaAR, string login)
    {
        this.id = id;
        this.name = name;
        this.password = password;      
        this.energy = energy;
        this.animal = animal;
        this.grade = grade;
        this.walking = walking;
        this.total = total;
        this.vuforiaNormal = vuforiaNormal;
        this.vuforiaAR = vuforiaAR;
        this.login = login;
    }
}


public class stepInformation
{
    public string idd;
    public string date;
    public string step;


    public stepInformation(string idd, string date, string step)
    {
        this.idd = idd;
        this.date = date;
        this.step = step;
    }
}



public class AdminScript : MonoBehaviour
{
    public Text eco;
    public Text step;

    public List<playerInfo> playerinfoList = new List<playerInfo>();
    public List<stepInformation> stepInformationList = new List<stepInformation>();

    // Start is called before the first frame update
    void Start()
    {
        ReadBtn();
        ReadBtn2();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReadBtn()
    {
        StartCoroutine(PlayerInfoDBParsing("ecoDB.sqlite"));
    }

    // DB 에서 읽어오기
    IEnumerator PlayerInfoDBParsing(string p)
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
                string sqlQuery = "SELECT * FROM ecoCAT";

                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Debug.Log(reader.GetString(0));
                        playerinfoList.Add(new playerInfo(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3),
                            reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7),
                            reader.GetString(8), reader.GetString(9), reader.GetString(10)));
                    }
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }

        for (int i = 0; i < playerinfoList.Count; i++)
        {
            eco.text = eco.text + "id : " + playerinfoList[i].id + ", " +
                        "name : " + playerinfoList[i].name + ", " +
                        "password : " + playerinfoList[i].password + ", " +
                        "energy : " + playerinfoList[i].energy + ", " +
                        "animal : " + playerinfoList[i].animal + ", " +
                        "grade : " + playerinfoList[i].grade + ", " +
                        "walking : " + playerinfoList[i].walking + ", " +
                        "total : " + playerinfoList[i].total + ", " +
                        "vuforiaNor : " + playerinfoList[i].vuforiaNormal + ", " +
                        "vuforiaAR : " + playerinfoList[i].vuforiaAR + ", " +
                        "login : " + playerinfoList[i].login + " ";
         //   Debug.Log(playerinfoList[i].name + ", " + playerinfoList[i].password);
        }

        yield return null;
    }

    public void ReadBtn2()
    {
        StartCoroutine(PlayerInfoDBParsing2("ecoDB.sqlite"));
    }

    // DB 에서 읽어오기
    IEnumerator PlayerInfoDBParsing2(string p)
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
                string sqlQuery = "SELECT * FROM stepDB";

                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Debug.Log(reader.GetString(0));
                        stepInformationList.Add(new stepInformation(reader.GetString(0), reader.GetString(1), reader.GetString(2)));
                    }
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }

        for (int i = 0; i < stepInformationList.Count; i++)
        {
            step.text = step.text + "아이디 : " + stepInformationList[i].idd + ", " +
                        "날짜 : " + stepInformationList[i].date + ", " +
                        "걸음수 : " + stepInformationList[i].step + " ";
            //   Debug.Log(playerinfoList[i].name + ", " + playerinfoList[i].password);
        }

        yield return null;
    }
}
