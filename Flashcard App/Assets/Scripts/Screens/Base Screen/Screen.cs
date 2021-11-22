using UnityEngine;

namespace BlitzyUI
{
    [RequireComponent(typeof(Canvas))]
    public abstract partial class Screen : MonoBehaviour
    {
        public ScreenID id { get; private set; }
        public string PrefabName { get; private set; }

        [Header("Screen Info")]
        public bool keepCached = false;
        public bool overrideManagedSorting;
        public int overrideSortValue;


        public delegate void ScreenDelegate (Screen screen);
        public event ScreenDelegate PushFinishedEvent;
        public event ScreenDelegate PopFinishedEvent;

        //I added this for when pressing back.
        private bool allowPopScreenOnPressingBack = true;
        public virtual bool AllowPopScreenOnPressingBack { get { return allowPopScreenOnPressingBack; } set { allowPopScreenOnPressingBack = value; } } 

        public void Setup(ScreenID id, string prefabName)
        {
            this.id = id;
            PrefabName = prefabName;

            OnSetup();
        }

        /// <summary>
        /// Setup is called after instantiating a Screen prefab. It is only called once for the lifecycle of the Screen.
        /// </summary>
        public abstract void OnSetup();

        /// <summary>
        /// Called by the UIManager when this Screen is being pushed to the screen stack.
        /// Be sure to call PushPopFinished when your screen is done pushing. Delaying the PushPopFinished call
        /// allows the screen to delay execution of the UIManager's screen queue.
        /// </summary>
        public abstract void OnPush(ScreenData data);

        /// <summary>
        /// Called by the UIManager when this Screen is being popped from the screen stack.
        /// Be sure to call PopFinished when your screen is done popping. Delaying the PushPopFinished call
        /// allows the screen to delay execution of the UIManager's screen queue.
        /// </summary>
        public abstract void OnPop();

        /// <summary>
        /// Called by the UIManager when this Screen becomes the top most screen in the stack.
        /// </summary>
        public abstract void OnFocus();

        /// <summary>
        /// Called by the UIManager when this Screen is no longer the top most screen in the stack.
        /// </summary>
        public abstract void OnFocusLost();

        //Added this myself which screen controller calls when back button is pressed. 
        //Default thing to happen is that the ui manager just pops the screen. 
        //Can override this in any screen to do something before you pop the screen (in navicon screen - move navicon off screen using leantween then queuepop once that's done.
        public virtual void OnBackButtonPressed() { UIManager.Instance.QueuePop(null); } 

        protected void PushFinished ()
        {
            if (PushFinishedEvent != null)
                PushFinishedEvent(this);
        }

        protected void PopFinished ()
        {
            if (PopFinishedEvent != null)
                PopFinishedEvent(this);
        }
    }
}