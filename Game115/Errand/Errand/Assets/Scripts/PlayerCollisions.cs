using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{

    //Door Animator
    [SerializeField] GameObject door; //Door variable is initialized, makes a slot in the inspector to drag the door into

    Animator doorAnimator;

    //To check if doorOpen = true or not
    private bool doorIsOpen = false;

    //Door timer
    private bool startDoorTimer = false;
    private float doorTimer = 0.0f;
    //commands in [] represent a verb, [SerializeField] tells computer to make a new field in a game object
    [SerializeField] private float doorOpenTime = 3.0f;

    //Door Sounds
    [SerializeField] private AudioClip doorOpenSound;
    [SerializeField] private AudioClip doorShutSound;

    //Voice Acting
    //I definitely did this the long way, especially since each line has its own method called by the Invoke() function... but it works soooooo...
    [SerializeField] private AudioClip Monologue1;
    [SerializeField] private AudioClip Monologue2;
    [SerializeField] private AudioClip Dialogue0Index1;
    [SerializeField] private AudioClip Dialogue0Index3;
    [SerializeField] private AudioClip Dialogue0Index5;
    [SerializeField] private AudioClip Dialogue0Index7;
    [SerializeField] private AudioClip Dialogue1Index1;
    [SerializeField] private AudioClip Dialogue1Index3;
    [SerializeField] private AudioClip Dialogue1Index6;
    [SerializeField] private AudioClip Dialogue1Index8;
    [SerializeField] private AudioClip Dialogue1Index10;
    [SerializeField] private AudioClip Dialogue1Index12;
    [SerializeField] private AudioClip Dialogue1Index14;
    [SerializeField] private AudioClip Dialogue1Index16;
    [SerializeField] private AudioClip Dialogue2Index0;
    [SerializeField] private AudioClip Dialogue2Index2;
    [SerializeField] private AudioClip Dialogue2Index4;

    //General Timer for this code
    private float generalTimer = 0.0f;

    //index for computer to keep track of which line it's on for dialogue
    int index = 0;

    //Fruit Collect Sound
    [SerializeField] private AudioClip fruitCollectSound;

    private new AudioSource audio;

    //To keep sickGirl dialogue in correct succession
    private int talkToGirl = 0;

    //Water boolean
    private bool hasWater = false;

    // Start is called before the first frame update
    void Start()
    {

        doorAnimator = door.GetComponent<Animator>();

        audio = GetComponent<AudioSource>();

        //Call for beginning text, might as well make it a method :/
        Invoke("BeginningMonologue1", 2.0f);

    }

    // Update is called once per frame
    void Update()
    {
        
        //Run the timer here
        if(startDoorTimer == true)
        {

            doorTimer += Time.deltaTime;

        }

        if(doorTimer > doorOpenTime)
        {

            ShutDoor();

            doorTimer = 0.0f;

        }

        if (BatteryCollect.fruits == 4)
        {

            Invoke("UnlockMono1", 0.0f);

        }

    }

    void UnlockMono1()
    {

        //play audio here

        TextHints.message = "I have all the fruits I need now. I'll go back and give them to her by hand.";
        TextHints.textOn = true;
        TextHints.textOnTime = 5.0f;

        Invoke("UnlockMono2", 5.0f);

    }

    void UnlockMono2()
    {

        //play audio here

        TextHints.textOn = true;
        TextHints.message = "Since she's bedridden I'll have to give it to her by touching the bed.";
        TextHints.textOnTime = 5.0f;

        //increasing fruits here so the monologue doesn't loop, coded so it doesn't affect UI or door unlocking (I tested it don't worry future me)
        BatteryCollect.fruits++;

    }

    //Collision Detection
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {

        if (hit.gameObject.tag == "shackDoor" && BatteryCollect.fruits >= 4)
        {

            OpenDoor();

            BatteryCollect.fruitUI.enabled = false;

        }
        else if (hit.gameObject.tag == "shackDoor" && BatteryCollect.fruits < 4)
        {

            BatteryCollect.fruitUI.enabled = true;


            TextHints.message = "Can't go back to her yet, I stil have some fruit to find. She told me they'd be in craters on the ground";
            TextHints.textOn = true;

        }
        else if (hit.gameObject.tag == "sickGirl")
        {

            GirlDialogue();

            talkToGirl++;

        }       
        else if (hit.gameObject.tag == "Respawn")
        {

            

        }
        else if (hit.gameObject.tag == "pondWater" && BatteryCollect.bucket == true)
        {

            hasWater = true;

            TextHints.message = "Water: acquired, I don't want to make her wait too long so I should get back.";
            TextHints.textOn = true;
            TextHints.textOnTime = 3.0f;


        }


    }

    void OpenDoor()
    {

        //Set animator parameter
        doorAnimator.SetBool("doorOpen", true);

        //Start door timer
        startDoorTimer = true;

        //Play audio
        if (doorIsOpen == false)
        {

            audio.PlayOneShot(doorOpenSound);

            doorIsOpen = true;

        }

    }

    void ShutDoor()
    {

        //Set animator parameter
        doorAnimator.SetBool("doorOpen", false);

        startDoorTimer = false;

        //Play audio
        if (doorIsOpen == true)
        {

            audio.PlayOneShot(doorShutSound);
            
            doorIsOpen = false;

        }

    }

    //Fruit, Bucket, and Flower Collision (I'm thinking of keeping all objectives in the same function, hopefully this works)
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "fruitObjective")
        {

            BatteryCollect.fruits++;

            audio.PlayOneShot(fruitCollectSound);

            Destroy(other.gameObject);

        }
        else if (other.gameObject.tag == "bucketObjective" && talkToGirl == 1)
        {

            BatteryCollect.bucket = true;

            Destroy(other.gameObject);

        }
        else if (other.gameObject.tag == "flowerObjective" && talkToGirl == 2)
        {

            BatteryCollect.flower = true;

            Destroy(other.gameObject);

        }

    }

    //Welp here we go time to die...
    void GirlDialogue()
    {

        if (talkToGirl == 0)
        {

            Invoke("TTG0I0", 0.0f);

        }
        else if (talkToGirl == 1 && hasWater == true)
        {

            //Invoke("TTG1I0", 0.0f);

        }
        else if (talkToGirl == 2 && BatteryCollect.flower = true)
        {

            //Invoke("TTG2I0", 0.0f);

        }

    }

    //I'm sorry to whoever has been cursed to read this...
    void TTG0I0()
    {

        //audio here

        TextHints.message = "Oh hey, thanks for the (cough cough) fruits.";
        TextHints.textOn = true;
        TextHints.textOnTime = 3.0f; //Time voice audio (F)

        Invoke("TTG0I1", 3.0f);

    }

    void TTG0I1()
    {

        audio.PlayOneShot(Dialogue0Index1);

        TextHints.message = "Mhm no problem.";
        TextHints.textOn = true;
        TextHints.textOnTime = 3.0f; //Time voice audio (M)

        Invoke("TTG0I2", 3.0f);

    }

    void TTG0I2()
    {



    }

    void TTG0I3()
    {



    }

    void TTG0I4()
    {



    }

    void TTG0I5()
    {



    }

    void TTG0I6()
    {



    }

    void TTG0I7()
    {



    }

    void temp()
    {

        if (talkToGirl == 0) //number indicates how many times player has talked to sickGirl thus far
        {

            if (index == 0)
            {

                //audio here

                

                index++;

            }
            else if (index == 1)
            {

                

                index++;

            }
            else if (index == 2)
            {

                //audio here

                TextHints.message = "Before you sit down could you go to the pond and get some water?";
                TextHints.textOn = true;
                TextHints.textOnTime = 3.0f; //Time voice audio (F)

                index++;

            }
            else if (index == 3)
            {

                audio.PlayOneShot(Dialogue0Index3);

                TextHints.message = "Why? I'm not thirsty";
                TextHints.textOn = true;
                TextHints.textOnTime = 3.0f; //Time voice audio (M)

                index++;

            }
            else if (index == 4)
            {

                //audio here

                TextHints.message = "Not for you, for me. My throat's sore from (cough) coughing.";
                TextHints.textOn = true;
                TextHints.textOnTime = 3.0f; //Time voice audio (F)

                index++;

            }
            else if (index == 5)
            {

                audio.PlayOneShot(Dialogue0Index5);

                TextHints.message = "Oh okay, I'll go grab some.";
                TextHints.textOn = true;
                TextHints.textOnTime = 3.0f; //Time voice audio (M)

                index++;

            }
            else if (index == 6)
            {

                //audio here

                TextHints.message = "Take the bucket in the corner over there. And try not to fall in this time...";
                TextHints.textOn = true;
                TextHints.textOnTime = 3.0f; //Time voice audio (F)

                index++;

            }
            else if (index == 7)
            {

                audio.PlayOneShot(Dialogue0Index7);

                TextHints.message = "Haha, you're funny, I'll be back in a couple.";
                TextHints.textOn = true;
                TextHints.textOnTime = 3.0f; //Time voice audio (M)

                //talkToGirl++;

            }

            index = 0;

        }
        else if (talkToGirl == 1 && hasWater == true)
        {

            if (index == 0)
            {

                //audio here

                TextHints.message = "Thanks for the water (cough) (cough)";
                TextHints.textOn = true;
                TextHints.textOnTime = 3.0f; //Time voice audio (F)

                index++;

            }
            else if (index == 1)
            {

                audio.PlayOneShot(Dialogue1Index1);

                TextHints.message = "No problem...";
                TextHints.textOn = true;
                TextHints.textOnTime = 3.0f; //Time voice audio (M)

                index++;

            }
            else if (index == 2)
            {

                //audio here

                TextHints.message = "You take some, too, it's hot out there isn't it?";
                TextHints.textOn = true;
                TextHints.textOnTime = 3.0f; //Time voice audio (F)

                index++;

            }
            else if (index == 3)
            {

                audio.PlayOneShot(Dialogue1Index3);

                TextHints.message = "I'm fine, like I said I'm not thirsty";
                TextHints.textOn = true;
                TextHints.textOnTime = 3.0f; //Time voice audio (M)

                index++;

            }
            else if (index == 4)
            {

                //audio here

                TextHints.message = "Whatever...";
                TextHints.textOn = true;
                TextHints.textOnTime = 3.0f; //Time voice audio (F)

                index++;

            }
            else if (index == 5)
            {

                //audio here

                TextHints.message = "Anyway before the sun goes down I want you to try and get me something else.";
                TextHints.textOn = true;
                TextHints.textOnTime = 3.0f; //Time voice audio (F)

                index++;

            }
            else if (index == 6)
            {

                audio.PlayOneShot(Dialogue1Index6);

                TextHints.message = "What is it now?";
                TextHints.textOn = true;
                TextHints.textOnTime = 3.0f; //Time voice audio (M)

                index++;

            }
            else if (index == 7)
            {

                //audio here

                TextHints.message = "You ever been to the top of Mount Mons?";
                TextHints.textOn = true;
                TextHints.textOnTime = 3.0f; //Time voice audio (F)

                index++;

            }
            else if (index == 8)
            {

                audio.PlayOneShot(Dialogue1Index8);

                TextHints.message = "No? I've never tried to climb it... but why?";
                TextHints.textOn = true;
                TextHints.textOnTime = 3.0f; //Time voice audio (M)

                index++;

            }
            else if (index == 9)
            {

                //audio here

                TextHints.message = "I read in a book that at the top of the mountain on this island there lies a (cough) mystical flower unlike anything we've seen before.";
                TextHints.textOn = true;
                TextHints.textOnTime = 3.0f; //Time voice audio (F)

                index++;

            }
            else if (index == 10)
            {

                audio.PlayOneShot(Dialogue1Index10);

                TextHints.message = "Where is the book? What does the flower look like?";
                TextHints.textOn = true;
                TextHints.textOnTime = 3.0f; //Time voice audio (M)

                index++;

            }
            else if (index == 11)
            {

                //audio here

                TextHints.message = "The book is in the cabinet...";
                TextHints.textOn = true;
                TextHints.textOnTime = 3.0f; //Time voice audio (F)

                index++;

            }
            else if (index == 12)
            {

                audio.PlayOneShot(Dialogue1Index12);

                TextHints.message = "Of course, and the doors are stuck so I can't open it anymore.";
                TextHints.textOn = true;
                TextHints.textOnTime = 3.0f; //Time voice audio (M)

                index++;

            }
            else if (index == 13)
            {

                //audio here

                TextHints.message = "Sorry about that, but it seems to be pretty distinct, unlike any flower I've seen on the (cough) (cough) island before...";
                TextHints.textOn = true;
                TextHints.textOnTime = 3.0f; //Time voice audio (F)

                index++;

            }
            else if (index == 14)
            {

                audio.PlayOneShot(Dialogue1Index14);

                TextHints.message = "So you want me to grab it for you because... Wait will it help cure you?";
                TextHints.textOn = true;
                TextHints.textOnTime = 3.0f; //Time voice audio (M)

                index++;

            }
            else if (index == 15)
            {

                //audio here

                TextHints.message = "Uh...";
                TextHints.textOn = true;
                TextHints.textOnTime = 3.0f; //Time voice audio (F)

                index++;

            }
            else if (index == 16)
            {

                audio.PlayOneShot(Dialogue1Index16);

                TextHints.message = "I'll get it right away!";
                TextHints.textOn = true;
                TextHints.textOnTime = 3.0f; //Time voice audio (M)

                index++;

            }
            else if (index == 17)
            {

                //audio here

                TextHints.message = "Wait you didn't let me...";
                TextHints.textOn = true;
                TextHints.textOnTime = 3.0f; //Time voice audio (F)

                //talkToGirl++;

            }

            index = 0;

        }
        else if (talkToGirl == 2 && BatteryCollect.flower == true)
        {

            if (index == 0)
            {

                audio.PlayOneShot(Dialogue2Index0);

                TextHints.message = "I got the flower for you! What do I need to do to make it into a cure?";
                TextHints.textOn = true;
                TextHints.textOnTime = 3.0f; //Time voice audio (M)

                index++;

            }
            else if (index == 1)
            {

                //audio here

                TextHints.message = "You didn't let me finish...";
                TextHints.textOn = true;
                TextHints.textOnTime = 3.0f; //Time voice audio (F)

                index++;

            }
            else if (index == 2)
            {

                audio.PlayOneShot(Dialogue2Index2);

                TextHints.message = "Oh, carry on...";
                TextHints.textOn = true;
                TextHints.textOnTime = 3.0f; //Time voice audio (M)

                index++;

            }
            else if (index == 3)
            {

                //audio here

                TextHints.message = "I just wanted to look at it, seen as though I haven't seen anything other than a palm tree out here in forever.";
                TextHints.textOn = true;
                TextHints.textOnTime = 3.0f; //Time voice audio (F)

                index++;

            }
            else if (index == 4)
            {

                audio.PlayOneShot(Dialogue2Index4);

                TextHints.message = "So you're not gonna be healed then...?";
                TextHints.textOn = true;
                TextHints.textOnTime = 3.0f; //Time voice audio (M)

                index++;

            }
            else if (index == 5)
            {

                //audio here

                TextHints.message = "Nope, who knows (cough (cough) if I'll get better.";
                TextHints.textOn = true;
                TextHints.textOnTime = 3.0f; //Time voice audio (F)

                //talkToGirl++;

            }

            index = 0;

        }

    }

    void BeginningMonologue1()
    {

        audio.PlayOneShot(Monologue1);

        TextHints.message = "Okay... She told me to get fruits, right?";
        TextHints.textOn = true;
        TextHints.textOnTime = 4.0f; //Time voice audio (M)

        Invoke("BeginningMonologue2", 4.0f);

        /*if (index == 0)
        {

            audio.PlayOneShot(Monologue1);

            TextHints.message = "Okay... She told me to get fruits, right?";
            TextHints.textOn = true;
            TextHints.textOnTime = 4.0f; //Time voice audio (M)
            
            //if (generalTimer > 3.0f)
            //{

                //generalTimer += Time.deltaTime;

            //}

            index++;

        }
        
        else if (index == 1)
        {

            audio.PlayOneShot(Monologue2);

            TextHints.message = "I'm sure they'll be in convenient places on the ground. I'll just have to look for them.";
            TextHints.textOn = true;
            TextHints.textOnTime = 5.0f; //Time voice audio (M)

        }*/

    }

    void BeginningMonologue2()
    {

        audio.PlayOneShot(Monologue2);

        TextHints.message = "I'm sure they'll be in convenient places on the ground. I'll just have to look for them.";
        TextHints.textOn = true;
        TextHints.textOnTime = 5.0f; //Time voice audio (M)

    }

}
