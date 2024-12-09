using Assets.Scripts.UI.UiControllers;
using TMPro;
using UnityEngine;

namespace UI.Scenes.Gameplay.AmmoPanels
{
    public class AmmoPanelView : UiView
    {
        [field: SerializeField] public TMP_Text CurrentAmmoText { get; private set; }
    }
}
