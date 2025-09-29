namespace XmlGeneratorAPI.Enums
{
    public enum BizStep
    {
        Commissioning =1,
        Packing,
        Shipping,
        VoidShipping,
        Receiving,
        ReceivingReturning,
        PartialReceivingReturning,
        Unpacking,
        Sampling,
        Missing,
        Destroy,
        ErrorDeclaration
    }

    public enum EventAction
    {
        Add = 1,
        Delete,
        Observe 
    }
    public enum EventType
    {
        Object = 1,
        Aggregation,
        Transaction,
        Transformation
    }

    public enum DispositionEvent
    {
        Active=1,
        InProgress,       
        InTransit,
        Returned,
        Stolen,
        Destroyed,
        RecallPending
    }
}