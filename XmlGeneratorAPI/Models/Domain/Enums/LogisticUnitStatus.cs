namespace SSCCProject.Domain.Enums;

public enum LogisticUnitStatus
{
    Created = 1,   // freshly created in the system
    Packed = 2,    // products assigned inside
    Sealed = 3,    // closed and ready for shipping
    InTransit = 4, // on the move
    Delivered = 5, // reached destination
    Returned = 6,  // sent back
    Damaged = 7    // flagged as damaged
}
