using Microsoft.Xna.Framework;


namespace RadicalSkiingPrototypeOne.Core
{

    public class Camera
    {
        private Vector2 _position;
        public Vector2 _relativeOrigin;

        private Matrix _transform = Matrix.Identity;
        private Matrix _translate = Matrix.Identity;
        private ResolutionManager _resolutionManager;

        public Camera(ResolutionManager resolutionManager)
        {
            _resolutionManager = resolutionManager;
            _position = new Vector2((float)_resolutionManager.InternalWidth / 2, (float)_resolutionManager.InternalHeight / 2);
            _relativeOrigin = new Vector2(0, 0);
            

        }

        public void Update(Vector2 newPosition)
        {

            _position = newPosition;

            _relativeOrigin.X = _position.X - (float)_resolutionManager.InternalWidth / 2;
            _relativeOrigin.Y = _position.Y - (float)_resolutionManager.InternalHeight / 2;

            Matrix.CreateTranslation(-_relativeOrigin.X, -_relativeOrigin.Y, 0, out _translate);

            _transform = _translate * _resolutionManager.GetResolutionTransformMatrix();
        }

        public Matrix GetTransformMatrix()
        {
            return _transform;
        }

        public void RecalculateTransformationMatrix()
        {

        }
    }

}
