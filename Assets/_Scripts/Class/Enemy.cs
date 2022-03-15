using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Empty enemy if we want to just make them automatically
/// through script instead of having to put stuff manually in prefabs
/// </summary>
public class Enemy<T> where T : Enemy
{
    public GameObject GameObject;
    public T ScriptComponent;

    public Enemy(string name)
    {
        GameObject = new GameObject(name);
        ScriptComponent = GameObject.AddComponent<T>();
    }
}

/// <summary>
/// Base script for the enemies
/// </summary>
public abstract class Enemy : MonoBehaviour
{
    public GameObject[] spawnPoints;

    protected void Spawner()
    {

    }
}
