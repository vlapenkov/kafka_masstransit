using System;
using System.Collections.Generic;

namespace KafkaMasstransitWebApp2.Events
{
    public record VideoCreatedEvent
    {
        public string Title { get; set; }

        public IEnumerable<decimal> SomeData { get; set; } = Array.Empty<decimal>();
    }
}
