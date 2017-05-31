using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneNameManager : MonoBehaviour
{
    public void Start()
    {
        GetComponent<Text>().text = SceneManager.GetActiveScene().name;
    }
}
