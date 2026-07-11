using DaggerfallWorkshop.Game;
using UnityEngine;

namespace TerminatorUnity.Game
{
    
    public class FSBootstrap : MonoBehaviour
    {
        public enum StartMode
        {
            MAIN_MENU            
        }

        private StartMode? targetMode = null;

        void Awake()
        {
            
        }

        void Start()
        {
            // TEMP for initial development
            targetMode = StartMode.MAIN_MENU;
        }

        void Update()
        {
            if (targetMode != null)
            {
                InitMode();
                targetMode = null;
            }
        }

        private void InitMode()
        {
            switch(targetMode)
            {
                case StartMode.MAIN_MENU:
                    InitMainMenu();
                    break;
                default:
                    break;
            }
        }

        private void InitMainMenu()
        {
        }

    }

}