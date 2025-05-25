using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] private int _sceneNumber;

    public void SceneSwitch()
    {
        SceneManager.LoadScene(_sceneNumber);
    }
}
