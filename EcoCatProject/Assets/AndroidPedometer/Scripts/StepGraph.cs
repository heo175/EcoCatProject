using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Mono.Data.SqliteClient;
using System.IO;
using System.Data;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class stepInfo
{
    public string step;
    public string date;

    public stepInfo(string date, string step)
    {
        this.date = date;
        this.step = step;
    }
}

public class StepGraph : MonoBehaviour
{
    public GameObject a1;
    public GameObject a2;
    public GameObject a3;
    public GameObject a4;
    public GameObject a5;
    public GameObject a6;
    public GameObject a7;

    public Text text1;
    public Text text2;
    public Text text3;
    public Text text4;
    public Text text5;
    public Text text6;
    public Text text7;

    int b1;
    int b2;
    int b3;
    int b4;
    int b5;
    int b6;
    int b7;

    string id;

    public List<stepInfo> stepinfoList = new List<stepInfo>();

    // Start is called before the first frame update
    void Start()
    {
        SelectId();
        SelectStep();

        a1.transform.localPosition += new Vector3(0, (b1 * 1 / 10), 0);
        a2.transform.localPosition += new Vector3(0, (b2 * 1 / 10), 0);
        a3.transform.localPosition += new Vector3(0, (b3 * 1 / 10), 0);
        a4.transform.localPosition += new Vector3(0, (b4 * 1 / 10), 0);
        a5.transform.localPosition += new Vector3(0, (b5 * 1 / 10), 0);
        a6.transform.localPosition += new Vector3(0, (b6 * 1 / 10), 0);
        a7.transform.localPosition += new Vector3(0, (b7 * 1 / 10), 0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // sqlite db에서 login=1 인 id 받아오기 - 함수
    public void SelectId()
    {
        StartCoroutine(SelectDBId("ecoDB.sqlite"));
    }

    // sqlite db에서 login=1 인 id 받아오기 
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

    // sqlite db에서 날짜별 step 받아오기 - 함수
    public void SelectStep()
    {
        StartCoroutine(SelectDBStep("ecoDB.sqlite"));
    }

    // sqlite db에서 날짜별 step 받아오기
    IEnumerator SelectDBStep(string p)
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
                string sqlQuery = "SELECT DISTINCT date, step FROM stepDB WHERE id = '" + id.ToString() + "' AND " +
                    "date <= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' order by date DESC limit 7";
                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        stepinfoList.Add(new stepInfo(reader.GetString(0), reader.GetString(1)));
                    }
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }
        for (int i = 0; i < stepinfoList.Count; i++)
        {
            if (stepinfoList[i].step == null) stepinfoList[i].step = "0";
            if (stepinfoList[i].date == null) stepinfoList[i].date = " ";
            Debug.Log(stepinfoList[i].date + " " + stepinfoList[i].step + "\n");
        }

        b1 = Int32.Parse(stepinfoList[6].step);
        b2 = Int32.Parse(stepinfoList[5].step);
        b3 = Int32.Parse(stepinfoList[4].step);
        b4 = Int32.Parse(stepinfoList[3].step);
        b5 = Int32.Parse(stepinfoList[2].step);
        b6 = Int32.Parse(stepinfoList[1].step);
        b7 = Int32.Parse(stepinfoList[0].step);

        text1.text = stepinfoList[6].date;
        text2.text = stepinfoList[5].date;
        text3.text = stepinfoList[4].date;
        text4.text = stepinfoList[3].date;
        text5.text = stepinfoList[2].date;
        text6.text = stepinfoList[1].date;
        text7.text = stepinfoList[0].date;

        yield return null;
    }
}
