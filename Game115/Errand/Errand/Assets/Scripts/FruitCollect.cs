using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FruitCollect : MonoBehaviour
{

    //Fruit Collect Logic
    [SerializeField] public static int fruits = 0;

    //Bucket Logic
    [SerializeField] public static bool bucket = false;

    //Flower Logic
    [SerializeField] public static bool flower = false;

    //Holds the images
    /*public static Image appleUI;
    public static Image pearUI;
    public static Image peachUI;
    public static Image melonUI;*/

    //Fruit Images
    [SerializeField] Image apple;
    [SerializeField] Image pear;
    [SerializeField] Image peach;
    [SerializeField] Image melon;
    //[SerializeField] Image fruit0;

    //Fruit UI Booleans
    public static bool hasApple = false;
    public static bool hasPear = false;
    public static bool hasMelon = false;
    public static bool hasPeach = false;

    // Start is called before the first frame update
    void Start()
    {

        //Find Fruit UI GameObject
        /*appleUI = gameObject.GetComponent<Image>();
        pearUI = gameObject.GetComponent<Image>();
        peachUI = gameObject.GetComponent<Image>();
        melonUI = gameObject.GetComponent<Image>();*/

        //Hide Images on Start
        /*appleUI.enabled = false;
        pearUI.enabled = false;
        peachUI.enabled = false;
        melonUI.enabled = false;*/

        apple.enabled = false;
        pear.enabled = false;
        peach.enabled = false;
        melon.enabled = false;

        //Set initial fruit value
        fruits = 0;

    }

    // Update is called once per frame
    void Update()
    {

        if (hasApple == true)
        {

            apple.enabled = true;

            hasApple = false;

        }
        
        if (hasPear == true)
        {

            pear.enabled = true;

            hasPear = false;

        }
        
        if (hasPeach == true)
        {

            peach.enabled = true;

            hasPeach = false;

        }
        
        if (hasMelon == true)
        {

            melon.enabled = true;

            hasMelon = false;

        }

    }

    void CheckFruit()
    {

        if (hasApple == true)
        {

            apple.enabled = true;

        }
        else if (hasPear == true)
        {

            pear.enabled = true;

        }
        else if (hasPeach == true)
        {

            peach.enabled = true;

        }
        else if (hasMelon == true)
        {

            melon.enabled = true;

        }

    }

}
