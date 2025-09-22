using StereoKit;


namespace Scene
{
    public static class LoadModels
    {
        public static Models LoadAll()
        {
            Log.Info("Loading Models");

            var circulatorySystem = Model.FromFile("circulatorySystem.glb");
            var digestiveSystem = Model.FromFile("digestiveSystem.opt.glb");
            var endocrineSystem = Model.FromFile("endocrineSystem.opt.glb");
            var lymphaticSystem = Model.FromFile("lymphaticSystem.opt.glb");
            var muscularSystem = Model.FromFile("muscularSystem.opt.glb");
            var nervousSystem = Model.FromFile("nervousSystem.opt.glb");
            var respiratorySystem = Model.FromFile("respiratorySystem.glb");
            var skeletalSystem = Model.FromFile("skeletalSystem.opt.glb");
            var urinarySystem = Model.FromFile("urinarySystem.opt.glb");


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

            return new Models(circulatorySystem, digestiveSystem, endocrineSystem, lymphaticSystem, muscularSystem, nervousSystem, respiratorySystem, skeletalSystem, urinarySystem);
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
                Log.Info($"Playing anim: {name}");
                m.PlayAnim(name, mode);        // <-- call ONCE
            }
            else
            {
                Log.Warn($"No animations found on {m}");
            }
        }
    }

     public record Models(Model circulatorySystem, Model digestiveSystem, Model endocrineSystem, Model lymphaticSystem, Model muscularSystem, Model nervousSystem, Model respiratorySystem, Model skeletalSystem, Model urinarySystem);
}
    




