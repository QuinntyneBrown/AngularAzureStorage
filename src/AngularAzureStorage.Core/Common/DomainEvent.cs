using MediatR;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AngularAzureStorage.Core.Common
{
    public class DomainEvent: INotification
    {
        public Guid DomainEventId { get; set; }
        public string EventType { get; set; }
        public string Payload { get; protected set; }
        public string Subject { get; set; }
        public DateTime EventTime { get; set; }
        public string DataVersion { get; set; }
        [NotMapped]
        public dynamic EventData { get; set; }

    }
}
