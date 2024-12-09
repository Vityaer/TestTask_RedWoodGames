using Gameplay.Loots;
using System;

namespace Gameplay.Services.Loots
{
    public interface ILootService
    {
        IObservable<Loot> OnGetUpLoot { get; }
    }
}