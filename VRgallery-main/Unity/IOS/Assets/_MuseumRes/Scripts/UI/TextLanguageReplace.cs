using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextLanguageReplace : MonoBehaviour
{
    private Text text;

    public string ZH;
    public string EN;
    public string JA;

    private void Awake()
    {
        text = this.GetComponent<Text>();
    }

    public void ReplaceLanguageText(Language language)
    {

        switch (language)
        {
            case Language.中文:
                text.text = ZH;
                break;
            case Language.英语:
                text.text = EN;
                break;
            case Language.日文:
                text.text = JA;
                break;
        }
    }
}
