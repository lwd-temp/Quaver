﻿using System.Collections.Generic;
using Quaver.API.Enums;
using Quaver.Main;

namespace Quaver.States.Gameplay.HitObjects
{
    internal abstract class HitObjectManager
    {
        /// <summary>
        ///     All of the objects in the pool.
        /// </summary>
        internal List<HitObject> ObjectPool { get; }

        /// <summary>
        ///     The object pool size.
        /// </summary>
        internal int PoolSize { get; }

        /// <summary>
        ///     Ctor - 
        /// </summary>
        /// <param name="size"></param>
        internal HitObjectManager(int size)
        {
            PoolSize = size;
            ObjectPool = new List<HitObject>(PoolSize);
        }
        
        /// <summary>
        ///     Plays the correct hitsounds based on the note index of the HitObjectPool.
        /// </summary>
        internal void PlayObjectHitSounds(int index)
        {
            var hitObject = ObjectPool[index].Info;

            // Normal
            if (hitObject.HitSound == 0 || (HitSounds.Normal & hitObject.HitSound) != 0)
                GameBase.AudioEngine.PlaySoundEffect(GameBase.LoadedSkin.SoundHit);

            // Clap
            if ((HitSounds.Clap & hitObject.HitSound) != 0)
                GameBase.AudioEngine.PlaySoundEffect(GameBase.LoadedSkin.SoundHitClap);

            // Whistle
            if ((HitSounds.Whistle & hitObject.HitSound) != 0)
                GameBase.AudioEngine.PlaySoundEffect(GameBase.LoadedSkin.SoundHitWhistle);

            // Finish
            if ((HitSounds.Finish & hitObject.HitSound) != 0)
                GameBase.AudioEngine.PlaySoundEffect(GameBase.LoadedSkin.SoundHitFinish);
        }

        /// <summary>
        ///     Updates all the containing HitObjects
        /// </summary>
        /// <param name="dt"></param>
        internal abstract void Update(double dt);
    }
}