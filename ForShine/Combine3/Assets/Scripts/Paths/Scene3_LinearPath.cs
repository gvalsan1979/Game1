using UnityEngine;

namespace Assets.Scripts.Paths {

    public class Scene3_LinearPath : Scene3_IPath {

        Vector2 Scene3_IPath.GeneratePath (Vector2 current, Vector2 start, Vector2 end, float elapsedTime, float totalTime) {
            var fractionDone = elapsedTime / totalTime;
            return (end - start) * fractionDone + start;
        }
    }
}
