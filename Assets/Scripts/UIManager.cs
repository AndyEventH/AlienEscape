using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;

public class UIManager : MonoBehaviour
{
    [SerializeField] public GameObject StartGameCanvas;
    [SerializeField] public GameObject InMenuCanvas;
    [SerializeField] public GameObject EndGameCanvas;
    [SerializeField] public GameObject winText;
    [SerializeField] public GameObject loseText;


    public void ShowStartMenu()
    {
        StartGameCanvas.SetActive(true);
    }
}