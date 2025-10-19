using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class DialogueManager_2 : MonoBehaviour
{

    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    private int index;
    private LevelLoader level;
    public float newAlpha = 0.8f;
    public TextMeshProUGUI nameText;
    private bool activeButton = false;
    public Button button1;
    public Button button2;
    public Button button3;
    private bool isTyping = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        level = FindFirstObjectByType<LevelLoader>().GetComponent<LevelLoader>();

        textComponent.text = string.Empty;

        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }


    void StartDialogue()
    {
        index = 0;

        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        isTyping = true;


        if (index == 0)
        {
            yield return new WaitForSeconds(2);

            ChangeAlpha();

            nameText.text = "??";
        }

        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        if (index == 1)
        {
            button1.gameObject.SetActive(true);
        }

        if (index == 2)
        {
            button2.gameObject.SetActive(true);
        }

        if (index == 4)
        {
            button3.gameObject.SetActive(true);
        }

        isTyping = false;
    }

    public void NextLine()
    {

        if (isTyping) return;


        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);

            // transition to next scene
            // level.LoadNextLevel();

        }
    }

    void ChangeAlpha()
    {
        Image img = gameObject.GetComponent<Image>();
        Color c = img.color;   // get the current color
        c.a = newAlpha;        // change the alpha
        img.color = c;         // assign the modified color back
    }


    public void HideButtonOne()
    {
        button1.gameObject.SetActive(false);
    }

    public void HideButtonTwo()
    {
        button2.gameObject.SetActive(false);
    }
    
    public void HideButtonThree()
    {
        button3.gameObject.SetActive(false);
    }
    
}
