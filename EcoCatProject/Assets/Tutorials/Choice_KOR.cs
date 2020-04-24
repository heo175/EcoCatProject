using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Choice_KOR : MonoBehaviour
{
    public SimpleDialogMgr_KOR Dialog;// 이 선택문 오브젝트에 연결되는 대화창 스크립트
    public bool ContinueChoice = false;//선택문 뒤로 이어지는 대화에 선택문이 또 있는가
    public TextAsset DialogFile;//이 뒤로 이어질 대화에 필요한 대화창 파일
    public Sprite[] imageFile;//이 뒤로 이어질 대화에 필요한 이미지 파일
    public bool takeControl = false;//take변수를 컨트롤 할것인가
    public int take;//take변수

    public void Change()//다음 대화로 이어지기위한 파일 교체함수
    {
        OnOff();

        if (takeControl == true)
        {
            Dialog.take = take;
        }
        if (Dialog.theImage != null)//이미지오브젝트가 비어있지 않으면
        {
            ChangeImageFile();
        }
        ChangeDialogFile();

        Disable();
    }

    void ChangeDialogFile()//대화창 파일 교체함수
    {
        Dialog.ReadyDialogue(DialogFile);
    }
    void ChangeImageFile()//이미지 파일 교체함수
    {
        Dialog.ImageFile = imageFile;
    }
    void OnOff()//다음에도 또 선택문 할건지 결정하는 함수
    {
        Dialog.ChoiceOnOff(ContinueChoice);
    }
    public void Disable()//자체 비활성화 함수
    {
        gameObject.transform.parent.gameObject.SetActive(false);
    }
}