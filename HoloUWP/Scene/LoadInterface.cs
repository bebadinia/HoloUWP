using StereoKit;
using System.Xml.Linq;

namespace Scene
{
    public static class LoadInterfaces
    {
        public static int DrawStartInterface()
        {
            int result = 0;

            UI.WindowBegin("Main Window", ref AppState.StartingWinPose);

            UI.Label("Welcome to the Mixed Reality Experience!");
            UI.Label("Choose a system to explore:");

            if (UI.Button("Circulatory System"))
            {
                System.Diagnostics.Debug.WriteLine("Circulatory System button pressed");
                result = 1;
            }

            if (UI.Button("Digestive System"))
            {
                System.Diagnostics.Debug.WriteLine("Digestive System button pressed");
                result = 2;
            }

            if (UI.Button("Endocrine System"))
            {
                System.Diagnostics.Debug.WriteLine("Endocrine System button pressed");
                result = 3;
            }

            if (UI.Button("Lymphatic System"))
            {
                System.Diagnostics.Debug.WriteLine("Lymphatic System button pressed");
                result = 4;
            }

            if (UI.Button("Muscular System"))
            {
                System.Diagnostics.Debug.WriteLine("Muscular System button pressed");
                result = 5;
            }

            if (UI.Button("Nervous System"))
            {
                System.Diagnostics.Debug.WriteLine("Nervous System button pressed");
                result = 6;
            }

            if (UI.Button("Respiratory System"))
            {
                System.Diagnostics.Debug.WriteLine("Respiratory System button pressed");
                result = 7;
            }

            if (UI.Button("Skeletal System"))
            {
                System.Diagnostics.Debug.WriteLine("Skeletal System button pressed");
                result = 8;
            }

            if (UI.Button("Urinary System"))
            {
                System.Diagnostics.Debug.WriteLine("Urinary System button pressed");
                result = 9;
            }

            UI.WindowEnd();
            return result; // Indicate that the button was not pressed
        }


        public static bool ModelInterface(Model model)
        {
            UI.WindowBegin("Model Controls", ref AppState.ModelWinPose);

            //UI.Label("This is the Model Interface");
            //UI.Label("Use the menu to Go Back.");

            if (UI.Button("◀  Back"))
            {
                System.Diagnostics.Debug.WriteLine("Go Back button pressed");
                UI.WindowEnd();
                AppState.LoopAnim = false; // Reset loop state when going back

                if (model.ActiveAnim != null)
                {
                    model.PlayAnim(model.ActiveAnim,  AnimMode.Once); ; // Stop any active animation
                }

                AppState.ShowLabels = false;

                return true; // Indicate that the button was pressed
            }

            UI.HSeparator();

            // Scale slider for the big model
            UI.Label("Scale: ");
            UI.SameLine();
            UI.HSlider("system-scale", ref AppState.SystemScale, 0.2f, 1.0f, 0.1f, 0.15f);

            UI.HSeparator();

            if (AppState.SelectedAnim < 0 || AppState.SelectedAnim >= model.Anims.Count) // Clamp selection just in case
            {
                AppState.SelectedAnim = 0;
            }

            // Guard if the model has no animations
            if (model.Anims.Count == 0)
            {
                UI.Label("No animations found for this model.");
                //UI.WindowEnd();
                //return false;
            }
            else
            {
                UI.Label($"Animations ({model.Anims.Count}):");
                // Radio list of all animations (one selectable)
                for (int i = 0; i < model.Anims.Count; i++)
                {
                    bool isActive = (AppState.SelectedAnim == i);
                    // StereoKit radio pattern: UI.Radio("label", ref int value, int id)
                    if (UI.Radio(model.Anims[i].Name, isActive))
                    {
                        AppState.SelectedAnim = i;
                        model.PlayAnim(model.Anims[i].Name, AppState.LoopAnim ? AnimMode.Loop : AnimMode.Once);
                    }
                }

                UI.HSeparator();

                if (UI.Toggle("Loop", ref AppState.LoopAnim))
                {
                    // If an animation is already selected, restart it with the new loop setting
                    if (AppState.SelectedAnim >= 0 && AppState.SelectedAnim < model.Anims.Count)
                    {
                        string name = model.Anims[AppState.SelectedAnim].Name;
                        model.PlayAnim(name, AppState.LoopAnim ? AnimMode.Loop : AnimMode.Once);
                    }
                }

                if (UI.Button("▶  Play Selected"))
                {
                    string name = model.Anims[AppState.SelectedAnim].Name;
                    model.PlayAnim(name, AppState.LoopAnim ? AnimMode.Loop : AnimMode.Once);

                    
                }
            }

            UI.HSeparator();

            if (UI.Toggle("Labels", ref AppState.ShowLabels))
            {
                // If an animation is already selected, restart it with the new loop setting
                if (AppState.SelectedAnim >= 0 && AppState.SelectedAnim < model.Anims.Count)
                {
                    string name = model.Anims[AppState.SelectedAnim].Name;
                    model.PlayAnim(name, AppState.LoopAnim ? AnimMode.Loop : AnimMode.Once);
                }
            }

            UI.HSeparator();

            if (UI.Button("Reset Models"))
            {
                AppState.Reset();
            }

            UI.WindowEnd();
            //UI.HandleEnd();
            return false; // Indicate that the button was not pressed
        }
    }
}