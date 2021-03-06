using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryCollect : MonoBehaviour
{

    //Fruit Collect Logic
    [SerializeField] public static int fruits = 0;

    //Bucket Logic
    [SerializeField] public static bool bucket = false;

    //Flower Logic
    [SerializeField] public static bool flower = false;

    //Holds the images
    public static Image appleUI;
    public static Image pearUI;
    public static Image peachUI;
    public static Image melonUI;

    //Fruit Sprites
    [SerializeField] Sprite apple;
    [SerializeField] Sprite pear;
    [SerializeField] Sprite peach;
    [SerializeField] Sprite melon;
    [SerializeField] Sprite fruit0;

    //Fruit UI Booleans
    public static bool hasApple = false;
    public static bool hasPear = false;
    public static bool hasMelon = false;
    public static bool hasPeach = false;

    // Start is called before the first frame update
    void Start()
    {

        //Find Fruit UI GameObject
        appleUI = gameObject.GetComponent<Image>();
        pearUI = gameObject.GetComponent<Image>();
        peachUI = gameObject.GetComponent<Image>();
        melonUI = gameObject.GetComponent<Image>();

        //Hide Images on Start
        appleUI.enabled = false;
        pearUI.enabled = false;
        peachUI.enabled = false;
        melonUI.enabled = false;

        //Set initial fruit value
        fruits = 0;

    }

    // Update is called once per frame
    void Update()
    {

        if (fruits == 1)
        {

            //fruitUI.sprite = fruit1;

            CheckFruit();

            //fruitUI.enabled = true;

        }
        else if (fruits == 2)
        {

            //fruitUI.sprite = fruit2;

            CheckFruit();

        }
        else if (fruits == 3)
        {

            //fruitUI.sprite = fruit3;

            CheckFruit();

        }
        else if (fruits >= 4)
        {

            //fruitUI.sprite = fruit4;

            CheckFruit();

        }
        else
        {

            //fruitUI.sprite = fruit0;

        }

    }

    void CheckFruit()
    {

        if (apple == true)
        {

            appleUI.sprite = apple;

        }
        else if (pear == true)
        {

            pearUI.sprite = pear;

        }
        else if (peach == true)
        {

            peachUI.sprite = peach;

        }
        else if (melon == true)
        {

            melonUI.sprite = melon;

        }

    }

}
