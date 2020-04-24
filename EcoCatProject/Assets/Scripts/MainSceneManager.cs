using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Mono.Data.SqliteClient;
using System.IO;
using System.Data;
using System;
using UnityEngine.SceneManagement;

public class MainSceneManager : MonoBehaviour
{
    //db에서 값을 받아옴
    bool PlayerFirst;
    public int energy;
    public int animal;
    public int grade;
    public bool imsi;
    public Text energyDisplay;
    private int UseEnergy;
    public GameObject[] AnimalsImage1 = new GameObject[5];
    public GameObject[] AnimalsImage2 = new GameObject[5];
    public GameObject[] AnimalsImage3 = new GameObject[5];
    public GameObject[] AnimalsImage4 = new GameObject[5];
    public GameObject[] AnimalsImage5 = new GameObject[5];
    public GameObject UseEnergyPopup;
    public GameObject UseEnergyButton;
    public Text UseEnergyNum;
    public Text UseEnergyNum2;
    public GameObject[] animalpanel = new GameObject[5];
    string name;
    public Text nameTxt;
    public GameObject useEnergyBtn;

    // Start is called before the first frame update
    void Start()
    {
        // PlayerPrefs에 이름 저장 : name 만들어야해!! (다른 페이지에서 db 로 안 받고 PlayerPrefs로 한다면)
        SelectName();
        nameTxt.text = name;
        PlayerEnergySetting();
        updateMainDisplay();
    }

    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                if (UseEnergyPopup.activeSelf == true)
                {
                    UseEnergyPopup.SetActive(false);
                }
            }
        }

        if (animal == 4) {
            useEnergyBtn.SetActive(false);
        }

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
                        name = reader.GetString(0);
                    }
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }

        Debug.Log(energy);

        yield return null;
    }

    // sqlite db에서 energy, grade, animal 받아오기 - 함수
    public void SelectEnergy()
    {
        StartCoroutine(SelectDBEnergy("ecoDB.sqlite"));
    }

    // sqlite db에서 energy, grade, animal 받아오기
    IEnumerator SelectDBEnergy(string p)
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
                string sqlQuery = "SELECT energy, animal, grade FROM ecoCAT WHERE name = '" + name.ToString() + "'";
                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        energy = Int32.Parse(reader.GetString(0));
                        animal = Int32.Parse(reader.GetString(1));
                        grade = Int32.Parse(reader.GetString(2));
                    }
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }

        Debug.Log(energy);

        yield return null;
    }



    // sqlite db에 energy 업데이트 - 함수
    public void UpdateEnergy()
    {
        StartCoroutine(UpdateDBEnergy("ecoDB.sqlite"));
    }

    // sqlite db에 energy 업데이트
    IEnumerator UpdateDBEnergy(string p)
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
                string sqlQuery = "UPDATE ecoCAT SET energy =" +
                        energy +
                        " WHERE name = '" + name.ToString() + "'";

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

    // sqlite db에 grade 업데이트 - 함수
    public void UpdateGrade()
    {
        StartCoroutine(UpdateDBGrade("ecoDB.sqlite"));
    }

    // sqlite db에 grade 업데이트
    IEnumerator UpdateDBGrade(string p)
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
                string sqlQuery = "UPDATE ecoCAT SET grade =" +
                        grade +
                        " WHERE name = '" + name.ToString() + "'";

                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    dbConnection.Close();
                    reader.Close();
                }
            }

        }

        Debug.Log("grade : " + grade);
        yield return null;
    }

    // sqlite db에 grade, animal 업데이트 - 함수
    public void UpdateAnimal()
    {
        StartCoroutine(UpdateDBAnimal("ecoDB.sqlite"));
    }

    // sqlite db에 grade, animal 업데이트
    IEnumerator UpdateDBAnimal(string p)
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
                string sqlQuery = "UPDATE ecoCAT SET grade =" +
                        grade +
                        ", animal =" + animal + " WHERE name = '" + name.ToString() + "'";

                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    dbConnection.Close();
                    reader.Close();
                }
            }

        }

        Debug.Log("grade : " + grade + " animal : " + animal);
        yield return null;
    }


    void PlayerEnergySetting()
    {
        SelectEnergy(); // db에서 받아옴
        Debug.Log("현재 energy : " + energy + " grade : " + grade + " animal : " + animal);
        PanelOnOff();
        EnergyDisplay();
    }

    void PanelOnOff()
    {
        for (int i = 0; i < 5; i++)
        {
            if (animal == i)
            {
                animalpanel[i].SetActive(true);
            }
            else if (animal != i)
            {
                animalpanel[i].SetActive(false);
            }
        }
    }

    void EnergyDisplay()
    {
        energyDisplay.text = "" + energy;
    }

    public void ClosePopup()
    {
        UseEnergyPopup.SetActive(false);
    }

    public void UseEnergy1() // 에너지 쓰겠다는 팝업 뜨고 사용 에너지가 몇인지 나옴
    {
        UseEnergyPopup.SetActive(true);
        UseEnergy = 2000 * ((grade % 5) + 1);
        if (energy >= (UseEnergy))
        {
            UseEnergyButton.SetActive(true);
            UseEnergyNum.text = "" + UseEnergy;
            UseEnergyNum2.text = "" + UseEnergy;
        }
        else
        {
            UseEnergyButton.SetActive(false);
            UseEnergyNum2.text = "";
        }
    }

    public void UseEnergy2() // 에너지 사용
    {
        energy -= UseEnergy;
        UpdateEnergy(); // energy 가 db에 업데이트 되는 코드
        Debug.Log("에너지 : " + energy);
        UseEnergyPopup.SetActive(false);
        //이펙트? 넣으면 좋은뎅
        if (!(grade % 5 == 4))
        {
            EnergyDisplay();
            grade++;
            UpdateGrade(); // grade 가 db에 업데이트 되는 코드
            updateMainDisplay();

        }
        else if ((grade % 5 == 4))
        {
            //현재동물단계 엔딩 + 다음단계 튜토리얼 실행

            animal++;
            grade = 0;
            UpdateAnimal(); // animal, grade 가 db에 업데이트 되는 코드 
            PanelOnOff();
            EnergyDisplay();
            // updateMainDisplay();

            // 아웃트로 가자
            goAnimalOutro();
        }
        //db에 저장
    }

    void updateEnergy()
    {
        energyDisplay.text = " " + energy;
    }

    void updateMainDisplay() // 화면 바뀜
    {
        /*
        if (animal > 4)
        {

            for (int i = 0; i < 5; i++)
            {
                AnimalsImage1[i].SetActive(false);
                AnimalsImage2[i].SetActive(false);
                AnimalsImage3[i].SetActive(false);
                AnimalsImage4[i].SetActive(false);
                AnimalsImage5[i].SetActive(true);
            }
        }
        else
        {
        */
            if (grade % 5 == 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    AnimalsImage1[i].SetActive(true);
                    AnimalsImage2[i].SetActive(false);
                    AnimalsImage3[i].SetActive(false);
                    AnimalsImage4[i].SetActive(false);
                    AnimalsImage5[i].SetActive(false);
                }
            }
            else if (grade % 5 == 1)
            {
                for (int i = 0; i < 5; i++)
                {
                    AnimalsImage1[i].SetActive(false);
                    AnimalsImage2[i].SetActive(true);
                    AnimalsImage3[i].SetActive(false);
                    AnimalsImage4[i].SetActive(false);
                    AnimalsImage5[i].SetActive(false);
                }
            }
            else if (grade % 5 == 2)
            {
                for (int i = 0; i < 5; i++)
                {
                    AnimalsImage1[i].SetActive(false);
                    AnimalsImage2[i].SetActive(false);
                    AnimalsImage3[i].SetActive(true);
                    AnimalsImage4[i].SetActive(false);
                    AnimalsImage5[i].SetActive(false);
                }
            }
            else if (grade % 5 == 3)
            {
                for (int i = 0; i < 5; i++)
                {
                    AnimalsImage1[i].SetActive(false);
                    AnimalsImage2[i].SetActive(false);
                    AnimalsImage3[i].SetActive(false);
                    AnimalsImage4[i].SetActive(true);
                    AnimalsImage5[i].SetActive(false);
                }
            }
            else if (grade % 5 == 4)
            {
                for (int i = 0; i < 5; i++)
                {
                    AnimalsImage1[i].SetActive(false);
                    AnimalsImage2[i].SetActive(false);
                    AnimalsImage3[i].SetActive(false);
                    AnimalsImage4[i].SetActive(false);
                    AnimalsImage5[i].SetActive(true);
                }
            }
        
    }

    void goAnimalOutro()
    {
        if (animal == 1 && grade == 0)
        {
            SceneManager.LoadScene("PolarBearOutro");
        }

        if (animal == 2 && grade == 0)
        {
            SceneManager.LoadScene("DolphinOutro");
        }

        if (animal == 3 && grade == 0)
        {
            SceneManager.LoadScene("PandaOutro");
        }

        if (animal == 4 && grade == 0)
        {
            SceneManager.LoadScene("TreeOutro");
        }

    }
}
