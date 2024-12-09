using Assets.Scripts.UI.UiControllers;
using UnityEngine;

namespace UI.Scenes.Gameplay.EnemyHealthPanels
{
    public class EnemyHealthPanelView : UiView
    {
        [field: SerializeField] public RectTransform HealthsContainer { get; private set; }
        [field: SerializeField] public EnemyHealthView EnemyHealthViewPrefab { get; private set; }
        [field: SerializeField] public Vector2 Offset { get; private set; }
    }
}
