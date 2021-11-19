//#define PRINT_STACK
//#define PRINT_QUEUE
//#define PRINT_CACHE
//#define PRINT_FOCUS


/*Notes for myself:
 * CACHED: 
 * Meaning: screen is disabled in hierarchy to be reenabled later. 
 * _cache dictionary: keeps track of cached screens. Dictionary that holds prefab name and screen that is cached.
 * Can make a screen cache by setting keepCached variable in Screen class.
 * 
 * 
 * 
 */


//Script to handle switching between screens. Screen ID's are held in ScreenController.

namespace BlitzyUI
{
    public partial class UIManager
    {
        private class QueuedScreenPop : QueuedScreen
        {
            public PoppedDelegate callback;

            public override string ToString()
            {
                return string.Format("[Pop] {0}", id);
            }
        }
    }
}