using Asteroids.CodeBase.Enemies;
using Asteroids.CodeBase.Factories;
using Asteroids.CodeBase.Spawners;
using UnityEngine;

public class UFOFactory : Factory<UFO>
{
    public UFOFactory(UFO prefab, int capacity, int maxSize, Transform container) : base(prefab, capacity, maxSize, container)
    {
    }
}
