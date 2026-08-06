using UnityEngine;
using System.IO;
using System;

[System.Serializable]
public class GameSettings //это просто контейнер, который показывает какие настройки вообще есть. Сюда можно добавить и убрать поля. чтобы добавить или убрать настройки
{
    public float minTextSize = 1f;
    public float maxTextSize = 5f;
}

public class GameSetingsManager : MonoBehaviour
{
    public static Action OnSettingsChange;
    private GameSettings _settings;
    private static GameSetingsManager thisScript;

    private void Awake()
    {
        _settings = LoadSettings();
        thisScript = this;
    }
    public static GameSettings GetSettings()
    {
        return thisScript._settings;
    }

    private void SaveSettings()
    {
        string path = Path.Combine(Application.persistentDataPath, "settings.json");
        string json = JsonUtility.ToJson(_settings, true);

#if UNITY_WEBGL && !UNITY_EDITOR
        // Для WebGL нельзя писать напрямую. Здесь можно вызвать JS для сохранения
        // или вообще ничего не делать, если используешь экспорт/импорт вручную.
        Debug.Log("WebGL: используйте экспорт/импорт вместо автосохранения");
#else
        Directory.CreateDirectory(Path.GetDirectoryName(path));
        File.WriteAllText(path, json);
#endif
    }

    private GameSettings LoadSettings()
    {
        string path = Path.Combine(Application.persistentDataPath, "settings.json");

#if UNITY_WEBGL && !UNITY_EDITOR
        // В WebGL файл может отсутствовать, если не было импорта
        return new GameSettings();
#else
        if (!File.Exists(path))
            return new GameSettings();

        string json = File.ReadAllText(path);
        return JsonUtility.FromJson<GameSettings>(json);
#endif
    }

    private void ApplySettings()
    {
        // Применяй настройки к громкости, шрифту и т.д.

        //Debug.Log($"Volume: {_settings.MasterVolume}, FontSize: {_settings.FontSize}");
    }
}
