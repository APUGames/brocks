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

            fruits++;

            hasApple = false;

        }
        
        if (hasPear == true)
        {

            pear.enabled = true;

            fruits++;

            hasPear = false;

        }
        
        if (hasPeach == true)
        {

            peach.enabled = true;

            fruits++;

            hasPeach = false;

        }
        
        if (hasMelon == true)
        {

            melon.enabled = true;

            fruits++;

            hasMelon = false;

        }

        if (PlayerCollisions.talkToGirl >= 1)
        {

            apple.enabled = false;
            pear.enabled = false;
            melon.enabled = false;
            peach.enabled = false;

        }

    }

}
