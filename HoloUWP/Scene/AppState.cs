using System.Collections.Generic;
using StereoKit;

namespace Scene
{
    public static class AppState
    {
        //Interfaces
        public static Pose StartingWinPose = new Pose(0, 0, -0.5f, Quat.LookDir(-Vec3.Forward));
        public static Pose ModelWinPose = new Pose(-0.25f, 0, -0.5f, Quat.LookDir(-Vec3.Forward));
        //Models
        public static Pose SystemPose = new Pose(0, 0, -1f, Quat.LookDir(-Vec3.Forward));

        // Model/UI placement state so we only anchor once
        public static bool Anchored = false;



        //Main Scale for Models
        public static float SystemScale = 1.0f;


        // Adjustments to re-anchor the handle at the chest
        // chestF:     fraction of height for chest (0=feet, 1=head)
        // offsetLocal:fine tweak in MODEL space (meters), e.g. nudge forward/up
        // lineOffset: shift guide line left/right in MODEL +X (meters)
        public static Dictionary<int, (float chestF, Vec3 offsetLocal)> ChestAdj = new Dictionary<int, (float, Vec3)>
        {
            { 1, (0.75f, new Vec3(0.02f, 0.01f, -0.1f)) },       // Circulatory
            { 2, (0.75f, new Vec3(0.01f, 0.01f, -0.1f)) },       // Digestive
            { 3, (0.75f, new Vec3(-0.05f, 0.01f, -0.1f)) },      // Endocrine
            { 4, (0.75f, new Vec3(0.08f, 0.01f, -0.1f)) },       // Lymphatic
            { 5, (0.75f, new Vec3(-0.03f, 0.01f, -0.1f)) },      // Muscular
            { 6, (0.75f, new Vec3(0.03f, 0.01f, -0.1f)) },       // Nervous
            { 7, (0.75f, new Vec3(0.07f, 0.01f, -0.1f)) },       // Respiratory
            { 8, (0.75f, new Vec3(-0.05f, -0.04f, -0.1f)) },     // Skeletal
            { 9, (0.75f, new Vec3(0.03f, 0.01f, -0.1f)) },       // Urinary
        };

        // Animation state
        public static int SelectedAnim = 0;
        public static bool LoopAnim = false;

        // Label state
        public static bool ShowLabels = false;

        //Reset to defaults
        public static void Reset()
        {
            //StartingWinPose = new Pose(0, 0, -0.5f, Quat.LookDir(-Vec3.Forward));
            // Base of system relative to the Start Menu
            // +X = right of the menu, +Y = up, -Z = in front (toward the user), +Z = behind
            Vec3 modelFromMenu = new Vec3(-0.75f, 0.00f, 0.50f); // tweak to taste

            // AnchorSystem: pose at menu + rotated offset, inherit menu facing
            AppState.SystemPose.position = AppState.StartingWinPose.position + (AppState.StartingWinPose.orientation * modelFromMenu);
            AppState.SystemPose.orientation = AppState.StartingWinPose.orientation;

            // Anchor Model UI making it the same as Start window
            AppState.ModelWinPose.position = AppState.StartingWinPose.position;
            AppState.ModelWinPose.orientation = AppState.StartingWinPose.orientation;
            SystemScale = 1.0f;
            SelectedAnim = 0;
            LoopAnim = false;

            ShowLabels = false;

            // important: allow re-anchoring next time
            Anchored = false;

        }

    }
}