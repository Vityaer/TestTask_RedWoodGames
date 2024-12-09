using UnityEngine;

namespace Infrastructures.Common
{
    public static class GameConstants
    {
        public static class Visual
        {
            public static int ANIMATOR_SHOOT_NAME_HASH = Animator.StringToHash("Shoot");
            public static int ANIMATOR_RUN_NAME_HASH = Animator.StringToHash("Speed");
        }

        public static class Fight
        {

            public static int BulletTargetMask = LayerMask.GetMask("Enemy", "Ground");
            public static int PlayerLayer = LayerMask.GetMask("Player");
        }

        public static class Scenes
        {
            public const string GAMEPLAY_SCENE_NAME = "Gameplay";
            public const string MAIN_MENU_SCENE_NAME = "MainMenu";
        }
    }
}
