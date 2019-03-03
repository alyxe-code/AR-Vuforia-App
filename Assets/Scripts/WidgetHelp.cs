using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WidgetHelp : MonoBehaviour
{

    public Color InitBackground;
    public Color FinalBackground;

    public RawImage Background;

    public bool IsCompleted;

    public TextMeshProUGUI Message;
    public Button BtnShowMessage;

    public static bool ShouldWork = true;

    public string SessionFile = "widget-help";

    private void Start()
    {
        SessionFile = $"{Application.persistentDataPath}/{SessionFile}";
        if (File.Exists(SessionFile))
        {
            var content = File.ReadAllText(SessionFile);
            if (content == "shown=true")
            {
                Destroy(gameObject);
            }
        }

        IsCompleted = false;
        Background.color = InitBackground;

        Message.gameObject.SetActive(false);

        Debug.Log($"{InitBackground.a} {FinalBackground.a}");
    }

    public void StartChangeBackground()
    {
        if (!ShouldWork) return;

        StartCoroutine(ChangeBackground().GetEnumerator());
        File.WriteAllText(SessionFile, "shown=true");
    }

    private IEnumerable ChangeBackground()
    {
        var color = Background.color;
        while (Background.color.a <= FinalBackground.a)
        {
            color.a++;
            Background.color = color;
            Debug.Log($"Changing... {color.a} -> {FinalBackground.a}");
        }

        IsCompleted = true;

        Message.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);

    }
}
