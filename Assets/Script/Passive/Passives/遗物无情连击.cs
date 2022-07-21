using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 遗物无情连击 : StackPassive
{
    public int stack;
    public Queue<float> deadtimeline = new();
    public override void OnSucceedDamage(Damage damage, Role target)
    {
        if (deadtimeline.Count != 0)
        while(deadtimeline.Peek() < Time.time)
        {
            deadtimeline.Dequeue();
            stack--;
            if (deadtimeline.Count == 0)
                break;
        }
        damage.FinalDamage += damage.damage * stack * 0.01f;
        stack++;
        deadtimeline.Enqueue(Time.time + 2);
    }

}
