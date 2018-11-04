﻿using System;
using System.Collections.Generic;
using ManagedBass;
using Microsoft.Xna.Framework;
using Quaver.Audio;
using Quaver.Graphics;
using Wobble.Graphics;
using Wobble.Graphics.Sprites;
using Wobble.Graphics.Animations;

namespace Quaver.Screens.Menu.UI.Visualizer
{
    public class MenuAudioVisualizer : Sprite
    {
        /// <summary>
        ///     The number of bars in the visualizer.
        /// </summary>
        public List<Sprite> Bars { get; }

        /// <summary>
        ///     The max height of the bars.
        /// </summary>
        public int MaxBarHeight { get; }

        /// <inheritdoc />
        ///   <summary>
        ///   </summary>
        ///   <param name="width"></param>
        ///   <param name="maxHeight"></param>
        ///   <param name="numBars"></param>
        ///  <param name="barWidth"></param>
        public MenuAudioVisualizer(int width, int maxHeight, int numBars, int barWidth)
        {
            MaxBarHeight = maxHeight;

            Size = new ScalableVector2(width, maxHeight);
            Alpha = 0f;

            Bars = new List<Sprite>();

            for (var i = 0; i < numBars; i++)
            {
                var bar = new Sprite()
                {
                    Parent = this,
                    Alignment = Alignment.BotLeft,
                    Tint = Colors.MainAccentInactive,
                    Width = barWidth,
                    X = barWidth * i + i * 5,
                    Alpha = 0.20f
                };

                Bars.Add(bar);
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            InterpolateBars();
            base.Update(gameTime);
        }

        /// <summary>
        ///     Changes the height of the bars.
        /// </summary>
        private void InterpolateBars()
        {
            var spectrumData = new float[2048];

            if (AudioEngine.Track == null || AudioEngine.Track.IsDisposed)
                return;

            if (AudioEngine.Track.IsPlaying)
                Bass.ChannelGetData(AudioEngine.Track.Stream, spectrumData, (int)DataFlags.FFT2048);

            for (var i = 0; i < Bars.Count; i++)
            {
                var bar = Bars[i];

                var targetHeight = spectrumData[i] * MaxBarHeight;

                // Lock the Animations to prevent any current updates.
                lock (bar.Animations)
                {
                    bar.Animations.Clear();
                    bar.Animations.Add(new Animation(AnimationProperty.Height, Easing.Linear,
                        bar.Height, targetHeight, 50f));
                }
            }
        }
    }
}