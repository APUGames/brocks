using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    //Objective SFX
    [SerializeField] private AudioClip woodenBucket;
    [SerializeField] private AudioClip waterSloosh;
    [SerializeField] private AudioClip flowerPick;

    //Voice Acting
    //I definitely did this the long way, especially since each line has its own method called by the Invoke() function... but it works soooooo...
    [SerializeField] private AudioClip Monologue1;
    [SerializeField] private AudioClip Monologue2;
    [SerializeField] private AudioClip Dialogue0Index0;
    [SerializeField] private AudioClip Dialogue0Index1;
    [SerializeField] private AudioClip Dialogue0Index2;
    [SerializeField] private AudioClip Dialogue0Index3;
    [SerializeField] private AudioClip Dialogue0Index4;
    [SerializeField] private AudioClip Dialogue0Index5;
    [SerializeField] private AudioClip Dialogue0Index6;
    [SerializeField] private AudioClip Dialogue0Index7;
    [SerializeField] private AudioClip Dialogue1Index0;
    [SerializeField] private AudioClip Dialogue1Index1;
    [SerializeField] private AudioClip Dialogue1Index2;
    [SerializeField] private AudioClip Dialogue1Index3;
    [SerializeField] private AudioClip Dialogue1Index4;
    [SerializeField] private AudioClip Dialogue1Index5;
    [SerializeField] private AudioClip Dialogue1Index6;
    [SerializeField] private AudioClip Dialogue1Index7;
    [SerializeField] private AudioClip Dialogue1Index8;
    [SerializeField] private AudioClip Dialogue1Index9;
    [SerializeField] private AudioClip Dialogue1Index10;
    [SerializeField] private AudioClip Dialogue1Index11;
    [SerializeField] private AudioClip Dialogue1Index12;
    [SerializeField] private AudioClip Dialogue1Index13;
    [SerializeField] private AudioClip Dialogue1Index14;
    [SerializeField] private AudioClip Dialogue1Index15;
    [SerializeField] private AudioClip Dialogue1Index16;
    [SerializeField] private AudioClip Dialogue1Index17;
    [SerializeField] private AudioClip Dialogue2Index0;
    [SerializeField] private AudioClip Dialogue2Index1;
    [SerializeField] private AudioClip Dialogue2Index2;
    [SerializeField] private AudioClip Dialogue2Index3;
    [SerializeField] private AudioClip Dialogue2Index4;
    [SerializeField] private AudioClip Dialogue2Index5;
    [SerializeField] private AudioClip TooFewFruits;
    [SerializeField] private AudioClip UnlockMonologue1;
    [SerializeField] private AudioClip UnlockMonologue2;
    [SerializeField] private AudioClip WaterMonologue;
    [SerializeField] private AudioClip FlowerMonologue;

    //to hopefully make sure the dialogue doesn't stack
    bool dialogueDone = true;

    //Fruit Collect Sound
    [SerializeField] private AudioClip fruitCollectSound;

    private new AudioSource audio;

    //To keep sickGirl dialogue in correct succession
    public static int talkToGirl = 0;

    //Water boolean
    private bool hasWater = false;

    // Start is called before the first frame update
    void Start()
    {

        //Debug.Log(FruitCollect.pearUI.enabled);

        doorAnimator = door.GetComponent<Animator>();

        audio = GetComponent<AudioSource>();

        //Call for beginning text, might as well make it a method :/
        Invoke("BeginningMonologue1", 6.0f);

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

        if (FruitCollect.fruits == 4)
        {

            //increasing fruits here so the monologue doesn't loop, coded so it doesn't affect UI or door unlocking (I tested it don't worry future me)
            FruitCollect.fruits++;

            Invoke("UnlockMono1", 0.0f);

        }

    }

    void UnlockMono1()
    {

        audio.PlayOneShot(UnlockMonologue1);

        TextHints.message = "I have all the fruits I need now.\n I'll go back and give them to her by hand.";
        TextHints.textOff = false;
        TextHints.textOn = true;
        //TextHints.textOnTime = 5.0f;

        Invoke("UnlockMono2", 5.0f);

    }

    void UnlockMono2()
    {

        audio.PlayOneShot(UnlockMonologue2);
        
        TextHints.message = "Since she's bedridden,\n I'll have to give it to her by touching the bed.";
        TextHints.textOn = true;

        Invoke("EndText", 5.0f);

    }

    //Collision Detection
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {

        if (hit.gameObject.tag == "shackDoor" && FruitCollect.fruits >= 4 || hit.gameObject.tag == "shackDoor" && talkToGirl >= 1)
        {

            OpenDoor();

        }
        else if (hit.gameObject.tag == "shackDoor" && FruitCollect.fruits < 4 && dialogueDone == true)
        {

            //FruitCollect.fruitUI.enabled = true;

            dialogueDone = false;

            audio.PlayOneShot(TooFewFruits);

            TextHints.message = "Can't go back to her yet, I stil have some fruit to find.\n She told me they'd be in craters on the ground, I think.";
            TextHints.textOff = false;
            TextHints.textOn = true;

            Invoke("EndText", 6.0f);

        }
        else if (hit.gameObject.tag == "sickGirl")
        {

            GirlDialogue();

        }
        if (hit.gameObject.tag == "Respawn")
        {

            audio.PlayOneShot(waterSloosh);

            SceneManager.LoadScene("Errand");

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

        if (other.gameObject.tag == "appleObjective")
        {

            //FruitCollect.fruits++;

            FruitCollect.hasApple = true;

            audio.PlayOneShot(fruitCollectSound);

            Destroy(other.gameObject);

        }
        else if (other.gameObject.tag == "pearObjective")
        {

            //FruitCollect.fruits++;

            FruitCollect.hasPear = true;

            audio.PlayOneShot(fruitCollectSound);

            Destroy(other.gameObject);

        }
        else if (other.gameObject.tag == "melonObjective")
        {

            //FruitCollect.fruits++;

            FruitCollect.hasMelon = true;

            audio.PlayOneShot(fruitCollectSound);

            Destroy(other.gameObject);

        }
        else if (other.gameObject.tag == "peachObjective")
        {

            //FruitCollect.fruits++;

            FruitCollect.hasPeach = true;

            audio.PlayOneShot(fruitCollectSound);

            Destroy(other.gameObject);

        }
        else if (other.gameObject.tag == "bucketObjective" && talkToGirl == 1)
        {

            audio.PlayOneShot(woodenBucket);

            FruitCollect.bucket = true;

            Destroy(other.gameObject);

        }
        else if (other.gameObject.tag == "flowerObjective" && talkToGirl == 2)
        {

            FruitCollect.flower = true;

            audio.PlayOneShot(flowerPick);

            Destroy(other.gameObject);

            audio.PlayOneShot(FlowerMonologue);

            TextHints.message = "Is this what she means? It's the most unique flower up here so I assume so. I need to get back to her ASAP so I can start making medicine out of it!";
            TextHints.textOff = false;
            TextHints.textOn = true;
            //TextHints.textOnTime = 3.0f;
            Invoke("EndText", 8.0f);

        }
        else if (other.gameObject.tag == "pondWater" && FruitCollect.bucket == true && hasWater == false)
        {

            hasWater = true;

            audio.PlayOneShot(waterSloosh);

            audio.PlayOneShot(WaterMonologue);

            TextHints.message = "Water: acquired, I don't want to make her wait too long so I should get back.";
            TextHints.textOff = false;
            TextHints.textOn = true;
            
            Invoke("EndText", 3.0f);

        }
        

    }

    //Welp here we go time to die...
    void GirlDialogue()
    {

        if (talkToGirl == 0 && dialogueDone == true)
        {

            dialogueDone = false;

            Invoke("TTG0I0", 0.0f);

        }
        else if (talkToGirl == 1 && hasWater == true && dialogueDone == true)
        {

            dialogueDone = false;

            Invoke("TTG1I0", 0.0f);

        }
        else if (talkToGirl == 2 && FruitCollect.flower == true && dialogueDone == true)
        {

            dialogueDone = false;

            Invoke("TTG2I0", 0.0f);

        }

    }

    //I'm sorry to whoever has been cursed to read this...
    //Also for whatever reason the subtitles work some tests and don't work on others... It's weird. never mind it fixed
    //Nevermind the subtitles don't work at all and I have no idea why... kinda wanna abandon them but that with the beginning monologue is inconsistent
    void TTG0I0()
    {

        audio.PlayOneShot(Dialogue0Index0);

        talkToGirl++;

        TextHints.message = "Oh hey, thanks for the (cough cough) fruits.";
        TextHints.textOff = false;
        TextHints.textOn = true;
        //TextHints.textOnTime = 4.0f; //Time voice audio (F)

        Invoke("TTG0I1", 4.0f);

    }

    void TTG0I1()
    {

        audio.PlayOneShot(Dialogue0Index1);

        TextHints.message = "Mhm no problem.";
        TextHints.textOff = false;
        TextHints.textOn = true;
        //TextHints.textOnTime = 2.0f; //Time voice audio (M)

        Invoke("TTG0I2", 2.0f);

    }

    void TTG0I2()
    {

        audio.PlayOneShot(Dialogue0Index2);

        TextHints.message = "Before you sit down,\n could you go to the pond and get some water?";
        TextHints.textOff = false;
        TextHints.textOn = true;
        //TextHints.textOnTime = 4.0f; //Time voice audio (F)

        Invoke("TTG0I3", 4.0f);

    }

    void TTG0I3()
    {

        audio.PlayOneShot(Dialogue0Index3);

        TextHints.message = "Why? I'm not thirsty";
        TextHints.textOff = false;
        TextHints.textOn = true;
        //TextHints.textOnTime = 3.0f; //Time voice audio (M)

        Invoke("TTG0I4", 3.0f);

    }

    void TTG0I4()
    {

        audio.PlayOneShot(Dialogue0Index4);

        TextHints.message = "Not for you, for me. My throat's sore from (cough) coughing.";
        TextHints.textOff = false;
        TextHints.textOn = true;
        //TextHints.textOnTime = 5.5f; //Time voice audio (F)

        Invoke("TTG0I5", 5.5f);

    }

    void TTG0I5()
    {

        audio.PlayOneShot(Dialogue0Index5);

        TextHints.message = "Oh okay, I'll go grab some.";
        TextHints.textOff = false;
        TextHints.textOn = true;
        //TextHints.textOnTime = 3.0f; //Time voice audio (M)

        Invoke("TTG0I6", 3.0f);

    }

    void TTG0I6()
    {

        audio.PlayOneShot(Dialogue0Index6);

        TextHints.message = "Take the bucket in the corner over there.\n And try not to fall in this time...";
        TextHints.textOff = false;
        TextHints.textOn = true;
        //TextHints.textOnTime = 4.5f; //Time voice audio (F)

        Invoke("TTG0I7", 4.5f);

    }

    void TTG0I7()
    {

        audio.PlayOneShot(Dialogue0Index7);

        TextHints.message = "Haha, you're funny, I'll be back in a couple.";
        TextHints.textOff = false;
        TextHints.textOn = true;
        
        Invoke("EndText", 3.0f);

        dialogueDone = true;

    }

    void TTG1I0()
    {

        audio.PlayOneShot(Dialogue1Index0);

        TextHints.message = "Thanks for the water (cough) (cough)";
        TextHints.textOff = false;
        TextHints.textOn = true;
        //TextHints.textOnTime = 2.0f; //Time voice audio (F)

        Invoke("TTG1I1", 2.0f);

    }

    void TTG1I1()
    {

        audio.PlayOneShot(Dialogue1Index1);

        TextHints.message = "No problem...";
        TextHints.textOff = false;
        TextHints.textOn = true;
        //TextHints.textOnTime = 3.0f; //Time voice audio (M)

        Invoke("TTG1I2", 1.5f);

    }

    void TTG1I2()
    {

        audio.PlayOneShot(Dialogue1Index2);

        TextHints.message = "You take some, too, it's hot out there isn't it?";
        TextHints.textOff = false;
        TextHints.textOn = true;
        //TextHints.textOnTime = 4.0f; //Time voice audio (F)

        Invoke("TTG1I3", 4.0f);

    }

    void TTG1I3()
    {

        audio.PlayOneShot(Dialogue1Index3);

        TextHints.message = "I'm fine, like I said I'm not thirsty";
        TextHints.textOff = false;
        TextHints.textOn = true;
        //TextHints.textOnTime = 2.5f; //Time voice audio (M)

        Invoke("TTG1I4", 2.5f);

    }

    void TTG1I4()
    {

        audio.PlayOneShot(Dialogue1Index4);

        TextHints.message = "Whatever...";
        TextHints.textOff = false;
        TextHints.textOn = true;
        //TextHints.textOnTime = 2.0f; //Time voice audio (F)

        Invoke("TTG1I5", 2.0f);

    }

    void TTG1I5()
    {

        audio.PlayOneShot(Dialogue1Index5);

        TextHints.message = "Anyway before the sun goes down \nI want you to try and get me something else.";
        TextHints.textOff = false;
        TextHints.textOn = true;
        //TextHints.textOnTime = 5.0f; //Time voice audio (F)

        Invoke("TTG1I6", 5.0f);

    }

    void TTG1I6()
    {

        audio.PlayOneShot(Dialogue1Index6);

        TextHints.message = "What is it now?";
        TextHints.textOff = false;
        TextHints.textOn = true;
        //TextHints.textOnTime = 3.0f; //Time voice audio (M)

        Invoke("TTG1I7", 3.0f);

    }

    void TTG1I7()
    {

        audio.PlayOneShot(Dialogue1Index7);

        TextHints.message = "Have you ever been to the top of Mount Mons?";
        TextHints.textOff = false;
        TextHints.textOn = true;
        //TextHints.textOnTime = 3.5f; //Time voice audio (F)

        Invoke("TTG1I8", 3.5f);

    }

    void TTG1I8()
    {

        audio.PlayOneShot(Dialogue1Index8);

        TextHints.message = "No? I've never tried to climb it... but why?";
        TextHints.textOff = false;
        TextHints.textOn = true;
        //TextHints.textOnTime = 4.0f; //Time voice audio (M)

        Invoke("TTG1I9", 4.0f);

    }

    void TTG1I9()
    {

        audio.PlayOneShot(Dialogue1Index9);

        TextHints.message = "I read in a book before that at the top of the mountain on this\n island, there lies a (cough) mystical flower unlike anything we've seen before.";
        TextHints.textOff = false;
        TextHints.textOn = true;
        //TextHints.textOnTime = 8.5f; //Time voice audio (F)

        Invoke("TTG1I10", 9.5f);

    }

    void TTG1I10()
    {

        audio.PlayOneShot(Dialogue1Index10);

        TextHints.message = "Where is the book? What does the flower look like?";
        TextHints.textOff = false;
        TextHints.textOn = true;
        //TextHints.textOnTime = 4.0f; //Time voice audio (M)

        Invoke("TTG1I11", 4.0f);

    }

    void TTG1I11()
    {

        audio.PlayOneShot(Dialogue1Index11);

        TextHints.message = "The book is in the cabinet...";
        TextHints.textOff = false;
        TextHints.textOn = true;
        //TextHints.textOnTime = 2.0f; //Time voice audio (F)

        Invoke("TTG1I12", 2.0f);

    }

    void TTG1I12()
    {

        audio.PlayOneShot(Dialogue1Index12);

        TextHints.message = "Of course, and the doors are stuck so I can't open it anymore.";
        TextHints.textOff = false;
        TextHints.textOn = true;
        //TextHints.textOnTime = 3.5f; //Time voice audio (M)

        Invoke("TTG1I13", 3.5f);

    }

    void TTG1I13()
    {

        audio.PlayOneShot(Dialogue1Index13);

        TextHints.message = "Sorry about that, but it seems to be pretty distinct,\n unlike any flower I've seen on the (cough) (cough) island before...";
        TextHints.textOff = false;
        TextHints.textOn = true;
        //TextHints.textOnTime = 6.0f; //Time voice audio (F)

        Invoke("TTG1I14", 7.0f);

    }

    void TTG1I14()
    {

        audio.PlayOneShot(Dialogue1Index14);

        TextHints.message = "So you want me to grab it for you because...\n Wait will it help cure you?";
        TextHints.textOff = false;
        TextHints.textOn = true;
        //TextHints.textOnTime = 6.0f; //Time voice audio (M)

        Invoke("TTG1I15", 6.0f);

    }

    void TTG1I15()
    {

        audio.PlayOneShot(Dialogue1Index15);

        TextHints.message = "Uh...";
        TextHints.textOff = false;
        TextHints.textOn = true;
        //TextHints.textOnTime = 1.5f; //Time voice audio (F)

        Invoke("TTG1I16", 1.5f);

    }

    void TTG1I16()
    {

        audio.PlayOneShot(Dialogue1Index16);

        TextHints.message = "I'll get it right away!";
        TextHints.textOff = false;
        TextHints.textOn = true;
        //TextHints.textOnTime = 3.0f; //Time voice audio (M)

        Invoke("TTG1I17", 3.0f);

    }

    void TTG1I17()
    {

        audio.PlayOneShot(Dialogue1Index17);

        TextHints.message = "Wait you didn't let me...";
        TextHints.textOff = false;
        TextHints.textOn = true;
        
        Invoke("EndText", 3.0f);

        talkToGirl++;
        dialogueDone = true;

    }

    void TTG2I0()
    {

        audio.PlayOneShot(Dialogue2Index0);

        TextHints.message = "I got the flower for you!\n What do I need to do to make it into a cure?";
        TextHints.textOff = false;
        TextHints.textOn = true;
        TextHints.textOnTime = 4.0f; //Time voice audio (M)

        Invoke("TTG2I1", 4.0f);

    }

    void TTG2I1()
    {

        audio.PlayOneShot(Dialogue2Index1);

        TextHints.message = "You didn't let me finish...";
        TextHints.textOn = true;
        TextHints.textOnTime = 3.0f; //Time voice audio (F)

        Invoke("TTG2I2", 3.0f);

    }

    void TTG2I2()
    {

        audio.PlayOneShot(Dialogue2Index2);

        TextHints.message = "Oh, carry on...";
        TextHints.textOn = true;
        TextHints.textOnTime = 3.0f; //Time voice audio (M)

        Invoke("TTG2I3", 3.0f);

    }

    void TTG2I3()
    {

        audio.PlayOneShot(Dialogue2Index3);

        TextHints.message = "I just wanted to look at it, seen as though I haven't seen \n anything other than a palm tree out here in forever.";
        TextHints.textOn = true;
        TextHints.textOnTime = 5.0f; //Time voice audio (F)

        Invoke("TTG2I4", 5.0f);

    }

    void TTG2I4()
    {

        audio.PlayOneShot(Dialogue2Index4);

        TextHints.message = "So you're not gonna be healed then...?";
        TextHints.textOn = true;
        TextHints.textOnTime = 3.0f; //Time voice audio (M)

        Invoke("TTG2I5", 3.0f);

    }

    void TTG2I5()
    {

        audio.PlayOneShot(Dialogue2Index5);

        TextHints.message = "Nope, who knows (cough) (cough) if I'll get better.";
        TextHints.textOn = true;

        talkToGirl++;
        dialogueDone = true;

        Invoke("FinalText", 5.0f);

    }
    //It's over... well kinda...

    void BeginningMonologue1()
    {

        audio.PlayOneShot(Monologue1);

        TextHints.message = "Okay... She told me to get fruits, right?";
        TextHints.textOn = true; //So for some reason these subtitles show themselves but not literally all the other ones...
        //What makes these lines so special??? nvm found it
        TextHints.textOff = false;

        Invoke("BeginningMonologue2", 4.0f);

    }

    void BeginningMonologue2()
    {
        
        audio.PlayOneShot(Monologue2);

        TextHints.message = "I'm sure they'll be in convenient places on the ground.\n I'll just have to look for them.";
        TextHints.textOn = true;
        
        Invoke("EndText", 5.0f);

    }

    void EndText()
    {

        TextHints.textOn = false;

        TextHints.textOff = true;

        dialogueDone = true;

    }

    void FinalText()
    {

        SceneManager.LoadScene("EndScene");

    }

}