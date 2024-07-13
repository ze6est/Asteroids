using Asteroids.CodeBase.Enemies;
using Asteroids.CodeBase.Factories;
using Asteroids.CodeBase.Spawners;
using UnityEngine;

public class UfoFactory : Factory<Ufo>
{
    public UfoFactory(Ufo prefab, int capacity, int maxSize, Transform container) : base(prefab, capacity, maxSize, container)
    {
    }
}
