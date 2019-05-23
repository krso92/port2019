using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Image profileImage;

    // Start is called before the first frame update
    void Start()
    {
        profileImage.sprite = DateManager.Instance.currentDate.ProfileImage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
