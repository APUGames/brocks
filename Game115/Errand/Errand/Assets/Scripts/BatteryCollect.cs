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
    public static Image fruitUI;

    //Battery Sprites
    [SerializeField] Sprite fruit1;
    [SerializeField] Sprite fruit2;
    [SerializeField] Sprite fruit3;
    [SerializeField] Sprite fruit4;
    [SerializeField] Sprite fruit0;

    // Start is called before the first frame update
    void Start()
    {

        //Find Fruit UI GameObject
        fruitUI = gameObject.GetComponent<Image>();

        //Hide Image on Start
        fruitUI.enabled = false;

        //Set initial fruit value
        fruits = 0;

    }

    // Update is called once per frame
    void Update()
    {

        if (fruits == 1)
        {

            fruitUI.sprite = fruit1;
            fruitUI.enabled = true;

        }
        else if (fruits == 2)
        {

            fruitUI.sprite = fruit2;

        }
        else if (fruits == 3)
        {

            fruitUI.sprite = fruit3;

        }
        else if (fruits >= 4)
        {

            fruitUI.sprite = fruit4;

        }
        else
        {

            fruitUI.sprite = fruit0;

        }

    }

}
