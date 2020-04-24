using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.SqliteClient;
using System.IO;
using System.Data;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class NicknameScript : MonoBehaviour
{
    public Text uidTxt;
    public InputField nicknameInputField;
    public GameObject okPanel;

    string nickname;
    string uid;

    private bool okOn = false;

    // Start is called before the first frame update
    void Start()
    {
        uidTxt.text = StartScript.uid.ToString();
        uid = StartScript.uid.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (nicknameInputField.text != null) {
            nickname = nicknameInputField.text;
        }
    }

    public void ActiveokOnPanel()
    {
        if (!okOn)
        {
            okPanel.SetActive(true);
        }
        else
        {
            okPanel.SetActive(false);
        }
        okOn = !okOn;
    }

    public void okBtn() {
        ActiveokOnPanel();
    }

    public void submitBtn() {
        if (nicknameInputField.text == null || nicknameInputField.text == "" || nicknameInputField.text == " "
            || nicknameInputField.text == "  " || nicknameInputField.text == "    ")
        {
            ActiveokOnPanel();
        }
        else {
            StartCoroutine(Join("ecoDB.sqlite"));
            Invoke("goTutorialscene", 5);
        }
    }

    public void goTutorialscene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Tutorial1");
    }

    // 회원가입
    IEnumerator Join(string p)
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


                string sqlQuery = "INSERT INTO ecoCAT (id, name, password, energy, animal, grade, walking, login) VALUES('" +
                    uid.ToString() + "', '" + nickname.ToString() +
                    "', 'googleLogin', '0', '0', '0', '0', '1')";

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

