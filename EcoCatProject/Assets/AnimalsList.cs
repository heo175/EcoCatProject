using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Mono.Data.SqliteClient;
using System.IO;
using System.Data;
using System;
using UnityEngine.SceneManagement;

public class AnimalsList : MonoBehaviour
{
    public int pageNum;
    int animal;
    int grade;
    string name;

    public GameObject LOCKpage;
    public GameObject Clearpage;

    public GameObject Panel1;
    public GameObject Panel2;
    public GameObject Panel3;
    public GameObject Panel4;
    public GameObject Panel5;
    
    void Start()
    {
        StartCoroutine(SelectDBName("ecoDB.sqlite"));

        Panel1.SetActive(false);
        Panel2.SetActive(false);
        Panel3.SetActive(false);
        Panel4.SetActive(false);
        Panel5.SetActive(false);
        Clearpage.SetActive(false);
        LOCKpage.SetActive(false);

        SelectPage();
        PageCompare();

        Debug.Log("이름은 "+name);
    }

    public void SelectPage()
    {
        StartCoroutine(SelectDBAnimal("ecoDB.sqlite"));
    }

    IEnumerator SelectDBAnimal(string p)
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
                string sqlQuery = "SELECT animal, grade FROM ecoCAT WHERE name = '" + name.ToString() + "'";
                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        animal = Int32.Parse(reader.GetString(0));
                        grade = Int32.Parse(reader.GetString(1));
                    }
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }
        yield return null;
    }

    IEnumerator SelectDBName(string p)
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
                string sqlQuery = "SELECT name FROM ecoCAT WHERE login = 1";
                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        name = reader.GetString(0);
                       
                    }
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }
        yield return null;
    }

    void PageCompare() {
        if (pageNum > animal)
        {
            Panel1.SetActive(true);
            LOCKpage.SetActive(true);
        }
        else if (pageNum == animal)
        {
            if(grade == 0)
                Panel1.SetActive(true);
            else if (grade == 1)
                Panel2.SetActive(true);
            else if (grade == 2)
                Panel3.SetActive(true);
            else if (grade == 3)
                Panel4.SetActive(true);
            else if (grade == 4)
                Panel5.SetActive(true);
            else if (grade == 5)
                Clearpage.SetActive(true);
        }
        else if (pageNum < animal)
        {
            Clearpage.SetActive(true);
        }
    }
}
