using StereoKit;
using System;
using Scene;

namespace HoloUWP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Initialize StereoKit
            SKSettings settings = new SKSettings
            {
                appName = "Systems Simulation",
                assetsFolder = "Assets",
                //displayPreference = DisplayMode.MixedReality
            };

            if (!SK.Initialize(settings))
                Environment.Exit(1);


            var models = LoadModels.LoadAll();     // load once
            var poses = LoadPoses.CreateDefaults();            // set default transforms

            bool showUI = true;
            int startButtonPressed = 0;

            int selectedAnim = 0;       // current anim index for the active model
            bool loopAnim = false;    // loop toggle

            // Tiny preview scale for the on-panel model
            float previewScale = 0.10f;


            SK.Run(() =>
            {
                
                if (showUI)
                {
                    var startPose = poses.startingWinPose;
                    int choice = LoadInterfaces.DrawStartInterface(ref startPose);
                    poses.startingWinPose = startPose;

                    if (choice != 0)
                    {
                        startButtonPressed = choice;
                        Log.Info("Start Exploring button prssed - Implement further actions here");
                        showUI = false;
                    }
                }
                else
                {
                    // Decide which model is active, and draw it
                    Model? active = null;

                    switch (startButtonPressed)
                    {
                        case 1:
                            // Draw the circulatory system
                            active = models.circulatorySystem;
                            break;
                        case 2:
                            // Draw the digestive system
                            active = models.digestiveSystem;
                            break;
                        case 3:
                            // Draw the endocrine system
                            active = models.endocrineSystem;
                            break;
                        case 4:
                            // Draw the lymphatic system
                            active = models.lymphaticSystem;
                            break;
                        case 5:
                            // Draw the muscular system
                            active = models.muscularSystem;
                            break;
                        case 6:
                            // Draw the nervous system
                            active = models.nervousSystem;
                            break;
                        case 7:
                            // Draw the respiratory system
                            active = models.respiratorySystem;
                            break;
                        case 8:
                            // Draw the skeletal system
                            active = models.skeletalSystem;
                            break;
                        case 9:
                            // Draw the urinary system
                            active = models.urinarySystem;
                            break;
                        default:
                            Log.Warn("Unknown button pressed state");
                            break;
                    }

                    if (active != null)
                    {
                        var modelWinPose = poses.modelWinPose;   // local copy so we can pass by ref
                        float scale = poses.systemScale;         // local copy so we can pass by ref

                        bool goBack = LoadInterfaces.ModelInterface(ref modelWinPose, active, ref selectedAnim, ref loopAnim, ref scale);

                        poses.modelWinPose = modelWinPose;       // write back the updated window pose
                        poses.systemScale = scale;              // write back the updated scale
                        //LoadInterfaces.ModelInterface(poses.modelWinPose) == true
                        if (goBack)
                        {
                            showUI = true;
                            startButtonPressed = 0;
                            selectedAnim = 0;
                            Log.Info("Model interface button pressed - Implement further actions here");
                        }

                        Bounds humanBounds = new Bounds(Vec3.Zero, new Vec3(0.6f, 1.8f, 0.6f));
                        var systemPose = poses.systemPose;   // local copy so we can pass by ref
                        UI.HandleBegin("system-handle", ref systemPose, humanBounds);
                        UI.HandleEnd();
                        poses.systemPose = systemPose;       // write back the updated window pose

                        // Draw the big model with the current scale
                        active.Draw(poses.systemPose, poses.systemScale);
                    }

                } 

                

            });
        }
    }
}
