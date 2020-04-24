using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SimpleDialogMgr_ENG : MonoBehaviour
{
    //Made By MeanDeuLeMiSol
    //Precaution: The LAST LINE of the Dialogue file must be Colon(:)!!!

    #region Variables
    public GameObject Dialog;//Dialog Object
    public TextAsset DialogFile;//Dialog File
    public Text theName;//Name Text Object
    public Text theDialogue;//Dialogue Text Object

    public GameObject theImage;//Image Object
    public GameObject nextButton;//SkipButton Object
    public GameObject QuitButton;//QuitButton Object
    public GameObject[] Choice;//ChoiceSentence Object

    public string[] NtextLines;//Name
    public string[] DtextLines;//Line
    public Sprite[] ImageFile;//illustration File

    public bool useTyping = false;//Are you use AutoTyping effect
    public float typingSpeed = 0f;//AutoTyping effect speed
    private bool isTyping = false;//Is the autotyping effect currently running?
    private bool cancelTyping = false;//Typing effect stop

    public int currentLine = 0;//Current line
    public int endAtLine;//Last line
    public float BwaitTime = 1f;//SkipButton waittime

    [System.NonSerialized]
    public bool ContinueChoice = false;//Do you continue to use the ChoiceSentence next take?
    [System.NonSerialized]
    public int take = 0;//How many Choice step
    [System.NonSerialized]
    public int howManyChoice = 0;//Number Of ChoiceSentence

    #endregion


    //Functions that are executed each time this script is activated
    void OnEnable()
    {
        ReadyDialogue(null);
    }


    //void Update()
    //{//See here for a person who says, "I want to press it with the keyboard, I'm annoying to click with the mouse."
    //    if (nextButton.activeSelf == true)
    //    {
    //        if (Input.GetKeyDown(KeyCode.Space))//It is important to enter it only once with the keyboard.
    //        {
    //            skipButtonControl();
    //        }
    //    }
    //}

    //Dialog box ready function
    public void ReadyDialogue(TextAsset DFile2)
    {
        currentLine = 0;//Talk state Initialization      

        if (Choice[0] != null)//If you use ChoiceSentence
        {
            howManyChoice = Choice.Length - 1;//Initialize to array length

            if (DFile2 != null)//If it is next DialogFile is NOT empty
            {
                DialogFile = DFile2;//Change Dialog file             
            }
            else if (take == 0)//If it is the first take
            {
                ContinueChoice = true;//ChiceSentence use THIS take
            }
            if (howManyChoice > take)//If it is NOT the LAST take
            {
                Choice[take].SetActive(false);
            }
        }

        if (DialogFile != null)//Dialog File is NOT empty
        {
            char[] cut = { '\n', ':' };//The characters that are the basis for cutting
            string[] imsi = DialogFile.text.Split(cut);//Cut the string in the file properly

            NtextLines = new string[imsi.Length / 2];//Sets the size of the array divided by 2
            DtextLines = new string[imsi.Length / 2];//Because it's two pieces of information with Name and Dialogue

            int a = 0, b = 0;//Classification Variables
            for (int n = 0; n < imsi.Length; n++)//Classify by content
            {
                if (n % 2 == 0)//Information with even index number
                {
                    NtextLines[a] = imsi[n];//To NameArray
                    a++;
                }
                else//Information with odd index number
                {
                    DtextLines[b] = imsi[n];//To DialogueArray
                    b++;
                }
            }
        }
        endAtLine = NtextLines.Length - 1;//Initialize to array length

        if (theImage != null)//If you use illustration
        {
            theImage.SetActive(false);
        }

        if (useTyping == true)//If you typing effect
        {
            nextButton.SetActive(false);
            NextDialogue();
        }
        else//If you don't use
        {
            nextButton.SetActive(true);
            if (QuitButton != null)//If you use QuitButton
            {
                QuitButton.SetActive(true);
            }
            NextText();
        }
    }

    //Next Dialogue function
    #region NextTalk

    void NextDialogue()//If you use AutoTyping effect
    {
        if (isTyping == false)//If AutoTyping effect is NOT currently running
        {
            if (theImage != null)//Use the illustration
            {
                if (ImageFile[currentLine] != null)//If ImageFile is NOT empty
                {
                    theImage.GetComponent<Image>().sprite = ImageFile[currentLine];//Put the ImageFile in ImageObject
                    theImage.SetActive(true);//ImageObject Activated
                }
                else//ImageFile is empty 
                {
                    theImage.SetActive(false);//ImageObject Deactivated
                }
            }

            theName.text = NtextLines[currentLine];//Put the NameArray in NameObject

            if (currentLine == endAtLine)//if Dialogue is over
            {
                DisableDialog();//Deactivated function
            }
            else//if Dialog is NOT over
            {
                StartCoroutine(TypingEffect(DtextLines[currentLine]));//AutoTyping effect Coroutine Start
            }

            currentLine += 1;//Progressive Variable Update
        }

        else if (isTyping && cancelTyping)//If the output is finished
        {
            cancelTyping = true;//AutoTyping effect Stop
        }

        nextButton.SetActive(false);
        if (QuitButton != null)//If you use QuitButton
        {
            QuitButton.SetActive(false);
        }
    }



    void NextText()//If you don't use AutoTyping effect
    {
        if (isTyping == false)//If AutoTyping effect is NOT currently running
        {
            if (theImage != null)//Use the illustration
            {
                if (ImageFile[currentLine] != null)//If ImageFile is NOT empty
                {
                    theImage.GetComponent<Image>().sprite = ImageFile[currentLine];//Put the ImageFile in ImageObject
                    theImage.SetActive(true);//ImageObject Activated
                }
                else//ImageFile is empty 
                {
                    theImage.SetActive(false);//ImageObject Deactivated
                }
            }

            theName.text = NtextLines[currentLine];//Put the NameArray in NameObject
            theDialogue.text = DtextLines[currentLine];//Put the DialogueArray in DialogueObject

            currentLine += 1;//Progressive Variable Update

            if (currentLine > endAtLine)//If big
            {
                DisableDialog();//Deactivated function
            }
        }

        if (Choice[0] != null)//If you use ChoiceSentence
        {
            if (howManyChoice > take)//If it is NOT the LAST take
            {
                if (ContinueChoice == true)//If ChiceSentence use THIS take
                {
                    if (currentLine == endAtLine)//if Dialogue is over
                    {
                        nextButton.SetActive(false);
                        Choice[take].SetActive(true);
                    }
                    else//if Dialogue is NOT over
                    {
                        nextButton.SetActive(true);
                        Choice[take].SetActive(false);
                    }
                }
                else//If ChiceSentence DON'T use THIS take
                {
                    nextButton.SetActive(true);
                    Choice[take].SetActive(false);
                }
            }
            else//If it is the LAST take
            {
                nextButton.SetActive(true);
            }
        }
    }
    #endregion

    ////AutoTyping Effect Function
    private IEnumerator TypingEffect(string lineOfletter)
    {
        int letter = 0;//Variable to check the number of characters printed
        theDialogue.text = "";//What you see actually
        isTyping = true;//AutoTyping effect is now running
        cancelTyping = false;//AutoTyping effect Stop

        while (isTyping && !cancelTyping && (letter < lineOfletter.Length - 1))
        {
            theDialogue.text += lineOfletter[letter];
            letter += 1;
            yield return new WaitForSeconds(typingSpeed);//Watting time until next character is printed
        }

        theDialogue.text = lineOfletter;
        isTyping = false;
        cancelTyping = false;

        if (QuitButton != null)//If you use QuitButton
        {
            QuitButton.SetActive(true);
        }
        yield return new WaitForSeconds(BwaitTime);//SkipButton WaitTime

        if (Choice[0] == null)//If you DON'T use ChoiceSentence
        {
            nextButton.SetActive(true);
        }
        else//If you use ChoiceSentence
        {
            if (howManyChoice > take)//If it is NOT the LAST take
            {
                if (ContinueChoice == true)//If ChiceSentence use THIS take
                {
                    if (currentLine == endAtLine)//if Dialogue is over
                    {
                        nextButton.SetActive(false);
                        Choice[take].SetActive(true);
                    }
                    else//if Dialogue is NOT over
                    {
                        nextButton.SetActive(true);
                        Choice[take].SetActive(false);
                    }
                }
                else//If ChiceSentence DON'T use THIS take
                {
                    nextButton.SetActive(true);
                    Choice[take].SetActive(false);
                }
            }
            else//If it is the LAST take
            {
                nextButton.SetActive(true);
            }
        }
    }

    //Function that executes the appropriate function depending on the situation when the Skip Button is pressed
    public void skipButtonControl()
    {
        if (useTyping == true)
        {
            NextDialogue();
        }
        else
        {
            NextText();
        }
    }

    //Activate Deactivate functions
    #region ActiveDeactive

    public void ChoiceOnOff(bool onoff)//ChoiceSentence ON/OFF function
    {
        take += 1;//Progressive Variable Update
        ContinueChoice = onoff;//Get the value with EventTrigger and put it into a variable
    }

    //Dialog Activated function
    public void EnableDialog()
    {
        OnEnable();
    }

    //Dialog Deactivated fuction
    public void DisableDialog()
    {
        currentLine = 0;//Dialogue state Initialization

        if (theImage != null)//If you use illustration
        {
            theImage.SetActive(false);
        }

        if (Choice[0] != null)//If you use ChoiceSentence
        {
            if (howManyChoice > take)//If it is NOT the LAST take
            {
                for (int n = 0; n < Choice.Length - 1; n++)
                {
                    Choice[n].SetActive(false);
                }
            }
        }

        StopCoroutine("TypingEffect");//Coroutine End
        Dialog.SetActive(false);//Dialog Deactivated
    }
    #endregion
}