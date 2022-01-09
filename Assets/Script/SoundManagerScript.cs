using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip AngelMeleeAttackSound, AttackUndeadSound, DropCharacterSound, WalkUndeadSound, SelectSound, WinSound;
    static AudioSource audiosrc;
    // Start is called before the first frame update
    void Start()
    {
        AngelMeleeAttackSound = Resources.Load<AudioClip> ("AngelMeleeAttack");
        AttackUndeadSound = Resources.Load<AudioClip> ("AttackUndead");
        DropCharacterSound = Resources.Load<AudioClip> ("DropCharacter");
        WalkUndeadSound = Resources.Load<AudioClip> ("WalkUndead");
        SelectSound = Resources.Load<AudioClip> ("Select");
        WinSound = Resources.Load<AudioClip> ("Win");

        audiosrc = GetComponent<AudioSource> ();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound (string clip)
    {
        switch (clip)
        {
            case "AngelMeleeAttack":
            audiosrc.PlayOneShot (AngelMeleeAttackSound);
            break;
            case "AttackUndead":
            audiosrc.PlayOneShot (AttackUndeadSound);
            break;
            case "DropCharacter":
            audiosrc.PlayOneShot (DropCharacterSound);
            break;
            case "WalkUndead":
            audiosrc.PlayOneShot (WalkUndeadSound);
            break;
            case "Select":
            audiosrc.PlayOneShot (SelectSound);
            break;
        }
    }
}
