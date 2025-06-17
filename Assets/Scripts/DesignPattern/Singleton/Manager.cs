using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager
{
    public static FightManager FightInstance => FightManager.FightInstance;
    public static GameManager Instance => GameManager.Instance;
}
