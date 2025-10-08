using StereoKit;

namespace Scene
{
    public static class LoadInterfaces
    {
        public static int DrawStartInterface(ref Pose windowPose)
        {
            // Make the whole panel movable
            //Bounds panelBounds = new Bounds(Vec3.Zero, new Vec3(0.45f, 0.35f, 0.02f));
            //UI.HandleBegin("main-window-handle", ref windowPose, panelBounds);

            UI.WindowBegin("Main Window", ref windowPose);

            UI.Label("Welcome to the Mixed Reality Experience!");
            UI.Label("Choose a system to explore:");

            if (UI.Button("Circulatory System"))
            {
                Log.Info("Circulatory System button pressed");
                //UI.WindowEnd();
                return 1;
            }

            if (UI.Button("Digestive System"))
            {
                Log.Info("Digestive System button pressed");
                //UI.WindowEnd();
                return 2;
            }

            if (UI.Button("Endocrine System"))
            {
                Log.Info("Endocrine System button pressed");
                //UI.WindowEnd();
                return 3;
            }

            if (UI.Button("Lymphatic System"))
            {
                Log.Info("Lymphatic System button pressed");
                //UI.WindowEnd();
                return 4; 
            }

            if (UI.Button("Muscular System"))
            {
                Log.Info("Muscular System button pressed");
                //UI.WindowEnd();
                return 5;
            }

            if (UI.Button("Nervous System"))
            {
                Log.Info("Nervous System button pressed");
                //UI.WindowEnd();
                return 6;
            }

            if (UI.Button("Respiratory System"))
            {
                Log.Info("Respiratory System button pressed");
                //UI.WindowEnd();
                return 7;
            }

            if (UI.Button("Skeletal System"))
            {
                Log.Info("Skeletal System button pressed");
                //UI.WindowEnd();
                return 8;
            }

            if (UI.Button("Urinary System"))
            {
                Log.Info("Urinary System button pressed");
                //UI.WindowEnd();
                return 9;
            }

            UI.WindowEnd();
            //UI.HandleEnd();
            return 0; // Indicate that the button was not pressed
        }

        public static bool ModelInterface(ref Pose windowPose, Model model, ref int selectedAnim, ref bool loopAnim, ref float systemScale)
        {

            Bounds panelBounds = new Bounds(Vec3.Zero, new Vec3(0.45f, 0.40f, 0.02f));
            UI.HandleBegin("model-window-handle", ref windowPose, panelBounds);

            UI.WindowBegin("Model Controls", ref windowPose);

            //UI.Label("This is the Model Interface");
            //UI.Label("Use the menu to Go Back.");

            if (UI.Button("◀  Back"))
            {
                Log.Info("Go Back button pressed");
                UI.WindowEnd();
                return true; // Indicate that the button was pressed
            }

            // Scale slider for the big model
            UI.Label("Model Scale");
            UI.HSlider("system-scale", ref systemScale, 0.2f, 1.0f, 0.1f);

            UI.HSeparator();
            UI.Label($"Animations ({model.Anims.Count}):");

            // Guard if the model has no animations
            if (model.Anims.Count == 0)
            {
                UI.Label("No animations found for this model.");
                UI.WindowEnd();
                return false;
            }

            // Clamp selection just in case
            if (selectedAnim < 0 || selectedAnim >= model.Anims.Count)
            {
                selectedAnim = 0;
            }

            // Radio list of all animations (one selectable)
            for (int i = 0; i < model.Anims.Count; i++)
            {
                bool isActive = (selectedAnim == i);
                // StereoKit radio pattern: UI.Radio("label", ref int value, int id)
                if (UI.Radio(model.Anims[i].Name, isActive))
                {
                    selectedAnim = i;
                    model.PlayAnim(model.Anims[i].Name, loopAnim ? AnimMode.Loop : AnimMode.Once);

                }
            }

            UI.HSeparator();

            if (UI.Toggle("Loop", ref loopAnim))
            {
                // If an animation is already selected, restart it with the new loop setting
                if (selectedAnim >= 0 && selectedAnim < model.Anims.Count)
                {
                    string name = model.Anims[selectedAnim].Name;
                    model.PlayAnim(name, loopAnim ? AnimMode.Loop : AnimMode.Once);
                }
            }

            if (UI.Button("▶  Play Selected"))
            {
                string name = model.Anims[selectedAnim].Name;
                model.PlayAnim(name, loopAnim ? AnimMode.Loop : AnimMode.Once);
            }


            UI.WindowEnd();
            UI.HandleEnd();
            return false; // Indicate that the button was not pressed
        }
    }
}