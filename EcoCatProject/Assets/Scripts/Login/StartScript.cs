using Firebase.Auth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SceneManagement;
using Mono.Data.SqliteClient;
using System.IO;
using System.Data;
using System;

public class StartScript : MonoBehaviour
{
    [SerializeField] string email;
    [SerializeField] string password;

    public Text loginResult;

    FirebaseAuth auth;
    FirebaseUser user;
    public GameObject dd;
    public static string uid; // 파이어베이스에 등록한 uid
    string dbUid; // sqlite에서 가져온 uid
    string dblogin; // sqlite에서 가져온 login 상태 (0 - 로그아웃했던 것 / 3 - 튜토리얼 안 함 / 1 - 메인으로) 
    string dbname; // login = 1 인 name 존재하면 구글 인증 안돼도 바로 메인으로


    void Awake()
    {
        loginResult.text = "로그인 중,,,";
        DontDestroyOnLoad(gameObject);

        auth = FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);

        GoogleLoginBtnOnClick();

    }

    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
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
                loginResult.text = "로그인 성공!";
            }
        }
    }


    public void GoogleLoginBtnOnClick()
    {
        GooglePlayServiceInitialize();

        Social.localUser.Authenticate(success =>
        {
            if (success == false)
            {
                loginResult.text = "아예 안됨";
                SelectName();
                if (dbname == null)
                {
                    Invoke("goLoginscene", 5);
                }
                else {
                    Invoke("goMainscene", 5);
                }
                return;
            }
            loginResult.text = "코루틴 시작 전";
            StartCoroutine(coLogin());
            loginResult.text = "코루틴 끝남";
        });
    }

    void GooglePlayServiceInitialize()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
            .RequestIdToken()
            .Build();

        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
    }

    IEnumerator coLogin()
    {
        while (System.String.IsNullOrEmpty(((PlayGamesLocalUser)Social.localUser).GetIdToken()))
            yield return null;

        string idToken = ((PlayGamesLocalUser)Social.localUser).GetIdToken();
        string accessToken = null;

        Credential credential = GoogleAuthProvider.GetCredential(idToken, accessToken);
        auth.SignInWithCredentialAsync(credential).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithCredentialAsync was canceled.");
                loginResult.text = "구글 로그인 실패";
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithCredentialAsync encountered an error: " + task.Exception);
                loginResult.text = "구글 로그인 실패";
                return;
            }

            FirebaseUser newUser = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
            loginResult.text = "구글 로그인 성공!";

            uid = newUser.UserId;

            selectUID();

            if (dbUid == null)
            {
                // 닉네임 만들러
                Invoke("goNicknamescene", 5);
            }
            else
            {        
                    UpdateLogin(); // 로그인 상태 1로 만들고 메인으로
                    Invoke("goMainscene", 5);
            }
        });
    }
    public void goNicknamescene() // 구글 성공 + 첫 접속
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("NicknameScene");
    }

    public void goMainscene() // 1. 구글 성공 + 기접속     2. 구글 실패 + 기접속
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainScene");
    }

    public void goLoginscene() // 구글 실패 + 첫 접속
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Login");
    }

    public void selectUID()
    {
        StartCoroutine(selectUIDFromSQLITE("ecoDB.sqlite"));
    }

    // 아이디 중복 확인
    IEnumerator selectUIDFromSQLITE(string p)
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
                string sqlQuery = "SELECT id, login FROM ecoCAT WHERE id = '" + uid.ToString() + "'";

                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dbUid = reader.GetString(0);
                        dblogin = reader.GetString(1);
                    }
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }

        yield return null;
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
                string sqlQuery = "UPDATE ecoCAT SET login = 1 WHERE id = '" + uid.ToString() + "'";

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


    // sqlite db에서 login=1 인 name 받아오기 (login=1이면 접속중) - 함수
    public void SelectName()
    {
        StartCoroutine(SelectDBName("ecoDB.sqlite"));
    }

    // sqlite db에서 login=1 인 name 받아오기 (login=1이면 접속중)
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
                string sqlQuery = "SELECT name FROM ecoCAT WHERE login = 1 OR login = 2";
                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        dbname = reader.GetString(0);
                    }
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }



        yield return null;
    }
}
