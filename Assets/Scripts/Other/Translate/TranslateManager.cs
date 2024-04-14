using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using YG;

public class TranslateManager : MonoBehaviour
{
    [SerializeField] private TextAsset ru, eng;

    public static TranslateManager inst;
    private Dictionary<string, string> translate = new Dictionary<string, string>();

    private void Awake()
    {
        DontDestroyOnLoad(this);

        inst = this;

        Setup();
    }

    private void Setup()
    {
        string lang = YandexGame.EnvironmentData.language;

        if (lang == "ru" || lang == "be" || lang == "kk" || lang == "uk" || lang == "uz")
            Fill(ru);
        else
            Fill(eng);
    }

    private void Fill(TextAsset txt)
    {
        Regex regex = new Regex(":|;\r?\n?");
        var arr = regex.Split(txt.text);

        for (int i = 0; i < arr.Length - 1; i += 2)
        {
            translate[arr[i]] = arr[i + 1];
        }
    }

    public string GetText(string text)
    {
        return translate[text];
    }
}