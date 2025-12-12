// Program.cs by Ben Ebadinia

using Scene;
using StereoKit;
using System;
using System.Reflection;

namespace HoloUWP
{

    class Program
    {


#if DEBUG
        static bool runOnce = true;         // Only exists in Debug builds
#endif

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

            SystemsModels.LoadAll();        // Load and Set all models once

            // Show main menu and set initial selection state (No button pressed)
            bool showUI = true;
            int startButtonPressed = 0;

            // Actual application loop
            SK.Run(() =>
            {
                // Checks to see if we should show the main menu or a system model
                if (showUI)
                {
                    // Draw the main menu and check if button pressed
                    int choice = LoadInterfaces.DrawStartInterface();

                    // If a button was pressed, set the state to show that model
                    if (choice != 0)
                    {
                        startButtonPressed = choice;
                        showUI = false;                 // Turn off the main menu
                        AppState.Anchored = false;      // Re-anchor model and UI next frame
                    }

                }
                else
                {
                    // Decide which model is active, and draw it
                    Model? active = null;

                    // Select the model based on which button was pressed
                    switch (startButtonPressed)
                    {
                        case 1:
                            // Draw the circulatory system
                            // Check if labels should be shown based on AppState
                            active = AppState.ShowLabels ? SystemsModels.Circulatory : SystemsModels.CirculatoryNL;
                            break;
                        case 2:
                            // Draw the digestive system
                            active = AppState.ShowLabels ? SystemsModels.Digestive : SystemsModels.DigestiveNL;
                            break;
                        case 3:
                            // Draw the endocrine system
                            active = AppState.ShowLabels ? SystemsModels.Endocrine : SystemsModels.EndocrineNL;
                            break;
                        case 4:
                            // Draw the lymphatic system
                            active = AppState.ShowLabels ? SystemsModels.Lymphatic : SystemsModels.LymphaticNL;
                            break;
                        case 5:
                            // Draw the muscular system
                            active = AppState.ShowLabels ? SystemsModels.Muscular : SystemsModels.MuscularNL;
                            break;
                        case 6:
                            // Draw the nervous system
                            active = AppState.ShowLabels ? SystemsModels.Nervous : SystemsModels.NervousNL;
                            break;
                        case 7:
                            // Draw the respiratory system
                            active = AppState.ShowLabels ? SystemsModels.Respiratory : SystemsModels.RespiratoryNL;
                            break;
                        case 8:
                            // Draw the skeletal system
                            active = AppState.ShowLabels ? SystemsModels.Skeletal : SystemsModels.SkeletalNL;
                            break;
                        case 9:
                            // Draw the urinary system
                            active = AppState.ShowLabels ? SystemsModels.Urinary : SystemsModels.UrinaryNL;
                            break;
                        default:
                            System.Diagnostics.Debug.WriteLine("Unknown button pressed state");
                            break;
                    }

                    // If we have a valid model
                    if (active != null)
                    {
                        // Draw the model interface and check if we should go back
                        bool goBack = LoadInterfaces.ModelInterface(active);

                        // If user requested to go back, reset state to show main menu
                        if (goBack)
                        {
                            showUI = true;
                            startButtonPressed = 0;
                            AppState.Anchored = false;
                            System.Diagnostics.Debug.WriteLine("Going back to home screen");
                        }

                        // Boundary checking to find the chest height and offset for the grab handle (based on human height)
#if false
                        Bounds HumanBounds = new Bounds(Vec3.Zero, new Vec3(0.6f, 1.8f, 0.6f));
                        Bounds HumanBounds= active.Bounds;
                        humanBounds.dimensions *= AppState.SystemScale * 1.05f; // a tiny padding helps selection
                        UI.Handle("system-handle", ref AppState.SystemPose, HumanBounds);
                        UI.HandleEnd();
#endif

                        // Lookup per-system config directly by the selected button id.
                        if (startButtonPressed != 0)
                        {
                            // Config for each system
                            var cfg = AppState.ChestAdj[startButtonPressed];

                            // Model-space bounds and chest point
                            Bounds HumanBounds = active.Bounds;
                            float totalHeight = HumanBounds.dimensions.y;
                            float bottomY = HumanBounds.center.y - totalHeight * 0.5f;

                           
                            float middleF = cfg.chestF;     // fraction of height
                            float middleY = bottomY + totalHeight * middleF;

                            // Chest pivot in model space using manual local offset
                            Vec3 chestLocal = new Vec3(HumanBounds.center.x, middleY, HumanBounds.center.z) + cfg.offsetLocal;

                            // One time anchor of the handle pose relative to the Start Menu
                            if (!AppState.Anchored)
                            {
                                // Base of system relative to the Start Menu
                                // +X = right of the menu, +Y = up, -Z = in front (toward the user), +Z = behind
                                Vec3 modelFromMenu = new Vec3(-0.75f, 0.00f, 0.50f); // tweak to taste

                                // AnchorSystem: pose at menu + rotated offset, inherit menu facing
                                AppState.SystemPose.position = AppState.StartingWinPose.position + (AppState.StartingWinPose.orientation * modelFromMenu);
                                AppState.SystemPose.orientation = AppState.StartingWinPose.orientation;

                                // Anchor Model UI making it the same as Start window
                                AppState.ModelWinPose.position = AppState.StartingWinPose.position;
                                AppState.ModelWinPose.orientation = AppState.StartingWinPose.orientation;

                                AppState.Anchored = true; // don’t re-anchor after user grabs it
                            }

                            // Bounds of the model relative to the chest pivot (scaled)
                            Bounds handleBounds = new Bounds(
                                  (HumanBounds.center - chestLocal) * AppState.SystemScale,     // center offset
                                  HumanBounds.dimensions * AppState.SystemScale * 1.01f);       // tiny padding

                            // Grabbable handle with adjusted grab point
                            UI.Handle("system-handle", ref AppState.SystemPose, handleBounds);


                            // Draw model offset so visuals stay aligned under the chest pivot
                            Matrix modelXform = Matrix.TRS(
                                  AppState.SystemPose.position - (AppState.SystemPose.orientation * (chestLocal * AppState.SystemScale)),
                                  AppState.SystemPose.orientation,
                                  AppState.SystemScale);

                            // Finally draw the active model
                            active.Draw(modelXform);



#if false
                            if (runOnce)
                            {
                                foreach (ModelNode node in active.Nodes)
                                {

                                    Matrix nodeWorld =
                                        modelXform * node.ModelTransform; // or node.Transform in some versions
                                    Vec3 p = nodeWorld.Translation;
                                    System.Diagnostics.Debug.WriteLine(
                                        $"[NODE ] {node.Name} world=({p.x:0.###},{p.y:0.###},{p.z:0.###})");
                                }

                                System.Diagnostics.Debug.WriteLine($"[btn {startButtonPressed}] chestF={middleF:0.##} offset=({cfg.offsetLocal.x:0.###},{cfg.offsetLocal.y:0.###},{cfg.offsetLocal.z:0.###})");

                                runOnce = false;
                            }
#endif

#if false
                            System.Diagnostics.Debug.WriteLine(
                                 $"[BOUNDS] center=({HumanBounds.center.x:0.###},{HumanBounds.center.y:0.###},{HumanBounds.center.z:0.###}) " +
                                 $"size=({HumanBounds.dimensions.x:0.###},{HumanBounds.dimensions.y:0.###},{HumanBounds.dimensions.z:0.###})");
                             System.Diagnostics.Debug.WriteLine(
                                 $"[HEIGHT] totalHeight={totalHeight:0.###} bottomY={bottomY:0.###} middleF={middleF:0.##} middleY={middleY:0.###}");
                             System.Diagnostics.Debug.WriteLine(
                                 $"[WORLD ] feet=({feetWorld.x:0.###},{feetWorld.y:0.###},{feetWorld.z:0.###}) " +
                                 $"chest=({chestWorld.x:0.###},{chestWorld.y:0.###},{chestWorld.z:0.###}) " +
                                 $"head=({headWorld.x:0.###},{headWorld.y:0.###},{headWorld.z:0.###})");
#endif
                        }





                    }

                } 

                

            });
        }
    }
}
