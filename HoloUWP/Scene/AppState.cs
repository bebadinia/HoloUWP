using StereoKit;

namespace Scene
{
    public static class AppState
    {
        //Interfaces
        public static Pose StartingWinPose = new Pose(0, 0, -0.5f, Quat.LookDir(-Vec3.Forward));
        public static Pose ModelWinPose = new Pose(-0.25f, 0, -0.5f, Quat.LookDir(-Vec3.Forward));

        //Models
        public static Pose SystemPose = new Pose(0, -1.5f, -1f, Quat.LookDir(-Vec3.Forward));

        //Main Scale for Models
        public static float SystemScale = 1.0f;

        // Animation state
        public static int SelectedAnim = 0;
        public static bool LoopAnim = false;

        //Reset to defaults
        public static void Reset()
        {
            StartingWinPose = new Pose(0, 0, -0.5f, Quat.LookDir(-Vec3.Forward));
            ModelWinPose = new Pose(-0.25f, 0, -0.5f, Quat.LookDir(-Vec3.Forward));
            SystemPose = new Pose(0, -1.5f, -1f, Quat.LookDir(-Vec3.Forward));
            SystemScale = 1.0f;
            SelectedAnim = 0;
            LoopAnim = false;
        }

    }
}