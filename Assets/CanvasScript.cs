using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasScript : MonoBehaviour
{
    public void LoadNextLevel()
    {
        SceneController.instance.LoadNextLevel();
    }
}
