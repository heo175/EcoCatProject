/*
using UnityEngine;
using UnityEngine.UI;
using Firebase.Unity.Editor;
using Firebase.Database;
using Firebase;

public class RealtimeDatabase : MonoBehaviour
{
    public FirebaseManager FirebaseManager;
    public FirebaseApp firebaseApp;
    DatabaseReference databaseReference;
    
    public Text checkuser;
    public Text ScoreDisplay;

    private void Awake()
    {
        firebaseApp = FirebaseDatabase.DefaultInstance.App;
        firebaseApp.SetEditorDatabaseUrl("https://environmentkeepereco.firebaseio.com/");
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        FirebaseApp.DefaultInstance.SetEditorP12FileName("environmentkeepereco-620922cd5dcd.p12");

        // 아래 비밀번호에는 특별하게 설정한거 없으면 notasecret 일 겁니다.
        FirebaseApp.DefaultInstance.SetEditorP12Password("notasecret");
    }

    public void InitDatabase()
    {
        if (FirebaseManager.user != null)
        {
            WriteNewUser(FirebaseManager.user.UserId, FirebaseManager.user.DisplayName, FirebaseManager.user.Email);
        }
    }

    public void InitDatabase2()
    {
        if (FirebaseManager != null)
        {
            checkuser.text = "로그인상태!!!";
            WriteNewUser(FirebaseManager.userId, FirebaseManager.userName, FirebaseManager.userEmail);
        }
        else if (FirebaseManager == null)
        {
            checkuser.text = "로그아웃상태!!";
        }
    }

    private void WriteNewUser(string uid, string name, string email)
    {
        User user = new User(uid ,name, email);
        string json = JsonUtility.ToJson(user);
        databaseReference.Child("users").Child(uid).SetRawJsonValueAsync(json);
        checkuser.text = "id" + uid + "이름" + name + "이메일" + email;
    }
}
 
    public class User
    {
        public string username;
        public string email;

        public User()
        {
        }

        public User(string username, string email, string email1)
        {
            this.username = username;
            this.email = email;
        }
    }



    */