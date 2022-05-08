using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuUIHandler : MonoBehaviour
{
    [SerializeField] private TMP_InputField NameInputField;

    // Start is called before the first frame update
    void Start()
    {
        NameInputField.onEndEdit.AddListener(PlayerNameChanged);
    }

    public void PlayerNameChanged(string playerName)
    {
        StateManager.Instance.PlayerName = playerName;
    }

    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
