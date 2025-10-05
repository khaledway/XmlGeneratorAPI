using XmlGeneratorAPI.Enums;

namespace XmlGeneratorAPI.Builders
{
    public interface IXmlBuilder
    {
        IXmlBuilder Reset();
        IXmlBuilder AddEpcisHeader(DateTime creationDate  , bool includeCbvmdaNamespace = false);
        IXmlBuilder AddEventTime(DateTime eventTime);
        IXmlBuilder AddRecordTime(string recordTime);
        IXmlBuilder AddEventTimeZoneOffset(string offset);
        IXmlBuilder AddEpcList(List<string> epcs);
        IXmlBuilder AddAction(string action);
        IXmlBuilder AddBizStep(string bizStep);
        IXmlBuilder AddDisposition(string disposition);
        IXmlBuilder AddReadPoint(string readPoint);
        IXmlBuilder AddBizLocation(string bizLocation);
        IXmlBuilder AddSourceList(string sourceType, string sourceValue);
        IXmlBuilder AddDestinationList(string destType, string destValue);
        IXmlBuilder AddIlmd(string lotNumber, DateOnly expirationDate);
        IXmlBuilder AddParentId(string parentId);
        IXmlBuilder AddChildEPCs(List<string> childEpcs);
        IXmlBuilder AddQuantityList(string epcClass, double quantity, string uom);
        IXmlBuilder AddErrorDeclaration(DateTime declarationTime, string reason, string correctiveEventId);
        IXmlBuilder AddEventId(string eventId);
        IXmlBuilder AddCustomExtension(string xmlNamespace, string elementName, string value);

        IXmlBuilder SetEventType(EventType eventType);
        string Build();
    }

   
}
