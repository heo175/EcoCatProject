/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Unity.Editor;
using Firebase.Database;
using UnityEngine.UI;
public class User
{
    public string username;
    public string email;
    public string userid;
    public User()
    {
    }

    public User(string userid, string username, string email)
    {
        this.username = username;
        this.email = email;
        this.userid = userid;
    }
}
public class FirebaseTest2 : MonoBehaviour
{
    FirebaseManager firebaseManager;
    public Text checkuser;
    void Start()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://본인 프로젝트 ID.firebaseio.com/");
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        User user = new User("TEST", "TEST", "test");
        string json = JsonUtility.ToJson(user);
        reference.Child("users").SetRawJsonValueAsync(json);
    }

}
*/