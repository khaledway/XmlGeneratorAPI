using System.Text;
using System.Xml;
using XmlGeneratorAPI.Dtos;
using XmlGeneratorAPI.Enums;
using XmlGeneratorAPI.Requests;

namespace XmlGeneratorAPI.Builders
{
    public interface IXmlBuilder
    {
        IXmlBuilder Reset();
        IXmlBuilder AddEpcisHeader(DateTime creationDate);
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

    public class EpcisXmlBuilder : IXmlBuilder
    {
        private readonly StringBuilder _xml;
        private XmlWriter _writer;
        private readonly MemoryStream _stream;
        private EventType _eventType;
        private bool _isEventStarted;

        public EpcisXmlBuilder()
        {
            _stream = new MemoryStream();
            _xml = new StringBuilder();
            Reset();
        }

        public IXmlBuilder Reset()
        {
            _stream.SetLength(0);
            _xml.Clear();

            var settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "  ",
                OmitXmlDeclaration = false,
                Encoding = Encoding.UTF8
            };

            _writer = XmlWriter.Create(_stream, settings);
            _isEventStarted = false;
            return this;
        }

        public IXmlBuilder SetEventType(EventType eventType)
        {
            _eventType = eventType;
            return this;
        }

        public IXmlBuilder AddEpcisHeader(DateTime creationDate)
        {
            _writer.WriteStartElement("epcis", "EPCISDocument", "urn:epcglobal:epcis:xsd:2");

            _writer.WriteAttributeString("xmlns", "cbvmda", null, "urn:epcglobal:cbv:mda");


            _writer.WriteAttributeString("schemaVersion", "2.0");
            _writer.WriteAttributeString("creationDate", creationDate.ToString("yyyy-MM-ddTHH:mm:sszzz"));

            _writer.WriteStartElement("EPCISBody");
            _writer.WriteStartElement("EventList");

            return this;
        }

        private void StartEventIfNeeded()
        {
            if (!_isEventStarted)
            {
                string eventName = _eventType switch
                {
                    EventType.Object => "ObjectEvent",
                    EventType.Aggregation => "AggregationEvent",
                    EventType.Transaction => "TransactionEvent",
                    EventType.Transformation => "TransformationEvent",
                    _ => "ObjectEvent"
                };

                _writer.WriteStartElement(eventName);
                _isEventStarted = true;
            }
        }

        public IXmlBuilder AddEventTime(DateTime eventTime)
        {
            StartEventIfNeeded();
            _writer.WriteElementString("eventTime", eventTime.ToString("yyyy-MM-ddTHH:mm:sszzz"));
            return this;
        }

        public IXmlBuilder AddRecordTime(string recordTime)
        {
            StartEventIfNeeded();
            if (!string.IsNullOrWhiteSpace(recordTime))
            {
                _writer.WriteElementString("recordTime", recordTime);
            }
            return this;
        }

        public IXmlBuilder AddEventTimeZoneOffset(string offset)
        {
            StartEventIfNeeded();
            _writer.WriteElementString("eventTimeZoneOffset", offset);
            return this;
        }

        public IXmlBuilder AddErrorDeclaration(DateTime declarationTime, string reason, string correctiveEventId)
        {
            StartEventIfNeeded();
            _writer.WriteStartElement("errorDeclaration");
            _writer.WriteElementString("declarationTime", declarationTime.ToString("yyyy-MM-ddTHH:mm:ss'Z'"));
            _writer.WriteElementString("reason", reason);

            _writer.WriteStartElement("correctiveEventIDs");
            _writer.WriteElementString("correctiveEventID", correctiveEventId);
            _writer.WriteEndElement(); // correctiveEventIDs

            _writer.WriteEndElement(); // errorDeclaration
            return this;
        }

        public IXmlBuilder AddEventId(string eventId)
        {
            StartEventIfNeeded();
            if (!string.IsNullOrWhiteSpace(eventId))
            {
                _writer.WriteElementString("eventID", eventId);
            }
            return this;
        }

        public IXmlBuilder AddParentId(string parentId)
        {
            StartEventIfNeeded();
            if (!string.IsNullOrWhiteSpace(parentId))
            {
                _writer.WriteElementString("parentID", parentId);
            }
            return this;
        }

        public IXmlBuilder AddChildEPCs(List<string> childEpcs)
        {
            StartEventIfNeeded();
            if (childEpcs != null && childEpcs.Any())
            {
                _writer.WriteStartElement("childEPCs");
                foreach (var epc in childEpcs)
                {
                    _writer.WriteElementString("epc", epc);
                }
                _writer.WriteEndElement(); // childEPCs
            }
            return this;
        }

