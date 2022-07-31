using Microsoft.Xna.Framework;

namespace FloodControl
{
    public class RotatingPiece : GamePiece
    {
        private float rotationAmount = 0;

        public bool clockwise;

        public static float rotationRate = (MathHelper.PiOver2 / 10);
        public int rotationTicksRemaining = 10;

        public float RotationAmount => clockwise ? rotationAmount : (MathHelper.Pi * 2) - rotationAmount;

        public RotatingPiece(string pieceType, bool clockwise)
            : base(pieceType)
        {
            this.clockwise = clockwise;
        }

        public void UpdatePiece()
        {
            rotationAmount += rotationRate;
            rotationTicksRemaining = (int)MathHelper.Max(0, rotationTicksRemaining - 1);
        }
    }
}
