using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public TextMeshProUGUI TextMeshProUGUI;
    public TextMeshProUGUI Text_EnterDungeon;
    public TextMeshProUGUI Text_FindGems;
    public TextMeshProUGUI Text_BackToVillage;
    public TextMeshProUGUI Text_DefeatWizard;

    void Awake() {
        Instance = this;
    }

    private void Start() {
        TextMeshProUGUI.text = Text_EnterDungeon.text;
    }
}
