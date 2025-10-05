namespace XmlGeneratorAPI.Enums
{
    public enum BizStep
    {
        CommissioningSGTIN =1,
        CommissioningSSCC,
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
    public enum FileType
    {
        SGTIN = 1,
        SSCC
    }
}