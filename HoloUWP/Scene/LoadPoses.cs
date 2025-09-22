
using StereoKit;
using System;

namespace Scene
{
    public static class LoadPoses
    {
        public static ScenePoses CreateDefaults()
        {
            Console.WriteLine("Creating Poses");

            //Interfaces
            var startingWinPose = new Pose(0, 0, -0.5f, Quat.LookDir(-Vec3.Forward));
            var modelWinPose = new Pose(-0.25f, 0, -0.5f, Quat.LookDir(-Vec3.Forward));

            //Models
            // Respiratory System
            var respiratoryPose = new Pose(0, -1.5f, -1f, Quat.LookDir(-Vec3.Forward));
            float respiratorySystemScale = 1.0f;

            // Heart
            var heartPose = new Pose(0, 0, -1f, Quat.LookDir(-Vec3.Forward));
            float heartScale = 0.25f; // start smaller

            // Fox
            var foxPose = new Pose(0, 0, -1f, Quat.LookDir(-Vec3.Forward));
            float foxScale = 0.005f;

            return new ScenePoses(startingWinPose, modelWinPose, respiratoryPose, respiratorySystemScale, heartPose, heartScale, foxPose, foxScale);
        }
    }

    public record ScenePoses(Pose startingWinPose, Pose modelWinPose, Pose respiratoryPose, float respiratorySystemScale, Pose heartPose, float heartScale, Pose foxPose, float foxScale);
}
