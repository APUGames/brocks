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

    //To keep sickGirl dialogue in correct succession
    private int talkToGirl = 0;

    // Start is called before the first frame update
    void Start()
    {

        doorAnimator = door.GetComponent<Animator>();
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Collision Detection
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "shackDoor")
        {

            //Debug.Log("Hit the door!");

            OpenDoor();

        }

        if (hit.gameObject.tag == "sickGirl")
        {



        }
        
    }

    void OpenDoor()
    {

        //Set animator parameter
        doorAnimator.SetBool("doorOpen", true);
        

    }

    void GirlDialogue()
    {

        if (talkToGirl == 0) //number indicates how many times player has talked to sickGirl thus far
        {

            /*
            "Oh hey, thanks for the (cough cough) fruits."
            "Mhm no problem."
            "Before you sit down could you go to the pond and get some water?"
            "I'm not thirsty"
            "Not for you, for me. My throat's sore from (cough) coughing."
            "Oh okay, I'll go grab some."
            "Take the bucket in the corner over there. And try not to fall in this time..."
            "You're funny, I'll be back in a couple."
             */

            talkToGirl++;

        } 
        else if (talkToGirl == 1)
        {

            /*
            "Thanks for the water (cough) (cough)"
            "No problem..."
            "You take some, too, it's hot out there isn't it?"
            "I'm fine, like I said I'm not thirsty"
            "Whatever..."
            "Anyway before the sun goes down I want you to try and get me something else."
            "What is it now?"
            "You ever been to the top of Mount Mons?"
            "No? I've never tried to climb it... but why?"
            "I read in a book that at the top of the mountain on this island there lies a (cough) mystical flower unlike anything we've seen before."
            "Where is the book, what does it look like?"
            "The book is in the cabinet..."
            "Of course, and the doors are stuck so I can't open it anymore."
            "Sorry about that, but it seems to be pretty distinct, unlike any flower I've seen on the (cough) (cough) island before..."
            "So you want me to grab it for you because... Wait will it help cure you?"
            "Uh..."
            "I'll get it right away!"
            "Wait you didn't let me..."
            */

            talkToGirl++;
            
        }
        else if (talkToGirl == 3)
        {

            /*
            "I got the flower for you! What do I need to do to make it into a cure?"
            "You didn't let me finish..."
            "Oh, carry on..."
            "I just wanted to look at it, seen as though I haven't seen anything other than a palm tree out here in forever."
            "So you're not gonna be healed then...?"
            "Nope, who knows (cough (cough) if I'll get better."
            */

        }
        
        

    }

}
