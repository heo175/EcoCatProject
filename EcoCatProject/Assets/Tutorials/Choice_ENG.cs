using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Choice_ENG : MonoBehaviour
{
    public SimpleDialogMgr_ENG Dialog;//Dialogue script linked to this ChoiceSentence Object
    public bool ContinueChoice = false;//Is there another ChoiceSentence in the following Dialogue?
    public TextAsset DialogFile;//Dialog file for next Dialogue
    public Sprite[] imageFile;//Image file for next Dialogue
    public bool takeControl = false;//Do you take Variable
    public int take;//take Variable

    public void Change()//File replacement function for next ChoiceSentence
    {
        OnOff();

        if (takeControl == true)
        {
            Dialog.take = take;
        }
        if (Dialog.theImage != null)//If ImageFile is NOT empty
        {
            ChangeImageFile();
        }
        ChangeDialogFile();

        Disable();
    }

    void ChangeDialogFile()//Function to replace Dialog file
    {
        Dialog.ReadyDialogue(DialogFile);
    }
    void ChangeImageFile()//Function to replace Image file
    {
        Dialog.ImageFile = imageFile;
    }
    void OnOff()//Function to decide whether or not to use the ChoiceSentence again
    {
        Dialog.ChoiceOnOff(ContinueChoice);
    }
    public void Disable()//Deactivated Function
    {
        gameObject.transform.parent.gameObject.SetActive(false);
    }
}