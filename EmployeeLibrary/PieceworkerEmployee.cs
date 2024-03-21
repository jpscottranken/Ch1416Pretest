using System;

namespace EmployeeLibrary
{
    public class PieceworkerEmployee : Employee
    {
        //  Constructor
        public PieceworkerEmployee(string firstName,
                                   string lastName,
                                   int empNumber,
                                   int pieces,
                                   decimal priceperpiece)
                        : base(firstName, lastName, empNumber)
        {
            Pieces = pieces;
            PricePerPiece = priceperpiece;
        }

        //  Getters and Setters
        int Pieces { get; set; }

        decimal PricePerPiece { get; set; }

        public override string displayText()
        {
            return base.displayText()   + 
                   "\r\nPieces: "       + Pieces.ToString() +
                   "\r\nPrice/Piece: "  + PricePerPiece.ToString("c");
        }
    }
}
