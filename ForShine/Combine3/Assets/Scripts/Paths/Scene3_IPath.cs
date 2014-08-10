using UnityEngine;

namespace Assets.Scripts.Paths {

    //0 <= elapsedTime <= totalTime
    public interface Scene3_IPath {

        /// <summary>
        /// Returns the position the object should be in at elapsed time.
        /// </summary>
        /// <param name="current">current position</param>
        /// <param name="start">starting position</param>
        /// <param name="end">ending position</param>
        /// <param name="elapsedTime">time taken so far, with 0 being start of path</param>
        /// <param name="totalTime">total time it takes to complete path</param>
        /// <returns></returns>
        Vector2 GeneratePath (Vector2 current, Vector2 start, Vector2 end, float elapsedTime, float totalTime);
    }
}