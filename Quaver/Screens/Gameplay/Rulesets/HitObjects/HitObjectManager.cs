using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Quaver.API.Enums;
using Quaver.API.Maps;
using Quaver.API.Maps.Structures;
using Quaver.Skinning;

namespace Quaver.Screens.Gameplay.Rulesets.HitObjects
{
    public abstract class HitObjectManager
    {
        /// <summary>
        ///     The number of objects left in the map
        ///     (Has to be implemented per game mode because pooling may be different.)
        /// </summary>
        public abstract int ObjectsLeft { get; }

        /// <summary>
        ///     The next object in the pool. Used for skipping.
        /// </summary>
        public abstract HitObjectInfo NextHitObject { get; }

        /// <summary>
        ///     Used to determine if the player is currently on a break in the song.
        /// </summary>
        public abstract bool OnBreak { get; }

        /// <summary>
        ///     If there are no more objects and the map is complete.
        /// </summary>
        public bool IsComplete => ObjectsLeft == 0;

        /// <summary>
        ///     Ctor -
        /// </summary>
        /// <param name="size"></param>
        public HitObjectManager(Qua map) { }

        /// <summary>
        ///     Updates all the containing HitObjects
        /// </summary>
        /// <param name="gameTime"></param>
        public abstract void Update(GameTime gameTime);

        /// <summary>
        ///     Plays the correct hitsounds based on the note index of the HitObjectPool.
        /// </summary>
        public static void PlayObjectHitSounds(API.Maps.Structures.HitObjectInfo hitObject)
        {
            // Normal
            if (hitObject.HitSound == 0 || (HitSounds.Normal & hitObject.HitSound) != 0)
                SkinManager.Skin.SoundHit.CreateChannel().Play();

            // Clap
            if ((HitSounds.Clap & hitObject.HitSound) != 0)
                SkinManager.Skin.SoundHitClap.CreateChannel().Play();

            // Whistle
            if ((HitSounds.Whistle & hitObject.HitSound) != 0)
                SkinManager.Skin.SoundHitWhistle.CreateChannel().Play();

            // Finish
            if ((HitSounds.Finish & hitObject.HitSound) != 0)
                SkinManager.Skin.SoundHitFinish.CreateChannel().Play();
        }
    }
}
