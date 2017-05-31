using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosePanel : MonoBehaviour
{
    [SerializeField] private GameObject Panel;

    public void PanelClose()
    {
        Panel.SetActive(false);
    }

}
