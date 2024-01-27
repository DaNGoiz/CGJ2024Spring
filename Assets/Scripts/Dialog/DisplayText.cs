using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// This script is attached to the DialogCanvas
/// It reads the text from a text file and displays it on the screen
/// It also displays the face of the character speaking
/// 
/// TODO:
/// 分离读取和显示的功能
/// 从一个预设好的列表里统一读取
/// 从一个预设好的列表里统一做出文字的显示效果
/// 相当于读取file的脚本在提取数据，然后把数据直接传给控制输出的脚本，控制输出的脚本再根据数据做出显示效果
/// </summary>

public class DisplayText : MonoBehaviour
{
    // UI component
    private Text textLabel; // 使用 Text 组件而不是 TextMeshProUGUI
    private Image faceImage;

    [Header("Text")]
    public TextAsset textFile;
    public int currentLine;

    [Header("Avatar")]
    public Sprite[] faceSprites;

    [Header("Effect")]
    public float textSpeed;

    bool isTypingFinished;
    bool cancelTyping;

    List<string> textList = new List<string>();

    void Awake()
    {
        // Get the UI components
        textLabel = GetComponentInChildren<Text>(); // 获取 Text 组件
        faceImage = GetComponentInChildren<Image>();

        GetTextFronFile(textFile); 
    }

    private void OnEnable()
    {
        // Start to play text when the object is enabled
        isTypingFinished = true;
        StartCoroutine(PlayTextByCharacter());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && currentLine == textList.Count)
        {
            currentLine = 0;
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (isTypingFinished && !cancelTyping)
            {
                StartCoroutine(PlayTextByCharacter());
            }
            else if (!isTypingFinished)
            {
                cancelTyping = !cancelTyping;
            }
        }
    }

    void GetTextFronFile(TextAsset textFile)
    {
        textList.Clear();
        currentLine = 0;

        if (textFile != null)
        {
            string text = textFile.text;
            textList = new List<string>(text.Split('\n'));
        }
    }

    IEnumerator PlayTextByCharacter()
    {
        isTypingFinished = false;
        textLabel.text = "";
         
        // should be moved into a function
        
        switch(textList[currentLine])
        {
            case "A":
                faceImage.sprite = faceSprites[0];
                currentLine++;
                print("A");
                break;
            case "B":
                faceImage.sprite = faceSprites[1];
                currentLine++;
                break;
        }

        int letter = 0;
        while(!cancelTyping && letter < textList[currentLine].Length - 1)
        {
            textLabel.text += textList[currentLine][letter];
            letter++;
            yield return new WaitForSeconds(textSpeed);
        }
        textLabel.text = textList[currentLine];
        cancelTyping = false;
        isTypingFinished = true;
        currentLine++;
    }
}
