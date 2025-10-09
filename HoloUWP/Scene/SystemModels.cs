using StereoKit;


namespace Scene
{
    public static class SystemsModels
    {
        public static Model Circulatory;
        public static Model Digestive;
        public static Model Endocrine;
        public static Model Lymphatic;
        public static Model Muscular;
        public static Model Nervous;
        public static Model Respiratory;
        public static Model Skeletal;
        public static Model Urinary;

        public static void LoadAll()
        {
            System.Diagnostics.Debug.WriteLine("Loading Models");

            Circulatory = Model.FromFile("circulatorySystem.glb");
            Digestive = Model.FromFile("digestiveSystem.opt.glb");
            Endocrine = Model.FromFile("endocrineSystem.opt.glb");
            Lymphatic = Model.FromFile("lymphaticSystem.opt.glb");
            Muscular = Model.FromFile("muscularSystem.opt.glb");
            Nervous = Model.FromFile("nervousSystem.opt.glb");
            Respiratory = Model.FromFile("respiratorySystem.glb");
            Skeletal = Model.FromFile("skeletalSystem.opt.glb");
            Urinary = Model.FromFile("urinarySystem.opt.glb");


            // Try to play the animations if they exist
            /*TryPlayFirstAnimation(circulatorySystem, AnimMode.Loop);
            TryPlayFirstAnimation(digestiveSystem, AnimMode.Loop);
            TryPlayFirstAnimation(endocrineSystem, AnimMode.Loop);
            TryPlayFirstAnimation(lymphaticSystem, AnimMode.Loop);
            TryPlayFirstAnimation(muscularSystem, AnimMode.Loop);
            TryPlayFirstAnimation(nervousSystem, AnimMode.Loop);
            TryPlayFirstAnimation(respiratorySystem, AnimMode.Loop);
            TryPlayFirstAnimation(skeletalSystem, AnimMode.Loop);
            TryPlayFirstAnimation(urinarySystem, AnimMode.Loop); */
        }


        static void TryPlayFirstAnimation(Model m, AnimMode mode)
        {
            if (m.Anims.Count > 0)
            {
                for (int i = 0; i < m.Anims.Count; i++)
                {
                    Log.Info($"Anim {i}: {m.Anims[i].Name}");
                }
                string name = m.Anims[0].Name;
                System.Diagnostics.Debug.WriteLine($"Playing anim: {name}");
                m.PlayAnim(name, mode);        // <-- call ONCE
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"No animations found on {m}");
            }
        }

    }
}
    




