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
                        Log.Info("Start Exploring button pressed - Implement further actions here");
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
                            // Draw the heart model
                            active = models.Heart;
                            active.Draw(poses.heartPose, poses.heartScale);
                            break;
                        case 2:
                            // Draw the respiratory system
                            active = models.RespiratorySystem;
                            active.Draw(poses.respiratoryPose, poses.respiratorySystemScale);
                            break;
                        case 3:
                            // Draw the fox model
                            active = models.Fox;
                            active.Draw(poses.foxPose, poses.foxScale);
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
