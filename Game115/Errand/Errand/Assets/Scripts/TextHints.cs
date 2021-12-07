using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextHints : MonoBehaviour
{

    //Text Hint Logic
    public static string message; //message content

    static Text textHint; //Holds the string (message variable)

    //Timer
    public static bool textOn = false;

    public static float timer = 0.0f;

    [SerializeField] public static float textOnTime = 5.0f;

    // Start is called before the first frame update
    void Start()
    {

        textHint = GetComponent<Text>();

        timer = 0.0f;

        textOn = false;

        textHint.text = "";

    }

    // Update is called once per frame
    void Update()
    {

        if (textOn == true)
        {

            textHint.enabled = true;

            textHint.text = message;

            timer += Time.deltaTime;

        }

        if (timer >= textOnTime)
        {

            textOn = false;

            textHint.enabled = false;

            timer = 0.0f;

        }

    }

}
