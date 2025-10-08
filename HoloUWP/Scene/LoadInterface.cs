using StereoKit;

namespace Scene
{
    public static class LoadInterfaces
    {
        public static int DrawStartInterface(ref Pose windowPose)
        {
            int result = 0;

            UI.WindowBegin("Main Window", ref windowPose);

            UI.Label("Welcome to the Mixed Reality Experience!");
            UI.Label("Choose a system to explore:");

            if (UI.Button("Circulatory System"))
            {
                Log.Info("Circulatory System button pressed");
                result = 1;
            }

            if (UI.Button("Digestive System"))
            {
                Log.Info("Digestive System button pressed");
                result = 2;
            }

            if (UI.Button("Endocrine System"))
            {
                Log.Info("Endocrine System button pressed");
                result = 3;
            }

            if (UI.Button("Lymphatic System"))
            {
                Log.Info("Lymphatic System button pressed");
                result = 4; 
            }

            if (UI.Button("Muscular System"))
            {
                Log.Info("Muscular System button pressed");
                result = 5;
            }

            if (UI.Button("Nervous System"))
            {
                Log.Info("Nervous System button pressed");
                result = 6;
            }

            if (UI.Button("Respiratory System"))
            {
                Log.Info("Respiratory System button pressed");
                result = 7;
            }

            if (UI.Button("Skeletal System"))
            {
                Log.Info("Skeletal System button pressed");
                result = 8;
            }

            if (UI.Button("Urinary System"))
            {
                Log.Info("Urinary System button pressed");
                result = 9;
            }

            UI.WindowEnd();
            return result; // Indicate that the button was not pressed
        }

        public static bool ModelInterface(ref Pose windowPose, Model model, ref int selectedAnim, ref bool loopAnim /*, ref float systemScale*/)
        {
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
            //UI.Label("Model Scale");
            //UI.HSlider("system-scale", ref systemScale, 0.2f, 1.0f, 0.1f);

            UI.HSeparator();

            if (selectedAnim < 0 || selectedAnim >= model.Anims.Count) // Clamp selection just in case
            {
                selectedAnim = 0;
            }

            // Guard if the model has no animations
            if (model.Anims.Count == 0)
            {
                UI.Label("No animations found for this model.");
                UI.WindowEnd();
                return false;
            }
            else 
            {
                UI.Label($"Animations ({model.Anims.Count}):");
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
            }

            UI.WindowEnd();
            //UI.HandleEnd();
            return false; // Indicate that the button was not pressed
        }
    }
}