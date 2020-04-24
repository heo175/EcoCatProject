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

public class LoginFirebase : MonoBehaviour
{
    [SerializeField] string email;
    [SerializeField] string password;

    public InputField inputTextEmail;
    public InputField inputTextPassword;
    public Text loginResult;

    FirebaseAuth auth;
    FirebaseUser user;

    public static string uid; // 파이어베이스에 등록한 uid
    string dbUid; // sqlite에서 가져온 uid



    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        auth = FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);

    }
    
    void AuthStateChanged(object sender, System.EventArgs eventArgs) {
       // Firebase.Auth.FirebaseUser user = auth.CurrentUser; // 내가 따로 추가한 코드

        if (auth.CurrentUser != user) {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && user != null) {
                Debug.Log("Signed out" + user.UserId);
            }
            user = auth.CurrentUser;
            if (signedIn) {
                Debug.Log("Signed in" + user.UserId);
                loginResult.text = user.UserId + "로그인 굿럭";
            }
        }
    }
    
    // 버튼 눌리면 실행할 함수
    public void JoinBtnOnClick() {
        email = inputTextEmail.text;
        password = inputTextPassword.text;

        Debug.Log("email : "+email+", password : "+password);

        CreateUser();
    }

    void CreateUser() {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>{
            if (task.IsCanceled) {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                loginResult.text = "회원가입 실패";
                return;
            }

            if (task.IsFaulted) {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error." + task.Exception);
                loginResult.text = "회원가입 실패";
                return;
            }

            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
            newUser.DisplayName, newUser.UserId);

            loginResult.text = "회원가입 굿럭";

        });
    }

    public void LoginBtnOnClick() {
        email = inputTextEmail.text;
        password = inputTextPassword.text;

        Debug.Log("email : " + email + ", password : " + password);

        LoginUser();

        Invoke("goLoginscene", 5);
        
    }

    public void goLoginscene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Login");
    }


    void LoginUser() {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
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

            uid = newUser.UserId;
        });

    }
    
    public void GoogleLoginBtnOnClick()
    {
        GooglePlayServiceInitialize();

        Social.localUser.Authenticate(success =>
        {
            if (success == false) {
                loginResult.text = "아예 안됨";
                SceneManager.LoadScene("Login");
                return;}
            loginResult.text = "코루틴 시작 전";
            StartCoroutine(coLogin());
            loginResult.text = "코루틴 끝남";
        });
    }

    void GooglePlayServiceInitialize() {
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
            if (task.IsCanceled) {
                Debug.LogError("SignInWithCredentialAsync was canceled.");
                loginResult.text = "구글 로그인 실패 ㅜㅜ";
                return;
            }
            if (task.IsFaulted) {
                Debug.LogError("SignInWithCredentialAsync encountered an error: " + task.Exception);
                loginResult.text = "구글 로그인 안됨 ㅠ";
                return;
            }

            FirebaseUser newUser = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
            loginResult.text = "구글 로그인 굿럭";

            uid = newUser.UserId;

            selectUID();

            if (dbUid == null)
            {
                // 닉네임 만들러
                Invoke("goNicknamescene", 5);
            }
            else {
                // 메인으로
                Invoke("goMainscene", 5);
            }
        });
    }
    public void goNicknamescene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("NicknameScene");
    }

    public void goMainscene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainScene");
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
                string sqlQuery = "SELECT * FROM ecoCAT WHERE id = '" + uid.ToString() + "'";

                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dbUid = reader.GetString(0);
                    }
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }
        
        yield return null;
    }
}
