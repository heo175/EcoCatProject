using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Mono.Data.SqliteClient;
using System.IO;
using System.Data;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using Firebase.Auth;

public class LoginDB : MonoBehaviour
{
    public Text IdInput;
    public Text PasswordInput;
    string id;
    string password;
    string pwd;

    string dbid;
    string dbpwd;

    public GameObject OkPanel;
    public Text OkText;
    int ok = 0;

    string dblogin;

    [SerializeField] string femail;
    [SerializeField] string fpassword;

    public Text loginResult;

    FirebaseAuth auth;
    FirebaseUser user;

    // Start is called before the first frame update
    void Awake()
    {
        auth = FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        // Firebase.Auth.FirebaseUser user = auth.CurrentUser; // 내가 따로 추가한 코드

        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && user != null)
            {
                Debug.Log("Signed out" + user.UserId);
            }
            user = auth.CurrentUser;
            if (signedIn)
            {
                Debug.Log("Signed in" + user.UserId);
                loginResult.text = user.UserId + "로그인 굿럭";
            }
        }
    }

    bool IsValidEmail(string strIn)
    {
        return Regex.IsMatch(strIn, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
    }

    public void LoginBtnOnClick()
    {

        


        if (IdInput.text != "" && PasswordInput.text != "")
        {
            femail = IdInput.text;
            fpassword = PasswordInput.text;

            Debug.Log("email : " + femail + ", password : " + fpassword);

            LoginUser();
        }

        

    }

    public void goMainscene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainScene");
    }

    public void goTutorialscene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Tutorial1");
    }


    void LoginUser()
    {
        auth.SignInWithEmailAndPasswordAsync(femail, fpassword).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                loginResult.text = "로그인 실패";
                return;
            }

            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error." + task.Exception);
                loginResult.text = "로그인 실패";
                return;
            }

            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("User signed in successfully: {0}, ({1})",
                newUser.DisplayName, newUser.UserId);

            
        });

    }

    // Update is called once per frame
    void Update()
    {
        if (IdInput.text != null)
        {
            if (PasswordInput.text != null)
            {
                id = IdInput.text;
                password = PasswordInput.text;
            }
        }

        if (ok == 0) // 처음 상태면 안 보이게
        {
            OkPanel.SetActive(false);
        }
        else {
            OkPanel.SetActive(true);
        }
    }

    public void LoginBtn()
    {
        StartCoroutine(Login("ecoDB.sqlite"));
    }

    // 로그인 
    IEnumerator Login(string p)
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
                string sqlQuery = "SELECT id, password, login FROM ecoCAT WHERE id = '"+id.ToString()+"'";
                dbCmd.CommandText = sqlQuery;

                 using (IDataReader reader = dbCmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        dbid = reader.GetString(0);
                        dbpwd = reader.GetString(1);
                        dblogin = reader.GetString(2);
                    }
                    dbConnection.Close();
                    reader.Close();
                }
            }
            Debug.Log(dbid + " " + dbpwd);
        }

        if (IdInput.text == "")
        {
            ok = 1;
            OkText.text = "아이디를 입력해주세요.";
        }
        else if (!IsValidEmail(IdInput.text))
        {
            ok = 1;
            OkText.text = "아이디를 이메일 형식으로 입력해주세요.";
        }
        else if (PasswordInput.text == "")
        {
            ok = 1;
            OkText.text = "비밀번호를 입력해주세요.";
        }
        else
        {
            
            if (dbid != null)
            {
                // 디비에 있는 이름과 일치
                if (id.ToString() == dbid.ToString())
                {
                    if (password.ToString() == dbpwd.ToString()) // 이름 + 비밀번호 둘 다 일치
                    {
                        LoginBtnOnClick();

                        if (dblogin == "3") // 첫접속이면 튜토리얼로
                        {
                            UpdateLogin(); // login = 1 로 바꿔줌
                            Invoke("goTutorialscene", 5);
                        }
                        else
                        { // 기접속자면 메인으로
                            UpdateLogin(); // login = 1 로 바꿔줌
                            Invoke("goMainscene", 5);
                        }
                        
                    }
                    else
                    { // 이름은 일치, 비밀번호는 불일치
                        ok = 1;
                        OkText.text = "비밀번호가 일치하지 않습니다.";
                    }
                }
                else// 디비에 없으면 로그인 실패
                {
                    ok = 1;
                    OkText.text = "존재하지 않는 아이디입니다.";
                }
            }
            else
            {
                ok = 1;
                OkText.text = "존재하지 않는 아이디입니다.";
            }
        }
        yield return null;
    }

    public void Okbtn() {
        ok = 0;
    }

    // sqlite db에 login 업데이트 - 함수
    public void UpdateLogin()
    {
        StartCoroutine(UpdateDBLogin("ecoDB.sqlite"));
    }

    // sqlite db에 login 업데이트
    IEnumerator UpdateDBLogin(string p)
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
                string sqlQuery = "UPDATE ecoCAT SET login = 1 WHERE id = '" + id.ToString() + "'";

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

