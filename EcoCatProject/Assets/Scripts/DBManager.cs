using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Mono.Data.SqliteClient;
using System.IO;
using System.Data;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using Firebase.Auth;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System.Text.RegularExpressions;

public class DBManager : MonoBehaviour
{
    int g = 0;
    string gid = "google"; // 구글 아이디
    string id; // 내가 만드는 아이디
    string name; // 닉네임
    string password; // 비밀번호
    public GameObject IdInput; // 아이디 인풋필드
    public GameObject okBtnObject; // 중복 확인 버튼
    public Text IdInputFiled; // 아이디 인풋필드의 텍스트 (원래 얘가 IdInput 이어야 했는데 ㅋㅎ)
    public Text NameInput; // 네임 인풋필드의 텍스트
    public Text PasswordInput;
    public Text googleid; // 구글 아이디 표시해줌

    string dbid;

    public Text OkText;
    public GameObject OkPanel;
    int ok = 0;
    int distinct;
    int okk = 0;

    public GameObject JoinBtnObject;

    [SerializeField] string femail;
    [SerializeField] string fpassword;

    public Text loginResult;

    FirebaseAuth auth;
    FirebaseUser user;

    void Start()
    {
        /* 구글 아이디 인증되면 g = 1 로 바뀜
        if () {
            g = 1;

        }
        */

       
    }

    void Awake()
    {
        auth = FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
        JoinBtnObject.SetActive(false);
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

    // 버튼 눌리면 실행할 함수
    public void JoinBtnOnClick()
    {
        if (IdInputFiled.text == "")
        {
            ok = 1;
            OkText.text = "아이디를 입력해주세요.";
        }
        else if (!IsValidEmail(IdInputFiled.text))
        {
            ok = 1;
            OkText.text = "아이디를 이메일 형식으로 입력해주세요.";
        }
        else if (NameInput.text == "")
        {
            ok = 1;
            OkText.text = "이름을 입력해주세요.";
        }
        else if (PasswordInput.text == "")
        {
            ok = 1;
            OkText.text = "비밀번호를 입력해주세요.";
        }
        else if (PasswordInput.text.Length < 6) {
            ok = 1;
            OkText.text = "6자리 이상의 영문/숫자/한글로 비밀번호를 입력해주세요.";
        }

        if (IdInputFiled.text != "" && PasswordInput.text != "")
        {
            femail = id;
            fpassword = password;

        Debug.Log("email : " + femail + ", password : " + fpassword);

            CreateUser();
        }
    }

    void CreateUser()
    {
        auth.CreateUserWithEmailAndPasswordAsync(femail, fpassword).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                loginResult.text = "회원가입 실패";
                return;
            }

            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error." + task.Exception);
                ok = 1;
                OkText.text = "이미 존재하는 계정입니다.";
                loginResult.text = "회원가입 실패";
                return;
            }

            Firebase.Auth.FirebaseUser newUser = task.Result;

            okk = 1;

            Debug.Log("okk : " + okk);

            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
            newUser.DisplayName, newUser.UserId);
            loginResult.text = "회원가입 굿럭";
            
        });
    }

    void Update()
    {
        if (g == 1)
        {
            if (NameInput.text != null)
            {
                if (PasswordInput.text != null)
                {
                    id = gid;
                    name = NameInput.text;
                    password = PasswordInput.text;
                    IdInput.SetActive(false);
                    okBtnObject.SetActive(false);
                    googleid.text = gid.ToString();
                }
            }
        }
        else if(g==0)
        {
            if (IdInputFiled.text != null)
            {
                if (NameInput.text != null)
                {
                    if (PasswordInput.text != null)
                    {
                        id = IdInputFiled.text;
                        name = NameInput.text;
                        password = PasswordInput.text;
                    }
                }
            }
        }

        if (ok == 0) // 처음 상태면 안 보이게
        {
            OkPanel.SetActive(false);
        }
        else if (ok == 3) { // 중복 아니라서 사용 가능, 안 보이게
            OkPanel.SetActive(false);
        }
        else
        {
            OkPanel.SetActive(true);
        }
    }

    public void JoinBtn()
    {
            StartCoroutine(Join("ecoDB.sqlite"));
            if (ok == 4)
            {               
                OkText.text = "회원가입 완료!";
                JoinBtnObject.SetActive(false);
            }
    }

    // 회원가입
    IEnumerator Join(string p)
    {
        if (IdInputFiled.text == "")
        {
            if (g == 0)
            {
                ok = 1;
                OkText.text = "아이디 중복 확인을 해주세요.";
            }

            
        }
        else if (id!="" && NameInput.text == "" && PasswordInput.text =="")
        {
            ok = 1;
            OkText.text = "닉네임을 입력해주세요.";
        }
        else if (NameInput.text != "" && PasswordInput.text == "")
        {
            ok = 1;
            OkText.text = "비밀번호를 입력해주세요.";
        } 
        else {
            if (distinct == 1)
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
                            id.ToString() + "', '" + name.ToString() +
                            "', '" + password.ToString() +
                            "', '0', '0', '0', '0', '3')";

                        dbCmd.CommandText = sqlQuery;

                        using (IDataReader reader = dbCmd.ExecuteReader())
                        {
                            dbConnection.Close();
                            reader.Close();
                        }
                    }

                }
                ok = 4;
            }
        }
        yield return null;
    }


    public void OkBtn()
    {
        JoinBtnOnClick();

        Invoke("startRead", 3);
    }

    public void startRead()
    {
        Time.timeScale = 1f;

        if (okk == 1)
        {
            StartCoroutine(Read("ecoDB.sqlite"));
        }

    }

    // 아이디 중복 확인
    IEnumerator Read(string p)
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
                string sqlQuery = "SELECT * FROM ecoCAT WHERE id = '" + id.ToString() + "'";

                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dbid = reader.GetString(0);
                    }
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }

        
            if (dbid != null)
            {
                // 디비에 있는 이름과 일치하면 누군가 사용 중
                if (id.ToString() == dbid.ToString())
                {
                    ok = 1;
                    OkText.text = "이미 사용하고 있는 아이디입니다.";
                }

            }
            else if(dbid == null)// 디비에 없으면 아무도 사용하고 있지 않아서 사용 가능
            {
                ok = 1;
                OkText.text = "사용가능한 아이디입니다.";
                ok = 2;
            }
        
        yield return null;
    }

    // Panel 안에 있는 '확인' 버튼
    public void OkBtn2() {

        if (ok == 1) // 아이디 중복
        {
            OkPanel.SetActive(false);
            ok = 0;
        }
        else if (ok == 2) // 아이디 사용 가능, ok가 3이 되어야 회원가입 가능
        {
            Debug.Log("ok : " + ok);
            // OkPanel.SetActive(false);
            ok = 3;
            distinct = 1;
            okBtnObject.SetActive(false);
            JoinBtnObject.SetActive(true);
            IdInput.SetActive(false);
            googleid.text = id;

        }
        else if (ok == 4) { // 회원가입 후 나오는 창
            OkPanel.SetActive(false);
            ok = 0;
        }
    }
    
}
