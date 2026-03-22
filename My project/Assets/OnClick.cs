using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

public class OnClick : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Button button = GetComponent<Button>();
        
        button.onClick.AddListener(() => {;
            DiceRollButtonController.Instance.OnRollButtonClicked();
        });
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
