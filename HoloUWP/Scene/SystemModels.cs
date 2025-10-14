using StereoKit;
using System;
using System.Reflection;


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
            TryPlayFirstAnimation(Circulatory, AnimMode.Once);
            //TryPlayFirstAnimation(digestiveSystem, AnimMode.Loop);
            //TryPlayFirstAnimation(endocrineSystem, AnimMode.Loop);
            //TryPlayFirstAnimation(lymphaticSystem, AnimMode.Loop);
            //TryPlayFirstAnimation(muscularSystem, AnimMode.Loop);
            //TryPlayFirstAnimation(nervousSystem, AnimMode.Loop);
            TryPlayFirstAnimation(Respiratory, AnimMode.Once);
            //TryPlayFirstAnimation(skeletalSystem, AnimMode.Loop);
            //TryPlayFirstAnimation(urinarySystem, AnimMode.Loop); 

            // Uncomment to show the different Nodes
            ModelNode node = Skeletal.RootNode;
            int depth = 0;
            while (node != null)
            {
                string tabs = new string(' ', depth * 2);
                System.Diagnostics.Debug.WriteLine(tabs + node.Name);

                if (node.Child != null) { node = node.Child; depth++; }
                else if (node.Sibling != null) node = node.Sibling;
                else
                {
                    while (node != null)
                    {
                        if (node.Sibling != null)
                        {
                            node = node.Sibling;
                            break;
                        }
                        depth--;
                        node = node.Parent;
                    } 
                } 
            }

        }


        static void TryPlayFirstAnimation(Model m, AnimMode mode)
        {
            if (m.Anims.Count > 0)
            {
                for (int i = 0; i < m.Anims.Count; i++)
                {
                    System.Diagnostics.Debug.WriteLine($"Anim {i}: {m.Anims[i].Name}");
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
    




