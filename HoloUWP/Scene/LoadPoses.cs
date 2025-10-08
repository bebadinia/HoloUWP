
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
            var systemPose = new Pose(0, -1.5f, -1f, Quat.LookDir(-Vec3.Forward));
            float systemScale = 1.0f;

            return new ScenePoses
            {
                startingWinPose = startingWinPose,
                modelWinPose = modelWinPose,
                systemPose = systemPose,
                systemScale = systemScale,
            };
        }
    }

    // Mutable container 
    public class ScenePoses
    {
        public Pose startingWinPose { get; set; }
        public Pose modelWinPose { get; set; }
        public Pose systemPose { get; set; }
        public float systemScale { get; set; }
    }
}
