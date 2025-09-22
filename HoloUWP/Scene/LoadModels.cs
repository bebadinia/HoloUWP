using StereoKit;


namespace Scene
{
    public static class LoadModels
    {
        public static Models LoadAll()
        {
            Log.Info("Loading Models");
            //var helmet = Model.FromFile("DamagedHelmet.opt.glb");
            //var game = Model.FromFile("ABeautifulGame.opt.glb");
            //var sphere = Model.FromFile("AnimatedMorphSphere.glb");
            var fox = Model.FromFile("Fox.glb");
            var resSystem = Model.FromFile("Respitory.glb");
            var heart = Model.FromFile("Heart.opt.glb");

            // Try to play the first animation on respiratory (if any)
            //TryPlayFirstAnimation(resSystem, AnimMode.Loop);
            //TryPlayFirstAnimation(heart, AnimMode.Loop);
            //TryPlayFirstAnimation(fox, AnimMode.Loop);

            return new Models(resSystem, heart, fox);
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

     public record Models(Model RespiratorySystem, Model Heart, Model Fox);
}
    




