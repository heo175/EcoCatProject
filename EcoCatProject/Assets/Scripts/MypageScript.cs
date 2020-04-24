using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.SqliteClient;
using System.IO;
using System.Data;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class MypageScript : MonoBehaviour
{
    public Text nameTxt;
    public GameObject nameCavas;
    public InputField nameInput;
    public GameObject updateNameBtn;
    public GameObject nameOkBtn;
    public GameObject panel;
    public GameObject updatepwdBtnn;
    public Text panelTxt;
    public Text pwdPanelTxt;
    public GameObject nameXBtn;


    public GameObject pwdPanel;
    public InputField pwdInputField;
    public GameObject pwdCanvas;
    public InputField newPwdField;
    public Text newPwdWarningTxt;


    string id;
    string name;
    string password;
    string nowpassword;
    string newpassword;

    string inputname;

    int n = 0; // nameInput 보이게, 안 보이게 할 수 있도록 
    int n2 = 0; // name 수정한 뒤 다시 화면에 수정된 닉네임 보일 수 있게
    int ok = 0; // name 인풋필드가 빈칸이면 닉네임 써달라는 판넬 보이고, 거기에 있는 ok 버튼

    int p = 0; // pwdPanel 보이게, 안보이게
    int p1 = 0; // 비밀번호 수정 canvas 보이게, 안보이게
    int np = 0; // newpwdfield 안 채우면 경고 띄울 텍스트

    // Start is called before the first frame update
    void Start()
    {
        SelectName();

        if (password == "googleLogin") {
            updatepwdBtnn.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (nameInput.text != null)
        {
            inputname = nameInput.text;
        }


        if (n == 0)
        {
            nameCavas.SetActive(false);
            nameOkBtn.SetActive(false);
            nameXBtn.SetActive(false);

            updateNameBtn.SetActive(true);
            nameInput.text = "";
        }
        else if (n == 1)
        {
            nameCavas.SetActive(true);
            nameOkBtn.SetActive(true);
            nameXBtn.SetActive(true);

            updateNameBtn.SetActive(false);
             
        }


        if (n2 == 1) {
            nameTxt.text = inputname;
        }

        if (ok == 0) // 평소
        {
            panel.SetActive(false);
        }
        else if (ok == 1) { // 빈칸이라 판넬 뜰 때
            panel.SetActive(true);
        }

        if (p == 1)
        {
            pwdPanel.SetActive(true);
        }
        else {
            pwdPanel.SetActive(false);
        }

        if (pwdInputField.text != null)
        {
            nowpassword = pwdInputField.text;
        }

        if (p1 == 1)
        {
            pwdCanvas.SetActive(true);
        }
        else
        {
            pwdCanvas.SetActive(false);
        }


        if (newPwdField.text != null)
        {
            newpassword = newPwdField.text;
        }

        if (newpassword == "")
        {
            newPwdWarningTxt.text = "새 비밀번호를 입력해주세요.";
            np = 1;
        }
        else {
            newPwdWarningTxt.text = "";
            np = 0;
        }
    }

    public void clickNameXBtn() {
        n = 0;
    }

    public void setClick1() // 닉네임 옆 수정 누르면
    {

        n = 1;
        nameTxt.text = "";

    }

    public void setClick2() // 닉네임 inputfield 에 쓰고나서 확인 누르면
    {
        if (nameInput.text != "")
        {
            n = 0;
            SceneManager.LoadScene("Mypage");
        }
        else
        {
            n = 1;


        }
    }


    // sqlite db에서 login=1 또는 2 인 id, name, password 받아오기 - 함수
    public void SelectName()
    {
        StartCoroutine(SelectDBName("ecoDB.sqlite"));
    }

    // sqlite db에서 login=1 또는 2 id, name, password  받아오기 
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
                string sqlQuery = "SELECT id, name, password FROM ecoCAT WHERE login = 1 OR login = 2";
                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        id = reader.GetString(0);
                        name = reader.GetString(1);
                        password = reader.GetString(2);
                    }
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }

        nameTxt.text = name.ToString();


        yield return null;
    }

    public void panelBtn() {
        ok = 0;
    }

    // sqlite db에 name 업데이트 - 함수
    public void UpdateName()
    {
        if (nameInput.text != null)
        {
            if (nameInput.text != "")
            {
                StartCoroutine(UpdateDBName("ecoDB.sqlite"));
                n2 = 1;
            }
            else {
                ok = 1;
                panelTxt.text = "닉네임을 입력해주세요.";
            }
        }

    }

    // sqlite db에 name 업데이트
    IEnumerator UpdateDBName(string p)
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
                string sqlQuery = "UPDATE ecoCAT SET name = '" +
                        inputname.ToString() +
                        "' WHERE login = 1 OR login = 2";

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

    // sqlite db에 password 업데이트 - 함수
    public void UpdatePassword()
    {
        if (np != 1)
        {
            StartCoroutine(UpdateDBPassword("ecoDB.sqlite"));
            p1 = 0;
            newPwdField.text = ""; 
        }
        else {
            p1 = 1;
        }
    }

    // sqlite db에 newpassword 업데이트
    IEnumerator UpdateDBPassword(string p)
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
                string sqlQuery = "UPDATE ecoCAT SET password = '" +
                        newpassword.ToString() +
                        "' WHERE login = 1 OR login = 2";

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

    public void updatePwdBtn() { // '비밀번호 변경' 버튼 누르면 판넬 보이게
        pwdPanelTxt.text = "현재 비밀번호를 입력해주세요.";
        p = 1;
    }

    public void okBtn() { // 판넬 속 '확인' 버튼 누르면
        if (nowpassword == password)
        {
            p1 = 1;
            p = 0;
            pwdInputField.text = "";
        }
        else {
            pwdPanelTxt.text = "잘못된 비밀번호를 입력했습니다.";
            pwdInputField.text = "";
        }
    }

    public void xBtn() {
        p = 0;
        pwdInputField.text = "";
    }

    public void clickNewPwdXBtn() {
        p1 = 0;
        newPwdField.text = "";
    }
}
