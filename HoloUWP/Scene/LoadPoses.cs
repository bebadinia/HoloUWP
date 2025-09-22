
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
            // Systems
            var SystemPose = new Pose(0, -1.5f, -1f, Quat.LookDir(-Vec3.Forward));
            float SystemScale = 1.0f;

            return new ScenePoses(startingWinPose, modelWinPose, SystemPose, SystemScale);
        }
    }

    public record ScenePoses(Pose startingWinPose, Pose modelWinPose, Pose SystemPose, float SystemScale);
}
