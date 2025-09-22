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

            SK.Run(() =>
            {

                if (showUI)
                {
                    int choice = LoadInterfaces.DrawStartInterface(poses.startingWinPose);
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
                            active.Draw(poses.SystemPose, poses.SystemScale);
                            break;
                        case 2:
                            // Draw the digestive system
                            active = models.digestiveSystem;
                            active.Draw(poses.SystemPose, poses.SystemScale);
                            break;
                        case 3:
                            // Draw the endocrine system
                            active = models.endocrineSystem;
                            active.Draw(poses.SystemPose, poses.SystemScale);
                            break;
                        case 4:
                            // Draw the lymphatic system
                            active = models.lymphaticSystem;
                            active.Draw(poses.SystemPose, poses.SystemScale);
                            break;
                        case 5:
                            // Draw the muscular system
                            active = models.muscularSystem;
                            active.Draw(poses.SystemPose, poses.SystemScale);
                            break;
                        case 6:
                            // Draw the nervous system
                            active = models.nervousSystem;
                            active.Draw(poses.SystemPose, poses.SystemScale);
                            break;
                        case 7:
                            // Draw the respiratory system
                            active = models.respiratorySystem;
                            active.Draw(poses.SystemPose, poses.SystemScale);
                            break;
                        case 8:
                            // Draw the skeletal system
                            active = models.skeletalSystem;
                            active.Draw(poses.SystemPose, poses.SystemScale);
                            break;
                        case 9:
                            // Draw the urinary system
                            active = models.urinarySystem;
                            active.Draw(poses.SystemPose, poses.SystemScale);
                            break;
                        default:
                            Log.Warn("Unknown button pressed state");
                            break;
                    }

                    if (active != null)
                    {
                        bool goBack = LoadInterfaces.ModelInterface(poses.modelWinPose, active, ref selectedAnim, ref loopAnim);
                        //LoadInterfaces.ModelInterface(poses.modelWinPose) == true
                        if (goBack)
                        {
                            showUI = true;
                            startButtonPressed = 0;
                            selectedAnim = 0;
                            Log.Info("Model interface button pressed - Implement further actions here");
                        }
                    }

                }

                // draw scene
                //models.RespiratorySystem.Draw(poses.RespiratoryPose);
                //models.Heart.Draw(poses.HeartPose, poses.HeartScale);
                //models.Fox.Draw(poses.FoxPose, poses.FoxScale);

            });
        }
    }
}
