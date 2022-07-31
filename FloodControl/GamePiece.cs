using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace FloodControl
{
    public class GamePiece
    {
        public static string[] PieceTypes =
        {
            "Left,Right",
            "Top,Bottom",
            "Left,Top",
            "Top,Right",
            "Right,Bottom",
            "Bottom,Left",
            "Empty"
        };

        public const int PieceHeight = 40;
        public const int PieceWidth = 40;

        public const int MaxPlayablePieceIndex = 5;
        public const int EmptyPieceIndex = 6;

        private const int textureOffsetX = 1;
        private const int textureOffsetY = 1;
        private const int texturePaddingX = 1;
        private const int texturePaddingY = 1;

        public string PieceType { get; private set; } = string.Empty;
        public string PieceSuffix { get; private set; } = string.Empty;


        public GamePiece(string pieceType)
            : this(pieceType, string.Empty)
        {}

        public GamePiece(string pieceType, string pieceSuffix)
        {
            PieceType = pieceType;
            PieceSuffix = pieceSuffix;
        }

        public void SetPiece(string type, string suffix)
        {
            PieceType = type;
            PieceSuffix = suffix;   
        }

        public void SetPiece(string type) => SetPiece(type, string.Empty);

        public void AddSuffix(string suffix)
        {
            if (PieceSuffix.Contains(suffix) is false)
            {
                PieceSuffix += suffix;
            }
        }

        public void RemoveSuffix(string suffix) => PieceSuffix = PieceSuffix.Replace(suffix, string.Empty);

        public void RotatePiece(bool clockWise)
        {
            switch (PieceType)
            {
                case "Left,Right":
                    PieceType = "Top,Bottom";
                    break;
                case "Top,Bottom":
                    PieceType = "Left,Right";
                    break;
                case "Left,Top":
                    PieceType = clockWise ?
                            "Top,Right" :
                            "Bottom,Left";
                    break;
                case "Top,Right":
                    PieceType = clockWise ?
                        "Right,Bottom" :
                        "Left,Top";
                    break;
                case "Right,Bottom":
                    PieceType = clockWise ?
                        "Bottom,Left" :
                        "Top,Right";
                    break;
                case "Bottom,Left":
                    PieceType = clockWise ?
                            "Left,Top" :
                            "Right,Bottom";
                    break;
                default:
                    break;
            }
        }

        public string[] GetOtherEnds(string startingEnd)
        {
            List<string> opposites = new List<string>();
            foreach (var end in PieceType.Split(","))
            {
                if (end != startingEnd)
                {
                    opposites.Add(end);
                }
            }

            return opposites.ToArray();
        }

        public bool HasConnector(string direction) => PieceType.Contains(direction);

        public Rectangle GetSourceRect()
        {
            int x = textureOffsetX;
            int y = textureOffsetY;

            if (PieceSuffix.Contains("W"))
            {
                x += PieceWidth + texturePaddingX;
            }

            y += (Array.IndexOf(PieceTypes, PieceType) *
                    (PieceHeight + texturePaddingY));

            return new Rectangle(x, y, PieceWidth, PieceHeight);
        }
    }
}
