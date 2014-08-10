using System;
using UnityEngine;

namespace Assets.Scripts.Paths {

    public class Scene3_SPath : Scene3_IPath {



        Vector2 Scene3_IPath.GeneratePath (Vector2 current, Vector2 start, Vector2 end,
            float elapsedTime, float totalTime) {
            var a = (end - start) * (elapsedTime / totalTime);
            var diffLen = Scene3_BeachConstants.AMPLITUDE_SPATH * Math.Sin(a.magnitude);
            var ortho = new Vector2(-a.y, a.x) * (float)(diffLen / a.magnitude);
            var result = start + ortho + a;
            return result;
        }
    }
}
