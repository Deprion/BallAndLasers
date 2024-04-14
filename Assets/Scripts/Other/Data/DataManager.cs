using UnityEngine;
using YG;

public class DataManager : MonoBehaviour
{
    private void SaveData()
    {
        YandexGame.SaveProgress();
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus) SaveData();
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }
}
