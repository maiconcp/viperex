using System;
using System.Linq;
using System.Collections.Generic;
using Flunt.Notifications;

namespace Viper.Common
{
    public class DomainException : Exception
    {        
        public IReadOnlyCollection<Notification> Notifications { get; private set; }
        public DomainException(IReadOnlyCollection<Notification> notifications) 
            : base(string.Join(Environment.NewLine, notifications.Select(s => s.Message)))
        {
            Notifications = notifications;
        }

        public DomainException(IReadOnlyCollection<Notification> notifications, Exception innerException) 
            : base(string.Join(Environment.NewLine, notifications.Select(s => s.Message)), innerException)
        {
            Notifications = notifications;
        }
    }
}