        public IXmlBuilder AddEpcList(List<string> epcs)
        {
            StartEventIfNeeded();
            if (epcs != null && epcs.Any())
            {
                _writer.WriteStartElement("epcList");
                foreach (var epc in epcs)
                {
                    _writer.WriteElementString("epc", epc);
                }
                _writer.WriteEndElement(); // epcList
            }
            return this;
        }

        public IXmlBuilder AddAction(string action)
        {
            StartEventIfNeeded();
            _writer.WriteElementString("action", action);
            return this;
        }

        public IXmlBuilder AddBizStep(string bizStep)
        {
            StartEventIfNeeded();
            _writer.WriteElementString("bizStep", bizStep);
            return this;
        }

        public IXmlBuilder AddDisposition(string disposition)
        {
            StartEventIfNeeded();
            _writer.WriteElementString("disposition", disposition);
            return this;
        }

        public IXmlBuilder AddReadPoint(string readPoint)
        {
            StartEventIfNeeded();
            if (!string.IsNullOrWhiteSpace(readPoint))
            {
                _writer.WriteStartElement("readPoint");
                _writer.WriteElementString("id", readPoint);
                _writer.WriteEndElement(); // readPoint
            }
            return this;
        }

        public IXmlBuilder AddBizLocation(string bizLocation)
        {
            StartEventIfNeeded();
            if (!string.IsNullOrWhiteSpace(bizLocation))
            {
                _writer.WriteStartElement("bizLocation");
                _writer.WriteElementString("id", bizLocation);
                _writer.WriteEndElement(); // bizLocation
            }
            return this;
        }

        public IXmlBuilder AddSourceList(string sourceType, string sourceValue)
        {
            StartEventIfNeeded();
            if (!string.IsNullOrWhiteSpace(sourceValue))
            {
                _writer.WriteStartElement("sourceList");
                _writer.WriteStartElement("source");
                _writer.WriteAttributeString("type", sourceType);
                _writer.WriteString(sourceValue);
                _writer.WriteEndElement(); // source
                _writer.WriteEndElement(); // sourceList
            }
            return this;
        }

        public IXmlBuilder AddDestinationList(string destType, string destValue)
        {
            StartEventIfNeeded();
            if (!string.IsNullOrWhiteSpace(destValue))
            {
                _writer.WriteStartElement("destinationList");
                _writer.WriteStartElement("destination");
                _writer.WriteAttributeString("type", destType);
                _writer.WriteString(destValue);
                _writer.WriteEndElement(); // destination
                _writer.WriteEndElement(); // destinationList
            }
            return this;
        }

        public IXmlBuilder AddIlmd(string lotNumber, DateOnly expirationDate)
        {
            StartEventIfNeeded();
            if (!string.IsNullOrWhiteSpace(lotNumber) || expirationDate != default)
            {
                _writer.WriteStartElement("ilmd");

                if (!string.IsNullOrWhiteSpace(lotNumber))
                {
                    _writer.WriteElementString("cbvmda", "lotNumber", "urn:epcglobal:cbv:mda", lotNumber);
                }

                if (expirationDate != default)
                {
                    _writer.WriteElementString("cbvmda", "itemExpirationDate", "urn:epcglobal:cbv:mda",
                        expirationDate.ToString("yyyy-MM-dd"));
                }

                _writer.WriteEndElement(); // ilmd
            }
            return this;
        }

        public IXmlBuilder AddQuantityList(string epcClass, double quantity, string uom)
        {
            StartEventIfNeeded();
            if (!string.IsNullOrWhiteSpace(epcClass))
            {
                _writer.WriteStartElement("quantityList");
                _writer.WriteStartElement("quantityElement");
                _writer.WriteElementString("epcClass", epcClass);
                _writer.WriteElementString("quantity", quantity.ToString("F1"));
                _writer.WriteElementString("uom", uom);
                _writer.WriteEndElement(); // quantityElement
                _writer.WriteEndElement(); // quantityList
            }
            return this;
        }

        public IXmlBuilder AddCustomExtension(string xmlNamespace, string elementName, string value)
        {
            StartEventIfNeeded();
            if (!string.IsNullOrWhiteSpace(elementName) && !string.IsNullOrWhiteSpace(value))
            {
                _writer.WriteElementString(elementName, xmlNamespace, value);
            }
            return this;
        }

        public string Build()
        {
            if (_isEventStarted)
            {
                _writer.WriteEndElement(); // ObjectEvent/AggregationEvent/etc
            }

            _writer.WriteEndElement(); // EventList
            _writer.WriteEndElement(); // EPCISBody
            _writer.WriteEndElement(); // EPCISDocument

            _writer.Flush();
            _stream.Position = 0;

            using (var reader = new StreamReader(_stream, Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
