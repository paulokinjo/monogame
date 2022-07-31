using Microsoft.Xna.Framework;

namespace FloodControl
{
    public class FallingPiece : GamePiece
    {
        public int VerticalOffset;
        public static int fallRate = 5;

        public FallingPiece(string pieceType, int verticalOffset) 
            : base(pieceType)
        {
            VerticalOffset = verticalOffset;
        }

        public void UpdatePiece()
        {
            VerticalOffset = (int)MathHelper.Max(0, VerticalOffset - fallRate);
        }
    }
}
