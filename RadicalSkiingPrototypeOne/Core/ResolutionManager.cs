using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace RadicalSkiingPrototypeOne.Core
{

    public class ResolutionManager
    {
        private Game _game;
        private GraphicsDeviceManager _graphics;
        private Matrix _scaleMatrix = Matrix.Identity;
        private Matrix _translateMatrix = Matrix.Identity;


        public int InternalHeight, InternalWidth;
        public int DefaultHeight, DefaultWidth;
        public int PreferredHeight, PreferredWidth, TargetPreferredWidth;

        private bool _isInternalResolutionGreater = true;
        private bool _scaleEvenWhenInternalResolutionIsGreater = true; //True for testing only

        public Color BackgroundColor = Color.Orange;

        public ResolutionManager(Game game, GraphicsDeviceManager graphics)
        {
            _game = game;
            _graphics = graphics;

            InternalWidth = 1920; //480; 
            InternalHeight = 1080; //270;
            DefaultWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            DefaultHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            PreferredWidth = 1366; //1024;  //DefaultWidth;
            PreferredHeight = 768;  //576;  //DefaultHeight;
            TargetPreferredWidth = PreferredWidth;

        }

        public void Initialize()
        {
            _graphics.PreferredBackBufferWidth = PreferredWidth;
            _graphics.PreferredBackBufferHeight = PreferredHeight;

            if (PreferredWidth > InternalWidth)
                _isInternalResolutionGreater = false;
            else
                _isInternalResolutionGreater = true;

                 _graphics.ToggleFullScreen();
            //_game.Window.IsBorderless = true;
            //_game.Window.AllowUserResizing = true; //Very Bad Idea 
            _graphics.ApplyChanges();

        }

        public void RecreateTranslationMatrix()
        {
            var AspectRatio = PreferredWidth / PreferredHeight;
            int TranslateX = 0; int TranslateY = 0;
            //Console.WriteLine("PreferredWidth During Translate: " + PreferredWidth);
            if (_isInternalResolutionGreater && !_scaleEvenWhenInternalResolutionIsGreater) // will not translate when _scaleEvenWhenInteralResolutionIsGreater is enabled
            {
                //Scaling not done by default in this case
                TranslateX = InternalWidth / 2 - PreferredWidth / 2;
                TranslateY = InternalHeight / 2 - PreferredHeight / 2;
                //Console.WriteLine("Translateddd.....");
            }
            else
            {
                //Since the view is already scaled to PreferredHeight, only Width is needed to be cropped
                TranslateX = TargetPreferredWidth / 2 - PreferredWidth / 2;
                TranslateY = 0;
            }
            Matrix.CreateTranslation(-TranslateX, -TranslateY, 0, out _translateMatrix);

        }

        public void RecreateScaleMatrix()
        {

            float RatioX = 1f, RatioY = 1f;

            float InternalAspectRatio = (float)InternalWidth / InternalHeight;
            float TargetWidth = PreferredHeight * InternalAspectRatio;

            //Console.WriteLine("Changed PreferredWidth from: " + PreferredWidth + " To :" + (int)TargetPreferredWidth);
            TargetPreferredWidth = (int)TargetWidth;

            if (!_isInternalResolutionGreater)
            {   //Default scaling only done when Internal resolution is smaller than Preferred resolution
                //Upscaling
                RatioX = (float)TargetPreferredWidth / InternalWidth;
                RatioY = (float)PreferredHeight / InternalHeight;
            }
            else if (_scaleEvenWhenInternalResolutionIsGreater)
            {
                //Manual scaling only done for testing even when Internal resolution is larger than Preferred resolution
                //DownScaling
                //For testing only
                RatioX = (float)PreferredWidth / InternalWidth;
                RatioY = (float)PreferredHeight / InternalHeight;

            }

            Matrix.CreateScale(RatioX, RatioY, 1f, out _scaleMatrix);
        }


        public Matrix GetResolutionTransformMatrix()
        {
            RecreateScaleMatrix();
            RecreateTranslationMatrix();
            return _scaleMatrix * _translateMatrix;
        }

    }

}
