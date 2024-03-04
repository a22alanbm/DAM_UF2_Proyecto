using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCharacter : Character
{
    protected override void TimeInitializer(){
        TimeAtk1=0.8f;
        TimeAtk2=1.1f;
        TimeAtk3=1.0f;
        TimeSpecialAtk=2.8f;
    }
}
